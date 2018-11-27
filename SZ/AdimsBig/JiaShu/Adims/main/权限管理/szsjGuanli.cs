using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.权限管理
{
    public partial class szsjGuanli : Form
    {
        PACU_DAL dal = new PACU_DAL();
        string ypLx;
        public string ypName1;
        public szsjGuanli()
        {
            InitializeComponent();
        }

        private void dgvYaowu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtYPname.Text = dgvYaowu.SelectedRows[0].Cells[1].Value.ToString();
            cmbYpType.Text = dgvYaowu.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void cmbYpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dal.GetAdims_szsjListByType(cmbYpType.Text.Trim());
            dgvYaowu.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtYPname.Text != "" && cmbYpType.Text != "")
            {
                int i = 0;
                ypName1 = txtYPname.Text.Trim();
                ypLx = cmbYpType.Text.Trim();
                DataTable dt = dal.GetAdims_szsjListByType(ypName1,ypLx);
                if (dt.Rows.Count == 0)
                {
                    i = dal.InsertAdims_szsjList(ypName1, ypLx);
                    BinddgvYaopin();
                    if (i == 0)
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("同类型术中事件已存在，不能重复添加！");

            }
            else
                MessageBox.Show("术中事件和事件类型不能为空！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int flag = 0;
                ypName1 = txtYPname.Text.Trim();
                ypLx = cmbYpType.Text.Trim();
                DataTable dt = dal.GetAdims_YaoPin(ypName1, ypLx);
                if (dt.Rows.Count == 0)
                {
                    flag = dal.UpdateAdims_szsjList(ypName1, ypLx, Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value));
                    BinddgvYaopin();
                    if (flag == 0)
                        MessageBox.Show("修改失败，请重试！");
                }
                else
                    MessageBox.Show("术中时间已存在，不能重复添加！");
            }
            else
                MessageBox.Show("请选择需要修改的术中时间！"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = dal.deleteAdims_szsjList(Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value));
                BinddgvYaopin();
                if (flag == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的术中事件！"); 
        }
        private void BinddgvYaopin()
        {
            DataTable dt = dal.GetAdims_szsjListByType(cmbYpType.Text.Trim());
            dgvYaowu.DataSource = dt.DefaultView;

        }
        private void szsjGuanli_Load(object sender, EventArgs e)
        {
            BinddgvYaopin();
        }
    }
}
