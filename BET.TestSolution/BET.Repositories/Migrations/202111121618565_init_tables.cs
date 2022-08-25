namespace BET.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Users",
               c => new
               {
                 UserId = c.Int(nullable: false, identity: true),
                 Login = c.String(),
                 FullName = c.String(),
                 IsAdmin = c.Boolean(nullable: false),
                 IsActive = c.Boolean(nullable: false),
                 IsDeleted = c.Boolean(nullable: false),
                 Password = c.String(),
               })
               .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Image = c.String(),
                        LastModifiedUserId = c.Int(nullable: false),
                        LastModifiedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.LastModifiedUserId, cascadeDelete: false)
                .Index(t => t.LastModifiedUserId);

            CreateTable(
                "dbo.Carts",
                c => new
                {
                  CartId = c.Int(nullable: false, identity: true),
                  Totals = c.Decimal(nullable: false, precision: 18, scale: 2),
                  LastModifiedUserId = c.Int(nullable: false),
                  LastModifiedDateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Users", t => t.LastModifiedUserId, cascadeDelete: false)
                .Index(t => t.LastModifiedUserId);

            CreateTable(
               "dbo.CartItems",
               c => new
               {
                 CartItemId = c.Int(nullable: false, identity: true),
                 CartId = c.Int(nullable: false),
                 ProductId = c.Int(nullable: false),
                 Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                 Quantity = c.Int(nullable: false),
                 Totals = c.Decimal(nullable: false, precision: 18, scale: 2),
               })
               .PrimaryKey(t => t.CartItemId)
               .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
               .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
               .Index(t => t.CartId)
               .Index(t => t.ProductId);     

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "LastModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropIndex("dbo.Carts", new[] { "LastModifiedUserId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropTable("dbo.Carts");
            DropTable("dbo.CartItems");
            DropForeignKey("dbo.Products", "LastModifiedUserId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "LastModifiedUserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
        }
    }
}
