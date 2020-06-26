using System.Collections.Generic;

namespace Blog.MVP.Blazor.SSR.Extensions
{
    public class UserInfoModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public bool Expired { get; set; }
        public UserProfileModel Profile { get; set; } = new UserProfileModel();
    }

    public class UserProfileModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }

    public class ContentHeaderModel
    {
        public string ContentHeader { get; set; } = "Dashboard";
        public List<BreadcrumbItem> BreadcrumbItems { get; set; }
        public ContentHeaderModel()
        {
            BreadcrumbItems = new List<BreadcrumbItem> {
                new BreadcrumbItem { Text = "Home", Route = "#" },
                new BreadcrumbItem{ Text = "Dashboard", Route = "dashboard" }
            };
        }
    }

    public class BreadcrumbItem
    {
        public string Text { get; set; }
        public string Route { get; set; }
    }
}
