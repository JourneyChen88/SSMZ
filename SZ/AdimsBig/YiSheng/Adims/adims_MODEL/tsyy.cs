﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class tsyy
    {

        private string name; 

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double id;//ID

        public double Id
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

        private DateTime d;//时间

        public DateTime D
        {
            get { return d; }
            set { d = value; }
        }

        private string yyfs;//用药方式

        public string Yyfs
        {
            get { return yyfs; }
            set { yyfs = value; }
        }
    }
}
