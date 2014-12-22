using System;

namespace TM.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? LastModifiedBy { get; set; }
    }
}
