namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        GroupCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupCategory", t => t.GroupCategory_Id)
                .Index(t => t.GroupCategory_Id);
            
            CreateTable(
                "dbo.GroupCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TempJustToChangeDatabase = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrganisationalUnitInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganisationalUnitId = c.String(),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        ImagePath = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        Contact = c.String(),
                        Email = c.String(),
                        OrganisationalForm = c.String(),
                        Website = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        Other = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.CategoryPropertyQuery",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebServiceName = c.String(),
                        QueryId = c.String(),
                        Title = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryPropertyQuery", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Category", "GroupCategory_Id", "dbo.GroupCategory");
            DropIndex("dbo.CategoryPropertyQuery", new[] { "Category_Id" });
            DropIndex("dbo.OrganisationalUnitInfo", new[] { "Category_Id" });
            DropIndex("dbo.Category", new[] { "GroupCategory_Id" });
            DropTable("dbo.CategoryPropertyQuery");
            DropTable("dbo.OrganisationalUnitInfo");
            DropTable("dbo.GroupCategory");
            DropTable("dbo.Category");
        }
    }
}
