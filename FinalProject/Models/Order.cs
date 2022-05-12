 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required, ForeignKey("AspNetUsers")]
        public string CustomerID { get; set; }
        public string details { get; set; }

       // public virtual IdentityUser Customer { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    }
}
