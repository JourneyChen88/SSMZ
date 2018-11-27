using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class LockSet : Form
    {
        public LockSet()
        {
            InitializeComponent();
        }
        int MinitsTime = 0;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public Timer timer11 = new Timer();
        public int count = 0; 
        private void btnSetLockTime_Click(object sender, EventArgs e)
        {
           int a= dal.UpdateLockTime(Convert.ToInt32(nudSPJG.Value));
            MinitsTime = (int)nudSPJG.Value;
            timer11.Stop();
            timer11.Start();
            if (a>0)
            {
                MessageBox.Show("锁屏时间设置成功!");
                this.Close();
            }
            
            
        }

        private void LockSet_Load(object sender, EventArgs e)
        {
            DataTable dtLockTime = dal.SelectLockTime();
            nudSPJG.Value = int.Parse(dtLockTime.Rows[0][0].ToString());
            MinitsTime = (int)nudSPJG.Value;
            this.timer11.Interval = 1000;
            this.timer11.Tick += new System.EventHandler(this.timer11_Tick);
        }
        private void timer11_Tick(object sender, EventArgs e)
        {
            if (GetLastInputTime() > MinitsTime * 60 * 1000)
            {
                this.timer11.Stop();
                LogIn f1 = new LogIn();
                f1.ShowDialog();

            }
        }
        #region//当鼠标和键盘静止一段时间后做的事情，使用Timer来控制
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public int cbSize;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public uint dwTime;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        ///
        /// 返回鼠标和键盘静止的时间长度
        /// 当鼠标和键盘静止一段时间后做的事情，使用Timer来控制
        ///
        ///
        public static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }
        #endregion 

    }
}
