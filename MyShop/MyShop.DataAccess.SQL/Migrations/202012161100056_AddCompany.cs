namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(maxLength: 450),
                        ContactPersonName = c.String(maxLength: 450),
                        ContactPersonPMobile = c.String(maxLength: 10),
                        ContactPersonEmail = c.String(maxLength: 450),
                        CompanyAddress = c.String(maxLength: 450),
                        CompanyPhone = c.String(maxLength: 10),
                        Password = c.String(maxLength: 450),
                        Role = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        StatesId = c.String(maxLength: 128),
                        CountryId = c.String(maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.States", t => t.StatesId)
                .Index(t => t.ContactPersonEmail, unique: true)
                .Index(t => t.CompanyPhone, unique: true)
                .Index(t => t.StatesId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CountryName = c.String(maxLength: 450),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        State = c.String(maxLength: 450),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Country_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "StatesId", "dbo.States");
            DropForeignKey("dbo.Companies", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_Id", "dbo.Countries");
            DropIndex("dbo.States", new[] { "Country_Id" });
            DropIndex("dbo.Companies", new[] { "CountryId" });
            DropIndex("dbo.Companies", new[] { "StatesId" });
            DropIndex("dbo.Companies", new[] { "CompanyPhone" });
            DropIndex("dbo.Companies", new[] { "ContactPersonEmail" });
            DropTable("dbo.States");
            DropTable("dbo.Countries");
            DropTable("dbo.Companies");
        }
    }
}
