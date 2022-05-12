using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Photo { get; set; }
       

        public virtual ICollection<Item> Items { get; set; }  = new HashSet<Item>();
    }
}
