namespace SDM
{
  partial class FormSBizRuleOperation
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSBizRuleOperation));
      this.TbxLastModified = new System.Windows.Forms.TextBox();
      this.LblLastModified = new System.Windows.Forms.Label();
      this.TbxRegistered = new System.Windows.Forms.TextBox();
      this.label88 = new System.Windows.Forms.Label();
      this.LblDesc = new System.Windows.Forms.Label();
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
      this.BtnTranslate = new System.Windows.Forms.Button();
      this.CkbDesignDetail = new System.Windows.Forms.CheckBox();
      this.CkbDesignSkeleton = new System.Windows.Forms.CheckBox();
      this.CkbTransaction = new System.Windows.Forms.CheckBox();
      this.LblSampleMethodName = new System.Windows.Forms.Label();
      this.TbxOutput = new System.Windows.Forms.TextBox();
      this.LblReturn = new System.Windows.Forms.Label();
      this.TbxInput = new System.Windows.Forms.TextBox();
      this.LblParameter = new System.Windows.Forms.Label();
      this.BtnAddComment = new System.Windows.Forms.Button();
      this.LbxMessageList = new System.Windows.Forms.ListBox();
      this.LblCallList = new System.Windows.Forms.Label();
      this.BtnMoveUpCall = new System.Windows.Forms.Button();
      this.BtnMoveDownCall = new System.Windows.Forms.Button();
      this.BtnDeleteCall = new System.Windows.Forms.Button();
      this.BtnEditComment = new System.Windows.Forms.Button();
      this.LblSampleBROperationName = new System.Windows.Forms.Label();
      this.BtnHelpCallList = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.BtnHelpConsumer = new System.Windows.Forms.Button();
      this.BtnConsumer = new System.Windows.Forms.Button();
      this.BtnDeleteOutput = new System.Windows.Forms.Button();
      this.BtnDeleteInput = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(396, 491);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 21;
      // 
      // LblLastModified
      // 
      this.LblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblLastModified.AutoSize = true;
      this.LblLastModified.Location = new System.Drawing.Point(313, 495);
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
      this.TbxRegistered.Location = new System.Drawing.Point(110, 491);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(188, 21);
      this.TbxRegistered.TabIndex = 20;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(10, 495);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 52;
      this.label88.Text = "Registered";
      // 
      // LblDesc
      // 
      this.LblDesc.AutoSize = true;
      this.LblDesc.Location = new System.Drawing.Point(10, 338);
      this.LblDesc.Name = "LblDesc";
      this.LblDesc.Size = new System.Drawing.Size(68, 12);
      this.LblDesc.TabIndex = 50;
      this.LblDesc.Text = "Description";
      // 
      // TbxDescription
      // 
      this.TbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxDescription.Location = new System.Drawing.Point(110, 335);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(598, 123);
      this.TbxDescription.TabIndex = 16;
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
      this.BtnSave.Location = new System.Drawing.Point(633, 538);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 22;
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
      this.TbxParentPath.Size = new System.Drawing.Size(598, 21);
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
      this.TbxMethodName.ImeMode = System.Windows.Forms.ImeMode.Alpha;
      this.TbxMethodName.Location = new System.Drawing.Point(110, 88);
      this.TbxMethodName.Name = "TbxMethodName";
      this.TbxMethodName.Size = new System.Drawing.Size(230, 21);
      this.TbxMethodName.TabIndex = 6;
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
      this.BtnViewSpec.Location = new System.Drawing.Point(686, 462);
      this.BtnViewSpec.Name = "BtnViewSpec";
      this.BtnViewSpec.Size = new System.Drawing.Size(23, 23);
      this.BtnViewSpec.TabIndex = 19;
      this.BtnViewSpec.UseVisualStyleBackColor = true;
      this.BtnViewSpec.Click += new System.EventHandler(this.BtnViewSpec_Click);
      // 
      // BtnSpecFile
      // 
      this.BtnSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSpecFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpecFile.Image")));
      this.BtnSpecFile.Location = new System.Drawing.Point(661, 462);
      this.BtnSpecFile.Name = "BtnSpecFile";
      this.BtnSpecFile.Size = new System.Drawing.Size(23, 23);
      this.BtnSpecFile.TabIndex = 18;
      this.BtnSpecFile.UseVisualStyleBackColor = true;
      this.BtnSpecFile.Click += new System.EventHandler(this.BtnSpecFile_Click);
      // 
      // TbxSpecFile
      // 
      this.TbxSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSpecFile.Location = new System.Drawing.Point(110, 464);
      this.TbxSpecFile.Name = "TbxSpecFile";
      this.TbxSpecFile.Size = new System.Drawing.Size(545, 21);
      this.TbxSpecFile.TabIndex = 17;
      // 
      // label78
      // 
      this.label78.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label78.AutoSize = true;
      this.label78.Location = new System.Drawing.Point(10, 467);
      this.label78.Name = "label78";
      this.label78.Size = new System.Drawing.Size(58, 12);
      this.label78.TabIndex = 65;
      this.label78.Text = "Spec File";
      // 
      // BtnTranslate
      // 
      this.BtnTranslate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnTranslate.Image = ((System.Drawing.Image)(resources.GetObject("BtnTranslate.Image")));
      this.BtnTranslate.Location = new System.Drawing.Point(344, 86);
      this.BtnTranslate.Name = "BtnTranslate";
      this.BtnTranslate.Size = new System.Drawing.Size(23, 23);
      this.BtnTranslate.TabIndex = 7;
      this.BtnTranslate.UseVisualStyleBackColor = true;
      this.BtnTranslate.Click += new System.EventHandler(this.BtnTranslate_Click);
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
      // CkbTransaction
      // 
      this.CkbTransaction.AutoSize = true;
      this.CkbTransaction.Location = new System.Drawing.Point(346, 63);
      this.CkbTransaction.Name = "CkbTransaction";
      this.CkbTransaction.Size = new System.Drawing.Size(91, 16);
      this.CkbTransaction.TabIndex = 5;
      this.CkbTransaction.Text = "Transaction";
      this.CkbTransaction.UseVisualStyleBackColor = true;
      // 
      // LblSampleMethodName
      // 
      this.LblSampleMethodName.AutoSize = true;
      this.LblSampleMethodName.Location = new System.Drawing.Point(372, 91);
      this.LblSampleMethodName.Name = "LblSampleMethodName";
      this.LblSampleMethodName.Size = new System.Drawing.Size(110, 12);
      this.LblSampleMethodName.TabIndex = 102;
      this.LblSampleMethodName.Text = "(ex : cancelOrder)";
      // 
      // TbxOutput
      // 
      this.TbxOutput.AllowDrop = true;
      this.TbxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxOutput.Location = new System.Drawing.Point(110, 142);
      this.TbxOutput.Name = "TbxOutput";
      this.TbxOutput.Size = new System.Drawing.Size(569, 21);
      this.TbxOutput.TabIndex = 9;
      this.TbxOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbxOutput_DragDrop);
      this.TbxOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbxOutput_DragEnter);
      this.TbxOutput.DoubleClick += new System.EventHandler(this.TbxOutput_DoubleClick);
      // 
      // LblReturn
      // 
      this.LblReturn.AutoSize = true;
      this.LblReturn.Location = new System.Drawing.Point(10, 145);
      this.LblReturn.Name = "LblReturn";
      this.LblReturn.Size = new System.Drawing.Size(41, 12);
      this.LblReturn.TabIndex = 106;
      this.LblReturn.Text = "Output";
      // 
      // TbxInput
      // 
      this.TbxInput.AllowDrop = true;
      this.TbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxInput.Location = new System.Drawing.Point(110, 115);
      this.TbxInput.Name = "TbxInput";
      this.TbxInput.Size = new System.Drawing.Size(569, 21);
      this.TbxInput.TabIndex = 8;
      this.TbxInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbxInput_DragDrop);
      this.TbxInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbxInput_DragEnter);
      this.TbxInput.DoubleClick += new System.EventHandler(this.TbxInput_DoubleClick);
      // 
      // LblParameter
      // 
      this.LblParameter.AutoSize = true;
      this.LblParameter.Location = new System.Drawing.Point(10, 118);
      this.LblParameter.Name = "LblParameter";
      this.LblParameter.Size = new System.Drawing.Size(32, 12);
      this.LblParameter.TabIndex = 105;
      this.LblParameter.Text = "Input";
      // 
      // BtnAddComment
      // 
      this.BtnAddComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnAddComment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAddComment.BackgroundImage")));
      this.BtnAddComment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnAddComment.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnAddComment.Location = new System.Drawing.Point(685, 250);
      this.BtnAddComment.Name = "BtnAddComment";
      this.BtnAddComment.Size = new System.Drawing.Size(23, 23);
      this.BtnAddComment.TabIndex = 14;
      this.BtnAddComment.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.BtnAddComment.UseVisualStyleBackColor = true;
      this.BtnAddComment.Click += new System.EventHandler(this.BtnAddComment_Click);
      // 
      // LbxMessageList
      // 
      this.LbxMessageList.AllowDrop = true;
      this.LbxMessageList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.LbxMessageList.FormattingEnabled = true;
      this.LbxMessageList.ItemHeight = 12;
      this.LbxMessageList.Location = new System.Drawing.Point(110, 169);
      this.LbxMessageList.Name = "LbxMessageList";
      this.LbxMessageList.ScrollAlwaysVisible = true;
      this.LbxMessageList.Size = new System.Drawing.Size(569, 160);
      this.LbxMessageList.TabIndex = 10;
      this.LbxMessageList.DragDrop += new System.Windows.Forms.DragEventHandler(this.LbxMessageList_DragDrop);
      this.LbxMessageList.DragEnter += new System.Windows.Forms.DragEventHandler(this.LbxMessageList_DragEnter);
      this.LbxMessageList.DoubleClick += new System.EventHandler(this.LbxMessageList_DoubleClick);
      // 
      // LblCallList
      // 
      this.LblCallList.AutoSize = true;
      this.LblCallList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblCallList.Location = new System.Drawing.Point(10, 173);
      this.LblCallList.Name = "LblCallList";
      this.LblCallList.Size = new System.Drawing.Size(51, 12);
      this.LblCallList.TabIndex = 108;
      this.LblCallList.Text = "Call List";
      // 
      // BtnMoveUpCall
      // 
      this.BtnMoveUpCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnMoveUpCall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMoveUpCall.BackgroundImage")));
      this.BtnMoveUpCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnMoveUpCall.Font = new System.Drawing.Font("굴림", 13F);
      this.BtnMoveUpCall.Location = new System.Drawing.Point(685, 169);
      this.BtnMoveUpCall.Name = "BtnMoveUpCall";
      this.BtnMoveUpCall.Size = new System.Drawing.Size(23, 23);
      this.BtnMoveUpCall.TabIndex = 11;
      this.BtnMoveUpCall.UseVisualStyleBackColor = true;
      this.BtnMoveUpCall.Click += new System.EventHandler(this.BtnMoveUpCall_Click);
      // 
      // BtnMoveDownCall
      // 
      this.BtnMoveDownCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnMoveDownCall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMoveDownCall.BackgroundImage")));
      this.BtnMoveDownCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnMoveDownCall.Font = new System.Drawing.Font("굴림", 13F);
      this.BtnMoveDownCall.Location = new System.Drawing.Point(685, 193);
      this.BtnMoveDownCall.Name = "BtnMoveDownCall";
      this.BtnMoveDownCall.Size = new System.Drawing.Size(23, 23);
      this.BtnMoveDownCall.TabIndex = 12;
      this.BtnMoveDownCall.UseVisualStyleBackColor = true;
      this.BtnMoveDownCall.Click += new System.EventHandler(this.BtnMoveDownCall_Click);
      // 
      // BtnDeleteCall
      // 
      this.BtnDeleteCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnDeleteCall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDeleteCall.BackgroundImage")));
      this.BtnDeleteCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDeleteCall.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnDeleteCall.Location = new System.Drawing.Point(685, 283);
      this.BtnDeleteCall.Name = "BtnDeleteCall";
      this.BtnDeleteCall.Size = new System.Drawing.Size(23, 23);
      this.BtnDeleteCall.TabIndex = 15;
      this.BtnDeleteCall.UseVisualStyleBackColor = true;
      this.BtnDeleteCall.Click += new System.EventHandler(this.BtnDeleteCall_Click);
      // 
      // BtnEditComment
      // 
      this.BtnEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnEditComment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnEditComment.BackgroundImage")));
      this.BtnEditComment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnEditComment.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnEditComment.Location = new System.Drawing.Point(685, 226);
      this.BtnEditComment.Name = "BtnEditComment";
      this.BtnEditComment.Size = new System.Drawing.Size(23, 23);
      this.BtnEditComment.TabIndex = 13;
      this.BtnEditComment.UseVisualStyleBackColor = true;
      this.BtnEditComment.Click += new System.EventHandler(this.BtnEditComment_Click);
      // 
      // LblSampleBROperationName
      // 
      this.LblSampleBROperationName.AutoSize = true;
      this.LblSampleBROperationName.Location = new System.Drawing.Point(440, 66);
      this.LblSampleBROperationName.Name = "LblSampleBROperationName";
      this.LblSampleBROperationName.Size = new System.Drawing.Size(89, 12);
      this.LblSampleBROperationName.TabIndex = 114;
      this.LblSampleBROperationName.Text = "(ex : 주문취소)";
      // 
      // BtnHelpCallList
      // 
      this.BtnHelpCallList.BackColor = System.Drawing.Color.Transparent;
      this.BtnHelpCallList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnHelpCallList.BackgroundImage")));
      this.BtnHelpCallList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnHelpCallList.FlatAppearance.BorderSize = 0;
      this.BtnHelpCallList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnHelpCallList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnHelpCallList.Location = new System.Drawing.Point(71, 170);
      this.BtnHelpCallList.Name = "BtnHelpCallList";
      this.BtnHelpCallList.Size = new System.Drawing.Size(16, 16);
      this.BtnHelpCallList.TabIndex = 116;
      this.BtnHelpCallList.UseVisualStyleBackColor = false;
      this.BtnHelpCallList.Click += new System.EventHandler(this.BtnHelpCallList_Click);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 523);
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
      this.BtnHelpConsumer.Location = new System.Drawing.Point(75, 521);
      this.BtnHelpConsumer.Name = "BtnHelpConsumer";
      this.BtnHelpConsumer.Size = new System.Drawing.Size(16, 16);
      this.BtnHelpConsumer.TabIndex = 120;
      this.BtnHelpConsumer.UseVisualStyleBackColor = false;
      this.BtnHelpConsumer.Click += new System.EventHandler(this.BtnHelpConsumer_Click);
      // 
      // BtnConsumer
      // 
      this.BtnConsumer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.BtnConsumer.Location = new System.Drawing.Point(110, 518);
      this.BtnConsumer.Name = "BtnConsumer";
      this.BtnConsumer.Size = new System.Drawing.Size(30, 23);
      this.BtnConsumer.TabIndex = 119;
      this.BtnConsumer.Text = "0";
      this.BtnConsumer.UseVisualStyleBackColor = true;
      this.BtnConsumer.Click += new System.EventHandler(this.BtnConsumer_Click);
      // 
      // BtnDeleteOutput
      // 
      this.BtnDeleteOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnDeleteOutput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDeleteOutput.BackgroundImage")));
      this.BtnDeleteOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDeleteOutput.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnDeleteOutput.Location = new System.Drawing.Point(685, 140);
      this.BtnDeleteOutput.Name = "BtnDeleteOutput";
      this.BtnDeleteOutput.Size = new System.Drawing.Size(23, 23);
      this.BtnDeleteOutput.TabIndex = 123;
      this.BtnDeleteOutput.UseVisualStyleBackColor = true;
      this.BtnDeleteOutput.Click += new System.EventHandler(this.BtnDeleteOutput_Click);
      // 
      // BtnDeleteInput
      // 
      this.BtnDeleteInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnDeleteInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDeleteInput.BackgroundImage")));
      this.BtnDeleteInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDeleteInput.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnDeleteInput.Location = new System.Drawing.Point(685, 113);
      this.BtnDeleteInput.Name = "BtnDeleteInput";
      this.BtnDeleteInput.Size = new System.Drawing.Size(23, 23);
      this.BtnDeleteInput.TabIndex = 122;
      this.BtnDeleteInput.UseVisualStyleBackColor = true;
      this.BtnDeleteInput.Click += new System.EventHandler(this.BtnDeleteInput_Click);
      // 
      // FormSBizRuleOperation
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(724, 573);
      this.Controls.Add(this.BtnDeleteOutput);
      this.Controls.Add(this.BtnDeleteInput);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.BtnHelpConsumer);
      this.Controls.Add(this.BtnConsumer);
      this.Controls.Add(this.BtnHelpCallList);
      this.Controls.Add(this.LblSampleBROperationName);
      this.Controls.Add(this.BtnAddComment);
      this.Controls.Add(this.BtnMoveUpCall);
      this.Controls.Add(this.BtnMoveDownCall);
      this.Controls.Add(this.BtnDeleteCall);
      this.Controls.Add(this.BtnEditComment);
      this.Controls.Add(this.LbxMessageList);
      this.Controls.Add(this.LblCallList);
      this.Controls.Add(this.TbxOutput);
      this.Controls.Add(this.LblReturn);
      this.Controls.Add(this.TbxInput);
      this.Controls.Add(this.LblParameter);
      this.Controls.Add(this.LblSampleMethodName);
      this.Controls.Add(this.CkbTransaction);
      this.Controls.Add(this.BtnTranslate);
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
      this.Controls.Add(this.LblDesc);
      this.Controls.Add(this.TbxDescription);
      this.Controls.Add(this.TbxID);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.TbxName);
      this.Controls.Add(this.LblIDMS);
      this.Controls.Add(this.BtnSave);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormSBizRuleOperation";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "FormBizPackage";
      this.Load += new System.EventHandler(this.FormSBizRuleOperation_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox TbxLastModified;
    private System.Windows.Forms.Label LblLastModified;
    private System.Windows.Forms.TextBox TbxRegistered;
    private System.Windows.Forms.Label label88;
    private System.Windows.Forms.Label LblDesc;
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
    private System.Windows.Forms.Button BtnTranslate;
    private System.Windows.Forms.CheckBox CkbDesignDetail;
    private System.Windows.Forms.CheckBox CkbDesignSkeleton;
    private System.Windows.Forms.CheckBox CkbTransaction;
    private System.Windows.Forms.Label LblSampleMethodName;
    private System.Windows.Forms.TextBox TbxOutput;
    private System.Windows.Forms.Label LblReturn;
    private System.Windows.Forms.TextBox TbxInput;
    private System.Windows.Forms.Label LblParameter;
    private System.Windows.Forms.Button BtnAddComment;
    private System.Windows.Forms.ListBox LbxMessageList;
    private System.Windows.Forms.Label LblCallList;
    private System.Windows.Forms.Button BtnMoveUpCall;
    private System.Windows.Forms.Button BtnMoveDownCall;
    private System.Windows.Forms.Button BtnDeleteCall;
    private System.Windows.Forms.Button BtnEditComment;
    private System.Windows.Forms.Label LblSampleBROperationName;
    private System.Windows.Forms.Button BtnHelpCallList;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button BtnHelpConsumer;
    private System.Windows.Forms.Button BtnConsumer;
    private System.Windows.Forms.Button BtnDeleteOutput;
    private System.Windows.Forms.Button BtnDeleteInput;
  }
}