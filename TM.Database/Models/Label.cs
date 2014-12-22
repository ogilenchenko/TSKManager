using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.Database.Models
{
    [Table("Labels")]
    public class Label : IntPrimaryKey
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int Position { get; set; }
    }
}