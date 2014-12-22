using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.Database;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class UserBO : BaseBO
    {
        public UserBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {
        }

        public async Task<IdentityResult> CreateUser(UserModel model)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = model.UserName
            };
            var result = await UnitOfWork.UserManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<IdentityUser> GetByNameAndPassword(string userName, string password)
        {
            return await UnitOfWork.UserManager.FindAsync(userName, password);
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await UnitOfWork.UserManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityUser> FindByNameAsync(string name)
        {
            IdentityUser user = await UnitOfWork.UserManager.FindByNameAsync(name);

            return user;
        }

        public IdentityUser FindByName(string name)
        {
            IdentityUser user = UnitOfWork.UserManager.FindByName(name);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await UnitOfWork.UserManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await UnitOfWork.UserManager.AddLoginAsync(userId, login);

            return result;
        }

        //#region Map

        //public UserModel Map(User user)
        //{
        //    if (user == null)
        //        return null;

        //    return new UserModel
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        IsDeleted = user.IsDeleted,
        //        CreatedBy = user.CreatedBy,
        //        CreatedDate = user.CreatedDate,
        //        LastModifiedBy = user.LastModifiedBy,
        //        LastModifiedDate = user.LastModifiedDate
        //    };
        //}

        //public User Map(UserModel model)
        //{
        //    if (model == null)
        //        return null;

        //    User user;
        //    if (model.Id == 0)
        //    {
        //        user = new User
        //        {
        //            //CreatedBy = UserProfile.UserId,
        //            CreatedBy = 0,
        //            CreatedDate = DateTime.UtcNow
        //        };

        //        UnitOfWork.UserRepository.Add(user);
        //    }
        //    else
        //    {
        //        user = UnitOfWork.UserRepository.GetById(model.Id);
        //        if (user == null)
        //            throw new NoNullAllowedException("user");

        //        //user.LastModifiedBy = UserProfile.UserId;
        //        user.LastModifiedBy = 0;
        //        user.LastModifiedDate = DateTime.UtcNow;
        //    }

        //    user.Id = model.Id;
        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.Email = model.Email;

        //    return user;
        //}

        //#endregion
    }
}