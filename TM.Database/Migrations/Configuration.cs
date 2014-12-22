using System;
using TM.Database.Enums;
using TM.Database.Helpers;
using TM.Database.Models;

namespace TM.Database.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EfUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EfUnitOfWork context)
        {
            context.Clients.AddOrUpdate(x => x.Id,
                new Client
                {
                    Id = "ngTmApp",
                    Secret = EncryptionHelper.GetHash("abc@123"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://tm-web.loc"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret = EncryptionHelper.GetHash("123@abc"),
                    Name = "Console Application",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                });
            
            context.Labels.AddOrUpdate(x => x.Name,
                new Label
                {
                    Name = "Error",
                    Color = "#F00",
                    Position = 1,
                    CreatedBy = "0",
                    CreatedDate = DateTime.UtcNow
                },
                new Label
                {
                    Name = "Warning",
                    Color = "#dbdb57",
                    Position = 2,
                    CreatedBy = "0",
                    CreatedDate = DateTime.UtcNow
                });

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
