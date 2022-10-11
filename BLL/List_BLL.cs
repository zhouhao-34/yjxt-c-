using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class List_BLL
    {
        List_DAL list_DAL = new List_DAL();
        /// <summary>
        /// 查询菜单无限级最大分多少级
        /// </summary>
        /// <returns></returns>
        public int menuMaxClass()
        {
            return list_DAL.menuMaxClass();
        }
        /// <summary>
        /// 查询3天的数据求平均用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<YJ_ProductXiaohao> listThree(int proID)
        {
            return list_DAL.listThree(proID);
        }
        /// <summary>
        /// 添加监控项
        /// </summary>
        /// <param name="proList">监控项</param>
        /// <param name="weihuzhe">维护者多选</param>
        /// <returns></returns>
        public bool listAdd(Object[] proList, ArrayList weihuzhe)
        {
            return list_DAL.listAdd(proList, weihuzhe);
        }
        /// <summary>
        /// 根据proID查询产品信息
        /// </summary>
        /// <param name="proID"></param>
        /// <returns></returns>
        public List<YJ_Product> proListFrist(int proID)
        {
            return list_DAL.proListFrist(proID);
        }
        /// <summary>
        /// 保存处理预警-维保成功
        /// </summary>
        /// <param name="proList"></param>
        /// <param name="weihuzhe"></param>
        /// <returns></returns>
        public bool listWeibaoAdd(Object[] proList, ArrayList weihuzhe)
        {
            return list_DAL.listWeibaoAdd(proList, weihuzhe);
        }
        /// <summary>
        /// 保存处理预警-更换设备
        /// </summary>
        /// <param name="proList"></param>
        /// <param name="weihuzhe"></param>
        /// <returns></returns>
        public bool listWeibaoAdd2(Object[] proList, ArrayList weihuzhe)
        {
            return list_DAL.listWeibaoAdd2(proList, weihuzhe);
        }
        /// <summary>
        /// 查询报警日志列表
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array proLogList(int PageIndex, int PageSize, int seach_menuID, string seach_type)
        {
            return list_DAL.proLogList(PageIndex, PageSize, seach_menuID,seach_type);
        }
        /// <summary>
        /// 根据产品ID查询维护人信息
        /// </summary>
        /// <param name="proID"></param>
        /// <returns></returns>
        public List<YJ_proUser> userList(int proID)
        {
            return list_DAL.userList(proID);
        }
        /// <summary>
        /// 预警日志ID查询通知人及通知信息
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public List<YJ_ManageSend> manageSendList(int MID)
        {
            return list_DAL.manageSendList(MID);
        }
        /// <summary>
        /// 根据userID查询会员信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<YJ_User> userList_U(int userID)
        {
            return list_DAL.userList_U(userID);
        }
        /// <summary>
        /// 查询处理日志列表
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array proLogListCL(int PageIndex, int PageSize, int seach_menuID)
        {
            return list_DAL.proLogListCL(PageIndex, PageSize, seach_menuID);
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="proID"></param>
        /// <returns></returns>
        public bool proDel(int proID)
        {
            return list_DAL.proDel(proID);
        }
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="proID"></param>
        /// <returns></returns>
        public bool proEdit(Object[] proList, ArrayList weihuzhe)
        {
            return list_DAL.proEdit(proList, weihuzhe);
        }
        /// <summary>
        /// 根据ID查询当前需要修改的数据
        /// </summary>
        /// <returns></returns>
        public List<YJ_ProductList> proEdit_list(int proID)
        {
            return list_DAL.proEdit_list(proID);
        }
        /// <summary>
        /// 1数据不为空，修改产品表的当前值。要求PLC收到的值必须是数字
        /// </summary>
        /// <param name="proID">product表ID</param>
        /// <param name="plcReadValue">PLC读取的值</param>
        /// <param name="chuli">数据处理方式：替换，累加</param>
        /// <returns></returns>
        //public bool proUpdate_DValue(List<YJ_PLC_list> plcList, string plcReadValue,string chuli, int plcListID)
        //{
        //    return list_DAL.proUpdate_DValue(plcList, plcReadValue, chuli, plcListID);
        //}
        ///// <summary>
        ///// 2与上一次不同,如果收到的数据是int型执行，累加或替换。bit即bool型=true执行DValue累加+1,其它类型不为空执行DValue累加+1
        ///// </summary>
        ///// <param name="plcList">plc_list表数组</param>
        ///// <param name="plcReadValue"></param>
        ///// <param name="chuli"></param>
        ///// <returns></returns>
        //public bool proUpdate_DValue2(List<YJ_PLC_list> plcList, string plcReadValue, string chuli)
        //{
        //    return list_DAL.proUpdate_DValue2(plcList, plcReadValue, chuli);
        //}
        ///// <summary>
        ///// 3只处理bit型，对应chufa=4,5
        ///// </summary>
        ///// <param name="plcList">plc_list表数组</param>
        ///// <param name="plcReadValue"></param>
        ///// <param name="chuli"></param>
        ///// <returns></returns>
        //public bool proUpdate_DValue3(List<YJ_PLC_list> plcList, string plcReadValue, string chuli)
        //{
        //    return list_DAL.proUpdate_DValue3(plcList, plcReadValue, chuli);
        //}
    }
}
