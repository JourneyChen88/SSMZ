using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace main.科室事物管理
{
    public partial class DateSetup : Form
    {
        string TableName, TextName;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public DateSetup()
        {
            InitializeComponent();
        }


        private void data_maintenance_Load(object sender, EventArgs e)
        {
            //获取数据表名与维护数据之间的关联文本文件
            /*    string file =  @".\datatable.txt";
                string content = "已经维护的基本数据表的都只包含两个字段：id（int ,not null primarykey）name(varchar(50),null)";
                content += "\r\n";
                content += "维护的数据   表名";
                content += "\r\n";
                foreach (TreeNode mynode in treeView1.Nodes)
                {    foreach(TreeNode node in mynode.Nodes)
                    {
                        if (node.Name != "")
                            content += node.Text + "    " +node.Name+ "\r\n";
                    }
                }
                if (File.Exists(file) == true)
                     {
                     MessageBox.Show("存在此文件!");
                      }
                else            
               {                
                    FileStream myFs = new FileStream(file, FileMode.Create);                
                    StreamWriter mySw = new StreamWriter(myFs);                
                    mySw.Write(content);               
                    mySw.Close();                
                    myFs.Close();               
                    MessageBox.Show("写入成功");}
             */
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 0)//如果是最后一级的节点才需要将子窗体添加到主窗体里面
            {
                TextName = e.Node.Text;
                lbText.Text = TextName;
                TableName = e.Node.Name;
                if (TableName.IsNullOrEmpty())
                { MessageBox.Show("数据库中没有建立相应的表", "警告"); return; }
                else
                {
                    dgvTable.DataSource = dal.SelectData(TableName);
                }
                //DataAdd f = new DataAdd(TreenodeText, DataName);//TreenodeText表示是哪一个树节点被单击了
                ////AddForm(f);//将数据维护窗体添加的主窗体里面
                //f.TopLevel = false;
                //panel1.Width = f.Width;
                //panel1.Height = f.Height;
                //panel1.Controls.Add(f);
                //f.Show();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
             int i;
            if (!string.IsNullOrEmpty(this.tbName.Text.Trim()))
            {
                bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.tbName.Text.Trim(), TableName);//判断是否已经添加此证书名称
                if (IsHaveName)
                {
                    MessageBox.Show(tbName.Text.Trim()+"已存在，不能重复添加");
                }
                else
                {
                    if (TableName!="ssjstate")
                    {
                       i = dal.AddData(tbName.Text.Trim(), TableName);
                    }
                    else
                    {
                        i = dal.AddDatas(tbName.Text.Trim(), TableName);
                    }
                    
                    if (i > 0)
                    {
                        dgvTable.DataSource = dal.SelectData(TableName);
                    }
                    else MessageBox.Show("添加失败");
                }
                
            }
            else
            {
                MessageBox.Show(TextName + "名称不能为空");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 1)
            {
                int i=dal.DeleteData(dgvTable.CurrentRow.Cells[0].Value.ToString(), TableName);
                if (i>0)
                {
                    dgvTable.DataSource = dal.SelectData(TableName);
                }
                else MessageBox.Show("删除失败");
            }
            else
            {
                MessageBox.Show("请选择需要删除的行。");
            }
        }

       

       
    }
}
