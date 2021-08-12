using System.Collections.Generic;
using Vchat.Models;

namespace Vchat.ViewModels{
    public class MyContactsViewModel{
        public string UserId { get; set; }
        public List<MyContactsModel> MyContacts {get; set; }

        public MyContactsViewModel(){
            MyContacts = new List<MyContactsModel>();
        }
    }
}