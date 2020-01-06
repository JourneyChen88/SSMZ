using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL.Dtos
{
   public class PaibanDto
    {
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


        public string Amethod { get; set; }
        public DateTime Odate { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Second { get; set; }
        public string Oroom { get; set; }
    }
}
