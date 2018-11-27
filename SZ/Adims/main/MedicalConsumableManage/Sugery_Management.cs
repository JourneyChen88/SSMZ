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
using adims_DAL;
namespace main.MedicalConsumableManage
{
    public partial class Sugery_Management : Form
    {
        public Sugery_Management()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet input_set = new DataSet();
        DataSet output_set = new DataSet();
        DataSet checkin_set = new DataSet();
        admin_T_SQL ats = new admin_T_SQL();
        AdimsProvider DAL = new AdimsProvider();
        private void Sugery_Management_Load(object sender, EventArgs e)
        {
            BindcmbOrrom();
      
            surgeryId_CHECK_com.SelectedIndex = 0;
            roomId_OUT_com.SelectedIndex = 0;
            roomId_CHECK_com.SelectedIndex = 0;
            produceTime_picker.Value = DateTime.Now;
            deadline_picker.Value = DateTime.Now;
            input_set = bll.select_surgery_input();
            output_set = bll.select_surgery_output();
            checkin_set = bll.select_surgery_checkin();
            dataGridView1.DataSource = input_set.Tables[0];
            dataGridView2.DataSource = output_set.Tables[0];
            dataGridView3.DataSource = checkin_set.Tables[0];

        }
        //手术室号
        private void BindcmbOrrom()
        {
            this.surgeryId_IN_com.Items.Clear();
            DataTable dt1 = new DataTable();
            dt1 = DAL.GetOROOM_all();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.surgeryId_IN_com.Items.Add(dt1.Rows[i][0]);
            }
            surgeryId_IN_com.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            surgery_stock stock = new surgery_stock();
            medicine_info medicine = new medicine_info();
            surgery_input input = new surgery_input();
            stock.surgery_id = int.Parse(surgeryId_IN_com.Text.Trim());
            stock.medicine_number = medNub_IN_txt.Text;
            stock.count = int.Parse(count_IN_txt.Text.Trim());
            medicine.medicine_number = medNub_IN_txt.Text;
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
            input.surgery_id = int.Parse(surgeryId_IN_com.Text.Trim());
            input.medicine_number = medNub_IN_txt.Text;
            input.input_count = int.Parse(count_IN_txt.Text);
            input.input_time = DateTime.Now;
            input.confirm_person = confirmPerson_IN_txt.Text;
            bll.Add_surgery_input(input);
            input_set = bll.select_surgery_input();
            dataGridView1.DataSource = input_set.Tables[0];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            surgery_output output = new surgery_output();
            output.surgery_id = int.Parse(surgeryId_OUT_com.Text.Trim());
            output.room_id = int.Parse(roomId_OUT_com.Text.Trim());
            output.medicine_number = medNum_OUT_txt.Text;
            output.output_count = int.Parse(count_OUT_txt.Text.Trim());
            output.confirm_person = confirmPerson_OUT_txt.Text;
            output.output_time = DateTime.Now;
            int test = bll.TestSub_surgery_stock(output);
            if (0 == test)
            { MessageBox.Show("药库中没有这种药", "警告"); return; }
            if (1 == test)
            { MessageBox.Show("药库中这种药的库存不足", "警告"); return; }
            bll.UpdateSub_surgery_stock(output);
            bll.Add_surgery_output(output);
            output_set = bll.select_surgery_output();
            dataGridView2.DataSource = output_set.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            surgery_checkin checkin = new surgery_checkin();
            checkin.surgery_id = int.Parse(surgeryId_CHECK_com.Text.Trim());
            checkin.room_id = int.Parse(roomId_CHECK_com.Text.Trim());
            checkin.medicine_number = medNub_CHECK_txt.Text;
            checkin.count = int.Parse(count_CHECK_txt.Text);
            checkin.checkin_time = DateTime.Now;
            checkin.confirm_person = confirm_CHECK_txt.Text;
            if (0 == bll.Test_surgery_checkin(checkin))
            { MessageBox.Show("该手术室没有向该手术间输出过此类药品", "警告"); return; }
            bll.Update_surgery_stock(checkin);
            bll.Add_surgery_checkin(checkin);
            checkin_set = bll.select_surgery_checkin();
            dataGridView3.DataSource = checkin_set.Tables[0];
        }
    }
}
