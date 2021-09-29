namespace SDM
{
  partial class FormHelp
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelp));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.TbxHistory = new System.Windows.Forms.RichTextBox();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.BtnClose = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.LblContact = new System.Windows.Forms.Label();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Location = new System.Drawing.Point(12, 59);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(547, 500);
      this.tabControl1.TabIndex = 99;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.TbxHistory);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(539, 474);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "History";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // TbxHistory
      // 
      this.TbxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TbxHistory.Location = new System.Drawing.Point(3, 3);
      this.TbxHistory.Name = "TbxHistory";
      this.TbxHistory.ReadOnly = true;
      this.TbxHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
      this.TbxHistory.Size = new System.Drawing.Size(533, 468);
      this.TbxHistory.TabIndex = 0;
      this.TbxHistory.Text = "\n2021.08.11 CNS쇼핑몰 모델러 사용자만 포함된 신규 모델\n2021.08.06 API/PUB/SUB/BROp/DAOp Input/Outp" +
    "ut 연결 및 설계서 함께 생성\n2021.08.05 SDM Version2 최초 배포\n";
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.richTextBox2);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(539, 474);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Shortcut";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // richTextBox2
      // 
      this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBox2.Location = new System.Drawing.Point(3, 3);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
      this.richTextBox2.Size = new System.Drawing.Size(533, 468);
      this.richTextBox2.TabIndex = 1;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      // 
      // BtnClose
      // 
      this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.BtnClose.Location = new System.Drawing.Point(480, 565);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(75, 23);
      this.BtnClose.TabIndex = 98;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(262, 12);
      this.label3.TabIndex = 96;
      this.label3.Text = "Microservice 정의 > 기본설계 > 상세설계 도구";
      // 
      // LblContact
      // 
      this.LblContact.AutoSize = true;
      this.LblContact.Location = new System.Drawing.Point(10, 31);
      this.LblContact.Name = "LblContact";
      this.LblContact.Size = new System.Drawing.Size(329, 12);
      this.LblContact.TabIndex = 93;
      this.LblContact.Text = "[문의] LG CNS MSA모델링팀 구태형(taekoo@lgcns.com)";
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.webBrowser1);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(539, 474);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "SQLite License";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // webBrowser1
      // 
      this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser1.Location = new System.Drawing.Point(3, 3);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(533, 468);
      this.webBrowser1.TabIndex = 0;
      this.webBrowser1.Url = new System.Uri("https://www.sqlite.org/copyright.html", System.UriKind.Absolute);
      // 
      // FormHelp
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(572, 599);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.LblContact);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormHelp";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormHelp";
      this.Load += new System.EventHandler(this.FormHelp_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage2;
    public System.Windows.Forms.RichTextBox TbxHistory;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.Label LblContact;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.WebBrowser webBrowser1;
  }
}