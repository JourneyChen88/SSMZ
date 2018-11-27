using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.MedicalConsumableManage
{
    public partial class YPguanli : Form
    {
        PACU_DAL dal = new PACU_DAL();
        string ypLx;
        public string ypName1;
        public string bagName1;
        public YPguanli()
        {
            InitializeComponent();
        }

        private void BindYaopin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType(cmbYpType.Text.Trim());
            dgvYaowu.DataSource = dt.DefaultView;

        }
        private void BindYaoPinBag()
        {
            DataTable dt = dal.GetAdims_YaoPinBagByType(this.cmbBagName.Text.Trim());
            this.dataGridView1.DataSource = dt.DefaultView;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtYPname.Text != "" && cmbYpType.Text != "")
            {
                int i = 0;
                ypName1 = txtYPname.Text.Trim();
                ypLx = cmbYpType.Text.Trim();
                DataTable dt = dal.GetAdims_YaoPin(ypName1, ypLx);
                if (dt.Rows.Count == 0)
                {
                    i = dal.InsertAdims_YaoPin(ypName1, ypLx);
                    BindYaopin();
                    if (i == 0)
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("同类型药品已存在，不能重复添加！");

            }
            else
                MessageBox.Show("药品名和药品类型不能为空！");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = dal.deleteAdims_YaoPin(Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value));
                BindYaopin();
                if (flag == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的药品！");
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
                    flag = dal.UpdateAdims_YaoPin(ypName1, ypLx, Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value));
                    BindYaopin();
                    if (flag == 0)
                        MessageBox.Show("修改失败，请重试！");
                }
                else
                    MessageBox.Show("药品已存在，不能重复添加！");
            }
            else
                MessageBox.Show("请选择需要修改的药品！");
        }

        private void YPguanli_Load(object sender, EventArgs e)
        {
            BindYaopin();
            BindYaoPinBag();
            DataTable dt = dal.GetAdims_ALLBagName();
            cmbBagName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbBagName.Items.Add(dt.Rows[i][0]);
            }
            cmbBagName.SelectedIndex = 0;
        }

        private void dgvYaowu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtYPname.Text = dgvYaowu.SelectedRows[0].Cells[1].Value.ToString();
            cmbYpType.Text = dgvYaowu.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void cmbYpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dal.GetAdims_YaoPinByType(cmbYpType.Text.Trim());
            dgvYaowu.DataSource = dt.DefaultView;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.tbYL.Text != "" && cmbDW.Text != "" && comboBox2.Text != "" && cmbBagName.Text != "")
            {
                int i = 0;
                bagName1 = cmbBagName.Text.Trim();
                ypName1 = comboBox2.Text.Trim();

                int cxyy1 = checkBox1.Checked ? 1 : 0;
                DataTable dt = dal.GetAdims_YaoPinBag(ypName1, bagName1);
                if (dt.Rows.Count == 0)
                {
                    i = dal.InsertAdims_YaoPinBag(bagName1, ypName1, tbYL.Text.Trim(), cmbDW.Text.Trim(), cmbZRFS.Text.Trim(), cxyy1);
                    BindYaoPinBag();
                    if (i == 0)
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("添加成功！");

            }
            else
                MessageBox.Show("药品名,药品类型,药包名不能为空！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = dal.deleteAdims_YaoPinBag(Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value));
                BindYaoPinBag();
                if (flag == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的药品！");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            DataTable dt = dal.GetAdims_YaoPinByType(comboBox1.Text.Trim());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i]["YPNAME"]);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && cmbZRFS.Text != "" && cmbBagName.Text != "" && cmbDW.Text != "" && tbYL.Text != "")
            {
                string ypname = comboBox2.Text;
                string ybName = cmbBagName.Text;
                string dw = cmbDW.Text;
                string yl = tbYL.Text;
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                int cxyy = 0;
                if (checkBox1.Checked)
                {
                    cxyy = 1;
                }
                else
                {
                    cxyy = 0;
                }
                string zrff = cmbZRFS.Text;
                int num = dal.updateYaoPin(ybName, ypname, cxyy, id, dw, yl, zrff);
                if (num > 0)
                {
                    MessageBox.Show("修改成功");
                    BindYaoPinBag();
                }
            }
        }

        private void cmbBagName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindYaoPinBag();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells["ypname2"].Value.ToString();
            cmbBagName.Text = dataGridView1.SelectedRows[0].Cells["bagName"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["yl"].Value.ToString();
            cmbDW.Text = dataGridView1.SelectedRows[0].Cells["DW"].Value.ToString();
            int checkNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["cxyy"].Value.ToString());
            if (checkNo == 0)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;

        }

    }
}
