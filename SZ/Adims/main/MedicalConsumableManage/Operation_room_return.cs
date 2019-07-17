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

namespace main
{
    public partial class Operation_room_return : Form
    {
        public Operation_room_return()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet room_stock_set = new DataSet();
        DataSet return_set = new DataSet();
        DataSet medicine_set = new DataSet();
        private void button1_Click(object sender, EventArgs e)
        {
            room_checkin checkin = new room_checkin();
            checkin.room_id = int.Parse(roomId_com.Text.Trim());
            checkin.patid = int.Parse(patid_com.Text.Trim());
            checkin.medicine_number = medNub_com.Text;
            checkin.count = int.Parse(count_txt.Text);
            checkin.checkin_time = DateTime.Now;
            checkin.confirm_person = confirmPerson_txt.Text;
            if (0 == bll.Test_room_checkin(checkin))
            { MessageBox.Show("该手术间没有向该病人输出过此类药品", "警告"); return; }
            bll.Update_room_stock(checkin);
            bll.Add_room_checkin(checkin);
            return_set = bll.select_room_checkin();
            dataGridView1.DataSource = return_set.Tables[0];
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView2.DataSource = room_stock_set.Tables[0];
            medNub_com.SelectedIndex = 0;
        }

        private void Operation_room_return_Load(object sender, EventArgs e)
        {
            confirmPerson_txt.Text = Program.Customer.user_name;
            for (int i = 0; i < 10; i++)
                roomId_com.Items.Add(i.ToString());
            roomId_com.SelectedIndex = 0;
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView2.DataSource = room_stock_set.Tables[0];
            return_set = bll.select_room_checkin();
            dataGridView1.DataSource = return_set.Tables[0];
            for (int i = 0; i < room_stock_set.Tables[0].Rows.Count; i++)
                medNub_com.Items.Add(room_stock_set.Tables[0].Rows[i][0].ToString());
            medicine_set = bll.select_medicine_info();
            if (room_stock_set.Tables[0].Rows.Count!=0) medNub_com.SelectedIndex = 0;
            for (int i = 0; i < 20; i++)
                patid_com.Items.Add(i.ToString());
            patid_com.SelectedIndex = 0;
        }

        private void roomId_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView2.DataSource = room_stock_set.Tables[0];
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
