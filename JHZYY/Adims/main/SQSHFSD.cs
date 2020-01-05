using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace main.CGG
{
    public partial class SQSHFSD : Form
    {
        adims_BLL.YXL_BLL bll = new adims_BLL.YXL_BLL();
        adims_DAL.YXL_DAL dal = new adims_DAL.YXL_DAL();
        
        private string patid = "";
        public SQSHFSD(string id)
        {
            // patid = id;
            patid = id;
            InitializeComponent();
        }

        private void SQSHFSD_Load(object sender, EventArgs e)
        {
            this.Text = "手术室术前、术后访视单";
            #region 病人信息
            try
            {
                //病人新
                DataTable dt = bll.SelectPatInfo111(patid);
                if (dt.Rows.Count > 0)
                {

                    txtbKeShi.Text = dt.Rows[0]["Patdpm"].ToString();
                    txtbChuangHao.Text = dt.Rows[0]["Patname"].ToString();
                    txtbZYH.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
                    txtbAge.Text = dt.Rows[0]["Patage"].ToString();
                    txtbSex.Text = dt.Rows[0]["Patsex"].ToString();
                    txtbHZName.Text = dt.Rows[0]["Patname"].ToString();
                    txtbMZFS.Text = dt.Rows[0]["Amethod"].ToString();
                    txtbMZSSMC.Text = dt.Rows[0]["Oname"].ToString();
                }
                //初始化数据 SQSHFSD
                dt = bll.SQGetInfo(patid);
                if (dt.Rows.Count > 0)
                {
                    //    dateTimeP.Text = dt.Rows[0]["Createdatetime"].ToString();
                    // groupBox1
                    txtb11.Text = dt.Rows[0]["txtb11"].ToString();
                    txtb12.Text = dt.Rows[0]["txtb12"].ToString();
                    chk11.Checked = (dt.Rows[0]["chk11"].ToString()) != "" ? true : false;
                    chk12.Checked = (dt.Rows[0]["chk12"].ToString()) != "" ? true : false;
                    chk13.Checked = (dt.Rows[0]["chk13"].ToString()) != "" ? true : false;
                    chk14.Checked = (dt.Rows[0]["chk14"].ToString()) != "" ? true : false;

                    txtb13.Text = dt.Rows[0]["txtb13"].ToString();
                    txtb14.Text = dt.Rows[0]["txtb14"].ToString();
                    rtxtb1.Text = dt.Rows[0]["rtxtb1"].ToString();
                    chk15.Checked = (dt.Rows[0]["chk15"].ToString()) != "" ? true : false;
                    chk16.Checked = (dt.Rows[0]["chk16"].ToString()) != "" ? true : false;
                    chk17.Checked = (dt.Rows[0]["chk17"].ToString()) != "" ? true : false;
                    chk18.Checked = (dt.Rows[0]["chk18"].ToString()) != "" ? true : false;
                    chk19.Checked = (dt.Rows[0]["chk19"].ToString()) != "" ? true : false;
                    chk110.Checked = (dt.Rows[0]["chk110"].ToString()) != "" ? true : false;
                    chk111.Checked = (dt.Rows[0]["chk111"].ToString()) != "" ? true : false;
                    chk112.Checked = (dt.Rows[0]["chk112"].ToString()) != "" ? true : false;
                    chk113.Checked = (dt.Rows[0]["chk113"].ToString()) != "" ? true : false;
                    chk114.Checked = (dt.Rows[0]["chk114"].ToString()) != "" ? true : false;
                    chk115.Checked = (dt.Rows[0]["chk115"].ToString()) != "" ? true : false;


                    rtxtb2.Text = dt.Rows[0]["rtxtb2"].ToString();
                    rtxtb3.Text = dt.Rows[0]["rtxtb3"].ToString();
                    rtxtb4.Text = dt.Rows[0]["rtxtb4"].ToString();
                    txtb21.Text = dt.Rows[0]["txtb21"].ToString();
                    txtb22.Text = dt.Rows[0]["txtb22"].ToString();

                    chk21.Checked = (dt.Rows[0]["chk21"].ToString()) != "" ? true : false;
                    chk22.Checked = (dt.Rows[0]["chk22"].ToString()) != "" ? true : false;
                    chk23.Checked = (dt.Rows[0]["chk23"].ToString()) != "" ? true : false;
                    chk24.Checked = (dt.Rows[0]["chk24"].ToString()) != "" ? true : false;
                    chk25.Checked = (dt.Rows[0]["chk23"].ToString()) != "" ? true : false;
                    chk26.Checked = (dt.Rows[0]["chk24"].ToString()) != "" ? true : false;


                }
                else
                {
                    bll.SQCreateInfo(patid);
                }

            }
            catch { }

            #endregion
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bll.SQCreateInfo(patid);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string num = "";
                foreach (Control c in groupBox3.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox ct = (TextBox)c;
                        bll.SQUpdateInfo(patid, ct.Name, ct.Text);
                    }
                    if (c is CheckBox)
                    {
                        CheckBox ct = (CheckBox)c;
                        num = ct.Checked ? "1" : "";
                        bll.SQUpdateInfo(patid, ct.Name, num);
                    }

                }
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox ct = (TextBox)c;
                        bll.SQUpdateInfo(patid, ct.Name, ct.Text);
                    }
                    if (c is CheckBox)
                    {
                        CheckBox ct = (CheckBox)c;
                        num = ct.Checked ? "1" : "";
                        bll.SQUpdateInfo(patid, ct.Name, num);
                    }
                    if (c is RichTextBox)
                    {
                        RichTextBox ct = (RichTextBox)c;
                        bll.SQUpdateInfo(patid, ct.Name, ct.Text);
                    }
                }
                foreach (Control c in groupBox2.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox ct = (TextBox)c;
                        bll.SQUpdateInfo(patid, ct.Name, ct.Text);
                    }
                    if (c is CheckBox)
                    {
                        CheckBox ct = (CheckBox)c;
                        num = ct.Checked ? "1" : "";
                        bll.SQUpdateInfo(patid, ct.Name, num);
                    }
                    if (c is RichTextBox)
                    {
                        RichTextBox ct = (RichTextBox)c;
                        bll.SQUpdateInfo(patid, ct.Name, ct.Text);
                    }
                }


            }
            catch { }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printDocument1.DefaultPageSettings.PaperSize =
                      new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            // printDocument1.DefaultPageSettings.Landscape = true;
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体 
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   


            int y = 40; int x = 25;
            e.Graphics.DrawString("新疆医科大学第五附属医院", ptzt11, Brushes.Black, new Point(x + 270, y - 10));

            string title1 = "手术室术前、术后访视单";
            e.Graphics.DrawString(title1, ptzt18, Brushes.Black, new Point(x + 220, y + 10));

            x = x + 10; y = y + 30;

            y = y + 40;
            //横线

            for (int i = 0; i < 33; i++)
            {
                if (i == 5 || i == 12 || i == 19) continue;
                e.Graphics.DrawLine(Pens.Black, x + 0, y + i * 30 + 0, x + 750, y + i * 30 + 0);
            }
            ////竖线
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 960);
            e.Graphics.DrawLine(Pens.Black, x + 750, y - 0, x + 750, y + 960);
            y = y + 7;
            e.Graphics.DrawString("手术日期:" + dateTimeP.Value.ToString("yyyy-MM-dd HH:mm"), ptzt11, Brushes.Black, new Point(x + 5, y + 0));
            e.Graphics.DrawString("手术科室:" + txtbKeShi.Text, ptzt11, Brushes.Black, new Point(x + 300, y + 0));
            e.Graphics.DrawString("床号:" + txtbChuangHao.Text, ptzt11, Brushes.Black, new Point(x + 540, y + 0));

            e.Graphics.DrawString("住院号:" + txtbZYH.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 30));
            e.Graphics.DrawString("病人姓名:" + txtbHZName.Text, ptzt11, Brushes.Black, new Point(x + 300, y + 30));
            e.Graphics.DrawString("性别:" + txtbSex.Text, ptzt11, Brushes.Black, new Point(x + 540, y + 30));
            e.Graphics.DrawString("年龄:" + txtbAge.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 30));

            e.Graphics.DrawString("拟施手术名称:" + txtbMZSSMC.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 60));
            e.Graphics.DrawString("拟施麻醉方式:" + txtbMZFS.Text, ptzt11, Brushes.Black, new Point(x + 400, y + 60));

            e.Graphics.DrawString("过敏史:    □有   □无 详情:" + txtb11.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 90));
            if (chk11.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 95, y + 90));
            if (chk12.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 150, y + 90));

            e.Graphics.DrawString("特殊传染:    □有   □无 详情:" + txtb12.Text, ptzt11, Brushes.Black, new Point(x + 400, y + 90));
            if (chk13.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 505, y + 90));
            if (chk14.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 560, y + 90));


            e.Graphics.DrawString("为了您能更好地配合医护人员顺利地完成手术，请您仔细阅读访视单。如有不明之处或什么要求，请您告\n诉探望您的手术护士。", ptzt11, Brushes.Black, new Point(x + 5, y + 125));
            e.Graphics.DrawString("手术前及手术中的注意事项：", ptzt11, Brushes.Black, new Point(x + 5, y + 180));
            e.Graphics.DrawString("1、术前一日洗澡更衣(建议全棉开襟衣物)，注意保暖，保证休息。", ptzt11, Brushes.Black, new Point(x + 5, y + 210));
            e.Graphics.DrawString("2、术日晨，请排空大、小便，穿好修养服（禁止穿毛衣，内衣等）卧床静候，手术室护士将到您床旁接您。", ptzt11, Brushes.Black, new Point(x + 5, y + 240));
            e.Graphics.DrawString("3、将你的手表、发卡、饰品取下，不要带钱及其他贵重物品。", ptzt11, Brushes.Black, new Point(x + 5, y + 270));
            e.Graphics.DrawString("4、术前请不要化妆、涂口红，以病情变化影响观擦。", ptzt11, Brushes.Black, new Point(x + 5, y + 300));
            e.Graphics.DrawString("5、为了手术安全进行，请如实告知手术室护士您是否打术前针、是否进食水、对药物及消毒液有无过敏史\n及曾患疾病。如您发热或来月经请告知主治医生及手术室护土。", ptzt11, Brushes.Black, new Point(x + 5, y + 335));
            e.Graphics.DrawString("6、因手术室床较窄，在床上时不要随意翻身，以免坠床。 ", ptzt11, Brushes.Black, new Point(x + 5, y + 390));
            e.Graphics.DrawString("7、手术室护士为您进行的技术操作有:a、静脉输液  b、摆置麻醉体位  c、摆置手术体位  d、粘贴负极等", ptzt11, Brushes.Black, new Point(x + 5, y + 420));
            e.Graphics.DrawString("8、术中如有不适请告知医护人员。", ptzt11, Brushes.Black, new Point(x + 5, y + 450));
            e.Graphics.DrawString("9、手术间内各种手术仪器、监护器会发出声响，请不要紧张。 ", ptzt11, Brushes.Black, new Point(x + 5, y + 480));
            e.Graphics.DrawString("10、术后请遵从医嘱配合适当体位", ptzt11, Brushes.Black, new Point(x + 5, y + 510));

            e.Graphics.DrawString("我们将以高度的责任心，全力做好手术配合。为了更好地改进工作，请您协助填写以下问题，在相应的项目\n上打钩。", ptzt11, Brushes.Black, new Point(x + 5, y + 545));

            e.Graphics.DrawString("访问护士的服务态度：        □好      □较好      □一般     □差", ptzt11, Brushes.Black, new Point(x + 5, y + 600));
            if (chk15.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 225, y + 600));
            if (chk16.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 305, y + 600));
            if (chk17.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 400, y + 600));
            if (chk18.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 485, y + 600));

            e.Graphics.DrawString("您是否对手术过程基本了解:   □了解    □一般      □模糊     □不了解", ptzt11, Brushes.Black, new Point(x + 5, y + 630));
            if (chk19.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 225, y + 630));
            if (chk110.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 305, y + 630));
            if (chk111.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 400, y + 630));
            if (chk112.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 485, y + 630));

            e.Graphics.DrawString("访视护士解释是否清楚:       □清楚    □一般      □不清楚 ", ptzt11, Brushes.Black, new Point(x + 5, y + 660));
            if (chk113.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 225, y + 660));
            if (chk114.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 305, y + 660));
            if (chk115.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 400, y + 660));

            e.Graphics.DrawString("您还想了解那方面的知识:" + rtxtb1.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 690));

            e.Graphics.DrawString("访视护士:" + txtb13.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 720));
            e.Graphics.DrawString("患者:" + txtb14.Text, ptzt11, Brushes.Black, new Point(x + 420, y + 720));

            e.Graphics.DrawString("术后访视", ptzt13, Brushes.Black, new Point(x + 5, y + 780));
            e.Graphics.DrawString("患者皮肤:     □完整      □损伤    详情:"+rtxtb2.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 810));
            if (chk21.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 120, y + 810));
            if (chk22.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 215, y + 810));

            e.Graphics.DrawString("肢体活动:     □无损伤    □有损伤  详情:"+rtxtb3.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 840));
            if (chk23.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 120, y + 840));
            if (chk24.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 215, y + 840));
            e.Graphics.DrawString("其他:         □无        □有      详情:"+rtxtb4.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 870));
            if (chk25.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 120, y + 870));
            if (chk26.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 215, y + 870));


            e.Graphics.DrawString("访视护士:"+txtb21.Text, ptzt11, Brushes.Black, new Point(x + 5, y + 900));
            e.Graphics.DrawString("患者:" + txtb22.Text, ptzt11, Brushes.Black, new Point(x + 420, y + 900));








        }


    }
}
