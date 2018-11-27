using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_MODEL;
using adims_DAL;

namespace main.MedicalConsumableManage
{
    public partial class OperationRoom_Management : Form
    {
        public OperationRoom_Management()
        {
            InitializeComponent();
        }

        AdimsController bll = new AdimsController();
        DataSet input_set = new DataSet();
        DataSet output_set = new DataSet();
        DataSet checkin_set = new DataSet();
        admin_T_SQL ats = new admin_T_SQL();
        AdimsProvider DAL = new AdimsProvider();
        admin_T_SQL at = new admin_T_SQL();
        private void OperationRoom_Management_Load(object sender, EventArgs e)
        {
            this.confirmPerson_IN_txt.Text = "" + Program.customer.user_name;//谁登陆谁就是用药确认人
            this.confirmPerson_OUT_txt.Text = "" + Program.customer.user_name;//谁登陆谁就是用药确认人
            this.confirm_CHECK_txt.Text = "" + Program.customer.user_name;//谁登陆谁就是用药确认人

            BindcmbOrrom();
            Bindmedicine_number();
            //roomId_IN_com.SelectedIndex = 0;
            //roomId_OUT_com.SelectedIndex = 0;
            //patid_OUT_com.SelectedIndex = 0;
            //roomId_CHECK_com.SelectedIndex = 0;
            patid_CHECK_com.SelectedIndex = 0;
            input_set = bll.select_room_input();
            output_set = bll.select_room_output();
            checkin_set = bll.select_room_checkin();
            dataGridView1.DataSource = input_set.Tables[0];
            dataGridView2.DataSource = output_set.Tables[0];
            dataGridView3.DataSource = checkin_set.Tables[0];
            roomId_IN_com.SelectedIndex = 0;
        }
        //药品编号
        private void Bindmedicine_number()
        {
            this.medNumber_OUT_txt.Items.Clear();
            this.medNumber_CHECK_txt.Items.Clear();
            textBox9.Items.Clear();
            DataTable dt1 = new DataTable();
            dt1 = at.Getmedicine_number();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                this.medNumber_OUT_txt.Items.Add(dt1.Rows[i][0]);
                this.medNumber_CHECK_txt.Items.Add(dt1.Rows[i][0]);
                this.textBox9.Items.Add(dt1.Rows[i][0]);
            }

        }
        //手术室号
        private void BindcmbOrrom()
        {

            this.roomId_IN_com.Items.Clear();
            this.roomId_OUT_com.Items.Clear();
            this.roomId_CHECK_com.Items.Clear();
            comboBox3.Items.Clear();
            DataTable dt1 = new DataTable();
            dt1 = DAL.GetOROOM_all();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string rows = dt1.Rows[i][0].ToString();
                int s = Convert.ToInt32(rows.Substring(2, 1));
                this.roomId_IN_com.Items.Add(s);
                this.roomId_OUT_com.Items.Add(s);
                this.roomId_CHECK_com.Items.Add(s);
                this.comboBox3.Items.Add(s);
            }
            roomId_IN_com.SelectedIndex = 0;
            roomId_OUT_com.SelectedIndex = 0;
            roomId_CHECK_com.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (medNub_IN_txt.Text.Trim() == "")
            {
                MessageBox.Show("药品编号不能为空");
                medNub_IN_txt.Focus();
            }
            else if (medName_IN_txt.Text.Trim() == "")
            {
                MessageBox.Show("药品名称不能为空");
                medName_IN_txt.Focus();
            }
            else if (count_IN_txt.Text.Trim() == "")
            {
                MessageBox.Show("进药数量不能为空");
                count_IN_txt.Focus();
            }
            else if (confirmPerson_IN_txt.Text.Trim() == "")
            {
                MessageBox.Show("确认人不能为空");
                confirmPerson_IN_txt.Focus();
            }
            else
            {
                room_input input = new room_input();
                medicine_info medicine = new medicine_info();
                input.room_id = int.Parse(roomId_IN_com.Text);
                input.medicine_number = medNub_IN_txt.Text;
                input.input_count = int.Parse(count_IN_txt.Text);
                input.confirm_person = confirmPerson_IN_txt.Text;
                input.input_time = DateTime.Now;
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
                bll.UpdateAdd_room_stock(input);
                bll.Add_room_input(input);
                input_set = bll.select_room_input();
                dataGridView1.DataSource = input_set.Tables[0];
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            room_output output = new room_output();
            output.room_id = int.Parse(roomId_OUT_com.Text.Trim());
            output.medicine_number = medNumber_OUT_txt.Text;
            output.output_count = int.Parse(count_OUT_txt.Text.Trim());
            //output.patid = int.Parse(patid_OUT_com.Text.Trim());
            output.output_time = DateTime.Now;
            output.confirm_person = confirmPerson_OUT_txt.Text;
            int test = bll.TestSub_room_stock(output);
            if (0 == test) { MessageBox.Show("手术间里没有这种药品", "警告"); return; }
            if (1 == test) { MessageBox.Show("手术间里这种药品不足", "警告"); return; }
            bll.UpdateSub_room_stock(output);
            bll.Add_room_output(output);
            output_set = bll.select_room_output();
            dataGridView2.DataSource = output_set.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            room_checkin checkin = new room_checkin();
            checkin.room_id = int.Parse(roomId_CHECK_com.Text.Trim());
            //checkin.patid = int.Parse(patid_CHECK_com.Text.Trim());
            checkin.medicine_number = medNumber_CHECK_txt.Text;
            checkin.count = int.Parse(count_CHECK_txt.Text);
            checkin.checkin_time = DateTime.Now;
            checkin.confirm_person = confirm_CHECK_txt.Text;
            if (0 == bll.Test_room_checkin(checkin))
            { MessageBox.Show("该手术间没有向该病人输出过此类药品", "警告"); return; }
            bll.Update_room_stock(checkin);
            bll.Add_room_checkin(checkin);
            checkin_set = bll.select_room_checkin();
            dataGridView3.DataSource = checkin_set.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string sqlwhere = "";
            sqlwhere += " and CONVERT(varchar, input_time , 23 ) between'" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' And  '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' ";
            if (comboBox3.Text != "") sqlwhere += " and adims_room_input.room_id= '" + comboBox3.Text.Trim() + "'";
            if (textBox9.Text != "") sqlwhere += " and Adims_Surgery_Input.medicine_number = '" + textBox9.Text.Trim() + "'";
            if (textBox8.Text != "") sqlwhere += " and medicine_name = '" + textBox8.Text.Trim() + "'";
            if (textBox5.Text != "") sqlwhere += " and confirm_person  = '" + textBox5.Text.Trim() + "'";
            input_set = ats.select_surgery_input(sqlwhere);
            dataGridView4.DataSource = input_set.Tables[0];
            if (input_set.Tables.Count <= 0)
            {
                MessageBox.Show("没有信息");
            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox8.Text = "";
            textBox5.Text = "";
            comboBox3.Text = "";
        }

    }
}
