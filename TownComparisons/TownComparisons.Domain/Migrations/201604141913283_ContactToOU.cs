namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactToOU : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        OrganisationalUnitInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganisationalUnitInfo", t => t.OrganisationalUnitInfo_Id)
                .Index(t => t.OrganisationalUnitInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "OrganisationalUnitInfo_Id", "dbo.OrganisationalUnitInfo");
            DropIndex("dbo.Contact", new[] { "OrganisationalUnitInfo_Id" });
            DropTable("dbo.Contact");
        }
    }
}
