namespace SDM
{
  partial class FormLogin
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
      this.ProjectIconPictureBox = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.BtnLogin = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.CmbPart = new System.Windows.Forms.ComboBox();
      this.TbxPassword = new System.Windows.Forms.TextBox();
      this.LblPassword = new System.Windows.Forms.Label();
      this.LblProjectName = new System.Windows.Forms.Label();
      this.TbxName = new System.Windows.Forms.TextBox();
      this.CbxSaveName = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.ProjectIconPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // ProjectIconPictureBox
      // 
      this.ProjectIconPictureBox.ImageLocation = "https://www.lgcns.com/static/images/header_logo.png";
      this.ProjectIconPictureBox.Location = new System.Drawing.Point(154, 70);
      this.ProjectIconPictureBox.Name = "ProjectIconPictureBox";
      this.ProjectIconPictureBox.Size = new System.Drawing.Size(143, 77);
      this.ProjectIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.ProjectIconPictureBox.TabIndex = 14;
      this.ProjectIconPictureBox.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(86, 169);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(31, 12);
      this.label1.TabIndex = 16;
      this.label1.Text = "Part";
      // 
      // BtnLogin
      // 
      this.BtnLogin.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnLogin.Location = new System.Drawing.Point(303, 166);
      this.BtnLogin.Name = "BtnLogin";
      this.BtnLogin.Size = new System.Drawing.Size(79, 75);
      this.BtnLogin.TabIndex = 3;
      this.BtnLogin.Text = "Login";
      this.BtnLogin.UseVisualStyleBackColor = true;
      this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label2.Location = new System.Drawing.Point(86, 196);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 12);
      this.label2.TabIndex = 19;
      this.label2.Text = "Name";
      // 
      // CmbPart
      // 
      this.CmbPart.DisplayMember = "Name";
      this.CmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbPart.FormattingEnabled = true;
      this.CmbPart.Location = new System.Drawing.Point(154, 166);
      this.CmbPart.Name = "CmbPart";
      this.CmbPart.Size = new System.Drawing.Size(143, 20);
      this.CmbPart.Sorted = true;
      this.CmbPart.TabIndex = 0;
      this.CmbPart.SelectedIndexChanged += new System.EventHandler(this.CmbPart_SelectedIndexChanged);
      // 
      // TbxPassword
      // 
      this.TbxPassword.Location = new System.Drawing.Point(154, 220);
      this.TbxPassword.Name = "TbxPassword";
      this.TbxPassword.PasswordChar = '*';
      this.TbxPassword.Size = new System.Drawing.Size(143, 21);
      this.TbxPassword.TabIndex = 2;
      this.TbxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxPassword_KeyDown);
      // 
      // LblPassword
      // 
      this.LblPassword.AutoSize = true;
      this.LblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LblPassword.Location = new System.Drawing.Point(86, 223);
      this.LblPassword.Name = "LblPassword";
      this.LblPassword.Size = new System.Drawing.Size(62, 12);
      this.LblPassword.TabIndex = 32;
      this.LblPassword.Text = "Password";
      // 
      // LblProjectName
      // 
      this.LblProjectName.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblProjectName.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LblProjectName.Location = new System.Drawing.Point(5, 38);
      this.LblProjectName.Name = "LblProjectName";
      this.LblProjectName.Size = new System.Drawing.Size(431, 24);
      this.LblProjectName.TabIndex = 34;
      this.LblProjectName.Text = "ProjectName";
      this.LblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TbxName
      // 
      this.TbxName.Location = new System.Drawing.Point(154, 192);
      this.TbxName.Name = "TbxName";
      this.TbxName.Size = new System.Drawing.Size(143, 21);
      this.TbxName.TabIndex = 1;
      this.TbxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxName_KeyDown);
      // 
      // CbxSaveName
      // 
      this.CbxSaveName.AutoSize = true;
      this.CbxSaveName.Location = new System.Drawing.Point(154, 247);
      this.CbxSaveName.Name = "CbxSaveName";
      this.CbxSaveName.Size = new System.Drawing.Size(90, 16);
      this.CbxSaveName.TabIndex = 35;
      this.CbxSaveName.Text = "Save Name";
      this.CbxSaveName.UseVisualStyleBackColor = true;
      // 
      // FormLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(445, 345);
      this.Controls.Add(this.CbxSaveName);
      this.Controls.Add(this.TbxName);
      this.Controls.Add(this.LblProjectName);
      this.Controls.Add(this.TbxPassword);
      this.Controls.Add(this.LblPassword);
      this.Controls.Add(this.CmbPart);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.BtnLogin);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ProjectIconPictureBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormLogin";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormLogin";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
      this.Load += new System.EventHandler(this.FormLogin_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ProjectIconPictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox ProjectIconPictureBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button BtnLogin;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox CmbPart;
    private System.Windows.Forms.TextBox TbxPassword;
    private System.Windows.Forms.Label LblPassword;
    private System.Windows.Forms.Label LblProjectName;
    private System.Windows.Forms.TextBox TbxName;
    private System.Windows.Forms.CheckBox CbxSaveName;
  }
}