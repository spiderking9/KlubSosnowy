namespace KlubSosnowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jeden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zamowienias", "DataRealizacji", c => c.DateTime(nullable: false));
            AddColumn("dbo.Zamowienias", "CzyZrealizowane", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Zamowienias", "IdKlienta", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zamowienias", "IdKlienta", c => c.Int(nullable: false));
            DropColumn("dbo.Zamowienias", "CzyZrealizowane");
            DropColumn("dbo.Zamowienias", "DataRealizacji");
        }
    }
}
