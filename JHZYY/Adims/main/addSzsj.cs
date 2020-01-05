using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;

namespace main
{
    public partial class addSzsj : Form
    {
        #region <<Members>>

        adims_DAL.AdimsProvider apro = new AdimsProvider();
        AdimsController acon = new AdimsController();
        PACU_DAL PDAL = new PACU_DAL();
        int MZID;
        int type;
        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="l"></param>
        public addSzsj(int mzid,int type1)
        {
            type = type1;           
            MZID = mzid;
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.szsj s = new adims_MODEL.szsj();
                s.D = DateTime.Now;
                s.Name = textBox1.Text;
                DataTable dt = PDAL.GetAdims_szsjListInt(s.Name);
                string szid="";
                if (dt.Rows.Count>0)
                {
                    szid = dt.Rows[0][0].ToString();
                }
                else
                {
                    szid =string.Empty;
                }
                
                int i = acon.InsertIntoSZSJ(MZID, textBox1.Text, DateTime.Now, type, szid);
                if (i > 0)
                {                   
                    datagridBind(); 
                }
                              
            }

        }

        private void datagridBind()
        {
            DataTable dt = apro.GetSZSJbytype(MZID,type);
            dataGridView1.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    int j = acon.deleteSZSJ(ID, type);
                    if (j > 0)
                    {
                        datagridBind();                        
                    }
                }
                else
                    MessageBox.Show("请选择已添加的术中事件！");
            }
        }
        
        private void addszsj_Load(object sender, EventArgs e)
        {
            datagridBind();
            DataTable DT1= PDAL.GetAdims_szsjListSingleType();
            for (int i = 0; i < DT1.Rows.Count; i++)
			{
                cmbType.Items.Add(DT1.Rows[i][0].ToString());
			}
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (cmbType.Text.Trim()!="")
            {
                DataTable dt= PDAL.GetAdims_szsjListByType(cmbType.Text.Trim());               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox1.Items.Add(dt.Rows[i]["Name"].ToString());
                }
            }
            //else 
            //{                
            //    listBox1.Items.AddRange(new object[] {
            //"肺动脉阻断",
            //"上腔静脉阻断",
            //"下腔静脉阻断",
            //"血管阻断",
            //"牵拉",
            //"压迫心包",
            //"压迫主动脉",
            //"吻合",
            //"放腹水",
            //"放胸水",
            //"单肺通气",
            //"关胸",
            //"术中病理送标本",
            //"下标本",
            //"双肺通气",
            //"上肺CPAP",
            //"关胸",
            //"桡动脉穿刺置管",
            //"硬膜外穿刺置管",
            //"锁骨下静脉穿刺置管",
            //"蛛网膜下腔穿刺",
            //"纤维支气管镜检查",
            //"纤维支气管镜引导插管",
            //"吸痰",
            //"腹腔冲洗",
            //"胸腔冲洗",
            //"导尿",
            //"人工气腹",
            //"肝门阻断",
            //"分皮瓣",
            //"开放肝门",
            //"切皮",
            //"气腹",
            //"出血",
            //"双腔管换单腔气管导管",
            //"改变体位",
            //"腹腔化疗",
            //"换单腔管",
            //"探查",
            //"头高位",
            //"气腹结束",
            //"头低位",
            //"翻身",
            //"缝皮",
            //"阻断右肝门",
            //"膨肺",
            //"换双腔管",
            //"椎旁阻滞",
            //"环甲膜穿刺",
            //"阻断肝门",
            //"局麻",
            //"阻断肺动脉",
            //"气管切开",
            //"门静脉阻断",
            //"足背动脉穿刺置管",
            //"无创心功能监测",
            //"加强钢丝导管气管插管",
            //"换气管切开套管",
            //"清扫腋窝淋巴结",
            //"冲洗",
            //"胸导管结扎",
            //"回肠造瘘",
            //"取皮",
            //"植皮",
            //"上驱血带",
            //"转移皮瓣",
            //"松驱血带",
            //"还纳",
            //"止血",
            //"肺动脉开放",
            //"颈内静脉穿刺",
            //"空肠造瘘",
            //"打石膏",
            //"插胃管",
            //"开胸",
            //"断胃",
            //"粘连松解",
            //"分离血管",
            //"游离转移皮瓣",
            //"血管吻合",
            //"经鼻插管.",
            //"热灌注",
            //"血气、离子、血糖、乳酸",
            //"盆腔淋巴结清扫",
            //"骨水泥",
            //"胆肠吻合",
            //"清扫颈淋巴结",
            //"压迫下腔静脉",
            //"连接镇痛泵",
            //"膀胱造瘘",
            //"胸腔镜探查",
            //"植入扩张器",
            //"左肺动脉阻断",
            //"触压颈动脉窦",
            //"胸膜破裂",
            //"控制性降压",
            //"頚内静脉穿刺",
            //"测血糖",
            //"压迫心脏",
            //"闭孔神经阻滞",
            //"下腔静脉开放",
            //"取出瘤栓",
            //"造瘘",
            //"刺激肿瘤",
            //"室早二联律",
            //"阻断肾蒂",
            //"开放肾蒂",
            //"胃镜检查",
            //"回肠造口还纳",
            //"劈胸骨",
            //"修补心包",
            //"频发室上性早搏",
            //"左腹股沟淋巴结活检",
            //"修补",
            //"上腔静脉部分阻断",
            //"上腔静脉开放",
            //"阻断第一肝门",
            //"松开",
            //"等冰冻",
            //"肠镜检查",
            //"折刀位",
            //"肝脏射频",
            //"胃镜探查",
            //"腹腔镜探查",
            //"胸腔闭式引流",
            //"房早",
            //"房颤",
            //"头高脚低位",
            //"清扫腹股沟淋巴结",
            //"术毕，发现气管内导管完全 脱出",
            //"面罩辅助通气",
            //"再次行气管插管",
            //"气管内导管脱出",
            //"膀胱镜检",
            //"肝门开放",
            //"插尿管困难",
            //"频发室早",
            //"腹腔减压",
            //"暖风毯加温",
            //"取环",
            //"左侧卧位",
            //"平卧位",
            //"膀胱截石位",
            //"肠切除吻合术",
            //"回肠造口",
            //"术中B超",
            //"室早伴长间歇",
            //"腹腔淋巴结清扫",
            //"阵发性室上速",
            //"术中肠镜",
            //"三度房室传导阻滞",
            //"门静脉探查",
            //"冲洗引流",
            //"打开心包",
            //"锁骨下静脉穿刺",
            //"困难插管",
            //"腹壁补片植入",
            //"触压肾上腺",
            //"室早",
            //"电切",
            //"注射副肾盐水",
            //"血糖",
            //"取出扩张器",
            //"植入假体",
            //"乙状结肠造瘘",
            //"游离背阔肌",
            //"喉罩置入",
            //"加快输液",
            //"病人核对(麻醉前）",
            //"病人核对(暂停）",
            //"病人核对(手术后）",
            //"体位肢体保护",
            //"剥离肿瘤",
            //"扩张器取出",
            //"假体置入",
            //"输尿管支架置入",
            //"身体保护",
            //"耳眼保护",
            //"眼耳保护",
            //"上肢护垫保护",
            //"下肢护垫保护",
            //"吸痰、膨肺",
            //"瑞芬1mg/250ml ivgtt",
            //"阻断门静脉",
            //"开放门静脉",
            //"胃镜治疗",
            //"取出假体",
            //"局麻下气管切开",
            //"更换气管套管",
            //"锁穿管胆管引流",
            //"腹腔关闭",
            //"胸腔关闭",
            //"置入喉罩",
            //"下标本.",
            //"清扫盆腔淋巴结",
            //"肝门阻断15分钟后开放5分钟 循环",
            //"凯纷50mg 静脉滴注",
            //"西地兰0.2mgiv",
            //"心动过缓",
            //"心动过速",
            //"ST段低",
            //"单腔管换喉罩",
            //"电温毯保温",
            //"开放左肺动脉",
            //"去甲肾上腺素12mg泵入",
            //"左无名静脉阻断",
            //"左无名静脉开放",
            //"赛格恩5mg 静脉滴注",
            //"凯纷50mg 静脉滴注",
            //"磷酸肌酸钠2g 静脉滴注",
            //"磷酸肌酸钠1g 静脉滴注",
            //"凯纷100mg 静脉滴注",
            //"送冰冻",
            //"吻合气管",
            //"右美托咪啶 静脉泵入",
            //"管状胃重建",
            //"取出BJ管",
            //"取活检",
            //"全身麻醉",
            //"特殊方法气管插管术",
            //"麻醉后监护室（PACU）",
            //"术后镇痛",
            //"麻醉中监测",
            //"静脉穿刺置管术",
            //"血液加温治疗",
            //"静脉输液",
            //"静脉注射",
            //"氧气吸入",
            //"气管插管",
            //"吸痰护理",
            //"三通开关",
            //"留置针第四代",
            //"胃管（进口）",
            //"鼻饲管置管",
            //"奥美输注装置",
            //"延长管",
            //"葡萄糖-简易血糖测定",
            //"动脉穿刺置管术",
            //"压力传导组",
            //"气管插管（Safety-flex）",
            //"支气管双腔管",
            //"气管切开插管及附件",
            //"椎管内麻醉",
            //"气管插管术",
            //"中心静脉穿刺置管术"});
            //}
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = PDAL.GetAdims_szsjListBySuoxie(tbPinyin.Text.Trim());
                listBox1.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr[0].ToString());
                }
                tbPinyin.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        
    }
}
