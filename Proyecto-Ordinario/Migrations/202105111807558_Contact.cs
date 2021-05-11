namespace Proyecto_Ordinario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(),
                        CellPhoneNumber = c.String(),
                        Photo = c.String(maxLength: 250),
                        Email = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
