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
    public class Kanban_DAL
    {
        ArrayList M = new ArrayList();
        public string modelAdd(string ModelName, string intervalTime, string ChangeTime, int plcListID,int PLC_modelID)
        {
            try
            {
                if (string.IsNullOrEmpty(ModelName) || Convert.ToDecimal(intervalTime) <= 0 || Convert.ToDecimal(ChangeTime) <= 0)
                {
                    return "参数不全";
                }
                using (easyYJEntities db = new easyYJEntities())
                {
                    var list = db.KB_Model.Where(m => m.ModelName == ModelName).ToList();
                    if (list.Count > 0)
                    {
                        return "型号名称已存在";
                    }
                    KB_Model md = new KB_Model
                    {
                        ModelName = ModelName,
                        intervalTime = Convert.ToDecimal(intervalTime),
                        ChangeTime = Convert.ToDecimal(ChangeTime),
                        plcListID = plcListID,
                        PLC_modelID = PLC_modelID,
                    };
                    db.KB_Model.Add(md);
                    int reslut = db.SaveChanges();
                    if (reslut > 0)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("update YJ_PLC_list set modelID = '{0}',updateTime='{1}',status=1 where plcListID='{2}'", md.modelID, DateTime.Now, plcListID));
                        return "成功";
                    }
                    return "失败";
                }
            }
            catch(Exception ex)
            {
                return "出错啦";
            }
            
        }
        public Array modelList(int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Model.OrderBy(o => o.modelID).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                    int[] TolistCount = new int[1] { db.KB_Model.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_Model> modelList()
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Model.OrderBy(o => o.modelID).ToList();
                    return Tolist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_Model> _modelList(string modelName)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Model.OrderBy(o => o.modelID);
                    if (!string.IsNullOrEmpty(modelName))
                    {
                        Tolist = (IOrderedQueryable<KB_Model>)Tolist.Where(v => v.ModelName.Contains(modelName));
                    }                    
                    return Tolist.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_Model> modelList(int modelID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Model.Where(o => o.modelID == modelID).ToList();
                    return Tolist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string modelEdit(string ModelName, string intervalTime, string ChangeTime, int plcListID,int PLC_modelID, int modelID)
        {
            try
            {
                if (string.IsNullOrEmpty(ModelName) || Convert.ToDecimal(intervalTime) <= 0 || Convert.ToDecimal(ChangeTime) <= 0)
                {
                    return "参数不全";
                }
                using (easyYJEntities db = new easyYJEntities())
                {
                    var list = db.KB_Model.Where(m => m.ModelName == ModelName && m.modelID != modelID).ToList();
                    if (list.Count > 0)
                    {
                        return "型号名称已存在";
                    }
                    int reslut=db.Database.ExecuteSqlCommand(string.Format("update KB_Model set ModelName='{0}',intervalTime='{1}',ChangeTime='{2}',plcListID='{3}',PLC_modelID='{4}' where modelID='{5}'", ModelName, Convert.ToDecimal(intervalTime), Convert.ToDecimal(ChangeTime), plcListID, PLC_modelID, modelID));
                    if (reslut > 0)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("update YJ_PLC_list set modelID = '{0}',updateTime='{1}',status=1 where plcListID='{2}'", modelID, DateTime.Now, plcListID));
                        return "成功";
                    }
                    return "失败";
                }
            }
            catch (Exception ex)
            {
                return "出错啦";
            }
        }
        public bool modelDel(int modelID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var planList = db.KB_Model.Where(p => p.modelID == modelID).ToList();
                    if (planList.Count == 0)
                    {
                        return false;
                    }
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("delete KB_Model where modelID='{0}'", modelID));
                    if (reslut > 0)
                    {
                        int plcListID = (int)planList[0].plcListID;
                        db.Database.ExecuteSqlCommand(string.Format("delete YJ_PLC_list where plcListID='{0}'", plcListID));
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string shiftAdd(string ShiftName, string startTime, string endTime)
        {
            try
            {
                if(string.IsNullOrEmpty(ShiftName) || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime))
                {
                    return "班组名/开始时间/结束时间不能为空";
                }
                using(easyYJEntities db = new easyYJEntities())
                {
                    var list = db.KB_Shift.Where(s=>s.ShiftName == ShiftName).ToList();
                    if (list.Count > 0)
                    {
                        return "班组名称已存在";
                    }
                    KB_Shift shift = new KB_Shift
                    {
                        ShiftName=ShiftName,
                        startTime=startTime,
                        endTime=endTime,
                    };
                    db.KB_Shift.Add(shift);
                    int reslut = db.SaveChanges();
                    if (reslut > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public Array shiftList(int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Shift.OrderBy(o => o.shiftID).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                    int[] TolistCount = new int[1] { db.KB_Shift.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_Shift> shiftList()
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_Shift.OrderBy(o => o.shiftID).ToList();
                    return Tolist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string shiftEdit(string ShiftName, string startTime, string endTime, int shiftID)
        {
            try
            {
                if (string.IsNullOrEmpty(ShiftName) || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime) || string.IsNullOrEmpty(shiftID.ToString()))
                {
                    return "参数不全";
                }
                using (easyYJEntities db = new easyYJEntities())
                {
                    var list = db.KB_Shift.Where(s => s.ShiftName == ShiftName && s.shiftID!= shiftID).ToList();
                    if (list.Count > 0)
                    {
                        return "班组名称已存在";
                    }
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("update KB_Shift set ShiftName = '{0}',startTime='{1}',endTime='{2}' where shiftID='{3}'", ShiftName, startTime, endTime,shiftID));
                    if (reslut > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }
            catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public bool shiftDel(int shiftID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var planList = db.KB_Shift.Where(p => shiftID == shiftID).ToList();
                    if (planList.Count == 0)
                    {
                        return false;
                    }
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("delete KB_Shift where shiftID='{0}'", shiftID));
                    if (reslut > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string planAdd(string ShiftName, int modelID, int planNub, int actualNub, string pandTime)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    var modelList = db.KB_Model.Where(m => m.modelID == modelID).ToList();
                    if (modelList.Count == 0)
                    {
                        return "未找到可生产的型号";
                    }
                    DateTime Ptime = Convert.ToDateTime(pandTime);
                    var planList = db.KB_ProductionPlan.Where(p => p.planTime.Value.Year == Ptime.Year && p.planTime.Value.Month == Ptime.Month && p.planTime.Value.Day == Ptime.Day && p.ModelID==modelID).ToList();
                    if (planList.Count > 0)
                    {
                        return "生产型号已在当天生产中，请修改生产数量";
                    }
                    KB_ProductionPlan plan = new KB_ProductionPlan
                    {
                        Shift = ShiftName,
                        ModelID = modelID,
                        ModelName = modelList[0].ModelName,
                        Planproduction = planNub,
                        ActualProduction = actualNub,
                        planTime = Convert.ToDateTime(pandTime),
                        createTime = DateTime.Now,
                    };
                    db.KB_ProductionPlan.Add(plan);
                    int reslut = db.SaveChanges();
                    if (reslut > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public string planEdit(string ShiftName, int modelID, int planNub, int actualNub, string pandTime, int planID)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    var modelList = db.KB_Model.Where(m => m.modelID == modelID).ToList();
                    if (modelList.Count == 0)
                    {
                        return "未找到可生产的型号";
                    }
                    var planList = db.KB_ProductionPlan.Where(p => p.planID == planID).ToList();
                    if (planList.Count == 0)
                    {
                        return "修改计划不存在";
                    }
                    //if (!string.IsNullOrEmpty(planList[0].Planproduction.ToString()))
                    //{
                    //    return "已生产或生产中计划不能修改";
                    //}
                    //需要修改的计划必须是：计划生产日期大于等于当前日期。已经过去的计划不能修改
                    if (planList[0].planTime.Value.Year == DateTime.Now.Year && planList[0].planTime.Value.Month >= DateTime.Now.Month && planList[0].planTime.Value.Day >= DateTime.Now.Day)
                    {
                        int reslut = db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set Shift = '{0}',ModelID = '{1}',ModelName = '{2}',Planproduction = '{3}',ActualProduction = '{4}',planTime = '{5}',createTime = '{6}' where planID='{7}'", ShiftName, modelID, modelList[0].ModelName, planNub, actualNub, pandTime, DateTime.Now, planID));
                        if (reslut > 0)
                        {
                            return "成功";
                        }
                    }
                    else
                    {
                        return "计划生产时间已过";
                    }
                    return "失败";
                }
            }catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public string planDel(int planID)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    var planList = db.KB_ProductionPlan.Where(p => p.planID == planID).ToList();
                    if (planList.Count == 0)
                    {
                        return "修改计划不存在";
                    }
                    if (!string.IsNullOrEmpty(planList[0].Planproduction.ToString()))
                    {
                        return "已生产或生产中计划不能删除";
                    }
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("delete KB_ProductionPlan where planID='{0}'",planID));
                    if (reslut > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public Array planList(int PageSize, int PageIndex,string startTime,string endTime,string modelName)
        {
            try
            {
                
                using(easyYJEntities db = new easyYJEntities())
                {
                    if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                    {
                        var Tolist = db.KB_ProductionPlan.OrderByDescending(o => o.planTime).Where(o => o.ModelName.Contains(modelName)).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                        int[] TolistCount = new int[1] { db.KB_ProductionPlan.Count() };
                        Array[] arr = new Array[2] { TolistCount, Tolist };
                        return arr;
                    }
                    else
                    {
                        DateTime sTime = Convert.ToDateTime(startTime);
                        DateTime eTime = Convert.ToDateTime(endTime);
                        var Tolist = db.KB_ProductionPlan.OrderByDescending(o => o.planTime).Where(o => o.ModelName.Contains(modelName) && o.planTime>=sTime && o.planTime<=eTime).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                        int[] TolistCount = new int[1] { db.KB_ProductionPlan.Count() };
                        Array[] arr = new Array[2] { TolistCount, Tolist };
                        return arr;
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_ProductionPlan> planListD()
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    List<KB_ProductionPlan> Tolist = null;
                    string st1 = "15:30";
                    string st2 = DateTime.Now.ToString("HH:mm");
                    DateTime dt1 = Convert.ToDateTime(st1);
                    DateTime dt2 = Convert.ToDateTime(st2);
                    //if (DateTime.Compare(dt1, dt2) > 0)
                    //{
                        Tolist = db.KB_ProductionPlan.OrderBy(o => o.planID).Where(o => o.planTime.Value.Day == DateTime.Now.Day && o.planTime.Value.Month == DateTime.Now.Month && o.planTime.Value.Year == DateTime.Now.Year).ToList();
                        
                    //}
                    //else
                    //{
                    //    Tolist = db.KB_ProductionPlan.Where(o => o.planID == -1).ToList();

                    //}
                    return Tolist;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Array DataSaveList(int PageSize, int PageIndex, string startTime, string endTime, string modelName)
        {
            try
            {
               
                using (easyYJEntities db = new easyYJEntities())
                {
                    if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                    {
                        var Tolist = db.KB_DataSave.OrderBy(o => o.ID).Where(o => o.ModelName.Contains(modelName)).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                        int[] TolistCount = new int[1] { db.KB_DataSave.Count() };
                        Array[] arr = new Array[2] { TolistCount, Tolist };
                        return arr;
                    }
                    else
                    {
                        DateTime sTime = Convert.ToDateTime(startTime);
                        DateTime eTime = Convert.ToDateTime(endTime);
                        var Tolist = db.KB_DataSave.OrderBy(o => o.ID).Where(o => o.ModelName.Contains(modelName) && o.Datetime >= sTime && o.Datetime<= eTime).Skip(PageSize * PageIndex).Take(PageSize).ToArray();
                        int[] TolistCount = new int[1] { db.KB_DataSave.Count() };
                        Array[] arr = new Array[2] { TolistCount, Tolist };
                        return arr;
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PLC_guding(int plcID, string plc_address, int GID,int addressLenth)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("update KB_PLC_guding set plcID='{0}',plc_address='{1}',addressLenth='{2}',updateTime='{3}' where GID='{4}'", plcID, plc_address, addressLenth,DateTime.Now, GID));
                    if (reslut > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }
            catch (Exception ex)
            {
                return "失败";
            }
        }
        public List<KB_PLC_guding> PLC_gudingList()
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    return db.KB_PLC_guding.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_DataSave> kanban()
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    return db.KB_DataSave.OrderBy(o => o.ID).Where(o => o.Datetime.Value.Day == DateTime.Now.Day&& o.Datetime.Value.Month == DateTime.Now.Month && o.Datetime.Value.Year == DateTime.Now.Year).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string mojuAdd(string mojuName, int liftNub, int[] modelID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    //modelID = new int[]{ 1,2,3};
                    var modelList = db.KB_Model.Where(m => modelID.Contains(m.modelID)).ToList();
                    var mojuList = db.KB_moju.Where(mm => mm.mojuName == mojuName).ToList();
                    if (mojuList.Count > 0)
                    {
                        return "模具已存在";
                    }
                    KB_moju mj = new KB_moju
                    {
                        mojuName = mojuName,
                        mojuNub = 0,
                        liftNub = liftNub,
                        createTime = DateTime.Now,
                        status = 1,
                    };
                    db.KB_moju.Add(mj);
                    int result = db.SaveChanges();
                    foreach (var item in modelList)
                    {
                        KB_mojuModel mjm = new KB_mojuModel
                        {
                            modelID = item.modelID,
                            modelName = item.ModelName,
                            mjID = mj.mjID,
                            status=1,
                        };
                        db.KB_mojuModel.Add(mjm);
                    }
                    db.SaveChanges();
                    if (result > 0)
                    {
                        return "成功";
                    }
                    return "失败";
                }
            }
            catch(Exception ex)
            {
                return "出错啦";
            }            
        }
        public string mujuWeihu(int mjID) {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("update KB_moju set mojuNub=0,baoyangTime='{0}' where mjID='{1}'",  DateTime.Now, mjID));
                    return "成功";
                }
            }
            catch (Exception)
            {
                return "失败";
            }
        }
        public string mojuEdit(int mjID, string mojuName, int liftNub, int[] modelID)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    int reslut = db.Database.ExecuteSqlCommand(string.Format(@"update KB_moju set mojuName='{0}',liftNub='{1}',updateTime='{2}' where mjID='{3}'", mojuName,liftNub,DateTime.Now,mjID));
                    if (reslut > 0)
                    {
                        //删除关联型号从新添加
                        db.Database.ExecuteSqlCommand(string.Format("update KB_mojuModel set status='{0}' where mjID='{1}'", 99, mjID));
                        var modelList = db.KB_Model.Where(m => modelID.Contains(m.modelID)).ToList();
                        foreach (var item in modelList)
                        {
                            KB_mojuModel mjm = new KB_mojuModel
                            {
                                modelID = item.modelID,
                                modelName = item.ModelName,
                                mjID = mjID,
                                status=1,
                            };
                            db.KB_mojuModel.Add(mjm);
                        }
                        db.SaveChanges();
                        return "成功";
                    }
                    return "失败";
                }
            }catch(Exception ex)
            {
                return "出错啦";
            }
        }
        public Array mojuList(int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_moju.OrderBy(o => o.mojuNub).Where(o=>o.status==1).Skip(PageSize * PageIndex).Take(PageSize);
                    Tolist = Tolist.Where(v => v.status <99);
                    int[] TolistCount = new int[1] { db.KB_moju.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist.ToArray() };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string deleteMuju(int mjID) {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    //var cangkuList = db.KB_cangKu.Where(v => v.ckID == ckID).ToList();
                    //var num = cangkuList[0].ckNub - ckNub;
                    db.Database.ExecuteSqlCommand(string.Format("update KB_moju set status=99 where mjID='{0}'", mjID));
                    var Tolist = db.KB_mojuModel.Where(v => v.mjID == mjID).ToList();
                    for (int i = 0; i < Tolist.Count(); i++)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("update KB_mojuModel set status=99 where mjID='{0}'", mjID));
                    }
                    return "成功";
                }
            }
            catch (Exception)
            {

                return "失败";
            }
        }
        public List<KB_moju2> mojuListOnly(int[] modelID)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    var tolist = (from mm in db.KB_mojuModel join mj in db.KB_moju on mm.mjID equals mj.mjID where modelID.Contains((int)mm.modelID) && mj.status==1 && mm.status==1
                                  select new KB_moju2 {
                                      mjID=mj.mjID,
                                      mojuName = mj.mojuName,
                                      mojuNub = mj.mojuNub,
                                      liftNub = mj.liftNub,
                                      createTime = mj.createTime,
                                      baoyangTime = mj.baoyangTime,
                                      updateTime = mj.updateTime,
                                      status = mj.status
                                  }).ToList();
                    return tolist;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Array modelMujuList(int mjID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_mojuModel.Where(v => v.mjID == mjID && v.status==1);
                    return Tolist.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Array warehouseList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_cangKu.OrderBy(o => o.ckID).Skip(PageSize * PageIndex).Take(PageSize);
                    if (productName != "") {
                        Tolist = Tolist.Where(v => v.productName == productName);
                    }
                    if (bandName != "")
                    {
                        Tolist = Tolist.Where(v => v.bandName == bandName);
                    }
                    if (modelName != "")
                    {
                        Tolist = Tolist.Where(v => v.modelName == modelName);
                    }
                    int[] TolistCount = new int[1] { db.KB_cangKu.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist.ToArray() };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string cangkuAdd(string productName, string bandName, string modelName, int ckNub,int userID,string userName, string mark, int[] menuID,int[] number)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int ckID = 0;
                    var cangkuList = db.KB_cangKu.Where(v => v.productName== productName&& v.bandName == bandName && v.modelName == modelName).ToList();
                    if (cangkuList.Count ==1)
                    {
                        var num= cangkuList[0].ckNub+ckNub;
                        db.Database.ExecuteSqlCommand(string.Format("update KB_cangKu set ckNub='{0}' where ckID='{1}'", num, cangkuList[0].ckID));
                        ckID = cangkuList[0].ckID;
                    }
                    else
                    {
                        KB_cangKu ck = new KB_cangKu
                        {
                            productName = productName,
                            bandName = bandName,
                            modelName = modelName,
                            ckNub = ckNub,
                            updateTime = DateTime.Now,
                        };
                        db.KB_cangKu.Add(ck);
                        db.SaveChanges();
                        ckID = ck.ckID;
                    }
                    cangKuIn(productName, bandName, modelName, ckNub, userID, userName);
                    cangkuSY(bandName, modelName, ckID, menuID, number);
                    return "成功";
                }
            }
            catch (Exception ex)
            {
                return "出错啦";
            }
        }
        public string addStock(string productName, string bandName, string modelName, int ckNub, int userID, string userName)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int ckID = 0;
                    var cangkuList = db.KB_cangKu.Where(v => v.productName == productName && v.bandName == bandName && v.modelName == modelName).ToList();
                    
                        var num = cangkuList[0].ckNub + ckNub;
                        db.Database.ExecuteSqlCommand(string.Format("update KB_cangKu set ckNub='{0}' where ckID='{1}'", num, cangkuList[0].ckID));
                        ckID = cangkuList[0].ckID;
                    cangKuIn(productName, bandName, modelName, ckNub, userID, userName);
                    return "成功";
                }
            }
            catch (Exception ex)
            {
                return "出错啦";
            }
        }
        public void cangKuIn(string productName, string bandName, string modelName, int ckNub, int userID, string userName) {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    DateTime dangTime = Convert.ToDateTime(DateTime.Now.ToString());
                    KB_cangKuIn ckIN = new KB_cangKuIn
                    {
                         productName = productName,
                         bandName = bandName,
                         modelName = modelName,
                         inNub = ckNub,
                        ruTime= dangTime,
                        userID= userID,
                        userName= userName,                        
                    };
                    db.KB_cangKuIn.Add(ckIN);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 处理入库适用型号
        /// </summary>
        /// <param name="bandName"></param>
        /// <param name="modelName"></param>
        /// <param name="ckID"></param>
        /// <param name="menuID"></param>
        public void cangkuSY(string bandName, string modelName, int ckID, int[] menuID,int[] number)
        {
            using(easyYJEntities db = new easyYJEntities())
            {
                //创建数组保存添加成功的适用设备表ID，通过ID反向删除旧数据
                int mCount = menuID.Count();
                int[] syID = new int[mCount];
                //根据适用设备的ID查询设备
                var menuList = db.YJ_Menu.Where(m => menuID.Contains(m.menuID)).ToList();
                int i = 0;
                foreach(var item in menuList)
                {
                    //查询适用设备数据是否存在
                    var shebeiSY = db.KB_cangKuSY.Where(s => s.menuID == item.menuID && s.brandName == bandName && s.modelName == modelName && s.status == 1).ToList();
                    if (shebeiSY.Count == 0)
                    {
                        KB_cangKuSY sy = new KB_cangKuSY()
                        {
                            menuID = item.menuID,
                            menuName = item.menuName,
                            ckID = ckID,
                            brandName = bandName,
                            modelName = modelName,
                            number = number[i],
                            status = 1,
                        };
                        db.KB_cangKuSY.Add(sy);
                        db.SaveChanges();
                        syID[i] = sy.syID;
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand(string.Format("update  KB_cangKuSY set number = "+ number[i] + " where syID =" + shebeiSY[0].syID));
                        syID[i] = shebeiSY[0].syID;
                    }
                    i++;
                }
                //数组转字符串
                string str = string.Join(",", syID);
                db.Database.ExecuteSqlCommand(string.Format("update  KB_cangKuSY set status = 99 where syID not in ("+str+")"));//删除多余的部分
            }            
        }
        public string cangkuEdit(string productName, string bandName, string modelName, int ckID, int userID, string userName, string mark, int[] menuID, int[] number)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var cangkuList = db.KB_cangKu.Where(v => v.ckID == ckID).ToList();
                    if (cangkuList.Count == 1)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("update KB_cangKu set productName='{0}',bandName='{1}',modelName='{2}',updateTime='{3}' where ckID='{4}'", productName, bandName, modelName, DateTime.Now, ckID));
                        cangkuSY(bandName, modelName, ckID, menuID, number);//处理适用型号
                    }
                    else
                    {
                        return "修改数据不存在";
                    }                    
                    return "成功";
                }
            }
            catch (Exception ex)
            {
                return "出错啦";
            }
        }
        public string chuku(int ckID, string productName, string bandName, string modelName, int ckNub, string mark, int userID, string userName,int syID) {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var cangkuList = db.KB_cangKu.Where(v => v.ckID == ckID ).ToList();
                    var num = cangkuList[0].ckNub - ckNub;
                    db.Database.ExecuteSqlCommand(string.Format("update KB_cangKu set ckNub='{0}' where ckID='{1}'", num, ckID));
               
                    cangKuOut(productName, bandName, modelName, ckNub, mark, userID, userName, syID);
                    return "成功";
                }
            }
            catch (Exception)
            {

                return "失败";
            }
        }
        public void cangKuOut(string productName, string bandName, string modelName, int ckNub, string mark, int userID, string userName,int syID) {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    DateTime dangTime = Convert.ToDateTime(DateTime.Now.ToString());
                    KB_cangKuOut ckout = new KB_cangKuOut
                    {
                        productName = productName,
                        bandName = bandName,
                        modelName = modelName,
                        outNub = ckNub,
                        outTime = dangTime,
                        userID = userID,
                        userName = userName,
                        mark= mark,
                        syID = syID,
                    };
                    db.KB_cangKuOut.Add(ckout);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string deletecangku(int ckID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    db.Database.ExecuteSqlCommand(string.Format("delete KB_cangKu  where ckID='{0}'", ckID));
                    return "成功";
                }
            }
            catch (Exception)
            {

                return "失败";
            }
        }
        public Array cangkuInList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var Tolist = db.KB_cangKuIn.OrderBy(o => o.ckrID).Skip(PageSize * PageIndex).Take(PageSize);
                    if (productName != "")
                    {
                        Tolist = Tolist.Where(v => v.productName == productName);
                    }
                    if (bandName != "")
                    {
                        Tolist = Tolist.Where(v => v.bandName == bandName);
                    }
                    if (modelName != "")
                    {
                        Tolist = Tolist.Where(v => v.modelName == modelName);
                    }
                    int[] TolistCount = new int[1] { db.KB_cangKuIn.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist.ToArray() };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Array cangkuOutList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    // Tolist = db.KB_cangKuOut.OrderBy(o => o.ckcID).Skip(PageSize * PageIndex).Take(PageSize);
                    var query = (from ot in db.KB_cangKuOut
                                 join sy in db.KB_cangKuSY on ot.syID equals sy.syID
                                 where ot.ckcID>0 && (string.IsNullOrEmpty(productName) || ot.productName == productName)
                                 && (string.IsNullOrEmpty(bandName) || ot.bandName == bandName)
                                 && (string.IsNullOrEmpty(modelName) || ot.modelName == modelName)
                                 select new KB_cangKuOutSY
                                 {
                                     ckcID = ot.ckcID,
                                     productName = ot.productName,
                                     bandName = ot.bandName,
                                     modelName = ot.modelName,
                                     outNub = ot.outNub,
                                     outTime = ot.outTime,
                                     userID = ot.userID,
                                     userName = ot.userName,
                                     mark = ot.mark,
                                     syID = ot.syID,
                                     menuID = sy.menuID,
                                     menuName = sy.menuName,
                                     number = sy.number,
                                 });
                    var Tolist = query.OrderByDescending(o => o.ckcID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    int[] TolistCount = new int[1] { db.KB_cangKuOut.Count() };
                    Array[] arr = new Array[2] { TolistCount, Tolist.ToArray() };
                    return arr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KB_cangKuSY> cangKuSY(int ckID)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    return db.KB_cangKuSY.Where(s => s.ckID == ckID && s.status == 1).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Array KBproList(int PageIndex, int PageSize, int seach_menuID, string seach_type)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string str;
            string seach_where = "";
            
            using (easyYJEntities db = new easyYJEntities())
            {
                var menulist = db.YJ_Menu.Where(o => o.menuID == seach_menuID).ToList();
                
                try
                {
                    var query = (from pro in db.YJ_Product
                                 where pro.status==1
                                 select new YJ_ProductLOG
                                 {
                                     proID = pro.proID,
                                     brand = pro.brand,
                                     model = pro.model,
                                     proName = pro.proName,
                                     lifeValue = pro.lifeValue,
                                     unit = pro.unit,
                                     menuID = pro.menuID,
                                     DValue = pro.DValue,
                                     yujingValue = pro.yujingValue,
                                     proCreateTime = pro.createTime,
                                     status = pro.status,
                                 });
                    var Tolist3 = query.OrderBy(o => o.lifeValue-o.DValue).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    foreach (var item in Tolist3)
                    {
                        int Mid = Convert.ToInt32(item.menuID);
                        string[] menuName2 = new Menu_DAL().GetSysMenu(Mid);
                        string NM = "";
                        if (menuName2.Length > 0)
                        {
                            for(int a= 0; a < menuName2.Length; a++)
                            {
                                NM = NM + "/" + menuName2[a];
                            }
                        }
                        item.menuName = NM;
                        //判断预警是否自然日
                        if (item.unit == "自然日")
                        {
                            DateTime createTime = Convert.ToDateTime(item.proCreateTime);
                            TimeSpan span = currentTime.Subtract(createTime);
                            item.DValue = span.Days + 1;
                        }                        
                    }
                    Array list = Tolist3.ToArray();
                    int[] zCount = new int[1] { query.Count() };
                    Array[] arr = new Array[2] { zCount, list };
                    return arr;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public bool editHuanMo(int dataSaveID,int ChangeNumber)
        {
            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    int reslut = db.Database.ExecuteSqlCommand(string.Format("update  KB_DataSave set ChangeNumber = '{0}' where ID='{1}'", ChangeNumber,dataSaveID));
                    if (reslut > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
