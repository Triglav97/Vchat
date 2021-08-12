using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
 
namespace Vchat.Models{
    public class User : IdentityUser{
        
        public int Year { get; set; }

        public CryptKeysModel CryptKey { get; set; }
        public ICollection <CryptMessagesModel> CryptMessages {get; set; }
        public ICollection <MyContactsModel> MyContacts {get; set;}
    }
}