namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyQueryInfoRenamed : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoryPropertyQuery", newName: "PropertyQueryInfo");
            AddColumn("dbo.PropertyQueryInfo", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyQueryInfo", "Type");
            RenameTable(name: "dbo.PropertyQueryInfo", newName: "CategoryPropertyQuery");
        }
    }
}
