using System.Collections.Generic;
using Vchat.Models;

namespace Vchat.ViewModels{
    public class MessangerUserViewModel{
        public string ContactId { get; set; }
        public string UserName {get; set; }
        public string Messange {get; set; } 
        public List <CryptMessagesModel> Listmess {get; set; }
    }
}