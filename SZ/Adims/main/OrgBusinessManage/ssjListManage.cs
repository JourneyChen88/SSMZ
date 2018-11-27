using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;

namespace main
{
    public partial class ssjListManage : Form
    {
        DBConn dbcon = new DBConn();
        public ssjListManage()
        {
            InitializeComponent();
        }

        private void ssjListManage_Load(object sender, EventArgs e)
        {
            DataBind();


        }

        private void DataBind()
        {
            string sql = "select id,oname from ssjstate";
            DataTable dt = dbcon.GetDataTable(sql);
            datagridview1.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
                MessageBox.Show("手术间名不能为空！");
            else
            {
                string sql = "select oname from ssjstate where oname='" + textBox1.Text.Trim() + "'";
                DataTable dt = dbcon.GetDataTable(sql);
                string sqlInsert = "insert into ssjstate(oname) values('" + textBox1.Text.Trim() + "')";
                int i = 0;
                if (dt.Rows.Count > 0)
                    MessageBox.Show("手术间名已存在，不能重复添加");
                else
                    
                    i = dbcon.ExecuteNonQuery(sqlInsert);

                if (i > 0)
                {
                    MessageBox.Show("手术间添加成功");
                    DataBind();
                }
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (datagridview1.SelectedRows.Count == 1)
            {
                if (tbName.Text.Trim() == "")
                    MessageBox.Show("手术间名不能为空！");
                else
                {
                    string sql = "select oname from ssjstate where oname='" + tbName.Text.Trim() + "'";
                    DataTable dt = dbcon.GetDataTable(sql);
                    string sqlInsert = "update ssjstate set oname='" + tbName.Text.Trim() + "'where id='" + datagridview1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                    int i = 0;
                    if (dt.Rows.Count > 0)
                        MessageBox.Show("手术间名已存在，不能修改");
                    else i = dbcon.ExecuteNonQuery(sqlInsert);

                    if (i > 0)
                    {
                        MessageBox.Show("手术间修改成功");
                        DataBind();
                    }
                }
            }
            else MessageBox.Show("请选中一行需要修改的手术间");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (datagridview1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string sqlInsert = "delete from ssjstate  where oname= '" + datagridview1.SelectedRows[0].Cells["ssjName"].Value.ToString() + "'";
                    int i = 0;
                    i = dbcon.ExecuteNonQuery(sqlInsert);
                    if (i > 0)
                    {
                        MessageBox.Show("手术间删除成功");
                        DataBind();
                    }
                }
            }
            else MessageBox.Show("请选中一行需要删除的手术间");
        }

        private void datagridview1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (datagridview1.SelectedRows.Count > 0)
            {

                tbName.Text = datagridview1.SelectedRows[0].Cells["ssjName"].Value.ToString();
                label3.Text = datagridview1.SelectedRows[0].Cells["ssjName"].Value.ToString();
            }
        }
    }
}
