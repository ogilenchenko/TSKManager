using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TM.Database;
using TM.Database.Models;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class CardBO : BaseBO
    {
        public CardBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {

        }

        public CardModel GetCard(int id)
        {
            var card = UnitOfWork.CardRepository.GetById(id);
            return Map(card);
        }

        public IEnumerable<CardModel> GetCards(int listId)
        {
            var cards = UnitOfWork.CardRepository.Find(x => x.ListId == listId).OrderBy(x => x.Position);
            return cards.Select(Map);
        }

        public int Save(CardModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var board = Map(model);
            var result = UnitOfWork.Commit();

            return result;
        }

        #region Map

        public CardModel Map(Card card)
        {
            if (card == null)
                return null;

            return new CardModel
            {
                Id = card.Id,
                ListId = card.ListId,
                BoardId = card.BoardId,
                Name = card.Name,
                Position = card.Position,
                IsDeleted = card.IsDeleted,
                LastModifiedDate = card.LastModifiedDate,
                LastModifiedBy = card.LastModifiedBy,
                CreatedBy = card.CreatedBy,
                CreatedDate = card.CreatedDate
            };
        }

        public Card Map(CardModel model)
        {
            if (model == null)
                return null;

            Card card;
            if (model.Id == 0)
            {
                card = new Card
                {
                    CreatedBy = UserProfile.UserId,
                    CreatedDate = DateTime.UtcNow
                };

                UnitOfWork.CardRepository.Add(card);
            }
            else
            {
                card = UnitOfWork.CardRepository.GetById(model.Id);
                if (card == null)
                    throw new NoNullAllowedException("card");

                card.LastModifiedBy = UserProfile.UserId;
                card.LastModifiedDate = DateTime.UtcNow;
            }

            card.BoardId = model.BoardId;
            card.ListId = model.ListId;
            card.Name = model.Name;
            card.Position = model.Position;
            card.IsDeleted = false;

            return card;
        }

        #endregion
    }
}
