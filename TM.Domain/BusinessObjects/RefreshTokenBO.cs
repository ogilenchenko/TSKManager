using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Database;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class RefreshTokenBO : BaseBO
    {
        public RefreshTokenBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {
        }

        public async Task<bool> AddRefreshToken(RefreshTokenModel token)
        {
            var existingToken = UnitOfWork.RefreshTokenRepository.Find(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken.Id);
            }

            UnitOfWork.RefreshTokenRepository.Add(RefreshTokenModel.Map(token));
            
            return await UnitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await UnitOfWork.RefreshTokenRepository.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                UnitOfWork.RefreshTokenRepository.Delete(refreshToken);
                return await UnitOfWork.CommitAsync() > 0;
            }

            return false;
        }

        //public async Task<bool> RemoveRefreshToken(RefreshTokenModel refreshToken)
        //{
        //   // UnitOfWork.RefreshTokenRepository.Delete(refreshToken);
        //    return await UnitOfWork.CommitAsync() > 0;
        //}

        public async Task<RefreshTokenModel> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await UnitOfWork.RefreshTokenRepository.FindAsync(refreshTokenId);

            return RefreshTokenModel.Map(refreshToken);
        }

        public List<RefreshTokenModel> GetAllRefreshTokens()
        {
            return UnitOfWork.RefreshTokenRepository.GetAll().ToList().Select(RefreshTokenModel.Map).ToList();
        }

    }
}