using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class SourceFile : Form
    {
        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();
        public SourceFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//插入记录
        {
            int result=0;
            string filename=txtFileName.Text;
            string fileclass=cmbClass.Text;
            string author=txtAuthor.Text;

           
            DataTable dt = apro.GetAdims_SourceFile(filename);
            if (dt.Rows.Count==0)
            {
                apro.InsertAdims_SourceFileName(filename, fileclass, author, DateTime.Now);
                DataTable dt2 = apro.GetAdims_SourceFile(filename);
                int FileID = Convert.ToInt32(dt2.Rows[0][0]);
                for (int  i= 0;  i< richTextBox1.Lines.Count(); i++)
                {
                    string rowtext = richTextBox1.Lines[i].ToString();
                    result = apro.InsertAdims_SourceFileContent(FileID, i, richTextBox1.Lines[i].ToString());
                    result++;
                }
                if (result>0)
                {
                    MessageBox.Show("添加成功");
                    TreeView_Bind();
                }
                else
                    MessageBox.Show("添加失败");
                
            }
        }

      
     
        private void AddSourceFile_Load(object sender, EventArgs e)
        {
            TreeView_Bind();

        }
        private void TreeView_Bind()
        {
            treeView1.Nodes.Clear();

            DataTable dt = apro.GetAllFileClass();
            TreeNode node = new TreeNode();
            for (int i = 0; i < dt.Rows.Count; i++)
			{
                treeView1.Nodes.Add(dt.Rows[i][0].ToString());
                DataTable dt1 = apro.GetAllFileNameByClass(dt.Rows[i][0].ToString());
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    treeView1.Nodes[i].Nodes.Add(dt1.Rows[j][0].ToString());
                }
			}
            
            
        
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (treeView1.SelectedNode.Level == 0)
            {
                cmbClass.Text = treeView1.SelectedNode.Text;
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                txtFileName.Text = treeView1.SelectedNode.Text;
                cmbClass.Text = treeView1.SelectedNode.Parent.Text;
                richTextBox1.Text = "";
                DataTable dt2 = apro.GetAdims_SourceFileContent(txtFileName.Text);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    richTextBox1.Text += dt2.Rows[i][1].ToString() + "\n";
                }
            }
            else
                return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                if (MessageBox.Show("你确定要修改此知识库吗?", "信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string filename = treeView1.SelectedNode.Text;
                    int i = 0, j = 0;
                    j = apro.DeleteAdims_SourceFileNameontent(filename);
                    i = apro.DeleteAdims_SourceFileName(filename);

                    if (i > 0 && j > 0)
                    {
                        MessageBox.Show("删除成功");
                        TreeView_Bind();

                    }
                    else
                        MessageBox.Show("删除失败，请重试");
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的文章");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int result = 0, j = 0;
            if (MessageBox.Show("你确定要修改此知识库吗?", "信息", MessageBoxButtons.YesNo) == DialogResult.Yes)

            {
                string filename = txtFileName.Text;
                j = apro.DeleteAdims_SourceFileNameontent(filename);
                DataTable dt2 = apro.GetAdims_SourceFile(filename);
                int FileID = Convert.ToInt32(dt2.Rows[0][0]);
                for (int i = 0; i < richTextBox1.Lines.Count(); i++)
                {
                    string rowtext = richTextBox1.Lines[i].ToString();
                    result = apro.InsertAdims_SourceFileContent(FileID, i, rowtext);
                    result++;
                }
                if (j>0&&result>0)
                {
                    MessageBox.Show("修改成功");
                }
                else
                    MessageBox.Show("修改失败");

            }
        }

      
    }
}
