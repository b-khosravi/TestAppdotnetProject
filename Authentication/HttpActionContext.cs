using System.Net.Http;

namespace TestApp.Authentication
{
    public class HttpActionContext
    {
        public HttpResponseMessage Response { get; internal set; }
    }
}