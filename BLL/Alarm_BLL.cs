using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class Alarm_BLL
    {
        Alarm_DAL alarm_Dal = new Alarm_DAL();
        public List<YJ_Product> alarmTask()
        {
            return alarm_Dal.alarmTask();
        }
        /// <summary>
        /// 根据proID查询内容
        /// </summary>
        /// <param name="proID"></param>
        /// <returns></returns>
        public List<YJ_Product> alarmTask(int proID)
        {
            return alarm_Dal.alarmTask(proID);
        }
        /// <summary>
        /// 添加预警记录
        /// </summary>
        /// <param name="proID"></param>
        /// <param name="manageType"></param>
        /// <returns></returns>
        public int messageSendAdd(int proID,string manageType)
        {
            return alarm_Dal.messageSendAdd(proID, manageType);
        }
        /// <summary>
        /// 发送通知记录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type">手机，Email</param>
        /// <param name="address">手机号或Email地址</param>
        /// <param name="MID"></param>
        /// <param name="sendContent">发送的内容</param>
        /// <returns></returns>
        public bool messageSendLogAdd(int userID,string type,string address,int MID,string sendContent,string sendStatus)
        {
            return alarm_Dal.messageSendLogAdd(userID, type, address, MID, sendContent, sendStatus);
        }
        /// <summary>
        ///根据MID查询mange报警表内容
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public List<YJ_Manage> manage_MID(int MID)
        {
            return alarm_Dal.manage_MID(MID);
        }
    }
}
