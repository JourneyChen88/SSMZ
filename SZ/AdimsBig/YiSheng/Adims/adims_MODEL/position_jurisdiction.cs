﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class position_jurisdiction
    {
        private int id;      //标识
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string Position;  //职位
       public string position
       {
           get{ return Position;}
           set{ Position=value;}
       }

       private string Jurisdiction; //权限
       public string jurisdiction
       {
           get { return Jurisdiction; }
           set { Jurisdiction = value; }
       }

    }
}
