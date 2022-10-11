using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Entity;

namespace DAL
{
    public class List_DAL
    {
        easyYJEntities DB = new easyYJEntities();
        ArrayList M = new ArrayList();
        public int menuMaxClass()
        {
            var list = DB.YJ_Menu.Where(o => o.status == 1).OrderByDescending(o=>o.parentID).FirstOrDefault();
            int maxParentID = Convert.ToInt32(list.parentID);//查出最多几级分类
            return maxParentID;
        }
        public List<YJ_ProductXiaohao> listThree(int proID)
        {
            try
            {
                var proList = DB.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                if (proList != null)
                {
                    proID = Convert.ToInt32(proList.proID);
                }
                var query = DB.YJ_ProductXiaohao.Where(o => o.createtTime.Value.Day >= DateTime.Now.Day - 3 && o.createtTime.Value.Month == DateTime.Now.Month && o.createtTime.Value.Year == DateTime.Now.Year && o.proID == proID);             
                var list = query.ToList();
                return list;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool listAdd(Object[] proList, ArrayList weihuzhe)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            int reslut = 0;
            using (TransactionScope trans = new TransactionScope())
            {
                //Object[] obj = new Object[10]{Tex_menu, Tex_barnd, Tex_model, Tex_proName, Tex_life, Tex_unit, Tex_shopDay, Tex_shopUnit, Tex_yujingVal, ImageBox};
                string unit = (string)proList[5];
                YJ_Product yJ_Product = new YJ_Product
                {
                    menuID = Convert.ToInt32(proList[0]),
                    brand = (string)proList[1],
                    model = (string)proList[2],
                    proName = (string)proList[3],
                    lifeValue = Convert.ToInt32(proList[4]),
                    unit = unit,
                    shopTime = Convert.ToInt32(proList[6]),
                    shopTimeType = (string)proList[7],
                    yujingValue = Convert.ToInt32(proList[8]),
                    imgPath = (string)proList[9],
                    createTime = currentTime,
                    DValue = 0,
                    updateTime = currentTime,
                    status=1,
                    updateTimeW = currentTime,
                };
                DB.YJ_Product.Add(yJ_Product);
                //关闭验证
                DB.Configuration.ValidateOnSaveEnabled = false;
                reslut = DB.SaveChanges();
                DB.Configuration.ValidateOnSaveEnabled = true;
                foreach (var i in weihuzhe)
                {
                    YJ_ManageUser user = new YJ_ManageUser
                    {
                        proID = yJ_Product.proID,
                        userID = Convert.ToInt32(i),
                        status = 1,
                    };
                    DB.YJ_ManageUser.Add(user);
                    DB.SaveChanges();
                }
                //回写YJ_PLC_list表proID
                if (unit != "自然日")
                {
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set proID='{0}',status='{1}' where plcListID='{2}'", yJ_Product.proID, 1,proList[10]));
                }                
                trans.Complete();
            }
            if (reslut > 0)
            {
                return true;
            }
            return false;
        }
        public List<YJ_Product> proListFrist(int proID)
        {
            try
            {
                return DB.YJ_Product.Where(p => p.proID == proID).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool listWeibaoAdd(Object[] proList, ArrayList weihuzhe)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            int reslut = 0;
            int proID = Convert.ToInt32(proList[0]);
            var plist = DB.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
            var db = new easyYJEntities();
            var manegeList = db.YJ_Manage.Where(o => o.status == "否" && o.proID == plist.proID).ToList();
            int proLogID = 0;
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (var mm in manegeList)
                {
                    //处理前先备份product表
                    YJ_ProductLog yJ_ProductLog = new YJ_ProductLog
                    {
                        proID = plist.proID,
                        proName = plist.proName,
                        brand = plist.brand,
                        model = plist.model,
                        lifeValue = plist.lifeValue,
                        unit = plist.unit,
                        yujingValue = plist.yujingValue,
                        DValue = plist.DValue,
                        shopTime = plist.shopTime,
                        menuID = plist.menuID,
                        imgPath = plist.imgPath,
                        shopTimeType = plist.shopTimeType,
                        createTime = plist.createTime,
                        updateTime = plist.updateTime,
                        status = plist.status,
                        updateTimeW = plist.updateTimeW,
                        yujing_sendTime = plist.yujing_sendTime,
                        baojing_sendTime = plist.baojing_sendTime,
                        upType = "维保修改",
                        upTime = currentTime,
                        MID = Convert.ToInt32(mm.MID),
                    };
                    DB.YJ_ProductLog.Add(yJ_ProductLog);
                    //关闭验证
                    DB.Configuration.ValidateOnSaveEnabled = false;
                    reslut = DB.SaveChanges();
                    DB.Configuration.ValidateOnSaveEnabled = true;
                    proLogID = yJ_ProductLog.proLogID;
                }
            
                //Object[] obj = new Object[6] { proID, Life, Unit, Yjvalue, TypeCL, Mark };
                foreach (var i in weihuzhe)
                {
                    YJ_ManageChuli chuli = new YJ_ManageChuli
                    {
                        proID = proID,
                        userID = Convert.ToInt32(i),
                        lifeValue = Convert.ToInt32(proList[1]),
                        yujingValue = Convert.ToInt32(proList[3]),
                        unit = proList[2].ToString(),
                        typeCL = "已维保设备",
                        createTime = currentTime,
                        mark = proList[5].ToString(),
                        proLogID = proLogID,
                        MID = Convert.ToInt32(proList[6]),
                    };
                    DB.YJ_ManageChuli.Add(chuli);
                    DB.SaveChanges();
                }
                if (string.CompareOrdinal(proList[4].ToString(), "已维保设备")==0)
                {
                    //修改维保记录表状态
                    //DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Manage set status='{0}'where MID='{1}'", "是", Convert.ToInt32(proList[6])));
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Manage set status='{0}'where proID='{1}'", "是", proID));
                    //修改产品表
                    reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set lifeValue='{0}',unit='{1}',yujingValue='{2}',yujing_sendTime='{3}',baojing_sendTime='{4}',updateTimeW='{5}' where proID='{6}'", proList[1], proList[2], proList[3],null,null, currentTime,proList[0]));
                }
                else
                {
                    return false;
                }
                trans.Complete();
            }
            if (reslut > 0)
            {
                return true;
            }
            return false;
        }
        public bool listWeibaoAdd2(Object[] proList, ArrayList weihuzhe)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            int reslut = 0;
            int proID = Convert.ToInt32(proList[11]);
            var plist = DB.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
            using (TransactionScope trans = new TransactionScope())
            {
                //处理前先备份product表
                YJ_ProductLog yJ_ProductLog = new YJ_ProductLog
                {
                    proID = plist.proID,
                    proName = plist.proName,
                    brand = plist.brand,
                    model = plist.model,
                    lifeValue = plist.lifeValue,
                    unit = plist.unit,
                    yujingValue = plist.yujingValue,
                    DValue = plist.DValue,
                    shopTime = plist.shopTime,
                    menuID = plist.menuID,
                    imgPath = plist.imgPath,
                    shopTimeType = plist.shopTimeType,
                    createTime = plist.createTime,
                    updateTime = plist.updateTime,
                    status = plist.status,
                    updateTimeW = plist.updateTimeW,
                    yujing_sendTime = plist.yujing_sendTime,
                    baojing_sendTime = plist.baojing_sendTime,
                    upType = "维保修改",
                    upTime = currentTime,
                    MID = Convert.ToInt32(proList[13]),
                };
                DB.YJ_ProductLog.Add(yJ_ProductLog);
                //关闭验证
                DB.Configuration.ValidateOnSaveEnabled = false;
                reslut = DB.SaveChanges();
                DB.Configuration.ValidateOnSaveEnabled = true;
                foreach (var i in weihuzhe)
                {
                    YJ_ManageChuli chuli = new YJ_ManageChuli
                    {
                        proID = proID,
                        userID = Convert.ToInt32(i),
                        lifeValue = Convert.ToInt32(proList[4]),
                        yujingValue = Convert.ToInt32(proList[8]),
                        unit = proList[5].ToString(),
                        typeCL = "已更换设备",
                        createTime = currentTime,
                        mark = proList[10].ToString(),
                        proLogID = yJ_ProductLog.proLogID,
                        MID = Convert.ToInt32(proList[13]),
                    };
                    DB.YJ_ManageChuli.Add(chuli);
                    DB.SaveChanges();
                }
                //删除原来的产品记录和读取PLC信息记录
                //Object[] obj = new Object[12] { Tex_menu, Tex_barnd, Tex_model, Tex_proName, Tex_life, Tex_unit, Tex_shopDay, Tex_shopUnit, Tex_yujingVal, ImageBox, Tex_mark, proID };
                reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set status='{0}' where proID='{1}'", 99, proList[11]));
                DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set status='{0}' where proID='{1}'", 99, proList[11]));
                
                YJ_Product yJ_Product = new YJ_Product
                {
                    menuID = Convert.ToInt32(proList[0]),
                    brand = (string)proList[1],
                    model = (string)proList[2],
                    proName = (string)proList[3],
                    lifeValue = Convert.ToInt32(proList[4]),
                    unit = (string)proList[5],
                    shopTime = Convert.ToInt32(proList[6]),
                    shopTimeType = (string)proList[7],
                    yujingValue = Convert.ToInt32(proList[8]),
                    imgPath = (string)proList[9],
                    createTime = currentTime,
                    DValue = 0,
                    updateTime = currentTime,
                    status = 1,
                    updateTimeW = currentTime,
                };
                DB.YJ_Product.Add(yJ_Product);
                //关闭验证
                DB.Configuration.ValidateOnSaveEnabled = false;
                reslut = DB.SaveChanges();
                DB.Configuration.ValidateOnSaveEnabled = true;
                //设置PLC
                DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set proID='{0}',status='{1}' where plcListID='{2}'", yJ_Product.proID,1, proList[12]));
                //继承通知人
                DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_ManageUser set proID='{0}' where proID='{1}' and status='{2}'", yJ_Product.proID, proID,1));
                trans.Complete();
            }
            if (reslut > 0)
            {
                //修改维保记录表状态
                //DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Manage set status='{0}'where MID='{1}'", "是", Convert.ToInt32(proList[13])));
                DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Manage set status='{0}'where proID='{1}'", "是", proList[11]));
                return true;
            }
            return false;
        }
        public Array proLogList(int PageIndex, int PageSize, int seach_menuID, string seach_type)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string str;
            string seach_where = seach_menuID.ToString();
            //查询所有子级菜单ID
            M.Clear();            
            using(easyYJEntities db = new easyYJEntities())
            {
                var menulist = db.YJ_Menu.Where(o => o.menuID == seach_menuID).ToList();
                M.Add(seach_menuID);

                try
                {
                    var query = (from log in db.YJ_Manage
                                 join pro in db.YJ_Product on log.proID equals pro.proID
                                 
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
                                     manageType = log.manageType,
                                     sendStatus = log.sendStatus,
                                     manageCreateTime = log.createTime,
                                     MID = log.MID,
                                     manageStatus = log.status,
                                 }).AsEnumerable();
                    //Where(o => o.manageType == seach_type && M.Contains(o.menuID))
                    var Tolist3 = query.OrderByDescending(n => n.manageCreateTime).OrderBy(o => o.manageStatus).Where(pro => (string.IsNullOrEmpty(seach_type) || pro.manageType == seach_type)).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    if (seach_menuID > 0)
                    {
                        Tolist3 = query.OrderByDescending(n => n.manageCreateTime).OrderBy(o => o.manageStatus).Where(pro => (string.IsNullOrEmpty(seach_type) || pro.manageType == seach_type) && M.Contains(pro.menuID)).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    }
                    
                    foreach (var item in Tolist3)
                    {
                        //如果是维护过的，查询历史记录productLog表
                        if (item.manageStatus == "是")
                        {
                            //var proUplogList = db.YJ_ProductLog.Where(L => L.MID == item.MID).ToList();
                            //if (proUplogList.Count > 0)
                            //{
                                //判断预警是否自然日
                                if (item.unit == "自然日")
                                {
                                    DateTime createTime = Convert.ToDateTime(item.proCreateTime);
                                    TimeSpan span = currentTime.Subtract(createTime);
                                    item.DValue = span.Days + 1;
                                }
                                else
                                {
                                    item.DValue = item.DValue;
                                }
                                //item.proID = (int)proUplogList[0].proID;
                                //item.brand = proUplogList[0].brand;
                                //item.model = proUplogList[0].model;
                                //item.proName = proUplogList[0].proName;
                                //item.lifeValue = proUplogList[0].lifeValue;
                                //item.unit = proUplogList[0].unit;
                                //item.menuID = proUplogList[0].menuID;
                                //item.yujingValue = proUplogList[0].yujingValue;
                                //item.proCreateTime = proUplogList[0].createTime;
                                //item.upTime = proUplogList[0].upTime;
                            //}
                        }
                        else
                        {
                            //判断预警是否自然日
                            if (item.unit == "自然日")
                            {
                                DateTime createTime = Convert.ToDateTime(item.proCreateTime);
                                TimeSpan span = currentTime.Subtract(createTime);
                                item.DValue = span.Days + 1;
                            }
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
        public Array proLogListCL(int PageIndex, int PageSize, int seach_menuID)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string str;
            string seach_where = "";
            //查询所有子级菜单ID
            M.Clear();
            var menulist = DB.YJ_Menu.Where(o => o.menuID == seach_menuID).ToList();
            M.Add(seach_menuID);
            if (seach_menuID > 0)
            {
                FillTree(menulist);
                var query = (from cl in DB.YJ_ManageChuli join  pro in DB.YJ_ProductLog on cl.proLogID equals pro.proLogID
                             where pro.status == 1
                             select new YJ_chuli_manage
                             {
                                 proID = (int)pro.proID,
                                 brand = pro.brand,
                                 model = pro.model,
                                 proName = pro.proName,
                                 lifeValue = pro.lifeValue,
                                 unit = pro.unit,
                                 menuID = pro.menuID,
                                 DValue = pro.DValue,
                                 yujingValue = pro.yujingValue,
                                 userID = (int)cl.userID,
                                 typeCL = cl.typeCL,
                                 createTime = pro.createTime,
                                 createTimeCL = cl.createTime,
                                 mark = cl.mark,
                                 userName = DB.YJ_User.Where(u => u.userID == cl.userID).FirstOrDefault().userName,
                             }).AsEnumerable();
                var Tolist = query.OrderByDescending(o => o.createTime).Where(o => M.Contains(o.menuID)).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                foreach (var item in Tolist)
                {
                    //判断预警是否自然日
                    if (item.unit == "自然日")
                    {
                        DateTime createTime = Convert.ToDateTime(item.createTime);
                        TimeSpan span = currentTime.Subtract(createTime);
                        item.DValue = span.Days + 1;
                    }
                }
                Array list = Tolist.ToArray();
                int[] zCount = new int[1] { query.Count() };
                Array[] arr = new Array[2] { zCount, list };
                return arr;
            }

            try
            {
                var query = (from cl in DB.YJ_ManageChuli
                             join pro in DB.YJ_ProductLog on cl.proLogID equals pro.proLogID
                             where pro.status == 1
                             select new YJ_chuli_manage
                             {
                                 proID = (int)pro.proID,
                                 brand = pro.brand,
                                 model = pro.model,
                                 proName = pro.proName,
                                 lifeValue = pro.lifeValue,
                                 unit = pro.unit,
                                 menuID = pro.menuID,
                                 DValue = pro.DValue,
                                 yujingValue = pro.yujingValue,
                                 userID = (int)cl.userID,
                                 typeCL = cl.typeCL,
                                 createTime = pro.createTime,
                                 createTimeCL = cl.createTime,
                                 mark = cl.mark,
                                 userName = DB.YJ_User.Where(u => u.userID == cl.userID).FirstOrDefault().userName,
                             });
                var Tolist1 = query.OrderByDescending(o => o.createTime).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                foreach (var item in Tolist1)
                {
                    //判断预警是否自然日
                    if (item.unit == "自然日")
                    {
                        DateTime createTime = Convert.ToDateTime(item.createTime);
                        TimeSpan span = currentTime.Subtract(createTime);
                        item.DValue = span.Days + 1;
                    }
                }
                Array list = Tolist1.ToArray();
                int[] zCount = new int[1] { query.Count() };
                Array[] arr = new Array[2] { zCount, list };
                return arr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void FillTree(List<YJ_Menu> list)
        {
            int menuID = Convert.ToInt32(list[0].menuID);
            var childs = DB.YJ_Menu.Where(o => o.parentID == menuID).ToList();
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    if (M.Count == 0)
                    {
                        M.Add(item.menuID);
                    }
                    else
                    {
                        //判断menuID是否存在M数组中
                        if (!M.Contains(item.menuID))
                        {
                            M.Add(item.menuID);
                        }
                    }                    
                    if (item.parentID > 0)
                    {
                        FillTree(childs);
                    }
                }
            }
        }
        public List<YJ_proUser> userList(int proID)
        {
            try
            {
                var query = (from muser in DB.YJ_ManageUser
                             join user in DB.YJ_User on muser.userID equals user.userID
                             where muser.status == 1 && muser.proID == proID
                             select new YJ_proUser
                             {
                                 proID = (int)muser.proID,
                                 userID = user.userID,
                                 userName = user.userName,
                                 mobile = user.mobile,
                                 Email = user.Email,
                                 status = user.status,
                             });
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<YJ_ManageSend> manageSendList(int MID)
        {
            return DB.YJ_ManageSend.Where(s => s.MID == MID).ToList();
        }
        public List<YJ_User> userList_U(int userID)
        {
            return DB.YJ_User.Where(s => s.userID == userID).ToList();
        }
        public bool proDel(int proID)
        {
            try
            {
                int reslut = 0;
                using (TransactionScope trans = new TransactionScope())
                {
                    reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set status='{0}' where proID='{1}'", 99, proID));
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set status='{0}' where proID='{1}'", 99, proID));
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_ManageUser set status='{0}' where proID='{1}'", 99, proID));
                    trans.Complete();
                }
                if (reslut > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool proEdit(Object[] proList, ArrayList weihuzhe)
        {
            try
            {
                System.DateTime currentTime = new System.DateTime();
                currentTime = System.DateTime.Now;
                int reslut = 0;
                int proID = Convert.ToInt32(proList[7]);
                int plcListID = string.IsNullOrEmpty(proList[8].ToString()) ? 0 : Convert.ToInt32(proList[8]);
                int menuID = Convert.ToInt32(proList[0]);
                int shopTime = Convert.ToInt32(proList[4]);
                string barnd = proList[1].ToString();
                using (TransactionScope trans = new TransactionScope())
                {
                    //Object[] obj = new Object[8] { Tex_menu, Tex_barnd, Tex_model, Tex_proName, Tex_shopDay, Tex_shopUnit, ImageBox,proID, plcListID };
                    reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set menuID='{0}',brand='{1}',model='{2}',proName='{3}',shopTime='{4}',shopTimeType='{5}',imgPath='{6}',updateTime='{7}' where proID='{8}'"
                                                                , menuID, barnd, proList[2], proList[3], shopTime, proList[5], proList[6], currentTime,proID));
                    //先删除维护者在重新添加
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_ManageUser set status='{0}' where proID='{1}'", 99, proID));
                    foreach (var i in weihuzhe)
                    {
                        YJ_ManageUser user = new YJ_ManageUser
                        {
                            proID = Convert.ToInt32(proList[7]),
                            userID = Convert.ToInt32(i),
                            status = 1,
                        };
                        DB.YJ_ManageUser.Add(user);
                        DB.SaveChanges();
                    }
                    //先删除除本次添加的plcListID外所有本产品ID的记录
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set status='{0}' where plcListID!='{1}' and proID ='{2}'", 99, plcListID, proID));
                    //回写YJ_PLC_list表proID
                    DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_PLC_list set proID='{0}' where plcListID='{1}'", proID, plcListID));
                    trans.Complete();
                }
                if (reslut > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public List<YJ_ProductList> proEdit_list(int proID)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var query = (from pro in db.YJ_Product
                                 where pro.proID == proID
                                 select new YJ_ProductList
                                 {
                                     proID = pro.proID,
                                     menuID = pro.menuID,
                                     proName = pro.proName,
                                     brand = pro.brand,
                                     model = pro.model,
                                     shopTime = pro.shopTime,
                                     imgPath = pro.imgPath,
                                     shopTimeType = pro.shopTimeType,
                                     plcListID = "",
                                 });
                    var Tolist = query.ToList();
                    foreach (var item in Tolist)
                    {
                        var plcList = db.YJ_PLC_list.Where(u => u.proID == proID && u.status == 1).ToList();
                        if (plcList.Count>0)//判断用户是否上传图片
                        {
                            item.plcListID = plcList[0].plcListID.ToString();
                        }
                    }
                    return Tolist;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int ReadUpdate(List<readPlcList> readVal)
        {
            int result = 0;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    List<YJ_PLC_list> _list = db.YJ_PLC_list.Where(r => r.status == 1 && r.chufa < 6 && r.proID != null).AsNoTracking().ToList();
                    
                    foreach (var rtem in readVal.ToArray())
                    {
                        string sql_field = "";
                        int plcListID = rtem.plclistID;
                        //Console.WriteLine($"plcListID:{plcListID}");
                        var pList = _list.Where(L => L.plcListID == plcListID).ToList();
                        if (pList.Count == 0)
                        {
                            continue;
                        }
                        string _readVal = rtem.val;
                        string val = rtem.val;
                        string shuju = pList[0].shuju_u;//上一次读取的数据
                        int proID = Convert.ToInt32(pList[0].proID);
                        string chuli = pList[0].chuli;

                        if (pList[0].chufa == 1)
                        {
                            //不等于空
                            if (!string.IsNullOrEmpty(_readVal) && _readVal != "0")
                            {
                                sql_field += proUpdate_DValue(pList, _readVal, chuli, plcListID, sql_field);
                            }
                        }
                        else if (pList[0].chufa == 2)
                        {
                            if (pList[0].PLC_addressType == "bool")
                            {
                                //如果布尔型判断等于true还是等于false才能执行
                                if (Convert.ToBoolean(readVal) == true && pList[0].chufa == 4)
                                {
                                    sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                                }
                                else if (Convert.ToBoolean(readVal) == false && pList[0].chufa == 5)
                                {
                                    sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                                }
                            }
                            else if (shuju != _readVal)
                            {
                                sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                            }
                        }
                        else if (pList[0].chufa == 3)//3和2处理数据程序相同
                        {
                            if (pList[0].PLC_addressType == "bool")
                            {
                                //如果布尔型判断等于true还是等于false才能执行
                                if (Convert.ToBoolean(readVal) == true && pList[0].chufa == 4)
                                {
                                    sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                                }
                                else if (Convert.ToBoolean(readVal) == false && pList[0].chufa == 5)
                                {
                                    sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                                }
                            }
                            //else if (shuju != readVal)
                            else if (!string.IsNullOrEmpty(_readVal) || _readVal != "0")
                            {
                                sql_field += proUpdate_DValue2(pList, _readVal, chuli, sql_field);
                            }
                        }
                        else if (pList[0].chufa == 4 || pList[0].chufa == 5)
                        {
                            if (pList[0].PLC_addressType == "bool")
                            {
                                //如果布尔型判断等于true还是等于false才能执行
                                if (Convert.ToBoolean(_readVal) == true && pList[0].chufa == 4)
                                {
                                    sql_field += proUpdate_DValue3(pList, _readVal, chuli, sql_field);
                                }
                                else if (Convert.ToBoolean(_readVal) == false && pList[0].chufa == 5)
                                {
                                    sql_field += proUpdate_DValue3(pList, _readVal, chuli, sql_field);
                                }
                            }
                        }
                        var abc = sql_field;
                        if (!string.IsNullOrEmpty(sql_field))
                        {
                            Console.WriteLine($"sql:{sql_field}");
                            result = db.Database.ExecuteSqlCommand(sql_field);
                            db.SaveChanges();
                        }
                    }
                    

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// byte = byte
        /// int16 = short
        /// Dint32 = int
        /// Uint16 = ushort
        /// UDint32 = uint
        /// real = float
        /// bool = bool
        /// string = string
        public string proUpdate_DValue(List<YJ_PLC_list> plcList, string plcReadValue, string chuli,int plcListID, string sql_field)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int proID = Convert.ToInt32(plcList[0].proID);
                    int xiaohao = 0;
                    var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                    int chuliType = 0;
                    if (plcList[0].PLC_addressType != "string")
                    {
                        if (chuli == "累加")
                        {
                            
                            int DValue = Convert.ToInt32(proList.DValue);
                            int val = 0;
                            //累加方式 chuliType：1读取数据直接累加，2每次累加1
                            if (plcList[0].chuliType == 1)
                            {
                                if (plcList[0].PLC_addressType == "real")
                                {
                                    val = (int)(DValue + Math.Ceiling(float.Parse(plcReadValue)));
                                    xiaohao = (int)Math.Ceiling(float.Parse(plcReadValue));
                                }
                                else
                                {
                                    val = DValue + Convert.ToInt32(plcReadValue);
                                    xiaohao = Convert.ToInt32(plcReadValue);
                                }
                            }
                            else
                            {
                                val = DValue + 1;
                            }                           
                            sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                            
                        }
                        else if (chuli == "替换")
                        {
                            int val = 0;
                            if (plcList[0].PLC_addressType == "real")
                            {
                                val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                            }
                            else
                            {
                                val =  Convert.ToInt32(plcReadValue);
                            }
                            if (val > 0)
                            {
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao = val - Convert.ToInt32(proList.DValue);
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
                                //var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                                int DValue = Convert.ToInt32(proList.DValue);
                                int val = DValue + 1;
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao =1;
                            }
                        }
                    }
                    //记录消耗
                    if (xiaohao > 0)
                    {
                        proXiaohao(proID, plcList[0].plcListID, xiaohao);
                    }
                }                   
            }
            catch(Exception ex)
            {
                return sql_field;
            }
            
            sql_field += string.Format(@"update YJ_PLC_list set shuju_u='{0}',updateTime='{1}' where plcListID='{2}'", plcReadValue, DateTime.Now, plcListID);
            
            return sql_field;
        }
        public string proUpdate_DValue2(List<YJ_PLC_list> plcList, string plcReadValue, string chuli,string sql_field)
        {
            
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int proID = Convert.ToInt32(plcList[0].proID);
                    int xiaohao = 0;
                    var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                    //1、判断数据类型
                    if (plcList[0].PLC_addressType != "string")
                    {
                        if (chuli == "累加")
                        {
                            int DValue = Convert.ToInt32(proList.DValue);
                            int val = 0;
                            //累加方式 chuliType：1读取数据直接累加，2每次累加1
                            if (plcList[0].chuliType == 1)
                            {
                                if (plcList[0].PLC_addressType == "real")
                                {
                                    val = (int)(DValue + Math.Ceiling(float.Parse(plcReadValue)));
                                    xiaohao = (int)Math.Ceiling(float.Parse(plcReadValue));
                                }
                                else
                                {
                                    val = DValue + Convert.ToInt32(plcReadValue);
                                    xiaohao = Convert.ToInt32(plcReadValue);
                                }
                            }
                            else
                            {
                                val = DValue + 1;
                            }                            
                            sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                        }
                        else if (chuli == "替换")
                        {
                            int val = 0;
                            if (plcList[0].PLC_addressType == "real")
                            {
                                val = (int)(Math.Ceiling(float.Parse(plcReadValue)));
                            }
                            else
                            {
                                val = Convert.ToInt32(plcReadValue);
                            }
                            if (val > 0)
                            {
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao = val - Convert.ToInt32(proList.DValue);
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
                                //var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                                int DValue = Convert.ToInt32(proList.DValue);
                                int val = DValue + 1;
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao = 1;
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
                                //var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                                int DValue = Convert.ToInt32(proList.DValue);
                                int val = DValue + 1;
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao = 1;
                            }
                        }
                    }
                    //记录消耗
                    if (xiaohao > 0)
                    {
                        proXiaohao(proID, plcList[0].plcListID, xiaohao);
                    }

                }
            }
            catch(Exception ex)
            {
                return sql_field;
            }
            sql_field += string.Format(@"update YJ_PLC_list set shuju_u='{0}',updateTime='{1}' where plcListID='{2}'", plcReadValue, DateTime.Now, plcList[0].plcListID);
            return sql_field;
        }
        public string proUpdate_DValue3(List<YJ_PLC_list> plcList, string plcReadValue, string chuli, string sql_field)
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    int proID = Convert.ToInt32(plcList[0].proID);
                    int xiaohao = 0;
                    var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
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

                                //var proList = db.YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                                int DValue = Convert.ToInt32(proList.DValue);
                                int val = DValue + 1;
                                sql_field += string.Format(@"update YJ_Product set DValue='{0}',updateTime='{1}' where proID='{2}'", val, DateTime.Now, proID);
                                xiaohao = 1;
                            }
                        }
                    }
                    //记录消耗
                    if (xiaohao > 0)
                    {
                        proXiaohao(proID, plcList[0].plcListID, xiaohao);
                    }
                }
            }
            catch(Exception ex)
            {
                return sql_field;
            }
            sql_field += string.Format(@"update YJ_PLC_list set shuju_u='{0}',updateTime='{1}' where plcListID='{2}'", plcReadValue, DateTime.Now, plcList[0].plcListID);
            return sql_field;
        }
        //写入消耗记录表
        private void proXiaohao(int proID,int prolistID,int xiaohao)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            try {
                using (easyYJEntities db = new easyYJEntities())
                {
                    var proList = db.YJ_Product.Where(p => p.proID == proID).ToList();
                    if (proList.Count == 0)
                    {
                        return;
                    }
                    YJ_ProductXiaohao xh = new YJ_ProductXiaohao
                    {
                        proID = proList[0].proID,
                        plcListID = prolistID,
                        xiaohao = xiaohao,
                        brand = proList[0].brand,
                        model = proList[0].model,
                        createtTime = currentTime,
                    };
                    db.YJ_ProductXiaohao.Add(xh);
                    db.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
