namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallFixesToCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category");
            DropIndex("dbo.OrganisationalUnitInfo", new[] { "Category_Id" });
            AlterColumn("dbo.OrganisationalUnitInfo", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.OrganisationalUnitInfo", "Category_Id");
            AddForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category");
            DropIndex("dbo.OrganisationalUnitInfo", new[] { "Category_Id" });
            AlterColumn("dbo.OrganisationalUnitInfo", "Category_Id", c => c.Int());
            CreateIndex("dbo.OrganisationalUnitInfo", "Category_Id");
            AddForeignKey("dbo.OrganisationalUnitInfo", "Category_Id", "dbo.Category", "Id");
        }
    }
}
