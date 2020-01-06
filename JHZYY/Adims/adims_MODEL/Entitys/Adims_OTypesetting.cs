using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    [SugarTable("Adims_OTypesetting")]
    public class OTypesetting
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        public string PatID { get; set; }
        public string PatZhuYuanID { get; set; }
        public string CardID { get; set; }
        public string Patname { get; set; }
        public string Patsex { get; set; }
        public string Patage { get; set; }
        public string PatNation { get; set; }
        public string Patbedno { get; set; }
        public string Patdpm { get; set; }
        public string Pattmd { get; set; }
        public string Oname { get; set; }

        public string OperNo { get; set; }

        public string Amethod { get; set; }

        public DateTime ApplyDate { get; set; }
        public string Tiwei { get; set; }
        public string Second { get; set; }
        public string Oroom { get; set; }
        public string GR { get; set; }
        public string BX { get; set; }
        public string AP1 { get; set; }
        public string AP2 { get; set; }
        public string AP3 { get; set; }
        public string OS { get; set; }
        public string OsNo { get; set; }

        public string OS1 { get; set; }
        public string OS2 { get; set; }
        public string OS3 { get; set; }
        public string OS4 { get; set; }
        public string ON1 { get; set; }
        public string ON2 { get; set; }
        public string SN1 { get; set; }
        public string SN2 { get; set; }
        public string Remarks { get; set; }
        public int Ostate { get; set; }
        public String StartTime { get; set; }
        public string Olevel { get; set; }
        public string isjizhen { get; set; }
        public string PatHeight { get; set; }
        public string PatWeight { get; set; }
        public string PatBloodType { get; set; }
        public string asa { get; set; }
        public DateTime Odate { get; set; }
        public int asae { get; set; }
        public string xueya { get; set; }
        public string maibo { get; set; }
        public string huxi { get; set; }
        public string tiwen { get; set; }
        public string Ocode { get; set; }
        public string CFSS { get; set; }
        public string SSLB { get; set; }
        public string QKDJ { get; set; }
        public int isQXSM { get; set; }
        public string sqys { get; set; }
        public string zrys { get; set; }
        public string osdm { get; set; }
        public string qxbs { get; set; }
        public string expertName { get; set; }
        public string zycs { get; set; }
        public string SSDJ { get; set; }
        public string yiliao { get; set; }
        public string SFZH { get; set; }
        public string chexiao { get; set; }
        public string BRSYH { get; set; }
        public string ZYSJ { get; set; }

        public string PidInfo { get; set; }

        public string Pv1Info { get; set; }




    }
}

