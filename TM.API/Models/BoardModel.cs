using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TM.API.Models
{
    public class BoardModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public bool IsStarred { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<ListModel> Lists { get; set; }

        public static BoardModel Map(Domain.Models.BoardModel model)
        {
            if (model == null)
                return new BoardModel();

            return new BoardModel
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Position = model.Position,
                IsStarred = model.IsStarred,
                IsDeleted = model.IsDeleted,
                LastModifiedDate = model.LastModifiedDate,
                LastModifiedBy = model.LastModifiedBy,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Lists = model.Lists.Select(ListModel.Map)
            };
        }

        public static Domain.Models.BoardModel Map(BoardModel model)
        {
            if (model == null)
                return new Domain.Models.BoardModel();

            return new Domain.Models.BoardModel
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Position = model.Position,
                IsStarred = model.IsStarred,
            };
        }
    }
}