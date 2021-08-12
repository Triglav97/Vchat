using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
 
namespace Vchat.Models{
    public class MyContactsModel{
        public string Id {get; set; }
        
        public string ConnectUser { get; set; }
        public string UserId { get; set; }
        public User User {get; set; }
    }
}