using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Linq.Expressions;

namespace BLL
{
    public class PLC_BLL
    {
        PLC_Dal plcDal = new PLC_Dal();
       
        /// <summary>
        /// 创建PLC读取数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int PLC_list_add(string[] dataList)
        {
            return plcDal.PLC_list_add(dataList);
        }

        /// <summary>
        /// 查找指定plcListID查询plc和plclist信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<YJ_plcList_PLC> PLC_listList(int plcListID)
        {
            return plcDal.PLC_listList(plcListID);
        }
        ///// <summary>
        ///// plc列表数据查询所有
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public List<YJ_PLC_list> PLC_list_all(int parentID, int plcLID)
        //{
        //    return plcDal.PLC_list_all(parentID, plcLID);
        //}
        ///// <summary>
        ///// 删除PLC列表
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public int PLC_list_delete(int plcLID)
        //{
        //    return plcDal.PLC_list_delete(plcLID);
        //}

        /// <summary>
        /// PLC读写记录日志
        /// </summary>
        /// <param name="proID">产品表ID</param>
        /// <param name="plcID">所属PLC表ID</param>
        /// <param name="plcListID">plc_list表ID</param>
        /// <param name="plcVal">读写的内容</param>
        /// <param name="type">类型：读，写</param>
        /// <param name="record">失败信息记录</param>
        public void plcListLog_Add(int proID,int plcID,int plcListID,string plcVal,string type,string record, int status)
        {
            plcDal.plcListLog_Add(proID, plcID, plcListID, plcVal, type, record, status);
        }
    }
}
