namespace Wishlist.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class itemispurchased : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "IsPurchased", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Items", "IsPurchased");
        }
    }
}
