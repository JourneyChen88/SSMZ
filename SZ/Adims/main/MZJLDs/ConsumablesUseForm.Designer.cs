namespace main.MZJLDs
{
    partial class ConsumablesUseForm
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvUseList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCost = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUseList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(715, 189);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 46);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(715, 302);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 46);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvUseList
            // 
            this.dgvUseList.AllowUserToAddRows = false;
            this.dgvUseList.AllowUserToDeleteRows = false;
            this.dgvUseList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvUseList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUseList.ColumnHeadersHeight = 25;
            this.dgvUseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.name,
            this.Dosage,
            this.Price,
            this.Unit,
            this.IsCost,
            this.UseTime});
            this.dgvUseList.Location = new System.Drawing.Point(12, 12);
            this.dgvUseList.MultiSelect = false;
            this.dgvUseList.Name = "dgvUseList";
            this.dgvUseList.RowHeadersVisible = false;
            this.dgvUseList.RowTemplate.Height = 23;
            this.dgvUseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUseList.Size = new System.Drawing.Size(664, 572);
            this.dgvUseList.TabIndex = 30;
            this.dgvUseList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUseList_CellClick);
            this.dgvUseList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUseList_CellDoubleClick);
            this.dgvUseList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvUseList_EditingControlShowing);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 42;
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.Frozen = true;
            this.name.HeaderText = "耗材名";
            this.name.MinimumWidth = 200;
            this.name.Name = "name";
            this.name.Width = 200;
            // 
            // Dosage
            // 
            this.Dosage.DataPropertyName = "Dosage";
            this.Dosage.Frozen = true;
            this.Dosage.HeaderText = "用量";
            this.Dosage.MinimumWidth = 100;
            this.Dosage.Name = "Dosage";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.Frozen = true;
            this.Price.HeaderText = "单价";
            this.Price.MinimumWidth = 100;
            this.Price.Name = "Price";
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.Frozen = true;
            this.Unit.HeaderText = "单位";
            this.Unit.MinimumWidth = 100;
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // IsCost
            // 
            this.IsCost.DataPropertyName = "IsCost";
            this.IsCost.HeaderText = "是否计费";
            this.IsCost.MinimumWidth = 100;
            this.IsCost.Name = "IsCost";
            this.IsCost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UseTime
            // 
            this.UseTime.DataPropertyName = "UseTime";
            this.UseTime.HeaderText = "时间";
            this.UseTime.MinimumWidth = 150;
            this.UseTime.Name = "UseTime";
            this.UseTime.Visible = false;
            this.UseTime.Width = 150;
            // 
            // ConsumablesUseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 596);
            this.Controls.Add(this.dgvUseList);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "ConsumablesUseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材使用管理";
            this.Load += new System.EventHandler(this.ConsumablesUseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUseList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvUseList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseTime;
    }
}