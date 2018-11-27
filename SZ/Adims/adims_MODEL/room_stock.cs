using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class room_stock
    {
        private int Id;      //标识
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private int Medicine_number;      //药品编号
        public int medicine_number
        {
            get { return Medicine_number; }
            set { Medicine_number = value; }
        }

        private int Roomt_id;      //手术间号
        public int room_ID
        {
            get { return Roomt_id; }
            set { Roomt_id = value; }
        }

        private int Count;      //数量
        public int count
        {
            get { return Count; }
            set { Count = value; }
        }
    }
}
