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
using System.Globalization;
using adims_Utility;
using adims_DAL.Dics;

namespace main.科室事物管理
{
    public partial class QJJBRecorder : Form
    {
        UserDal _UserDal = new UserDal();
        private AdimsController adimsController = new AdimsController();
        private DataTable dtStaff = null;
        private AdimsProvider dal = new AdimsProvider();
        public QJJBRecorder()
        {
            InitializeComponent();
        }

        private void LBJL_Load(object sender, EventArgs e)
        {            
            this.dtQJStart.Format = DateTimePickerFormat.Custom;
            this.dtQJStart.CustomFormat = "yyyy/MM/dd HH:mm";
            this.dtQJEnd.Format = DateTimePickerFormat.Custom;
            this.dtQJEnd.CustomFormat = "yyyy/MM/dd HH:mm";
            this.BindComBox();
            Bind_dgvJB();
            Bind_dgvQJ();
        }

        #region <<Methods>>

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindComBox()
        {
            dtStaff = _UserDal.GetUserByCondition("type!='0'");
            this.cmbNo.DataSource = dtStaff;
            this.cmbNo.DisplayMember = "uid";
            this.cmbName.DataSource = dtStaff;
            this.cmbName.DisplayMember = "user_name";
            this.cmbNo1.DataSource = dtStaff;
            this.cmbNo1.DisplayMember = "uid";
            this.cmbName1.DataSource = dtStaff;
            this.cmbName1.DisplayMember = "user_name";
        }
        /// <summary>
        /// 获取请假信息
        /// </summary>
        private void Bind_dgvJB()
        {
            DataTable dt = dal.GetJBJLList(" 1=1 ");
            if (dt.Rows.Count != 0)
            {
                dgvJB.DataSource = dt;
            }        
        }
        private void Bind_dgvQJ()
        {
            DataTable dt = dal.GetLeaveRegistrationList(" 1=1 ");
            if (dt.Rows.Count != 0)
            {
                dgvQJ.DataSource = dt;
            }

        }
        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue()
        {
            string ID = Convert.ToString(dgvJB.SelectedRows[0].Cells["ID"].Value);
            if (string.IsNullOrEmpty(ID)) return;
            this.cmbNo.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["EmployeesNO"].Value);
            this.cmbName.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["EmployeesName"].Value);
            this.dtJBDay.Value = Convert.ToDateTime(dgvJB.SelectedRows[0].Cells["JBDay"].Value);
            this.dtJBDay.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["JBDay"].Value);
            this.tbJBksTime.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["startTime"].Value);
            this.tbJBjsTime.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["endTime"].Value);
            this.tbJBReason.Text = Convert.ToString(dgvJB.SelectedRows[0].Cells["JBReason"].Value);
        }
        /// <summary>
        ///验证
        /// </summary>
        private bool ValiDateLeaveJBJL()
        {
            if (string.IsNullOrEmpty(tbJBksTime.Text.Trim()))
            {
                MessageBox.Show("请填写开始时间！");
                tbJBksTime.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(tbJBjsTime.Text.Trim()))
            {
                MessageBox.Show("请填写结束时间！");
                tbJBjsTime.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(tbJBksTime.Text.Trim()))
            {
                MessageBox.Show("开始时间填写有误，请检查！");
                tbJBksTime.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(tbJBjsTime.Text.Trim()))
            {
                MessageBox.Show("结束时间填写有误，请检查！");
                tbJBjsTime.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(tbJBReason.Text.Trim()))
            {
                MessageBox.Show("请填写加班原因！");
                tbJBReason.Focus();
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
            if (Convert.ToDouble(tbJBjsTime.Text) <= Convert.ToDouble(tbJBksTime.Text))
            {
                MessageBox.Show("结束时间必须大于开始时间。");
                return;
            }
            try
            {
                switch (option)
                {
                    case "Added":
                        leaveList.Add(this.cmbNo.Text);
                        leaveList.Add(this.cmbName.Text);
                        leaveList.Add(Convert.ToString(this.dtJBDay.Value));
                        leaveList.Add(this.tbJBksTime.Text);
                        leaveList.Add(this.tbJBjsTime.Text);
                        leaveList.Add(this.tbJBReason.Text);
                        result = adimsController.InsertJBJL(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("新增成功！");
                            Bind_dgvJB();
                        }
                        else
                            MessageBox.Show("新增失败！");
                        break;
                    case "Modified":
                        leaveList.Add(this.cmbNo.Text);
                        leaveList.Add(this.cmbName.Text);
                        leaveList.Add(Convert.ToString(this.dtJBDay.Value));
                        leaveList.Add(this.tbJBksTime.Text);
                        leaveList.Add(this.tbJBjsTime.Text);
                        leaveList.Add(this.tbJBReason.Text);
                        leaveList.Add(Convert.ToString(this.dgvJB.SelectedRows[0].Cells["ID"].Value));
                        result = adimsController.UpdateJBJL(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("修改成功！");
                            Bind_dgvJB();
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



        #endregion 

        #region <<单击事件>>


        private void dgvJBJL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJB.RowCount == 0) return;
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
            if (dgvJB.RowCount == 0) return;
            SaveJBJL(Convert.ToString(DataRowState.Modified));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJB.RowCount == 0) return;
            string ID = Convert.ToString(this.dgvJB.SelectedRows[0].Cells["ID"].Value);
            int result = adimsController.DeleteJBJL(ID);
            if (MessageBox.Show("你确定要删除该记录?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (result > 0)
                {
                    MessageBox.Show("删除成功！");
                    Bind_dgvJB();
                }
                else
                    MessageBox.Show("删除失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbJBjsTime.Text = null;
            tbJBksTime.Text = null;
            tbJBReason.Text = null;
        }

        #endregion
        private int IsInJQ()//判断是否请假
        {
            string bianhao = cmbNo.Text;
            string year1 = dtQJStart.Value.Year.ToString();
            string month1 = dtQJStart.Value.Month.ToString();
            DateTime dt1 = dtQJStart.Value;
            DateTime dt2 = dtQJEnd.Value;
            DataTable dtable = dal.SlectInHoliday(bianhao, dt1, dt1);
            int i = Convert.ToInt32(dtable.Rows[0][0]);
            return i;

        }
        private void button5_Click(object sender, EventArgs e)
        {
                int i = this.IsInJQ();
                if (i == 0)
                {
                    SaveLeaveRegistration(Convert.ToString(DataRowState.Added));
                }
                else
                    MessageBox.Show("请假日期不能重复");
           
        }
        private void GetLeaveRegistrationList()
        {
            DataTable dt = adimsController.GetLeaveRegistrationList(" 1=1 ");
            if (dt.Rows.Count != 0)
            {
                dgvQJ.DataSource = dt;
                dgvLeaveRegistration_CellDoubleClick(null, null);
            }
        }
         private bool ValiDateLeaveRegistration()
        {
            if (string.IsNullOrEmpty(this.tbSumDay.Text.Trim()))
            {
                MessageBox.Show("请填写请假原因！");
                tbQJreason.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(tbQJreason.Text.Trim()))
            {
                MessageBox.Show("请填写请假原因！");
                tbQJreason.Focus();
                return false;
            }
            return true;
        }
        private void SaveLeaveRegistration(string option)
        {
            if (!ValiDateLeaveRegistration()) return;
            List<string> leaveList = new List<string>();
            int result = 0;
            if (Convert.ToDouble(this.dtQJEnd.Value.ToString("yyyyMMddHHmm")) <= Convert.ToDouble(dtQJStart.Value.ToString("yyyyMMddHHmm")))
            {
                MessageBox.Show("结束时间必须大于开始时间。");
                return;
            }
            try
            {
                switch (option)
                {
                    case "Added":
                        leaveList.Add(this.cmbNo1.Text);
                        leaveList.Add(this.cmbName1.Text);
                        leaveList.Add(this.dtQJStart.Value.ToString("yyyy/MM/dd HH:mm"));
                        leaveList.Add(this.dtQJEnd.Value.ToString("yyyy/MM/dd HH:mm"));
                        leaveList.Add(this.tbSumDay.Text);
                        leaveList.Add(this.tbQJreason.Text);
                        result = dal.InsertLeaveRegistration(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("新增成功！");
                            GetLeaveRegistrationList();
                        }
                        else
                            MessageBox.Show("新增失败！");
                        break;
                    case "Modified":
                        leaveList.Add(this.cmbNo.Text);
                        leaveList.Add(this.cmbName.Text);
                        leaveList.Add(this.dtQJStart.Value.ToString("yyyy/MM/dd HH:mm"));
                        leaveList.Add(this.dtQJEnd.Value.ToString("yyyy/MM/dd HH:mm"));
                        leaveList.Add(this.tbSumDay.Text);
                        leaveList.Add(this.tbQJreason.Text);
                        leaveList.Add(Convert.ToString(this.dgvQJ.SelectedRows[0].Cells["LeaveRegistrationID"].Value));
                        result = dal.UpdateLeaveRegistration(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("修改成功！");
                            GetLeaveRegistrationList();
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
        private void button4_Click(object sender, EventArgs e)
        {
            int i = this.IsInJQ();
            if (dgvQJ.RowCount == 0)
            {
                return;
            }
            else if (i == 0)
            {
                SaveLeaveRegistration(Convert.ToString(DataRowState.Modified));
            }
        }

        private void dgvLeaveRegistration_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQJ.RowCount == 0) return;
            SetEditValue1();
        }
        
        private void SetEditValue1()
        {
            string leaveRegistrationID = Convert.ToString(dgvQJ.SelectedRows[0].Cells["LeaveRegistrationID"].Value);
            if (string.IsNullOrEmpty(leaveRegistrationID)) return;
            this.cmbNo.Text = Convert.ToString(dgvQJ.SelectedRows[0].Cells["QJno"].Value);
            this.cmbName.Text = Convert.ToString(dgvQJ.SelectedRows[0].Cells["QJname"].Value);
            dtQJStart.Value = Convert.ToDateTime(dgvQJ.SelectedRows[0].Cells["QJstart"].Value);
            dtQJEnd.Value = Convert.ToDateTime(dgvQJ.SelectedRows[0].Cells["QJend"].Value);
            this.tbSumDay.Text = Convert.ToString(dgvQJ.SelectedRows[0].Cells["SumDay"].Value);
            this.tbQJreason.Text = Convert.ToString(dgvQJ.SelectedRows[0].Cells["LeaveReason"].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvQJ.RowCount == 0) return;
            string leaveRegistrationID = Convert.ToString(this.dgvQJ.SelectedRows[0].Cells["LeaveRegistrationID"].Value);
            int result = dal.DeleteLeaveRegistration(leaveRegistrationID);
            if (MessageBox.Show("你确定要删除该记录?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (result > 0)
                {
                    MessageBox.Show("删除成功！");
                    GetLeaveRegistrationList();
                }
                else
                    MessageBox.Show("删除失败！");
            }
        }

        private void txtSumDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

       

    }
}
