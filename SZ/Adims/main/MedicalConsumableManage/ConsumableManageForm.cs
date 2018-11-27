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
    public partial class ConsumableManageForm : Form
    {
        ConsumablesUseDal dal = new ConsumablesUseDal();
        public ConsumableManageForm()
        {
            InitializeComponent();
        }

        private void dgvYaowu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName.Text = dgvYaowu.SelectedRows[0].Cells[1].Value.ToString();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text != "")
            {
                int i = 0;
                string name = tbName.Text.Trim();
                string price = tbPrice.Text.Trim();
                string unit = tbUnit.Text.Trim();
                DataTable dt = dal.GetConsumablesByName(name);
                if (dt.Rows.Count == 0)
                {
                    i = dal.InsertConsumables(name, price, unit);
                    BinddgvYaopin();
                    if (i == 0)
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("耗材已存在，不能重复添加！");

            }
            else
                MessageBox.Show("耗材名不能为空！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int i = 0;
                string name = tbName.Text.Trim();
                int id = Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells["0"].Value);
                DataTable dt = dal.GetConsumablesByName(name);
                if (dt.Rows.Count == 0)
                {
                    i = dal.UpdateConsumables(name, id);
                    BinddgvYaopin();
                    if (i == 0)
                    {
                        MessageBox.Show("修改失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("耗材已存在！");
            }
            else
                MessageBox.Show("请选择需要修改的耗材！");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认是否删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgvYaowu.SelectedRows.Count > 0)
                {
                    int flag = 0;
                    flag = dal.DeleteConsumables(Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value));
                    BinddgvYaopin();
                    if (flag == 0)
                    {
                        MessageBox.Show("删除失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("请选择需要删除的耗材！");
            }
        }
        private void BinddgvYaopin()
        {
            DataTable dt = dal.GetConsumablesAll();
            dgvYaowu.DataSource = dt.DefaultView;

        }
        private void szsjGuanli_Load(object sender, EventArgs e)
        {
            BinddgvYaopin();
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }

        private void tbPrice_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }
    }
}
