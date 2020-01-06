using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Adims_User")]
    public class Adims_User
    {

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String Uid { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String Password { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String User_name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.String Position { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Type { get; set; }
    }
}

