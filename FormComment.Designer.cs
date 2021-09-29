namespace SDM
{
  partial class FormComment
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
      this.BtnClose = new System.Windows.Forms.Button();
      this.BtnSave = new System.Windows.Forms.Button();
      this.TbxComment = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // BtnClose
      // 
      this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.BtnClose.Location = new System.Drawing.Point(187, 39);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(46, 23);
      this.BtnClose.TabIndex = 2;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
      // 
      // BtnSave
      // 
      this.BtnSave.Location = new System.Drawing.Point(135, 39);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(46, 23);
      this.BtnSave.TabIndex = 1;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // TbxComment
      // 
      this.TbxComment.Location = new System.Drawing.Point(12, 12);
      this.TbxComment.Name = "TbxComment";
      this.TbxComment.Size = new System.Drawing.Size(221, 21);
      this.TbxComment.TabIndex = 0;
      this.TbxComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxComment_KeyDown);
      // 
      // FormComment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(250, 76);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.BtnSave);
      this.Controls.Add(this.TbxComment);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "FormComment";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormComment";
      this.Load += new System.EventHandler(this.FormComment_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.TextBox TbxComment;
  }
}