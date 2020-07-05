using System;

namespace Blog.MVP.Blazor.SSR.Models
{
    public class ServerAuthModel
    {
        public string SubjectId { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
