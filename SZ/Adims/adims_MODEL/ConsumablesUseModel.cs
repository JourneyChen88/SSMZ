using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class ConsumablesUseModel
    {
        public ConsumablesUseModel()
        {
            UseTime = DateTime.Now;
        }
        public int Id { get; set; }
        public int MzjldId { get; set; }
        public string PatId { get; set; }

        public string Name { get; set; }
        public int Dosage { get; set; }

        public double Price { get; set; }
        public string Unit { get; set; }
        public int IsCost { get; set; }

        public DateTime UseTime { get; set; }
    }
}
