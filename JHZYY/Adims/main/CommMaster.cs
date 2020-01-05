using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main
{
    public partial class CommMaster : Form
    {
        #region <<Members>>

        public string values = string.Empty;
        private string dtName = string.Empty;

        AdimsController adimsController = new AdimsController();

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dtName"></param>
        public CommMaster(string dtName)
        {
            if (!string.IsNullOrEmpty(dtName))
                this.dtName = dtName;
            InitializeComponent();
        }
        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(dtName))
                {
                    DataTable dt = adimsController.GetMasterList(dtName);
                    grdMaster.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                grdvMaster.CloseEditor();
                grdvMaster.UpdateCurrentRow();
                DataRow[] drs = (grdMaster.DataSource as DataTable).Select("SelectRowState = '1'");
                foreach (DataRow dr in drs)
                {
                    if (!string.IsNullOrEmpty(values))
                        values += "，" + Convert.ToString(dr["Name"]);
                    else
                        values += Convert.ToString(dr["Name"]);
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
