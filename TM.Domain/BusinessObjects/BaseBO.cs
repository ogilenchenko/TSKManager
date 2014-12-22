using TM.Database;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public abstract class BaseBO
    {
        internal IUnitOfWork UnitOfWork;
        internal UserProfile UserProfile;

        protected BaseBO(IUnitOfWork uow, UserProfile userProfile)
        {
            UnitOfWork = uow;
            UserProfile = userProfile;
        }
    }
}