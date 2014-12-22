namespace TM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoardListCardLabel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        IsStarred = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CardLabel",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        LabelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.LabelId })
                .ForeignKey("dbo.Cards", t => t.CardId)
                .ForeignKey("dbo.Labels", t => t.LabelId)
                .Index(t => t.CardId)
                .Index(t => t.LabelId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        ListId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId)
                .ForeignKey("dbo.Lists", t => t.ListId)
                .Index(t => t.BoardId)
                .Index(t => t.ListId);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Color = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardLabel", "LabelId", "dbo.Labels");
            DropForeignKey("dbo.CardLabel", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Cards", "ListId", "dbo.Lists");
            DropForeignKey("dbo.Lists", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Cards", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Boards", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Lists", new[] { "BoardId" });
            DropIndex("dbo.Cards", new[] { "ListId" });
            DropIndex("dbo.Cards", new[] { "BoardId" });
            DropIndex("dbo.CardLabel", new[] { "LabelId" });
            DropIndex("dbo.CardLabel", new[] { "CardId" });
            DropIndex("dbo.Boards", new[] { "UserId" });
            DropTable("dbo.Labels");
            DropTable("dbo.Lists");
            DropTable("dbo.Cards");
            DropTable("dbo.CardLabel");
            DropTable("dbo.Boards");
        }
    }
}
