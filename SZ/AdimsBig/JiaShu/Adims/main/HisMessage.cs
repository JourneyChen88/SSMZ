using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace main
{
    /// <summary>
    /// Message,请透过我来统一message窗口的标准...
    /// </summary>
    public class HisMessage
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;
        
        /// <summary>
        /// 跳出错误Message窗口
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        public static void ShowError(string MessageText)
        {
            MessageBox.Show(MessageText, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 跳出提示消息框
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        public static void ShowHint(string MessageText)
        {
            MessageBox.Show(MessageText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 跳出警告消息框
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        /// <returns>对话框的传回值</returns>
        public static void ShowWarning(string MessageText)
        {
            MessageBox.Show(MessageText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 跳出疑问消息框
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        /// <returns>对话框的传回值</returns>
        public static bool ShowQuestion(string MessageText)
        {
            return MessageBox.Show(MessageText, "问题", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes);
        }

        /// <summary>
        /// 跳出疑问消息框(YesNoCancel)
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        /// <returns>对话框的传回值DialogResult</returns>
        public static DialogResult ShowConfirm(string MessageText)
        {
            return MessageBox.Show(MessageText, "问题", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button3);
            
        }

        /// <summary>
        ///  跳出提示Message窗口 (计时关闭)
        /// </summary>
        /// <param name="MessageText">讯息内容</param>
        /// <param name="TimeoutMillsec">关闭时限</param>
        public static void ShowHintByTime(string MessageText, int TimeoutMillsec)
        {
            StartKiller(TimeoutMillsec);//设定关闭时间
            MessageBox.Show(MessageText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void StartKiller(int TimeoutMillsec)
        {
            Timer timer = new Timer();
            timer.Interval = TimeoutMillsec;//设定时间 (7秒 = 7000 TimeoutMillsec)
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            //停止定时器
            ((Timer)sender).Stop();
        }
        private static void KillMessageBox()
        {
            //找出MessageBox的跳出窗口。(找寻相同的标题名称)
            IntPtr ptr = FindWindow(null, "提示");
            if (ptr != IntPtr.Zero)
            {
                //找到就关掉它
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        






    }
    
}