using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_MODEL;
using adims_BLL;

namespace main.MZJLDs
{
    public partial class ConsumablesUseForm : Form
    {
        ConsumablesUseDal dal = new ConsumablesUseDal();
        int MzjldId;
        string PatId;
        public ConsumablesUseForm(int mzjldid, string patid)
        {
            InitializeComponent();
            MzjldId = mzjldid;
            PatId = patid;
        }


        private void BindGridView()
        {
            DataTable dt = dal.GetByMzjldId(MzjldId);
            if (dt.Rows.Count == 0)
            {
                DataTable dtCon = dal.GetConsumablesAll();
                foreach (DataRow dr in dtCon.Rows)
                {
                    ConsumablesUseModel cu = new ConsumablesUseModel();
                    cu.MzjldId = MzjldId;
                    cu.PatId = PatId;
                    cu.Name = Convert.ToString(dr["Name"]);
                    cu.Dosage = 0;
                    cu.Price = UserFunction.ToDouble(dr["Price"]);
                    cu.Unit = Convert.ToString(dr["Unit"]);
                    cu.IsCost = 1;
                    dal.Insert(cu);
                }
                dt = dal.GetByMzjldId(MzjldId);
                this.dgvUseList.DataSource = dt;
            }
            else
            {
                this.dgvUseList.DataSource = dt;
            }


        }

        ComboBox cmbName = new ComboBox();
        ComboBox cmbUnit = new ComboBox();
        private void ConsumablesUseForm_Load(object sender, EventArgs e)
        {


            DataTable dt = dal.GetConsumablesAll();
            cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i][1]);
            }
            cmbUnit.Items.Add("10ml");
            cmbUnit.Items.Add("个");
            cmbUnit.Items.Add("ml");
            cmbName.TextChanged += new EventHandler(cmbName_Changed);
            cmbUnit.TextChanged += new EventHandler(cmbUnit_Changed);
            //为生成的组合框控件，添加文本改变事件处理函数
            dgvUseList.Controls.Add(cmbName); //把组合框添加到dataGridView1中
            dgvUseList.Controls.Add(cmbUnit);
            cmbName.Visible = false;
            cmbUnit.Visible = false;
            BindGridView();

        }

        private void cmbName_Changed(object sender, EventArgs arg)
        {
            dgvUseList.CurrentRow.Cells["Name"].Value = cmbName.Text;
            cmbName.Visible = false;
        }
        private void cmbUnit_Changed(object sender, EventArgs arg)
        {
            dgvUseList.CurrentRow.Cells["Unit"].Value = cmbUnit.Text;
            cmbUnit.Visible = false;
        }


        private void tbDosage_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int res = 0;
            foreach (DataGridViewRow dr in dgvUseList.Rows)
            {
                ConsumablesUseModel cu = new ConsumablesUseModel();
                cu.MzjldId = MzjldId;
                cu.PatId = PatId;
                cu.Id = UserFunction.ToInt32(dr.Cells["Id"].Value);
                cu.Name = Convert.ToString(dr.Cells["Name"].Value);
                cu.Dosage = UserFunction.ToInt32(dr.Cells["Dosage"].Value);
                cu.Price = UserFunction.ToDouble(dr.Cells["Price"].Value);
                cu.Unit = Convert.ToString(dr.Cells["Unit"].Value);
                cu.IsCost = UserFunction.ToInt32(dr.Cells["IsCost"].Value);
                res = dal.Update(cu);
                res += 1;
            }

            if (res == 0)
            {
                MessageBox.Show("保存失败！");

            }

            else
            {
                MessageBox.Show("保存成功！");
                BindGridView();
            }

        }





        private void btnAdd_Click(object sender, EventArgs e)
        {
            //dgvUseList.Rows.Add(1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认是否删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvUseList.CurrentRow.Cells["Id"].Value);
                int res = dal.Delete(id);
                if (res == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }

                else
                    BindGridView();
            }
        }

        private void dgvUseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUseList.Rows.Count > 0)
            {
                if (e.ColumnIndex == 1)
                {
                    cmbUnit.Visible = false;
                    Rectangle rect = dgvUseList.GetCellDisplayRectangle(dgvUseList.CurrentCell.ColumnIndex, dgvUseList.CurrentCell.RowIndex, false);
                    cmbName.Left = rect.Left;
                    cmbName.Top = rect.Top;
                    cmbName.Width = rect.Width;
                    cmbName.Height = rect.Height;
                    cmbName.Visible = true;
                    cmbName.DroppedDown = true;
                }
                else if (e.ColumnIndex == 4)
                {
                    cmbName.Visible = false;
                    Rectangle rect = dgvUseList.GetCellDisplayRectangle(dgvUseList.CurrentCell.ColumnIndex, dgvUseList.CurrentCell.RowIndex, false);
                    cmbUnit.Left = rect.Left;
                    cmbUnit.Top = rect.Top;
                    cmbUnit.Width = rect.Width;
                    cmbUnit.Height = rect.Height;
                    cmbUnit.Visible = true;
                    cmbUnit.DroppedDown = true;
                }
                else
                {
                    cmbUnit.Visible = false;
                    cmbName.Visible = false;
                }
            }
        }

        private void dgvUseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbUnit.Visible = false;
            cmbName.Visible = false;
        }

        private void dgvUseList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvUseList.CurrentCell.ColumnIndex == 2)
            {
                e.Control.KeyPress += new KeyPressEventHandler(adims_BLL.UserFunction.Text_Value_Limit);
            }
            if (dgvUseList.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress += new KeyPressEventHandler(adims_BLL.UserFunction.Text_Value_Limit);
            }
        }
    }
}

