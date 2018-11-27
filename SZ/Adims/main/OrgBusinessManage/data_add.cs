using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;
using adims_DAL;
using adims_BLL;

namespace main.OrgBusinessManage
{

    public partial class data_add : Form
    {
        adims_DAL.AdimsProvider dal = new AdimsProvider();
        private string nodeText="";
        private string belongText = "";
        private string tableName="";
        string id = "";
        bool flag = false;     
        DataTable mytable = new DataTable();
        Label label2;
        ComboBox belong_com;
        string name = "";    //维护数据值
        string belong = "";  //维护数据所属类别
        public data_add(string text,string name)
        {   
            InitializeComponent();
            nodeText = text;
            tableName = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = name_txt.Text.Trim();
            if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
            {
                belong = belong_com.Text.Trim();
                if (!test2("添加")) return;
                AdimsProvider.AddData1(name, belong, tableName);
                reveal2();
            }

            else
            {
                if (!test1("添加")) return;
                AdimsProvider.AddData1(name, tableName);
                reveal1();
            }
        
        }

        
        private void data_add_Load(object sender, EventArgs e)
        {
            if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
            {
                string fatherText = "";
                string fatherName = "";
                switch (nodeText)
                {
                        case "麻醉小类":
                        fatherText="麻醉大类";
                        fatherName="mazuidalei";
                        break;
                        case "疾病小类":
                        fatherText="疾病大类";
                        fatherName="jibingdalei";
                        break;
                        case "药品小类":
                        fatherText="药品大类";
                        fatherName = "yaopindalei";
                        break;
                }
                label1.Text = nodeText;
                belongText =fatherText;
                name_txt.Location = new Point(label1.Location.X + label1.Width + 10, name_txt.Location.Y);
                label2 = new Label();
                panel1.Controls.Add(label2);
                label2.Text = fatherText;
                name_txt.Width = 90;
                label2.Location = new Point(name_txt.Location.X + name_txt.Width + 15, label1.Location.Y);
                label2.Visible = true;
                label2.AutoSize = true;
                belong_com = new ComboBox();
                panel1.Controls.Add(belong_com);
                belong_com.Location = new Point(label2.Location.X + label2.Width + 10, name_txt.Location.Y);
                belong_com.Width = 90;
                belong_com.Visible = true;
                mytable = AdimsProvider.GetData1(fatherName, fatherText);
                for (int k = 0; k < mytable.Rows.Count; k++)
                    belong_com.Items.Add(mytable.Rows[k][1].ToString());
                if (belong_com.Items.Count > 0) belong_com.SelectedIndex = 0;
                reveal2();
            }

            else
            {
                label1.Text = nodeText;
                reveal1();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(flag)
            {   name = name_txt.Text.ToString();
            if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
            {
                belong = belong_com.Text.Trim();
                if (!test2("修改")) return;
                AdimsProvider.UpdateData1(id, name, belong, tableName);
                reveal2();
            }

            else
            {
                if (!test1("修改")) return;
                AdimsProvider.UpdateData1(id, name, tableName);
                reveal1();
            }
                
                flag = false;
            }
        }
        
        
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count-1)
            {
                if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
                { belong_com.Text = dataGridView1.Rows[(e.RowIndex)].Cells[1].Value.ToString();
                belong=belong_com.Text.Trim();}
                mytable = AdimsProvider.GetData1(tableName, nodeText);
                name_txt.Text = dataGridView1.Rows[(e.RowIndex)].Cells[0].Value.ToString();  
                name=name_txt.Text;
                id = mytable.Rows[(e.RowIndex)][0].ToString().Trim();
                flag = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flag)
            {    
                string temp_belong = "";
                if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类") 
                { 
                   temp_belong = belong + "下的";
                }
                MessageBox.Show("你真的要删除"+belongText+temp_belong+nodeText+name+"吗","警告");
                AdimsProvider.DeleteData(id, tableName);
                if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
                {
                    reveal2();
                }

                else
                {
                    reveal1();
                }
                flag = false;
            }
        }

        private void reveal1()
        {
            mytable = AdimsProvider.GetData1(tableName, nodeText);
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn mycol = new DataGridViewTextBoxColumn();
            mycol.HeaderText = mytable.Columns[1].Caption;
            mycol.Visible = true;
            mycol.ReadOnly = true;
            dataGridView1.Columns.Add(mycol);
            for (int j = 0; j < mytable.Rows.Count; j++)
            {
                DataGridViewRow newrow = new DataGridViewRow();
                dataGridView1.Rows.Add(newrow);
                dataGridView1.Rows[j].Cells[0].Value = mytable.Rows[j][1];
            }
        }
        private void reveal2()
        {
            mytable = AdimsProvider.GetData1(tableName, nodeText,belongText);
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn mycol1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn mycol2 = new DataGridViewTextBoxColumn();
            mycol1.HeaderText = mytable.Columns[1].Caption;
            mycol2.HeaderText = mytable.Columns[2].Caption;
            mycol1.Visible = true;
            mycol1.ReadOnly = true;
            mycol2.Visible = true;
            mycol2.ReadOnly = true;
            dataGridView1.Columns.Add(mycol1);
            dataGridView1.Columns.Add(mycol2);
            for (int j = 0; j < mytable.Rows.Count; j++)
            {
                DataGridViewRow newrow = new DataGridViewRow();
                dataGridView1.Rows.Add(newrow);
                dataGridView1.Rows[j].Cells[0].Value = mytable.Rows[j][1];
                dataGridView1.Rows[j].Cells[1].Value = mytable.Rows[j][2];
            }
        }
        private bool test1(string opt)
        {
            if (name == "")
            {
                MessageBox.Show(nodeText + "不能为空", "警告");
                return false;
            }
            mytable = AdimsProvider.GetData1(tableName, nodeText);
            for (int i = 0; i < mytable.Rows.Count; i++)
            {
                if (mytable.Rows[i][1].ToString().Trim() == name.Trim())
                {
                    MessageBox.Show("此" + nodeText + "已存在，"+opt+"被拒绝", "警告");
                    return false;
                }
            }
            return true;
        }

        private bool test2(string opt)
        {
            if (name == "")
            {
                MessageBox.Show(nodeText + "不能为空", "警告");
                return false;
            }
            mytable = AdimsProvider.GetData1(tableName, nodeText, belongText);
            for (int i = 0; i < mytable.Rows.Count; i++)
            {
                if (mytable.Rows[i][1].ToString().Trim() == name && mytable.Rows[i][2].ToString().Trim() == belong)
                {
                    MessageBox.Show("属于此" +belongText+"的"+ nodeText + name+"已存在，"+opt+"被拒绝", "警告");
                    return false;
                }
            }
            return true;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                if (nodeText == "麻醉小类" || nodeText == "疾病小类" || nodeText == "药品小类")
                {
                    belong_com.Text = dataGridView1.Rows[(e.RowIndex)].Cells[1].Value.ToString();
                    belong = belong_com.Text.Trim();
                }
                mytable = AdimsProvider.GetData1(tableName, nodeText);
                name_txt.Text = dataGridView1.Rows[(e.RowIndex)].Cells[0].Value.ToString();
                name = name_txt.Text;
                id = mytable.Rows[(e.RowIndex)][0].ToString().Trim();
                flag = true;
            }
        }
        DataTable excelTable = new DataTable();
        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
