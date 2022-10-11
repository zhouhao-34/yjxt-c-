using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class Log_DAL
    {
        public void Log_ALL(string logContent)
        {
            using(easyYJEntities db = new easyYJEntities())
            {
                YJ_LogALL LA = new YJ_LogALL()
                {
                    logContent = logContent,
                    createTime = DateTime.Now,
                };
                db.YJ_LogALL.Add(LA);
                db.SaveChanges();
            }
        }
    }
}
