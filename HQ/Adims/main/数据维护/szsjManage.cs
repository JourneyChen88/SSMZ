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
        SzsjListDal _SzsjListDal = new SzsjListDal();
        YaopinDal _YaopinDal = new YaopinDal();
        string name1="", suoxie1="",type1="";
        public szsjGuanli()
        {
            InitializeComponent();
        }

        private void dgvYaowu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtSJname.Text = dgvShijian.CurrentRow.Cells["name"].Value.ToString();
            cmbSJType.Text = dgvShijian.CurrentRow.Cells["type"].Value.ToString();
            tbSJSuoxie.Text = dgvShijian.CurrentRow.Cells["suoxie"].Value.ToString();
        }

        private void cmbYpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = _SzsjListDal.GetSzsjListByType(cmbSJType.Text.Trim());
            dgvShijian.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSJname.Text != "" && cmbSJType.Text != "")
            {
                int i = 0;
                name1 = txtSJname.Text.Trim();
                type1 = cmbSJType.Text.Trim();
                suoxie1=tbSJSuoxie.Text.Trim();
                DataTable dt = _SzsjListDal.GetSzsjListByName(name1);
                if (dt.Rows.Count == 0)
                {
                    i = _SzsjListDal.InsertSzsjList(name1,suoxie1, type1);
                    BinddgvShijian();
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
            if (dgvShijian.SelectedRows.Count > 0)
            {
                int flag = 0;
                name1 = txtSJname.Text.Trim();
                type1 = cmbSJType.Text.Trim();
                suoxie1 = tbSJSuoxie.Text.Trim();
                DataTable dt = _SzsjListDal.GetSzsjListByName(name1);
                if (dt.Rows.Count == 0)
                {
                    flag = _SzsjListDal.UpdateSzsjList(name1, suoxie1, type1, Convert.ToInt32(dgvShijian.SelectedRows[0].Cells[0].Value));
                    BinddgvShijian();
                    if (flag == 0)
                        MessageBox.Show("修改失败，请重试！");
                }
                else
                    MessageBox.Show("术中事件已存在，不能重复添加！");
            }
            else
                MessageBox.Show("请选择需要修改的术中事件！"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvShijian.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = _SzsjListDal.DeleteSzsjList(Convert.ToInt32(dgvShijian.SelectedRows[0].Cells[0].Value));
                BinddgvShijian();
                if (flag == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的术中事件！"); 
        }
        private void BinddgvShijian()
        {
            DataTable dt = _SzsjListDal.GetSzsjListByType(cmbSJType.Text.Trim());
            dgvShijian.DataSource = dt.DefaultView;

        }
        private void szsjGuanli_Load(object sender, EventArgs e)
        {
            BinddgvShijian();
        }
    }
}
