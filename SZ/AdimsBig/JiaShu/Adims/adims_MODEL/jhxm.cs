using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class jhxm
    {
        private DateTime d; //time

        public DateTime D
        {
            get { return d; }
            set { d = value; }
        }

        private int v;  //value

        public int V
        {
            get { return v; }
            set { v = value; }
        }

        private string sy; //属于哪个监护项目

        public string Sy
        {
            get { return sy; }
            set { sy = value; }
        }


    }
}
