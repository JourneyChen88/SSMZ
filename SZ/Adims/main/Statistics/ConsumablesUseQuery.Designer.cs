using main.UserControl;

namespace main.Statistics
{
    partial class ConsumablesUseQuery
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
            this.dgvUseList = new System.Windows.Forms.DataGridView();
            this.PatZhuyuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbZhuyuanID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbKeshi = new main.UserControl.UcComboBox();
            this.tbPatName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUseList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUseList
            // 
            this.dgvUseList.AllowUserToAddRows = false;
            this.dgvUseList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUseList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvUseList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatZhuyuanID,
            this.PatID,
            this.PatName,
            this.Name1,
            this.Dosage,
            this.price,
            this.Unit,
            this.IsCost,
            this.UseTime});
            this.dgvUseList.Location = new System.Drawing.Point(19, 159);
            this.dgvUseList.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dgvUseList.Name = "dgvUseList";
            this.dgvUseList.ReadOnly = true;
            this.dgvUseList.RowHeadersVisible = false;
            this.dgvUseList.RowTemplate.Height = 23;
            this.dgvUseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUseList.Size = new System.Drawing.Size(1006, 470);
            this.dgvUseList.TabIndex = 28;
            // 
            // PatZhuyuanID
            // 
            this.PatZhuyuanID.DataPropertyName = "PatZhuyuanID";
            this.PatZhuyuanID.HeaderText = "住院号";
            this.PatZhuyuanID.Name = "PatZhuyuanID";
            this.PatZhuyuanID.ReadOnly = true;
            // 
            // PatID
            // 
            this.PatID.DataPropertyName = "PatID";
            this.PatID.HeaderText = "病人编号";
            this.PatID.Name = "PatID";
            this.PatID.ReadOnly = true;
            this.PatID.Visible = false;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "患者名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            // 
            // Name1
            // 
            this.Name1.DataPropertyName = "Name";
            this.Name1.FillWeight = 123.6804F;
            this.Name1.HeaderText = "药物名称";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // Dosage
            // 
            this.Dosage.DataPropertyName = "Dosage";
            this.Dosage.FillWeight = 68.03897F;
            this.Dosage.HeaderText = "用量";
            this.Dosage.Name = "Dosage";
            this.Dosage.ReadOnly = true;
            // 
            // price
            // 
            this.price.DataPropertyName = "Price";
            this.price.HeaderText = "单价";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // IsCost
            // 
            this.IsCost.DataPropertyName = "IsCost";
            this.IsCost.FillWeight = 97.38615F;
            this.IsCost.HeaderText = "是否计费";
            this.IsCost.Name = "IsCost";
            this.IsCost.ReadOnly = true;
            // 
            // UseTime
            // 
            this.UseTime.DataPropertyName = "UseTime";
            this.UseTime.FillWeight = 103.0145F;
            this.UseTime.HeaderText = "使用时间";
            this.UseTime.Name = "UseTime";
            this.UseTime.ReadOnly = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.ForeColor = System.Drawing.Color.Blue;
            this.btnQuery.Location = new System.Drawing.Point(723, 73);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(91, 34);
            this.btnQuery.TabIndex = 24;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "手术日期：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(268, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 20);
            this.label8.TabIndex = 38;
            this.label8.Text = "到：";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(107, 37);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(141, 25);
            this.dtStart.TabIndex = 39;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(312, 35);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(141, 25);
            this.dtEnd.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "住院号：";
            // 
            // tbZhuyuanID
            // 
            this.tbZhuyuanID.Location = new System.Drawing.Point(107, 80);
            this.tbZhuyuanID.Name = "tbZhuyuanID";
            this.tbZhuyuanID.Size = new System.Drawing.Size(141, 25);
            this.tbZhuyuanID.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(719, 647);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "总计：";
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(771, 645);
            this.tbSum.Name = "tbSum";
            this.tbSum.ReadOnly = true;
            this.tbSum.Size = new System.Drawing.Size(141, 25);
            this.tbSum.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "科室：";
            // 
            // cmbKeshi
            // 
            this.cmbKeshi.FormattingEnabled = true;
            this.cmbKeshi.Location = new System.Drawing.Point(312, 78);
            this.cmbKeshi.Name = "cmbKeshi";
            this.cmbKeshi.Size = new System.Drawing.Size(141, 27);
            this.cmbKeshi.TabIndex = 46;
            // 
            // tbPatName
            // 
            this.tbPatName.Location = new System.Drawing.Point(556, 80);
            this.tbPatName.Name = "tbPatName";
            this.tbPatName.Size = new System.Drawing.Size(120, 25);
            this.tbPatName.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(470, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 47;
            this.label4.Text = "病人姓名：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(846, 75);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 34);
            this.button1.TabIndex = 49;
            this.button1.Text = "按科室统计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConsumablesUseQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 695);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPatName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbKeshi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbZhuyuanID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.dgvUseList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "ConsumablesUseQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材使用统计";
            this.Load += new System.EventHandler(this.ConsumablesUseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUseList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvUseList;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuyuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbZhuyuanID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label label3;
        private UcComboBox cmbKeshi;
        private System.Windows.Forms.TextBox tbPatName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}