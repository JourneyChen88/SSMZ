namespace main
{
    partial class addszsj
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjldid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "事件类别";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(76, 237);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(106, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
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
            "下标本.",
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
            this.listBox1.Size = new System.Drawing.Size(182, 220);
            this.listBox1.TabIndex = 2;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "事件名称";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(256, 235);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 21);
            this.textBox1.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Location = new System.Drawing.Point(76, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 31);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "添加";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(276, 276);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 31);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 220);
            this.panel1.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjldid,
            this.name,
            this.time});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(182, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(333, 220);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // mzjldid
            // 
            this.mzjldid.DataPropertyName = "mzjldid";
            this.mzjldid.HeaderText = "麻醉单号";
            this.mzjldid.Name = "mzjldid";
            this.mzjldid.Width = 80;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "事件名称";
            this.name.Name = "name";
            this.name.Width = 120;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "添加时间";
            this.time.Name = "time";
            this.time.Width = 120;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(175, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 31);
            this.button1.TabIndex = 10;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(427, 235);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(44, 21);
            this.textBox2.TabIndex = 11;
            this.textBox2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "数值：";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(470, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "mmol/L";
            this.label4.Visible = false;
            // 
            // addszsj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 319);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "addszsj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "术中事件";
            this.Load += new System.EventHandler(this.addszsj_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjldid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}