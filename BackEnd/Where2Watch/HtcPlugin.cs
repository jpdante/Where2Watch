using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtcSharp.Core.Logging.Abstractions;
using HtcSharp.Core.Plugin;
using HtcSharp.Core.Plugin.Abstractions;
using HtcSharp.HttpModule;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Routing;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using Where2Watch.Core;
using Where2Watch.Extensions;
using Where2Watch.Mvc;
using Where2Watch.Security;

namespace Where2Watch {
    public class HtcPlugin : IPlugin, IHttpEvents {
        public string Name => "Where2Watch";
        public string Version => Where2Watch.Version.GetVersion();

        private Dictionary<string, (HttpMethodAttribute, bool, Type, MethodInfo)> _routes;

        internal static ILogger Logger { get; private set; }
        internal static Config Config { get; private set; }
        internal static ConnectionMultiplexer RedisConnection { get; private set; }
        internal static IDatabase Redis { get; private set; }
        internal static ElasticClient ElasticClient;
        //internal static SearchEngine TitleSearch { get; private set; }

        public async Task Load(PluginServerContext pluginServerContext, ILogger logger) {
            Logger = logger;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            string configPath = Path.Combine(pluginServerContext.PluginsPath, "w2w-data", "config.json");
            if (!File.Exists(configPath)) {
                Config = Config.NewConfig();
                await File.WriteAllTextAsync(configPath, JsonConvert.SerializeObject(Config));
            }
            Config = JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync(configPath));

            DatabaseContext.SetConnectionString($"server={Config.Db.Host};port={Config.Db.Port};database={Config.Db.Database};user={Config.Db.Username};password={Config.Db.Password}");
            _ = new Captcha(Config.CaptchaKey);
            LoadControllers();

            //TitleSearch = new SearchEngine();
            //await TitleSearch.Configure(Path.Combine(pluginServerContext.PluginsPath, "w2w-data", "index"));
            var elasticSettings = new ConnectionSettings(new Uri("http://127.0.0.1:9200"));
            ElasticClient = new ElasticClient(elasticSettings);

            var context = new DatabaseContext();
            await context.Database.MigrateAsync();
            //await context.Database.EnsureCreatedAsync();

            RedisConnection = await ConnectionMultiplexer.ConnectAsync(Config.Redis.ConnectionString);
            Redis = RedisConnection.GetDatabase(0);
        }

        public Task Enable() {
            return Task.CompletedTask;
        }

        public async Task Disable() {
            await RedisConnection.CloseAsync();
            RedisConnection.Dispose();
        }

        public bool IsCompatible(int htcMajor, int htcMinor, int htcPatch) {
            return true;
        }

        private void LoadControllers() {
            _routes = new Dictionary<string, (HttpMethodAttribute, bool, Type, MethodInfo)>();
            MethodInfo[] methods = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(HttpMethodAttribute), false).Length > 0)
                .ToArray();
            foreach (var method in methods) {
                if (method.ReturnType != typeof(Task) || !method.IsStatic) continue;
                ParameterInfo[] parameters = method.GetParameters();
                var attribute = method.GetCustomAttributes(typeof(HttpMethodAttribute), false).First() as HttpMethodAttribute;
                if (attribute == null) continue;
                if (parameters.Length < 1 || parameters[0].ParameterType != typeof(HttpContext)) continue;
                if (parameters.Length == 2 && !parameters[1].ParameterType.GetInterfaces().Contains(typeof(IHttpJsonObject))) continue;
                Logger.LogInfo(parameters.Length == 2 ? $"Registering route: {attribute.Path}, JsonObject" : $"Registering route: {attribute.Path}");
                _routes.Add(attribute.Path, (attribute, parameters.Length == 2, parameters.Length == 2 ? parameters[1].ParameterType : null, method));
                UrlMapper.RegisterPluginPage(attribute.Path, this);
            }
        }

        public async Task OnHttpPageRequest(HttpContext httpContext, string filename) {
            try {
                if (_routes.TryGetValue(filename, out var value)) {
                    if (httpContext.Request.Method.Equals(value.Item1.Method)) {
                        httpContext.Session = new Session(httpContext);
                        await httpContext.Session.LoadAsync();
                        if (value.Item1.RequireSession && !httpContext.Session.IsAvailable) {
                            await httpContext.Response.SendInvalidSessionErrorAsync();
                            return;
                        }

                        if (value.Item1.RequireContentType != null) {
                            if (httpContext.Request.ContentType == null || httpContext.Request.ContentType.Split(";")[0] != value.Item1.RequireContentType) {
                                await httpContext.Response.SendRequestErrorAsync(415, "Content-Type invalid or not recognized.");
                                return;
                            }
                        }

                        try {
                            object[] data = null;
                            if (value.Item2) {
                                using var streamReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
                                if (JsonConvertExt.TryDeserializeObject(await streamReader.ReadToEndAsync(), value.Item3, out var obj)) {
                                    if (obj is IHttpJsonObject httpJsonObject && await httpJsonObject.ValidateData(httpContext))
                                        data = new object[] { httpContext, obj };
                                    else {
                                        if (!httpContext.Response.HasStarted) await httpContext.Response.SendDecodeErrorAsync();
                                        return;
                                    }
                                } else {
                                    await httpContext.Response.SendDecodeErrorAsync();
                                }

                                streamReader.Close();
                            } else {
                                data = new object[] { httpContext };
                            }

                            // ReSharper disable once PossibleNullReferenceException
                            await (Task)value.Item4.Invoke(null, data);
                        } catch (HttpException ex) {
                            await httpContext.Response.SendErrorAsync(ex.Status, ex.Message);
                        } catch (Exception ex) {
                            if (!httpContext.Response.HasStarted) {
                                Logger.LogError($"[{httpContext.Connection.Id}]", ex);
                                Logger.LogError(ex.StackTrace);
                                await httpContext.Response.SendInternalErrorAsync(500, $"[{httpContext.Connection.Id}] An internal failure occurred. Please try again later.");
                            }
                        }
                    } else {
                        await httpContext.Response.SendInvalidRequestMethodErrorAsync(value.Item1.Method);
                    }
                }
            } catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        public Task OnHttpExtensionRequest(HttpContext httpContext, string filename, string extension) {
            return Task.CompletedTask;
        }
    }
}