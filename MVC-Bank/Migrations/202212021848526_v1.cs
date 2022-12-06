namespace MVC_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Balance1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        ActionType = c.String(),
                        Amount = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Details");
            DropTable("dbo.Balances");
        }
    }
}
