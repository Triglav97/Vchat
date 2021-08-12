using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
 
namespace Vchat.Models{
    public class CryptKeysModel{
        public string Id { get; set; }
        public string encryptKey { get; set; }
        public string decryptKey { get; set; }
        
        public string UserId {get; set; }
        public User User {get; set; }
    }
}