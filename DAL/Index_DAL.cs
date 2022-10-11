using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class Index_DAL
    {
        ArrayList M = new ArrayList();
        public List<YJ_indexPanel> indexPanel()
        {
            try
            {
                using (easyYJEntities db = new easyYJEntities())
                {
                    List<YJ_childsPanel> child = null;
                    var query = (from menu in db.YJ_Menu
                                 where menu.parentID == 0 && menu.status == 1
                                 select new YJ_indexPanel
                                 {
                                     menuID = menu.menuID,
                                     menuName = menu.menuName,
                                     parentID = menu.parentID,
                                     status = menu.status,
                                     
                                 });
                    var parentMenu = query.ToList();
                    foreach (var item in parentMenu)
                    {
                        item.childs = childList(item.menuID);
                    }
                    return parentMenu;
                }
            }
            catch(Exception EX)
            {
                throw;
            }
        }
        public List<YJ_childsPanel> childList(int parentID)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                //var list = db.YJ_Menu.Where(m => m.parentID == parentID && m.status == 1).ToList();
                var query = (from menu in db.YJ_Menu
                             where menu.parentID == parentID && menu.status == 1
                             select new YJ_childsPanel
                             {
                                 menuID = menu.menuID,
                                 menuName = menu.menuName,
                                 parentID = menu.parentID,
                                 status = menu.status,
                                 proCount = 0,
                                 proyujing = 0,
                                 probaojing = 0,
                             });
                var list = query.ToList();
                foreach (var item in list)
                {
                    //查询当前菜单下设备总数
                    M.Clear();
                    FillTree_children(db.YJ_Menu.Where(o=>o.menuID ==item.menuID).ToList());
                    int proShu = 0;
                    int yujingShu = 0;
                    int baojingShu = 0;
                    for (int m = 0; m < M.Count; m++)
                    {
                        int MID = Convert.ToInt32(M[m].ToString());
                        var proList = db.YJ_Product.Where(p => p.menuID == MID && p.status == 1).ToList();
                        proShu = proList.Count();//计算当前菜单下已添加设备总数
                        yujingShu = proList.Where(p=>p.DValue > p.yujingValue && p.DValue < p.lifeValue).Count();
                        baojingShu = proList.Where(p => p.DValue > p.lifeValue && p.status == 1).Count();
                    }
                    item.proCount = proShu;
                    item.proyujing = yujingShu;
                    item.probaojing = baojingShu;
                }
                return list;
            }                
        }

        private void FillTree_children(List<YJ_Menu> list)
        {
            using (easyYJEntities db = new easyYJEntities())
            {
                int menuID = Convert.ToInt32(list[0].menuID);
                var childs = db.YJ_Menu.Where(o => o.parentID == menuID).ToList();
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
                else
                {
                    M.Add(menuID);
                }
                //int menuID = parentID;
                //var childs = db.YJ_Menu.Where(o => o.parentID == menuID).ToList();
                //if (childs.Count() > 0)
                //{
                //    foreach (var item in childs)
                //    {
                //        if (M.Count == 0)
                //        {
                //            M.Add(item.menuID);
                //        }
                //        else
                //        {
                //            //判断menuID是否存在M数组中
                //            if (!M.Contains(item.menuID))
                //            {
                //                M.Add(item.menuID);
                //            }
                //        }
                //        if (item.parentID > 0)
                //        {
                //            FillTree_children((int)item.parentID);
                //        }
                //    }
                //}
            }
        }
    }
}
