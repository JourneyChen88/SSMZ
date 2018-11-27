using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class pointF   //要画的点
    {
        private int lx;

        public int Lx
        {
            get { return lx; }
            set { lx = value; }
        }

        private DateTime d; //时间

        public DateTime D
        {
            get { return d; }
            set { d = value; }
        }

        private float v; //值

        public float V
        {
            get { return v; }
            set { v = value; }
        }
    }
}
