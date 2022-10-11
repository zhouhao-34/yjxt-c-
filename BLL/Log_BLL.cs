using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Log_BLL
    {
        Log_DAL log_DAL = new Log_DAL();
        /// <summary>
        /// 全局LOG保存
        /// </summary>
        /// <param name="logContent"></param>
        public void Log_ALL(string logContent)
        {
            log_DAL.Log_ALL(logContent);
        }
    }
}
