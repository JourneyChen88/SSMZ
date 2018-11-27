using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
   public class room_output
    {
        private int Id;      //标识
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private int Patid;      //病人id
        public int patid
        {
            get { return Patid; }
            set { Patid = value; }
        }

        private int Room_id;      //手术间号
        public int room_id
        {
            get { return Room_id; }
            set { Room_id = value; }
        }

        private string Medicine_number;      //药品编号
        public string medicine_number
        {
            get { return Medicine_number; }
            set { Medicine_number = value; }
        }

        private int Output_count;      //输出数量
        public int output_count
        {
            get { return Output_count; }
            set { Output_count = value; }
        }

        private string Confirm_person;      //确认人
        public string confirm_person
        {
            get { return Confirm_person; }
            set { Confirm_person = value; }
        }

        private DateTime Output_time;      //输出时间
        public DateTime output_time
        {
            get { return Output_time; }
            set { Output_time = value; }
        }
    }
}
