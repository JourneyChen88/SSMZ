///************************************
///Updated By        : Senvi
///************************************

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
using System.Threading;
using System.Globalization;

namespace main.科室事物管理
{
    public partial class LeaveRegistration : Form
    {
        #region <<Members>>

        private AdimsController adimsController = new AdimsController();
        private AdimsProvider apro = new AdimsProvider();
        private DataTable dtStaff = null;

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public LeaveRegistration()
        {
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaveRegistration_Load(object sender, EventArgs e)
        {
          
            BindLookUpEdit();
            try
            {
                GetLeaveRegistrationList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "窗体加载出现异常，请检查！");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtStartTime.Text) <= 30)
            {
                
                int i = this.IsInJQ();
                if (i == 0)
                {
                    SaveLeaveRegistration(Convert.ToString(DataRowState.Added));

                }
                else
                {
                    
                    MessageBox.Show("请假日期不能重复");
                }
            
            }
            else MessageBox.Show("请假天数不能超过三十天");
           
        }
        private int IsInJQ()//判断是否请假
        {
            string bianhao = cmbEmployeesNO.Text;
            //DateTime DT1 = DateTime.ParseExact(dtStartDate.Value.Year, "yyyyMMdd", Thread.CurrentThread.CurrentCulture);
            string year1 = dtStartDate.Value.Year.ToString();
            string month1 = dtStartDate.Value.Month.ToString();
            int dt1 =Convert.ToInt32( dtStartDate.Value.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            int dt2 = Convert.ToInt32(dtEndDate.Value.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            //string day1 = dtStartDate.Value.Day.ToString();
            //int dt1 = Convert.ToInt32(year1 + month1 + day1);
            //string year2 = dtEndDate.Value.Year.ToString();
            //string month2 = dtEndDate.Value.Month.ToString();
            //string day2 = dtEndDate.Value.Day.ToString();
            //int dt2 = Convert.ToInt32(year2 + month2 + day2);

            DataTable dtable = apro.SlectInHoliday(bianhao, dt1, dt1);
            int i = Convert.ToInt32(dtable.Rows[0][0]);
            
            return i;
 
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i = this.IsInJQ();
            if (dgvLeaveRegistration.RowCount == 0)
            {
                return;
            }
            else if (i == 0)
            {
                SaveLeaveRegistration(Convert.ToString(DataRowState.Modified));
            }
           
        }

        /// <summary>
        ///删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLeaveRegistration.RowCount == 0) return;
            string leaveRegistrationID = Convert.ToString(this.dgvLeaveRegistration.SelectedRows[0].Cells["LeaveRegistrationID"].Value);
            int result = adimsController.DeleteLeaveRegistration(leaveRegistrationID);
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

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLeaveRegistration_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLeaveRegistration.RowCount == 0) return;
            SetEditValue();
        }

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 获取请假信息
        /// </summary>
        private void GetLeaveRegistrationList()
        {
            DataTable dt = adimsController.GetLeaveRegistrationList(" 1=1 ");
            if (dt.Rows.Count != 0)
            {
                dgvLeaveRegistration.DataSource = dt;
                dgvLeaveRegistration_CellDoubleClick(null, null);
            }
        }

        /// <summary>
        ///验证
        /// </summary>
        private bool ValiDateLeaveRegistration()
        {
            if (string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                MessageBox.Show("请填写请假天数！");
                txtStartTime.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtStartTime.Text.Trim()))
            {
                MessageBox.Show("请假天数填写有误，请检查！");
                txtStartTime.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLeaveReason.Text.Trim()))
            {
                MessageBox.Show("请填写请假原因！");
                txtLeaveReason.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 绑定数据源
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
        /// 控件赋值
        /// </summary>
        private void SetEditValue()
        {
            string leaveRegistrationID = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["LeaveRegistrationID"].Value);
            if (string.IsNullOrEmpty(leaveRegistrationID)) return;
            this.cmbEmployeesNO.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["EmployeesNO"].Value);
            this.cmbEmployeesName.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["EmployeesName"].Value);
           
             string str1= dgvLeaveRegistration.SelectedRows[0].Cells["StartDate"].Value.ToString();
             this.dtStartDate.Value = DateTime.ParseExact(str1, "yyyyMMdd", null);

;


            this.txtStartTime.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["StartTime"].Value);
            string str2 = dgvLeaveRegistration.SelectedRows[0].Cells["EndDate"].Value.ToString();
            this.dtEndDate.Value = DateTime.ParseExact(str2, "yyyyMMdd", null);
            this.txtEndTime.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["EndTime"].Value);
            this.txtSumDay.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["SumDay"].Value);
            this.txtLeaveReason.Text = Convert.ToString(dgvLeaveRegistration.SelectedRows[0].Cells["LeaveReason"].Value);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="option"></param>
        private void SaveLeaveRegistration(string option)
        {
            if (!ValiDateLeaveRegistration()) return;
            List<string> leaveList = new List<string>();
            int result = 0;
            try
            {
                switch (option)
                {
                    case "Added":
                                            
                        leaveList.Add(this.cmbEmployeesNO.Text);
                        leaveList.Add(this.cmbEmployeesName.Text);
                        leaveList.Add(this.dtStartDate.Value.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo));
                        leaveList.Add(this.txtStartTime.Text);
                        leaveList.Add(this.dtStartDate.Value.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo));
                        leaveList.Add(this.txtEndTime.Text);
                        leaveList.Add(this.txtSumDay.Text);
                        leaveList.Add(this.txtLeaveReason.Text);
                        result = adimsController.InsertLeaveRegistration(leaveList);
                        if (result > 0)
                        {
                            MessageBox.Show("新增成功！");
                            GetLeaveRegistrationList();
                        }
                        else
                            MessageBox.Show("新增失败！");
                        break;
                    case "Modified":
                        leaveList.Add(this.cmbEmployeesNO.Text);
                        leaveList.Add(this.cmbEmployeesName.Text);
                        leaveList.Add(this.dtStartDate.Value.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo));
                        leaveList.Add(this.txtStartTime.Text);
                        leaveList.Add(this.dtStartDate.Value.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo));
                        //leaveList.Add(this.txtEndTime.Text);
                        leaveList.Add(this.txtEndTime.Text);
                        leaveList.Add(this.txtSumDay.Text);
                        leaveList.Add(this.txtLeaveReason.Text);
                        leaveList.Add(Convert.ToString(this.dgvLeaveRegistration.SelectedRows[0].Cells["LeaveRegistrationID"].Value));
                        result = adimsController.UpdateLeaveRegistration(leaveList);
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
       
        #endregion

       
    }
}