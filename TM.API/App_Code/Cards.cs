using System.Collections.Generic;
using System.Linq;
using TM.Database;
using TM.Domain.BusinessObjects;
using TM.Domain.Models;

namespace TM.API
{
    public class Cards
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserProfile _userProfile;

        public Cards(IUnitOfWork unitOfWork, UserProfile userProfile)
        {
            _unitOfWork = unitOfWork;
            _userProfile = userProfile;
        }

        public IEnumerable<Models.CardModel> GetCards(int listId)
        {
            var cardBo = new CardBO(_unitOfWork, _userProfile);
            var result = cardBo.GetCards(listId);

            return result.Select(Models.CardModel.Map);
        }

        public int AddCard(Models.CardModel model)
        {
            var cardBo = new CardBO(_unitOfWork, _userProfile);
            var cards = cardBo.GetCards(model.ListId).ToList();
            var maxPosition = cards.Count != 0 ? cards.Max(x => x.Position) : 0;
            model.Position = maxPosition + 1;
            var result = cardBo.Save(Models.CardModel.Map(model));

            return result;
        }
    }
}