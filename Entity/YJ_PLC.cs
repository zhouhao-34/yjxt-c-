//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class YJ_PLC
    {
        public int plcID { get; set; }
        public string PLC_brand { get; set; }
        public string PLC_ip { get; set; }
        public Nullable<int> PLC_port { get; set; }
        public string PLC_name { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<int> status { get; set; }
        public string className { get; set; }
        public string PLC_model { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
    }
}
