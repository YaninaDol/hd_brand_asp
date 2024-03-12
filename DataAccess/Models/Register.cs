using System.ComponentModel.DataAnnotations;

namespace RepositoriesLibrary.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Email  is  Required! ")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "User password is  Required! ")]
        public string? Password { get; set; }
      
       
    }
}
