using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TM.API.Models;
using TM.API.ViewModels;
using TM.Database;

namespace TM.API.Controllers
{
    [RoutePrefix("api/boarddetail")]
    public class BoardDetailController : BaseController
    {
        public BoardDetailController(IUnitOfWork uow) : base(uow)
        {
        }

        [Authorize]
        [Route("{boardId}")]
        public IHttpActionResult Get(int boardId)
        {
            var boards = new Boards(_unitOfWork, UserProfile);

            var board = boards.GetBoardById(boardId);
            if (board.Id == 0)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Board {0} not found.", boardId)));
            }

            return Ok(board);
        }

        [Authorize]
        [HttpPost]
        [Route("addlist")]
        public IHttpActionResult AddList(CardModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cards = new Cards(_unitOfWork, UserProfile);

            var result = cards.AddCard(model);
            if (Convert.ToBoolean(result))
            {
                var cardList = cards.GetCards(model.ListId);
                return Ok(cardList);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("updatecardsposition")]
        public IHttpActionResult UpdateCardsPosition(CardsPositionViewModel model)
        {
            return Ok(model);
        }

        [Authorize]
        [HttpPost]
        [Route("updatelistsposition")]
        public IHttpActionResult UpdateListsPosition(ListsPositionViewModel model)
        {
            return Ok(model);
        }
    }
}