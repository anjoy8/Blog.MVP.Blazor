using System;
using System.Collections.Generic;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class UserInfoModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public long ExpireTimeTamp { get; set; }
        public UserProfileModel Profile { get; set; } = new UserProfileModel();

        private DateTime _ex;

        /// <summary>
        /// 当前时间-过期时间
        /// 如果true，代表已经过期了
        /// </summary>
        /// <returns></returns>
        public bool IsExpired() => DateTime.Now.Time1SubTIme2(ExpireTimeTamp.TimeStampToDateTime());
        public DateTime ExpireTime
        {
            get
            {
                return _ex;
            }
            
            set
            {
                _ex = ExpireTimeTamp.TimeStampToDateTime();
                value = _ex;
            }
        }
    }

    public class UserProfileModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}
