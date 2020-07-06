using System;

namespace Blog.MVP.Blazor.SSR.Models
{
    public class ServerAuthModel
    {
        /// <summary>
        /// sid 当前用户标识
        /// </summary>
        public string SubjectId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTimeOffset Expiration { get; set; }
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
