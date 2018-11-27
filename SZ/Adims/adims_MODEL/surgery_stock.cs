using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class surgery_stock
    {
        private int Id;      //标识
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private int Surgery_id;      //手术室编号
        public int surgery_id
        {
            get { return Surgery_id; }
            set { Surgery_id = value; }
        }

        private string Medicine_number;      //药品编号
        public string medicine_number
        {
            get { return Medicine_number; }
            set { Medicine_number = value; }
        }

      

        private int Count;      //数量
        public int count
        {
            get { return Count; }
            set { Count = value; }
        }

        


    }
}
