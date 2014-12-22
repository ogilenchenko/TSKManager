namespace TM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUsersFieled : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Boards", "LastModifiedBy", c => c.String());
            AlterColumn("dbo.Boards", "CreatedBy", c => c.String());
            AlterColumn("dbo.Cards", "LastModifiedBy", c => c.String());
            AlterColumn("dbo.Cards", "CreatedBy", c => c.String());
            AlterColumn("dbo.Lists", "LastModifiedBy", c => c.String());
            AlterColumn("dbo.Lists", "CreatedBy", c => c.String());
            AlterColumn("dbo.Labels", "LastModifiedBy", c => c.String());
            AlterColumn("dbo.Labels", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Labels", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Labels", "LastModifiedBy", c => c.Int());
            AlterColumn("dbo.Lists", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Lists", "LastModifiedBy", c => c.Int());
            AlterColumn("dbo.Cards", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Cards", "LastModifiedBy", c => c.Int());
            AlterColumn("dbo.Boards", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Boards", "LastModifiedBy", c => c.Int());
        }
    }
}
