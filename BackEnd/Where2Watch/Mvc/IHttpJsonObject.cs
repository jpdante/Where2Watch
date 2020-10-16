using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;

namespace Where2Watch.Mvc {
    public interface IHttpJsonObject {

        public Task<bool> ValidateData(HttpContext httpContext);

    }
}