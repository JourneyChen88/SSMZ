﻿///************************************
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

namespace main.OrgBusinessManage
{
    public partial class EmployeesList : Form
    {
        AdimsController adimsController = new AdimsController();

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmployeesList()
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
        private void EmployeesList_Load(object sender, EventArgs e)
        {
            DataTable dt = adimsController.GetEmployeesList(" 1=1 ");
            this.dgvEmployees.DataSource = dt.DefaultView;
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeesEdit frmEmployeesEdit = new EmployeesEdit("");
            if (frmEmployeesEdit.ShowDialog() == DialogResult.OK)
            {
                EmployeesList_Load(null, null);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                string employeesID = Convert.ToString(this.dgvEmployees.SelectedRows[0].Cells["EmployeesID"].Value);
                int result = adimsController.DeleteEmployees(employeesID);
                if (MessageBox.Show("你确定要删除该记录?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    if (result > 0)
                    {
                        MessageBox.Show("删除排班信息成功！");
                        EmployeesList_Load(null, null);
                    }
                    else
                        MessageBox.Show("删除排班信息失败！");
                }
            }
            MessageBox.Show("无记录可删");
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string employeesID = Convert.ToString(this.dgvEmployees.SelectedRows[0].Cells["EmployeesID"].Value);
            EmployeesEdit frmEmployeesEdit = new EmployeesEdit(employeesID);
            if (frmEmployeesEdit.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("修改排班信息成功！");
                EmployeesList_Load(null, null);
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion


    }
}
