using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    /// <summary>
    /// 月报表统计
    /// </summary>
    public class Admins_Mbbtj
    {
        private int id;
        /// <summary>
        /// 统计ID
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime tj_date;

        /// <summary>
        /// 统计的年份
        /// </summary>
        public DateTime Tj_date
        {
            get { return tj_date; }
            set { tj_date = value; }
        }

        private string tj_YF;

        /// <summary>
        /// 统计月份，季度，全年
        /// </summary>
        public string Tj_YF
        {
            get { return tj_YF; }
            set { tj_YF = value; }
        }

        private string ygzl;
        /// <summary>
        /// 月工作量
        /// </summary>
        public string Ygzl
        {
            get { return ygzl; }
            set { ygzl = value; }
        }

        private string cgqm;
        /// <summary>
        /// 插管全麻
        /// </summary>
        public string Cgqm
        {
            get { return cgqm; }
            set { cgqm = value; }
        }

        private string twxh;
        /// <summary>
        /// 插管全麻中体外循环
        /// </summary>
        public string Twxh
        {
            get { return twxh; }
            set { twxh = value; }
        }

        private string zgnmz;
        /// <summary>
        /// 椎管麻醉
        /// </summary>
        public string Zgnmz
        {
            get { return zgnmz; }
            set { zgnmz = value; }
        }

        private string sjzd;
        /// <summary>
        /// 神经阻滞
        /// </summary>
        public string Sjzd
        {
            get { return sjzd; }
            set { sjzd = value; }
        }

        private string wtrl;
        /// <summary>
        /// 无痛人流
        /// </summary>
        public string Wtrl
        {
            get { return wtrl; }
            set { wtrl = value; }
        }

        private string wtwj;
        /// <summary>
        /// 无痛胃镜
        /// </summary>
        public string Wtwj
        {
            get { return wtwj; }
            set { wtwj = value; }
        }

        private string sjmcc;
        /// <summary>
        /// 深静脉穿刺
        /// </summary>
        public string Sjmcc
        {
            get { return sjmcc; }
            set { sjmcc = value; }
        }

        private string xffs;
        /// <summary>
        /// 心肺复苏
        /// </summary>
        public string Xffs
        {
            get { return xffs; }
            set { xffs = value; }
        }

        private string mzfs;
        /// <summary>
        /// 麻醉复苏
        /// </summary>
        public string Mzfs
        {
            get { return mzfs; }
            set { mzfs = value; }
        }

        private string yzbfz;
        /// <summary>
        /// 严重麻醉并发症
        /// </summary>
        public string Yzbfz
        {
            get { return yzbfz; }
            set { yzbfz = value; }
        }

        private string Steward;
        /// <summary>
        /// Steward>4
        /// </summary>
        public string Steward1
        {
            get { return Steward; }
            set { Steward = value; }
        }

        private string shzt;
        /// <summary>
        /// 术后镇痛
        /// </summary>
        public string Shzt
        {
            get { return shzt; }
            set { shzt = value; }
        }

        private string ssswqj;
        /// <summary>
        /// 手术室外插管抢救
        /// </summary>
        public string Ssswqj
        {
            get { return ssswqj; }
            set { ssswqj = value; }
        }

        private string mzfyq;
        /// <summary>
        /// 麻醉非预期的相关事件
        /// </summary>
        public string Mzfyq
        {
            get { return mzfyq; }
            set { mzfyq = value; }
        }

        private string ysza;
        /// <summary>
        /// 麻醉中发生未预期的意识障碍
        /// </summary>
        public string Ysza
        {
            get { return ysza; }
            set { ysza = value; }
        }

        private string ybhd;
        /// <summary>
        /// 麻醉中出现氧饱和度重度降低例数
        /// </summary>
        public string Ybhd
        {
            get { return ybhd; }
            set { ybhd = value; }
        }

        private string qmjscx;
        /// <summary>
        /// 全身麻醉结束时使用催醒药物例数
        /// </summary>
        public string Qmjscx
        {
            get { return qmjscx; }
            set { qmjscx = value; }
        }

        private string hxdza;
        /// <summary>
        /// 麻醉中因误咽误吸引发呼吸道梗阻例数
        /// </summary>
        public string Hxdza
        {
            get { return hxdza; }
            set { hxdza = value; }
        }

        private string mzywsw;
        /// <summary>
        /// 麻醉意外死亡例数
        /// </summary>
        public string Mzywsw
        {
            get { return mzywsw; }
            set { mzywsw = value; }
        }

        private string fyqxgsj;
        /// <summary>
        /// 其他非预期的相关事件例数
        /// </summary>
        public string Fyqxgsj
        {
            get { return fyqxgsj; }
            set { fyqxgsj = value; }
        }

        private string aSAY1;
        /// <summary>
        /// 
        /// </summary>
        public string ASAY1
        {
            get { return aSAY1; }
            set { aSAY1 = value; }
        }

        private string aSAN1;

        public string ASAN1
        {
            get { return aSAN1; }
            set { aSAN1 = value; }
        }

        private string aSAY2;

        public string ASAY2
        {
            get { return aSAY2; }
            set { aSAY2 = value; }
        }

        private string aSAN2;

        public string ASAN2
        {
            get { return aSAN2; }
            set { aSAN2 = value; }
        }

        private string aSAY3;

        public string ASAY3
        {
            get { return aSAY3; }
            set { aSAY3 = value; }
        }

        private string aSAN3;

        public string ASAN3
        {
            get { return aSAN3; }
            set { aSAN3 = value; }
        }

        private string aSAY4;

        public string ASAY4
        {
            get { return aSAY4; }
            set { aSAY4 = value; }
        }

        private string aSAN4;

        public string ASAN4
        {
            get { return aSAN4; }
            set { aSAN4 = value; }
        }

        private string aSAY5;

        public string ASAY5
        {
            get { return aSAY5; }
            set { aSAY5 = value; }
        }

        private string aSAN5;

        public string ASAN5
        {
            get { return aSAN5; }
            set { aSAN5 = value; }
        }
    }
}
