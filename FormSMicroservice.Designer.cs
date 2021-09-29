namespace SDM
{
  partial class FormSMicroservice
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSMicroservice));
      this.BtnSave = new System.Windows.Forms.Button();
      this.LblSampleMSCode = new System.Windows.Forms.Label();
      this.label142 = new System.Windows.Forms.Label();
      this.CmbPart = new System.Windows.Forms.ComboBox();
      this.TbxLastModified = new System.Windows.Forms.TextBox();
      this.LblLastModified = new System.Windows.Forms.Label();
      this.TbxRegistered = new System.Windows.Forms.TextBox();
      this.label88 = new System.Windows.Forms.Label();
      this.label40 = new System.Windows.Forms.Label();
      this.TbxDescription = new System.Windows.Forms.TextBox();
      this.TbxID = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.TbxName = new System.Windows.Forms.TextBox();
      this.LblIDMS = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.BtnViewSpec = new System.Windows.Forms.Button();
      this.BtnSpecFile = new System.Windows.Forms.Button();
      this.TbxSpecFile = new System.Windows.Forms.TextBox();
      this.label78 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // BtnSave
      // 
      this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSave.Location = new System.Drawing.Point(594, 437);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 6;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // LblSampleMSCode
      // 
      this.LblSampleMSCode.AutoSize = true;
      this.LblSampleMSCode.Location = new System.Drawing.Point(346, 9);
      this.LblSampleMSCode.Name = "LblSampleMSCode";
      this.LblSampleMSCode.Size = new System.Drawing.Size(94, 12);
      this.LblSampleMSCode.TabIndex = 42;
      this.LblSampleMSCode.Text = "(ex : ORD 주문)";
      // 
      // label142
      // 
      this.label142.AutoSize = true;
      this.label142.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label142.Location = new System.Drawing.Point(10, 63);
      this.label142.Name = "label142";
      this.label142.Size = new System.Drawing.Size(31, 12);
      this.label142.TabIndex = 41;
      this.label142.Text = "Part";
      // 
      // CmbPart
      // 
      this.CmbPart.DisplayMember = "Name";
      this.CmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbPart.FormattingEnabled = true;
      this.CmbPart.Location = new System.Drawing.Point(98, 60);
      this.CmbPart.Name = "CmbPart";
      this.CmbPart.Size = new System.Drawing.Size(242, 20);
      this.CmbPart.Sorted = true;
      this.CmbPart.TabIndex = 2;
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(396, 410);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 5;
      // 
      // LblLastModified
      // 
      this.LblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblLastModified.AutoSize = true;
      this.LblLastModified.Location = new System.Drawing.Point(313, 414);
      this.LblLastModified.Name = "LblLastModified";
      this.LblLastModified.Size = new System.Drawing.Size(81, 12);
      this.LblLastModified.TabIndex = 40;
      this.LblLastModified.Text = "Last Modified";
      this.LblLastModified.DoubleClick += new System.EventHandler(this.LblLastModified_DoubleClick);
      // 
      // TbxRegistered
      // 
      this.TbxRegistered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxRegistered.Enabled = false;
      this.TbxRegistered.Location = new System.Drawing.Point(98, 410);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(200, 21);
      this.TbxRegistered.TabIndex = 4;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(10, 414);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 38;
      this.label88.Text = "Registered";
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Location = new System.Drawing.Point(10, 90);
      this.label40.Name = "label40";
      this.label40.Size = new System.Drawing.Size(68, 12);
      this.label40.TabIndex = 36;
      this.label40.Text = "Description";
      // 
      // TbxDescription
      // 
      this.TbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxDescription.Location = new System.Drawing.Point(98, 86);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(570, 291);
      this.TbxDescription.TabIndex = 3;
      // 
      // TbxID
      // 
      this.TbxID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.TbxID.Location = new System.Drawing.Point(98, 6);
      this.TbxID.Name = "TbxID";
      this.TbxID.Size = new System.Drawing.Size(242, 21);
      this.TbxID.TabIndex = 0;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label4.Location = new System.Drawing.Point(10, 37);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 12);
      this.label4.TabIndex = 34;
      this.label4.Text = "Name";
      // 
      // TbxName
      // 
      this.TbxName.Location = new System.Drawing.Point(98, 33);
      this.TbxName.Name = "TbxName";
      this.TbxName.Size = new System.Drawing.Size(242, 21);
      this.TbxName.TabIndex = 1;
      // 
      // LblIDMS
      // 
      this.LblIDMS.AutoSize = true;
      this.LblIDMS.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
      this.LblIDMS.Location = new System.Drawing.Point(10, 10);
      this.LblIDMS.Name = "LblIDMS";
      this.LblIDMS.Size = new System.Drawing.Size(18, 12);
      this.LblIDMS.TabIndex = 35;
      this.LblIDMS.Text = "ID";
      // 
      // BtnViewSpec
      // 
      this.BtnViewSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnViewSpec.Image = ((System.Drawing.Image)(resources.GetObject("BtnViewSpec.Image")));
      this.BtnViewSpec.Location = new System.Drawing.Point(646, 381);
      this.BtnViewSpec.Name = "BtnViewSpec";
      this.BtnViewSpec.Size = new System.Drawing.Size(23, 23);
      this.BtnViewSpec.TabIndex = 68;
      this.BtnViewSpec.UseVisualStyleBackColor = true;
      this.BtnViewSpec.Click += new System.EventHandler(this.BtnViewSpec_Click);
      // 
      // BtnSpecFile
      // 
      this.BtnSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSpecFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpecFile.Image")));
      this.BtnSpecFile.Location = new System.Drawing.Point(623, 381);
      this.BtnSpecFile.Name = "BtnSpecFile";
      this.BtnSpecFile.Size = new System.Drawing.Size(23, 23);
      this.BtnSpecFile.TabIndex = 67;
      this.BtnSpecFile.UseVisualStyleBackColor = true;
      this.BtnSpecFile.Click += new System.EventHandler(this.BtnSpecFile_Click);
      // 
      // TbxSpecFile
      // 
      this.TbxSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSpecFile.Location = new System.Drawing.Point(98, 382);
      this.TbxSpecFile.Name = "TbxSpecFile";
      this.TbxSpecFile.Size = new System.Drawing.Size(524, 21);
      this.TbxSpecFile.TabIndex = 66;
      // 
      // label78
      // 
      this.label78.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label78.AutoSize = true;
      this.label78.Location = new System.Drawing.Point(10, 386);
      this.label78.Name = "label78";
      this.label78.Size = new System.Drawing.Size(58, 12);
      this.label78.TabIndex = 69;
      this.label78.Text = "Spec File";
      // 
      // FormSMicroservice
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(685, 467);
      this.Controls.Add(this.BtnViewSpec);
      this.Controls.Add(this.BtnSpecFile);
      this.Controls.Add(this.TbxSpecFile);
      this.Controls.Add(this.label78);
      this.Controls.Add(this.LblSampleMSCode);
      this.Controls.Add(this.label142);
      this.Controls.Add(this.CmbPart);
      this.Controls.Add(this.TbxLastModified);
      this.Controls.Add(this.LblLastModified);
      this.Controls.Add(this.TbxRegistered);
      this.Controls.Add(this.label88);
      this.Controls.Add(this.label40);
      this.Controls.Add(this.TbxDescription);
      this.Controls.Add(this.TbxID);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.TbxName);
      this.Controls.Add(this.LblIDMS);
      this.Controls.Add(this.BtnSave);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormSMicroservice";
      this.ShowIcon = false;
      this.Text = "FormMicroservice";
      this.Load += new System.EventHandler(this.FormMicroservice_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.Label LblSampleMSCode;
    private System.Windows.Forms.Label label142;
    private System.Windows.Forms.ComboBox CmbPart;
    private System.Windows.Forms.TextBox TbxLastModified;
    private System.Windows.Forms.Label LblLastModified;
    private System.Windows.Forms.TextBox TbxRegistered;
    private System.Windows.Forms.Label label88;
    private System.Windows.Forms.Label label40;
    private System.Windows.Forms.TextBox TbxDescription;
    private System.Windows.Forms.TextBox TbxID;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label LblIDMS;
    private System.Windows.Forms.ToolTip toolTip1;
    public System.Windows.Forms.TextBox TbxName;
    private System.Windows.Forms.Button BtnViewSpec;
    private System.Windows.Forms.Button BtnSpecFile;
    private System.Windows.Forms.TextBox TbxSpecFile;
    private System.Windows.Forms.Label label78;
  }
}