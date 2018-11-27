using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main.OrgBusinessManage
{
    public partial class JBJL : Form
    {
        private AdimsController adimsController = new AdimsController();
        private DataTable dtStaff = null;
        public JBJL()
        {
            InitializeComponent();
        }

        private void LBJL_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            datagridBind();
        }

        #region <<Methods>>

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindLookUpEdit()
        {
            dtStaff = adimsController.GetSurgeryStaff(" 1=1 ");
            this.cmbEmployeesNO.DataSource = dtStaff;
            this.cmbEmployeesNO.DisplayMember = "MedicalNO";

            this.cmbEmployeesName.DataSource = dtStaff;
            this.cmbEmployeesName.DisplayMember = "MedicalName";
        }
        /// <summary>
        /// 获取请假信息
        /// </summary>
        private void datagridBind()
        {
            DataTable dt = adimsController.GetJBJLList(" 1=1 ");
            if (dt.Rows.Count != 0)
            {
                dgvJBJL.DataSource = dt;
                dgvJBJL_CellDoubleClick(null, null);
            }
        
        }
        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue()
        {
            string ID = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["ID"].Value);
            if (string.IsNullOrEmpty(ID)) return;
            this.cmbEmployeesNO.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["EmployeesNO"].Value);
            this.cmbEmployeesName.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["EmployeesName"].Value);
            this.dtJBDay.Value = Convert.ToDateTime(dgvJBJL.SelectedRows[0].Cells["JBDay"].Value);
            this.dtJBDay.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["JBDay"].Value);
            this.TBstartTime.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["startTime"].Value);
            this.TBendTime.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["endTime"].Value);
            this.txtJBReason.Text = Convert.ToString(dgvJBJL.SelectedRows[0].Cells["JBReason"].Value);
        }
        /// <summary>
        ///验证
        /// </summary>
        private bool ValiDateLeaveJBJL()
        {
            if (string.IsNullOrEmpty(TBstartTime.Text.Trim()))
            {
                MessageBox.Show("请填写开始时间！");
                TBstartTime.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TBendTime.Text.Trim()))
            {
                MessageBox.Show("请填写结束时间！");
                TBendTime.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(TBstartTime.Text.Trim()))
            {
                MessageBox.Show("开始时间填写有误，请检查！");
                TBstartTime.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(TBendTime.Text.Trim()))
            {
                MessageBox.Show("结束时间填写有误，请检查！");
                TBendTime.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtJBReason.Text.Trim()))
            {
                MessageBox.Show("请填写加班原因！");
                txtJBReason.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="option"></param>
        private void SaveJBJL(string option)
        {
            if (!ValiDateLeaveJBJL()) return;
            List<string> leaveList = new List<string>();
            int result = 0;
            try
            {
                switch (option)
                {
                    case "Added":
                        leaveList.Add(this.cmbEmployeesNO.Text);
                        leaveList.Add(this.cmbEmployeesName.Text);
                        leaveList.Add(Convert.ToString(this.dtJBDay.Value));
                        leaveList.Add(this.TBstartTime.Text);
                        leaveList.Add(this.TBendTime.Text);
                        leaveList.Add(this.txtJBReason.Text);
                        result = adimsController.InsertJBJL(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("新增成功！");
                            datagridBind();
                        }
                        else
                            MessageBox.Show("新增失败！");
                        break;
                    case "Modified":
                        leaveList.Add(this.cmbEmployeesNO.Text);
                        leaveList.Add(this.cmbEmployeesName.Text);
                        leaveList.Add(Convert.ToString(this.dtJBDay.Value));
                        leaveList.Add(this.TBstartTime.Text);
                        leaveList.Add(this.TBendTime.Text);
                        leaveList.Add(this.txtJBReason.Text);
                        leaveList.Add(Convert.ToString(this.dgvJBJL.SelectedRows[0].Cells["ID"].Value));
                        result = adimsController.UpdateJBJL(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("修改成功！");
                            datagridBind();
                        }
                        else
                            MessageBox.Show("修改失败！");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "异常抛出，请检查！");
            }
        }


        ///// <summary>
        ///// 绑定数据源
        ///// </summary>
        //private void BindLookUpEdit()
        //{
        //    dtStaff = adimsController.GetSurgeryStaff(" 1=1 ");
        //    this.cmbEmployeesNO.DataSource = dtStaff;
        //    this.cmbEmployeesNO.DisplayMember = "MedicalNO";

        //    this.cmbEmployeesName.DataSource = dtStaff;
        //    this.cmbEmployeesName.DisplayMember = "MedicalName";
        //}


        #endregion 

        #region <<单击事件>>


        private void dgvJBJL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJBJL.RowCount == 0) return;
            SetEditValue();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SaveJBJL(Convert.ToString(DataRowState.Added));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvJBJL.RowCount == 0) return;
            SaveJBJL(Convert.ToString(DataRowState.Modified));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJBJL.RowCount == 0) return;
            string ID = Convert.ToString(this.dgvJBJL.SelectedRows[0].Cells["ID"].Value);
            int result = adimsController.DeleteJBJL(ID);
            if (MessageBox.Show("你确定要删除该记录?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (result > 0)
                {
                    MessageBox.Show("删除成功！");
                    datagridBind();
                }
                else
                    MessageBox.Show("删除失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TBendTime.Text = null;
            TBstartTime.Text = null;
            txtJBReason.Text = null;
        }

        #endregion

    }
}
