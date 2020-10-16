﻿using System;
using HtcSharp.HttpModule.Routing;

namespace Where2Watch.Mvc {
    public class HttpMethodAttribute : Attribute {

        public string Method { get; }

        public string Path { get; }

        public string RequireContentType { get; }

        public bool RequireSession { get; }

        public HttpMethodAttribute(string method, string path, ContentType requireContentType, bool requireSession) {
            Method = method;
            Path = path;
            RequireContentType = requireContentType == ContentType.DEFAULT ? null : requireContentType.ToValue();
            RequireSession = requireSession;
        }
    }
}
