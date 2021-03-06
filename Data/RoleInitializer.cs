using Vchat.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
 
namespace Vchat.Data{
    public class RoleInitializer{

        public static async Task InitializeAsync(
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager){
            
            string adminEmail = "admin@mail.ru";
            string password = "1234567890";
            if (await roleManager.FindByNameAsync("admin") == null){
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null){
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("donate") == null){
                await roleManager.CreateAsync(new IdentityRole("donate"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null){
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded){
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}