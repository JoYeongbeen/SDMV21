namespace SDM
{
  partial class FormMyOption
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMyOption));
      this.BtnSave = new System.Windows.Forms.Button();
      this.BtnClose = new System.Windows.Forms.Button();
      this.fontDialog1 = new System.Windows.Forms.FontDialog();
      this.LblUser = new System.Windows.Forms.Label();
      this.TbxPassword1 = new System.Windows.Forms.TextBox();
      this.LblPassword = new System.Windows.Forms.Label();
      this.TbxPassword2 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // BtnSave
      // 
      this.BtnSave.Location = new System.Drawing.Point(115, 101);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 1;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // BtnClose
      // 
      this.BtnClose.Location = new System.Drawing.Point(196, 101);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(75, 23);
      this.BtnClose.TabIndex = 2;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
      // 
      // LblUser
      // 
      this.LblUser.AutoSize = true;
      this.LblUser.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblUser.Location = new System.Drawing.Point(11, 11);
      this.LblUser.Name = "LblUser";
      this.LblUser.Size = new System.Drawing.Size(103, 12);
      this.LblUser.TabIndex = 70;
      this.LblUser.Text = "[Part]Designer";
      // 
      // TbxPassword1
      // 
      this.TbxPassword1.Location = new System.Drawing.Point(128, 38);
      this.TbxPassword1.Name = "TbxPassword1";
      this.TbxPassword1.PasswordChar = '*';
      this.TbxPassword1.Size = new System.Drawing.Size(143, 21);
      this.TbxPassword1.TabIndex = 0;
      // 
      // LblPassword
      // 
      this.LblPassword.AutoSize = true;
      this.LblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LblPassword.Location = new System.Drawing.Point(12, 41);
      this.LblPassword.Name = "LblPassword";
      this.LblPassword.Size = new System.Drawing.Size(62, 12);
      this.LblPassword.TabIndex = 72;
      this.LblPassword.Text = "Password";
      // 
      // TbxPassword2
      // 
      this.TbxPassword2.Location = new System.Drawing.Point(128, 65);
      this.TbxPassword2.Name = "TbxPassword2";
      this.TbxPassword2.PasswordChar = '*';
      this.TbxPassword2.Size = new System.Drawing.Size(143, 21);
      this.TbxPassword2.TabIndex = 73;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(12, 68);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(110, 12);
      this.label1.TabIndex = 74;
      this.label1.Text = "Confirm Password";
      // 
      // FormMyOption
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(289, 136);
      this.Controls.Add(this.TbxPassword2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.TbxPassword1);
      this.Controls.Add(this.LblPassword);
      this.Controls.Add(this.LblUser);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.BtnSave);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormMyOption";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "My Option";
      this.Load += new System.EventHandler(this.FormMyOption_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.Label LblUser;
    private System.Windows.Forms.TextBox TbxPassword1;
    private System.Windows.Forms.Label LblPassword;
    private System.Windows.Forms.TextBox TbxPassword2;
    private System.Windows.Forms.Label label1;
  }
}