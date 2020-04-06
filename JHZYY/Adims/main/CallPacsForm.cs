using Adims_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class CallPacsForm : Form
    {
        public CallPacsForm(string patid, int PatIdType)
        {

            InitializeComponent();
            tbPatId.Text = patid;
            cmbType.SelectedIndex = PatIdType - 1;
            cmbPictureType.SelectedIndex = 0;
        }
        /// <summary>
        /// 初始化PACS,返回值，0失败，1成功
        /// </summary>
        /// <returns></returns>
        [DllImport(@"\joint.dll")]
        public static extern int PacsInitialize();

        /// <summary>
        /// 关闭PACS
        /// </summary>
        [DllImport(@"\joint.dll")]
        public static extern void PacsRelease();

        /// <summary>
        /// 显示PACS信息，返回值，0失败，1成功
        /// </summary>
        /// <param name="nPatientType">病人类型，1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号</param>
        /// <param name="lpszID">病人ID</param>
        /// <param name="nImageType">图像类型，1 图像 ， 2 报告</param>
        /// <returns></returns>
        [DllImport(@"\joint.dll")]

        public static extern int PacsView(int nPatientType, string lpszID, int nImageType);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //[DllImport(@"D:\HeYi\CallPacs\CallPacs\bin\Debug\Display\PacsQueryDBAccess.dll")]
        //public static extern int PacsView2(int nPatientType, string lpszID, int nImageType);
        private void buttonCall_Click(object sender, EventArgs e)
        {
            if (tbPatId.Text.IsNullOrEmpty())
            {
                MessageBox.Show("编号不能为空");
                return;
            }
            try
            {
                int res = PacsView(cmbType.SelectedIndex + 1, tbPatId.Text, cmbPictureType.SelectedIndex + 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void CallForm_Load(object sender, EventArgs e)
        {

            try
            {
                int result = PacsInitialize();
                if (result != 1)
                {
                    MessageBox.Show("连接Pcas失败！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void CallForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PacsRelease();
        }
    }
}
