using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class UserInfo
    {      
        public int Id { get; set; }       
        public string uid { get; set; }
        public string password { get; set; }        
        public string user_name { get; set; }
        private string Position;
        public string position { get; set; }
        public int type { get; set; }
        
    }
}
