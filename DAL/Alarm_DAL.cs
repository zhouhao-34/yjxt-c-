using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class Alarm_DAL
    {
        easyYJEntities DB = new easyYJEntities();
        public List<YJ_Product> alarmTask()
        {
            List<YJ_Product> pList = null;
            //重新定义连接数据，去除上次查询的缓存问题
            using(easyYJEntities db2 = new easyYJEntities())
            {
                pList = db2.YJ_Product.Where(p => p.status == 1 && (p.DValue > 0 || p.unit=="自然日")).ToList();
            }                     
            return pList;
        }
        public List<YJ_Product> alarmTask(int proID)
        {
            var list = DB.YJ_Product.Where(p=>p.proID==proID).ToList();
            return list;
        }
        public int messageSendAdd(int proID, string manageType)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            YJ_Manage manage = new YJ_Manage
            {
                proID = proID,
                manageType = manageType,
                sendStatus = "",
                createTime = currentTime,
                status = "否",
            };
            DB.YJ_Manage.Add(manage);
            int result = DB.SaveChanges();
            return manage.MID;
        }
        public bool messageSendLogAdd(int userID, string type, string address, int MID, string sendContent, string sendStatus)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            YJ_ManageSend yJ_ManageSend = new YJ_ManageSend
            {
                userID = userID,
                type = type,
                address = address,
                createTime = currentTime,
                sendTime = currentTime,
                sendContent = sendContent,
                MID = MID,
                sendStatus = sendStatus,
            };
            DB.YJ_ManageSend.Add(yJ_ManageSend);
            int result = DB.SaveChanges();
            if (result > 0)
            {
                //修改预警记录为已通知
                DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Manage set sendStatus='{0}' where MID='{1}'", "已通知", MID));
                return true;
            }
            return false;
        }
        public List<YJ_Manage> manage_MID(int MID)
        {
            using(easyYJEntities db = new easyYJEntities())
            {
                return db.YJ_Manage.Where(o => o.MID == MID).ToList();
            }
        }
    }
}
