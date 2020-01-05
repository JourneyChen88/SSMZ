namespace main.PACU_LEVEL
{
    partial class PACU_AddSJ
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjlid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sjname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sjtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 25);
            this.button1.TabIndex = 18;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(372, 292);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "开腹",
            "关腹",
            "肺动脉阻断",
            "上腔静脉阻断",
            "下腔静脉阻断",
            "血管阻断",
            "牵拉",
            "压迫心包",
            "压迫主动脉",
            "吻合",
            "放腹水",
            "放胸水",
            "单肺通气",
            "关胸",
            "术中病理送标本",
            "下标本",
            "双肺通气",
            "上肺CPAP",
            "关胸",
            "桡动脉穿刺置管",
            "硬膜外穿刺置管",
            "锁骨下静脉穿刺置管",
            "蛛网膜下腔穿刺",
            "纤维支气管镜检查",
            "纤维支气管镜引导插管",
            "吸痰",
            "腹腔冲洗",
            "胸腔冲洗",
            "导尿",
            "人工气腹",
            "肝门阻断",
            "分皮瓣",
            "开放肝门",
            "切皮",
            "气腹",
            "出血",
            "双腔管换单腔气管导管",
            "改变体位",
            "上止血带",
            "松止血带",
            "腹腔化疗",
            "换单腔管",
            "探查",
            "头高位",
            "气腹结束",
            "头低位",
            "翻身",
            "缝皮",
            "阻断右肝门",
            "膨肺",
            "换双腔管",
            "椎旁阻滞",
            "环甲膜穿刺",
            "阻断肝门",
            "局麻",
            "阻断肺动脉",
            "气管切开",
            "门静脉阻断",
            "足背动脉穿刺置管",
            "无创心功能监测",
            "加强钢丝导管气管插管",
            "换气管切开套管",
            "清扫腋窝淋巴结",
            "冲洗",
            "胸导管结扎",
            "回肠造瘘",
            "取皮",
            "植皮",
            "上驱血带",
            "转移皮瓣",
            "松驱血带",
            "还纳",
            "止血",
            "肺动脉开放",
            "颈内静脉穿刺",
            "空肠造瘘",
            "打石膏",
            "插胃管",
            "开胸",
            "断胃",
            "粘连松解",
            "分离血管",
            "游离转移皮瓣",
            "血管吻合",
            "经鼻插管.",
            "热灌注",
            "血气、离子、血糖、乳酸",
            "盆腔淋巴结清扫",
            "骨水泥",
            "胆肠吻合",
            "清扫颈淋巴结",
            "压迫下腔静脉",
            "连接镇痛泵",
            "膀胱造瘘",
            "胸腔镜探查",
            "植入扩张器",
            "左肺动脉阻断",
            "触压颈动脉窦",
            "胸膜破裂",
            "控制性降压",
            "頚内静脉穿刺",
            "测血糖",
            "压迫心脏",
            "闭孔神经阻滞",
            "下腔静脉开放",
            "取出瘤栓",
            "造瘘",
            "刺激肿瘤",
            "室早二联律",
            "阻断肾蒂",
            "开放肾蒂",
            "胃镜检查",
            "回肠造口还纳",
            "劈胸骨",
            "修补心包",
            "频发室上性早搏",
            "左腹股沟淋巴结活检",
            "修补",
            "上腔静脉部分阻断",
            "上腔静脉开放",
            "阻断第一肝门",
            "松开",
            "等冰冻",
            "肠镜检查",
            "折刀位",
            "肝脏射频",
            "胃镜探查",
            "腹腔镜探查",
            "胸腔闭式引流",
            "房早",
            "房颤",
            "头高脚低位",
            "清扫腹股沟淋巴结",
            "术毕，发现气管内导管完全 脱出",
            "面罩辅助通气",
            "再次行气管插管",
            "气管内导管脱出",
            "膀胱镜检",
            "肝门开放",
            "插尿管困难",
            "频发室早",
            "腹腔减压",
            "暖风毯加温",
            "取环",
            "左侧卧位",
            "平卧位",
            "膀胱截石位",
            "肠切除吻合术",
            "回肠造口",
            "术中B超",
            "室早伴长间歇",
            "腹腔淋巴结清扫",
            "阵发性室上速",
            "术中肠镜",
            "三度房室传导阻滞",
            "门静脉探查",
            "冲洗引流",
            "打开心包",
            "锁骨下静脉穿刺",
            "困难插管",
            "腹壁补片植入",
            "触压肾上腺",
            "室早",
            "电切",
            "注射副肾盐水",
            "血糖",
            "取出扩张器",
            "植入假体",
            "乙状结肠造瘘",
            "游离背阔肌",
            "喉罩置入",
            "加快输液",
            "病人核对(麻醉前）",
            "病人核对(暂停）",
            "病人核对(手术后）",
            "体位肢体保护",
            "剥离肿瘤",
            "扩张器取出",
            "假体置入",
            "输尿管支架置入",
            "身体保护",
            "耳眼保护",
            "眼耳保护",
            "上肢护垫保护",
            "下肢护垫保护",
            "吸痰、膨肺",
            "瑞芬1mg/250ml ivgtt",
            "阻断门静脉",
            "开放门静脉",
            "胃镜治疗",
            "取出假体",
            "局麻下气管切开",
            "更换气管套管",
            "锁穿管胆管引流",
            "腹腔关闭",
            "胸腔关闭",
            "置入喉罩",
            "关腹.",
            "关胸.",
            "下标本.",
            "开腹.",
            "开胸.",
            "清扫盆腔淋巴结",
            "肝门阻断15分钟后开放5分钟 循环",
            "凯纷50mg 静脉滴注",
            "西地兰0.2mgiv",
            "心动过缓",
            "心动过速",
            "ST段低",
            "单腔管换喉罩",
            "电温毯保温",
            "开放左肺动脉",
            "去甲肾上腺素12mg泵入",
            "左无名静脉阻断",
            "左无名静脉开放",
            "赛格恩5mg 静脉滴注",
            "凯纷50mg 静脉滴注",
            "磷酸肌酸钠2g 静脉滴注",
            "磷酸肌酸钠1g 静脉滴注",
            "凯纷100mg 静脉滴注",
            "送冰冻",
            "吻合气管",
            "右美托咪啶 静脉泵入",
            "管状胃重建",
            "取出BJ管",
            "取活检",
            "全身麻醉",
            "特殊方法气管插管术",
            "麻醉后监护室（PACU）",
            "术后镇痛",
            "麻醉中监测",
            "静脉穿刺置管术",
            "血液加温治疗",
            "静脉输液",
            "静脉注射",
            "氧气吸入",
            "气管插管",
            "吸痰护理",
            "三通开关",
            "留置针第四代",
            "胃管（进口）",
            "鼻饲管置管",
            "奥美输注装置",
            "延长管",
            "葡萄糖-简易血糖测定",
            "动脉穿刺置管术",
            "压力传导组",
            "气管插管（Safety-flex）",
            "支气管双腔管",
            "气管切开插管及附件",
            "椎管内麻醉",
            "气管插管术",
            "中心静脉穿刺置管术"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(130, 331);
            this.listBox1.TabIndex = 13;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(183, 292);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(340, 225);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 21);
            this.textBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "事件名称";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(191, 226);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "事件类别";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjlid,
            this.sjname,
            this.sjtime});
            this.dataGridView1.Location = new System.Drawing.Point(136, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(353, 197);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // mzjlid
            // 
            this.mzjlid.DataPropertyName = "mzjldid";
            this.mzjlid.HeaderText = "麻醉记录单号";
            this.mzjlid.Name = "mzjlid";
            this.mzjlid.ReadOnly = true;
            this.mzjlid.Width = 120;
            // 
            // sjname
            // 
            this.sjname.DataPropertyName = "sjname";
            this.sjname.HeaderText = "药品名字";
            this.sjname.Name = "sjname";
            this.sjname.ReadOnly = true;
            // 
            // sjtime
            // 
            this.sjtime.DataPropertyName = "sjtime";
            this.sjtime.HeaderText = "用药时间";
            this.sjtime.Name = "sjtime";
            this.sjtime.ReadOnly = true;
            this.sjtime.Width = 120;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(193, 258);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(103, 21);
            this.dateTimePicker1.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "开始时间";
            // 
            // PACU_AddSJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 331);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "PACU_AddSJ";
            this.Text = "PACU_AddSJ";
            this.Load += new System.EventHandler(this.PACU_AddSJ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjlid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sjname;
        private System.Windows.Forms.DataGridViewTextBoxColumn sjtime;
    }
}