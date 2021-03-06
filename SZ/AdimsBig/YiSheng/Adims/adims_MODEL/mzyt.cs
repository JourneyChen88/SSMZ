﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class mzyt
    {
        private int id; //诱导药，吸入式麻醉药

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ytname; //诱导药，吸入式麻醉药

        public string Ytname
        {
            get { return ytname; }
            set { ytname = value; }
        }

        private double yl;//用量

        public double Yl
        {
            get { return yl; }
            set { yl = value; }
        }

        private string dw;//单位

        public string Dw
        {
            get { return dw; }
            set { dw = value; }
        }
        private string yyfs;//用药方式

        public string Yyfs
        {
            get { return yyfs; }
            set { yyfs = value; }
        }

        private bool cxyy;//持续用药

        public bool Cxyy
        {
            get { return cxyy; }
            set { cxyy = value; }
        }



        private DateTime sysj;// 使用时间

        public DateTime Sysj
        {
            get { return sysj; }
            set { sysj = value; }
        }

        private DateTime jssj;//结束时间

        public DateTime Jssj
        {
            get { return jssj; }
            set { jssj = value; }
        }

        private int bz = 0;//标志 0没有，1正使用，2暂停

        public int Bz
        {
            get { return bz; }
            set { bz = value; }
        }
    }
}
