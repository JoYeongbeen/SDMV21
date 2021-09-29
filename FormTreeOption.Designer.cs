namespace SDM
{
  partial class FormTreeOption
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTreeOption));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.BtnSave = new System.Windows.Forms.Button();
      this.BtnClose = new System.Windows.Forms.Button();
      this.LblFontSize = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.LblFont = new System.Windows.Forms.Label();
      this.BtnSetFont = new System.Windows.Forms.Button();
      this.fontDialog1 = new System.Windows.Forms.FontDialog();
      this.DgvMS = new System.Windows.Forms.DataGridView();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.DgvMS)).BeginInit();
      this.SuspendLayout();
      // 
      // BtnSave
      // 
      this.BtnSave.Location = new System.Drawing.Point(222, 418);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 2;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // BtnClose
      // 
      this.BtnClose.Location = new System.Drawing.Point(303, 418);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(75, 23);
      this.BtnClose.TabIndex = 28;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
      // 
      // LblFontSize
      // 
      this.LblFontSize.AutoSize = true;
      this.LblFontSize.Location = new System.Drawing.Point(138, 33);
      this.LblFontSize.Name = "LblFontSize";
      this.LblFontSize.Size = new System.Drawing.Size(27, 12);
      this.LblFontSize.TabIndex = 47;
      this.LblFontSize.Text = "9.75";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 12);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(59, 12);
      this.label6.TabIndex = 46;
      this.label6.Text = "Tree Font";
      // 
      // LblFont
      // 
      this.LblFont.AutoSize = true;
      this.LblFont.Location = new System.Drawing.Point(138, 12);
      this.LblFont.Name = "LblFont";
      this.LblFont.Size = new System.Drawing.Size(57, 12);
      this.LblFont.TabIndex = 45;
      this.LblFont.Text = "맑은 고딕";
      // 
      // BtnSetFont
      // 
      this.BtnSetFont.Image = ((System.Drawing.Image)(resources.GetObject("BtnSetFont.Image")));
      this.BtnSetFont.Location = new System.Drawing.Point(255, 12);
      this.BtnSetFont.Name = "BtnSetFont";
      this.BtnSetFont.Size = new System.Drawing.Size(23, 23);
      this.BtnSetFont.TabIndex = 44;
      this.BtnSetFont.UseVisualStyleBackColor = true;
      this.BtnSetFont.Click += new System.EventHandler(this.BtnSetFont_Click);
      // 
      // DgvMS
      // 
      this.DgvMS.AllowUserToAddRows = false;
      this.DgvMS.AllowUserToDeleteRows = false;
      this.DgvMS.AllowUserToResizeRows = false;
      this.DgvMS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DgvMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DgvMS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5});
      this.DgvMS.Location = new System.Drawing.Point(12, 71);
      this.DgvMS.MultiSelect = false;
      this.DgvMS.Name = "DgvMS";
      this.DgvMS.RowHeadersVisible = false;
      this.DgvMS.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.DgvMS.RowTemplate.Height = 23;
      this.DgvMS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.DgvMS.Size = new System.Drawing.Size(366, 341);
      this.DgvMS.TabIndex = 0;
      // 
      // Column2
      // 
      this.Column2.HeaderText = "GUID";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Visible = false;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Display";
      this.Column1.Name = "Column1";
      this.Column1.Width = 60;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "ProgramID";
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column3.HeaderText = "Code";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 70;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "Name";
      this.Column4.HeaderText = "Name";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Width = 250;
      // 
      // Column5
      // 
      this.Column5.HeaderText = "Type";
      this.Column5.Name = "Column5";
      this.Column5.Visible = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(167, 12);
      this.label3.TabIndex = 74;
      this.label3.Text = "Tree에 표시할 Microservices";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 33);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 12);
      this.label1.TabIndex = 75;
      this.label1.Text = "Tree Font Size";
      // 
      // FormTreeOption
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(390, 454);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.DgvMS);
      this.Controls.Add(this.LblFontSize);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.LblFont);
      this.Controls.Add(this.BtnSave);
      this.Controls.Add(this.BtnSetFont);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormTreeOption";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Tree Option";
      this.Load += new System.EventHandler(this.FormTreeOption_Load);
      ((System.ComponentModel.ISupportInitialize)(this.DgvMS)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.Label LblFont;
    private System.Windows.Forms.Button BtnSetFont;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label LblFontSize;
    private System.Windows.Forms.DataGridView DgvMS;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
  }
}