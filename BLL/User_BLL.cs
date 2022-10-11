using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class User_BLL
    {
        User_DAL user_DAL = new User_DAL();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Array login(string userName, string password)
        {
            return user_DAL.login(userName, password);
        }
        /// <summary>
        /// 添加维护人
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mobile"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool userAdd(string userName, string mobile, string Email, string password, int userType)
        {
            return user_DAL.userAdd(userName, mobile, Email, password, userType);
        }
        /// <summary>
        /// 查询所有维护者
        /// </summary>
        /// <returns></returns>
        public List<YJ_User> userList()
        {
            return user_DAL.userList();
        }
        /// <summary>
        /// 根据userID查询会员信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<YJ_User> userList(int userID)
        {
            return user_DAL.userList(userID);
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mobile"></param>
        /// <param name="Email"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool userEdit(string userName, string mobile, string Email, string password, int userType, int userID)
        {
            return user_DAL.userEdit(userName, mobile, Email, password, userType,userID);
        }
        /// <summary>
        /// 删除维护者
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool userDel(int userID)
        {
            return user_DAL.userDel(userID);
        }
        /// <summary>
        /// 添加PLC
        /// </summary>
        /// <param name="className">所属车间</param>
        /// <param name="PLC_name">PLC名称</param>
        /// <param name="PLC_brand">PLC品牌</param>
        /// <param name="PLC_model">PLC型号</param>
        /// <param name="PLC_ip">IP地址</param>
        /// <param name="PLC_port">端口号</param>
        /// <returns></returns>
        public bool plcAdd(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port)
        {
            return user_DAL.plcAdd(className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port);
        }
        /// <summary>
        /// 查询PLC列表
        /// </summary>
        /// <returns></returns>
        public List<YJ_PLC> plcList()
        {
            return user_DAL.plcList();
        }
        /// <summary>
        /// 根据ID查询PLC设置信息
        /// </summary>
        /// <returns></returns>
        public List<YJ_PLC> plcList(int plcID)
        {
            return user_DAL.plcList(plcID);
        }
        /// <summary>
        /// 修改PLC
        /// </summary>
        /// <param name="className">所属车间</param>
        /// <param name="PLC_name">PLC名称</param>
        /// <param name="PLC_brand">PLC品牌</param>
        /// <param name="PLC_model">PLC型号</param>
        /// <param name="PLC_ip">IP地址</param>
        /// <param name="PLC_port">端口号</param>
        /// <param name="plcID">主键</param>
        /// <returns></returns>
        public bool plcEdit(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port, int plcID)
        {
            return user_DAL.plcEdit(className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port, plcID);
        }
        /// <summary>
        /// 删除PLC
        /// </summary>
        /// <param name="plcID"></param>
        /// <returns></returns>
        public bool plcDel(int plcID)
        {
            return user_DAL.plcDel(plcID);
        }
    }
}
