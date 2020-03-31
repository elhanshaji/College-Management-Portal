using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Model;


namespace DataAccessLayer
{
    public class Data
    {
        public static string roles(int id)
        {
            Database DB = new Database();
            int role = DB.Logins.Where(i => i.LoginId == id).Select(i => i.RoleId).FirstOrDefault();
            return DB.Roles.Where(i => i.RoleId == role).Select(i => i.RoleName).FirstOrDefault();
        }
        public static List<Student> students(int id)
        {
            
            Database DB = new Database();
            List<Student> StdList = new List<Student>();
            int Deptid = DB.Logins.Where(i => i.LoginId == id).Select(i => i.DID).FirstOrDefault();
            int roleid = DB.Roles.Where(i => i.RoleName == "Student").Select(i => i.RoleId).FirstOrDefault();
             var std=DB.Logins.Where(i => i.DID == Deptid && i.RoleId == roleid);
            foreach(var n in std)
            {
                Student stclass = new Student();
                stclass.stName = n.UserName;
                stclass.stid = n.LoginId;
                StdList.Add(stclass);
            }
            return StdList;
        }
        public static int Batch(int id, int year, int sem)
        {
            Database DB = new Database();
            int depid = DB.Logins.Where(i => i.LoginId == id).Select(i => i.DID).FirstOrDefault();
            return  DB.Batches.Where(i => i.DID == depid && i.Year==year && i.Semester==sem).Select(i => i.BID).FirstOrDefault();
            
        }
        public static int stddetails(int batchid,int id)
        {
            Database DB = new Database();
            return DB.StudentDetails.Where(i => i.BID == batchid && i.SID == id).Select(i => i.SDID).First();
        }
        public static List<Markdetail> marks(int stddetail, int batchid)
        {
            Database DB = new Database();
            List<Markdetail> MarkList = new List<Markdetail>();
            var subjects = DB.Subjects.Where(i => i.BID == batchid);

            foreach(var n in subjects)
            {
                int Mark = DB.Marks.Where(i => i.SDID == stddetail && n.SUBID == i.SUBID).Select(i => i.Marks).First();
                Markdetail subMark = new Markdetail();
                subMark.SubName = n.SubjectName;
                subMark.Mark = Mark;
                MarkList.Add(subMark);
            }
            return MarkList;
        }
        public static List<Batches> Batchs(int sid)
        {
            Database DB = new Database();
            List<Batches> BatchList = new List<Batches>();
            var sdid= DB.StudentDetails.Where(i => i.SID == sid).Select(i => i.SDID);
            foreach(int n in sdid)
            {
                int sdidnotentered = DB.Marks.Where(i => i.SDID == n).Select(i => i.SDID).FirstOrDefault();
                if(sdidnotentered==0)
                {
                    int Batch = DB.StudentDetails.Where(i => i.SDID ==n ).Select(i => i.BID).FirstOrDefault();
                    Batches batchclass = new Batches();
                    batchclass.batchid = Batch;
                    batchclass.year= DB.Batches.Where(i => i.BID == Batch).Select(i => i.Year).FirstOrDefault();
                    batchclass.sem = DB.Batches.Where(i => i.BID == Batch).Select(i => i.Semester).FirstOrDefault();
                    BatchList.Add(batchclass);
                }
            }
            return BatchList;
        }
        public static List<Markdetail> subjects(int bid , int sid )
        {
            Database DB = new Database();
            List<Markdetail> MarkList = new List<Markdetail>();
            var subject= DB.Subjects.Where(i => i.BID == bid).Select(i => i.SUBID);
            int SDID = DB.StudentDetails.Where(i => i.BID == bid && i.SID == sid).Select(i => i.SDID).FirstOrDefault(); 
            foreach (var n in subject)
            {
                
                Markdetail subMark = new Markdetail();
                subMark.Subid = n;
                subMark.SDID = SDID;
                subMark.SubName = DB.Subjects.Where(i=> i.SUBID==n).Select(i=> i.SubjectName).FirstOrDefault();
                subMark.Mark =0 ;
                MarkList.Add(subMark);
            }
            return MarkList;
        }
        public static void Addition(List<Markdetail> markdetail)
        {
            Database DB = new Database();
            foreach(var n in markdetail )
            {
                Mark NewEntry = new Mark();
                NewEntry.SDID = n.SDID;
                NewEntry.SUBID = n.Subid;
                NewEntry.Marks = n.Mark;
                DB.Marks.Add(NewEntry);                
            }
            DB.SaveChanges();
        }
        public static int loginfo(string user,string password)
        {
            Database DB = new Database();
            return DB.Logins.Where(i => i.UserName == user && i.Password == password).Select(i => i.LoginId).FirstOrDefault(); 
            
        }
        public static LogerinfoModel info(int id,string role)
        {
            Database DB = new Database();
            var user= DB.Logins.Where(i => i.LoginId==id).FirstOrDefault();
            LogerinfoModel userinfo = new LogerinfoModel();
            userinfo.age = user.Age;
            userinfo.gender = user.Gender;
            userinfo.image = user.Photo;
            userinfo.Name = user.UserName;
            userinfo.Role = role;
            return userinfo;
        }
        /*_______________________________________________________________________________________________________________________________________________________
            JISHNU*/
        public static List<checkfeesmodel> checkfees(int year, int sem)
        {
            Database DB = new Database();
            List<checkfeesmodel> FeeDefault = new List<checkfeesmodel>();
            var BatchList = DB.Batches.Where(i => i.Year == year && i.Semester == sem);
            foreach (var n in BatchList)
            {
                var SDIDs = DB.StudentDetails.Where(i => i.BID == n.BID).Select(i => i.SDID);
                foreach (int k in SDIDs)
                {
                    var feesid = DB.Feess.Where(i => i.SDID == k).FirstOrDefault();
                    if (feesid == null)
                    {
                        checkfeesmodel fees = new checkfeesmodel();
                        fees.SID = DB.StudentDetails.Where(i => i.SDID == k).Select(i => i.SID).First();
                        fees.name = DB.Logins.Where(i => i.LoginId == fees.SID).Select(i => i.UserName).First();
                        fees.branch = DB.Departments.Where(i => i.DID == n.DID).Select(i => i.DepartmentName).First();
                        fees.year = n.Year;
                        fees.sem = n.Semester;
                        FeeDefault.Add(fees);
                    }
                }
            }
            return FeeDefault;
        }
        public static List<Feesmodel> amount(int id)
        {
            Database DB = new Database();
            List<Feesmodel> FM = new List<Feesmodel>();
            var sdids = DB.StudentDetails.Where(i => i.SID == id);
            foreach (var n in sdids)
            {
                var a = DB.Feess.Where(i => i.SDID == n.SDID).FirstOrDefault();
                if (a == null)
                {
                    Feesmodel fm = new Feesmodel();
                    fm.SDID = n.SDID;
                    var batch = DB.Batches.Where(i => i.BID == n.BID).FirstOrDefault();
                    fm.year = batch.Year;
                    fm.sem = batch.Semester;
                    FM.Add(fm);
                }
            }

            return FM;
        }
        public static void pay(int sdid)
        {
            Database DB = new Database();
            Fees fee = new Fees();
            fee.FeesPaid = 50000;
            fee.SDID = sdid;
            DB.Feess.Add(fee);
            DB.SaveChanges();
        }
        /*_________________________________________________________________________________________________________________________________
            Kalyan*/
        public static int AddStudent(string studentname, string confirmpassword, int age, string depname, string gender)
        {


            Database data = new Database();
            Login log = new Login();
            Role rol = new Role();
            Department dep = new Department();
            StudentDetail sd = new StudentDetail();
            Batch batch = new Batch();
            log.UserName = studentname;
            log.Password = confirmpassword;
            log.RoleId = 1;
            log.Gender = gender;
            log.Age = age;
            log.DID = data.Departments.Where(i => i.DepartmentName == depname).Select(i => i.DID).FirstOrDefault();
            data.Logins.Add(log);
            data.SaveChanges();
            var depid = data.Departments.Where(i => i.DepartmentName == depname).Select(i => i.DID).FirstOrDefault();
            sd.BID = data.Batches.Where(i => i.Semester == 1 && i.Year == DateTime.Now.Year && i.DID == depid).Select(i => i.BID).FirstOrDefault();
            sd.SID = log.LoginId;
            data.StudentDetails.Add(sd);
            data.SaveChanges();
            return 1;
        }
        public static int AddTeacher(string teachername, string confirmpassword, int age, string depname, string gender, int experience, string qualification)
        {


            Database data = new Database();
            Login log = new Login();
            Role rol = new Role();
            Department dep = new Department();
            Teacher td = new Teacher();


            log.UserName = teachername;
            log.Password = confirmpassword;
            log.RoleId = 2;
            log.Gender = gender;
            log.Age = age;

            log.DID = data.Departments.Where(i => i.DepartmentName == depname).Select(i => i.DID).FirstOrDefault();
            data.Logins.Add(log);
            data.SaveChanges();
            td.TID = log.LoginId;
            td.Qualification = qualification;
            td.Experience = experience;

            data.Teachers.Add(td);
            data.SaveChanges();
            return 1;
        }
        /* ____________________________________________________________________________________________________________________________________________________
             elhan*/
        public static int NotificationSend(int FromID, string Roles, string content)
        {
            Database db = new Database();
            Notification not = new Notification();
            Login log = new Login();
            Role rol = new Role();

            not.FromUserID = FromID;
            not.Roles = Roles;
            not.Content = content;
            not.Date = DateTime.Now;
            db.Notifications.Add(not);
            db.SaveChanges();
            return 1;
        }
        public static List<NotifyList> ViewNotifs(int logID)
        {
            Database db = new Database();
            Login log = new Login();
            Notification not = new Notification();
            string role = Data.roles(logID);
            List<NotifyList> notify = new List<NotifyList>();
            int dept = db.Logins.Where(i=> i.LoginId == logID).Select(i=> i.DID).FirstOrDefault();
         
                var NotifyList = db.Notifications.Where(i => (i.Roles == "All" && i.Roles != role)).OrderBy(i => i.Date);
            
            if(role=="Teacher"|| role=="Student")
            {
                 NotifyList = db.Notifications.Where( i => (i.Roles == "All" && db.Logins.Where(j => j.LoginId == i.FromUserID).Select(j => j.RoleId).FirstOrDefault() == 3) || ((i.Roles == "All" || i.Roles == role )&& (db.Logins.Where(j => j.LoginId == i.FromUserID).Select(j => j.RoleId).FirstOrDefault() == 4 )&& (dept == db.Logins.Where(j => j.LoginId == i.FromUserID).Select(j => j.DID).FirstOrDefault())) ).OrderBy(i => i.Date);
            }
            
                foreach (var i in NotifyList)
                {
                    NotifyList stud = new NotifyList();
                    stud.FromUser = db.Logins.Where(j => j.LoginId == i.FromUserID).Select(j => j.UserName).FirstOrDefault();
                    stud.RoleName = db.Roles.Where(k => k.RoleId == (db.Logins.Where(j => j.LoginId == i.FromUserID).Select(j => j.RoleId).FirstOrDefault())).Select(k => k.RoleName).FirstOrDefault(); ;
                      stud.Date_Time = i.Date;
                      stud.Content = i.Content;
                    notify.Add(stud);
                }
                return notify;
        }
    }
}
