using System;

namespace TM.Domain.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
