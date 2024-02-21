using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public int QuantityAvailable { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
    }
}
