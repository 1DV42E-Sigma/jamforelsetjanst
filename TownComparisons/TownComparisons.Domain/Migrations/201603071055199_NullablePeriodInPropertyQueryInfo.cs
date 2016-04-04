namespace TownComparisons.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullablePeriodInPropertyQueryInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropertyQueryInfo", "Period", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropertyQueryInfo", "Period", c => c.Int(nullable: false));
        }
    }
}
