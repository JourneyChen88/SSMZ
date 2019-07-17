using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using main.UserSecurity;
using main.OrgBusinessManage;
using main.MedicalConsumableManage;
using main.PrivigeManage;
using adims_MODEL;
using adims_BLL;
using adims_DAL;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using main.Statistics;

namespace main
{
    public partial class MainForm : Form, IMessageFilter
    {
        #region <<Members>>
        int MinitsTime = 0;
        adims_DAL.AdimsProvider dal = new AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int xl1 = 0, yl1 = 0, xp1 = 0, yp1 = 0;
        int xl2 = 0, yl2 = 0, xp2 = 0, yp2 = 0;
        int xl3 = 0, yl3 = 0, xp3 = 0, yp3 = 0;
        int xl4 = 0, yl4 = 0, xp4 = 0, yp4 = 0;
        int xl5 = 0, yl5 = 0, xp5 = 0, yp5 = 0;
        int xl6 = 0, yl6 = 0, xp6 = 0, yp6 = 0;
        int xl7 = 0, yl7 = 0, xp7 = 0, yp7 = 0;
        int xl8 = 0, yl8 = 0, xp8 = 0, yp8 = 0;
        int xl9 = 0, yl9 = 0, xp9 = 0, yp9 = 0;


        #endregion
        public Timer timer11 = new Timer();
        public int count = 0;
        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);

        }

        #endregion

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






        #region <<Events>>  




        #region //IMessageFilter 成员,禁止下拉框鼠标滚动


        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522) return true;

            else
                return false;
        }

        #endregion

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);//禁止下拉框鼠标滚动
            xl1 = lab10.Location.X;
            yl1 = lab10.Location.Y;
            xp1 = pictureBox10.Location.X;
            yp1 = pictureBox10.Location.Y;
            xl2 = lab2.Location.X;
            yl2 = lab2.Location.Y;
            xp2 = pictureBox2.Location.X;
            yp2 = pictureBox2.Location.Y;
            xl3 = lab3.Location.X;
            yl3 = lab3.Location.Y;
            xp3 = pictureBox3.Location.X;
            yp3 = pictureBox3.Location.Y;
            xl4 = lab4.Location.X;
            yl4 = lab4.Location.Y;
            xp4 = pictureBox4.Location.X;
            yp4 = pictureBox4.Location.Y;
            xl5 = lab5.Location.X;
            yl5 = lab5.Location.Y;
            xp5 = pictureBox5.Location.X;
            yp5 = pictureBox5.Location.Y;
            xl6 = lab6.Location.X;
            yl6 = lab6.Location.Y;
            xp6 = pictureBox6.Location.X;
            yp6 = pictureBox6.Location.Y;
            xl7 = lab7.Location.X;
            yl7 = lab7.Location.Y;
            xp7 = pictureBox7.Location.X;
            yp7 = pictureBox7.Location.Y;
            xl8 = lab8.Location.X;
            yl8 = lab8.Location.Y;
            xp8 = pictureBox8.Location.X;
            yp8 = pictureBox8.Location.Y;
            xl9 = lab9.Location.X;
            yl9 = lab9.Location.Y;
            xp9 = pictureBox9.Location.X;
            yp9 = pictureBox9.Location.Y;


            /*   DataSet ds = new DataSet();
               ds = bll.yhxinxi(Program.zhanghao);
               Program.yh = ds.Tables[0].Rows[0][0].ToString();
               List<adims_MODEL.quanxian> qx = new List<adims_MODEL.quanxian>();
               bll.quanxian(qx, Convert.ToInt32(ds.Tables[0].Rows[0][1]));
               foreach (adims_MODEL.quanxian t_qx in qx)
               {
                   if (t_qx.Ml1 >= 0)
                       ((ToolStripMenuItem)menuStrip1.Items[t_qx.Ml1]).DropDownItems[t_qx.Ml2].Enabled = false;
                   else
                       menuStrip1.Items[t_qx.Ml2].Enabled = false;

               }  */
            //对不同的用户岗位采用不同的权限
            /*    for (int i = 0; i < menuStrip1.Items.Count; i++)
                {
                    Program.muluz.Add(menuStrip1.Items[i].Text);
                    for (int j = 0; j < ((ToolStripMenuItem)menuStrip1.Items[i]).DropDownItems.Count; j++)
                    {
                        Program.muluz.Add(((ToolStripMenuItem)menuStrip1.Items[i]).DropDownItems[j].Text);

                    }
                }*/
            //将菜单添加到目录表中
            this.Text += "(当前用户为: " + Program.Customer.user_name + ")";
            string jurisdiction = bll.Get_user_jurisdiction(Program.Customer);
            sqxxToolStripMenuItem.Visible = false;
            szxxToolStripMenuItem.Visible = false;
            shxxToolStripMenuItem.Visible = false;
            ypglToolStripMenuItem.Visible = false;
            ygxxToolStripMenuItem.Visible = false;
            sjwhToolStripMenuItem.Visible = false;
            qxglToolStripMenuItem.Visible = false;
            xxcxToolStripMenuItem.Visible = false;
            pictureBox2.Enabled = false;
            lab2.Enabled = false;
            pictureBox3.Enabled = false;
            lab3.Enabled = false;
            pictureBox5.Enabled = false;
            lab5.Enabled = false;
            pictureBox6.Enabled = false;
            lab6.Enabled = false;
            pictureBox8.Enabled = false;
            lab8.Enabled = false;
            pictureBox9.Enabled = false;
            lab9.Enabled = false;
            pictureBox10.Enabled = false;
            lab10.Enabled = false;


            if (jurisdiction.Contains("0"))
            {
                sqxxToolStripMenuItem.Visible = true;
                pictureBox2.Enabled = true; lab2.Enabled = true;
                pictureBox3.Enabled = true; lab3.Enabled = true;
            }
            if (jurisdiction.Contains("1"))
            {
                szxxToolStripMenuItem.Visible = true;
                pictureBox5.Enabled = true; lab5.Enabled = true;
            }
            if (jurisdiction.Contains("2"))
            {
                shxxToolStripMenuItem.Visible = true;
                pictureBox8.Enabled = true;
                lab8.Enabled = true;
                pictureBox9.Enabled = true;
                lab9.Enabled = true;
                pictureBox10.Enabled = true;
                lab10.Enabled = true;
                pictureBox6.Enabled = true;
                lab6.Enabled = true;
            }
            if (jurisdiction.Contains("3")) { ypglToolStripMenuItem.Visible = true; }
            //if (jurisdiction.Contains("4")) { ygxxToolStripMenuItem.Visible = true; }
            if (jurisdiction.Contains("5")) { xxcxToolStripMenuItem.Visible = true; }
            if (jurisdiction.Contains("6")) { sjwhToolStripMenuItem.Visible = true; }
            if (jurisdiction.Contains("7")) { qxglToolStripMenuItem.Visible = true; }
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
                f1.loginCount = 1;
                f1.ShowDialog();

            }
        }
        private void muMzjld_Click(object sender, EventArgs e)
        {
            MzjldSelet smzjld1 = new MzjldSelet();
            smzjld1.Show();
        }

        private void 排班ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaiBanForm formpaiban = new PaiBanForm();
            formpaiban.ShowDialog();
        }

        private void 术前访视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeforeVisit_Select formsqfs = new BeforeVisit_Select();
            formsqfs.ShowDialog();
        }


        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_users users = new Add_users();
            users.Show();
        }

        private void 屏幕锁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fpmsd f = new Fpmsd();
            f.ShowDialog();

        }

        private void 基础数据维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*  Fzlwf f = new Fzlwf();
                f.ShowDialog();
            */
            data_maintenance maintenance = new data_maintenance();
            maintenance.Show();
        }

        private void 科室药物申领ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fksypsl f = new Fksypsl();
            f.ShowDialog();
        }

        private void 药物申领入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frcd f = new Frcd();
            f.ShowDialog();
        }

        private void 手术间药品分发ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fssjypff f = new Fssjypff();
            f.ShowDialog();
        }

        private void 医师药品申领ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fysypsl f = new Fysypsl();
            f.ShowDialog();
        }

        private void 医师药品核销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fysyphx f = new Fysyphx();
            f.ShowDialog();
        }

        private void 药品退科室库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fmzkyptk f = new Fmzkyptk();
            f.ShowDialog();
        }

        private void 科室药品退库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fksyptk f = new Fksyptk();
            f.ShowDialog();
        }

        private void 排班查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeesList frmEmployeesList = new EmployeesList();
            frmEmployeesList.ShowDialog();

        }

        private void 员工排班ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeesEdit frmEmployeesEdit = new EmployeesEdit("");
            frmEmployeesEdit.ShowDialog();
        }

        private void 请假登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LeaveRegistration frmLeaveRegistration = new LeaveRegistration();
            frmLeaveRegistration.ShowDialog();
        }

        private void 手术间浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sjll1 f = new sjll1();
            f.ShowDialog();

        }

        private void 中央监护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FCenterJF f = new FCenterJF();
            f.ShowDialog();
        }

        private void 护理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NurseRecord_Select hlform = new NurseRecord_Select();
            hlform.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qxqd qxqdform = new qxqd();
            qxqdform.ShowDialog();
        }

        private void pACUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PACU_Select f = new PACU_Select();
            f.ShowDialog();
        }

        private void 器械清点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qxqd qxqdform = new qxqd();
            qxqdform.Show();
        }

        private void 麻醉记录单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mzjldQuery mquery = new mzjldQuery();
            mquery.ShowDialog();
        }
        private void 麻醉记录统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSTSTJ ssts = new SSTSTJ();
            ssts.ShowDialog();
        }

        private void 术后随访ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AfterVisit_Select shsfform = new AfterVisit_Select();
            shsfform.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MZZJ_select f = new MZZJ_select();
            f.ShowDialog();
        }

        private void szsjglToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sjll1 sjllform = new sjll1();
            szsjGuanli sjllform = new szsjGuanli();
            sjllform.ShowDialog();
        }

        /// <summary>
        /// 手术管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        SQLiteHelper sh = new SQLiteHelper();
        private void btnManager_Click(object sender, EventArgs e)
        {



            //double a = 1;
            //sh.insertJianCeDataPACU(123, 1, 1, 1, 1, 1, 1, 1, 1, a, DateTime.Now);
        }

        /// <summary>
        /// 紧急手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUrgent_Click(object sender, EventArgs e)
        {
            gbLOCK.Visible = true;
            btnLockSet.Visible = false;
            //pictureBox2.Visible = false;
            //pictureBox3.Visible = false;
            //lab2.Visible = false;
            //lab3.Visible = false;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            lab2.ForeColor = Color.Blue;
            pictureBox2.BackColor = Color.Lavender;
            //pictureBox2.Location = new Point(xp2, yp2 - 15);            
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            lab2.ForeColor = Color.Black;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Location = new Point(xp2, yp2);
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            lab3.ForeColor = Color.Blue;
            pictureBox3.BackColor = Color.Lavender;
            //pictureBox3.Location = new Point(xp3, yp3 - 15);   
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            lab3.ForeColor = Color.Black;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Location = new Point(xp3, yp3);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            lab4.ForeColor = Color.Black;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Location = new Point(xp4, yp4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                bll.inss(menuStrip1.Items[i].Text, -1, i);
                for (int j = 0; j < ((ToolStripMenuItem)menuStrip1.Items[i]).DropDownItems.Count; j++)
                {
                    bll.inss(((ToolStripMenuItem)menuStrip1.Items[i]).DropDownItems[j].Text, i, j);

                }
            }
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            lab4.ForeColor = Color.Blue;
            pictureBox4.BackColor = Color.Lavender;
            //pictureBox4.Location = new Point(xp4, yp4-15);
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            lab5.ForeColor = Color.Blue;
            pictureBox5.BackColor = Color.Lavender;
            //pictureBox5.Location = new Point(xp5, yp5 - 15);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            lab5.ForeColor = Color.Black;
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Location = new Point(xp5, yp5);
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            lab6.ForeColor = Color.Blue;
            pictureBox6.BackColor = Color.Lavender;
            //pictureBox6.Location = new Point(xp6, yp6 - 15);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            lab6.ForeColor = Color.Black;
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Location = new Point(xp6, yp6);
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            lab7.ForeColor = Color.Blue;
            pictureBox7.BackColor = Color.Lavender;
            //pictureBox7.Location = new Point(xp7, yp7-15);
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            lab7.ForeColor = Color.Black;
            pictureBox7.BackColor = Color.Transparent;
            pictureBox7.Location = new Point(xp7, yp7);
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            lab8.ForeColor = Color.Blue;
            //pictureBox8.Location = new Point(xp8, yp8 - 10);
            pictureBox8.BackColor = Color.Lavender;
            //pictureBox8.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            lab8.ForeColor = Color.Black;
            pictureBox8.BackColor = Color.Transparent;
            //pictureBox8.BorderStyle = BorderStyle.None;
            //pictureBox8.Location = new Point(xp8, yp8);
        }

        private void pictureBox9_MouseMove(object sender, MouseEventArgs e)
        {
            lab9.ForeColor = Color.Blue;
            pictureBox9.BackColor = Color.Lavender;
            //pictureBox9.Location = new Point(xp9, yp9 - 15);
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            lab9.ForeColor = Color.Black;
            pictureBox9.BackColor = Color.Transparent;
            pictureBox9.Location = new Point(xp9, yp9);

        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            lab10.ForeColor = Color.Blue;
            pictureBox10.BackColor = Color.Lavender;
            //pictureBox10.Location = new Point(xp1, yp1-15);   

        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            lab10.ForeColor = Color.Black;
            pictureBox10.BackColor = Color.Transparent;
            pictureBox10.Location = new Point(xp1, yp1);
        }

        /// <summary>
        /// 智能排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //PAIBAN_SGShuRu f = new PAIBAN_SGShuRu();
            //f.ShowDialog();
            PaiBanForm f1 = new PaiBanForm();
            f1.ShowDialog();
        }

        /// <summary>
        /// 术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            BeforeVisit_Select frmBeforeVisit = new BeforeVisit_Select();
            frmBeforeVisit.ShowDialog();
        }

        /// <summary>
        /// 麻醉记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///  
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            JizhenOper f1 = new JizhenOper();
            f1.ShowDialog();
            //MZ_zhentong ff = new MZ_zhentong();
            //ff.ShowDialog();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MzjldSelet f = new MzjldSelet();
            f.ShowDialog();
        }

        /// <summary>
        /// PACU 苏醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PACU_Select f = new PACU_Select();
            f.ShowDialog();
        }

        /// <summary>
        ///  麻醉计划书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Mzjhs_select f = new Mzjhs_select();
            f.ShowDialog();
        }

        /// <summary>
        /// 麻醉小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MZZJ_select f = new MZZJ_select();
            f.ShowDialog();
        }

        /// <summary>
        /// 术后随访
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            AfterVisit_Select frmAfterVisit = new AfterVisit_Select();
            frmAfterVisit.ShowDialog();
        }

        /// <summary>
        /// 护理记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            NurseRecord_Select frmNurseRecord = new NurseRecord_Select();
            frmNurseRecord.ShowDialog();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Close();

        }



        private void 手术室药品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sugery_Management mySurgery = new Sugery_Management();
            mySurgery.Show();
        }

        private void 手术间药品管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OperationRoom_Management myRoom = new OperationRoom_Management();
            myRoom.Show();
        }

        private void 添加岗位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maintenance mt = new maintenance();
            mt.Show();
        }

        private void 岗位权限分配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jurisdiction_distribute jd = new jurisdiction_distribute();
            jd.Show();
        }

        private void ConsumableQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsumablesUseQuery cuq = new ConsumablesUseQuery();
            cuq.ShowDialog();
        }

        private void 手术医生校对ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperDoctorUpdate f1 = new OperDoctorUpdate();
            f1.ShowDialog();
        }

        private void hcglToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsumableManageForm cf = new ConsumableManageForm();
            cf.ShowDialog();
        }

        private void 麻醉科药品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anesthesiology_input input = new Anesthesiology_input();
            input.Show();
        }

        private void 手术间药品分发ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Anesthesiology_delivery delivery = new Anesthesiology_delivery();
            delivery.Show();
        }

        private void 药品退麻醉科库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anesthesiology_return giveBack = new Anesthesiology_return();
            giveBack.Show();
        }

        private void 医师领用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_room__output output = new Operation_room__output();
            output.Show();
        }

        private void 药品退手术间库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_room_return giveback = new Operation_room_return();
            giveback.Show();
        }



        private void 加班记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JBJL mp = new JBJL();
            mp.ShowDialog();
        }

        private void 请假加班统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QJJBTJ F = new QJJBTJ();
            F.ShowDialog();
        }

        private void SsyytjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SsYytj ssy = new SsYytj();
            ssy.ShowDialog();
        }

        private void 增加资源文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddSourceFile asf = new AddSourceFile();
            //asf.Show();
        }

        #endregion



        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Close();
        }

        private void 药品列表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YPguanli a = new YPguanli();
            a.Show();
        }

        private void mzyb_toolsmeItem_Click(object sender, EventArgs e)
        {
            mzyb_Tongji f1 = new mzyb_Tongji();
            f1.ShowDialog();
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();


        }

        private void pACUToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PACU_Select f = new PACU_Select();
            f.ShowDialog();
        }

        private void qxmbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QiXieQD f1 = new QiXieQD();
            f1.ShowDialog();
        }

        private void btnXGmima_Click(object sender, EventArgs e)
        {
            modify_password mp = new modify_password();
            mp.Show();
        }

        private void btnZhuxiao_Click(object sender, EventArgs e)
        {
            Fpmsd f = new Fpmsd();
            f.ShowDialog();
            //Process.Start(@".\main.exe");
            //Application.Exit();    
        }

        private void ssjlbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ssjListManage f1 = new ssjListManage();
            f1.ShowDialog();
        }

        private void 科室信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_KS f1 = new Add_KS();
            f1.ShowDialog();
        }


        private void 锁屏间隔设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LockSet f1 = new LockSet();
            f1.ShowDialog();
        }

        private void btnSetLockTime_Click(object sender, EventArgs e)
        {
            int a = dal.UpdateLockTime(Convert.ToInt32(nudSPJG.Value));
            MinitsTime = (int)nudSPJG.Value;
            timer11.Stop();
            timer11.Start();
            if (a > 0)
            {
                MessageBox.Show("锁屏时间设置成功!");
                gbLOCK.Visible = false;
                btnLockSet.Visible = true;
            }

        }

        private void mzjhsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Mzjhs_select().ShowDialog();
        }

        private void zqtysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mzzqtys f1 = new Mzzqtys();
            f1.ShowDialog();
        }

        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable excelTable = new DataTable();
                openFileDialog1.Multiselect = false;
                string filePath = openFileDialog1.FileName;
                string fileType = System.IO.Path.GetExtension(filePath);
                string SheetName1 = "手术名字编号";
                if (string.IsNullOrEmpty(fileType)) return;
                excelTable = UserFunction.GetDataFromExcel(fileType, filePath, SheetName1);
                excelTable = UserFunction.removeEmptyRow(excelTable);
                string OperNo = ""; string OperName = ""; string NameSuoxie = "";
                foreach (DataRow drE in excelTable.Rows)
                {
                    OperNo = drE[0].ToString();
                    OperName = drE[1].ToString();
                    NameSuoxie = drE[2].ToString();
                    dal.InsertOperName(OperNo, OperName, NameSuoxie);
                }
                MessageBox.Show("导入完成");

            }
        }

        private void inputOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable excelTable = new DataTable();
                openFileDialog1.Multiselect = false;
                string filePath = openFileDialog1.FileName;
                string fileType = System.IO.Path.GetExtension(filePath);
                string SheetName1 = "手术医生编号";
                if (string.IsNullOrEmpty(fileType)) return;
                excelTable = UserFunction.GetDataFromExcel(fileType, filePath, SheetName1);
                excelTable = UserFunction.removeEmptyRow(excelTable);
                string OsNo = ""; string OsName = ""; string NameSuoxie = "";
                foreach (DataRow drE in excelTable.Rows)
                {
                    string OS = drE[0].ToString();
                    string[] OSlist = OS.Split('_');
                    OsNo = OSlist[0];
                    OsName = OSlist[1];
                    NameSuoxie = OSlist[2];
                    dal.InsertShoushuYisheng(OsNo, OsName, NameSuoxie);
                }
                MessageBox.Show("导入完成");

            }
        }

        private void mzjqsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }
}
