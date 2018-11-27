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

namespace main
{
    public partial class BeforeVisit_YS : Form
    {
        #region <<Members>>

        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();

        #endregion

        #region <<Constructors>>

        string patID;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit_YS(string patid)
        {
            patID = patid;
            InitializeComponent();

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit_YS()
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
        private void BeforeVisit_Load(object sender, EventArgs e)
        {
            BindYS();
            BindPatInfo();
            SetEditValue();
        
        }
        private void BindYS()
        {
            DataTable dt = bll.selectUserName(1);
            cmbYisheng.DataSource = dt;
            cmbYisheng.DisplayMember = "姓名";
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.SetVisibleCore(true);
        }

        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(patID);

            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbAge.Text = dt.Rows[0]["Patage"].ToString();
            cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
            tbBingqu.Text = dt.Rows[0]["patdpm"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();
            //tbOnameHistory.Text = dt.Rows[0]["Oname"].ToString();
            //tbSqzd.Text = dt.Rows[0]["Pattmd"].ToString();

        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            #region  16K纸张
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);

            Font ptzt = new Font("新宋体", 9);//普通字体
            Font ptzt2 = new Font("新宋体", 12);//普通字体
            Font ptzt3 = new Font("新宋体", 13);//普通字体
            int JG = 30; int y = 20; int x = 40; int y1 = 0;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "  术  前  访  视  单";
            int row = 0;
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 200, y));
            y = y + 25; y1 = y + 17;
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 200, y));
            y = y + 25; y1 = y + 17;
            e.Graphics.DrawString(title3, ptzt2, Brushes.Black, new Point(x + 200, y));

            row++;
            row++;
            //第一行

            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 110, y + 15 + JG * row);

            e.Graphics.DrawString("性别：" + cmbSex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 155, y + 15 + JG * row, x + 190, y + 15 + JG * row);


            e.Graphics.DrawString("病区：" + tbBingqu.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 235, y + 15 + JG * row, x + 435, y + 15 + JG * row);

            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 445, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 495, y + 15 + JG * row, x + 580, y + 15 + JG * row);

            row++;

            e.Graphics.DrawString("麻醉相关病史 ", ptzt2, Brushes.Black, new Point(x, y + JG * row));

            row++;

            e.Graphics.DrawString("1、手术：" + this.cmbShoushushi.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 60, y + 15 + JG * row, x + 200, y + 15 + JG * row);


            e.Graphics.DrawString("麻醉：" + cmbMZFF.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 255, y + 15 + JG * row, x + 350, y + 15 + JG * row);

            e.Graphics.DrawString("接受血制品：" + cmbXuezhipin.Text, ptzt, Brushes.Black, new Point(x + 380, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 450, y + 15 + JG * row, x + 550, y + 15 + JG * row);



            row++;
            //第四行

            e.Graphics.DrawString("2、吸烟：" + cmbXiyan.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 100, y + 15 + JG * row);

            if (cmbXiyan.Text == "是")
            {

                e.Graphics.DrawString(tbXiyanYear.Text + " 年约 " + tbXiyanliang.Text, ptzt, Brushes.Black, new Point(x + 110, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 15 + JG * row, x + 150, y + 15 + JG * row);


                e.Graphics.DrawString("支/天、戒烟约 " + tbJieyanDay.Text, ptzt, Brushes.Black, new Point(x + 150, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 250, y + 15 + JG * row, x + 300, y + 15 + JG * row);


                e.Graphics.DrawString("天", ptzt, Brushes.Black, new Point(x + 300, y + JG * row));


            }
            e.Graphics.DrawString("3、哮喘：" + cmbXiaochuan.Text, ptzt, Brushes.Black, new Point(x + 320, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 370, y + 15 + JG * row, x + 430, y + 15 + JG * row);


            e.Graphics.DrawString("处理方法：" + tbXiaochuanChuli.Text, ptzt, Brushes.Black, new Point(x + 450, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 510, y + 15 + JG * row, x + 650, y + 15 + JG * row);

            row++;
            //第六行

            e.Graphics.DrawString("4、近来感冒：" + cmbGanmao.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 140, y + 15 + JG * row);



            e.Graphics.DrawString("约 " + tbGanmaoDay.Text, ptzt, Brushes.Black, new Point(x + 150, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 165, y + 15 + JG * row, x + 210, y + 15 + JG * row);

            e.Graphics.DrawString("天前痊愈", ptzt, Brushes.Black, new Point(x + 210, y + JG * row));

            row++;

            //第七行

            e.Graphics.DrawString("5、近来咳嗽：" + cmbKesou.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 200, y + 15 + JG * row);


            e.Graphics.DrawString("6、睡觉时打呼：" + cmbDahu.Text, ptzt, Brushes.Black, new Point(x + 300, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 400, y + 15 + JG * row, x + 600, y + 15 + JG * row);


            row++;

            //第八行


            e.Graphics.DrawString("7、体力活动：" + cmbTilihuodong.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 15 + JG * row, x + 300, y + 15 + JG * row);


            row++;
            //第九行

            e.Graphics.DrawString("8、胸闷、胸痛：" + cmbXiongmen.Text, ptzt, Brushes.Black, new Point(x + 0, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 95, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("放射：" + cmbFangshe.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 255, y + 15 + JG * row, x + 350, y + 15 + JG * row);

            e.Graphics.DrawString("缓解：" + cmbHuanjie.Text, ptzt, Brushes.Black, new Point(x + 370, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 405, y + 15 + JG * row, x + 500, y + 15 + JG * row);

            row++;

            //第十行

            e.Graphics.DrawString("9、高血压：" + cmbGaoxueya.Text, ptzt, Brushes.Black, new Point(x + 0, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 15 + JG * row, x + 150, y + 15 + JG * row);

            e.Graphics.DrawString("(最高 " + tbGXY_High_down.Text + "/" + tbGXY_High_up.Text, ptzt, Brushes.Black, new Point(x + 160, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 195, y + 15 + JG * row, x + 290, y + 15 + JG * row);
            e.Graphics.DrawString("mmHg；最低 " + tbGXY_Low_down.Text + "/" + tbGXY_Low_up.Text, ptzt, Brushes.Black, new Point(x + 290, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 355, y + 15 + JG * row, x + 450, y + 15 + JG * row);
            e.Graphics.DrawString("mmHg", ptzt, Brushes.Black, new Point(x + 450, y + JG * row));

            row++;
            e.Graphics.DrawString("血压低于 " + tbXueyaUnderDown.Text + "/" + tbXueyaUnderUp.Text, ptzt, Brushes.Black, new Point(x + 20, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 15 + JG * row, x + 190, y + 15 + JG * row);
            e.Graphics.DrawString("mmHg时有头晕，平时血压 " + tbPJXueyaDown.Text + "/" + tbPJXueyaUp.Text, ptzt, Brushes.Black, new Point(x + 190, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 340, y + 15 + JG * row, x + 460, y + 15 + JG * row);
            e.Graphics.DrawString("mmHg", ptzt, Brushes.Black, new Point(x + 460, y + JG * row));

            row++;

            //第十一行

            e.Graphics.DrawString("10、四肢活动： " + cmbSizhi.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 85, y + 15 + JG * row, x + 220, y + 15 + JG * row);

            e.Graphics.DrawString("11、精神病史： " + cmbJingshenbing.Text, ptzt, Brushes.Black, new Point(x + 240, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 325, y + 15 + JG * row, x + 400, y + 15 + JG * row);

            e.Graphics.DrawString("晕厥史： " + cmbYunjue.Text, ptzt, Brushes.Black, new Point(x + 450, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 500, y + 15 + JG * row, x + 600, y + 15 + JG * row);

            row++;
            //第十三行            
            e.Graphics.DrawString("12、青光眼： " + cmbQinguanyan.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 75, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("13、糖尿病： " + cmbTangniaoB.Text, ptzt, Brushes.Black, new Point(x + 230, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 15 + JG * row, x + 400, y + 15 + JG * row);

            e.Graphics.DrawString("14、饮食： " + cmbYinshi.Text, ptzt, Brushes.Black, new Point(x + 420, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 480, y + 15 + JG * row, x + 600, y + 15 + JG * row);

            row++;
            //第十六行
            e.Graphics.DrawString("15、胃、十二指肠溃疡： " + cmb12Kuiyang.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 140, y + 15 + JG * row, x + 300, y + 15 + JG * row);

            e.Graphics.DrawString("16、饮酒： " + cmbYinjiu.Text, ptzt, Brushes.Black, new Point(x + 330, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 390, y + 15 + JG * row, x + 480, y + 15 + JG * row);

            row++;
            //第十八行

            e.Graphics.DrawString("17、受伤： " + cmbShoushang.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 60, y + 15 + JG * row, x + 150, y + 15 + JG * row);

            if (tbShouShangBW.Text.Trim() != "")
            {
                e.Graphics.DrawString("受伤部位： " + tbShouShangBW.Text, ptzt, Brushes.Black, new Point(x + 160, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 220, y + 15 + JG * row, x + 300, y + 15 + JG * row);
            }

            e.Graphics.DrawString("18、出血倾向： " + cmbChuxueQX.Text, ptzt, Brushes.Black, new Point(x + 330, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 420, y + 15 + JG * row, x + 600, y + 15 + JG * row);


            row++;
            e.Graphics.DrawString("19、药物过敏：" + cmbGuomin.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 85, y + 15 + JG * row, x + 140, y + 15 + JG * row);

            if (tbGuomingyao.Text.Trim() != "")
            {

                e.Graphics.DrawString("药物名称:" + tbGuomingyao.Text, ptzt, Brushes.Black, new Point(x + 140, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 195, y + 15 + JG * row, x + 330, y + 15 + JG * row);


            }

            e.Graphics.DrawString("食物过敏：" + cmbGuominshiwu.Text, ptzt, Brushes.Black, new Point(x + 350, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 410, y + 15 + JG * row, x + 460, y + 15 + JG * row);

            if (tbGuomingyao.Text.Trim() != "")
            {

                e.Graphics.DrawString("食物名称 " + tbGuominwu.Text, ptzt, Brushes.Black, new Point(x + 470, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 520, y + 15 + JG * row, x + 660, y + 15 + JG * row);


            }
            row++;

            //第二十一行

            e.Graphics.DrawString("20、近期服药： " + cmbJinqifuyao.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            if (tbJinqifuyaoOther.Text.Trim() != "")
            {

                e.Graphics.DrawString("其他药： " + tbJinqifuyaoOther.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 260, y + 15 + JG * row, x + 450, y + 15 + JG * row);


            }
            row++;

            //第二十二行

            e.Graphics.DrawString("21、平时腰痛：" + cmbYaotong.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 85, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("适年妇女月经： " + cmbJingqi.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 310, y + 15 + JG * row, x + 450, y + 15 + JG * row);

            e.Graphics.DrawString("怀孕： " + cmbHuaiyun.Text, ptzt, Brushes.Black, new Point(x + 470, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 500, y + 15 + JG * row, x + 650, y + 15 + JG * row);

            row++;

            //第二十三行

            e.Graphics.DrawString("22、婴幼儿出生： " + cmbYingErCS.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 100, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("婴儿活动： " + cmbYEhuodong.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 15 + JG * row, x + 400, y + 15 + JG * row);

            e.Graphics.DrawString("哭闹时口唇发紫： " + cmbKoucunfazi.Text, ptzt, Brushes.Black, new Point(x + 420, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 515, y + 15 + JG * row, x + 650, y + 15 + JG * row);

            row++;

            //第二十四行

            e.Graphics.DrawString("23、亲属(有血缘关系)麻醉手术史： " + cmbQSMZS.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 210, y + 15 + JG * row, x + 320, y + 15 + JG * row);

            if (tbQSMZS_Other.Text.Trim() != "")
            {

                e.Graphics.DrawString(", " + tbQSMZS_Other.Text, ptzt, Brushes.Black, new Point(x + 300, y + JG * row));
                e.Graphics.DrawLine(Pens.Black, x + 305, y + 15 + JG * row, x + 700, y + 15 + JG * row);


            }
            row++;

            //第二十五行 

            e.Graphics.DrawString("备注： " + rtbRemark.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 660, y + 15 + JG * row);

            row++;

            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 660, y + 15 + JG * row);


            row++;
            e.Graphics.DrawString("全身麻醉相关检查", ptzt2, Brushes.Black, new Point(x, y + JG * row));

            row++;

            e.Graphics.DrawString("1、瞳孔大小、形状、眼球活动：" + cmbTongkong.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 180, y + 15 + JG * row, x + 250, y + 15 + JG * row);

            e.Graphics.DrawString("2、开口度：" + cmbKaikoudu.Text, ptzt, Brushes.Black, new Point(x + 280, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 345, y + 15 + JG * row, x + 420, y + 15 + JG * row);

            e.Graphics.DrawString("3、Mallampati分级：" + cmbMallampati.Text, ptzt, Brushes.Black, new Point(x + 450, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 560, y + 15 + JG * row, x + 650, y + 15 + JG * row);



            //第二十八行
            row++;

            e.Graphics.DrawString("4、头颈活动：" + cmbToujingHD.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("气管居中：" + cmbQiguanJZ.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 280, y + 15 + JG * row, x + 440, y + 15 + JG * row);


            row++;

            e.Graphics.DrawString("5、牙：" + cmbYA.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 40, y + 15 + JG * row, x + 200, y + 15 + JG * row);

            e.Graphics.DrawString("活动的牙：" + cmbHuodongYA.Text, ptzt, Brushes.Black, new Point(x + 220, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 280, y + 15 + JG * row, x + 400, y + 15 + JG * row);

            e.Graphics.DrawString("易受伤牙：" + cmbYSS_YA.Text, ptzt, Brushes.Black, new Point(x + 420, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 480, y + 15 + JG * row, x + 600, y + 15 + JG * row);

            row++;
            //第二十九行
            e.Graphics.DrawString("医师：" + cmbYisheng.Text, ptzt, Brushes.Black, new Point(x + 250, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 15 + JG * row, x + 440, y + 15 + JG * row);
            e.Graphics.DrawString("访视日期："+dtVisitDate.Text, ptzt, Brushes.Black, new Point(x + 450, y  + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 515, y + 15 + JG * row, x + 660, y + 15 + JG * row);

            row++;


            
            
            #endregion
        }



        #endregion

        #region <<Methods>>

        /// <summary>
        /// 清空
        /// </summary>
        private void ClearEdit()
        {


        }

        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue()
        {
            DataTable dt = DAL.GetBeforeVisit_YS(patID);
            if (dt.Rows.Count == 0)
                return;
            else
            {
                DataRow dr = dt.Rows[0];
                cmbShoushushi.Text = dr["Shoushushi"].ToString();
                tbOnameHistory.Text = dr["ShoushushiName"].ToString();
                cmbMZFF.Text = dr["MZFF"].ToString();
                tbMZFF_Ci.Text = dr["MZFF_Ci"].ToString();
                cmbXuezhipin.Text = dr["Xuezhipin"].ToString();
                cmbXiyan.Text = dr["Xiyan"].ToString();
                tbXiyanYear.Text = dr["XiyanYear"].ToString();
                tbXiyanliang.Text = dr["Xiyanliang"].ToString();
                tbJieyanDay.Text = dr["JieyanDay"].ToString();
                cmbXiaochuan.Text = dr["Xiaochuan"].ToString();
                tbXiaochuanChuli.Text = dr["XiaochuanChuli"].ToString();
                cmbGanmao.Text = dr["Ganmao"].ToString();
                tbGanmaoDay.Text = dr["GanmaoDay"].ToString();
                cmbKesou.Text = dr["Kesou"].ToString();
                cmbDahu.Text = dr["Dahu"].ToString();
                cmbTilihuodong.Text = dr["Tilihuodong"].ToString();
                cmbXiongmen.Text = dr["Xiongmen"].ToString();
                cmbFangshe.Text = dr["Fangshe"].ToString();
                cmbHuanjie.Text = dr["Huanjie"].ToString();
                cmbGaoxueya.Text = dr["Gaoxueya"].ToString();
                tbGXY_High_down.Text = dr["GXY_High_down"].ToString();
                tbGXY_High_up.Text = dr["GXY_High_up"].ToString();
                tbGXY_Low_down.Text = dr["GXY_Low_down"].ToString();
                tbGXY_Low_up.Text = dr["GXY_Low_up"].ToString();
                tbXueyaUnderDown.Text = dr["XueyaUnderDown"].ToString();
                tbXueyaUnderUp.Text = dr["XueyaUnderUp"].ToString();
                tbPJXueyaDown.Text = dr["PJXueyaDown"].ToString();
                tbPJXueyaUp.Text = dr["PJXueyaUp"].ToString();
                cmbSizhi.Text = dr["Sizhi"].ToString();
                cmbJingshenbing.Text = dr["Jingshenbing"].ToString();
                cmbYunjue.Text = dr["Yunjue"].ToString();
                cmbQinguanyan.Text = dr["Qinguanyan"].ToString();
                cmbTangniaoB.Text = dr["TangniaoB"].ToString();
                cmbYinshi.Text = dr["Yinshi"].ToString();
                cmb12Kuiyang.Text = dr["Kuiyang12"].ToString();
                cmbYinjiu.Text = dr["Yinjiu"].ToString();
                cmbChuxueQX.Text = dr["ChuxueQX"].ToString();
                cmbGuomin.Text = dr["Guomin"].ToString();
                tbGuomingyao.Text = dr["Guomingyao"].ToString();
                cmbGuominshiwu.Text = dr["Guominshiwu"].ToString();
                tbGuominwu.Text = dr["Guominwu"].ToString();
                cmbJinqifuyao.Text = dr["Jinqifuyao"].ToString();
                tbJinqifuyaoOther.Text = dr["JinqifuyaoOther"].ToString();
                cmbYaotong.Text = dr["Yaotong"].ToString();
                cmbJingqi.Text = dr["Jingqi"].ToString();
                cmbHuaiyun.Text = dr["Huaiyun"].ToString();
                cmbYingErCS.Text = dr["YingErCS"].ToString();
                cmbYEhuodong.Text = dr["YEhuodong"].ToString();
                cmbKoucunfazi.Text = dr["Koucunfazi"].ToString();
                cmbQSMZS.Text = dr["QSMZS"].ToString();
                tbQSMZS_Other.Text = dr["QSMZS_Other"].ToString();
                rtbRemark.Text = dr["Remark"].ToString();
                cmbTongkong.Text = dr["Tongkong"].ToString();
                cmbKaikoudu.Text = dr["Kaikoudu"].ToString();
                cmbMallampati.Text = dr["Mallampati"].ToString();
                cmbToujingHD.Text = dr["ToujingHD"].ToString();
                cmbQiguanJZ.Text = dr["QiguanJZ"].ToString();
                cmbYA.Text = dr["YA"].ToString();
                cmbHuodongYA.Text = dr["HuodongYA"].ToString();
                cmbYSS_YA.Text = dr["YSS_YA"].ToString();
                dtVisitDate.Value = Convert.ToDateTime(dr["VisitDate"]);
                cmbYisheng.Text = dr["mzys"].ToString();
                if (Convert.ToInt32(dr["IsRead"]) == 1)
                {
                    SaveToolStripMenuItem.Enabled = false;
                }

            }
           
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <returns></returns>
        int BCCount = 0;
        private void SaveBeforeVisit()
        {
            if (!ValiDateBeforeVisit()) return;
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                beforeVisit.Add("Shoushushi", cmbShoushushi.Text);               
                beforeVisit.Add("MZFF", cmbMZFF.Text);
                beforeVisit.Add("MZFF_Ci", tbMZFF_Ci.Text);
                beforeVisit.Add("Xuezhipin", cmbXuezhipin.Text);
                beforeVisit.Add("Xiyan", cmbXiyan.Text);
                beforeVisit.Add("XiyanYear", tbXiyanYear.Text);
                beforeVisit.Add("Xiyanliang", tbXiyanliang.Text);
                beforeVisit.Add("JieyanDay", tbJieyanDay.Text);
                beforeVisit.Add("Xiaochuan", cmbXiaochuan.Text);
                beforeVisit.Add("XiaochuanChuli", tbXiaochuanChuli.Text);
                beforeVisit.Add("Ganmao", cmbGanmao.Text);
                beforeVisit.Add("GanmaoDay", tbGanmaoDay.Text);
                beforeVisit.Add("Kesou", cmbKesou.Text);
                beforeVisit.Add("Dahu", cmbDahu.Text);
                beforeVisit.Add("Tilihuodong", cmbTilihuodong.Text);
                beforeVisit.Add("Xiongmen", cmbXiongmen.Text);
                beforeVisit.Add("Fangshe", cmbFangshe.Text);
                beforeVisit.Add("Huanjie", cmbHuanjie.Text);
                beforeVisit.Add("Gaoxueya", cmbGaoxueya.Text);
                beforeVisit.Add("GXY_High_down", tbGXY_High_down.Text);
                beforeVisit.Add("GXY_High_up", tbGXY_High_up.Text);
                beforeVisit.Add("GXY_Low_down", tbGXY_Low_down.Text);
                beforeVisit.Add("GXY_Low_up", tbGXY_Low_up.Text);
                beforeVisit.Add("XueyaUnderDown", tbXueyaUnderDown.Text);
                beforeVisit.Add("XueyaUnderUp", tbXueyaUnderUp.Text);
                beforeVisit.Add("PJXueyaDown", tbPJXueyaDown.Text);
                beforeVisit.Add("PJXueyaUp", tbPJXueyaUp.Text);
                beforeVisit.Add("Sizhi", cmbSizhi.Text);
                beforeVisit.Add("Jingshenbing", cmbJingshenbing.Text);
                beforeVisit.Add("Yunjue", cmbYunjue.Text);
                beforeVisit.Add("Qinguanyan", cmbQinguanyan.Text);
                beforeVisit.Add("TangniaoB", cmbTangniaoB.Text);
                beforeVisit.Add("Yinshi", cmbYinshi.Text);
                beforeVisit.Add("Kuiyang12", cmb12Kuiyang.Text);
                beforeVisit.Add("Yinjiu", cmbYinjiu.Text);
                beforeVisit.Add("ChuxueQX", cmbChuxueQX.Text);
                beforeVisit.Add("Guomin", cmbGuomin.Text);
                beforeVisit.Add("Guomingyao", tbGuomingyao.Text);
                beforeVisit.Add("Guominshiwu", cmbGuominshiwu.Text);
                beforeVisit.Add("Guominwu", tbGuominwu.Text);
                beforeVisit.Add("Jinqifuyao", cmbJinqifuyao.Text);
                beforeVisit.Add("JinqifuyaoOther", tbJinqifuyaoOther.Text);
                beforeVisit.Add("Yaotong", cmbYaotong.Text);
                beforeVisit.Add("Jingqi", cmbJingqi.Text);
                beforeVisit.Add("Huaiyun", cmbHuaiyun.Text);
                beforeVisit.Add("YingErCS", cmbYingErCS.Text);
                beforeVisit.Add("YEhuodong", cmbYEhuodong.Text);
                beforeVisit.Add("Koucunfazi", cmbKoucunfazi.Text);
                beforeVisit.Add("QSMZS", cmbQSMZS.Text);
                beforeVisit.Add("QSMZS_Other", tbQSMZS_Other.Text);
                beforeVisit.Add("Remark", rtbRemark.Text);
                beforeVisit.Add("Tongkong", cmbTongkong.Text);
                beforeVisit.Add("Kaikoudu", cmbKaikoudu.Text);
                beforeVisit.Add("Mallampati", cmbMallampati.Text);
                beforeVisit.Add("ToujingHD", cmbToujingHD.Text);
                beforeVisit.Add("QiguanJZ", cmbQiguanJZ.Text);
                beforeVisit.Add("YA", cmbYA.Text);
                beforeVisit.Add("HuodongYA", cmbHuodongYA.Text);
                beforeVisit.Add("YSS_YA", cmbYSS_YA.Text);
                beforeVisit.Add("VisitDate", dtVisitDate.Value.ToString());
                beforeVisit.Add("patid", patID);
                beforeVisit.Add("MZYS", cmbYisheng.Text.Trim());
                beforeVisit.Add("ShoushushiName", tbOnameHistory.Text);
                DataTable dt = DAL.GetBeforeVisit_YS(patID);
                if (dt.Rows.Count == 0)
                    result = DAL.InsertBeforeVisit_YS(beforeVisit);
                else
                    result = DAL.UpdateBeforeVisit_YS(beforeVisit);
                if (result>0)
                {
                    MessageBox.Show("保存成功！");
                    BCCount++;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请重试！");
            }
                      
        }

      
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValiDateBeforeVisit()
        {
            if (!ValidationRegex.ValidteData(tbJieyanDay.Text))
            {
                MessageBox.Show("戒烟天数填写有误，请检查！");
                tbJieyanDay.Focus();
                return false;
            }
            
            if (!ValidationRegex.ValidteData(tbXiyanliang.Text))
            {
                MessageBox.Show("吸烟量填写有误，请检查！");
                tbJieyanDay.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteDouble(tbXiyanYear.Text))
            {
                MessageBox.Show("吸烟年数填写有误，请检查！");
                tbJieyanDay.Focus();
                return false;
            }

            else
                return true;
        }

        #endregion



        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBeforeVisit();
        }

        private void printStripMenuItem_Click(object sender, EventArgs e)
        {

            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo))
                {
                    SaveBeforeVisit();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();

                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
            }

        }

        private void 最大化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.SetVisibleCore(false);
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.SetVisibleCore(true);
        }

        private void cunDangStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetBeforeVisit_YS(patID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("术前访视信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateBeforeVisit_YS_STATE(patID);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void BeforeVisit_YS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                this.Close();
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo))
                {
                    SaveBeforeVisit();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();

                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_YS_FormClosing);
                    this.Close();
                }
            }
        }

       


    }
}
