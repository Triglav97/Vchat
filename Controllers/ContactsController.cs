using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vchat.ViewModels;
using Vchat.Models;
using Microsoft.AspNetCore.Identity;
using Vchat.Data;
using System.Collections.Generic;
using System;

using System.Linq;
 
namespace Vchat.Controllers{
    public class ContactsController : Controller{

        UserManager<User> _userManager;
        //UserManager<User> _myContacts;
 
        public ContactsController(UserManager<User> userManager){
            // foreach(var users in db.Users){
            //     userManager.Users.
            // }
            // User user = db.Users(_userManager.GetUserId(User));
            // userManager.UpdateAsync(db.Users);
            _userManager = userManager;
            //_myContacts = myContacts;
        }
        
        public IActionResult Index(){
            List <User> mcm = new List <User>();
            using (ApplicationDbContext db = new ApplicationDbContext()){
                foreach (var mc in db.MyContacts){
                    if (mc.UserId == _userManager.GetUserId(User)){
                        foreach(var us in db.Users){
                            if(us.Id == mc.ConnectUser){
                                mcm.Add(us);
                            }
                        }
                    }
                }
                return View(mcm);
            }
        }
        
        
        public IActionResult AddContacts(){   
            List <User> uss = new List <User>();
            List <User> mus = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext()){
                uss = db.Users.ToList();
                //var user = _userManager.FindByIdAsync(_userManager.GetUserId(User));
                foreach (var u in uss){
                    if ((u.Id == _userManager.GetUserId(User))){
                        uss.Remove(u);
                        break;
                    }
                }
                foreach (var mc in db.MyContacts){
                    if(mc.UserId == _userManager.GetUserId(User)){
                        foreach(var us in db.Users){
                            if (us.Id == mc.ConnectUser){
                                mus.Add(us);
                            }
                        } 
                    }
                }
                foreach(var mu in mus){
                    foreach (var u in uss){
                        if (u == mu){
                            uss.Remove(u);
                            break;
                        }
                    }
                }
                return View(uss); 
            } 
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(string id){
            GenerateId gen = new GenerateId();
            MyContactsModel myContacts = new MyContactsModel{Id = gen.GenRandomString(10), ConnectUser = id, UserId = _userManager.GetUserId(User)};
            MyContactsModel myContacts_rev = new MyContactsModel{Id = gen.GenRandomString(10), ConnectUser = _userManager.GetUserId(User), UserId = id};
            
            MyContactsViewModel myContactsViewModel = new MyContactsViewModel();
            
            // myContactsViewModel.UserId = _userManager.GetUserId(User);
            // using(ApplicationDbContext db = new ApplicationDbContext()){
            //     foreach(var myContact in db.MyContacts){
            //         if (myContact.UserId == myContactsViewModel.UserId){
            //             myContactsViewModel.MyContacts.Add(myContact);
            //             //db.Users.
            //         }
            //     }
            // }
            

            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            Add_contacts(user, myContacts);
            user = await _userManager.FindByIdAsync(id);
            Add_contacts(user, myContacts_rev);
            
            return RedirectToAction("AddContacts");
        }

        public async void Add_contacts(User user, MyContactsModel myContacts){
            ICollection <MyContactsModel> list = new List<MyContactsModel>();
            if (user.MyContacts != null){
                list = user.MyContacts;
            }
            int error = 0;
            // foreach (var i in list){     
            //     if ((i.ConnectUser == _userManager.GetUserId(User))||
            //             i.UserId == _userManager.GetUserId(User) && i.ConnectUser == id){
            //         error=+1;
            //     }
            // }
            if (error == 0){
                list.Add(myContacts);
                user.MyContacts = list;
                using (ApplicationDbContext db = new ApplicationDbContext()){
                    var result = await _userManager.UpdateAsync(user);
                    db.SaveChanges();
                } 
            }
        }

        [HttpGet]
        public async Task<IActionResult> Messanges(string Id){
            //ДОПИСАТЬ
            string UserId = _userManager.GetUserId(User);
            string ContactId = Id;
            List <CryptMessagesModel> list = new List <CryptMessagesModel>();
            using(ApplicationDbContext db = new ApplicationDbContext()){
                foreach(var u in db.CryptMessages){
                    if(((u.UserId == UserId) && (u.ToSendId == ContactId)) || ((u.UserId == ContactId) && (u.ToSendId == UserId))){
                        list.Add(u);//отсортировать по времени, лист сунуть в muvm, в html если 
                    }  
                }
            }
            if (list.Count > 0){
                string prk_contact = "", prk_user = "";
                using(ApplicationDbContext db = new ApplicationDbContext()){
                    foreach (var list_crypt in db.CryptKeys){
                        if(list_crypt.UserId == Id){
                            prk_contact = list_crypt.decryptKey;//test 11
                        }
                        if(list_crypt.UserId == UserId){
                            prk_user = list_crypt.decryptKey; //test 10
                        }
                    }
                }
                List <CryptMessagesModel> buf_list = new List <CryptMessagesModel>();
                buf_list.Add(list[0]);
                for (int i=0; i<list.Count; i++){
                    for(int j=i+1; j<list.Count; j++){
                        if(DateTime.Parse(list[i].SendTime) > DateTime.Parse(list[j].SendTime)){
                            buf_list[0] = list[i];
                            list[i]=list[j];
                            list[j] = buf_list[0];
                        }
                    }
                }
                buf_list.Clear();
                CryptKey crypt = new CryptKey();
                for (int i=0; i<list.Count; i++){
                    if(UserId == list[i].UserId){
                        if(list[i].ToSendId == Id){
                            list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact);
                        }
                        else{
                            list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_user);
                        }
                    }
                    else if(UserId == list[i].ToSendId){
                        if(list[i].UserId == Id){
                            list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_user);
                        }
                        else{
                            list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact);
                        }
                    }
                }

                // CryptKey crypt = new CryptKey();
                // for (int i=0; i<list.Count; i++){
                //     if(UserId == list[i].UserId){
                //         if(list[i].ToSendId == Id){
                //             string test = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false);
                //             list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false); 
                //         }
                //         else if(list[i].UserId == UserId){
                //             string test_2 = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false);
                //             list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_user, false); 
                //         }
                //     }
                //     else{
                //         if(list[i].ToSendId == UserId){
                //             string test = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false);
                //             list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_user, false); 
                //         }
                //         else if(list[i].UserId == Id){
                //             string test_2 = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false);
                //             list[i].Cryptmessage = crypt.RSADecrypt(list[i].Cryptmessage, prk_contact, false); 
                //         }
                //     }
                    
                // }
                
            }
            
            MessangerUserViewModel muvm = new MessangerUserViewModel();
            User user = await _userManager.FindByIdAsync(Id);
            muvm.UserName = user.UserName;
            muvm.ContactId = Id;
            muvm.Listmess = list;
            
            return View(muvm);
        }

        [HttpPost]
        public async Task<IActionResult> Messanges(MessangerUserViewModel muvm){
            //что надо спиздить: сообщение, юзерайди, контактайди, время, все
            GenerateId gen = new GenerateId();
            CryptKey crypt = new CryptKey();
            string puk = "";
            using(ApplicationDbContext db = new ApplicationDbContext()){
                foreach (var list_crypt in db.CryptKeys){
                    if(list_crypt.Id == muvm.UserName){
                        puk = list_crypt.encryptKey;
                    }
                }
            }
            CryptMessagesModel cmm =new CryptMessagesModel{
                Id = gen.GenRandomString(20), ToSendId = muvm.ContactId,
                Cryptmessage = crypt.RSAEncrypt(muvm.Messange, puk),
                SendTime = DateTime.Now.ToString(), UserId = _userManager.GetUserId(User)};

            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            ICollection <CryptMessagesModel> list = new List <CryptMessagesModel>();
            
            List <int> i = new List <int>();
            //list = user.CryptMessages;
            list.Add(cmm);
            user.CryptMessages = list;
            using (ApplicationDbContext db = new ApplicationDbContext()){
                var result = await _userManager.UpdateAsync(user);
                db.SaveChanges();
            }
            // cmm.ToSendId = muvm.ContactId;
            // cmm.UserId = _userManager.GetUserId(User);
            // cmm.SendTime = DateTime.Now.ToString();
            // cmm.Id = gen.GenRandomString(20);
            // cmm.Cryptmessage = muvm.Messange;
            return RedirectToAction("Messanges", new { Id = muvm.ContactId });
        }






    }
}