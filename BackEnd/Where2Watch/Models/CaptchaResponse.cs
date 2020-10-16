using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Where2Watch.Models {
    public class CaptchaResponse {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorMessage { get; set; }

        public string GetErrorMessage() {
            if (ErrorMessage == null || ErrorMessage.Count == 0) return "Unknown error, hcaptcha returned empty.";
            return ErrorMessage.Aggregate("", (current, line) => current + line + Environment.NewLine);
        }

    }
}