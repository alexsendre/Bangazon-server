using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
