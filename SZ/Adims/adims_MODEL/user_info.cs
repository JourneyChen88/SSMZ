﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class user_info
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Uid;
        public string uid
        {
            get { return Uid; }
            set { Uid = value; }
        }
        private string Userno;
        public string userno
        {
            get { return Userno; }
            set { Userno = value; }
        }
        private string Password;
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }

        private string User_name;
        public string user_name
        {
            get { return User_name; }
            set { User_name = value; }
        }

        private string Position;
        public string position
        {
            get { return Position; }
            set { Position = value; }
        }
        private int Type;
        public int type
        {
            get { return Type; }
            set { Type = value; }
        }
        private string YiyuanType;
        public string yiyuanType
        {
            get { return YiyuanType; }
            set { YiyuanType = value; }
        }
        
    }
}
