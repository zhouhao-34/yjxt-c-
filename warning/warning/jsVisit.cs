using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace warning
{
    internal class jsVisit
    {
        Menu_BLL menu_BLL = new Menu_BLL();
        User_BLL user_BLL = new User_BLL();
        List_BLL list_BLL = new List_BLL();
        FmList fm_BLL = new FmList();
        PLC_BLL PLC_BLL = new PLC_BLL();
        Index_BLL index_BLL = new Index_BLL();
        Kanban_BLL kanban_BLL = new Kanban_BLL();
        //启动预警
        public void start() { 
       FmMain fmMain = new FmMain();
         //fmMain.start();
        }
        //首页查询
        public object indexPanel()
        {
            try
            {
                var datalist = index_BLL.indexPanel();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        //查询菜单
        public object queryTreeData()
        {
            try
            {
                var datalist=  menu_BLL.GetSysMenus();
            var obj = new { data = datalist, code="1" };
            return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 新增子菜单
        public object menuAddChildren(string menuName, int parentID, int menuType)
        {
            try
            {
                var datalist = menu_BLL.menuADD(menuName, parentID, menuType);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改菜单
        public object menuEdit(int menuID, int parentID, string menuName, int menuType)
        {
            try
            {
                var datalist = menu_BLL.menuEdit(menuID, parentID, menuName, menuType);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除菜单
        public object menuDel(int menuID)
        {
            try
            {
                var datalist = menu_BLL.menuDel(menuID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 查询设备
        public object queryEquipment(int PageIndex, int PageSize)
        {
            try
            {
                var datalist = menu_BLL.GetSysMenuALL(PageIndex, PageSize);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 根据菜单id查询设备
        public object queryEquipmentCondition(int menuID, int PageIndex, int PageSize)
        {
            try
            {
                var datalist = menu_BLL.GetSysMenuChilds(menuID, PageIndex, PageSize);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 根据proID查询产品信息
        public object proListFrist(int proID)
        {
            try
            {
                var datalist = list_BLL.proListFrist(proID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 新增设备
        public object listAdd(Object[] proList, string weihuzhe)
        {
            try
            {
                ArrayList b = new ArrayList(weihuzhe.Split(','));
                var datalist = list_BLL.listAdd(proList, b);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }  
        // 推荐预警值查询
        public object tuijianYJ(string modelName, int menuID)
        {
            try
            {
                return menu_BLL.tuijianYJ(modelName, menuID); ;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 修改设备
        public object proEdit(Object[] proList, string weihuzhe)
        {
            try
            {
                ArrayList b = new ArrayList(weihuzhe.Split(','));
                var datalist = list_BLL.proEdit(proList, b);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 根据ID查询当前需要修改的数据
        public object proEdit_list(int proID)
        {
            try
            {
                var datalist = list_BLL.proEdit_list(proID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 根据产品ID查询维护人信息
        public object userList(int proID)
        {
            try
            {
                var datalist = list_BLL.userList(proID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        //上传图片
        public string uploadImg() {
            var Text="";
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            Thread t = new Thread((ThreadStart)(() =>
            {
                //选择照片
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;//该值确定是否可以选择多个文件
                dialog.Title = "请选择文件夹"; //窗体标题
                dialog.Filter = "图片文件(*.jpg,*.png)|*.jpg;*.png"; //文件筛选
                                                                 //默认路径设置为我的电脑文件夹
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string file = dialog.FileName;//文件夹路径
                    string path = file.Substring(file.LastIndexOf("\\") + 1); //格式化处理，提取文件名
                                                                              //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;  //使图像拉伸或收缩，以适合PictureBox
                                                                              //this.pictureBox1.ImageLocation = file;

                    //不过这样会有重复,可以给Random一个系统时间做为参数，以此产生随机数，就不会重复了
                    System.Random a = new Random();
                    string RandKey = DateTime.Now.ToString("yyyyMMdd") + a.Next(0, 9999);
                    //判断文件夹是否存
                    DirectoryInfo TheFolder = new DirectoryInfo(Application.StartupPath + "\\Image\\");
                    if (!TheFolder.Exists)
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\Image\\"); //新建文件夹  
                    }
                    File.Copy(dialog.FileName, Application.StartupPath + "\\Image\\" + RandKey + ".jpg");
                    Text = "\\Image\\" + RandKey + ".jpg";
                }
            }
                ));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
          
            return Text;
        }
        // 维保
        public object listWeibaoAdd(Object[] proList, string weihuzhe)
        {
            try
            {
                ArrayList b = new ArrayList(weihuzhe.Split(','));
                var datalist = list_BLL.listWeibaoAdd(proList, b);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 更换
        public object listWeibaoAdd2(Object[] proList, string weihuzhe)
        {
            try
            {
                ArrayList b = new ArrayList(weihuzhe.Split(','));
                var datalist = list_BLL.listWeibaoAdd2(proList, b);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 删除设备
        public object proDel(int proID)
        {
            try
            {
                var datalist = list_BLL.proDel(proID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = ex, code = "2" };
                return obj;
            }
        }
        // 创建PLC读取数据
        public int plc_list_add(string[] dataList)
        {
            return PLC_BLL.PLC_list_add(dataList);
        }
        // 创建PLC读取数据
        public object plc_listList(int plcListID)
        {
            var datalist = PLC_BLL.PLC_listList(plcListID);
            var obj = new { data = datalist, code = "1" };
            return obj;
        }
        // 报警日志
        public object proLogList(int PageSize, int PageIndex, int seach_menuID, string seach_type)
        {
            try
            {
                var datalist = list_BLL.proLogList(PageIndex - 1, PageSize,  seach_menuID, seach_type);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 故障处理日志 维保记录
        public object proLogListCL(int PageSize, int PageIndex, int seach_menuID)
        {
            try
            {
                var datalist = list_BLL.proLogListCL(PageIndex - 1, PageSize,  seach_menuID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 预警日志ID查询通知人及通知信息
        public object manageSendList(int MID)
        {
            try
            {
                return list_BLL.manageSendList(MID);
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 根据userID查询会员信息
        public object userList_U(int MID)
        {
            try
            {
                return list_BLL.userList_U(MID);
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 获取当菜单所有父级，不包括产品
        public object getSysMenu(int menuID)
        {
            try
            {
                return menu_BLL.GetSysMenu(menuID);
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // plc列表查询
        public object plcList()
        {
            try
            {
                var datalist = user_BLL.plcList();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        //添加PLC
        public object plcAdd(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port)
        {
            try
            {
                var datalist = user_BLL.plcAdd(className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        //修改PLC
        public object plcEdit(string className, string PLC_name, string PLC_brand, string PLC_model, string PLC_ip, string PLC_port, int plcID)
        {
            try
            {
                var datalist = user_BLL.plcEdit(className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port, plcID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        public object plcDel( int plcID)
        {
            try
            {
                var datalist = user_BLL.plcDel(plcID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 维护人员列表
        public object queryUserList()
        {
            try
            {
                var datalist = user_BLL.userList();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 登录
        public Array login(string userName, string password)
        {
            try
            {
                return user_BLL.login(userName, password); 
            }
            catch (Exception ex)
            {
                Array[] arr = new Array[0];
                return arr;
            }
        }
        // 添加维护人员
        public object userAdd(string userName, string mobile, string Email, string password, int userType)
        {
            try
            {
                var datalist = user_BLL.userAdd(userName, mobile, Email, password, userType);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改人员
        public object userEdit(string userName, string mobile, string Email, string password, int userID,int userType)
        {
            try
            {
                var datalist = user_BLL.userEdit(userName, mobile, Email, password, userType, userID );
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除
        public object userDel(int userID)
        {
            try
            {
                var datalist = user_BLL.userDel(userID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 型号列表
        public object modelList(int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.modelList(PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        public object modelListTwo(string modelName)
        {
            try
            {
                var datalist = kanban_BLL._modelList( modelName);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 添加型号
        public object modelAdd(string ModelName, string intervalTime, string ChangeTime, int plcListID, int PLC_modelID)
        {
            try
            {
                var datalist = kanban_BLL.modelAdd(ModelName, intervalTime, ChangeTime, plcListID, PLC_modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改型号
        public object modelEdit(string ModelName, string intervalTime, string ChangeTime, int plcListID, int PLC_modelID, int modelID)
        {
            try
            {
                var datalist = kanban_BLL.modelEdit(ModelName, intervalTime, ChangeTime, plcListID, PLC_modelID, modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除型号
        public object modelDel(int modelID)
        {
            try
            {
                var datalist = kanban_BLL.modelDel(modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 班次列表
        public object shiftList(int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.shiftList(PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        public object shiftListTwo()
        {
            try
            {
                var datalist = kanban_BLL.shiftList();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 添加班次
        public object shiftAdd(string ShiftName, string startTime, string endTime)
        {
            try
            {
                var datalist = kanban_BLL.shiftAdd(ShiftName, startTime, endTime);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改班次
        public object shiftEdit(string ShiftName, string startTime, string endTime, int shiftID)
        {
            try
            {
                var datalist = kanban_BLL.shiftEdit(ShiftName, startTime, endTime, shiftID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除班次
        public object shiftDel(int shiftID)
        {
            try
            {
                var datalist = kanban_BLL.shiftDel(shiftID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 计划列表
        public object planList(int PageSize, int PageIndex, string startTime, string endTime, string modelName)
        {
            try
            {
                var datalist = kanban_BLL.planList(PageSize, PageIndex, startTime, endTime, modelName);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 添加计划
        public object planAdd(string ShiftName, int modelID, int planNub, int actualNub, string pandTime)
        {
            try
            {
                var datalist = kanban_BLL.planAdd(ShiftName, modelID, planNub, actualNub, pandTime);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改计划
        public object planEdit(string ShiftName, int modelID, int planNub, int actualNub, string pandTime, int planID)
        {
            try
            {
                var datalist = kanban_BLL.planEdit(ShiftName, modelID, planNub, actualNub, pandTime, planID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除计划
        public object planDel(int planID)
        {
            try
            {
                var datalist = kanban_BLL.planDel(planID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 看板历史数据
        public object dataSaveList(int PageSize, int PageIndex, string startTime, string endTime, string modelName)
        {
            try
            {
                var datalist = kanban_BLL.DataSaveList(PageSize, PageIndex, startTime, endTime, modelName);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 获取所有固定读取PLC列表
        public object plc_gudingList()
        {
            try
            {
                var datalist = kanban_BLL.PLC_gudingList();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 设置看板固定读取PLC
        public object plc_guding(int plcID, string plc_address, int GID, int addressLenth)
        {
            try
            {
                var datalist = kanban_BLL.PLC_guding( plcID,plc_address,GID,addressLenth);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 看板下方列表数据
        public object planListD()
        {
            try
            {
                var datalist = kanban_BLL.planListD();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 读取看板数据
        public object kanban()
        {
            try
            {
                var datalist = kanban_BLL.kanban();
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 仓库列表
        public object warehouseList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.warehouseList(productName, bandName, modelName,PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 入库
        public object cangkuAdd(string productName, string bandName, string modelName, int ckNub, int userID, string userName, string mark, int[] menuID, int[] number)
        {
            try
            {
                var datalist = kanban_BLL.cangkuAdd(productName, bandName, modelName, ckNub, userID, userName, mark, menuID, number);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 增加库存
        public object addStock(string productName, string bandName, string modelName, int ckNub, int userID, string userName)
        {
            try
            {
                var datalist = kanban_BLL.addStock(productName, bandName, modelName, ckNub, userID, userName);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 出库
        public object chuku(int ckID, string productName, string bandName, string modelName, int ckNub, string mark, int userID, string userName, int syID)
        {
            try
            {
                var datalist = kanban_BLL.chuku(ckID, productName, bandName, modelName, ckNub, mark, userID, userName, syID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        //根据仓库ID查询适用设备
        public object cangKuSY(int ckID)
        {
            try
            {
                var datalist = kanban_BLL.cangKuSY(ckID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改库存
        public object cangkuEdit(string productName, string bandName, string modelName, int ckID, int userID, string userName, string mark, int[] menuID, int[] number)
        {
            try
            {
                var datalist = kanban_BLL.cangkuEdit(productName, bandName, modelName, ckID, userID, userName, mark, menuID, number);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除
        public object deletecangku(int ckID)
        {
            try
            {
                var datalist = kanban_BLL.deletecangku(ckID );
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 入库列表
        public object cangkuInList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.cangkuInList(productName, bandName, modelName, PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 出库列表
        public object cangkuOutList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.cangkuOutList(productName, bandName, modelName, PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 根据型号ID查询模具
        public object mojuListOnly(int[] modelID)
        {
            try
            {
                var datalist = kanban_BLL.mojuListOnly( modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 模具列表
        public object mojuList( int PageSize, int PageIndex)
        {
            try
            {
                var datalist = kanban_BLL.mojuList( PageSize, PageIndex);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 添加模具
        public object mojuAdd(string mojuName, int liftNub, int[] modelID)
        {
            try
            {
                var datalist = kanban_BLL.mojuAdd(mojuName, liftNub, modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 维护模具
        public object mujuWeihu(int mjID)
        {
            try
            {
                var datalist = kanban_BLL.mujuWeihu(mjID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 查询型号
        public object modelMujuList( int mjID )
        {
            try
            {
                var datalist = kanban_BLL.modelMujuList(mjID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改模具
        public object mojuEdit(int mjID, string mojuName, int liftNub, int[] modelID)
        {
            try
            {
                var datalist = kanban_BLL.mojuEdit(mjID, mojuName, liftNub, modelID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 删除模具
        public object deleteMuju(int mjID)
        {
            try
            {
                var datalist = kanban_BLL.deleteMuju(mjID);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 看板查询预警项
        public object kbproList(int PageIndex, int PageSize, int seach_menuID, string seach_type)
        {
            try
            {
                var datalist = kanban_BLL.KBproList(PageIndex, PageSize, seach_menuID, seach_type);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
        // 修改换模次数
        public object editHuanMo(int dataSaveID, int ChangeNumber)
        {
            try
            {
                var datalist = kanban_BLL.editHuanMo(dataSaveID, ChangeNumber);
                var obj = new { data = datalist, code = "1" };
                return obj;
            }
            catch (Exception ex)
            {
                var obj = new { data = "出现错误", code = "2" };
                return obj;
            }
        }
    }
}
