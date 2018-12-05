using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class clcxqt //岀尿出血引流量
    {
        private int lx;  //1是出尿2是出血3是引流量

        public int Lx
        {
            get { return lx; }
            set { lx = value; }
        }

        private int v;  //出量

        public int V
        {
            get { return v; }
            set { v = value; }
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
