using Vchat.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vchat.Data{
    public class ServiceCryptKey{

        public void GetCryptKeys(UserManager<User> userManager){
            using(ApplicationDbContext db = new ApplicationDbContext()){
                foreach(User user in userManager.Users){
                    foreach(CryptKeysModel cryptKeys in db.CryptKeys){
                        if(cryptKeys.UserId == user.Id){
                            user.CryptKey = cryptKeys;
                        }
                    }
                }
            }
        }
    }
}