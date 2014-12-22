using System.Threading.Tasks;
using System.Web.Http;
using TM.Database;

namespace TM.API.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : BaseController
    {
        public RefreshTokensController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

       // [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            var account = new Account(_unitOfWork);
            return Ok(account.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var account = new Account(_unitOfWork);
            var result = await account.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
