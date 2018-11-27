using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.OrgBusinessManage
{
    public partial class Add_KS : Form
    {
        admin_T_SQL at = new admin_T_SQL();
        public Add_KS()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string patname = this.txtpatName.Text.Trim();
            if (patname == "")
            {
                banding();
            }
            else
            {
                DataTable dt = at.select_Patdpm1(patname);
                dgvks.DataSource = dt;
            }
        }

        private void Add_KS_Load(object sender, EventArgs e)
        {
            banding();
        }
        public void banding()
        {
            DataTable dt = at.select_Patdpm();
            dgvks.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string patname = this.txtpatName.Text.Trim();
            if (patname == "")
            {
                MessageBox.Show("请输入科室名称！");
                txtpatName.Focus();
            }
            else
            {
                int a = at.inset_patdpm(patname);
                if (a > 0)
                {
                    MessageBox.Show("添加科室 " + txtpatName + " 成功！");
                    banding();
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string KSid = dgvks.SelectedRows[0].Cells["科室编号"].Value.ToString();
            if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int a = at.delete_patdpm(KSid);
                if (a > 0)
                {
                    MessageBox.Show("删除科室成功！");
                    banding();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string KSid = dgvks.SelectedRows[0].Cells["科室编号"].Value.ToString();
            if (txtpatName.Text.Trim() == "")
            {
                MessageBox.Show("请输入修改的名称！");

            }
            else
            {
                int a = at.update_patdpm(KSid, txtpatName.Text.Trim());
                if (a > 0)
                {
                    MessageBox.Show("修改科室名称成功！");
                    banding();
                }
            }
        }
    }
}
