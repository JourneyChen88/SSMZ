﻿using System;
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
    public partial class Operation_room__output : Form
    {
        public Operation_room__output()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet output_set = new DataSet();
        DataSet room_stock_set = new DataSet();
        DataSet medicine_set = new DataSet();
        private void Operation_room__output_Load(object sender, EventArgs e)
        {

            
            confirmPerson_txt.Text = Program.Customer.user_name;
            for (int i = 0; i < 10; i++)
            roomId_com.Items.Add(i.ToString());
            roomId_com.SelectedIndex = 0;
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView1.DataSource = room_stock_set.Tables[0];
            output_set = bll.select_room_output();
            dataGridView2.DataSource = output_set.Tables[0];
            for (int i = 0; i < room_stock_set.Tables[0].Rows.Count; i++)
                medNub_com.Items.Add(room_stock_set.Tables[0].Rows[i][0].ToString());
            medicine_set = bll.select_medicine_info();
             medNub_com.SelectedIndex = 0;
             for (int i = 0; i < 20; i++)
                 patid_com.Items.Add(i.ToString());
             patid_com.SelectedIndex = 0;
            
        }
      


        private void button1_Click(object sender, EventArgs e)
        {
            room_output output = new room_output();
            output.room_id = int.Parse(roomId_com.Text.Trim());
            output.medicine_number = medNub_com.Text;
            output.output_count = int.Parse(count_txt.Text.Trim());
            output.patid = int.Parse(patid_com.Text.Trim());
            output.output_time = DateTime.Now;
            output.confirm_person = confirmPerson_txt.Text;
            int test = bll.TestSub_room_stock(output);
            if (0 == test) { MessageBox.Show("手术间里没有这种药品", "警告"); return; }
            if (1 == test) { MessageBox.Show("手术间里这种药品不足", "警告"); return; }
            int flag = 0;
            flag = bll.Add_room_output(output);
            if (flag == 0) MessageBox.Show("添加分发记录失败");
            if (flag == -2) MessageBox.Show("逻辑错误");
            bll.UpdateSub_room_stock(output);
            output_set = bll.select_room_output();
            dataGridView2.DataSource = output_set.Tables[0];
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView1.DataSource = room_stock_set.Tables[0];
        }

        private void roomId_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView1.DataSource = room_stock_set.Tables[0];
        }

        private void medNub_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medNub_com.Text.Trim() != "")
            {
                DataRow[] medicine = medicine_set.Tables[0].Select("药品编号='" + medNub_com.Text.Trim() + "'");
                medName_txt.Text = medicine[0][1].ToString();
            }
        }

        private void Operation_room__output_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //定义界面大小
                groupBox1.Height = Height / 3;
                groupBox2.Top = Height / 3 + 1;
                groupBox2.Height = Height / 3;
                groupBox2.Top = Height * 2 / 3 + 1;
                
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                //定义界面大小
                groupBox1.Height = Height / 3;
                groupBox2.Top = Height / 3 + 1;
                groupBox2.Height = Height / 3;
                groupBox2.Top = Height * 2 / 3 + 1;
                
            }
           
        }

       
    }
}
