using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class SysLog
    {
        /// <summary>
        /// 是否手动添加
        /// </summary>
        public int IsManualAdd { get; set; }
        public Guid LogID { get; set; }
        public int LogType { get; set; }
        public string Content { get; set; }
        public DateTime LogTime { get; set; }
        public Guid Operator { get; set; }
        public Guid KeyID1 { get; set; }
        public Guid KeyID2 { get; set; }
        public Guid FlowOrgID { get; set; }
        public int LogLevel { get; set; }
        public Guid? KeyID3 { get; set; }
        public Guid? CheckID1 { get; set; }

        public string Barcode { get; set; }

    }
}
