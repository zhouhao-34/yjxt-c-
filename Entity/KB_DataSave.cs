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
    
    public partial class KB_DataSave
    {
        public int ID { get; set; }
        public string ShiftName { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> PlanNumber { get; set; }
        public Nullable<int> Single { get; set; }
        public Nullable<int> ChangeNumber { get; set; }
        public string ModelName { get; set; }
        public Nullable<int> Target { get; set; }
        public Nullable<int> Differences { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public Nullable<int> Actual { get; set; }
        public string completion { get; set; }
        public string OEE { get; set; }
    }
}