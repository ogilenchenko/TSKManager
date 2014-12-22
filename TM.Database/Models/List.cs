using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.Database.Models
{
    [Table("Lists")]
    public class List : IntPrimaryKey
    {
        [ForeignKey("Board"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BoardId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Board Board { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}