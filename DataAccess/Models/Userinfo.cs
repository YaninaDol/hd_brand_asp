using System.ComponentModel.DataAnnotations;

namespace RepositoriesLibrary.Models
{
    public class Userinfo
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public int Discount { get; set; } = 0;
    }
}
