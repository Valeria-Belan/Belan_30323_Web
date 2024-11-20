using Belan_30323.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Belan_30323.UI.Controllers
{
    //public class ImageController(UserManager<AppUser> userManager) : Controller
    //{
    //    public async Task<IActionResult> GetAvatar()
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            var email = User.FindFirst(ClaimTypes.Email).Value;
    //            var user = await userManager.FindByEmailAsync(email);
    //            if(user.Avatar is not null)
    //            {
    //                return File(user.Avatar, "image/*");
    //            }
    //        }
    //        return File("user.Avatar", "image/*");
    //    }
    //}

    public class ImageController(UserManager<AppUser>userManager) : Controller
    {
        public async Task<IActionResult> GetAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Avatar != null)
            {
                return File(user.Avatar, "image/*");
            }
            var imagePath = Path.Combine("Images", "default-profile-picture.png");
            return File("user.Avatar", "image/*");
        }        
    }
}

