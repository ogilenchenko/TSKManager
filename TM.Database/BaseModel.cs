using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TM.Domain")]
namespace TM.Database
{
    public abstract class BaseModel
    {
        public bool IsDeleted { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
