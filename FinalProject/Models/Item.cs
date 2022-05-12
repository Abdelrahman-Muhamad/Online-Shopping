using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photopath { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Details { get; set; }
        [Required]
        public int InStock { get; set; }

        [Required, ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderItem> Orders { get; set; } = new HashSet<OrderItem>();


    }
}
