using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        [Required]
        public bool IsSeller { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string Uid { get; set; }
    }
}
