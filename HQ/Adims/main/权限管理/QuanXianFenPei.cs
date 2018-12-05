using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL.Dics;

namespace main.权限管理
{
    public partial class QuanXianFenPei : Form
    {
        UserDal _UserDal = new UserDal();
        public QuanXianFenPei()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        ImageList myImage = new ImageList();
       
        private void jurisdiction_distribute_Load(object sender, EventArgs e)
        {
            DataTable pj_table = (_UserDal.GetPositionJurisdictionAll()).Tables[0];
            int count = pj_table.Rows.Count;
            int i = 0;
            for (i = 0; i < count; i++)
            {
                listBox1.Items.Add(pj_table.Rows[i][1]);
            }
            listBox1.SelectedIndex = 0;
        }
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            checkBox0.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox4.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox0.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void button3_Click(object sender, EventArgs e)
        {           
            try
            {
                string jurisdiction = string.Empty;
                if (checkBox0.Checked == true) jurisdiction += "0";
                if (checkBox1.Checked == true) jurisdiction += "1";
                if (checkBox2.Checked == true) jurisdiction += "2";
                if (checkBox3.Checked == true) jurisdiction += "3";
                if (checkBox4.Checked == true) jurisdiction += "4";
                if (checkBox5.Checked == true) jurisdiction += "5";
                if (checkBox6.Checked == true) jurisdiction += "6";
                if (checkBox7.Checked == true) jurisdiction += "7";
                if (checkBox8.Checked == true) jurisdiction += "8";
                DataTable da = _UserDal.GetPositionJurisdictionByPosition(listBox1.SelectedItem.ToString());
                if (da.Rows.Count > 0)
                {
                    int c = _UserDal.UpadtePositionJurisdiction(jurisdiction, listBox1.SelectedItem.ToString());
                    if (c > 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                }
                else
                {
                    int c = _UserDal.InsertPositionJurisdiction(listBox1.SelectedItem.ToString(), jurisdiction);
                    if (c > 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常", ex.ToString());
            }
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            checkBox0.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            DataTable dt = _UserDal.GetPositionJurisdictionByPosition(listBox1.SelectedItem.ToString());
            string quanlist = dt.Rows[0]["jurisdiction"].ToString();
            if (quanlist.Contains("0")) checkBox0.Checked = true;
            if (quanlist.Contains("1")) checkBox1.Checked = true;
            if (quanlist.Contains("2")) checkBox2.Checked = true;
            if (quanlist.Contains("3")) checkBox3.Checked = true;
            if (quanlist.Contains("4")) checkBox4.Checked = true;
            if (quanlist.Contains("5")) checkBox5.Checked = true;
            if (quanlist.Contains("6")) checkBox6.Checked = true;
            if (quanlist.Contains("7")) checkBox7.Checked = true;
            if (quanlist.Contains("8")) checkBox8.Checked = true;
        }

       
    }
}