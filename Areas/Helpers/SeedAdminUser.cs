using Microsoft.AspNetCore.Identity;

namespace Tips.Helpers
{
public static class SeedAdminUser
{
    public static void SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (userManager.FindByEmailAsync("admin@tips.com").Result==null)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "admin@tips.com",
                Email = "admin@tips.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "Aa123456!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }       
    }   
}
}