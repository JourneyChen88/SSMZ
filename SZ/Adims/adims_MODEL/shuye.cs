﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class shuye  //输液
    {

        private int id;//名字

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;//名字

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double jl;//剂量

        public double Jl
        {
            get { return jl; }
            set { jl = value; }
        }

        private string dw;//单位

        public string Dw
        {
            get { return dw; }
            set { dw = value; }
        }

        private string zrfs;//注入方式

        public string Zrfs
        {
            get { return zrfs; }
            set { zrfs = value; }
        }

        private DateTime kssj;

        public DateTime Kssj
        {
            get { return kssj; }
            set { kssj = value; }
        }

        private DateTime jssj;

        public DateTime Jssj
        {
            get { return jssj; }
            set { jssj = value; }
        }

        private int bz;

        public int Bz
        {
            get { return bz; }
            set { bz = value; }
        }


    }
}
