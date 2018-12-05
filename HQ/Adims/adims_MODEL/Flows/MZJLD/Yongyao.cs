using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class Yongyao
    {
        private int id; //用药编号

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int yptype; //用药类型

        public int YpType
        {
            get { return yptype; }
            set { yptype = value; }
        }
        private string name; //用药名

        public string Name
        {
            get { return name; }
            set { name = value; }
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
        private string hxb;

        public string Hxb
        {
            get { return hxb; }
            set { hxb = value; }
        }
        private string quanxue;//全血

        public string Quanxue
        {
            get { return quanxue; }
            set { quanxue = value; }
        }

        private string xuejiang;//血浆

        public string Xuejiang
        {
            get { return xuejiang; }
            set { xuejiang = value; }
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

        public DateTime KsTime
        {
            get { return sysj; }
            set { sysj = value; }
        }

        private DateTime jssj;//结束时间

        public DateTime JsTime
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
        private int y_zb;//控制y的坐标

        public int Y_zb
        {
            get { return y_zb; }
            set { y_zb = value; }
        }
    }
}
