using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class User_DAL
    {
        easyYJEntities DB = new easyYJEntities();
        public bool userAdd(string userName,string mobile,string Email,string password, int userType)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                var list = DB.YJ_User.Where(o=>(o.mobile == mobile || o.Email== Email || o.userName == userName) && o.status == 1).ToList();
                if (list.Count > 0)
                {
                    return false;
                }
                //密码转成md5
                string pwd = GetMd5FromString(password);
                YJ_User user = new YJ_User
                {
                    userName = userName,
                    password = pwd,
                    Email = Email,
                    mobile = mobile,
                    status = 1,
                    updateTime = DateTime.Now,
                    userType = userType,
                };
                DB.YJ_User.Add(user);
                int reslut = DB.SaveChanges();
                if (reslut > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public List<YJ_User> userList()
        {
            using(easyYJEntities db = new easyYJEntities())
            {
                var list = db.YJ_User.Where(o => o.status == 1).ToList();
                return list;
            }
        }
        public List<YJ_User> userList(int userID)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                var list = db.YJ_User.Where(o => o.status == 1 && o.userID == userID).ToList();
                return list;
            }            
        }
        public bool userEdit(string userName, string mobile, string Email, string password, int userType, int userID)
        {
            int reslut = 0;
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            var list = DB.YJ_User.Where(o => (o.mobile == mobile || o.Email == Email) && o.userID!=userID && o.status==1).ToList();
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            if (list.Count > 0)
            {
                return false;
            }
            //密码为空不修改
            if (string.IsNullOrEmpty(password))
            {
                reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_User set userName='{0}',mobile='{1}',Email='{2}',updateTime='{3}',userType='{4}' where userID='{5}'", userName, mobile, Email, currentTime, userType, userID));
            }
            else
            {
                string pwd = GetMd5FromString(password);
                reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_User set userName='{0}',mobile='{1}',Email='{2}',password='{3}',updateTime='{4}',userType='{5}' where userID='{6}'", userName, mobile, Email, pwd, currentTime, userType, userID));
            }
            
            if (reslut > 0)
            {
                return true;
            }
            return false;
        }

        public bool userDel(int userID)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            if (userID == 1)
            {
                return false;
            }
            int reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_User set status='{0}',updateTime='{1}' where userID='{2}'", 99, currentTime, userID));
            if (reslut > 0)
            {
                return true;
            }
            return false;
        }
        public Array login(string userName,string password)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    string pwd = GetMd5FromString(password);
                    var userList = db.YJ_User.Where(u => u.userName == userName && u.password == pwd && u.status==1).ToList();
                    if (userList.Count > 0)
                    {
                        return userList.ToArray();
                    }
                    Array[] arr = new Array[0] ;
                    return arr;
                }
            }catch(Exception ex)
            {
                Array[] arr = new Array[0];
                return arr;
            }
        }

        public bool plcAdd(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            try
            {
                var list = DB.YJ_PLC.Where(o => o.PLC_ip == PLC_ip && o.status==1).ToList();
                if (list.Count > 0)
                {
                    return false;
                }
                YJ_PLC plc = new YJ_PLC
                {
                    className = className,
                    PLC_name = PLC_name,
                    PLC_brand = PLC_brand,
                    PLC_model = PLC_model,
                    PLC_ip = PLC_ip,
                    PLC_port = Convert.ToInt32(PLC_port),
                    status = 1,
                    createTime = currentTime,
                };
                DB.YJ_PLC.Add(plc);
                int reslut = DB.SaveChanges();
                if (reslut > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public List<YJ_PLC> plcList()
        {
            using(easyYJEntities db = new easyYJEntities())
            {
                return db.YJ_PLC.Where(o => o.status == 1).ToList();
            }
            
        }
        public List<YJ_PLC> plcList(int plcID)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                return db.YJ_PLC.Where(o => o.status == 1 && o.plcID == plcID).ToList();
            }                
        }
        public bool plcEdit(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port,int plcID)
        {
            try
            {
                var list = DB.YJ_PLC.Where(o => o.PLC_ip == PLC_ip && o.status == 1 && o.plcID != plcID).ToList();
                if (list.Count > 0)
                {
                    return false;
                }
                System.DateTime currentTime = new System.DateTime();
                currentTime = System.DateTime.Now;
                int reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC set className='{0}',PLC_name='{1}',PLC_brand='{2}',PLC_model='{3}',PLC_ip='{4}',PLC_port='{5}',updateTime='{6}' where plcID='{7}'", className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port, DateTime.Now, plcID));
                if (reslut > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool plcDel(int plcID)
        {
            try
            {
                System.DateTime currentTime = new System.DateTime();
                currentTime = System.DateTime.Now;
                int reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC set status='{0}',updateTime='{1}' where plcID='{2}'", 99, currentTime, plcID));
                if (reslut > 0)
                {
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set status='{0}' where plcID='{1}'", 99, plcID));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 生成字符串的MD5码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string GetMd5FromString(string sInput)
        {
            var lstData = Encoding.GetEncoding("utf-8").GetBytes(sInput);
            var lstHash = new MD5CryptoServiceProvider().ComputeHash(lstData);
            var result = new StringBuilder(32);
            for (int i = 0; i < lstHash.Length; i++)
            {
                result.Append(lstHash[i].ToString("x2").ToUpper());
            }
            return result.ToString();
        }
    }
}
