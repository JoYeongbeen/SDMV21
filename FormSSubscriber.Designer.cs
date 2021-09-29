namespace SDM
{
  partial class FormSSubscriber
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSSubscriber));
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
      this.BtnSave = new System.Windows.Forms.Button();
      this.TbxParentPath = new System.Windows.Forms.TextBox();
      this.label34 = new System.Windows.Forms.Label();
      this.TbxMethodName = new System.Windows.Forms.TextBox();
      this.LblMethodName = new System.Windows.Forms.Label();
      this.BtnViewSpec = new System.Windows.Forms.Button();
      this.BtnSpecFile = new System.Windows.Forms.Button();
      this.TbxSpecFile = new System.Windows.Forms.TextBox();
      this.label78 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.CkbDesignDetail = new System.Windows.Forms.CheckBox();
      this.CkbDesignSkeleton = new System.Windows.Forms.CheckBox();
      this.BtnTranslate = new System.Windows.Forms.Button();
      this.LblSampleMethodName = new System.Windows.Forms.Label();
      this.LblSampleName = new System.Windows.Forms.Label();
      this.LblBROperation = new System.Windows.Forms.Label();
      this.TbxBROperation = new System.Windows.Forms.TextBox();
      this.BtnDeleteBROperation = new System.Windows.Forms.Button();
      this.label24 = new System.Windows.Forms.Label();
      this.LblSampleInput = new System.Windows.Forms.Label();
      this.TbxInput = new System.Windows.Forms.TextBox();
      this.LblInput = new System.Windows.Forms.Label();
      this.CmbTopic = new System.Windows.Forms.ComboBox();
      this.BtnSearchProvider = new System.Windows.Forms.Button();
      this.BtnDeleteInput = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(396, 398);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 16;
      // 
      // LblLastModified
      // 
      this.LblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblLastModified.AutoSize = true;
      this.LblLastModified.Location = new System.Drawing.Point(313, 402);
      this.LblLastModified.Name = "LblLastModified";
      this.LblLastModified.Size = new System.Drawing.Size(81, 12);
      this.LblLastModified.TabIndex = 54;
      this.LblLastModified.Text = "Last Modified";
      this.LblLastModified.DoubleClick += new System.EventHandler(this.LblLastModified_DoubleClick);
      // 
      // TbxRegistered
      // 
      this.TbxRegistered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxRegistered.Enabled = false;
      this.TbxRegistered.Location = new System.Drawing.Point(110, 398);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(188, 21);
      this.TbxRegistered.TabIndex = 15;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(10, 402);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 52;
      this.label88.Text = "Registered";
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Location = new System.Drawing.Point(10, 200);
      this.label40.Name = "label40";
      this.label40.Size = new System.Drawing.Size(68, 12);
      this.label40.TabIndex = 50;
      this.label40.Text = "Description";
      // 
      // TbxDescription
      // 
      this.TbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxDescription.Location = new System.Drawing.Point(110, 197);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(486, 166);
      this.TbxDescription.TabIndex = 11;
      // 
      // TbxID
      // 
      this.TbxID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.TbxID.Location = new System.Drawing.Point(110, 34);
      this.TbxID.Name = "TbxID";
      this.TbxID.Size = new System.Drawing.Size(230, 21);
      this.TbxID.TabIndex = 1;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label4.Location = new System.Drawing.Point(10, 65);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 12);
      this.label4.TabIndex = 48;
      this.label4.Text = "Name";
      // 
      // TbxName
      // 
      this.TbxName.Location = new System.Drawing.Point(110, 61);
      this.TbxName.Name = "TbxName";
      this.TbxName.Size = new System.Drawing.Size(230, 21);
      this.TbxName.TabIndex = 4;
      // 
      // LblIDMS
      // 
      this.LblIDMS.AutoSize = true;
      this.LblIDMS.Location = new System.Drawing.Point(10, 38);
      this.LblIDMS.Name = "LblIDMS";
      this.LblIDMS.Size = new System.Drawing.Size(16, 12);
      this.LblIDMS.TabIndex = 49;
      this.LblIDMS.Text = "ID";
      // 
      // BtnSave
      // 
      this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSave.Location = new System.Drawing.Point(521, 425);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 17;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // TbxParentPath
      // 
      this.TbxParentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxParentPath.Location = new System.Drawing.Point(110, 7);
      this.TbxParentPath.Name = "TbxParentPath";
      this.TbxParentPath.ReadOnly = true;
      this.TbxParentPath.Size = new System.Drawing.Size(490, 21);
      this.TbxParentPath.TabIndex = 0;
      // 
      // label34
      // 
      this.label34.AutoSize = true;
      this.label34.Location = new System.Drawing.Point(10, 10);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(54, 12);
      this.label34.TabIndex = 58;
      this.label34.Text = "Full Path";
      // 
      // TbxMethodName
      // 
      this.TbxMethodName.Location = new System.Drawing.Point(110, 88);
      this.TbxMethodName.Name = "TbxMethodName";
      this.TbxMethodName.Size = new System.Drawing.Size(230, 21);
      this.TbxMethodName.TabIndex = 5;
      // 
      // LblMethodName
      // 
      this.LblMethodName.AutoSize = true;
      this.LblMethodName.Location = new System.Drawing.Point(10, 91);
      this.LblMethodName.Name = "LblMethodName";
      this.LblMethodName.Size = new System.Drawing.Size(85, 12);
      this.LblMethodName.TabIndex = 61;
      this.LblMethodName.Text = "Method Name";
      // 
      // BtnViewSpec
      // 
      this.BtnViewSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnViewSpec.Image = ((System.Drawing.Image)(resources.GetObject("BtnViewSpec.Image")));
      this.BtnViewSpec.Location = new System.Drawing.Point(573, 369);
      this.BtnViewSpec.Name = "BtnViewSpec";
      this.BtnViewSpec.Size = new System.Drawing.Size(23, 23);
      this.BtnViewSpec.TabIndex = 14;
      this.BtnViewSpec.UseVisualStyleBackColor = true;
      this.BtnViewSpec.Click += new System.EventHandler(this.BtnViewSpec_Click);
      // 
      // BtnSpecFile
      // 
      this.BtnSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSpecFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpecFile.Image")));
      this.BtnSpecFile.Location = new System.Drawing.Point(549, 369);
      this.BtnSpecFile.Name = "BtnSpecFile";
      this.BtnSpecFile.Size = new System.Drawing.Size(23, 23);
      this.BtnSpecFile.TabIndex = 13;
      this.BtnSpecFile.UseVisualStyleBackColor = true;
      this.BtnSpecFile.Click += new System.EventHandler(this.BtnSpecFile_Click);
      // 
      // TbxSpecFile
      // 
      this.TbxSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSpecFile.Location = new System.Drawing.Point(110, 371);
      this.TbxSpecFile.Name = "TbxSpecFile";
      this.TbxSpecFile.Size = new System.Drawing.Size(433, 21);
      this.TbxSpecFile.TabIndex = 12;
      // 
      // label78
      // 
      this.label78.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label78.AutoSize = true;
      this.label78.Location = new System.Drawing.Point(10, 374);
      this.label78.Name = "label78";
      this.label78.Size = new System.Drawing.Size(58, 12);
      this.label78.TabIndex = 65;
      this.label78.Text = "Spec File";
      // 
      // CkbDesignDetail
      // 
      this.CkbDesignDetail.AutoSize = true;
      this.CkbDesignDetail.Location = new System.Drawing.Point(442, 36);
      this.CkbDesignDetail.Name = "CkbDesignDetail";
      this.CkbDesignDetail.Size = new System.Drawing.Size(96, 16);
      this.CkbDesignDetail.TabIndex = 3;
      this.CkbDesignDetail.Text = "상세설계완료";
      this.CkbDesignDetail.UseVisualStyleBackColor = true;
      this.CkbDesignDetail.CheckedChanged += new System.EventHandler(this.CkbDesignDetail_CheckedChanged);
      // 
      // CkbDesignSkeleton
      // 
      this.CkbDesignSkeleton.AutoSize = true;
      this.CkbDesignSkeleton.Location = new System.Drawing.Point(346, 36);
      this.CkbDesignSkeleton.Name = "CkbDesignSkeleton";
      this.CkbDesignSkeleton.Size = new System.Drawing.Size(96, 16);
      this.CkbDesignSkeleton.TabIndex = 2;
      this.CkbDesignSkeleton.Text = "기본설계완료";
      this.CkbDesignSkeleton.UseVisualStyleBackColor = true;
      this.CkbDesignSkeleton.CheckedChanged += new System.EventHandler(this.CkbDesignSkeleton_CheckedChanged);
      // 
      // BtnTranslate
      // 
      this.BtnTranslate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnTranslate.Image = ((System.Drawing.Image)(resources.GetObject("BtnTranslate.Image")));
      this.BtnTranslate.Location = new System.Drawing.Point(344, 87);
      this.BtnTranslate.Name = "BtnTranslate";
      this.BtnTranslate.Size = new System.Drawing.Size(23, 23);
      this.BtnTranslate.TabIndex = 6;
      this.BtnTranslate.UseVisualStyleBackColor = true;
      this.BtnTranslate.Click += new System.EventHandler(this.BtnTranslate_Click);
      // 
      // LblSampleMethodName
      // 
      this.LblSampleMethodName.AutoSize = true;
      this.LblSampleMethodName.Location = new System.Drawing.Point(370, 92);
      this.LblSampleMethodName.Name = "LblSampleMethodName";
      this.LblSampleMethodName.Size = new System.Drawing.Size(171, 12);
      this.LblSampleMethodName.TabIndex = 94;
      this.LblSampleMethodName.Text = "(ex : subscribeOrderProduct)";
      // 
      // LblSampleName
      // 
      this.LblSampleName.AutoSize = true;
      this.LblSampleName.Location = new System.Drawing.Point(346, 65);
      this.LblSampleName.Name = "LblSampleName";
      this.LblSampleName.Size = new System.Drawing.Size(113, 12);
      this.LblSampleName.TabIndex = 96;
      this.LblSampleName.Text = "(ex : 주문상품구독)";
      // 
      // LblBROperation
      // 
      this.LblBROperation.AutoSize = true;
      this.LblBROperation.Location = new System.Drawing.Point(10, 173);
      this.LblBROperation.Name = "LblBROperation";
      this.LblBROperation.Size = new System.Drawing.Size(79, 12);
      this.LblBROperation.TabIndex = 108;
      this.LblBROperation.Text = "BR Operation";
      // 
      // TbxBROperation
      // 
      this.TbxBROperation.AllowDrop = true;
      this.TbxBROperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxBROperation.BackColor = System.Drawing.Color.LightGray;
      this.TbxBROperation.Location = new System.Drawing.Point(110, 170);
      this.TbxBROperation.Name = "TbxBROperation";
      this.TbxBROperation.ReadOnly = true;
      this.TbxBROperation.Size = new System.Drawing.Size(457, 21);
      this.TbxBROperation.TabIndex = 9;
      this.TbxBROperation.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbxBROperation_DragDrop);
      this.TbxBROperation.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbxBROperation_DragEnter);
      this.TbxBROperation.DoubleClick += new System.EventHandler(this.TbxBROperation_DoubleClick);
      // 
      // BtnDeleteBROperation
      // 
      this.BtnDeleteBROperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnDeleteBROperation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDeleteBROperation.BackgroundImage")));
      this.BtnDeleteBROperation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDeleteBROperation.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnDeleteBROperation.Location = new System.Drawing.Point(573, 168);
      this.BtnDeleteBROperation.Name = "BtnDeleteBROperation";
      this.BtnDeleteBROperation.Size = new System.Drawing.Size(23, 23);
      this.BtnDeleteBROperation.TabIndex = 10;
      this.BtnDeleteBROperation.UseVisualStyleBackColor = true;
      this.BtnDeleteBROperation.Click += new System.EventHandler(this.BtnDeleteBROperation_Click);
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label24.Location = new System.Drawing.Point(9, 145);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(42, 12);
      this.label24.TabIndex = 114;
      this.label24.Text = "Topic";
      // 
      // LblSampleInput
      // 
      this.LblSampleInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.LblSampleInput.AutoSize = true;
      this.LblSampleInput.Location = new System.Drawing.Point(506, 118);
      this.LblSampleInput.Name = "LblSampleInput";
      this.LblSampleInput.Size = new System.Drawing.Size(90, 12);
      this.LblSampleInput.TabIndex = 112;
      this.LblSampleInput.Text = "(ex : OrderDto)";
      // 
      // TbxInput
      // 
      this.TbxInput.AllowDrop = true;
      this.TbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxInput.Location = new System.Drawing.Point(110, 115);
      this.TbxInput.Name = "TbxInput";
      this.TbxInput.Size = new System.Drawing.Size(361, 21);
      this.TbxInput.TabIndex = 7;
      this.TbxInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbxInput_DragDrop);
      this.TbxInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbxInput_DragEnter);
      this.TbxInput.DoubleClick += new System.EventHandler(this.TbxInput_DoubleClick);
      // 
      // LblInput
      // 
      this.LblInput.AutoSize = true;
      this.LblInput.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblInput.Location = new System.Drawing.Point(10, 119);
      this.LblInput.Name = "LblInput";
      this.LblInput.Size = new System.Drawing.Size(32, 12);
      this.LblInput.TabIndex = 111;
      this.LblInput.Text = "Input";
      // 
      // CmbTopic
      // 
      this.CmbTopic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbTopic.FormattingEnabled = true;
      this.CmbTopic.Location = new System.Drawing.Point(110, 143);
      this.CmbTopic.Name = "CmbTopic";
      this.CmbTopic.Size = new System.Drawing.Size(230, 20);
      this.CmbTopic.Sorted = true;
      this.CmbTopic.TabIndex = 8;
      // 
      // BtnSearchProvider
      // 
      this.BtnSearchProvider.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearchProvider.Image")));
      this.BtnSearchProvider.Location = new System.Drawing.Point(346, 141);
      this.BtnSearchProvider.Name = "BtnSearchProvider";
      this.BtnSearchProvider.Size = new System.Drawing.Size(23, 23);
      this.BtnSearchProvider.TabIndex = 115;
      this.BtnSearchProvider.UseVisualStyleBackColor = true;
      this.BtnSearchProvider.Click += new System.EventHandler(this.BtnSearchProvider_Click);
      // 
      // BtnDeleteInput
      // 
      this.BtnDeleteInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnDeleteInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDeleteInput.BackgroundImage")));
      this.BtnDeleteInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDeleteInput.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnDeleteInput.Location = new System.Drawing.Point(477, 113);
      this.BtnDeleteInput.Name = "BtnDeleteInput";
      this.BtnDeleteInput.Size = new System.Drawing.Size(23, 23);
      this.BtnDeleteInput.TabIndex = 123;
      this.BtnDeleteInput.UseVisualStyleBackColor = true;
      this.BtnDeleteInput.Click += new System.EventHandler(this.BtnDeleteInput_Click);
      // 
      // FormSSubscriber
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 460);
      this.Controls.Add(this.BtnDeleteInput);
      this.Controls.Add(this.BtnSearchProvider);
      this.Controls.Add(this.CmbTopic);
      this.Controls.Add(this.label24);
      this.Controls.Add(this.LblSampleInput);
      this.Controls.Add(this.TbxInput);
      this.Controls.Add(this.LblInput);
      this.Controls.Add(this.BtnDeleteBROperation);
      this.Controls.Add(this.LblBROperation);
      this.Controls.Add(this.TbxBROperation);
      this.Controls.Add(this.LblSampleName);
      this.Controls.Add(this.BtnTranslate);
      this.Controls.Add(this.LblSampleMethodName);
      this.Controls.Add(this.CkbDesignDetail);
      this.Controls.Add(this.CkbDesignSkeleton);
      this.Controls.Add(this.BtnViewSpec);
      this.Controls.Add(this.BtnSpecFile);
      this.Controls.Add(this.TbxSpecFile);
      this.Controls.Add(this.label78);
      this.Controls.Add(this.TbxMethodName);
      this.Controls.Add(this.LblMethodName);
      this.Controls.Add(this.TbxParentPath);
      this.Controls.Add(this.label34);
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
      this.Name = "FormSSubscriber";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "FormBizPackage";
      this.Load += new System.EventHandler(this.FormSSubscriber_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox TbxLastModified;
    private System.Windows.Forms.Label LblLastModified;
    private System.Windows.Forms.TextBox TbxRegistered;
    private System.Windows.Forms.Label label88;
    private System.Windows.Forms.Label label40;
    private System.Windows.Forms.TextBox TbxDescription;
    private System.Windows.Forms.TextBox TbxID;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label LblIDMS;
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.TextBox TbxParentPath;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.TextBox TbxMethodName;
    private System.Windows.Forms.Label LblMethodName;
    private System.Windows.Forms.Button BtnViewSpec;
    private System.Windows.Forms.Button BtnSpecFile;
    private System.Windows.Forms.TextBox TbxSpecFile;
    private System.Windows.Forms.Label label78;
    private System.Windows.Forms.ToolTip toolTip1;
    public System.Windows.Forms.TextBox TbxName;
    private System.Windows.Forms.CheckBox CkbDesignDetail;
    private System.Windows.Forms.CheckBox CkbDesignSkeleton;
    private System.Windows.Forms.Button BtnTranslate;
    private System.Windows.Forms.Label LblSampleMethodName;
    private System.Windows.Forms.Label LblSampleName;
    private System.Windows.Forms.Label LblBROperation;
    private System.Windows.Forms.TextBox TbxBROperation;
    private System.Windows.Forms.Button BtnDeleteBROperation;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Label LblSampleInput;
    private System.Windows.Forms.TextBox TbxInput;
    private System.Windows.Forms.Label LblInput;
    private System.Windows.Forms.ComboBox CmbTopic;
    private System.Windows.Forms.Button BtnSearchProvider;
    private System.Windows.Forms.Button BtnDeleteInput;
  }
}