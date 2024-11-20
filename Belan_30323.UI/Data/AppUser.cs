using Microsoft.AspNetCore.Identity;

namespace Belan_30323.UI.Data
{
    public class AppUser: IdentityUser
    {
        public byte[]? Avatar { get; set; }
        public string MimeType { get; set; } = string.Empty;
    }
}
