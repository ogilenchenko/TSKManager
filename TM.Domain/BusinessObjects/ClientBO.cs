using TM.Database;
using TM.Database.Models;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class ClientBO : BaseBO
    {
        public ClientBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {
        }

        public ClientModel GetClient(string clientId)
        {
            var client = UnitOfWork.ClientRepository.GetById(clientId);
            return Map(client);
        }

        #region Map

        public ClientModel Map(Client client)
        {
            if (client == null)
                return null;

            return new ClientModel
            {
                Id = client.Id,
                Name = client.Name,
                Secret = client.Secret,
                ApplicationType = client.ApplicationType,
                Active = client.Active,
                RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                AllowedOrigin = client.AllowedOrigin,
            };
        }

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

        #endregion
    }
}
