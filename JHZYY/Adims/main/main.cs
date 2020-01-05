using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using main.用户安全;
using main.科室事物管理;
using main.药品耗材管理;
using main.权限管理;
using adims_MODEL;
using adims_BLL;
using adims_DAL;
using System.Diagnostics;
using main.知识库;
using main.数据维护;


namespace main
{
    public partial class main : Form, IMessageFilter
    {
        #region <<Members>>

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
         
        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public main()
        {
            InitializeComponent();
            //this.skinEngine1.SkinFile = "SportsBlue.ssk"; // 指定皮肤文件
        }

        #endregion

        #region <<Events>>
        #region //IMessageFilter 成员,禁止下拉框鼠标滚动

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            
            this.Text += "(当前用户:  "+Program.customer.user_name+"+ 职称："+Program.customer.position+")";
            string jurisdiction = bll.Get_user_jurisdiction(Program.customer);
            sqxxToolStripMenuItem.Visible = false;
            szxxToolStripMenuItem.Visible = false;
            shxxToolStripMenuItem.Visible = false;
            ypglToolStripMenuItem.Visible = false;
            ygxxToolStripMenuItem.Visible = false;
            xxcxToolStripMenuItem.Visible = false;
            sjwhToolStripMenuItem.Visible = false;
            qxglToolStripMenuItem.Visible = false;
            tmisReadUpdateLog.Visible = false;
            if (jurisdiction.Contains("0")) sqxxToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("1")) szxxToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("2")) shxxToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("3")) ypglToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("4")) ygxxToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("5")) xxcxToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("6")) sjwhToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("7")) qxglToolStripMenuItem.Visible = true;
            if (jurisdiction.Contains("9")) tmisReadUpdateLog.Visible = true;
 
            //if (!Program.jurisdiction[0]) { ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[0].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[0].Visible = false; }
            //if (!Program.jurisdiction[1]) { ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[1].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[1].Visible = false; }
            //if (!Program.jurisdiction[0] && !Program.jurisdiction[1])
            //{ menuStrip1.Items[0].Enabled = false; menuStrip1.Items[0].Visible = false; }
            //if (!Program.jurisdiction[2]) { ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[0].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[0].Visible = false; }
            //if (!Program.jurisdiction[3]) { ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[1].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[1].Visible = false; }
            //if (!Program.jurisdiction[4]) { ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[2].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[2].Visible = false; }
            //if (!Program.jurisdiction[2] && !Program.jurisdiction[3] && !Program.jurisdiction[4])
            //{ menuStrip1.Items[1].Enabled = false; menuStrip1.Items[1].Visible = false; }
            //if (!Program.jurisdiction[5]) { ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[0].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[0].Visible = false; }
            //if (!Program.jurisdiction[6]) { ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[1].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[1].Visible = false; }
            //if (!Program.jurisdiction[7]) { ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[2].Enabled = false; ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[2].Visible = false; }
            //if (!Program.jurisdiction[5] && !Program.jurisdiction[6] && !Program.jurisdiction[7])
            //{ menuStrip1.Items[2].Enabled = false; menuStrip1.Items[2].Visible = false; }
            //if (!Program.jurisdiction[8])
            //{ menuStrip1.Items[3].Enabled = false; menuStrip1.Items[3].Visible = false; }
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[0].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[1].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[2].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[3].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[4].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[5].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[6].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[7].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[8].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[4]).DropDownItems[9].Visible = false;
            //((ToolStripMenuItem)menuStrip1.Items[8]).DropDownItems[0].Visible = false;
        }

        private void muMzjld_Click(object sender, EventArgs e)
        {
            mzjld_Select smzjld1 = new mzjld_Select();
            smzjld1.Show();
        }

        private void 排班ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paiban formpaiban = new paiban();
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
           DateSetup maintenance = new DateSetup();
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

      

        private void 手术间浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sjll1 f = new sjll1();
            f.ShowDialog();

        }


        private void 护理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NurseRecord_Select hlform = new NurseRecord_Select();
            hlform.ShowDialog();
        }

        private void pACUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PACU_Select f = new PACU_Select();
            f.ShowDialog();
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
            Lsyz_Select shsfform = new Lsyz_Select();
            shsfform.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MZZJ_Select f = new MZZJ_Select();
            f.ShowDialog();
        }
               

        /// <summary>
        /// 手术管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            lab2.Visible = true;
            lab3.Visible = true;
        }

        /// <summary>
        /// 紧急手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUrgent_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            lab2.Visible = false;
            lab3.Visible = false;
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
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            lab4.ForeColor = Color.Black;
            pictureBox4.BackColor = Color.Transparent;
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
            pictureBox8.BorderStyle = BorderStyle.None;
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
        }

        /// <summary>
        /// 智能排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            paiban f = new paiban();
            f.ShowDialog();
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
            MZZT_Select ff = new MZZT_Select();
            ff.ShowDialog();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            mzjld_Select f = new mzjld_Select();
            f.ShowDialog();
            
        }

        /// <summary>
        /// PACU 苏醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PACU_Select f = new  PACU_Select();
            f.ShowDialog();
        }

        /// <summary>
        ///  器械扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox7_Click(object sender, EventArgs e)
        {         
            //Lsyz_Select f = new Lsyz_Select();//器械清点
            QXBCodes_Select f1 = new QXBCodes_Select();
            f1.ShowDialog();
        }

        /// <summary>
        /// 麻醉小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            menzhenfashidan f = new menzhenfashidan();
            f.ShowDialog();
        }

        /// <summary>
        /// 术后随访
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Lsyz_Select frmAfterVisit = new Lsyz_Select();
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
            DialogResult result = MessageBox.Show("确定退出系统吗", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
                this.Close();                
            }
            //else
            //    return;
               
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
            ZhichengManage mt = new ZhichengManage();
            mt.Show();
        }

        private void 岗位权限分配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanXianFenPei jd = new QuanXianFenPei();
            jd.Show();
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
            QJJBRecorder mp = new QJJBRecorder();
            mp.ShowDialog();
        }

        private void 请假加班统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QJJBTJ F = new QJJBTJ();
            F.ShowDialog();
        }

        private void SsyytjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShoushuYYTJ ssy = new ShoushuYYTJ();
            ssy.ShowDialog();
        }

        private void 增加资源文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSourceFile asf = new AddSourceFile();
            asf.Show();
        }

        private void 查阅知识库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZhiShiKu f2 = new ZhiShiKu();
            f2.ShowDialog();
        }
        #endregion

        //private void cmbSkin_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (cmbSkin.Text=="波浪色")
        //    {
        //        this.skinEngine1.SkinFile = "Wave.ssk";
        //    }
        //    else if (cmbSkin.Text == "运动蓝色")
        //    {
        //        this.skinEngine1.SkinFile = "SportsBlue.ssk";
        //    }
        //    else if (cmbSkin.Text == "运动橙色")
        //    {
        //        this.skinEngine1.SkinFile = "SportsOrange.ssk";
        //    }
        //    else

        //        this.skinEngine1.SkinFile = "Page.ssk";
        //}

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定退出系统吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel) 
                e.Cancel = true;
        }

        private void 药品列表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YaopinManage a = new YaopinManage();
            a.Show();
        }

        private void mzyb_toolsmeItem_Click(object sender, EventArgs e)
        {
            MzybTJ f1 = new MzybTJ();
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
            QxmbManage f1 = new QxmbManage();
            f1.ShowDialog();
        }

        private void btnXGmima_Click(object sender, EventArgs e)
        {
            UpdatePwd mp = new UpdatePwd();
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
            SsjManage f1 = new SsjManage();
            f1.ShowDialog();
        }


        private void qjjbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QJJBRecorder f1 = new QJJBRecorder();
            f1.ShowDialog();
        }

        private void EmegengcyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PAIBAN_SG formpaiban = new PAIBAN_SG();
            formpaiban.ShowDialog();
        }

        private void ypflToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YaopinManage f1 = new YaopinManage();
            f1.ShowDialog();
        }

        private void szsjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            szsjGuanli sjllform = new szsjGuanli();
            sjllform.ShowDialog();
        }

        private void pbResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaibanResult f1 = new PaibanResult();
            f1.ShowDialog();
        }
        /// <summary>
        /// 输血评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShuXuePG_Click(object sender, EventArgs e)
        {
            ShuXuePG_Select f1 = new ShuXuePG_Select();
            f1.Show();
        }
        /// <summary>
        /// 查询修改日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmisReadUpdateLog_Click(object sender, EventArgs e)
        {
            ReadUpdateLog f1 = new ReadUpdateLog();
            f1.Show();
        }
        /// <summary>
        /// 麻醉计费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MZYYJF_Select f1 = new MZYYJF_Select();
            f1.Show();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            lab1.ForeColor = Color.Black;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            lab1.ForeColor = Color.Blue;
            pictureBox1.BackColor = Color.Lavender;
            //pictureBox1.Location = new Point(xp9, yp9 - 15);
        }
        /// <summary>
        /// 麻醉计费组套管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 麻醉用药计费组套管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MZYYZT f1 = new MZYYZT();
            f1.Show();
        }
        /// <summary>
        /// 手术登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSSDJ_Click(object sender, EventArgs e)
        {
            addHSDJ f1 = new addHSDJ();
            f1.Show();
        }
        /// <summary>
        /// 临时医嘱包管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmisLiYZBao_Click(object sender, EventArgs e)
        {
            addYZZT f1 = new addYZZT();
            f1.Show();
        }

        private void ypglToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 门诊特殊登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menzhentsdj f1 = new menzhentsdj();
            f1.Show();
        }

        private void 麻醉术后统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mazuishtjjitj f1 = new mazuishtjjitj();
            f1.Show();
        }

        private void 手术室查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hushizhangtongji f1 = new hushizhangtongji();
            f1.Show();
        }

        private void 复苏室查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chaxunfusubr f1 = new chaxunfusubr();
            f1.Show();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chaxunfusubr f1 = new chaxunfusubr();
            f1.Show();
            //mazuikesf f1 = new mazuikesf();
            //f1.Show();


            //shousjf f1 = new shousjf();
            //f1.Show();
        }

        private void 查询当月耗材ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chaxunfusubr f1 = new chaxunfusubr();
            f1.Show();
        }

        private void 查询当天门诊病人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chaxunfusubr f1 = new chaxunfusubr();
            f1.Show();
        }
        
      
    }
}
