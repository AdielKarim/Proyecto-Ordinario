namespace Proyecto_Ordinario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Major : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Majors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(),
                        Photo = c.String(maxLength: 250),
                        Email = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Majors");
        }
    }
}
