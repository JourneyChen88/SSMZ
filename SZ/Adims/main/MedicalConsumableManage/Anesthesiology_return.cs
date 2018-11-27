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
    public partial class Anesthesiology_return : Form
    {
        public Anesthesiology_return()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet stock_set = new DataSet();
        DataSet return_set = new DataSet();
        DataSet room_stock_set = new DataSet();
        DataSet medicine_set = new DataSet();
        private void Anesthesiology_return_Load(object sender, EventArgs e)
        {
            confirmPerson_txt.Text = Program.customer.user_name;
            for (int i = 0; i < 10; i++)
            roomId_com.Items.Add(i.ToString());
            roomId_com.SelectedIndex = 0;
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            for (int i = 0; i < room_stock_set.Tables[0].Rows.Count; i++)
            medNub_com.Items.Add(room_stock_set.Tables[0].Rows[i][0].ToString());
            dataGridView3.DataSource = room_stock_set.Tables[0];
            stock_set = bll.select_surgery_stock();
            dataGridView1.DataSource = stock_set.Tables[0];
            return_set = bll.select_surgery_checkin();
            dataGridView2.DataSource = return_set.Tables[0];
            medicine_set = bll.select_medicine_info();
            medNub_com.SelectedIndex = 0;
       }

        private void button1_Click(object sender, EventArgs e)
        {
            room_output output = new room_output();
            output.room_id = int.Parse(roomId_com.Text.Trim());
            output.medicine_number = medNub_com.Text;
            output.output_count = int.Parse(count_txt.Text.Trim());
            output.output_time = DateTime.Now;
            output.confirm_person = confirmPerson_txt.Text;
            int test = bll.TestSub_room_stock(output);
            if (0 == test) { MessageBox.Show("手术间里没有这种药品", "警告"); return; }
            if (1 == test) { MessageBox.Show("手术间里这种药品不足", "警告"); return; }
            bll.UpdateSub_room_stock(output);

            surgery_checkin checkin = new surgery_checkin();
            checkin.surgery_id = 0;  //麻醉科编号为0
            checkin.room_id = int.Parse(roomId_com.Text.Trim());
            checkin.medicine_number = medNub_com.Text;
            checkin.count = int.Parse(count_txt.Text);
            checkin.checkin_time = DateTime.Now;
            checkin.confirm_person = confirmPerson_txt.Text;
            if (0 == bll.Test_surgery_checkin(checkin))
            { MessageBox.Show("该手术室没有向该手术间输出过此类药品", "警告"); return; }
            bll.Update_surgery_stock(checkin);
            bll.Add_surgery_checkin(checkin);
            return_set = bll.select_surgery_checkin();
            dataGridView2.DataSource = return_set.Tables[0];
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text.Trim()));
            dataGridView3.DataSource = room_stock_set.Tables[0];
            stock_set = bll.select_surgery_stock();
            dataGridView1.DataSource = stock_set.Tables[0];
        }

        private void roomId_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text.Trim()));
            dataGridView3.DataSource = room_stock_set.Tables[0];
        }

        private void medNub_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medNub_com.Text.Trim() != "")
            {
                DataRow[] medicine = medicine_set.Tables[0].Select("药品编号='" + medNub_com.Text.Trim() + "'");
                medName_txt.Text = medicine[0][1].ToString();
            }
        }
    }
}
