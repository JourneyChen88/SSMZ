using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{

    public class PatOperation
    {
        public int Id { get; set; }
        public int MzjldId { get; set; }

        /// <summary>
        /// 手术名字
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// 手术等级
        /// </summary>
        public string OperLevel { get; set; }

        /// <summary>
        /// 切口类型
        /// </summary>
        public string CutType { get; set; }

        /// <summary>
        /// 手术编码
        /// </summary>
        public string OperCode { get; set; }
        
    }
}
