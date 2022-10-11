using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class YJ_ProductList
    {
        public int proID { get; set; }
        public string parentName { get; set; }
        public string menuName { get; set; }
        public string proName { get; set; }
        public int meitian { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public Nullable<int> lifeValue { get; set; }
        public string unit { get; set; }
        public Nullable<int> yujingValue { get; set; }
        public Nullable<int> DValue { get; set; }
        public Nullable<int> shopTime { get; set; }
        public string shopTimeType { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string imgPath { get; set; }
        public Nullable<int> menuID { get; set; }
        public string plcListID { get; set; }
        public string cangkuWei { get; set; }
        public Nullable<int> kuChun { get; set; }
        
    }
    public partial class YJ_ProductLOG
    {
        public int proID { get; set; }
        public string parentName { get; set; }
        public string menuName { get; set; }
        public string proName { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public Nullable<int> lifeValue { get; set; }
        public string unit { get; set; }
        public Nullable<int> yujingValue { get; set; }
        public Nullable<int> DValue { get; set; }
        public Nullable<int> menuID { get; set; }
        public int MID { get; set; }
        public string manageType { get; set; }
        public string sendStatus { get; set; }
        public Nullable<System.DateTime> proCreateTime { get; set; }
        public string manageStatus { get; set; }
        public Nullable<System.DateTime> manageCreateTime { get; set; }
        public Nullable<System.DateTime> upTime { get; set; }//productLog表字段
        public Nullable<int> status { get; set; }
    }
    public partial class YJ_proUser
    {
        public int proID { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public string mobile { get; set; }
        public string Email { get; set; }
        public Nullable<byte> status { get; set; }
    }
    public partial class YJ_plcList_PLC
    {
        public int plcID { get; set; }
        public string PLC_brand { get; set; }
        public string PLC_ip { get; set; }
        public Nullable<int> PLC_port { get; set; }
        public string PLC_name { get; set; }
        public Nullable<int> PLCstatus { get; set; }
        public string className { get; set; }
        public string PLC_model { get; set; }


        public int plcListID { get; set; }
        public string PLC_adress { get; set; }
        public string PLC_addressType { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> addressLenth { get; set; }
        public Nullable<int> proID { get; set; }
        public Nullable<int> chufa { get; set; }
        public string shuju_u { get; set; }
        public string where_PLC_address { get; set; }
        public string where_tiaojian { get; set; }
        public string where_content { get; set; }
        public string returnVal { get; set; }
        public string chuli { get; set; }
        public Nullable<int> chuliType { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public string where_PLC_addressType { get; set; }
        public Nullable<int> where_returnVal  { get; set; }
    }

    public partial class YJ_chuli_manage
    {
        public int CLID { get; set; }
        public int proID { get; set; }
        public string parentName { get; set; }
        public string menuName { get; set; }
        public string proName { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public Nullable<int> lifeValue { get; set; }
        public string unit { get; set; }
        public Nullable<int> yujingValue { get; set; }
        public Nullable<int> DValue { get; set; }
        public Nullable<int> menuID { get; set; }
        public int MID { get; set; }
        public string manageType { get; set; }
        public string sendStatus { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }

        public int userID { get; set; }
        public string typeCL { get; set; }
        public Nullable<System.DateTime> createTimeCL { get; set; }
        public string mark { get; set; }
        public string userName { get; set; }
    }
    public partial class YJ_indexPanel
    {
        public int menuID { get; set; }
        public string menuName { get; set; }
        public Nullable<int> parentID { get; set; }
        public Nullable<byte> status { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public List<YJ_childsPanel> childs { get; set; }
    }
    public partial class YJ_childsPanel
    {
        public int menuID { get; set; }
        public string menuName { get; set; }
        public Nullable<int> parentID { get; set; }
        public Nullable<byte> status { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public int proCount { get; set; }
        public int proyujing { get; set; }
        public int probaojing { get; set; }
    }

    public partial class Cache_readPLC
    {
        public string ipAddress { get; set; }
        public string address { get; set; }
        public string val { get; set; }
    }
    public partial class readPlcList
    {
        public int plclistID { get; set; }
        public string val { get; set; }
    }
    //PLC重连的数组
    public partial class plcRestartList
    {
        public int plcID { get; set; }
        public int n { get; set; }//重连前的线程号
    }
    public partial class KB_moju2
    {
        public int mjID { get; set; }
        public string mojuName { get; set; }
        public Nullable<int> mojuNub { get; set; }
        public Nullable<int> liftNub { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<System.DateTime> baoyangTime { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
    }
    //出库记录查询
    public partial class KB_cangKuOutSY
    {
        public int ckcID { get; set; }
        public string productName { get; set; }
        public string bandName { get; set; }
        public string modelName { get; set; }
        public Nullable<int> outNub { get; set; }
        public Nullable<System.DateTime> outTime { get; set; }
        public Nullable<int> userID { get; set; }
        public string userName { get; set; }
        public string mark { get; set; }
        public Nullable<int> syID { get; set; }
        public Nullable<int> menuID { get; set; }
        public string menuName { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> number { get; set; }
    }

}
