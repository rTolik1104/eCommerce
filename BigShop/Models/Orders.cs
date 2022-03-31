using System.ComponentModel.DataAnnotations;

namespace BigShop.Models
{
    public class Orders
    {
        [Key]
        public Guid OrderId { get; set; }= Guid.NewGuid();
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? Created { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
        public string? ProductName { get; set; }
        public int? Count { get; set; }
        public bool? IsComplete { get; set; }
    }
}
