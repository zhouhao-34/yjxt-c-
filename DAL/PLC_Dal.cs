using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using DAL;
using Entity;
using System.Data.OleDb;
using System.Collections;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace DAL
{
    public class PLC_Dal
    {
        /// <summary>
        /// 创建PLC读取数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int PLC_list_add(string[] dataList)
        {
            int result = 0;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int wval = 0;
                    string val = null;
                    int ct = 0;
                    if (!string.IsNullOrEmpty(dataList[12]))
                    {
                        ct = Convert.ToInt32(dataList[12]);
                    }
                    if (!string.IsNullOrEmpty(dataList[11])){
                        wval = Convert.ToInt32(dataList[11]);
                    }
                    if (!string.IsNullOrEmpty(dataList[8]))
                    {
                        val = dataList[8];
                    }
                    YJ_PLC_list plc = new YJ_PLC_list();
                    plc.plcID = Convert.ToInt32(dataList[0]);
                    plc.PLC_adress = dataList[1];
                    plc.PLC_addressType = dataList[2];
                    plc.createTime = DateTime.Now;
                    plc.addressLenth = Convert.ToInt32(dataList[3]);
                    plc.status = 99;
                    plc.chufa = Convert.ToInt32(dataList[4]);
                    plc.where_PLC_address = dataList[5];
                    plc.where_tiaojian = dataList[6];
                    plc.where_content = dataList[7];
                    plc.returnVal = val;
                    plc.chuli = dataList[9];
                    plc.where_PLC_addressType = dataList[10];
                    plc.where_returnVal = wval>0?wval : (int?)null;
                    plc.chuliType = ct > 0 ? ct : (int?)0;
                    plc.Type_y = Convert.ToInt32(dataList[13]);
                    db.YJ_PLC_list.Add(plc);
                    result = db.SaveChanges();
                    if (result > 0)
                    {
                        return (int)plc.plcListID;
                    }
                    return 0;
                }
                
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        /// <summary>
        /// plc列表数据查询
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<YJ_plcList_PLC> PLC_listList(int plcListID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var query = (from plclist in db.YJ_PLC_list
                                 join plc in db.YJ_PLC
                                 on plclist.plcID equals plc.plcID
                                 where plclist.plcListID == plcListID
                                 select new YJ_plcList_PLC
                                 {
                                     plcID = plc.plcID,
                                     PLC_brand = plc.PLC_brand,
                                     PLC_ip = plc.PLC_ip,
                                     PLC_port = plc.PLC_port,
                                     PLC_name = plc.PLC_name,
                                     PLCstatus = plc.status,
                                     className = plc.className,
                                     PLC_model = plc.PLC_model,

                                     plcListID = plclist.plcListID,
                                     PLC_adress = plclist.PLC_adress,
                                     PLC_addressType = plclist.PLC_addressType,
                                     status = plclist.status,
                                     addressLenth = plclist.addressLenth,
                                     proID = plclist.proID,
                                     chufa = plclist.chufa,
                                     where_PLC_address = plclist.where_PLC_address,
                                     where_tiaojian = plclist.where_tiaojian,
                                     where_content = plclist.where_content,
                                     returnVal = plclist.returnVal,
                                     chuli = plclist.chuli,
                                     chuliType = (int?)plclist.chuliType,
                                     where_PLC_addressType= plclist.where_PLC_addressType,
                                     where_returnVal= (int?)plclist.where_returnVal,

                                 });
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void plcListLog_Add(int proID, int plcID, int plcListID, string plcVal, string type, string record, int status)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            using (easyYJEntities db = new easyYJEntities())
            {
                YJ_PLC_log log = new YJ_PLC_log
                {
                    proID = proID,
                    plcID = plcID,
                    plcListID = plcListID,
                    plcVal = plcVal,
                    type = type,
                    createtTime = currentTime,
                    record = record,
                    status = status,
                };
                db.YJ_PLC_log.Add(log);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                
            }
        }

        ///// <summary>
        ///// plc列表数据查询所有
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public List<YJ_PLC_list> PLC_list_all(int parentID,int plcLID)
        //{
        //    List<YJ_PLC_list> list = new List<YJ_PLC_list>();
        //    try
        //    {
        //        using (s_easy db = new s_easy())
        //        {

        //            if(parentID == -1)
        //            {
        //                var query = (from plc in db.Base_PLC
        //                             join plclist in db.Base_PLC_list
        //                             on plc.plcID equals plclist.PLCID
        //                             where plc.status == 1 && plclist.status == 1
        //                             select new Base_PLCLIST {
        //                                 plcID = plc.plcID,
        //                                 PLC_brand = plc.PLC_brand,
        //                                 PLC_ip = plc.PLC_ip,
        //                                 PLC_port = plc.PLC_port,
        //                                 PLC_name = plc.PLC_name,
        //                                 plcLID = plclist.plcLID,
        //                                 PLCID = plclist.PLCID,
        //                                 PLC_adress = plclist.PLC_adress,
        //                                 PLC_addressType = plclist.PLC_addressType,
        //                                 tableID = plclist.tableID,
        //                                 createTime = plclist.createTime,
        //                                 tableNameCN = plclist.tableNameCN,
        //                                 fieldNameCN = plclist.fieldNameCN,
        //                                 status = plclist.status == 2 ? "禁用" : "启用",
        //                             });
        //                list = query.ToList();
        //            }
        //            else if (parentID == -2)
        //            {
        //                var query = (from plc in db.Base_PLC
        //                             join plclist in db.Base_PLC_list
        //                             on plc.plcID equals plclist.PLCID
        //                             where plc.status == 1 && plclist.status == 1 && plclist.parentID > 0
        //                             select new Base_PLCLIST {
        //                                 plcID = plc.plcID,
        //                                 PLC_brand = plc.PLC_brand,
        //                                 PLC_ip = plc.PLC_ip,
        //                                 PLC_port = plc.PLC_port,
        //                                 PLC_name = plc.PLC_name,
        //                                 plcLID = plclist.plcLID,
        //                                 PLCID = plclist.PLCID,
        //                                 PLC_adress = plclist.PLC_adress,
        //                                 PLC_addressType = plclist.PLC_addressType,
        //                                 tableID = plclist.tableID,
        //                                 createTime = plclist.createTime,
        //                                 tableNameCN = plclist.tableNameCN,
        //                                 fieldNameCN = plclist.fieldNameCN,
        //                                 status = plclist.status == 2 ? "禁用" : "启用",
        //                             });
        //                list = query.ToList();
        //            }
        //            else
        //            {
        //                var query = (from plc in db.Base_PLC
        //                             join plclist in db.Base_PLC_list
        //                             on plc.plcID equals plclist.PLCID
        //                             where plc.status == 1 && plclist.status == 1 && plclist.parentID == parentID
        //                             select new Base_PLCLIST {
        //                                 plcID = plc.plcID,
        //                                 PLC_brand = plc.PLC_brand,
        //                                 PLC_ip = plc.PLC_ip,
        //                                 PLC_port = plc.PLC_port,
        //                                 PLC_name = plc.PLC_name,
        //                                 plcLID = plclist.plcLID,
        //                                 PLCID = plclist.PLCID,
        //                                 PLC_adress = plclist.PLC_adress,
        //                                 PLC_addressType = plclist.PLC_addressType,
        //                                 tableID = plclist.tableID,
        //                                 createTime = plclist.createTime,
        //                                 tableNameCN = plclist.tableNameCN,
        //                                 fieldNameCN = plclist.fieldNameCN,
        //                                 status = plclist.status == 2 ? "禁用" : "启用",
        //                             });
        //                list = query.ToList();
        //            }
        //            if (plcLID > 0)
        //            {
        //                var query = (from plc in db.Base_PLC
        //                             join plclist in db.Base_PLC_list
        //                             on plc.plcID equals plclist.PLCID
        //                             where plc.status == 1 && plclist.status == 1 && plclist.parentID == parentID && plcLID == plcLID
        //                             select new Base_PLCLIST {
        //                                 plcID = plc.plcID,
        //                                 PLC_brand = plc.PLC_brand,
        //                                 PLC_ip = plc.PLC_ip,
        //                                 PLC_port = plc.PLC_port,
        //                                 PLC_name = plc.PLC_name,
        //                                 plcLID = plclist.plcLID,
        //                                 PLCID = plclist.PLCID,
        //                                 PLC_adress = plclist.PLC_adress,
        //                                 PLC_addressType = plclist.PLC_addressType,
        //                                 tableID = plclist.tableID,
        //                                 createTime = plclist.createTime,
        //                                 tableNameCN = plclist.tableNameCN,
        //                                 fieldNameCN = plclist.fieldNameCN,
        //                                 status = plclist.status == 2 ? "禁用" : "启用",
        //                             });
        //                list = query.ToList();
        //            }
        //            return list;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 删除PLC列表
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public int PLC_list_delete(int plcLID)
        //{
        //    try
        //    {
        //        using (s_easy db = new s_easy())
        //        {
        //            return db.Database.ExecuteSqlCommand(string.Format(@"delete Base_PLC_list where plcLID='{0}'", plcLID));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
