using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class mzks
    {
        private int lx;//1麻醉开始 2麻醉结束 3手术开始 4手术结束 5插管 6拔管

        public int Lx
        {
            get { return lx; }
            set { lx = value; }
        }

        private DateTime d;

        public DateTime D
        {
            get { return d; }
            set { d = value; }
        }

    }
}
