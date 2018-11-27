using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class shxjmzxx
    {
        private int mzjldid;

        public int Mzjldid
        {
            get { return mzjldid; }
            set { mzjldid = value; }
        }

        private string ssmc;//手术名称

        public string Ssmc
        {
            get { return ssmc; }
            set { ssmc = value; }
        }

        private string szzd;//术中诊断
        public string Szzd
        {
            get { return szzd; }
            set { szzd = value; }
        }

        private string mzfa;//麻醉方案

        public string Mzfa
        {
            get { return mzfa; }
            set { mzfa = value; }
        }

        private string yssss; //已实施手术

        public string Yssss
        {
            get { return yssss; }
            set { yssss = value; }
        }

        private string jhxm;//监护项目

        public string Jhxm
        {
            get { return jhxm; }
            set { jhxm = value; }
        }

        private string tw;//体位

        public string Tw
        {
            get { return tw; }
            set { tw = value; }
        }

        private string mzxg;//麻醉效果

        public string Mzxg
        {
            get { return mzxg; }
            set { mzxg = value; }
        }
    }
}
