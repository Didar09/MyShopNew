namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Roles", c => c.Int(nullable: false));
            DropColumn("dbo.Companies", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Role", c => c.String());
            DropColumn("dbo.Companies", "Roles");
        }
    }
}
