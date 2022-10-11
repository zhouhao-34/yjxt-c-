using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DAL
{
    public class Menu_DAL
    {
        easyYJEntities DB = new easyYJEntities();
        ArrayList M = new ArrayList();
        ArrayList M2 = new ArrayList();
        ArrayList menuIDarry = new ArrayList();
        public List<YJ_Menu> menuOne(string menuName, int parentID)
        {            
            return DB.YJ_Menu.Where(o => o.parentID == parentID && o.menuName == menuName).ToList();
        }

        public bool menuADD(string menuName, int parentID,int menuType)
        {
            try
            {
                YJ_Menu yJ_Menu = new YJ_Menu
                {
                    menuName = menuName,
                    parentID = parentID,
                    status = 1,
                    menuType = menuType,
                };
                DB.YJ_Menu.Add(yJ_Menu);
                int reslut = DB.SaveChanges();
                if (reslut == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public List<YJ_Menu> GetSysMenus()
        {

            try
            {
                using(easyYJEntities db = new easyYJEntities())
                {
                    return db.YJ_Menu.Where(o => o.status == 1).ToList();
                }                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Array GetSysMenuChilds(int menuID, int PageIndex, int PageSize)
        {
            
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            Array list;
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    M.Clear();
                    menuIDarry.Clear();
                    var menulist = db.YJ_Menu.Where(o => o.menuID == menuID).ToList();
                    menuIDarry.Add(menuID);
                    GetSysMenuChilds_FillTree(menulist);
                    menuIDarry.Reverse();
                    M.Reverse();
                    string menuNameCN = menulist[0].menuName;
                    string menuNameCN2 = "";
                    for (int i = 0; i < M.Count; i++)
                    {
                        if (i == 0)
                        {
                            menuNameCN2 = M[i].ToString();
                        }
                        else
                        {
                            menuNameCN2 = menuNameCN2 + "_" + M[i].ToString();
                        }

                    }
                    //arraylist转成list,为where in 准备。linq中的where in = list数组.contains(数据表字段)
                    var filedIN = new List<int>((int[])menuIDarry.ToArray(typeof(int)));
                    menuNameCN = menuNameCN2 + "_" + menuNameCN;
                    //string menuNameALL_CN(menulist);
                    var query = (from menu in db.YJ_Menu
                                 join pro in db.YJ_Product on menu.menuID equals pro.menuID
                                 where filedIN.Contains(menu.menuID) && menu.status == 1 && pro.status == 1
                                 select new YJ_ProductList
                                 {
                                     proID = pro.proID,
                                     menuID = pro.menuID,
                                     brand = pro.brand,
                                     model = pro.model,
                                     proName = pro.proName,
                                     lifeValue = pro.lifeValue,
                                     unit = pro.unit,
                                     DValue = pro.DValue,
                                     yujingValue = pro.yujingValue,
                                     shopTime = pro.shopTime,
                                     shopTimeType = pro.shopTimeType,
                                     createTime = pro.createTime,
                                     imgPath = pro.imgPath,
                                 }).AsEnumerable();
                    var Tolist = query.OrderByDescending(o => o.proID).Skip(PageSize * PageIndex).Take(PageSize).ToList();                    
                    
                    string imgPath = @"../../Resources/Noimg.jpg";
                    foreach (var item in Tolist)
                    {
                        //判断预警是否自然日
                        if (item.unit == "自然日")
                        {
                            DateTime createTime = Convert.ToDateTime(item.createTime);
                            TimeSpan span = currentTime.Subtract(createTime);
                            item.DValue = span.Days + 1;
                            item.meitian = 0;
                        }
                        else
                        {
                            item.meitian = threeSUM(item.proID);
                        }
                        if (!string.IsNullOrEmpty(item.imgPath))//判断用户是否上传图片
                        {
                            imgPath = System.Environment.CurrentDirectory + item.imgPath;
                            try
                            {
                                Bitmap bmp = new Bitmap(imgPath);
                                MemoryStream ms = new MemoryStream();
                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                byte[] imgArr = new byte[ms.Length];
                                ms.Position = 0;
                                ms.Read(imgArr, 0, (int)ms.Length);
                                ms.Close();
                                item.imgPath = Convert.ToBase64String(imgArr);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        
                    }
                    list = Tolist.ToArray();
                    int[] zCount = new int[1] { query.Count() };
                    Array[] arr = new Array[2] { zCount, list };
                    return arr;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void GetSysMenuChilds_FillTree(List<YJ_Menu> list)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                int menuID = Convert.ToInt32(list[0].menuID);
                var childs = db.YJ_Menu.Where(o => o.parentID == menuID).ToList();
                if (childs.Count() > 0)
                {
                    foreach (var item in childs)
                    {
                        menuIDarry.Add(item.menuID);
                        M.Add(item.menuName);
                        if (item.parentID > 0)
                        {
                            GetSysMenuChilds_FillTree(childs);
                        }
                    }
                }
            }
        }
        public string[] GetSysMenu(int menuID)
        {
            try
            {
                M.Clear();
                var menulist = DB.YJ_Menu.Where(o => o.menuID == menuID).ToList();
                FillTree(menulist);
                M.Reverse();
                string[] MenuArr = new string[M.Count+1];
                MenuArr[M.Count] = menulist[0].menuName;
                if (M.Count == 0)
                {
                    return MenuArr;
                }                
                for (int i = 0; i < M.Count; i++)
                {
                    MenuArr[i] = M[i].ToString();
                }
                
                return MenuArr;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Array GetSysMenuALL(int PageIndex, int PageSize)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            Array list;
            try
            {
                using (easyYJEntities db = new easyYJEntities()){
                    //string menuNameALL_CN(menulist);
                    var query = (from menu in db.YJ_Menu
                                 join pro in db.YJ_Product on menu.menuID equals pro.menuID
                                 where pro.status == 1
                                 select new YJ_ProductList
                                 {
                                     proID = pro.proID,
                                     menuID = pro.menuID,
                                     brand = pro.brand,
                                     model = pro.model,
                                     proName = pro.proName,
                                     lifeValue = pro.lifeValue,
                                     unit = pro.unit,
                                     DValue = pro.DValue,
                                     yujingValue = pro.yujingValue,
                                     shopTime = pro.shopTime,
                                     shopTimeType = pro.shopTimeType,
                                     createTime = pro.createTime,
                                     imgPath = pro.imgPath,
                                 }).AsEnumerable();
                    var Tolist = query.OrderByDescending(o => o.proID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    string imgPath = @"../../Resources/Noimg.jpg";
                    foreach (var item in Tolist)
                    {
                        //判断预警是否自然日
                        if(item.unit == "自然日")
                        {
                            DateTime createTime = Convert.ToDateTime(item.createTime);
                            TimeSpan span = currentTime.Subtract(createTime);
                            item.DValue = span.Days + 1;
                            item.meitian = 0;
                        }
                        else
                        {
                            item.meitian = threeSUM(item.proID);
                        }
                        //判断用户是否上传图片
                        if (!string.IsNullOrEmpty(item.imgPath))
                        {
                            imgPath = System.Environment.CurrentDirectory + item.imgPath;
                            try
                            {
                                Bitmap bmp = new Bitmap(imgPath);
                                MemoryStream ms = new MemoryStream();
                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                byte[] imgArr = new byte[ms.Length];
                                ms.Position = 0;
                                ms.Read(imgArr, 0, (int)ms.Length);
                                ms.Close();
                                item.imgPath = Convert.ToBase64String(imgArr);
                            }
                            catch
                            {
                                continue;
                            }
                        }                        
                    }
                    list = Tolist.ToArray();
                    int[] zCount = new int[1] { query.Count() };
                    Array[] arr = new Array[2] { zCount, list };
                    return arr;
                }                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void FillTree( List<YJ_Menu> list)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                int parentId = Convert.ToInt32(list[0].parentID);
                var childs = db.YJ_Menu.Where(o => o.menuID == parentId && o.status==1).ToList();
                //if (parentId == 0)
                //{
                //    M.Add(list[0].menuName);
                //}
                if (childs.Count() > 0)
                {
                    foreach (var item in childs)
                    {

                        M.Add(item.menuName);
                        if (item.parentID > 0)
                        {
                            FillTree(childs);
                        }
                    }
                }
            }                
        }
      
        public DataTable GetUserInfoByOrganization_Id(StringBuilder SqlWhere, IList<SqlParameter> IList_param)
        {
            DataTable dt = new DataTable();
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"SELECT 
                P.proID,
                M.menuName as '产线/设备',
                P.proName as '零部件名称',
                P.DValue as '当前值',
                P.yujingValue as '预警值', 
                P.unit as '单位',
                P.shopTime as '采购周期',
                P.shopTimeType as '采购单位',
                P.lifeValue as '设定寿命',
                P.createTime  as'创建时间',
                from YJ_Product P 
                LEFT JOIN YJ_Menu M 
                ON U.User_ID = S.User_ID where U.DeleteMark !=0");
                    strSql.Append(SqlWhere);
                    strSql.Append(" GROUP BY U.proID");

                    dt = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.Text, strSql.ToString(), IList_param.ToArray()).Tables[0];
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }
        public List<YJ_Menu> dlist(int menuID)
        {
            try
            {
                var list = DB.YJ_Menu.Where(o => o.menuID == menuID && o.status == 1).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<YJ_Menu> menulistType(int menuType)
        {
            try
            {
                var list = DB.YJ_Menu.Where(o => o.menuID == menuType && o.status==1).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //根据menuID查询所有子级菜单
        public string TreeChilds(int menuID)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                var menulist = db.YJ_Menu.Where(o => o.menuID == menuID && o.status == 1).ToList();
                FillTreeChilds(menulist);
                //M.Reverse();
                string menuNameCN = menulist[0].menuName;
                string menuNameCN2 = "";
                for (int i = 0; i < M2.Count; i++)
                {
                    if (i == 0)
                    {
                        menuNameCN2 = M2[i].ToString();
                    }
                    else
                    {
                        menuNameCN2 = menuNameCN2 + "_" + M2[i].ToString();
                    }

                }
                menuNameCN = menuNameCN2 + "_" + menuNameCN;
                return menuNameCN;
            }
            
        }
        private void FillTreeChilds(List<YJ_Menu> list)
        {
            int parentId = Convert.ToInt32(list[0].parentID);
            var childs = DB.YJ_Menu.Where(o => o.menuID == parentId && o.status == 1).ToList();
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    M2.Add(item.menuName);
                    if (item.parentID > 0)
                    {
                        FillTree(childs);
                    }
                }
            }
        }
        public bool menuEdit(int menuID, int parentID, string menuName, int menuType)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            try
            {
                int reslut = 0;
                reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Menu set parentID='{0}',menuName='{1}',updateTime='{2}',menuType='{3}' where menuID='{4}'", parentID, menuName, currentTime, menuType,menuID));
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
        public string menuDel(int menuID)
        {
            //判断菜单及子菜单下是否有产品
            var menuList = DB.YJ_Menu.Where(m => m.menuID == menuID).ToList();
            M.Clear();
            FillTree_children(menuList);
            M.Add(menuID);
            for (int m = 0; m < M.Count; m++)
            {
                int MID = Convert.ToInt32(M[m].ToString());
                var pro = DB.YJ_Product.Where(p => p.menuID == MID && p.status == 1).ToList();
                if (pro.Count > 0)
                {
                    return "请先删除菜单下的设备";                    
                }
            }
            int reslut = 0;
            //开始删除当前菜单及子菜单
            for (int m1 = 0; m1 < M.Count; m1++)
            {
                int MID1 = Convert.ToInt32(M[m1].ToString());
                reslut = DB.Database.ExecuteSqlCommand(string.Format(@"update YJ_Menu set status='{0}' where menuID='{1}'", 99, MID1));
            }
            if (reslut > 0)
            {
                return "成功";
            }
            return "失败";
        }
        //查询菜单子级ID
        private void FillTree_children(List<YJ_Menu> list)
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
                        FillTree_children(childs);
                    }
                }
            }
        }
        //计算每天使用量，取三天的数据求平均
        public int threeSUM(int proID)
        {
            var plcLog = new List_DAL().listThree(proID);
            //读取3天的数据
            int junZhi = 0;
            if (plcLog.Count > 0)
            {
                int val = 0;
                foreach (var item in plcLog)
                {
                    val = val + Convert.ToInt32(item.xiaohao);
                }
                junZhi = val / 3;

            }
            return junZhi;
        }
        public int tuijianYJ(string modelName,int menuID)
        {
            int junZhi = 0;
            var db = new easyYJEntities();
            var prolist = db.YJ_Product.Where(p => p.menuID == menuID && p.model == modelName && p.DValue>0).ToList();
            if (prolist.Count > 0)
            {
                var plcLog = new List_DAL().listThree(prolist[0].proID);
                //读取3天的数据
                if (plcLog.Count > 0)
                {
                    int val = 0;
                    foreach (var item in plcLog)
                    {
                        val = val + Convert.ToInt32(item.xiaohao);
                    }
                    junZhi = val / 3;

                }
            }            
            return junZhi;
        }
    }
}
