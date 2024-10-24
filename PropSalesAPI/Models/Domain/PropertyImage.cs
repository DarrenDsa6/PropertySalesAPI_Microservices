using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PropertySales.Models.Domain
{
    public class PropertyImage
    {
        [Key]
        public int ImageId { get; set; }

        public string ImagePath { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
    }
}
