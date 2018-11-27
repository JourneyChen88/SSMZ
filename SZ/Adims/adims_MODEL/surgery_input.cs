using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class surgery_input
    {
        private int Id;      //标识
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private int Surgery_id;      //麻醉科号
        public int surgery_id
        {
            get { return Surgery_id; }
            set { Surgery_id = value; }
        }


        private string Medicine_number;      //药品编号
        public string medicine_number
        {
            get { return Medicine_number; }
            set { Medicine_number = value; }
        }

        private int Input_count;      //输入数量
        public int input_count
        {
            get { return Input_count; }
            set { Input_count = value; }
        }

        private string Confirm_person;      //确认人
        public string confirm_person
        {
            get { return Confirm_person; }
            set { Confirm_person = value; }
        }

        private DateTime Input_time;      //输入时间
        public DateTime input_time
        {
            get { return Input_time; }
            set { Input_time = value; }
        }
    }
}
