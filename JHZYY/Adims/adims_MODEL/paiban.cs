using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class paiban //排班
    {
        private string patid;

        /// <summary>
        /// 病人id
        /// </summary>
        public string Patid
        {
            get { return patid; }
            set { patid = value; }
        }

        /// <summary>
        /// 病人名字
        /// </summary>
        private string patname;

        public string Patname
        {
            get { return patname; }
            set { patname = value; }
        }

        private string patsex;
        /// <summary>
        /// 病人性别
        /// </summary>
        public string Patsex
        {
            get { return patsex; }
            set { patsex = value; }
        }

        private int patage;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Patage
        {
            get { return patage; }
            set { patage = value; }
        }

        private string weight;
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private string height;
        /// <summary>
        /// 身高
        /// </summary>
        public string Height
        {
            get { return height; }
            set { height = value; }
        }
        private string zhuyuanNo;
        /// <summary>
        ///住院号
        /// </summary>
        public string ZhuyuanNo
        {
            get { return zhuyuanNo; }
            set { zhuyuanNo = value; }
        }
        private string bloodtype;
        /// <summary>
        ///血型
        /// </summary>
        public string Bloodtype
        {
            get { return bloodtype; }
            set { bloodtype = value; }
        }

        private string bednumber; //床号
        public string Bednumber
        {
            get { return bednumber; }
            set { bednumber = value; }
        }

        private string department;//科室
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        private string TMD; //主要诊断
        public string TMD1
        {
            get { return TMD; }
            set { TMD = value; }
        }

        private string patc; //病情
        public string Patc
        {
            get { return patc; }
            set { patc = value; }
        }

        private string oname; //拟实施手术名称
        public string Oname
        {
            get { return oname; }
            set { oname = value; }
        }

        private string oroom; //手术间
        public string Oroom
        {
            get { return oroom; }
            set { oroom = value; }
        }

        private int second; //台次
        public int Second
        {
            get { return second; }
            set { second = value; }
        }

        private string odepartment;//手术科室
        public string Odepartment
        {
            get { return odepartment; }
            set { odepartment = value; }
        }

        private string olevel;//手术等级
        public string Olevel
        {
            get { return olevel; }
            set { olevel = value; }
        }

        private string amethod; //麻醉方法
        public string Amethod
        {
            get { return amethod; }
            set { amethod = value; }
        }

        private string gl;  //隔离
        public string Gl
        {
            get { return gl; }
            set { gl = value; }
        }

        private string jz; //是否急诊
        public string Jz
        {
            get { return jz; }
            set { jz = value; }
        }

        private string ap1;//麻醉医师1
        public string Ap1
        {
            get { return ap1; }
            set { ap1 = value; }
        }
        private string ap2;//麻醉医师2
        public string Ap2
        {
            get { return ap2; }
            set { ap2 = value; }
        }

        private string ap3;//麻醉医师3
        public string Ap3
        {
            get { return ap3; }
            set { ap3 = value; }
        }

        private string aa1;//麻醉助手1
        public string Aa1
        {
            get { return aa1; }
            set { aa1 = value; }
        }

        private string aa2;//麻醉助手2
        public string Aa2
        {
            get { return aa2; }
            set { aa2 = value; }
        }

        private string aa3;//麻醉助手3
        public string Aa3
        {
            get { return aa3; }
            set { aa3 = value; }
        }

        private string os;//手术医师
        public string Os
        {
            get { return os; }
            set { os = value; }
        }

        private string oa1;//手术助手1
        public string Oa1
        {
            get { return oa1; }
            set { oa1 = value; }
        }
        private string oa2;//手术助手2
        public string Oa2
        {
            get { return oa2; }
            set { oa2 = value; }
        }

        private string oa3;//手术助手3
        public string Oa3
        {
            get { return oa3; }
            set { oa3 = value; }
        }

        private string oa4;//手术助手4
        public string Oa4
        {
            get { return oa4; }
            set { oa4 = value; }
        }

        private string tp;//输血医生
        public string Tp
        {
            get { return tp; }
            set { tp = value; }
        }

        private string on1;//手术护士1
        public string On1
        {
            get { return on1; }
            set { on1 = value; }
        }

        private string on2;//手术护士2
        public string On2
        {
            get { return on2; }
            set { on2 = value; }
        }

        private string sn1;//供应护士1
        public string Sn1
        {
            get { return sn1; }
            set { sn1 = value; }
        }

        private string sn2;//供应护士2
        public string Sn2
        {
            get { return sn2; }
            set { sn2 = value; }
        }

        private string sn3;//供应护士3
        public string Sn3
        {
            get { return sn3; }
            set { sn3 = value; }
        }
        private DateTime odate;//手术日期
        public DateTime Odate
        {
            get { return odate; }
            set { odate = value; }
        }

        private string asa;
        public string Asa
        {
            get { return asa; }
            set { asa = value; }
        }

        private int asae;//E
        public int Asae
        {
            get { return asae; }
            set { asae = value; }
        }
        private int xueya;//血压
        public int Xueya
        {
            get { return xueya; }
            set { xueya = value; }
        }
        private int maibo;//脉搏
        public int Maibo
        {
            get { return maibo; }
            set { maibo = value; }
        }
        private int huxi;//呼吸
        public int Huxi
        {
            get { return huxi; }
            set { huxi = value; }
        }
        private double tiwen;//体温

        public double Tiwen
        {
            get { return tiwen; }
            set { tiwen = value; }
        }

        private string remarks;//备注
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private string ostate;//手术状态
        public string Ostate
        {
            get { return ostate; }
            set { ostate = value; }
        }
    }
}
