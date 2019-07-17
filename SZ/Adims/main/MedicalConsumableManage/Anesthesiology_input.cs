using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;
using adims_BLL;

namespace main.MedicalConsumableManage
{
    public partial class Anesthesiology_input : Form
    {
        public Anesthesiology_input()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet input_set = new DataSet();
        DataSet stock_set = new DataSet();
        DataSet medicine_info_set = new DataSet();
        private void Anesthesiology_dpt_management_Load(object sender, EventArgs e)
        {
            confirmPerson_IN_txt.Text = Program.Customer.user_name;
            stock_set = bll.select_surgery_stock();
            input_set = bll.select_surgery_input();
            medicine_info_set = bll.select_medicine_info();
            for (int i = 0; i < medicine_info_set.Tables[0].Rows.Count; i++)
            medNub_com.Items.Add(medicine_info_set.Tables[0].Rows[i][0].ToString());
            medNub_com.SelectedIndex = 0;
            dataGridView1.DataSource = stock_set.Tables[0];
            dataGridView2.DataSource = input_set.Tables[0];
       }

        private void button1_Click(object sender, EventArgs e)
        {  
            surgery_stock stock = new surgery_stock();
            medicine_info medicine = new medicine_info();
            surgery_input input = new surgery_input();
            stock.surgery_id = 0; //麻醉科编号为0
            stock.medicine_number = medNub_com.Text;
            if (count_IN_txt.Text.Trim()=="")count_IN_txt.Text = "0";
            stock.count = int.Parse(count_IN_txt.Text.Trim());
            medicine.medicine_number = medNub_com.Text;
            medicine.medicine_name = medName_IN_txt.Text;
            medicine.phonetic_prefix = phoneticPrefix_IN_txt.Text;
            medicine.toxicology = toxicology_IN_txt.Text;
            medicine.state = state_IN_txt.Text;
            medicine.dosagy_form = dosagyForm_IN_txt.Text;
            medicine.specification = specification_IN_txt.Text;
            medicine.produce_time = produceTime_picker.Value;
            medicine.deadline = deadline_picker.Value;
            medicine.batch_number = batchNum_IN_txt.Text;
            medicine.origin_place = originPlace_IN_txt.Text;
            bll.Add_medicine_info(medicine);
            bll.UpdateAdd_surgery_stock(stock);
            input.surgery_id = 0;     //麻醉科编号为0
            input.medicine_number = medNub_com.Text;
            input.input_count = int.Parse(count_IN_txt.Text);
            input.input_time = DateTime.Now;
            input.confirm_person = confirmPerson_IN_txt.Text;
            bll.Add_surgery_input(input);
            input_set = bll.select_surgery_input();
            dataGridView2.DataSource = input_set.Tables[0];
            stock_set = bll.select_surgery_stock();
            dataGridView1.DataSource = stock_set.Tables[0];
            medNub_com.Items.Clear();
            medicine_info_set = bll.select_medicine_info();
            for (int i = 0; i < medicine_info_set.Tables[0].Rows.Count; i++)
                medNub_com.Items.Add(medicine_info_set.Tables[0].Rows[i][0].ToString());
            medNub_com.SelectedIndex = 0;

        }

        private void medNub_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            medicine_info_set = bll.select_medicine_info(); 
            DataTable mytable=new DataTable();
            mytable=medicine_info_set.Tables[0];  //mytable.select
            for (int i = 0; i < mytable.Rows.Count; i++)
            {
                if (mytable.Rows[i][0].ToString() == medNub_com.Text)
                {
                    medName_IN_txt.Text = mytable.Rows[i][1].ToString();
                    phoneticPrefix_IN_txt.Text = mytable.Rows[i][2].ToString();
                    toxicology_IN_txt.Text = mytable.Rows[i][3].ToString();
                    state_IN_txt.Text = mytable.Rows[i][4].ToString();
                    dosagyForm_IN_txt.Text = mytable.Rows[i][5].ToString();
                    specification_IN_txt.Text = mytable.Rows[i][6].ToString();
               if (mytable.Rows[i][7].ToString()!="")    produceTime_picker.Value = (DateTime)mytable.Rows[i][7];
               if (mytable.Rows[i][8].ToString()!= "") deadline_picker.Value = (DateTime)mytable.Rows[i][8];
                    batchNum_IN_txt.Text = mytable.Rows[i][9].ToString();
                    originPlace_IN_txt.Text = mytable.Rows[i][10].ToString();
                }
            }

        }
    }
}
