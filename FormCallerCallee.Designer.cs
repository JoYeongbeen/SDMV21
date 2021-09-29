namespace SDM
{
  partial class FormCallerCallee
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCallerCallee));
      this.LblComponentFullPath = new System.Windows.Forms.Label();
      this.LbxMessageList = new System.Windows.Forms.ListBox();
      this.BtnClose = new System.Windows.Forms.Button();
      this.BtnMoveRight = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // LblComponentFullPath
      // 
      this.LblComponentFullPath.AutoSize = true;
      this.LblComponentFullPath.Location = new System.Drawing.Point(12, 12);
      this.LblComponentFullPath.Name = "LblComponentFullPath";
      this.LblComponentFullPath.Size = new System.Drawing.Size(142, 12);
      this.LblComponentFullPath.TabIndex = 0;
      this.LblComponentFullPath.Text = "선택한 컴포넌트 FullPath";
      // 
      // LbxMessageList
      // 
      this.LbxMessageList.AllowDrop = true;
      this.LbxMessageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LbxMessageList.FormattingEnabled = true;
      this.LbxMessageList.ItemHeight = 12;
      this.LbxMessageList.Location = new System.Drawing.Point(12, 34);
      this.LbxMessageList.Name = "LbxMessageList";
      this.LbxMessageList.ScrollAlwaysVisible = true;
      this.LbxMessageList.Size = new System.Drawing.Size(590, 136);
      this.LbxMessageList.TabIndex = 11;
      this.LbxMessageList.DoubleClick += new System.EventHandler(this.LbxMessageList_DoubleClick);
      // 
      // BtnClose
      // 
      this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.BtnClose.Location = new System.Drawing.Point(527, 180);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(75, 23);
      this.BtnClose.TabIndex = 12;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
      // 
      // BtnMoveRight
      // 
      this.BtnMoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.BtnMoveRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMoveRight.BackgroundImage")));
      this.BtnMoveRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnMoveRight.Font = new System.Drawing.Font("굴림", 13F);
      this.BtnMoveRight.Location = new System.Drawing.Point(12, 177);
      this.BtnMoveRight.Name = "BtnMoveRight";
      this.BtnMoveRight.Size = new System.Drawing.Size(23, 23);
      this.BtnMoveRight.TabIndex = 13;
      this.BtnMoveRight.UseVisualStyleBackColor = true;
      this.BtnMoveRight.Click += new System.EventHandler(this.BtnMoveRight_Click);
      // 
      // FormCallerCallee
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.BtnClose;
      this.ClientSize = new System.Drawing.Size(614, 214);
      this.Controls.Add(this.BtnMoveRight);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.LbxMessageList);
      this.Controls.Add(this.LblComponentFullPath);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCallerCallee";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FormCallerCallee";
      this.Load += new System.EventHandler(this.FormCallerCallee_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label LblComponentFullPath;
    private System.Windows.Forms.ListBox LbxMessageList;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.Button BtnMoveRight;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}