using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;
namespace main
{
    public partial class QiXieQD : Form
    {
        adims_DAL.admin_T_SQL at = new admin_T_SQL();
        public QiXieQD()
        {
            InitializeComponent();
        }
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        private void Bind_cmbQXGL()
        {
            cmbQXGL.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbQXGL.Items.Add(dt.Rows[i][0]);
            }
            cmbQXGL.SelectedIndex = 0;
        }
        private void Bind_cmbModel()
        {
            cmbModel.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbModel.Items.Add(dt.Rows[i][0]);
            }
            cmbModel.SelectedIndex = 0;
        }
        private void QiXieQD_Load(object sender, EventArgs e)
        {
            Bind_cmbQXGL();
            Bind_cmbModel();
            BinddgvModel(cmbModel.Text);
            BinddgvQXGL(cmbQXGL.Text);
        }
        public void BinddgvModel(string cmb)
        {
            DataTable dt = DAL.SelectqxmcInmodel(cmb);
            dgvModel.DataSource = dt;
        }
        public void BinddgvQXGL(string cmb)
        {
            DataTable dt = DAL.SelectqxmcInmodel(cmb);
            dgvQXGL.DataSource = dt;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinddgvModel(cmbModel.Text);
        }


        private void txtQXnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            UserFunction.Text_Value_decimal(sender, e);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dgvQXGL.SelectedRows[0].Cells["qxcount1"].Value != null && dgvQXGL.SelectedRows[0].Cells["qxmc1"].Value != null)
            {
                button1.Text = "修改器械";
                this.txtQXName.Text = dgvQXGL.SelectedRows[0].Cells["qxmc1"].Value.ToString();
                this.txtQXnum.Text = dgvQXGL.SelectedRows[0].Cells["qxcount1"].Value.ToString();
            }
            else
                MessageBox.Show("请选择有效数据行!!!");


        }

        private void 删除器械ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvQXGL.SelectedRows[0].Cells["Column3"].Value != null)
            {
                int id = int.Parse(dgvQXGL.SelectedRows[0].Cells["Column3"].Value.ToString());
                int a = DAL.deleteQxmc(id);
                if (a > 0)
                {
                    MessageBox.Show("删除成功");
                    Bind_cmbQXGL();
                }

            }
            else
                MessageBox.Show("请选择有效数据");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int flag;
            try
            {
                if (txtQXName.Text == "")
                {
                    MessageBox.Show("请输入器械名称！");
                    txtQXName.Focus();
                }
                else if (txtQXnum.Text == "")
                {
                    MessageBox.Show("请输入器械数量！");
                    txtQXnum.Focus();
                }
                else
                {
                    string qzmz = this.txtQXName.Text;
                    int qxCount = int.Parse(this.txtQXnum.Text);
                    string qxmbm = cmbQXGL.Text;
                    if (button1.Text == "增加器械")
                    {
                        flag = DAL.InsertqxqdModel(qzmz, qxmbm, qxCount);
                        if (flag > 0)
                        {
                            MessageBox.Show("增加器械成功！");
                            this.txtQXName.Text = "";
                            this.txtQXnum.Text = "";
                            BinddgvQXGL(cmbQXGL.Text);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("确定修改", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            int id = int.Parse(dgvQXGL.SelectedRows[0].Cells["id1"].Value.ToString());
                            flag = DAL.UpdateqxqdModel(qzmz, qxmbm, qxCount, id);
                            if (flag > 0)
                            {
                                MessageBox.Show("修改器械成功！");
                                this.txtQXName.Text = "";
                                this.txtQXnum.Text = "";
                                button1.Text = "增加器械";
                                BinddgvQXGL(cmbQXGL.Text);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("增加器械失败！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入新包名！");
                textBox2.Focus();
            }
            else
            {
                int updatenum = at.updateQXMC(cmbModel.Text, textBox2.Text.Trim());
                int updateModelname = at.updateModelName(cmbModel.Text, textBox2.Text.Trim());
                if (updatenum > 0 && updateModelname > 0)
                {
                    MessageBox.Show("修改成功");
                    Bind_cmbModel();
                    BinddgvModel(cmbModel.Text);
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入器械模板名称！");
                textBox2.Focus();
            }
            else
            {
                DataTable dt = DAL.SlectqxModel(textBox2.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("器械模板已存在，请重新输入！");
                    textBox2.Focus();
                }
                else
                {
                    int j = 0, flag = 0;
                    string qxmbm = textBox2.Text.Trim();
                    try
                    {
                        flag = at.InsertqxqdModel(qxmbm);
                        j++;

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("添加模板失败，请重新添加！");
                    }
                    if (j > 0)
                    {
                        MessageBox.Show("添加模板成功！");
                        textBox2.Clear();
                        Bind_cmbQXGL();
                        Bind_cmbModel();
                    }
                }
            }
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinddgvModel(cmbModel.Text);
        }

        private void cmbQXGL_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinddgvQXGL(cmbQXGL.Text);
            txtQXName.Text = "";
            txtQXnum.Text = "";
            button1.Text = "增加器械";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            if (dgvQXGL.SelectedRows[0].Cells["id1"].Value != null)
            {
                int id = int.Parse(dgvQXGL.SelectedRows[0].Cells["id1"].Value.ToString());
                int a = DAL.deleteQxmc(id);
                if (a > 0)
                {
                    MessageBox.Show("删除成功");
                    BinddgvQXGL(cmbQXGL.Text);
                }

            }
            else
                MessageBox.Show("请选择有效数据");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除器械包，包下的器械同样会被删除是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int dt = at.DeleteqxqdModel1(cmbModel.Text);
                int dt1 = at.DeleteqxModelName(cmbModel.Text);
                if (dt > 0 && dt1 > 0)
                {
                    MessageBox.Show("删除成功！");
                    Bind_cmbModel();
                    BinddgvModel(cmbModel.Text);
                }
            }
        }
    }
}
