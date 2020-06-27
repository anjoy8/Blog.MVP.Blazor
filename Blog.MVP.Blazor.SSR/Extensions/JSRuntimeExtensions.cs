using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    /// <summary>
    /// JSRuntime扩展类
    /// 用来调取app.js文件
    /// </summary>
    public static class JSRuntimeExtensions
    {
        public async static Task SignInAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("users.startSigninMainWindow");
        }
        public async static Task<UserInfoModel> GetUserInfoFromStorage(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("users.getUserInfoFromStorage");
        }

        public async static Task SetUserInfoToStorage(this IJSRuntime jsRuntime, UserInfoModel userInfoModel)
        {
            await jsRuntime.InvokeAsync<UserInfoModel>("users.setUserInfoToStorage", userInfoModel);
        }

        public async static Task SignOutAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("users.startSignoutMainWindow");
        }

        public async static Task<UserInfoModel> CallbackAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("users.endSigninMainWindow");
        }

        public async static Task<UserInfoModel> GetUserInfoAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<UserInfoModel>("users.getUserInfo");
        }

        public async static Task LogAsync(this IJSRuntime jsRuntime, object output)
        {
            await jsRuntime.InvokeAsync<bool>("users.log", JsonConvert.SerializeObject(output));
        }


    }

}
