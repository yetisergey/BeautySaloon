namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appeals");
        }
    }
}
