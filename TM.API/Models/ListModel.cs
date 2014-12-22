using System;
using System.Collections.Generic;
using System.Linq;

namespace TM.API.Models
{
    public class ListModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<CardModel> Cards { get; set; }

        public static ListModel Map(Domain.Models.ListModel model)
        {
            if (model == null)
                return new ListModel();

            return new ListModel
            {
                Id = model.Id,
                BoardId = model.BoardId,
                Name = model.Name,
                Position = model.Position,
                IsDeleted = model.IsDeleted,
                LastModifiedDate = model.LastModifiedDate,
                LastModifiedBy = model.LastModifiedBy,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Cards = model.Cards.Select(CardModel.Map)
            };
        }

        public static Domain.Models.ListModel Map(ListModel model)
        {
            if (model == null)
                return new Domain.Models.ListModel();

            return new Domain.Models.ListModel
            {
                Id = model.Id,
                BoardId = model.BoardId,
                Name = model.Name,
                Position = model.Position
            };
        }
    }
}