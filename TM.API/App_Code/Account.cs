using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.Database;
using TM.Domain.BusinessObjects;
using TM.Domain.Models;
using UserModel = TM.API.Models.UserModel;

namespace TM.API
{
    public class Account
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserProfile _userProfile;

        public Account(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterUser(UserModel model)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            var result = await userBo.CreateUser(UserModel.Map(model));

            return result;
        }

        public async Task<IdentityUser> GetUser(string userName, string password)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            var result = await userBo.GetByNameAndPassword(userName, password);

            return result;
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            IdentityUser user = await userBo.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityUser> FindByNameAsync(string name)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            IdentityUser user = await userBo.FindByNameAsync(name);

            return user;
        }

        public IdentityUser FindByName(string name)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            IdentityUser user = userBo.FindByName(name);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            var result = await userBo.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var userBo = new UserBO(_unitOfWork, _userProfile);
            var result = await userBo.AddLoginAsync(userId, login);

            return result;
        }

        public ClientModel GetClient(string id)
        {
            var clientBo = new ClientBO(_unitOfWork, _userProfile);
            var result = clientBo.GetClient(id);

            return result;
        }

        public async Task<bool> AddRefreshToken(RefreshTokenModel model)
        {
            var refreshTokenBo = new RefreshTokenBO(_unitOfWork, _userProfile);
            var result = await refreshTokenBo.AddRefreshToken(model);

            return result;
        }

        public async Task<RefreshTokenModel> GetRefreshToken(string id)
        {
            var refreshTokenBo = new RefreshTokenBO(_unitOfWork, _userProfile);
            var result = await refreshTokenBo.FindRefreshToken(id);

            return result;
        }

        public async Task<bool> RemoveRefreshToken(string id)
        {
            var refreshTokenBo = new RefreshTokenBO(_unitOfWork, _userProfile);
            var result = await refreshTokenBo.RemoveRefreshToken(id);

            return result;
        }

        public List<RefreshTokenModel> GetAllRefreshTokens()
        {
            var refreshTokenBo = new RefreshTokenBO(_unitOfWork, _userProfile);
            var result = refreshTokenBo.GetAllRefreshTokens();

            return result;
        }
    }
}