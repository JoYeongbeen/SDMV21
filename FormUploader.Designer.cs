namespace SDM
{
  partial class FormUploader
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUploader));
      this.LblUploaderDescription = new System.Windows.Forms.Label();
      this.RbnAPI = new System.Windows.Forms.RadioButton();
      this.TbxInput = new System.Windows.Forms.TextBox();
      this.BtnSave = new System.Windows.Forms.Button();
      this.BtnClose = new System.Windows.Forms.Button();
      this.RbnBatch = new System.Windows.Forms.RadioButton();
      this.RbnOperation = new System.Windows.Forms.RadioButton();
      this.RbnOther = new System.Windows.Forms.RadioButton();
      this.RbnSub = new System.Windows.Forms.RadioButton();
      this.RbnPub = new System.Windows.Forms.RadioButton();
      this.RbnAttribute = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // LblUploaderDescription
      // 
      this.LblUploaderDescription.AutoSize = true;
      this.LblUploaderDescription.Location = new System.Drawing.Point(12, 35);
      this.LblUploaderDescription.Name = "LblUploaderDescription";
      this.LblUploaderDescription.Size = new System.Drawing.Size(210, 12);
      this.LblUploaderDescription.TabIndex = 0;
      this.LblUploaderDescription.Text = "한 줄 씩 입력하세요 input line by line";
      // 
      // RbnAPI
      // 
      this.RbnAPI.AutoSize = true;
      this.RbnAPI.ForeColor = System.Drawing.Color.Blue;
      this.RbnAPI.Location = new System.Drawing.Point(12, 12);
      this.RbnAPI.Name = "RbnAPI";
      this.RbnAPI.Size = new System.Drawing.Size(42, 16);
      this.RbnAPI.TabIndex = 1;
      this.RbnAPI.TabStop = true;
      this.RbnAPI.Text = "API";
      this.RbnAPI.UseVisualStyleBackColor = true;
      this.RbnAPI.CheckedChanged += new System.EventHandler(this.RbnAPI_CheckedChanged);
      // 
      // TbxInput
      // 
      this.TbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxInput.Location = new System.Drawing.Point(12, 54);
      this.TbxInput.Multiline = true;
      this.TbxInput.Name = "TbxInput";
      this.TbxInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxInput.Size = new System.Drawing.Size(731, 455);
      this.TbxInput.TabIndex = 2;
      // 
      // BtnSave
      // 
      this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSave.Location = new System.Drawing.Point(587, 515);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 3;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // BtnClose
      // 
      this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.BtnClose.Location = new System.Drawing.Point(668, 515);
      this.BtnClose.Name = "BtnClose";
      this.BtnClose.Size = new System.Drawing.Size(75, 23);
      this.BtnClose.TabIndex = 4;
      this.BtnClose.Text = "Close";
      this.BtnClose.UseVisualStyleBackColor = true;
      this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
      // 
      // RbnBatch
      // 
      this.RbnBatch.AutoSize = true;
      this.RbnBatch.Location = new System.Drawing.Point(350, 12);
      this.RbnBatch.Name = "RbnBatch";
      this.RbnBatch.Size = new System.Drawing.Size(85, 16);
      this.RbnBatch.TabIndex = 5;
      this.RbnBatch.TabStop = true;
      this.RbnBatch.Text = "Batch(Job)";
      this.RbnBatch.UseVisualStyleBackColor = true;
      this.RbnBatch.CheckedChanged += new System.EventHandler(this.RbnBatch_CheckedChanged);
      // 
      // RbnOperation
      // 
      this.RbnOperation.AutoSize = true;
      this.RbnOperation.Location = new System.Drawing.Point(272, 12);
      this.RbnOperation.Name = "RbnOperation";
      this.RbnOperation.Size = new System.Drawing.Size(77, 16);
      this.RbnOperation.TabIndex = 6;
      this.RbnOperation.TabStop = true;
      this.RbnOperation.Text = "Operation";
      this.RbnOperation.UseVisualStyleBackColor = true;
      this.RbnOperation.CheckedChanged += new System.EventHandler(this.RbnOperation_CheckedChanged);
      // 
      // RbnOther
      // 
      this.RbnOther.AutoSize = true;
      this.RbnOther.ForeColor = System.Drawing.Color.Crimson;
      this.RbnOther.Location = new System.Drawing.Point(217, 12);
      this.RbnOther.Name = "RbnOther";
      this.RbnOther.Size = new System.Drawing.Size(53, 16);
      this.RbnOther.TabIndex = 7;
      this.RbnOther.TabStop = true;
      this.RbnOther.Text = "Other";
      this.RbnOther.UseVisualStyleBackColor = true;
      this.RbnOther.CheckedChanged += new System.EventHandler(this.RbnOther_CheckedChanged);
      // 
      // RbnSub
      // 
      this.RbnSub.AutoSize = true;
      this.RbnSub.ForeColor = System.Drawing.Color.DarkGreen;
      this.RbnSub.Location = new System.Drawing.Point(132, 12);
      this.RbnSub.Name = "RbnSub";
      this.RbnSub.Size = new System.Drawing.Size(84, 16);
      this.RbnSub.TabIndex = 8;
      this.RbnSub.TabStop = true;
      this.RbnSub.Text = "Subscriber";
      this.RbnSub.UseVisualStyleBackColor = true;
      this.RbnSub.CheckedChanged += new System.EventHandler(this.RbnSub_CheckedChanged);
      // 
      // RbnPub
      // 
      this.RbnPub.AutoSize = true;
      this.RbnPub.ForeColor = System.Drawing.Color.DarkGreen;
      this.RbnPub.Location = new System.Drawing.Point(55, 12);
      this.RbnPub.Name = "RbnPub";
      this.RbnPub.Size = new System.Drawing.Size(76, 16);
      this.RbnPub.TabIndex = 9;
      this.RbnPub.TabStop = true;
      this.RbnPub.Text = "Publisher";
      this.RbnPub.UseVisualStyleBackColor = true;
      this.RbnPub.CheckedChanged += new System.EventHandler(this.RbnPub_CheckedChanged);
      // 
      // RbnAttribute
      // 
      this.RbnAttribute.AutoSize = true;
      this.RbnAttribute.Location = new System.Drawing.Point(439, 12);
      this.RbnAttribute.Name = "RbnAttribute";
      this.RbnAttribute.Size = new System.Drawing.Size(68, 16);
      this.RbnAttribute.TabIndex = 10;
      this.RbnAttribute.TabStop = true;
      this.RbnAttribute.Text = "Attribute";
      this.RbnAttribute.UseVisualStyleBackColor = true;
      this.RbnAttribute.CheckedChanged += new System.EventHandler(this.RbnDtoAttr_CheckedChanged);
      // 
      // FormUploader
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.BtnClose;
      this.ClientSize = new System.Drawing.Size(752, 550);
      this.Controls.Add(this.RbnAttribute);
      this.Controls.Add(this.RbnPub);
      this.Controls.Add(this.RbnSub);
      this.Controls.Add(this.RbnOther);
      this.Controls.Add(this.RbnOperation);
      this.Controls.Add(this.RbnBatch);
      this.Controls.Add(this.BtnClose);
      this.Controls.Add(this.BtnSave);
      this.Controls.Add(this.TbxInput);
      this.Controls.Add(this.RbnAPI);
      this.Controls.Add(this.LblUploaderDescription);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormUploader";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Uploader";
      this.Load += new System.EventHandler(this.FormUploader_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label LblUploaderDescription;
    private System.Windows.Forms.TextBox TbxInput;
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.Button BtnClose;
    public System.Windows.Forms.RadioButton RbnAPI;
    public System.Windows.Forms.RadioButton RbnBatch;
    public System.Windows.Forms.RadioButton RbnOperation;
    public System.Windows.Forms.RadioButton RbnOther;
    public System.Windows.Forms.RadioButton RbnSub;
    public System.Windows.Forms.RadioButton RbnPub;
    public System.Windows.Forms.RadioButton RbnAttribute;
  }
}