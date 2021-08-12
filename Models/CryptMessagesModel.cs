using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
 
namespace Vchat.Models{
    public class CryptMessagesModel{
        public string Id {get; set; }
        
        public string ToSendId{get; set; }
        public string Cryptmessage {get; set; }
        public string SendTime {get; set; } 

        public string UserId {get; set; }
        public User User {get; set; }
    }
}