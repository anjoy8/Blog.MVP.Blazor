using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class AccessState
    {
        private readonly IJSRuntime _jS;

        public AccessState(IJSRuntime JS)
        {
            _jS = JS;
        }

        public async Task<string> GetAccessToken()
        {
            var userInfo = await _jS.GetUserInfoFromStorage();

            if (!IsLogin(userInfo))
            {
                await _jS.SignInAsync();
            }


            return userInfo.AccessToken;
        }

        public bool IsLogin(UserInfoModel UserInfo) => UserInfo != null && UserInfo.AccessToken.IsNotEmptyOrNull() && !UserInfo.IsExpired();

    }
}
