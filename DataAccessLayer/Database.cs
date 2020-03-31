namespace DataAccessLayer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    
        public class Database : DbContext
        {

            public Database()
                : base("name=Database")
            {
            }

            public virtual DbSet<Notification> Notifications { get; set; }
            public virtual DbSet<Subject> Subjects { get; set; }
            public virtual DbSet<Mark> Marks { get; set; }
            public virtual DbSet<Login> Logins { get; set; }
            public virtual DbSet<Role> Roles { get; set; }
            public virtual DbSet<Department> Departments { get; set; }
            public virtual DbSet<Fees> Feess { get; set; }
            public virtual DbSet<Batch> Batches { get; set; }
            public virtual DbSet<Teacher> Teachers { get; set; }
            public virtual DbSet<StudentDetail> StudentDetails { get; set; }

        }

        public class Login
        {
            [Key]
            public int LoginId { get; set; }
            [Required]
            [StringLength(30)]
            public string UserName { get; set; }
            [Required]
            [StringLength(30)]
            public string Password { get; set; }
            
            public Role roleids { get; set; }
            [Required]
            [ForeignKey("roleids")]
            public int RoleId { get; set; }
          
            public Department depid { get; set; }
            [Required]
            [ForeignKey("depid")]
            public int DID { get; set; }
            [StringLength(30)]
            public string Gender { get; set; }
            public int Age { get; set; }
            [StringLength(100)]
            public string Photo { get; set; }
        }
        public class Role
        {
            [Key]
            public int RoleId { get; set; }
            [Required]
            [StringLength(30)]
            public string RoleName { get; set; }
        }
        public class Department
        {
            [Key]
            public int DID { get; set; }
            [Required]
            [StringLength(30)]
            public string DepartmentName { get; set; }
        }
        public class Batch
        {
            [Key]
            public int BID { get; set; }
            [Required]
            public int Year { get; set; }
            [Required]
            public int Semester { get; set; }
            public Department depidb { get; set; }
            [ForeignKey("depidb")]
                public int DID { get; set; }

    }
    public class Teacher
        {
            [Key]
            public int TDID { get; set; }
            [Required]
            public int Experience { get; set; }
           
            public Login logid { get; set; }
            [Required]
            [ForeignKey("logid")]
            public int TID { get; set; }
            [Required]
            [StringLength(30)]
            public string Qualification { get; set; }

        }
        public class StudentDetail
        {
            [Key]
            public int SDID { get; set; }
            
            public Batch batchid { get; set; }
            [Required]
            [ForeignKey("batchid")]
            public int BID { get; set; }
           
            public Login logids { get; set; }
            [Required]
            [ForeignKey("logids")]
            public int SID { get; set; }
        }
        public class Subject
        {
            [Key]
            public int SUBID { get; set; }
            [Required]
            [StringLength(30)]
            public string SubjectName { get; set; }
            
            public Batch batchids { get; set; }
            [Required]
            [ForeignKey("batchids")]
            public int BID { get; set; }
        }
        public class Mark
        {
            [Key]
            public int MID { get; set; }
            
            public StudentDetail sdidm { get; set; }
            [Required]
            [ForeignKey("sdidm")]
            public int SDID { get; set; }
            public Subject subjectid { get; set; }
            [Required]
            [ForeignKey("subjectid")]
            public int SUBID { get; set; }
            [Required]
            public int Marks { get; set; }
        }
        public class Notification
        {
            [Key]
            public int NotificationID { get; set; }
            [Required]
            public int FromUserID { get; set; }
         
           
            [Required]
            
            public string Roles { get; set; }
            [Required]
            public DateTime Date { get; set; }
            [Required]
            [StringLength(150)]
            public string Content { get; set; }
        }

        public class Fees
        {
            [Key]
            public int FID { get; set; }

            public StudentDetail sdidf { get; set; }
            [Required]
            [ForeignKey("sdidf")]
            public int SDID { get; set; }
            [Required]
            public double FeesPaid { get; set; }
        }
 }


