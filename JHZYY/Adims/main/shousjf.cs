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
    public partial class shousjf : Form
    {
        string patID1;
        public shousjf(string patID)
        {

           patID1 = patID;
            InitializeComponent();
        }
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        adims_DAL.mz dal = new adims_DAL.mz();
        adims_BLL.mz bll = new adims_BLL.mz();
        private void button1_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void info()//基础信息
        {
            try
            {
                DataTable dt1 = dal.GetShuShlJLDAN(patID1);
                if (dt1.Rows.Count > 0)
                {
                    DataRow dr1 = dt1.Rows[0];
                    tbPatname.Text = dr1["PatName"].ToString();
                    // tbMingzu.Text = dr1["PatNation"].ToString();
                    tbSex.Text = dr1["Patsex"].ToString();
                    tbAge.Text = dr1["Patage"].ToString();
                    tbZhuyuanID.Text = dr1["PatID"].ToString();
                    tbKeshi.Text = dr1["Patdpm"].ToString();
                    tbBedNO.Text = dr1["Patbedno"].ToString();
                    //tbSZZD.Text = dr1["Pattmd"].ToString();
                    tbShoushuName.Text = dr1["Oname"].ToString();
                    //tbGuominshi.Text = dr1["asa"].ToString();
                    //cmbMZFF.Text = dr1["Amethod"].ToString();
                    //textBox2.Text = dr1["PatWeight"].ToString();
                    //cmbOroom.Text = dr1["Oroom"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void shousjf_Load(object sender, EventArgs e)
        {
            info();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int row = 0;
            Font zt9 = new Font("宋体", 8);//字体
            Font zt10 = new Font("宋体", 10);
            Font zt11 = new Font("宋体", 12);
            Font ht13 = new Font("黑体", 13);
            Font ht18 = new Font("黑体", 18);
            Pen pblue = new Pen(Brushes.Blue);
            pblue.Width = 2;
            Pen ptp = Pens.Gray;//下划线画笔
            Pen pb1 = new Pen(Brushes.Black);  //普通画笔
            Pen pb2 = new Pen(Brushes.Black, 3);
            Brush bp1 = Brushes.Black;
            int y = 30; int x = 35; int y1 = 0; //整体位置
            //y = y + 20;
            //string title1 = " 新疆医科大学第五附属医院";
            string title2 = " 手 术 室 收 费 记 录 单 ";
            //e.Graphics.DrawString(title1, ht13, Brushes.Black, x + 230, y);
            y = y + 35;
            e.Graphics.DrawString(title2, ht18, Brushes.Black, x + 205, y);
            y = y + 35;
            e.Graphics.DrawString("科室:" + tbKeshi.Text + "  床号:" + tbBedNO.Text + "  住院号:" + tbZhuyuanID.Text + "   姓名:" + tbPatname.Text + "  年龄:" + tbAge.Text + "   性别:" + tbSex.Text + "   日期:" + dtVisitDate.Value.ToString("yyyy-MM-dd"), zt10, Brushes.Black, x + 10, y);
            
            y = y + 30;
            e.Graphics.DrawLine(pb1, x + 0, y, x + 750, y);
            e.Graphics.DrawLine(pb1, x + 0, y, x +0, y+905);
            e.Graphics.DrawLine(pb1, x + 20, y, x +20, y + 840);
            e.Graphics.DrawLine(pb1, x + 200, y, x + 200, y + 840);
            e.Graphics.DrawLine(pb1, x + 220, y, x + 220, y + 840);
            e.Graphics.DrawLine(pb1, x + 250, y, x + 250, y + 840);
            e.Graphics.DrawLine(pb1, x + 270, y, x + 270, y + 840);
            e.Graphics.DrawLine(pb1, x + 450, y, x + 450, y + 840);
            e.Graphics.DrawLine(pb1, x + 480, y, x + 480, y + 840);
            e.Graphics.DrawLine(pb1, x + 500, y, x + 500, y + 840);
            e.Graphics.DrawLine(pb1, x + 520, y, x + 520, y + 905);
            e.Graphics.DrawLine(pb1, x + 700, y, x + 700, y + 840);
            e.Graphics.DrawLine(pb1, x + 720, y, x +720, y + 840);
            e.Graphics.DrawLine(pb1, x + 750, y, x + 750, y + 905);
            //e.Graphics.DrawString("", zt10, Brushes.Black, x, y);
            e.Graphics.DrawString("            项目", zt9, Brushes.Black, x + 20, y + 15);
            e.Graphics.DrawString("单", zt9, Brushes.Black, x + 200, y + 5);
            e.Graphics.DrawString("位", zt9, Brushes.Black, x + 200, y + 25);
            e.Graphics.DrawString("数", zt9, Brushes.Black, x + 220, y + 5);
            e.Graphics.DrawString("量", zt9, Brushes.Black, x + 220, y + 25);
            e.Graphics.DrawString("            项目", zt9, Brushes.Black, x + 270, y + 15);
            e.Graphics.DrawString("单", zt9, Brushes.Black, x + 450, y + 5);
            e.Graphics.DrawString("位", zt9, Brushes.Black, x + 450, y + 25);
            e.Graphics.DrawString("数", zt9, Brushes.Black, x + 480, y + 5);
            e.Graphics.DrawString("量", zt9, Brushes.Black, x + 480, y + 25);
            e.Graphics.DrawString("            项目", zt9, Brushes.Black, x + 520, y + 15);
            e.Graphics.DrawString("单", zt9, Brushes.Black, x + 700, y + 5);
            e.Graphics.DrawString("位", zt9, Brushes.Black, x + 700, y + 25);
            e.Graphics.DrawString("数", zt9, Brushes.Black, x + 720, y + 5);
            e.Graphics.DrawString("量", zt9, Brushes.Black, x + 720, y + 25);
            y = y + 40;
            e.Graphics.DrawLine(pb1, x + 0, y, x + 750, y);
            e.Graphics.DrawString("手", zt10, Brushes.Black, x, y+5);
            e.Graphics.DrawString("特殊传染病人手术加收", zt9, Brushes.Black, x + 20, y+5);
            e.Graphics.DrawString("次", zt9, Brushes.Black, x + 200, y + 5);
            
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("可吸收止血膜（S-100）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 450, y + 5);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("氯化钠       100 ml", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            e.Graphics.DrawString("电切刀", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("袋", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x+20, y, x + 250, y);
            e.Graphics.DrawString("术", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("患者进入手术室自身原因终止手术", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("次", zt9, Brushes.Black, x + 200, y + 5);


            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);
            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("可吸收止血膜（大清）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 450, y + 5);
            

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);
            
           // e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("氯化钠       250 ml", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            e.Graphics.DrawString("等渗液", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("袋", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x , y, x + 250, y);
            e.Graphics.DrawString("泰", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("可吸收缝线（1/0泰科877）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("手术止血纱（速绫）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 450, y + 5);
           
            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("氯化钠       500 ml", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            e.Graphics.DrawString("骨蜡", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("科", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("可吸收缝线（2/0泰科923）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);
            e.Graphics.DrawString("防", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("透明质酸钠5ml昊泰", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            ////e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("生物止血材料(多糖)术泰舒", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("袋", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 250, y);
           // e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（1# vcp359）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("粘", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("透明质酸钠5ml海诺特", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 450, y + 5);
            

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("补", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("医用生物胶体液(50)术优康", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            //e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（1/0 VCP358H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("连", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("生物胶（50ML术尔泰）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 450, y + 5);
           

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("充", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("手术防粘连液(2M)赛必妥", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            //e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（2/0 VCP345H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);
            e.Graphics.DrawString("材", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("生物止血流体膜（术瑞吉100）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("袋", zt9, Brushes.Black, x + 450, y + 5);
           

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("体", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("功能性敷料(2G)速肤康", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（3/0 VCP311H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("料", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("多糖生物医用胶（术尔健）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 450, y + 5);
            

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("/", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("功能性敷料(8*6)速肤康", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("微", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（4/0 VCP310H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("防粘连膜（进口美国强生）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
          

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("药", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("功能性敷料(15*4)速肤康", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("乔", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（5/0 VCP433H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            // e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("护创敷料（小）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 450, y + 5);
          

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("乳酸钠林格液   500 ml", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（7/0 W9561）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            // e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("护创敷料（大）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 450, y + 5);
           

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("眼药膏 ", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("支", zt9, Brushes.Black, x + 700, y + 5);


            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（8/0 W9560）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 250, y, x + 500, y);

            // e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("银夹（心脏 大）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
           

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("甲硝唑针  100 ml ", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（1/0 W9215）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("结", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("银夹 (心脏 小)", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
            

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("双氧水    100 ml ", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);


            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（3/0 VCP772D）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("扎", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("钛夹(强生)", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
            

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            //e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            //e.Graphics.DrawString("亚甲蓝 ", zt9, Brushes.Black, x + 520, y + 5);
            //e.Graphics.DrawString("支", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("微乔线（4/0 VCP771D）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("材", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("生物夹（可吸收）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 500, y, x + 750, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("氯化钠       100 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("爱惜邦（2﹟ X519）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("料", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("钛夹（泰利福544240）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("液", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("氯化钠       250 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("爱惜邦（2/0 W6977）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("钛夹（泰利福544250）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("体", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("氯化钠       500 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("爱", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("爱惜邦（4/0 W6891）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 250, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("一次性引流管（多通道）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("/", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("5%葡萄糖液    250 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("惜", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("爱惜邦（5/0 W945）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);
            e.Graphics.DrawString("一", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("一次性引流管（双套管）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("药", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("5%葡萄糖液    500 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("邦", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("不锈钢丝（W649）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("次", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("T型引流导管（14-24#）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("10%葡萄糖液    500 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("爱惜邦（M66G）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("性", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("硅胶导尿管", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

           // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("5%GNS    500 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（3/0 W8558）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("管", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("双腔单囊导尿管（8-22#）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("碳酸氢钠    250 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（4/0 W8683）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("道", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("蘑菇头引流管", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("甘露醇    250 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（4/0 W8557）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 250, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("皮肤缝合器（拉钩小号2个）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("包", zt9, Brushes.Black, x + 450, y + 5);
            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("乳酸钠林格液    500 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（5/0 W8310）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("皮肤缝合器（拉钩大号3个）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("包", zt9, Brushes.Black, x + 450, y + 5);


            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("眼药膏", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("普", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（5/0 W8710H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("一次性吻合器（皮肤35R）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("甲硝唑针  100ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("理", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（6/0 EH7242H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("消融电极（C型）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("双氧水    100 ml", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("瓶", zt9, Brushes.Black, x + 700, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("灵", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（7/0 EH7222H）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 500, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("消融电极（A5型）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            e.Graphics.DrawLine(pb1, x + 520, y, x + 750, y);

            // e.Graphics.DrawString("品", zt9, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawString("亚甲蓝", zt9, Brushes.Black, x + 520, y + 5);
            e.Graphics.DrawString("支", zt9, Brushes.Black, x + 700, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普理灵（8/0 W2777）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);

            e.Graphics.DrawString("其", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("消融电极（E型）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普迪思（9236T）150环线", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);

            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针0.8", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普迪思（肝针9391）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针1.0", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普迪思（荷包针EH7625E）", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针1.5", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x , y, x + 250, y);
            e.Graphics.DrawString("自", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("可吸收缝线VLOC泰科 ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针2.0", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("锁", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("可吸收缝线（安捷泰） ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针2.5", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("线", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("普迪思（爱惜捷）强生 ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("克氏针3.0", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("根", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 250, y);
           // e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("速即纱1961 ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 250, y, x + 750, y);
            e.Graphics.DrawString("", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("压疮贴（心）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("止", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("速即纱1962 ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("补", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("压疮贴（方）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("血", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("胶原蛋白海绵（倍菱小） ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("充", zt9, Brushes.Black, x + 250, y + 5);
            e.Graphics.DrawString("压疮贴（足）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("胶原蛋白海绵（倍菱大10.5） ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("片", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("压疮贴（小）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);

            y = y + 20;
            e.Graphics.DrawLine(pb1, x + 20, y, x + 250, y);
            e.Graphics.DrawString("", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("明胶海绵 ", zt9, Brushes.Black, x + 20, y + 5);
            e.Graphics.DrawString("包", zt9, Brushes.Black, x + 200, y + 5);

            e.Graphics.DrawLine(pb1, x + 270, y, x + 750, y);
            e.Graphics.DrawString("压疮贴（面）", zt9, Brushes.Black, x + 270, y + 5);
            e.Graphics.DrawString("个", zt9, Brushes.Black, x + 450, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x , y, x + 750, y);
            e.Graphics.DrawString("手术名称：", zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawString("医生签字：", zt11, Brushes.Black, x + 525, y + 5);
            y = y + 30;
            //e.Graphics.DrawString("医生签字：" , zt11, Brushes.Black, x+520, y + 5);
            e.Graphics.DrawString("计费人签字：", zt11, Brushes.Black, x+525, y + 5);

            y = y + 35;
            e.Graphics.DrawLine(pb1, x, y, x + 750, y);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //           //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
    }
}
