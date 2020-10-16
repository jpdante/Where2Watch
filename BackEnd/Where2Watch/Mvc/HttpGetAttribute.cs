using HtcSharp.HttpModule.Routing;

namespace Where2Watch.Mvc {
    public class HttpGetAttribute : HttpMethodAttribute {

        public HttpGetAttribute(string path, bool requireSession = false) : base("GET", path, ContentType.DEFAULT, requireSession) { }

    }
}
