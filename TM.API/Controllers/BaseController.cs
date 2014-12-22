using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.Database;
using TM.Domain.Models;

namespace TM.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        internal readonly IUnitOfWork _unitOfWork;

        internal UserProfile UserProfile
        {
            get { return GetUserProfile(); }
        }

        protected BaseController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        private UserProfile GetUserProfile()
        {
            var userProfile = new UserProfile();
            var account = new Account(_unitOfWork);
            if (User.Identity.IsAuthenticated)
            {
                IdentityUser user = account.FindByName(User.Identity.Name);
                userProfile.UserId = user.Id;
                userProfile.Email = user.Email;
                userProfile.UserName = user.UserName;
                return userProfile;
            }
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ""));
        }
    }
}