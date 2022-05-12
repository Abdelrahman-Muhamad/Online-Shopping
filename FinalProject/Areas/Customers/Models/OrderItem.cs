using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Items")]
        public int ItemId { get; set; }
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
    }
}
