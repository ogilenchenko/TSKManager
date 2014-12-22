using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TM.Domain")]
namespace TM.Database
{
    public abstract class IntPrimaryKey : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
