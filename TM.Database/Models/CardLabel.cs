using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.Database.Models
{
    [Table("CardLabel")]
    public class CardLabel
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Card"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CardId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Label"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LabelId { get; set; }

        public virtual Card Card { get; set; }

        public virtual Label Label { get; set; }
    }
}
