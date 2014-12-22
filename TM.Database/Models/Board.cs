using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TM.Database.Models
{
    [Table("Boards")]
    public class Board : IntPrimaryKey
    {
        [ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Position { get; set; }

        public bool IsStarred { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual ICollection<List> Lists { get; set; }
    }
}