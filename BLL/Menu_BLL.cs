using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class Menu_BLL
    {
        Menu_DAL menu_DAL = new Menu_DAL();
        /// <summary>
        /// 根据父级ID+菜单名称查询
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="parentID">父级ID</param>
        /// <returns></returns>
        public List<YJ_Menu> menuOne(string menuName,int parentID)
        {
            try
            {
                return menu_DAL.menuOne(menuName, parentID);
            }
            catch
            {
                throw;
            }
            
        }
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="parentID">父级ID</param>
        /// <param name="parentID">菜单类型：1菜单，2设备</param>
        /// <returns></returns>
        public bool menuADD(string menuName,int parentID, int menuType)
        {
            try
            {
                return menu_DAL.menuADD(menuName, parentID, menuType);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        public List<YJ_Menu> GetSysMenus()
        {

            try
            {

                return menu_DAL.GetSysMenus();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 获取菜单节点包括产品信息
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页记录条数</param>
        /// <returns></returns>
        public Array GetSysMenuChilds(int menuID, int PageIndex, int PageSize)
        {
            try
            {

                return menu_DAL.GetSysMenuChilds(menuID, PageIndex, PageSize);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取当菜单所有父级，不包括产品
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string[] GetSysMenu(int menuID)
        {
            try
            {

                return menu_DAL.GetSysMenu(menuID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 全部设备数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页记录条数</param>
        /// <returns></returns>
        public Array GetSysMenuALL(int PageIndex, int PageSize)
        {
            try
            {

                return menu_DAL.GetSysMenuALL(PageIndex, PageSize);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据菜单ID查询当前菜单
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public List<YJ_Menu> dlist(int menuID)
        {
            try
            {
                return menu_DAL.dlist(menuID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据菜单类型查询
        /// </summary>
        /// <param name="menuType">根据类型查询菜单，menuType=1菜单，menuType=2设备</param>
        /// <returns></returns>
        public List<YJ_Menu> menulistType(int menuType)
        {
            try
            {
                return menu_DAL.menulistType(menuType);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据menuID查询所有子级菜单
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string TreeChilds(int menuID)
        {
            return menu_DAL.TreeChilds(menuID);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        public bool menuEdit(int menuID,int parentID,string menuName,int menuType)
        {
            return menu_DAL.menuEdit(menuID, parentID, menuName, menuType);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string menuDel(int menuID)
        {
            return menu_DAL.menuDel(menuID);
        }
        /// <summary>
        /// 推荐预警值=本产线同型号平均三的值
        /// </summary>
        /// <param name="modelName">型号名称</param>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        public int tuijianYJ(string modelName, int menuID)
        {
            return menu_DAL.tuijianYJ(modelName, menuID);
        }
    }
}
