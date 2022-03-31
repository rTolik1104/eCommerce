using System.ComponentModel.DataAnnotations;

namespace BigShop.Models
{
    public class Categories
    {
        [Key]
        public Guid CategoryId { get; set; }= Guid.NewGuid();
        public string? CategoryName { get; set; }
    }
}
