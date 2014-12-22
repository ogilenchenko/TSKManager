using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.Database.Models;

namespace TM.Database
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repository Interfaces (add one per entity)

        //IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Client> ClientRepository { get; }
        IGenericRepository<RefreshToken> RefreshTokenRepository { get; }
        UserManager<IdentityUser> UserManager { get; }
        IGenericRepository<Board> BoardRepository { get; }
        IGenericRepository<Card> CardRepository { get; }
        IGenericRepository<CardLabel> CardLabelRepository { get; }
        IGenericRepository<Label> LabelRepository { get; }
        IGenericRepository<List> ListRepository { get; }

        #endregion

        int Commit();
        Task<int> CommitAsync();
    }
}
