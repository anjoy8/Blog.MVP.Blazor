using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public static class JSRuntimeExtensions
    {
        public async static Task SignInAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("users.startSigninMainWindow");
        }
        public async static Task<string> GetTokenFromStorage(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<string>("users.getTokenFromStorage");
        }

        public async static Task SetTokenToStorage(this IJSRuntime jsRuntime, string token)
        {
            await jsRuntime.InvokeAsync<string>("users.setTokenToStorage", token);
        }

        public async static Task SignOutAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("users.startSignoutMainWindow");
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
