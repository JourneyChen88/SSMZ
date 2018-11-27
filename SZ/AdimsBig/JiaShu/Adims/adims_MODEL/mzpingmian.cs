using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    
    public class mzpingmian //麻醉平面
    {
        private string name;  //名称

        public string mzpmName
        {
            get { return name; }
            set { name = value; }
        }


        private DateTime d; //时间

        public DateTime D
        {
            get { return d; }
            set { d = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
     
}
