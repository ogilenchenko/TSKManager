using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.API.Models;
using TM.API.ViewModels;
using TM.Database;

namespace TM.API.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : BaseController
    {
        public DashboardController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            var boards = new Boards(_unitOfWork, UserProfile);

            var boardsList = boards.GetBoards(UserProfile.UserId);
            var result = new BoardsViewModel
            {
                Boards = boards.GetBoards(UserProfile.UserId),
                UserId = UserProfile.UserId
            };
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public  IHttpActionResult Add(BoardModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var boards = new Boards(_unitOfWork, UserProfile);

            var result = boards.AddBoard(model);
            if (Convert.ToBoolean(result))
            {
                var boardsList = boards.GetBoards(UserProfile.UserId);
                return Ok(boardsList);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("updatestatus")]
        public IHttpActionResult UpdateStatus([FromBody]int id)
        {
            var boards = new Boards(_unitOfWork, UserProfile);

            int isUpdated = boards.UpdateStatus(Convert.ToInt32(id));

            return Ok(isUpdated);
        }
    }
}