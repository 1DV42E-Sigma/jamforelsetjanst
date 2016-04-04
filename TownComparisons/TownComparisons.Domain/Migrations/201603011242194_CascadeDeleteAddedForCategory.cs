namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteAddedForCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "GroupCategory_Id", "dbo.GroupCategory");
            DropForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.CategoryPropertyQuery", "Category_Id", "dbo.Category");
            DropIndex("dbo.Category", new[] { "GroupCategory_Id" });
            DropIndex("dbo.OrganisationalUnitInfo", new[] { "Category_Id" });
            DropIndex("dbo.CategoryPropertyQuery", new[] { "Category_Id" });
            AlterColumn("dbo.Category", "GroupCategory_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganisationalUnitInfo", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CategoryPropertyQuery", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Category", "GroupCategory_Id");
            CreateIndex("dbo.OrganisationalUnitInfo", "Category_Id");
            CreateIndex("dbo.CategoryPropertyQuery", "Category_Id");
            AddForeignKey("dbo.Category", "GroupCategory_Id", "dbo.GroupCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryPropertyQuery", "Category_Id", "dbo.Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryPropertyQuery", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Category", "GroupCategory_Id", "dbo.GroupCategory");
            DropIndex("dbo.CategoryPropertyQuery", new[] { "Category_Id" });
            DropIndex("dbo.OrganisationalUnitInfo", new[] { "Category_Id" });
            DropIndex("dbo.Category", new[] { "GroupCategory_Id" });
            AlterColumn("dbo.CategoryPropertyQuery", "Category_Id", c => c.Int());
            AlterColumn("dbo.OrganisationalUnitInfo", "Category_Id", c => c.Int());
            AlterColumn("dbo.Category", "GroupCategory_Id", c => c.Int());
            CreateIndex("dbo.CategoryPropertyQuery", "Category_Id");
            CreateIndex("dbo.OrganisationalUnitInfo", "Category_Id");
            CreateIndex("dbo.Category", "GroupCategory_Id");
            AddForeignKey("dbo.CategoryPropertyQuery", "Category_Id", "dbo.Category", "Id");
            AddForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category", "Id");
            AddForeignKey("dbo.Category", "GroupCategory_Id", "dbo.GroupCategory", "Id");
        }
    }
}
