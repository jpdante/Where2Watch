using HtcSharp.HttpModule.Routing;

namespace Where2Watch.Mvc {
    public class HttpPostAttribute : HttpMethodAttribute {

        public HttpPostAttribute(string path, ContentType contentType = ContentType.DEFAULT, bool requireSession = false) : base("POST", path, contentType, requireSession) { }

    }
}
