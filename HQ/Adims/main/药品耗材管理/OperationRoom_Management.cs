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

namespace main.药品耗材管理
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
       
        private void OperationRoom_Management_Load(object sender, EventArgs e)
        {
             surgeryId_IN_com.SelectedIndex = 0;
             roomId_IN_com.SelectedIndex = 0;
             roomId_OUT_com.SelectedIndex = 0;
             patid_OUT_com.SelectedIndex = 0;
             roomId_CHECK_com.SelectedIndex = 0;
             patid_CHECK_com.SelectedIndex = 0;
             input_set=bll.select_room_input();
             output_set=bll.select_room_output();
             checkin_set = bll.select_room_checkin();
             dataGridView1.DataSource = input_set.Tables[0];
             dataGridView2.DataSource = output_set.Tables[0];
             dataGridView3.DataSource = checkin_set.Tables[0];
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            room_input input = new room_input();
            input.surgery_id = int.Parse(surgeryId_IN_com.Text.Trim());
            input.room_id = int.Parse(roomId_IN_com.Text);
            input.medicine_number = medNub_IN_txt.Text;
            input.input_count = int.Parse(count_IN_txt.Text);
            input.confirm_person = confirmPerson_IN_txt.Text;
            input.input_time = DateTime.Now;
            bll.UpdateAdd_room_stock(input);
            bll.Add_room_input(input);
            input_set = bll.select_room_input();
            dataGridView1.DataSource = input_set.Tables[0];
       }

        private void button2_Click(object sender, EventArgs e)
        {
            room_output output = new room_output();
            output.room_id = int.Parse(roomId_OUT_com.Text.Trim());
            output.medicine_number = medNum_OUT_txt.Text;
            output.output_count = int.Parse(count_OUT_txt.Text.Trim());
            output.patid = int.Parse(patid_OUT_com.Text.Trim());
            output.output_time = DateTime.Now;
            output.confirm_person = confirmPerson_OUT_txt.Text;
            int test = bll.TestSub_room_stock(output);
            if (0 == test) { MessageBox.Show("手术间里没有这种药品","警告"); return; }
            if (1 == test) { MessageBox.Show("手术间里这种药品不足","警告"); return; }
            bll.UpdateSub_room_stock(output);
            bll.Add_room_output(output);
            output_set = bll.select_room_output();
            dataGridView2.DataSource = output_set.Tables[0];
   }

        private void button3_Click(object sender, EventArgs e)
        {
            room_checkin checkin = new room_checkin();
            checkin.room_id = int.Parse(roomId_CHECK_com.Text.Trim());
            checkin.patid = int.Parse(patid_CHECK_com.Text.Trim());
            checkin.medicine_number = medNub_CHECK_txt.Text;
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
    }
}
