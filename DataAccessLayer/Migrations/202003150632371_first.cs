namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        BID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                        DID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BID)
                .ForeignKey("dbo.Departments", t => t.DID, cascadeDelete: true)
                .Index(t => t.DID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.DID);
            
            CreateTable(
                "dbo.Fees",
                c => new
                    {
                        FID = c.Int(nullable: false, identity: true),
                        SDID = c.Int(nullable: false),
                        FeesPaid = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FID)
                .ForeignKey("dbo.StudentDetails", t => t.SDID, cascadeDelete: true)
                .Index(t => t.SDID);
            
            CreateTable(
                "dbo.StudentDetails",
                c => new
                    {
                        SDID = c.Int(nullable: false, identity: true),
                        BID = c.Int(nullable: false),
                        SID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SDID)
                .ForeignKey("dbo.Batches", t => t.BID, cascadeDelete: true)
                .ForeignKey("dbo.Logins", t => t.SID, cascadeDelete: true)
                .Index(t => t.BID)
                .Index(t => t.SID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        RoleId = c.Int(nullable: false),
                        DID = c.Int(nullable: false),
                        Gender = c.String(maxLength: 30),
                        Age = c.Int(nullable: true),
                        Photo = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.LoginId)
                .ForeignKey("dbo.Departments", t => t.DID, cascadeDelete: false)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.DID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MID = c.Int(nullable: false, identity: true),
                        SDID = c.Int(nullable: false),
                        SUBID = c.Int(nullable: false),
                        Marks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MID)
                .ForeignKey("dbo.StudentDetails", t => t.SDID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SUBID, cascadeDelete: true)
                .Index(t => t.SDID)
                .Index(t => t.SUBID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SUBID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 30),
                        BID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SUBID)
                .ForeignKey("dbo.Batches", t => t.BID, cascadeDelete: false)
                .Index(t => t.BID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationID = c.Int(nullable: false, identity: true),
                        FromUserID = c.Int(nullable: false),
                        Roles = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.NotificationID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TDID = c.Int(nullable: false, identity: true),
                        Experience = c.Int(nullable: false),
                        TID = c.Int(nullable: false),
                        Qualification = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.TDID)
                .ForeignKey("dbo.Logins", t => t.TID, cascadeDelete: true)
                .Index(t => t.TID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "TID", "dbo.Logins");
            DropForeignKey("dbo.Marks", "SUBID", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "BID", "dbo.Batches");
            DropForeignKey("dbo.Marks", "SDID", "dbo.StudentDetails");
            DropForeignKey("dbo.Fees", "SDID", "dbo.StudentDetails");
            DropForeignKey("dbo.StudentDetails", "SID", "dbo.Logins");
            DropForeignKey("dbo.Logins", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Logins", "DID", "dbo.Departments");
            DropForeignKey("dbo.StudentDetails", "BID", "dbo.Batches");
            DropForeignKey("dbo.Batches", "DID", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "TID" });
            DropIndex("dbo.Subjects", new[] { "BID" });
            DropIndex("dbo.Marks", new[] { "SUBID" });
            DropIndex("dbo.Marks", new[] { "SDID" });
            DropIndex("dbo.Logins", new[] { "DID" });
            DropIndex("dbo.Logins", new[] { "RoleId" });
            DropIndex("dbo.StudentDetails", new[] { "SID" });
            DropIndex("dbo.StudentDetails", new[] { "BID" });
            DropIndex("dbo.Fees", new[] { "SDID" });
            DropIndex("dbo.Batches", new[] { "DID" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Notifications");
            DropTable("dbo.Subjects");
            DropTable("dbo.Marks");
            DropTable("dbo.Roles");
            DropTable("dbo.Logins");
            DropTable("dbo.StudentDetails");
            DropTable("dbo.Fees");
            DropTable("dbo.Departments");
            DropTable("dbo.Batches");
        }
    }
}
