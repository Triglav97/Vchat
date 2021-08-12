using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vchat.ViewModels;
using Vchat.Models;
using Microsoft.AspNetCore.Identity;
 
namespace Vchat.Controllers{
    public class UserManageController : Controller{

        public IActionResult Index(){
            return View();
        }

    }
}