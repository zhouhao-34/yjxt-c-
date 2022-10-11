using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class KanbanPlc_DAL
    {
        public int ReadUpdate(int plcListID, string val)
        {
            int result = 0;
            int leijiaVal = 0;//初值化累加值
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    
                    var _list = db.YJ_PLC_list.Where(pp => pp.plcListID == plcListID).ToList();
                    //int plcListID = _list[0].plcListID;
                    //string _readVal = val;
                    string shuju = _list[0].shuju_u;//上一次读取的数据
                    int modelID = Convert.ToInt32(_list[0].modelID);
                    string chuli = _list[0].chuli;

                    if (_list[0].chufa == 1)
                    {
                        //不等于空
                        if (!string.IsNullOrEmpty(val) && val != "0")
                        {
                            leijiaVal = proUpdate_DValue(_list, val, chuli);
                        }
                    }
                    else if (_list[0].chufa == 2)
                    {
                        if (_list[0].PLC_addressType == "bool")
                        {
                            //如果布尔型判断等于true还是等于false才能执行
                            if (Convert.ToBoolean(val) == true && _list[0].chufa == 4)
                            {
                                leijiaVal = proUpdate_DValue2(_list, val, chuli);
                            }
                            else if (Convert.ToBoolean(val) == false && _list[0].chufa == 5)
                            {
                                leijiaVal = proUpdate_DValue2(_list, val, chuli);
                            }
                        }
                        else if (shuju != val)
                        {
                            leijiaVal = proUpdate_DValue2(_list, val, chuli);
                        }
                    }
                    else if (_list[0].chufa == 3)//3和2处理数据程序相同
                    {
                        if (_list[0].PLC_addressType == "bool")
                        {
                            //如果布尔型判断等于true还是等于false才能执行
                            if (Convert.ToBoolean(val) == true && _list[0].chufa == 4)
                            {
                                leijiaVal = proUpdate_DValue2(_list, val, chuli);
                            }
                            else if (Convert.ToBoolean(val) == false && _list[0].chufa == 5)
                            {
                                leijiaVal = proUpdate_DValue2(_list, val, chuli);
                            }
                        }
                        //else if (shuju != readVal)
                        else if (!string.IsNullOrEmpty(val) || val != "0")
                        {
                            leijiaVal = proUpdate_DValue2(_list, val, chuli);
                        }
                    }
                    else if (_list[0].chufa == 4 || _list[0].chufa == 5)
                    {
                        if (_list[0].PLC_addressType == "bool")
                        {
                            //如果布尔型判断等于true还是等于false才能执行
                            if (Convert.ToBoolean(val) == true && _list[0].chufa == 4)
                            {
                                leijiaVal = proUpdate_DValue3(_list, val, chuli);
                            }
                            else if (Convert.ToBoolean(val) == false && _list[0].chufa == 5)
                            {
                                leijiaVal = proUpdate_DValue3(_list, val, chuli);
                            }
                        }
                    }
                    db.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set shuju_u='{0}',updateTime='{1}' where plcListID='{2}'", val, DateTime.Now, plcListID));
                    return leijiaVal;
                    //处理生产累加
                    //查询今天生产计划
                    //var planD = db.KB_ProductionPlan.OrderBy(o => o.planID).Where(o => o.planTime.Value.Day == DateTime.Now.Day && o.planTime.Value.Month == DateTime.Now.Month && o.planTime.Value.Year == DateTime.Now.Year).ToList();
                    //foreach(var item in planD)
                    //{
                    //    //如果不是当前生产型号AND生产开始时间不为空AND生产结束时间为空，那么修改生产结束时间为当前时间
                    //    if(item.ModelID!=modelID && !string.IsNullOrEmpty(item.startTime.ToString()) && string.IsNullOrEmpty(item.endTime.ToString()))
                    //    {
                    //        db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set endTime='{0}' where planID = '{1}'", DateTime.Now,item.planID));
                    //    }
                    //    //如果当前生产型号=计划生产型号AND开始时间为空，那么修改开始时间为当前时间
                    //    if (item.ModelID == modelID && string.IsNullOrEmpty(item.startTime.ToString()))
                    //    {
                    //        db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set startTime='{0}' where planID = '{1}'", DateTime.Now, item.planID));
                    //    }
                    //    int ActualVal = 0;
                    //    //修改实际生产数
                    //    if (chuli == "累加")
                    //    {
                    //        ActualVal = Convert.ToInt32(item.ActualProduction) + leijiaVal;
                    //    }else if(chuli == "替换")
                    //    {
                    //        ActualVal = leijiaVal;
                    //    }
                    //    else
                    //    {
                    //        return result;
                    //    }
                    //    result = db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set ActualProduction='{0}' where planID = '{1}'", ActualVal, item.planID));
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public int proUpdate_DValue(List<YJ_PLC_list> plcList, string plcReadValue, string chuli)
        {
            int _val = 0;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    if (plcList[0].PLC_addressType != "string")
                    {
                        if (chuli == "累加")
                        {
                            //累加方式 chuliType：1读取数据直接累加，2每次累加1
                            if (plcList[0].chuliType == 1)
                            {
                                if (plcList[0].PLC_addressType == "real")
                                {
                                    _val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                                }
                                else
                                {
                                    _val = Convert.ToInt32(plcReadValue);
                                }
                            }
                            else
                            {
                                _val = 1;
                            }
                        }
                        else if (chuli == "替换")
                        {
                            if (plcList[0].PLC_addressType == "real")
                            {
                                _val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                            }
                            else
                            {
                                _val = Convert.ToInt32(plcReadValue);
                            }
                        }
                    }
                    else
                    {
                        //其它类型，值不为空的情况下DValue+1，不能替换只能累加
                        if (!string.IsNullOrEmpty(plcReadValue))
                        {
                            if (chuli == "累加")
                            {
                                _val = 1;
                            }
                        }
                    }
                }
                return _val;
            }
            catch (Exception ex)
            {
                return _val;
            }
        }
        public int proUpdate_DValue2(List<YJ_PLC_list> plcList, string plcReadValue, string chuli)
        {
            int _val = 0;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {          
                    //1、判断数据类型
                    if (plcList[0].PLC_addressType != "string")
                    {
                        if (chuli == "累加")
                        {
                            //累加方式 chuliType：1读取数据直接累加，2每次累加1
                            if (plcList[0].chuliType == 1)
                            {
                                if (plcList[0].PLC_addressType == "real")
                                {
                                    _val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                                }
                                else
                                {
                                    _val = Convert.ToInt32(plcReadValue);
                                }
                            }
                            else
                            {
                                _val = 1;
                            }
                        }
                        else if (chuli == "替换")
                        {
                            if (plcList[0].PLC_addressType == "real")
                            {
                                _val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                            }
                            else
                            {
                                _val = Convert.ToInt32(plcReadValue);
                            }
                        }
                    }
                    else if (plcList[0].PLC_addressType == "bool")//是否bool型
                    {
                        bool _plcReadValue = Convert.ToBoolean(plcReadValue);
                        //BOOL类型为true的情况下DValue+1，不能替换只能累加
                        if (_plcReadValue == true)
                        {
                            if (chuli == "累加")
                            {
                                _val = 1;
                            }
                        }
                    }
                    else
                    {
                        //其它类型，值不为空的情况下DValue+1，不能替换只能累加
                        if (!string.IsNullOrEmpty(plcReadValue))
                        {
                            if (chuli == "累加")
                            {
                                _val = 1;
                            }
                        }
                    }

                }
                return _val;
            }
            catch (Exception ex)
            {
                return _val;
            }
            
        }
        public int proUpdate_DValue3(List<YJ_PLC_list> plcList, string plcReadValue, string chuli)
        {
            int _val = 0;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int proID = Convert.ToInt32(plcList[0].proID);
                    //1、判断数据类型必须是boll
                    if (plcList[0].PLC_addressType == "bool")//是否bool型
                    {
                        bool chufa = false;
                        if (plcList[0].chufa == 4)
                        {
                            chufa = true;
                        }
                        bool _plcReadValue = Convert.ToBoolean(plcReadValue);
                        //BOOL类型为true的情况下DValue+1，不能替换只能累加
                        if (_plcReadValue == chufa)
                        {
                            if (chuli == "累加")
                            {
                                _val = 1;
                            }
                        }
                    }
                }
                return _val;
            }
            catch (Exception ex)
            {
                return _val;
            }
            
        }
    }
}
