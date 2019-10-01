namespace MyDGV_MVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblPeople : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblPeople",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 100),
                        Birthdate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 150),
                        Photo = c.String(nullable: false, maxLength: 250),
                        Gender = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblPeople");
        }
    }
}
