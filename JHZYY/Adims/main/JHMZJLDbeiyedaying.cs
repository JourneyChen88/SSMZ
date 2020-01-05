using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace main
{
    public partial class JHMZJLDbeiyedaying : Form
    {
        string ZYH;
        public JHMZJLDbeiyedaying(string ID)
        {
            ZYH = ID;
            InitializeComponent();
        }
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();

        private void button1_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataTable dt = cll.mzbyjiazai(ZYH);
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Font ptzt8 = new Font("微软雅黑", 8);//普通字体 
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("微软雅黑", 11);//普通字体 
            Font ptzt12 = new Font("宋体", 10);//普通字体 
            Font ptzt08 = new Font("宋体", 8);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体   
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   
            int y = 20; int x = 70; int y1 = y + 15;
            e.Graphics.DrawString("说明：麻醉后医嘱请开在病历医嘱单上，开医嘱时参考下列内容", ptzt13, Brushes.Black, new Point(x + 220, y + 10));
            e.Graphics.DrawString("(1)                        麻醉后常规护理", ptzt11, Brushes.Black, new Point(x + 10, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 57, x + 130, y + 57);
            e.Graphics.DrawString("(2)体    位：", ptzt11, Brushes.Black, new Point(x + 10, y + 70));
            e.Graphics.DrawString("(3)血压，脉搏，呼吸每        分钟测量一次。平稳后停止或", ptzt11, Brushes.Black, new Point(x + 10, y + 100));
            e.Graphics.DrawLine(Pens.Black, x + 171, y + 117, x + 210, y + 117);
            e.Graphics.DrawString("(4)给    氧    鼻导管           L/MIN", ptzt11, Brushes.Black, new Point(x + 10, y + 130));
            e.Graphics.DrawLine(Pens.Black, x + 145, y + 147, x + 195, y + 147);
            e.Graphics.DrawString("面罩           L/MIN", ptzt11, Brushes.Black, new Point(x + 250, y + 130));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 147, x + 340, y + 147);
            e.Graphics.DrawString("(5)吸    痰", ptzt11, Brushes.Black, new Point(x + 10, y + 160));
            
            e.Graphics.DrawString("(6)动静脉穿刺后护理", ptzt11, Brushes.Black, new Point(x + 10, y + 190));
            
            e.Graphics.DrawString("(7)机械通气", ptzt11, Brushes.Black, new Point(x + 10, y + 220));
           
            e.Graphics.DrawString("(8)鼓励病人咳嗽，作深呼吸", ptzt11, Brushes.Black, new Point(x + 10, y + 250));
            
            e.Graphics.DrawString("(9)注意椎管内麻醉病人肢体感觉和活动恢复情况", ptzt11, Brushes.Black, new Point(x + 10, y + 280));
            
            e.Graphics.DrawString("(10)其他", ptzt11, Brushes.Black, new Point(x + 10, y + 310));

            x = 10;

            e.Graphics.DrawString("麻醉术后观察记录", ptzt18, Brushes.Black, new Point(x + 260, y + 350));
            
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 390, x + 780, y + 390);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 430, x + 780, y + 430);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 460, x + 780, y + 460);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 490, x + 780, y + 490);
                e.Graphics.DrawLine(Pens.Black, x + 35, y + 520, x + 780, y + 520);




                e.Graphics.DrawLine(Pens.Black, x + 35, y + 390, x + 35, y + 520);
                e.Graphics.DrawString("观察\n时间", ptzt11, Brushes.Black, new Point(x + 36, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 75, y + 390, x + 75, y + 520);
                e.Graphics.DrawString("呼吸\n抑制", ptzt11, Brushes.Black, new Point(x + 76, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 430, x + 100, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 76, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 106, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 135, y + 390, x + 135, y + 520);
                e.Graphics.DrawString("再插管", ptzt11, Brushes.Black, new Point(x + 136, y + 400));
                e.Graphics.DrawLine(Pens.Black, x + 160, y + 430, x + 160, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 136, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 166, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 195, y + 390, x + 195, y + 520);
                e.Graphics.DrawString("循环\n稳定", ptzt11, Brushes.Black, new Point(x + 196, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 220, y + 430, x + 220, y + 520);
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 440));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 440));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 470));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 470));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 196, y + 500));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 226, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 255, y + 390, x + 255, y + 520);
                e.Graphics.DrawString("咽喉痛", ptzt11, Brushes.Black, new Point(x + 256, y + 400));
                e.Graphics.DrawLine(Pens.Black, x + 280, y + 430, x + 280, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 256, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 286, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 315, y + 390, x + 315, y + 520);
                e.Graphics.DrawString("恶心\n呕吐", ptzt11, Brushes.Black, new Point(x + 316, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 340, y + 430, x + 340, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 316, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 346, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 375, y + 390, x + 375, y + 520);
                e.Graphics.DrawLine(Pens.Black, x + 400, y + 430, x + 400, y + 520);
                e.Graphics.DrawString("声音\n嘶哑", ptzt11, Brushes.Black, new Point(x + 375, y + 390));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 375, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 406, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 435, y + 390, x + 435, y + 520);
                e.Graphics.DrawString("下肢肌\n力恢复", ptzt11, Brushes.Black, new Point(x + 436, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 460, y + 430, x + 460, y + 520);
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 440));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 440));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 470));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 470));
                e.Graphics.DrawString("是", ptzt11, Brushes.Black, new Point(x + 436, y + 500));
                e.Graphics.DrawString("否", ptzt11, Brushes.Black, new Point(x + 466, y + 500));

                e.Graphics.DrawLine(Pens.Black, x + 495, y + 390, x + 495, y + 520);
                e.Graphics.DrawString("穿刺点\n压 痛", ptzt11, Brushes.Black, new Point(x + 496, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 540, y + 430, x + 540, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 496, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 545, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 585, y + 390, x + 585, y + 520);
                e.Graphics.DrawString("脊麻后\n头 痛", ptzt11, Brushes.Black, new Point(x + 586, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 630, y + 430, x + 630, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 586, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 635, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 675, y + 390, x + 675, y + 520);
                e.Graphics.DrawString("尿潴留", ptzt11, Brushes.Black, new Point(x + 676, y + 390));
                e.Graphics.DrawLine(Pens.Black, x + 700, y + 430, x + 700, y + 520);
                e.Graphics.DrawLine(Pens.Black, x + 730, y + 430, x + 730, y + 520);
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 440));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 440));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 440));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 470));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 470));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 470));
                e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 676, y + 500));
                e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 706, y + 500));
                e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 736, y + 500));
                e.Graphics.DrawLine(Pens.Black, x + 780, y + 390, x + 780, y + 520);
                e.Graphics.DrawString("其他特殊情况及处理：", ptzt11, Brushes.Black, new Point(x + 10, y + 530));
                y = 20 - 60;
            e.Graphics.DrawString("麻醉术后镇痛观察记录", ptzt18, Brushes.Black, new Point(x + 260, y + 670));
            e.Graphics.DrawString("镇痛方案：PCEA(      )；PCIA（     ）；其他", ptzt11, Brushes.Black, new Point(x + 10, y + 700));

            e.Graphics.DrawLine(Pens.Black, x + 35, y + 740, x + 780, y + 740);
            e.Graphics.DrawLine(Pens.Black, x + 105, y + 770, x + 240, y + 770);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 800, x + 780, y + 800);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 830, x + 780, y + 830);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 860, x + 780, y + 860);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 890, x + 780, y + 890);
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 920, x + 780, y + 920);



            e.Graphics.DrawLine(Pens.Black, x + 35, y + 740, x + 35, y + 920);
            e.Graphics.DrawString("观察时间", ptzt11, Brushes.Black, new Point(x + 36, y + 755));

            e.Graphics.DrawLine(Pens.Black, x + 105, y + 740, x + 105, y + 920);
            e.Graphics.DrawString("痛觉评分(VAS)", ptzt11, Brushes.Black, new Point(x + 126, y + 740));
            e.Graphics.DrawString("安静时", ptzt11, Brushes.Black, new Point(x + 106, y + 771));
            e.Graphics.DrawLine(Pens.Black, x + 170, y + 770, x + 170, y + 920);
            e.Graphics.DrawString("活动时", ptzt11, Brushes.Black, new Point(x + 176, y + 771));
            e.Graphics.DrawLine(Pens.Black, x + 240, y + 740, x + 240, y + 920);
            e.Graphics.DrawString("嗜睡", ptzt11, Brushes.Black, new Point(x + 260, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 285, y + 800, x + 285, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 241, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 301, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 330, y + 740, x + 330, y + 920);
            e.Graphics.DrawString("恶心", ptzt11, Brushes.Black, new Point(x + 350, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 375, y + 800, x + 375, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 331, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 391, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 420, y + 740, x + 420, y + 920);
            e.Graphics.DrawString("呕吐", ptzt11, Brushes.Black, new Point(x + 440, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 465, y + 800, x + 465, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 421, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 481, y + 900));

            e.Graphics.DrawLine(Pens.Black, x + 510, y + 740, x + 510, y + 920);
            e.Graphics.DrawString("瘙痒", ptzt11, Brushes.Black, new Point(x + 530, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 555, y + 800, x + 555, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 511, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 571, y + 900));
            e.Graphics.DrawLine(Pens.Black, x + 600, y + 740, x + 600, y + 920);
            e.Graphics.DrawString("尿潴留", ptzt11, Brushes.Black, new Point(x + 660, y + 755));
            e.Graphics.DrawLine(Pens.Black, x + 645, y + 800, x + 645, y + 920);
            e.Graphics.DrawLine(Pens.Black, x + 705, y + 800, x + 705, y + 920);
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 810));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 810));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 810));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 840));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 840));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 840));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 870));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 870));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 870));
            e.Graphics.DrawString("有", ptzt11, Brushes.Black, new Point(x + 601, y + 900));
            e.Graphics.DrawString("无", ptzt11, Brushes.Black, new Point(x + 661, y + 900));
            e.Graphics.DrawString("导尿", ptzt11, Brushes.Black, new Point(x + 721, y + 900));
            e.Graphics.DrawLine(Pens.Black, x + 780, y + 740, x + 780, y + 920);
            y = 20-120;
            e.Graphics.DrawString("其他特殊情况及处理：", ptzt11, Brushes.Black, new Point(x + 35, y + 980));
            e.Graphics.DrawString("麻醉医生签字：", ptzt11, Brushes.Black, new Point(x + 505, y + 1080));
            e.Graphics.DrawString("注:麻醉术后观察记录要求在术后24小时完成，若无麻醉相关并发症发生，观察记录一次即可；麻醉术后镇痛观察", ptzt10, Brushes.Black, new Point(x + 35, y + 1110));
            e.Graphics.DrawString("记录需观察两天，每天一次；若发现有麻醉相关并发症，应及时通知经治医师共同处理，并继续观察至病情好转", ptzt10, Brushes.Black, new Point(x + 35, y + 1140));
            e.Graphics.DrawString("为止。记录时请在观察项目下打勾即可", ptzt10, Brushes.Black, new Point(x + 35, y + 1170));
            e.Graphics.DrawString("镇痛评分:向患者充分介绍VAS的相关知识，记录相应时点的VAS值。评分标准：0分 无痛；10分 强烈疼痛；", ptzt10, Brushes.Black, new Point(x + 35, y + 1200));
            e.Graphics.DrawString("1-3分 轻度疼痛；4-6分 中度疼痛；7-10分 重度疼痛。", ptzt10, Brushes.Black, new Point(x + 35, y + 1230));
            //数据
            #region

            y = 20;
            x = 70;
            //for (int a = 0; a < dt.Rows.Count; a++)  //  a行 b列
            //{



            e.Graphics.DrawString(dt.Rows[0]["huli"].ToString(), ptzt11, Brushes.Black, x + 35, y + 40);//麻醉后

            e.Graphics.DrawString(dt.Rows[0]["tiwei"].ToString(), ptzt11, Brushes.Black, x + 100, y + 70);//体位


            e.Graphics.DrawString(dt.Rows[0]["xueya"].ToString(), ptzt11, Brushes.Black, x + 172, y + 100);//呼吸每分钟
            e.Graphics.DrawString(dt.Rows[0]["bidaoguan"].ToString(), ptzt11, Brushes.Black, x + 160, y + 130);//导管

            e.Graphics.DrawString(dt.Rows[0]["mianzhao"].ToString(), ptzt11, Brushes.Black, x + 301, y + 130);//面罩
                
                
                    string cd = dt.Rows[0]["fuxuankuang"].ToString();
                    for (int i = 0; i < cd.Length; i++)
                    {
                        string b = cd.Substring(i, 1);
                        string sj = b;
                        if (sj == "1")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 100, y + 165, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 100, y + 170, x + 105, y + 175);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 105, y + 175, x + 110, y + 165);
                            
                        }
                        else if (sj == "2")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 170, y + 195, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 170, y + 200, x + 175, y + 205);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 175, y + 205, x + 180, y + 195);
                        }
                        else if (sj == "3")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 110, y + 225, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 110, y + 230, x + 115, y + 235);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 115, y + 235, x + 120, y + 225);
                        }
                        else if (sj == "4")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 220, y + 255, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 220, y + 260, x + 225, y + 265);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 225, y + 265, x + 230, y + 255);
                        }
                        else if (sj == "5")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 285, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 360, y + 290, x + 365, y + 295);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 365, y + 295, x + 370, y + 285);
                        }
                        else if (sj == "6")
                        {
                            e.Graphics.DrawRectangle(Pens.Black, x + 90, y + 315, 10, 10);
                            e.Graphics.DrawLine(pblue2, x + 90, y + 320, x + 95, y + 325);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                            e.Graphics.DrawLine(pblue2, x + 95, y + 325, x + 100, y + 315);
                        }
                    }

                    e.Graphics.DrawString(dt.Rows[0]["qita"].ToString(), ptzt11, Brushes.Black, x + 101, y + 310);//其他
    
            #endregion
        }
    }
}
