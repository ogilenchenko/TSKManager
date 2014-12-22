using System;
using System.Collections.Generic;

namespace TM.Domain.Models
{
    public class BoardModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public bool IsStarred { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<ListModel> Lists { get; set; }
    }
}
