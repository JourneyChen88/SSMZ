using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_MODEL
{
    public class medicine_info
    {
        private int Id;      //标识
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }


        private string Medicine_number;      //药品编号
        public string medicine_number
        {
            get { return Medicine_number; }
            set { Medicine_number = value; }
        }

        private string Medicine_name;      //药品名称
        public string medicine_name
        {
            get { return Medicine_name; }
            set { Medicine_name = value; }
        }

        private string Phonetic_prefix;      //拼音字头
        public string phonetic_prefix
        {
            get { return Phonetic_prefix; }
            set { Phonetic_prefix = value; }
        }

        private string Toxicology;      //毒理
        public string toxicology
        {
            get { return Toxicology; }
            set { Toxicology = value; }
        }

        private string State;      //状态
        public string state
        {
            get { return State; }
            set { State = value; }
        }

        private string Dosagy_form;      //剂型
        public string dosagy_form
        {
            get { return Dosagy_form; }
            set { Dosagy_form = value; }
        }

        private string Specification;      //规格
        public string specification
        {
            get { return Specification; }
            set { Specification = value; }
        }


        private DateTime Produce_time;      //生产日期
        public DateTime produce_time
        {
            get { return Produce_time; }
            set { Produce_time = value; }
        }

        private DateTime Deadline;      //有效期
        public DateTime deadline
        {
            get { return Deadline; }
            set { Deadline = value; }
        }

        private string Batch_number;      //批号
        public string batch_number
        {
            get { return Batch_number; }
            set { Batch_number = value; }
        }

        private string Origin_place;      //产地
        public string origin_place
        {
            get { return Origin_place; }
            set { Origin_place = value; }
        }

    }
}
