using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.科室事物管理
{
    public partial class Fpb : Form
    {
        Label label = null;

        string riqi = "";
        public Fpb(Label label1)
        {

            InitializeComponent();
            label = label1;
            riqi = ((DateTime)(label.Tag)).Date.ToShortDateString();
            this.textBox2.Text = riqi;
            string text = label.Text;
            bool b = adims_BLL.AdimsController.isPaiBan(riqi);
            if (!b)
            {
                string[] s = text.Split('\n');
                for (int i = 1; i < 4; i++)
                {
                    string content = "";
                    bool iscontent = false;
                    for (int j = 0; j < s[i].Length; j++)//获得排班的内容，不包括日期
                    {
                        if (iscontent)
                        {
                            content = content + s[i][j];
                        }
                        if (s[i][j] == ' ')
                        {
                            iscontent = true;
                        }

                    }

                    if (i == 1)
                    { comboBox1.Text = content; }
                    else if (i == 2)
                    { comboBox3.Text = content; }
                    else if (i == 3)
                    { comboBox2.Text = content; }



                }
            }//修改排班

        }


        private void button1_Click(object sender, EventArgs e)
        {

            //string text = "\n" + label1.Text + "  " + comboBox1.Text + "\n" + label3.Text + "  "
            //    + comboBox3.Text + "\n" + label2.Text + "  " + comboBox2.Text;

            //bool b = adims_BLL.AdimsController.isPaiBan(riqi);
            //if (b)
            //{ adims_BLL.AdimsController.AddData(text, textBox2.Text); }//排班
            //else
            //{ adims_BLL.AdimsController.UpdateData(text, textBox2.Text); }//修改排班5
            //this.Close();
            //Fygpb f = (Fygpb)(this.label.Parent.Parent.Parent);
            //f.panel3.Controls.Clear();
            //f.Fygpb_Load(null, null);
            //MessageBox.Show("保存成功");
        }

        private void Fpb_Load(object sender, EventArgs e)
        {
            //comboBox1.DataSource = adims_BLL.AdimsController.getdata1();
            //comboBox1.DisplayMember = "Name";
            //comboBox3.DataSource = adims_BLL.AdimsController.getdata2();
            //comboBox3.DisplayMember = "Name";
            //comboBox2.DataSource = adims_BLL.AdimsController.getdata4();
            //comboBox2.DisplayMember = "Name";
        }


    }
}
