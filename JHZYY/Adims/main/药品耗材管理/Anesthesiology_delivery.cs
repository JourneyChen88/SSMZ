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

namespace main.药品耗材管理
{
    public partial class Anesthesiology_delivery : Form
    {
        private void Form1_Resize(object sender, EventArgs e)        
        {
            groupBox1.Top = 0;//控件距离界面上边缘          
            groupBox1.Height = Height * 5 / 18;//控件高度为界面的5/18                       
            groupBox1.Left = 0;//控件左边框据界面左边框的距离是界面宽度的1/2；
                    
            groupBox2.Height = Height * 5 / 18;//控件高度为界面的5/18                       

            panel1.Height = Height / 6;
                   
            groupBox1.Height = Height * 5 / 18;//控件高度为界面的5/18                       
            groupBox1.Left = 0;//控件左边框据界面左边框的距离是界面宽度的1/2；
                                
        }

        public Anesthesiology_delivery()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet output_set = new DataSet();
        DataSet stock_set = new DataSet();
        DataSet room_stock_set = new DataSet();
        DataSet medicine_set = new DataSet();
        private void Anesthesiology_delivery_Load(object sender, EventArgs e)
        {
            confirmPerson_txt.Text = Program.customer.user_name;
            stock_set = bll.select_surgery_stock();
            output_set = bll.select_surgery_output();
            medicine_set = bll.select_medicine_info();
            dataGridView1.DataSource = stock_set.Tables[0];
            dataGridView2.DataSource = output_set.Tables[0];          
            for (int i = 0; i < stock_set.Tables[0].Rows.Count; i++)
                medNub_com.Items.Add(stock_set.Tables[0].Rows[i][0].ToString());
            for (int i = 0; i < 10; i++)
                roomId_com.Items.Add(i.ToString());
            medNub_com.SelectedIndex = 0;
            roomId_com.SelectedIndex = 0;
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView3.DataSource = room_stock_set.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            surgery_output output = new surgery_output();
            output.surgery_id = 0;  //麻醉科编号为0
            output.room_id = int.Parse(roomId_com.Text.Trim());
            output.medicine_number = medNub_com.Text.Trim();
            output.output_count = int.Parse(count_txt.Text.Trim());
            output.confirm_person = confirmPerson_txt.Text;
            output.output_time = DateTime.Now;
            int test = bll.TestSub_surgery_stock(output);
            if (0 == test)
            { MessageBox.Show("药库中没有这种药", "警告"); return; }
            if (1 == test)
            { MessageBox.Show("药库中这种药的库存不足", "警告"); return; }
            int flag = 0;
            flag = bll.Add_surgery_output(output);
            if (flag == 0) MessageBox.Show("添加分发记录失败");
            if (flag == -2) MessageBox.Show("逻辑错误");
            bll.UpdateSub_surgery_stock(output);
            output_set = bll.select_surgery_output();
            dataGridView2.DataSource = output_set.Tables[0];
            room_input input = new room_input();
            input.surgery_id = 0; //麻醉科编号为0
            input.room_id = int.Parse(roomId_com.Text);
            input.medicine_number = medNub_com.Text;
            input.input_count = int.Parse(count_txt.Text);
            input.confirm_person = confirmPerson_txt.Text;
            input.input_time = DateTime.Now;
            bll.UpdateAdd_room_stock(input);
            bll.Add_room_input(input);
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
            dataGridView3.DataSource = room_stock_set.Tables[0];

            stock_set = bll.select_surgery_stock();
            dataGridView1.DataSource = stock_set.Tables[0];

        }

        private void roomId_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            room_stock_set = bll.select_room_stock(int.Parse(roomId_com.Text));
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
