namespace SDM
{
  partial class FormSPublisher
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSPublisher));
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
      this.TbxInput = new System.Windows.Forms.TextBox();
      this.LblInput = new System.Windows.Forms.Label();
      this.LblSampleInput = new System.Windows.Forms.Label();
      this.LblSampleTopic = new System.Windows.Forms.Label();
      this.CmbTopic = new System.Windows.Forms.ComboBox();
      this.label24 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.BtnHelpConsumer = new System.Windows.Forms.Button();
      this.BtnConsumer = new System.Windows.Forms.Button();
      this.BtnDeleteInput = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(396, 396);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 13;
      // 
      // LblLastModified
      // 
      this.LblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblLastModified.AutoSize = true;
      this.LblLastModified.Location = new System.Drawing.Point(313, 400);
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
      this.TbxRegistered.Location = new System.Drawing.Point(110, 396);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(188, 21);
      this.TbxRegistered.TabIndex = 12;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(10, 400);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 52;
      this.label88.Text = "Registered";
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Location = new System.Drawing.Point(10, 173);
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
      this.TbxDescription.Location = new System.Drawing.Point(110, 170);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(486, 193);
      this.TbxDescription.TabIndex = 8;
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
      this.BtnSave.Location = new System.Drawing.Point(521, 446);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 14;
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
      this.BtnViewSpec.Location = new System.Drawing.Point(573, 367);
      this.BtnViewSpec.Name = "BtnViewSpec";
      this.BtnViewSpec.Size = new System.Drawing.Size(23, 23);
      this.BtnViewSpec.TabIndex = 11;
      this.BtnViewSpec.UseVisualStyleBackColor = true;
      this.BtnViewSpec.Click += new System.EventHandler(this.BtnViewSpec_Click);
      // 
      // BtnSpecFile
      // 
      this.BtnSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSpecFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpecFile.Image")));
      this.BtnSpecFile.Location = new System.Drawing.Point(549, 367);
      this.BtnSpecFile.Name = "BtnSpecFile";
      this.BtnSpecFile.Size = new System.Drawing.Size(23, 23);
      this.BtnSpecFile.TabIndex = 10;
      this.BtnSpecFile.UseVisualStyleBackColor = true;
      this.BtnSpecFile.Click += new System.EventHandler(this.BtnSpecFile_Click);
      // 
      // TbxSpecFile
      // 
      this.TbxSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSpecFile.Location = new System.Drawing.Point(110, 369);
      this.TbxSpecFile.Name = "TbxSpecFile";
      this.TbxSpecFile.Size = new System.Drawing.Size(433, 21);
      this.TbxSpecFile.TabIndex = 9;
      // 
      // label78
      // 
      this.label78.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label78.AutoSize = true;
      this.label78.Location = new System.Drawing.Point(10, 372);
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
      this.BtnTranslate.TabIndex = 95;
      this.BtnTranslate.UseVisualStyleBackColor = true;
      this.BtnTranslate.Click += new System.EventHandler(this.BtnTranslate_Click);
      // 
      // LblSampleMethodName
      // 
      this.LblSampleMethodName.AutoSize = true;
      this.LblSampleMethodName.Location = new System.Drawing.Point(370, 92);
      this.LblSampleMethodName.Name = "LblSampleMethodName";
      this.LblSampleMethodName.Size = new System.Drawing.Size(156, 12);
      this.LblSampleMethodName.TabIndex = 94;
      this.LblSampleMethodName.Text = "(ex : publishOrderProduct)";
      // 
      // LblSampleName
      // 
      this.LblSampleName.AutoSize = true;
      this.LblSampleName.Location = new System.Drawing.Point(346, 65);
      this.LblSampleName.Name = "LblSampleName";
      this.LblSampleName.Size = new System.Drawing.Size(113, 12);
      this.LblSampleName.TabIndex = 96;
      this.LblSampleName.Text = "(ex : 주문상품발행)";
      // 
      // TbxInput
      // 
      this.TbxInput.AllowDrop = true;
      this.TbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxInput.Location = new System.Drawing.Point(110, 115);
      this.TbxInput.Name = "TbxInput";
      this.TbxInput.Size = new System.Drawing.Size(361, 21);
      this.TbxInput.TabIndex = 6;
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
      this.LblInput.TabIndex = 103;
      this.LblInput.Text = "Input";
      // 
      // LblSampleInput
      // 
      this.LblSampleInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.LblSampleInput.AutoSize = true;
      this.LblSampleInput.Location = new System.Drawing.Point(506, 119);
      this.LblSampleInput.Name = "LblSampleInput";
      this.LblSampleInput.Size = new System.Drawing.Size(90, 12);
      this.LblSampleInput.TabIndex = 104;
      this.LblSampleInput.Text = "(ex : OrderDto)";
      // 
      // LblSampleTopic
      // 
      this.LblSampleTopic.AutoSize = true;
      this.LblSampleTopic.Location = new System.Drawing.Point(346, 145);
      this.LblSampleTopic.Name = "LblSampleTopic";
      this.LblSampleTopic.Size = new System.Drawing.Size(95, 12);
      this.LblSampleTopic.TabIndex = 107;
      this.LblSampleTopic.Text = "(ex : 상품-주문)";
      // 
      // CmbTopic
      // 
      this.CmbTopic.FormattingEnabled = true;
      this.CmbTopic.Location = new System.Drawing.Point(110, 142);
      this.CmbTopic.Name = "CmbTopic";
      this.CmbTopic.Size = new System.Drawing.Size(230, 20);
      this.CmbTopic.Sorted = true;
      this.CmbTopic.TabIndex = 7;
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label24.Location = new System.Drawing.Point(9, 145);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(42, 12);
      this.label24.TabIndex = 106;
      this.label24.Text = "Topic";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 428);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(64, 12);
      this.label1.TabIndex = 121;
      this.label1.Text = "Consumer";
      // 
      // BtnHelpConsumer
      // 
      this.BtnHelpConsumer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.BtnHelpConsumer.BackColor = System.Drawing.Color.Transparent;
      this.BtnHelpConsumer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnHelpConsumer.BackgroundImage")));
      this.BtnHelpConsumer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnHelpConsumer.FlatAppearance.BorderSize = 0;
      this.BtnHelpConsumer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnHelpConsumer.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnHelpConsumer.Location = new System.Drawing.Point(75, 426);
      this.BtnHelpConsumer.Name = "BtnHelpConsumer";
      this.BtnHelpConsumer.Size = new System.Drawing.Size(16, 16);
      this.BtnHelpConsumer.TabIndex = 120;
      this.BtnHelpConsumer.UseVisualStyleBackColor = false;
      this.BtnHelpConsumer.Click += new System.EventHandler(this.BtnHelpConsumer_Click);
      // 
      // BtnConsumer
      // 
      this.BtnConsumer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.BtnConsumer.Location = new System.Drawing.Point(110, 423);
      this.BtnConsumer.Name = "BtnConsumer";
      this.BtnConsumer.Size = new System.Drawing.Size(30, 23);
      this.BtnConsumer.TabIndex = 119;
      this.BtnConsumer.Text = "0";
      this.BtnConsumer.UseVisualStyleBackColor = true;
      this.BtnConsumer.Click += new System.EventHandler(this.BtnConsumer_Click);
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
      this.BtnDeleteInput.TabIndex = 122;
      this.BtnDeleteInput.UseVisualStyleBackColor = true;
      this.BtnDeleteInput.Click += new System.EventHandler(this.BtnDeleteInput_Click);
      // 
      // FormSPublisher
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 481);
      this.Controls.Add(this.BtnDeleteInput);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.BtnHelpConsumer);
      this.Controls.Add(this.BtnConsumer);
      this.Controls.Add(this.LblSampleTopic);
      this.Controls.Add(this.CmbTopic);
      this.Controls.Add(this.label24);
      this.Controls.Add(this.LblSampleInput);
      this.Controls.Add(this.TbxInput);
      this.Controls.Add(this.LblInput);
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
      this.Name = "FormSPublisher";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "FormBizPackage";
      this.Load += new System.EventHandler(this.FormSPublisher_Load);
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
    private System.Windows.Forms.TextBox TbxInput;
    private System.Windows.Forms.Label LblInput;
    private System.Windows.Forms.Label LblSampleInput;
    private System.Windows.Forms.Label LblSampleTopic;
    private System.Windows.Forms.ComboBox CmbTopic;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button BtnHelpConsumer;
    private System.Windows.Forms.Button BtnConsumer;
    private System.Windows.Forms.Button BtnDeleteInput;
  }
}