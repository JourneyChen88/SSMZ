using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main.权限管理
{
    public partial class jurisdiction_distribute : Form
    {
        public jurisdiction_distribute()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        ImageList myImage = new ImageList();
        string module_name = "";

        private void jurisdiction_distribute_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“heYiAMISDataSet.Adims_position_jurisdiction”中。您可以根据需要移动或删除它。
            // this.adims_position_jurisdictionTableAdapter.Fill(this.heYiAMISDataSet.Adims_position_jurisdiction);
            //myImage.Images.Add(Image.FromFile(@".\ico\lion.ico"));
            //myImage.Images.Add(Image.FromFile(@".\ico\schmetterling.ico"));
            //module_tree.ImageList = myImage;
            //foreach (TreeNode node in module_tree.Nodes)
            //{
            //    node.ImageIndex = 0;           
            //}

            DataTable pj_table = (bll.Select_PJ()).Tables[0];
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
            //int count = listBox1.Items.Count;
            //int i = 0;
            //for (i = 0; i < count; i++)
            //{
            //    //listBox1.SetItemCheckState(i, CheckState.Checked);

            //}

            checkBox0.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox4.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int count = listBox1.Items.Count;
            //int i = 0;
            //for (i = 0; i < count; i++)
            //{
            //    listBox1.SetItemCheckState(i, CheckState.Unchecked);
            //}
            checkBox0.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //string pn, jn;
        //public jurisdiction_distribute(string position, string jurisdiction) {
        //    pn = position; jn = jurisdiction;
        //    InitializeComponent();
        //}
        private void button3_Click(object sender, EventArgs e)
        {
            #region MyRegion
            //if (module_name == "") return;
            //int count = checkedListBox1.Items.Count;
            //int[] be_able = new int[count];
            //int i = 0;
            //for (i = 0; i < count; i++)
            //    be_able[i] = 0;
            //for (i = 0; i < count; i++)
            //    be_able[i] = (checkedListBox1.GetItemCheckState(i) == CheckState.Checked) ? 1 : 0;
            //int module_index = 0;
            //if (module_name.Length < 3)
            //{
            //    module_index = int.Parse(module_name);
            //    bll.Upadte_jurisdiction(module_index, be_able);
            //}
            //else
            //{
            //    string[] module_array = module_name.Trim().Split('.');
            //    int start_index = int.Parse(module_array[0]);
            //    int index_array_count = int.Parse(module_array[1]);
            //    int[] module_index_array = new int[index_array_count];
            //    for (i = 0; i < index_array_count; i++) module_index_array[i] = start_index + i;
            //    for (i = 0; i < index_array_count; i++)
            //    {
            //        module_index = module_index_array[i];

            //    }
            //}
            #endregion
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
                DataTable da = bll.SelectQuanxian(listBox1.SelectedItem.ToString());
                if (da.Rows.Count > 0)
                {
                    int c = bll.Upadte_jurisdiction1(jurisdiction, listBox1.SelectedItem.ToString());
                    if (c > 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                }
                else
                {
                    int c = bll.InsertQuanxian(listBox1.SelectedItem.ToString(), jurisdiction);
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
            DataTable dt = bll.SelectQuanxian(listBox1.SelectedItem.ToString());
            string quanlist = dt.Rows[0]["jurisdiction"].ToString();
            if (quanlist.Contains("0")) checkBox0.Checked = true;
            if (quanlist.Contains("1")) checkBox1.Checked = true;
            if (quanlist.Contains("2")) checkBox2.Checked = true;
            if (quanlist.Contains("3")) checkBox4.Checked = true;
            if (quanlist.Contains("4")) checkBox4.Checked = true;
            if (quanlist.Contains("5")) checkBox5.Checked = true;
            if (quanlist.Contains("6")) checkBox6.Checked = true;
            if (quanlist.Contains("7")) checkBox6.Checked = true;
        }

       
    }
}