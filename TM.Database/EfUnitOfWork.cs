using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TM.Database.Models;

namespace TM.Database
{
    public class EfUnitOfWork : IdentityDbContext<IdentityUser>, IUnitOfWork
    {
        #region Private Repos (add one per entity)

        //private EfGenericRepository<User> _userRepo;
        private EfGenericRepository<Client> _clientRepo;
        private EfGenericRepository<RefreshToken> _refreshTokenRepo;
        private UserManager<IdentityUser> _userManager;
        private EfGenericRepository<Board> _boardRepo;
        private EfGenericRepository<Card> _cardRepo;
        private EfGenericRepository<CardLabel> _cardLabelRepo;
        private EfGenericRepository<Label> _labelRepo;
        private EfGenericRepository<List> _listRepo;
        
        #endregion

        #region Public DbSets (add one per entity)

        //public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardLabel> CardLabels { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<List> Lists { get; set; }

        #endregion

        #region Constructor

        public EfUnitOfWork()
            : base("TMDatabase")
        {
        }

        #endregion

        #region IUnitOfWork Implementation (add one per entity)

        public UserManager<IdentityUser> UserManager
        {
            get { return _userManager ?? (_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this))); }
        }

        public IGenericRepository<Board> BoardRepository
        {
            get { return _boardRepo ?? (_boardRepo = new EfGenericRepository<Board>(Boards)); }
        }

        public IGenericRepository<Client> ClientRepository
        {
            get { return _clientRepo ?? (_clientRepo = new EfGenericRepository<Client>(Clients)); }
        }

        public IGenericRepository<RefreshToken> RefreshTokenRepository
        {
            get { return _refreshTokenRepo ?? (_refreshTokenRepo = new EfGenericRepository<RefreshToken>(RefreshTokens)); }
        }

        public IGenericRepository<Card> CardRepository
        {
            get { return _cardRepo ?? (_cardRepo = new EfGenericRepository<Card>(Cards)); }
        }

        public IGenericRepository<CardLabel> CardLabelRepository
        {
            get { return _cardLabelRepo ?? (_cardLabelRepo = new EfGenericRepository<CardLabel>(CardLabels)); }
        }

        public IGenericRepository<Label> LabelRepository
        {
            get { return _labelRepo ?? (_labelRepo = new EfGenericRepository<Label>(Labels)); }
        }

        public IGenericRepository<List> ListRepository
        {
            get { return _listRepo ?? (_listRepo = new EfGenericRepository<List>(Lists)); }
        }

        #endregion

        public int Commit()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        #region Code First Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<EfUnitOfWork>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();           
        }

        #endregion

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
