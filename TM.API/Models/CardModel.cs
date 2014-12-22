using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TM.API.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public int ListId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<ListModel> Lists { get; set; }

        public static CardModel Map(Domain.Models.CardModel model)
        {
            if (model == null)
                return new CardModel();

            return new CardModel
            {
                Id = model.Id,
                BoardId = model.BoardId,
                ListId = model.ListId,
                Name = model.Name,
                Position = model.Position,
                IsDeleted = model.IsDeleted,
                LastModifiedDate = model.LastModifiedDate,
                LastModifiedBy = model.LastModifiedBy,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate
            };
        }

        public static Domain.Models.CardModel Map(CardModel model)
        {
            if (model == null)
                return new Domain.Models.CardModel();

            return new Domain.Models.CardModel
            {
                Id = model.Id,
                BoardId = model.BoardId,
                ListId = model.ListId,
                Name = model.Name,
                Position = model.Position
            };
        }
    }
}