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
    
    public partial class KB_ProductionPlan
    {
        public int planID { get; set; }
        public string Shift { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ModelName { get; set; }
        public Nullable<int> Planproduction { get; set; }
        public Nullable<int> ActualProduction { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }
        public Nullable<System.DateTime> planTime { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
    }
}
