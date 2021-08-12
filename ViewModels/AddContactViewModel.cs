using System.ComponentModel.DataAnnotations;
 
namespace Vchat.ViewModels{
    public class AddContactViewModel{
        [Required]
        [Display(Name = "Email")]
        public string Id {get; set; } 
    }
}    