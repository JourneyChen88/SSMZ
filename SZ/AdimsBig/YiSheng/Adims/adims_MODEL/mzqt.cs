﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class mzqt
    {  
        private string qtname; //气体名字

        public string Qtname
        {
            get { return qtname; }
            set { qtname = value; }
        }
        private int id;//ID

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private int bz=0;//标志 0没有，1气体正使用，2气体暂停

        public int Bz
        {
            get { return bz; }
            set { bz = value; }
        }
        public DateTime[] ztjs = new DateTime[10] { new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime()};
         
 

        

 

    }
}
