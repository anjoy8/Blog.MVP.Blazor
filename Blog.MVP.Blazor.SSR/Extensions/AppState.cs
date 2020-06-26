using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class AppState
    {
        private readonly IJSRuntime _jS;

        public AppState(IJSRuntime JS)
        {
            _jS = JS;
        }

        public event Action OnChange;

        public ContentHeaderModel ContentHeader { get; private set; } = new ContentHeaderModel();
        public UserInfoModel UserInfo { get; private set; } = new UserInfoModel();

        public void SetUserInfo(UserInfoModel userInfo)
        {

            UserInfo = userInfo;
            NotifyStateChanged();
        }

        public void SetContentHeader(string contentHeader, IEnumerable<BreadcrumbItem> breadcrumbItems)
        {
            ContentHeader = new ContentHeaderModel
            {
                ContentHeader = contentHeader,
                BreadcrumbItems = breadcrumbItems.ToList()
            };
            NotifyStateChanged();
        }

        public async Task<string> GetAccessToken()
        {
            var token = string.Empty;
            try
            {
                token = UserInfo.AccessToken.IsNotEmptyOrNull() ? UserInfo.AccessToken : await _jS.GetTokenFromStorage();

                if (!token.IsNotEmptyOrNull())
                {
                    await _jS.SignInAsync();
                }
            }
            catch (Exception)
            {
            }

            return token;
        }

        public bool IsLogin()
        {
            return !string.IsNullOrEmpty(UserInfo.AccessToken) && !UserInfo.Expired;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
