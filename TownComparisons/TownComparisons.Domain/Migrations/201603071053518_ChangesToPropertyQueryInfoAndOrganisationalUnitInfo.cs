namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToPropertyQueryInfoAndOrganisationalUnitInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyQueryInfo", "OriginalTitle", c => c.String());
            AddColumn("dbo.PropertyQueryInfo", "Period", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganisationalUnitInfo", "Latitude", c => c.Double());
            AlterColumn("dbo.OrganisationalUnitInfo", "Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrganisationalUnitInfo", "Longitude", c => c.String());
            AlterColumn("dbo.OrganisationalUnitInfo", "Latitude", c => c.String());
            DropColumn("dbo.PropertyQueryInfo", "Period");
            DropColumn("dbo.PropertyQueryInfo", "OriginalTitle");
        }
    }
}
