﻿using System.Threading.Tasks;
using HtcSharp.HttpModule.Http.Abstractions;
using HtcSharp.HttpModule.Http.Abstractions.Extensions;
using Newtonsoft.Json;

namespace Where2Watch.Extensions {
    public static class DefaultResponseExtension {

        public static async Task SendRequestErrorAsync(this HttpResponse httpResponse, int errorCode, string message) {
            httpResponse.StatusCode = 400;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{errorCode},\"message\":\"{message}\"}}}}");
        }

        public static async Task SendRequestErrorAsync(this HttpResponse httpResponse, int errorCode, string message, object data) {
            httpResponse.StatusCode = 400;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{errorCode},\"message\":\"{message}\",\"data\":{JsonConvert.SerializeObject(data)}}}}}");
        }

        public static async Task SendInvalidSessionErrorAsync(this HttpResponse httpResponse) {
            httpResponse.StatusCode = 403;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{403},\"invalidSession\":\"true\"}}}}");
        }

        public static async Task SendInternalErrorAsync(this HttpResponse httpResponse, int errorCode, string message) {
            httpResponse.StatusCode = 500;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{errorCode},\"message\":\"{message}\"}}}}");
        }

        public static async Task SendInternalErrorAsync(this HttpResponse httpResponse, int errorCode, string message, object data) {
            httpResponse.StatusCode = 500;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{errorCode},\"message\":\"{message}\",\"data\":{JsonConvert.SerializeObject(data)}}}}}");
        }

        public static async Task SendInvalidRequestMethodErrorAsync(this HttpResponse httpResponse, string method) {
            httpResponse.StatusCode = 405;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":405,\"message\":\"Invalid request method, expected '{method}'.\"}}}}");
        }

        public static async Task SendDecodeErrorAsync(this HttpResponse httpResponse) {
            httpResponse.StatusCode = 400;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":400,\"message\":\"Failed to decode request data.\"}}}}");
        }

        public static async Task SendErrorAsync(this HttpResponse httpResponse, int status, string message) {
            httpResponse.StatusCode = status;
            await httpResponse.WriteAsync($"{{\"error\":{{\"code\":{status},\"message\":\"{message}\"}}}}");
        }

    }
}
