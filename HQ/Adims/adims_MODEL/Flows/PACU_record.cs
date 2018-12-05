﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class PACU_record        //病人恢复记录
    {
        private int id;      //标识
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string mzjldid;     //病人ID
        public string mzjldID
        {
            get { return mzjldid; }
            set { mzjldid = value; }
        }

        private DateTime Visitdate;       //访视日期
        public DateTime visitDate
        {
            get { return Visitdate; }
            set { Visitdate = value; }
        }

        private string Cautions;         //注意事项
        public string cautions
        {
            get { return Cautions; }
            set { Cautions = value; }
        }

        private float Observetime;       //观察时间
        public float observeTime
        {
            get { return Observetime; }
            set { Observetime = value; }
        }

        private string Concious;        //清醒程度
        public string concious
        {
            get { return Concious; }
            set { Concious = value; }

        }

        private float Liquid;         //总输液量
        public float liquid
        {
            get { return Liquid; }
            set { Liquid = value; }
        }

        private float Urin;         //尿量
        public float urin
        {
            get { return Urin; }
            set { Urin= value; }
        }

        private string Cough;         //是否自主咳嗽
        public string cough
        {
            get { return Cough; }
            set { Cough = value; }
        }

        private string Stimulate;       //对刺激是否有反应
        public string stimulate
        {
            get { return Stimulate; }
            set { Stimulate = value; }
        }

        private string Breathe;      //呼吸通畅程度
        public string breathe
        {
            get { return Breathe; }
            set { Breathe = value; }
        }

        private string Duralplane;     //硬膜平面消退程度
        public string duralPlane
        {
            get { return Duralplane; }
            set { Duralplane = value; }
        }

        private string Limb;         //肢体是否有意识活动
        public string limb
        {
            get { return Limb; }
            set { Limb = value; }
        }

        private string Recorder;     //记录人
        public string recorder
        {
            get { return Recorder; }
            set { Recorder = value; }
        }


        
        
    }
}