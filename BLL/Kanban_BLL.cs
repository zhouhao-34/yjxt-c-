using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    
    public class Kanban_BLL
    {
        Kanban_DAL kanban_DAL = new DAL.Kanban_DAL();
        /// <summary>
        /// 添加型号
        /// </summary>
        /// <param name="ModelName">型号名称</param>
        /// <param name="intervalTime">节拍（秒）</param>
        /// <param name="ChangeTime">换模时间（分钟）</param>
        /// <param name="plcListID">读取PLC设置ID</param>
        /// <param name="PLC_modelID">型号对应PLC内的ID</param>
        /// <returns></returns>
        public string modelAdd(string ModelName, string intervalTime, string ChangeTime,int plcListID, int PLC_modelID)
        {
            return kanban_DAL.modelAdd(ModelName, intervalTime, ChangeTime, plcListID, PLC_modelID);
        }
        /// <summary>
        /// 型号删除
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public bool modelDel(int modelID)
        {
            return kanban_DAL.modelDel(modelID);
        }
        /// <summary>
        /// 型号列表
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array modelList(int PageSize, int PageIndex)
        {
            return kanban_DAL.modelList(PageSize, PageIndex);
        }
        /// <summary>
        /// 查询所有不带分面，给下拉框使用
        /// </summary>
        /// <returns></returns>
        public List<KB_Model> modelList()
        {
            return kanban_DAL.modelList();
        }
        /// <summary>
        /// 型号名称模糊查询
        /// </summary>
        /// <param name="modelName">型号名称</param>
        /// <returns></returns>
        public List<KB_Model> _modelList(string modelName)
        {
            return kanban_DAL._modelList(modelName);
        }
        /// <summary>
        /// 查询指定modelID
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public List<KB_Model> modelList(int modelID)
        {
            return kanban_DAL.modelList(modelID);
        }
        /// <summary>
        /// 修改型号
        /// </summary>
        /// <param name="ModelName">型号名称</param>
        /// <param name="intervalTime">节拍（秒）</param>
        /// <param name="ChangeTime">换模时间（分钟）</param>
        /// <param name="plcListID">读取PLC设置ID</param>
        /// <param name="PLC_modelID">型号对应PLC内的ID</param>
        /// <param name="modelID">型号ID</param>
        /// <returns></returns>
        public string modelEdit(string ModelName, string intervalTime, string ChangeTime, int plcListID, int PLC_modelID,int modelID)
        {
            return kanban_DAL.modelEdit(ModelName, intervalTime, ChangeTime, plcListID, PLC_modelID, modelID);
        }
        /// <summary>
        /// 添加班次
        /// </summary>
        /// <param name="ShiftName">班次名称</param>
        /// <param name="startTime">上班时间</param>
        /// <param name="endTime">下班时间</param>
        /// <returns></returns>
        public string shiftAdd(string ShiftName, string startTime, string endTime)
        {
            return kanban_DAL.shiftAdd(ShiftName, startTime, endTime);
        }
        /// <summary>
        /// 班次列表
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array shiftList(int PageSize, int PageIndex)
        {
            return kanban_DAL.shiftList(PageSize, PageIndex);
        }
        public List<KB_Shift> shiftList()
        {
            return kanban_DAL.shiftList();
        }
        /// <summary>
        /// 修改班次
        /// </summary>
        /// <param name="ShiftName">班次名称</param>
        /// <param name="startTime">上班时间</param>
        /// <param name="endTime">下班时间</param>
        /// <param name="shiftID"></param>
        /// <returns></returns>
        public string shiftEdit(string ShiftName, string startTime, string endTime,int shiftID)
        {
            return kanban_DAL.shiftEdit(ShiftName, startTime, endTime, shiftID);
        }
        /// <summary>
        /// 删除班次
        /// </summary>
        /// <param name="shiftID"></param>
        /// <returns></returns>
        public bool shiftDel(int shiftID)
        {
            return kanban_DAL.shiftDel(shiftID);
        }
        /// <summary>
        /// 添加生产计划
        /// </summary>
        /// <param name="ShiftName">班组</param>
        /// <param name="modelID">生产型号ID</param>
        /// <param name="planNub">计划生产数量</param>
        /// <param name="actualNub">实际生产数量</param>
        /// <param name="pandTime">计划生产日期</param>
        /// <returns></returns>
        public string planAdd(string ShiftName,int modelID,int planNub,int actualNub,string pandTime)
        {
            return kanban_DAL.planAdd(ShiftName, modelID, planNub, actualNub, pandTime);
        }
        /// <summary>
        /// 修改生产计划
        /// </summary>
        /// <param name="ShiftName">班组</param>
        /// <param name="modelID">生产型号ID</param>
        /// <param name="planNub">计划生产数量</param>
        /// <param name="actualNub">实际生产数量</param>
        /// <param name="pandTime">计划生产日期</param>
        /// <param name="planID">计划ID</param>
        /// <returns></returns>
        public string planEdit(string ShiftName, int modelID, int planNub, int actualNub, string pandTime,int planID)
        {
            return kanban_DAL.planEdit(ShiftName, modelID, planNub, actualNub, pandTime, planID);
        }
        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="planID">计划ID</param>
        /// <returns></returns>
        public string planDel(int planID)
        {
            return kanban_DAL.planDel(planID);
        }
        /// <summary>
        /// 计划列表--全部
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array planList(int PageSize,int PageIndex, string startTime, string endTime, string modelName)
        {
            return kanban_DAL.planList(PageSize, PageIndex, startTime, endTime, modelName);
        }
        /// <summary>
        /// 计划列表--当天
        /// </summary>
        /// <returns></returns>
        public List<KB_ProductionPlan> planListD()
        {
            return kanban_DAL.planListD();
        }
        /// <summary>
        /// 每日看板数据查询
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array DataSaveList(int PageSize, int PageIndex, string startTime, string endTime, string modelName)
        {
            return kanban_DAL.DataSaveList(PageSize, PageIndex, startTime, endTime, modelName);
        }
        /// <summary>
        /// 设置看板固定读取PLC
        /// </summary>
        /// <param name="plcID">设置读取PLC的ID</param>
        /// <param name="plc_address">设置读取PLC的地址</param>
        /// <param name="GID">固定修改的ID，1、获取当前生产型号，2、OEE</param>
        /// <param name="addressLenth">读取长度</param>
        /// <returns></returns>
        public string PLC_guding(int plcID,string plc_address,int GID,int addressLenth)
        {
            return kanban_DAL.PLC_guding(plcID, plc_address, GID, addressLenth);
        }
        /// <summary>
        /// 获取所有固定读取PLC列表
        /// </summary>
        /// <returns></returns>
        public List<KB_PLC_guding> PLC_gudingList()
        {
            return kanban_DAL.PLC_gudingList();
        }
        /// <summary>
        /// 读取看板数据
        /// </summary>
        /// <returns></returns>
        public List<KB_DataSave> kanban()
        {
            return kanban_DAL.kanban();
        }
     
        /// <summary>
        /// 添加模具
        /// </summary>
        /// <param name="mojuName">模具名称</param>
        /// <param name="liftNub">寿命</param>
        /// <param name="modelID">关联型号ID数组</param>
        /// <returns></returns>
        public string mojuAdd(string mojuName, int liftNub, int[] modelID)
        {
            return kanban_DAL.mojuAdd(mojuName, liftNub, modelID);
        }
        /// <summary>
        /// 维护模具
        /// </summary>
        /// <param name="mjID">模具表ID</param>
        /// <returns></returns>
        public string mujuWeihu(int mjID)
        {
            return kanban_DAL.mujuWeihu(mjID);
        }
        /// <summary>
        /// 修改模具
        /// </summary>
        /// <param name="mjID">模具表ID</param>
        /// <param name="mojuName">模具名称</param>
        /// <param name="liftNub">寿命</param>
        /// <param name="modelID">关联型号ID数组</param>
        /// <returns></returns>
        public string mojuEdit(int mjID, string mojuName, int liftNub, int[] modelID)
        {
            return kanban_DAL.mojuEdit(mjID, mojuName, liftNub, modelID);
        }
        /// <summary>
        /// 模具列表
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array mojuList(int PageSize, int PageIndex)
        {
            return kanban_DAL.mojuList(PageSize, PageIndex);
        }
        /// <summary>
        /// 根据型号ID查询模具
        /// </summary>
        /// <param name="modelID">型号ID</param>
        /// <returns></returns>
        public List<KB_moju2> mojuListOnly(int[] modelID)
        {
            return kanban_DAL.mojuListOnly(modelID);
        }
        /// <summary>
        /// 根据模具id查询关联型号
        /// </summary>
        /// <param name="mjID">模具ID</param>
        /// <returns></returns>
        public Array modelMujuList(int mjID)
        {
            return kanban_DAL.modelMujuList(mjID);
        }
        /// <summary>
        /// 删除模具
        /// </summary>
        /// <param name="mjID">模具ID</param>
        /// <returns></returns>
        public string deleteMuju(int mjID)
        {
            return kanban_DAL.deleteMuju(mjID);
        }
        /// <summary>
        /// 仓库列表
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array warehouseList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            return kanban_DAL.warehouseList(productName, bandName, modelName, PageSize, PageIndex);
        }
        /// <summary>
        /// 入库操作
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="ckNub">入库数量</param>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="mark">入库备注</param>
        /// <param name="menuID">设备ID=菜单ID</param>
        /// <param name="number">设备使用型号产品的数量</param>
        /// <returns></returns>
        public string cangkuAdd(string productName, string bandName, string modelName, int ckNub, int userID, string userName,string mark,int[] menuID,int[] number)
        {
            return kanban_DAL.cangkuAdd(productName, bandName, modelName, ckNub, userID, userName, mark, menuID, number);
        }
        /// <summary>
        /// 入库操作
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="ckNub">入库数量</param>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="mark">入库备注</param>
        /// <param name="menuID">设备ID=菜单ID</param>
        /// <param name="number">设备使用型号产品的数量</param>
        /// <returns></returns>
        public string addStock(string productName, string bandName, string modelName, int ckNub, int userID, string userName)
        {
            return kanban_DAL.addStock(productName, bandName, modelName, ckNub, userID, userName);
        }
        /// <summary>
        /// 修改库存产品信息，不包括数量
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="ckID">数据库表ID</param>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="mark">入库备注</param>
        /// <param name="menuID">设备ID=菜单ID</param>
        /// <param name="number">设备使用型号产品的数量</param>
        /// <returns></returns>
        public string cangkuEdit(string productName, string bandName, string modelName, int ckID, int userID, string userName, string mark, int[] menuID, int[] number)
        {
            return kanban_DAL.cangkuEdit(productName, bandName, modelName, ckID, userID, userName, mark, menuID, number);
        }
        /// <summary>
        /// 出库操作
        /// </summary>
        /// <param name="ckID">产品ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="ckNub">出库数量</param>
        /// <param name="mark">出库原因</param>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="syID">运用在什么设备上ID</param>
        /// <returns></returns>
        public string chuku(int ckID, string productName, string bandName, string modelName, int ckNub,string mark, int userID, string userName,int syID)
        {
            return kanban_DAL.chuku(ckID, productName, bandName, modelName, ckNub, mark, userID, userName, syID);
        }
        /// <summary>
        /// 出库操作
        /// </summary>
        /// <param name="ckID">产品ID</param>
        /// <returns></returns>
        public string deletecangku(int ckID)
        {
            return kanban_DAL.deletecangku(ckID);
        }
        /// <summary>
        /// 入库列表
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array cangkuInList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            return kanban_DAL.cangkuInList(productName, bandName, modelName, PageSize, PageIndex);
        }
        /// <summary>
        /// 出库列表
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="bandName">品牌</param>
        /// <param name="modelName">型号</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array cangkuOutList(string productName, string bandName, string modelName, int PageSize, int PageIndex)
        {
            return kanban_DAL.cangkuOutList(productName, bandName, modelName, PageSize, PageIndex);
        }
        /// <summary>
        /// 根据仓库ID查询适用设备
        /// </summary>
        /// <param name="ckID"></param>
        /// <returns></returns>
        public List<KB_cangKuSY> cangKuSY(int ckID)
        {
            return kanban_DAL.cangKuSY(ckID);
        }
        /// <summary>
        /// 看板查询预警项
        /// </summary>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public Array KBproList(int PageIndex, int PageSize, int seach_menuID, string seach_type)
        {
            return kanban_DAL.KBproList(PageIndex, PageSize, seach_menuID, seach_type);
        }
        /// <summary>
        /// 修改换模次数
        /// </summary>
        /// <param name="dataSaveID">当前看板的数据ID</param>
        /// <param name="ChangeNumber">换模次数</param>
        /// <returns></returns>
        public bool editHuanMo(int dataSaveID, int ChangeNumber)
        {
            return kanban_DAL.editHuanMo(dataSaveID, ChangeNumber);
        }
    }
}
