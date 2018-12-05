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
using adims_DAL.Dics;

namespace main.科室事物管理
{
    public partial class EmployeesEdit : Form
    {
        #region <<Members>>

        UserDal _UserDal = new UserDal();
        private string employeesID = string.Empty;
        AdimsController adimsController = new AdimsController();
        AdimsProvider apro = new AdimsProvider();

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="f"></param>
        public EmployeesEdit(string employeesID)
        {
            InitializeComponent();
            this.employeesID = employeesID;
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeesEdit_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            dtTime.Text = Convert.ToString(DateTime.Now);
            try
            {
                if (!string.IsNullOrEmpty(employeesID))
                {
                    DataTable dtEmployees = adimsController.GetEmployeesList(" EmployeesID = '" + employeesID + "'");
                    SetEditValue(dtEmployees);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "加载出现异常，请检查！");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployees();
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

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveEmployees()
        {
            try
            {
                int result = 0;
                if (!string.IsNullOrEmpty(employeesID)) // 修改
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("AnesthesiaDoctor", cmbAnesthesiaDoctor.Text.Trim());
                    dictionary.Add("Nurse", cmbNurse.Text.Trim());
                    dictionary.Add("Divisions", cmbDivisions.Text.Trim());
                    dictionary.Add("Time", dtTime.Text);
                    dictionary.Add("EmployeesID", employeesID);
                    DataTable dt = apro.GetIsEmployeesList(cmbDivisions.Text, dtTime.Text);
                    int i = dt.Rows.Count;
                    if (i > 0)
                    {
                        MessageBox.Show("当前时段排班已存在！");
                    }
                    else
                    result = adimsController.UpdateEmployees(dictionary);
                }
                else  // 新增
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("AnesthesiaDoctor", cmbAnesthesiaDoctor.Text.Trim());
                    dictionary.Add("Nurse", cmbNurse.Text.Trim());
                    dictionary.Add("Divisions", cmbDivisions.Text.Trim());
                    dictionary.Add("Time", dtTime.Text);
                    DataTable dt= apro.GetIsEmployeesList(cmbDivisions.Text, dtTime.Text);
                    int i = dt.Rows.Count;
                    if (i>0)
                    {
                        MessageBox.Show("当前时段排班已存在！");
                    }
                    else
                        result = adimsController.InsertEmployees(dictionary);
                }
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("保存失败，请检查！ ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        /// <summary>
        /// 控件赋值
        /// </summary>
        /// <param name="dt"></param>
        private void SetEditValue(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            cmbAnesthesiaDoctor.Text = Convert.ToString(dr["AnesthesiaDoctor"]);
            cmbNurse.Text = Convert.ToString(dr["Nurse"]);
            cmbDivisions.Text = Convert.ToString(dr["Divisions"]);
            dtTime.Text = Convert.ToString(dr["Time"]);
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dt = _UserDal.GetUserByCondition(" 1=1 ");
            string[] medicalNameStr = (from q in dt.Select()
                                       where Convert.ToInt32(q["PostType"]) == 0
                                       select Convert.ToString(q["MedicalName"])).ToArray();
            cmbAnesthesiaDoctor.Items.AddRange(medicalNameStr);
            string[] medicalNameArray = (from q in dt.Select()
                                         where Convert.ToInt32(q["PostType"]) != 0
                                         select Convert.ToString(q["MedicalName"])).ToArray();
            cmbNurse.Items.AddRange(medicalNameArray);
        }

        #endregion
    }
}
