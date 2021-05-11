namespace Proyecto_Ordinario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coordinator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordinators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        CellPhoneNumber = c.String(),
                        Photo = c.String(maxLength: 250),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Coordinators");
        }
    }
}
