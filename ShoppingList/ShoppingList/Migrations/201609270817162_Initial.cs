namespace ShoppingList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingListProducts",
                c => new
                    {
                        ShoppingList_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_Id, t.Product_Id })
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ShoppingList_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingListProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ShoppingListProducts", "ShoppingList_Id", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingListProducts", new[] { "Product_Id" });
            DropIndex("dbo.ShoppingListProducts", new[] { "ShoppingList_Id" });
            DropTable("dbo.ShoppingListProducts");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.Products");
        }
    }
}
