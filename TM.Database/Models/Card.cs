using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.Database.Models
{
    [Table("Cards")]
    public class Card : IntPrimaryKey
    {
        [ForeignKey("Board"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BoardId { get; set; }

        [ForeignKey("List"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ListId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Board Board { get; set; }

        public virtual List List { get; set; }

        public virtual ICollection<CardLabel> CardLabels { get; set; }
    }
}
