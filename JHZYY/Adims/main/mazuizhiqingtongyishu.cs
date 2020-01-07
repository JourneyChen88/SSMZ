using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using adims_DAL;

namespace main.CGG
{
    public partial class mazuigyishuzhiqingtongyishu : Form
    {
        DB2help emr = new adims_DAL.DB2help();
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        string paytime;
        string patID1;
        string zhuyaunhjcs;
   



        public mazuigyishuzhiqingtongyishu(string patID)
        {
            // TODO: Complete member initialization
            this.patID1 = patID;
            //paytime = date;
            InitializeComponent();
        }
       

        private void MZZQTIS_Load_1(object sender, EventArgs e)//总表单
        { 
            load_info();
            yinsu();
            pingjia();
            Gaowei();
            yishi();
            BindMZfangshi();
            baocunjiemian();
            BindMZfangshi();
          
           
        }
        //保存的页面
        private void baocunjiemian()
        {
            DataTable dt = dal.Getzhiqingtys(patID1);
            //dt4 = dal.Getzhiqingtys(patID1);
            if (dt.Rows.Count == 0)
            {

                DataTable em = emr.GetemrbyPatid(zhuyaunhjcs);
                if (em.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    //patheight patweight  patdpm  pattmd  oname
                    textBoxintroduce.Text = em.Rows[0]["zhusu"].ToString();
                 }

            }
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (Convert.ToString(dr["rqdate"]).Contains("全身麻醉")) checkBox10.Checked = true;
                if (Convert.ToString(dr["rqdate"]).Contains("全麻+硬膜外麻醉")) checkBox9.Checked = true;
                if (Convert.ToString(dr["rqdate"]).Contains("椎管内麻醉")) checkBox8.Checked = true;
                if (Convert.ToString(dr["rqdate"]).Contains("神经阻滞")) checkBox7.Checked = true;
                if (Convert.ToString(dr["rqdate"]).Contains("局部麻醉+强化")) checkBox6.Checked = true;
                if (Convert.ToString(dr["rqdate"]).Contains("其它")) checkBox11.Checked = true;
                //textBoxname.Text = dr["ZYNumber"].ToString();
                //textBoxage.Text = dr["age"].ToString();
                //textBoxkeshi.Text = dr["keshi"].ToString();
                //textBoxsex.Text = dr["sex"].ToString();
                //textBoxzhuyuanhao.Text = dr["zhuyaunhao"].ToString();
                textBoxintroduce.Text = dr["jibingjieshao"].ToString();
                textBoxmazui.Text = dr["mazui"].ToString();
                textBoxzhenduan.Text = dr["tidaifangan"].ToString();
                comboBoxgaowei.Text = dr["fanganpingjia"].ToString();
                textBoxmazuifangfa.Text = dr["zhenduan"].ToString();
                comboBoxpingjia.Text = dr["gaoweiyinsu"].ToString();
                comboBox1.Text = dr["yishiqianzi"].ToString();
                //textBoxzhenduan.Text = dr["zhenduan"].ToString();
                //dtVisitDate.Text = dr["caozuoshijian"].ToString();
                //comboBox1.Text = dr["yishiqianzi"].ToString();
                //comboBoxgaowei.Text = dr["gaoweiyinsu"].ToString();
                //dateTimePicker1.Text = dr["rqdate"].ToString();
            }
            
        }
        //麻醉方式
        private void BindMZfangshi()
        {
            DataTable dtMZfs = dal.GetMazuiFangfa();
            textBoxmazuifangfa.Items.Clear();
          foreach(DataRow dr in dtMZfs.Rows)//循环dt表里数组行。
          {
              textBoxmazuifangfa.Items.Add(dr["name"].ToString());

          }

      }
        //替代方案
        private void yinsu()
        {
            DataTable dtYS = dal.GetMazuiFangfa();
            textBoxmazuifangfa.Items.Clear();
            foreach (DataRow dr in dtYS.Rows)
            {
                textBoxmazuifangfa.Items.Add(dr["name"].ToString());
            }
        }
        //评价
        private void pingjia()
        {
            DataTable dtpj = dal.Getpingjia();
            comboBoxpingjia.Items.Clear();
            foreach (DataRow dr in dtpj.Rows)
            {
                comboBoxpingjia.Items.Add(dr["name"].ToString());
            }
        }
        //高危因素
        private void Gaowei()
        {
            DataTable dtMZYS = dal.GetGaoWei();
            comboBoxgaowei.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
                comboBoxgaowei.Items.Add(dr["name"].ToString());
            }
        }
        //绑定医师
        private void yishi()
        {
            DataTable dtMZYS = dal.GetAllMZYS();
            comboBox1.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
                comboBox1.Items.Add(dr["user_name"].ToString());
            }
        }

        private void load_info()//基本信息
        {
            DataTable dt = dal.GetMazuizhiqingshu(patID1);//创建dt表取得查询结果集
           
            DataRow dr1 = dt.Rows[0];//取出结果集中第一行所有数据
            textBoxname.Text = dr1["Patname"].ToString();//将dr1里的字段这个对象转换成字符串,并赋给textbox1这个TextBox控件的Text
            textBoxage.Text = dr1["Patage"].ToString();
            textBoxkeshi.Text = dr1["Patdpm"].ToString();
            textBoxsex.Text = dr1["Patsex"].ToString();
            textBoxzhuyuanhao.Text = dr1["PatID"].ToString();
            // textBoxjieshao.Text = dr1[""].ToString();
            textchuanghao.Text = dr1["patbedno"].ToString();
            textBoxzhenduan.Text = dr1["Pattmd"].ToString();
            textBoxmazui.Text = dr1["Amethod"].ToString();
            //textBoxAmethod.Text = dr1["Amethod"].ToString();
            textBoxmazuifangfa.Text = dr1["Amethod"].ToString();
            dtVisitDate.Text = paytime;
            zhuyaunhjcs = dt.Rows[0]["PatZhuYuanID"].ToString();




        }
        //麻醉打印
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            a = 0;
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            //printPreviewDialog1.WindowState = FormWindowState.Maximized;
            ////printDocument1.DefaultPageSettings.PaperSize =
            ////    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);
          
        }


        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        //打印界面
        #region
        int a = 0;


        public void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("宋体", 11);//普通字体
            Font ptzt1= new Font("新宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 18, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("宋体", 11, FontStyle.Bold);//加粗11号
            Font ptzt5 = new Font("宋体", 16, FontStyle.Bold);//加粗16号
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            int y = 40; int x = 65; int y1 = 0;
           
            if (a < 1)
            {
                string title = "新疆医科大学附属第五医院";
                e.Graphics.DrawString(title, ptzt3, Brushes.Black, new Point(x + 180, y));
                y = y + 35;
                string title1 = "麻醉知情同意书";
                e.Graphics.DrawString(title1, ptzt5, Brushes.Black, new Point(x + 240, y));
                y = y + 40; y1 = y + 15;
                string title2 = "尊敬的患者/患者亲属/法定监护人/授权委托人：";
                e.Graphics.DrawString(title2, ptzt, Brushes.Black, new Point(x, y));
                y = y + 20;
                string title3 = "您好！根据您目前的病情，我们特向您详细介绍和说明如下内容，同时对您的疑问和要求进行咨询和答";
                e.Graphics.DrawString(title3, ptzt1, Brushes.Black, new Point(x + 15, y));
                y = y + 20;
                string title4 = "复，以帮助您了解相关知识,做出选择.";
                e.Graphics.DrawString(title4, ptzt1, Brushes.Black, new Point(x, y));
                y = y + 20;
                e.Graphics.DrawString("▪ 您是否需要文化援助：（□不需要 □需要 □盲文 □手语 □翻译 等 ）", ptzt1, Brushes.Black, new Point(x, y));
                if (checkBox1.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 172, y));
                if (checkBox2.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 233, y));
                if (checkBox3.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 283, y));
                if (checkBox4.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 333, y));
                if (checkBox5.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 383, y));
                y = y + 20;
                //横线
                for (int i = 0; i < 2; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 0, y + i * 50 + 0, x + 660, y + i * 50 + 0);

                }
                //竖线
                e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 900);

                e.Graphics.DrawLine(Pens.Black, x + 660, y - 0, x + 660, y + 900);
                //短横线
                e.Graphics.DrawLine(Pens.Black, x + 75, y + 22, x + 160, y + 22);
                e.Graphics.DrawLine(Pens.Black, x + 297, y + 22, x + 329, y + 22);
                e.Graphics.DrawLine(Pens.Black, x + 45, y + 43, x + 120, y + 43);
                e.Graphics.DrawLine(Pens.Black, x + 297, y + 43, x + 329, y + 43);
                e.Graphics.DrawLine(Pens.Black, x + 450, y + 43, x + 550, y + 43);
                y = y + 7;
                e.Graphics.DrawLine(Pens.Black, x + 160, y + 65, x + 450, y + 65);
                e.Graphics.DrawLine(Pens.Black, x + 160, y + 89, x + 630, y + 89);
                e.Graphics.DrawLine(Pens.Black, x + 230, y + 111, x + 310, y + 111);
                e.Graphics.DrawLine(Pens.Black, x + 0, y + 893, x + 660, y + 893);
                //查询
                // e.Graphics.DrawString("手术日期:" + dateTimeP.Value.ToString("yyyy-MM-dd HH:mm"), ptzt11, Brushes.Black, new Point(x + 5, y + 0));
                e.Graphics.DrawString("患者姓名: " + textBoxname.Text, ptzt11, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("年龄: " + textBoxage.Text, ptzt11, Brushes.Black, new Point(x + 250, y + 0));
                e.Graphics.DrawString("床号: " + textchuanghao.Text, ptzt11, Brushes.Black, new Point(x + 400, y + 0));
                
                y = y + 20;
                e.Graphics.DrawString("科室: " + textBoxkeshi.Text, ptzt11, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("性别: " + textBoxsex.Text, ptzt11, Brushes.Black, new Point(x + 250, y + 0));
                e.Graphics.DrawString("住院号: " + textBoxzhuyuanhao.Text, ptzt11, Brushes.Black, new Point(x + 400, y + 0));
                y = y + 30;
                string title5 = "•疾病介绍：";
                e.Graphics.DrawString(title5, ptzt4, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawString(" 患者因 " + textBoxintroduce.Text, ptzt11, Brushes.Black, new Point(x + 100, y + 0));
                string title6 = "收住院，入院后经完善相关";
                e.Graphics.DrawString(title6, ptzt11, Brushes.Black, new Point(x + 450, y));
                y = y + 23;
                string title7 = "检查，目前初步诊断";
                e.Graphics.DrawString(title7, ptzt11, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString(textBoxzhenduan.Text, ptzt11, Brushes.Black, new Point(x + 172, y + 0));
                y = y + 23;
                e.Graphics.DrawString("根据现有诊疗规范，您需要在（ " + textBoxmazui.Text + "）麻醉下进行手术。", ptzt11, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("•麻醉的作用及其目的：", ptzt4, Brushes.Black, new Point(x + 10, y));
                //1.
                y = y + 23;
                e.Graphics.DrawString("1.麻醉作用的产生主要是利用麻醉药使神经系统中某些部位受到抑制的结果。临床麻醉的主要工作", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("是：尽可能消除手术疼痛，监测和调控生理功能，并为手术创造条件。麻醉的风险性与手术大小并非", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("完全一致。复杂的手术固然可使麻醉的风险性增加，而有时手术并非很复杂，但由于患者的病情及并", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("存疾病的影响，可为麻醉带来更大的风险。", ptzt1, Brushes.Black, new Point(x + 10, y));
                //2
                y = y + 23;
                e.Graphics.DrawString("2.为了尽可能保证手术时无痛和医疗安全，手术需要在麻醉和严密监测条件下进行。患者有权选择", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("适合自己的麻醉方法，但根据患者的病情和手术需要，麻醉医师建议患者选择以下麻醉方法，在病情", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("变化或术式改变时而不需要与家属再次沟通允许改变麻醉方式。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString(" □全身麻醉; □全麻+硬膜外麻醉; □椎管内麻醉; □神经阻滞; □局部麻醉+强化; □其他", ptzt1, Brushes.Black, new Point(x + 10, y));
                if (checkBox10.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 18, y));
                if (checkBox9.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 105, y));
                if (checkBox8.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 239, y));
                if (checkBox7.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 339, y));
                if (checkBox6.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 425, y));
                if (checkBox11.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x +547, y));
                y = y + 23;
                //3
                e.Graphics.DrawString("3.为了我的手术安全，麻醉医师将严格遵循麻醉操作规范和用药原则;在患者手术麻醉期间，对异", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("常情况及时进行治疗和处理。但任何麻醉方法都存在一定风险性，根据目前技术水平尚难以安全避", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("免发生一些医疗意外或并发症。如合并其他疾病，麻醉可诱发或加重已有症状，相关并发症和麻醉", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("风险性也显著增加。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                //4
                e.Graphics.DrawString("4.为了减轻患者术后疼痛，促进康复，麻醉医师向患者介绍术后疼痛治疗的优点，方法和可能引起", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("的意外与并发症,建议患者进行术后疼痛治疗。疼痛治疗是自愿选择和自费项目。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("•麻醉潜在风险包括但不限于以下：", ptzt4, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                //1
                e.Graphics.DrawString("1、与原发病或并存疾病相关：脑出血、脑梗塞、脑水肿;严重心律失常、心肌缺血/梗死、心力衰", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("竭;肺不张、肺水肿、肺栓塞、呼吸衰竭;肾功能障碍或衰竭等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("2、与药物相关;过敏反应或过敏性休克，局麻药全身毒性反应和神经毒性，严重呼吸和循环抑制，", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("呼吸心跳停止，器官功能损坏或衰竭，精神异常，恶性高热，甚至危及生命等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("3、与不同麻醉方法和操作相关：", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("1）神经阻滞：局部血肿，气胸，神经功能损害，喉返神经麻痹，全脊髓麻醉，肢体伤残，甚至呼", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("吸心跳停止，危及生命等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("2）椎管内麻醉：腰背痛，尿失禁或尿潴留，腰麻后头痛，颅神经麻痹，脊神经或脊髓损伤，呼吸", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("和循环抑制，全脊麻甚至循环骤停，硬膜外血肿，脓肿甚至截瘫，穿刺部位或椎管内感染，硬膜", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("外导管滞留或断裂，麻醉不完善或失败等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("3）全身麻醉：呕吐、误吸,喉痉挛，困难插管，气管和支气管痉挛，吸道梗阻，气管内插管失败，", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("再次插管，术后咽痛、环杓关节脱位，牙齿损伤或脱落，口唇、舌、咽喉、声带、气管和支气管损", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                e.Graphics.DrawString("伤,苏醒延迟、术中知晓和术后回忆等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                //4
                e.Graphics.DrawString("4、与有创伤性监测相关：局部血肿，纵膈血/气肿，血/气胸，感染，心律失常，血栓形成或肺栓", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 23;
                e.Graphics.DrawString("塞，心包填塞，导管打结或断裂，胸导管损伤，神经损伤，坏死等。", ptzt1, Brushes.Black, new Point(x + 10, y));
                y = y + 23;
                //5
                e.Graphics.DrawString("5、与输液、输血及血液制品相关：血源性传染病，致热源反应，过敏反应，凝血或溶血性疾病等。", ptzt1, Brushes.Black, new Point(x + 25, y));
                y = y + 21;
                //fenye
              //for(int i=0; i<1;i++)
              //{
                if(a < 1)
                {
                a++;
                e.HasMorePages = true;
                return ;
                }
            }
            else if(a > 0)
            {
            //第二页
                //竖线
            y= y + 20;
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 660, y - 0, x + 660, y + 1035);
            //横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 660, y + 0);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 455, x + 660, y + 455);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 1035, x + 660, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 950, x + 660, y + 950);
            e.Graphics.DrawLine(Pens.Black, x + 134, y + 450, x + 250, y + 450);
            e.Graphics.DrawLine(Pens.Black, x + 305, y + 277, x + 600, y + 277);
            e.Graphics.DrawLine(Pens.Black, x + 161, y + 302, x + 502, y + 302);
            e.Graphics.DrawLine(Pens.Black, x + 37, y + 850, x + 142, y + 850);
            e.Graphics.DrawLine(Pens.Black, x + 37, y + 805, x + 142, y + 805);
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 895, x + 222, y + 895);
            e.Graphics.DrawLine(Pens.Black, x + 435, y + 895, x + 503, y + 895);
            e.Graphics.DrawLine(Pens.Black, x + 518, y + 895, x + 548, y + 895);
            e.Graphics.DrawLine(Pens.Black, x + 563, y + 895, x + 593, y + 895);
            e.Graphics.DrawLine(Pens.Black, x + 605, y + 895, x + 635, y + 895);
            e.Graphics.DrawLine(Pens.Black, x + 62, y + 941, x + 195, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 276, y + 941, x + 368, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 435, y + 941, x + 503, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 518, y + 941, x + 548, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 563, y + 941, x + 593, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 605, y + 941, x + 635, y + 941);
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 1026, x + 222, y + 1026);
            e.Graphics.DrawLine(Pens.Black, x + 435, y + 1026, x + 503, y + 1026);
            e.Graphics.DrawLine(Pens.Black, x + 518, y + 1026, x + 548, y + 1026);
            e.Graphics.DrawLine(Pens.Black, x + 563, y + 1026, x + 593, y + 1026);
            e.Graphics.DrawLine(Pens.Black, x + 605, y + 1026, x + 635, y + 1026);
            y = y + 10;
            e.Graphics.DrawString("6、与外科手术相关：失血性休克，严重迷走神经反射引起的呼吸心跳骤停，压迫心脏或大血管引", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("起的严重循环抑制及其并发症等。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("7、与急诊手术相关：以上医疗意外和并发症均可发生于急诊手术病人，且发生率较择期手术明显", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("升高。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("8、手术室外的麻醉：以上医疗意外和并发症均可发生于手术室外的麻醉手术病人，且危险性较手", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("术室内麻醉明显升高。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("9、与术后镇痛相关：呼吸、循环抑制，恶心呕吐，皮肤瘙痒，便秘，镇痛不全，硬膜外导管脱出等", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("10、其他不可预见的风险和并发症。", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("11、患者自身存在高危因素：(       " + comboBoxgaowei.Text + "       )(如患者自身不存在高危因素，则删除此条款）", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("以上这些高危因素，可能导致麻醉风险更加大，或者在麻醉中或者在麻醉后出现相关的病情加", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("重或心脑血管意外，甚至死亡。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("如不实施该麻醉方式，可采取的替代方案（" + textBoxmazuifangfa.Text, ptzt1, Brushes.Black, new Point(x + 35, y));
            e.Graphics.DrawString( "),", ptzt1, Brushes.Black, new Point(x + 600, y));
            y = y + 23;
            e.Graphics.DrawString("该替代方案的评价：（" + comboBoxpingjia.Text, ptzt1, Brushes.Black, new Point(x + 20, y));
            e.Graphics.DrawString("),", ptzt1, Brushes.Black, new Point(x + 500, y));
            y = y + 23;
            e.Graphics.DrawString("我们将以高度的责任心，认真负责的执行麻醉操作规程，做好抢救物品的准备及手术过程中的监", ptzt1, Brushes.Black, new Point(x + 35, y));
            y = y + 23;
            e.Graphics.DrawString("测。一旦发生麻醉意外或并发症，我们将积极采取相应的抢救措施。但由于医疗水平的局限性及个人", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("体质的差异，意外风险不能做到完全避免，且不能确保救治完全成功。可能会出现残疾、组织器官损", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("伤导致功能阻碍、甚至死亡等严重不良后果，及其他不可预见且未能告知的特殊情况，敬请谅解。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("我已向患方解释了此知情同意书的全部内容和条款。", ptzt1, Brushes.Black, new Point(x + 35, y));
            y = y + 33;
            e.Graphics.DrawString("麻醉医师签字：    "  +"                                       签名日期   "+ dateTimePicker1.Text, ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 33;
            e.Graphics.DrawString("患者/患者家属/法定监护人/授权人（姓名）", ptzt4, Brushes.Black, new Point(x + 10, y));
            //e.Graphics.DrawString("确认：        ", ptzt4, Brushes.Black, new Point(x +479, y));
            y = y + 23;
            e.Graphics.DrawString("▪麻醉医师已对我的病情、病史进行了详细询问。我对麻醉医师所告知的、因受医学科学技术等各类条", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("件限制、目前尚难以完全避免麻醉意外和并发症表示理解。相信麻醉医师会采取积极有效措施加以避", ptzt1, Brushes.Black, new Point(x +10, y));
            y = y + 23;
            e.Graphics.DrawString("免。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("▪麻醉医生已经告知我将要施行的麻醉及麻醉后可能发生的并发症和风险、可能存在的其他麻醉方法并", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("且解答了我关于此次麻醉的相关问题。", ptzt1, Brushes.Black, new Point(x + 10, y));
            y = y + 23;
            e.Graphics.DrawString("▪我同意在治疗中医生可以根据我的病情对预定的麻醉方式做出调整。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("▪我理解在我的麻醉期间需要多位医生共同进行。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("▪我并未得到治疗百分之百无风险的许诺。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("基于以上陈诉，本人授权麻醉医师：", ptzt4, Brushes.Black, new Point(x + 20, y));
            y = y + 23;
            e.Graphics.DrawString("▪麻醉医师根据病情的需要决定将患者送回病房、麻醉复苏室或重症监护室病房。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("▪麻醉医师在病情治疗必要时使用医疗保险范围以外的麻醉和抢救药品及物品。保证承担所有费用。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("▪在术中或术后发生紧急情况下，为保障本人的生命安全，麻醉医师有权按照医学常规予以紧急处置", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("和全力抢救，更改并选择最适宜的麻醉与手术方案实施必要的抢救。", ptzt1, Brushes.Black, new Point(x + 20, y));
            y = y + 23;
            e.Graphics.DrawString("▪我               接受麻醉和手术室方案，并对产生的不良后果已做好充分的思想准备。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("（请患者签署“同意”同意字样） ", ptzt1, Brushes.Black, new Point(x + 20, y));
            y = y + 23;
            e.Graphics.DrawString("▪我               接受麻醉和手术室方案，并且愿意承担因拒绝施行麻醉及手术而发生的一切后果。", ptzt1, Brushes.Black, new Point(x + 15, y));
            y = y + 23;
            e.Graphics.DrawString("(请患者签署“不同意”同意字样） ", ptzt1, Brushes.Black, new Point(x + 20, y));
            y = y + 23;
            e.Graphics.DrawString("患者签名：              "  + "                        签名日期:          年    月    日   ", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("如果患者无法签署知情同意书，请患者的监护人、近亲属、授权委托人在此处签字：", ptzt1, Brushes.Black, new Point(x + 20, y));
            y = y + 23;
            e.Graphics.DrawString("签字：         "  + "         与患者关系:             签名日期：         年    月    日   " , ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 30;
            e.Graphics.DrawString("我已提供了（                              ）（请填写盲文、手语或患者具体语言）的翻译， ", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
            e.Graphics.DrawString("我翻译的内容包括此知情同意书中的内容及医师与患者或患者委托人的一切语言或书面信息。 ", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 30;
            e.Graphics.DrawString("翻译签字：              "  + "                        签名日期：         年    月    日   ", ptzt1, Brushes.Black, new Point(x + 25, y));
            y = y + 23;
     
           }
           
           
             
           
           
        }
        #endregion
        //打印button方法
        private void button1_Click(object sender, EventArgs e)
        {

            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            //PrintDialog pd = new PrintDialog();

            //pd.Document = printDocument1;
            //if (DialogResult.OK == pd.ShowDialog()) //如果确认，将会覆盖所有的打印参数设置
            //{
            //    //页面设置对话框（可以不使用，其实PrintDialog对话框已提供页面设置）
            //    PageSetupDialog psd = new PageSetupDialog();
            //    psd.Document = printDocument1;
            //    // this.pageSetupDialog1.ShowDialog();
            //    //BindJHDian();
            //    //pictureBox4.Refresh();
            //    //pictureBox3.Refresh();
            //    //pictureBox2.Refresh();
            //    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //        printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            //    printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //    printDocument1.DefaultPageSettings.PaperSize =
            //               new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            //    //if (printPreviewDialog1.ShowDialog() == DialogResult.OK) { }
            //    //    printDocument1.Print();
            //}
               
           
        }
        //void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{

        //}
        //{
        //    int y = printDocument1_PrintPage();


        //    if (y > 900)
        //    {
        //        e.HasMorePages = true;

        //    }
        //}  
        //保存界面
        #region
        private void button2_Click(object sender, EventArgs e)
        {
         
            if (textBoxintroduce.Text.IsNullOrEmpty())
            {
                MessageBox.Show("患者病因没有填写!");
                textBoxintroduce.Focus();
                return;
            }
            if(textBoxzhenduan.Text.IsNullOrEmpty())
            {
                MessageBox.Show("患者初步诊断没有填写!");
                textBoxzhenduan.Focus();
                return;
            }
            if (textBoxmazui.Text.IsNullOrEmpty())
            {
                MessageBox.Show("麻醉方式没有填写!");
                textBoxmazui.Focus();
                return;
            }
                 if (comboBoxgaowei.Text.IsNullOrEmpty())
            {
                MessageBox.Show("高危因素没有填写，没有请选择“无”!");
                comboBoxgaowei.Focus();
                return;
            }
                 if (textBoxmazuifangfa.Text.IsNullOrEmpty())
                 {
                     MessageBox.Show("替代方案没有填写，没有请选择“无”!");
                     textBoxmazuifangfa.Focus();
                     return;
                 }
                 if (comboBoxpingjia.Text.IsNullOrEmpty())
            {
                MessageBox.Show("代替方案的评价，没有请选择“无”!");
                comboBoxpingjia.Focus();
                return;
            }

                 if (comboBox1.Text.IsNullOrEmpty())
            {
                MessageBox.Show("麻醉医师不能为空!");
                comboBox1.Focus();
                return;
            }
                 baocun();
         }
        private void baocun()
        {
            Dictionary<string, string> sq = new Dictionary<string, string>();
            int result = 0;
            string additem = "";
            string additem1 = "";
            if (button2.Enabled)
            {
                sq.Add("IsRead", "0");
            }
            else
            {
                sq.Add("IsRead", "1");
            }
            sq.Add("ZYNumber", textBoxname.Text);
            sq.Add("sex", textBoxsex.Text);
            sq.Add("keshi", textBoxkeshi.Text);
            sq.Add("age", textBoxage.Text);
            sq.Add("caozuoshijian", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            sq.Add("zhuyuanhao", patID1);
            sq.Add("jibingjieshao", textBoxintroduce.Text);
            sq.Add("mazui", textBoxmazui.Text);
            sq.Add("tidaifangfa", textBoxmazuifangfa.Text);
            sq.Add("zhenduan", textBoxzhenduan.Text);
            sq.Add("gaoweixinsu", comboBoxgaowei.Text);
            sq.Add("yishiqianzi", comboBox1.Text);
            additem = "";
            if (checkBox10.Checked) additem += "全身麻醉";
            if (checkBox9.Checked) additem += "全麻+硬膜外麻醉";
            if (checkBox8.Checked) additem += "椎管内麻醉";
            if (checkBox7.Checked) additem += "神经阻滞";
            if (checkBox6.Checked) additem += "局部麻醉+强化";
            if (checkBox11.Checked) additem+= "其它";
            sq.Add("mazuifangan", additem);
            //additem1 = "";
            //if (checkBox10.Checked) additem1 += "全身麻醉";
            //if (checkBox9.Checked) additem1 += "全麻+硬膜外麻醉";
            //if (checkBox8.Checked) additem1 += "椎管内麻醉";
            //if (checkBox7.Checked) additem1 += "神经阻滞";
            //if (checkBox6.Checked) additem1 += "局部麻醉+强化";
            //if (checkBox11.Checked) additem1 += "其它";
            //sq.Add("mazuifangan", additem1);

            sq.Add("fanganpingjia", comboBoxpingjia.Text);
            sq.Add("rqdate", Convert.ToDateTime(dateTimePicker3.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt = dal.Getzhiqingtys(patID1);

            if (dt.Rows.Count > 0)
                result = dal.Updatezhiqingtys(sq);
            else
                result = dal.Insertzhiqingtys(sq);
            if (result > 0)
            {
                MessageBox.Show("保存成功！");
            }


        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
