namespace SDM
{
  partial class FormSUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSUI));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
      this.BtnViewSpec = new System.Windows.Forms.Button();
      this.BtnSpecFile = new System.Windows.Forms.Button();
      this.TbxSpecFile = new System.Windows.Forms.TextBox();
      this.LblSpecFile = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.LblSampleName = new System.Windows.Forms.Label();
      this.LblSampleProgram = new System.Windows.Forms.Label();
      this.BtnTranslate = new System.Windows.Forms.Button();
      this.RbnUIWidget = new System.Windows.Forms.RadioButton();
      this.RbnUIPopup = new System.Windows.Forms.RadioButton();
      this.RbnUIMain = new System.Windows.Forms.RadioButton();
      this.label30 = new System.Windows.Forms.Label();
      this.LblProgramName = new System.Windows.Forms.Label();
      this.TbxUIProgram = new System.Windows.Forms.TextBox();
      this.LblEventList = new System.Windows.Forms.Label();
      this.DgvEventList = new System.Windows.Forms.DataGridView();
      this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnRelatedAPI = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CkbDesignDetail = new System.Windows.Forms.CheckBox();
      this.CkbDesignSkeleton = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.DgvEventList)).BeginInit();
      this.SuspendLayout();
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(396, 468);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 14;
      // 
      // LblLastModified
      // 
      this.LblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblLastModified.AutoSize = true;
      this.LblLastModified.Location = new System.Drawing.Point(313, 472);
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
      this.TbxRegistered.Location = new System.Drawing.Point(110, 468);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(188, 21);
      this.TbxRegistered.TabIndex = 13;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(10, 472);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 52;
      this.label88.Text = "Registered";
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Location = new System.Drawing.Point(10, 141);
      this.label40.Name = "label40";
      this.label40.Size = new System.Drawing.Size(68, 12);
      this.label40.TabIndex = 50;
      this.label40.Text = "Description";
      // 
      // TbxDescription
      // 
      this.TbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxDescription.Location = new System.Drawing.Point(110, 138);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(486, 101);
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
      this.TbxName.TabIndex = 2;
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
      this.BtnSave.Location = new System.Drawing.Point(521, 495);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 15;
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
      // BtnViewSpec
      // 
      this.BtnViewSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnViewSpec.Image = ((System.Drawing.Image)(resources.GetObject("BtnViewSpec.Image")));
      this.BtnViewSpec.Location = new System.Drawing.Point(573, 439);
      this.BtnViewSpec.Name = "BtnViewSpec";
      this.BtnViewSpec.Size = new System.Drawing.Size(23, 23);
      this.BtnViewSpec.TabIndex = 12;
      this.BtnViewSpec.UseVisualStyleBackColor = true;
      this.BtnViewSpec.Click += new System.EventHandler(this.BtnViewSpec_Click);
      // 
      // BtnSpecFile
      // 
      this.BtnSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSpecFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpecFile.Image")));
      this.BtnSpecFile.Location = new System.Drawing.Point(549, 439);
      this.BtnSpecFile.Name = "BtnSpecFile";
      this.BtnSpecFile.Size = new System.Drawing.Size(23, 23);
      this.BtnSpecFile.TabIndex = 11;
      this.BtnSpecFile.UseVisualStyleBackColor = true;
      this.BtnSpecFile.Click += new System.EventHandler(this.BtnSpecFile_Click);
      // 
      // TbxSpecFile
      // 
      this.TbxSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSpecFile.Location = new System.Drawing.Point(110, 441);
      this.TbxSpecFile.Name = "TbxSpecFile";
      this.TbxSpecFile.Size = new System.Drawing.Size(433, 21);
      this.TbxSpecFile.TabIndex = 10;
      // 
      // LblSpecFile
      // 
      this.LblSpecFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LblSpecFile.AutoSize = true;
      this.LblSpecFile.Location = new System.Drawing.Point(10, 444);
      this.LblSpecFile.Name = "LblSpecFile";
      this.LblSpecFile.Size = new System.Drawing.Size(58, 12);
      this.LblSpecFile.TabIndex = 65;
      this.LblSpecFile.Text = "Spec File";
      // 
      // LblSampleName
      // 
      this.LblSampleName.AutoSize = true;
      this.LblSampleName.Location = new System.Drawing.Point(346, 65);
      this.LblSampleName.Name = "LblSampleName";
      this.LblSampleName.Size = new System.Drawing.Size(76, 12);
      this.LblSampleName.TabIndex = 96;
      this.LblSampleName.Text = "(ex : 주문UI)";
      // 
      // LblSampleProgram
      // 
      this.LblSampleProgram.AutoSize = true;
      this.LblSampleProgram.Location = new System.Drawing.Point(383, 114);
      this.LblSampleProgram.Name = "LblSampleProgram";
      this.LblSampleProgram.Size = new System.Drawing.Size(106, 12);
      this.LblSampleProgram.TabIndex = 104;
      this.LblSampleProgram.Text = "(ex : orderM.xml)";
      // 
      // BtnTranslate
      // 
      this.BtnTranslate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnTranslate.Image = ((System.Drawing.Image)(resources.GetObject("BtnTranslate.Image")));
      this.BtnTranslate.Location = new System.Drawing.Point(356, 109);
      this.BtnTranslate.Name = "BtnTranslate";
      this.BtnTranslate.Size = new System.Drawing.Size(23, 23);
      this.BtnTranslate.TabIndex = 7;
      this.BtnTranslate.UseVisualStyleBackColor = true;
      this.BtnTranslate.Visible = false;
      this.BtnTranslate.Click += new System.EventHandler(this.BtnTranslate_Click);
      // 
      // RbnUIWidget
      // 
      this.RbnUIWidget.AutoSize = true;
      this.RbnUIWidget.Location = new System.Drawing.Point(220, 89);
      this.RbnUIWidget.Name = "RbnUIWidget";
      this.RbnUIWidget.Size = new System.Drawing.Size(60, 16);
      this.RbnUIWidget.TabIndex = 5;
      this.RbnUIWidget.TabStop = true;
      this.RbnUIWidget.Text = "Widget";
      this.RbnUIWidget.UseVisualStyleBackColor = true;
      // 
      // RbnUIPopup
      // 
      this.RbnUIPopup.AutoSize = true;
      this.RbnUIPopup.Location = new System.Drawing.Point(161, 89);
      this.RbnUIPopup.Name = "RbnUIPopup";
      this.RbnUIPopup.Size = new System.Drawing.Size(59, 16);
      this.RbnUIPopup.TabIndex = 4;
      this.RbnUIPopup.TabStop = true;
      this.RbnUIPopup.Text = "Popup";
      this.RbnUIPopup.UseVisualStyleBackColor = true;
      // 
      // RbnUIMain
      // 
      this.RbnUIMain.AutoSize = true;
      this.RbnUIMain.Location = new System.Drawing.Point(110, 89);
      this.RbnUIMain.Name = "RbnUIMain";
      this.RbnUIMain.Size = new System.Drawing.Size(51, 16);
      this.RbnUIMain.TabIndex = 3;
      this.RbnUIMain.TabStop = true;
      this.RbnUIMain.Text = "Main";
      this.RbnUIMain.UseVisualStyleBackColor = true;
      // 
      // label30
      // 
      this.label30.AutoSize = true;
      this.label30.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label30.Location = new System.Drawing.Point(10, 91);
      this.label30.Name = "label30";
      this.label30.Size = new System.Drawing.Size(34, 12);
      this.label30.TabIndex = 99;
      this.label30.Text = "Type";
      // 
      // LblProgramName
      // 
      this.LblProgramName.AutoSize = true;
      this.LblProgramName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblProgramName.Location = new System.Drawing.Point(10, 115);
      this.LblProgramName.Name = "LblProgramName";
      this.LblProgramName.Size = new System.Drawing.Size(53, 12);
      this.LblProgramName.TabIndex = 98;
      this.LblProgramName.Text = "Program";
      // 
      // TbxUIProgram
      // 
      this.TbxUIProgram.Location = new System.Drawing.Point(110, 111);
      this.TbxUIProgram.Name = "TbxUIProgram";
      this.TbxUIProgram.Size = new System.Drawing.Size(242, 21);
      this.TbxUIProgram.TabIndex = 6;
      // 
      // LblEventList
      // 
      this.LblEventList.AutoSize = true;
      this.LblEventList.Location = new System.Drawing.Point(10, 245);
      this.LblEventList.Name = "LblEventList";
      this.LblEventList.Size = new System.Drawing.Size(60, 12);
      this.LblEventList.TabIndex = 106;
      this.LblEventList.Text = "Event List";
      // 
      // DgvEventList
      // 
      this.DgvEventList.AllowDrop = true;
      this.DgvEventList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DgvEventList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.DgvEventList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.DgvEventList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DgvEventList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12,
            this.ColumnEventName,
            this.ColumnRelatedAPI});
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.DgvEventList.DefaultCellStyle = dataGridViewCellStyle3;
      this.DgvEventList.Location = new System.Drawing.Point(110, 245);
      this.DgvEventList.MultiSelect = false;
      this.DgvEventList.Name = "DgvEventList";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.DgvEventList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.DgvEventList.RowHeadersWidth = 25;
      this.DgvEventList.RowTemplate.Height = 23;
      this.DgvEventList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.DgvEventList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DgvEventList.Size = new System.Drawing.Size(490, 188);
      this.DgvEventList.TabIndex = 9;
      this.DgvEventList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvEventList_CellDoubleClick);
      this.DgvEventList.DragDrop += new System.Windows.Forms.DragEventHandler(this.DgvEventList_DragDrop);
      this.DgvEventList.DragEnter += new System.Windows.Forms.DragEventHandler(this.DgvEventList_DragEnter);
      // 
      // Column12
      // 
      this.Column12.FillWeight = 27.84921F;
      this.Column12.HeaderText = "ID";
      this.Column12.Name = "Column12";
      // 
      // ColumnEventName
      // 
      this.ColumnEventName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.ColumnEventName.FillWeight = 62.5547F;
      this.ColumnEventName.HeaderText = "Event Name";
      this.ColumnEventName.Name = "ColumnEventName";
      // 
      // ColumnRelatedAPI
      // 
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
      this.ColumnRelatedAPI.DefaultCellStyle = dataGridViewCellStyle2;
      this.ColumnRelatedAPI.FillWeight = 183.9108F;
      this.ColumnRelatedAPI.HeaderText = "Callee API";
      this.ColumnRelatedAPI.Name = "ColumnRelatedAPI";
      // 
      // CkbDesignDetail
      // 
      this.CkbDesignDetail.AutoSize = true;
      this.CkbDesignDetail.Location = new System.Drawing.Point(444, 36);
      this.CkbDesignDetail.Name = "CkbDesignDetail";
      this.CkbDesignDetail.Size = new System.Drawing.Size(96, 16);
      this.CkbDesignDetail.TabIndex = 118;
      this.CkbDesignDetail.Text = "상세설계완료";
      this.CkbDesignDetail.UseVisualStyleBackColor = true;
      this.CkbDesignDetail.CheckedChanged += new System.EventHandler(this.CkbDesignDetail_CheckedChanged);
      // 
      // CkbDesignSkeleton
      // 
      this.CkbDesignSkeleton.AutoSize = true;
      this.CkbDesignSkeleton.Location = new System.Drawing.Point(348, 36);
      this.CkbDesignSkeleton.Name = "CkbDesignSkeleton";
      this.CkbDesignSkeleton.Size = new System.Drawing.Size(96, 16);
      this.CkbDesignSkeleton.TabIndex = 117;
      this.CkbDesignSkeleton.Text = "기본설계완료";
      this.CkbDesignSkeleton.UseVisualStyleBackColor = true;
      this.CkbDesignSkeleton.CheckedChanged += new System.EventHandler(this.CkbDesignSkeleton_CheckedChanged);
      // 
      // FormSUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 530);
      this.Controls.Add(this.CkbDesignDetail);
      this.Controls.Add(this.CkbDesignSkeleton);
      this.Controls.Add(this.LblEventList);
      this.Controls.Add(this.DgvEventList);
      this.Controls.Add(this.LblSampleProgram);
      this.Controls.Add(this.BtnTranslate);
      this.Controls.Add(this.RbnUIWidget);
      this.Controls.Add(this.RbnUIPopup);
      this.Controls.Add(this.RbnUIMain);
      this.Controls.Add(this.label30);
      this.Controls.Add(this.LblProgramName);
      this.Controls.Add(this.TbxUIProgram);
      this.Controls.Add(this.LblSampleName);
      this.Controls.Add(this.BtnViewSpec);
      this.Controls.Add(this.BtnSpecFile);
      this.Controls.Add(this.TbxSpecFile);
      this.Controls.Add(this.LblSpecFile);
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
      this.Name = "FormSUI";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "FormBizPackage";
      this.Load += new System.EventHandler(this.FormSUI_Load);
      ((System.ComponentModel.ISupportInitialize)(this.DgvEventList)).EndInit();
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
    private System.Windows.Forms.Button BtnViewSpec;
    private System.Windows.Forms.Button BtnSpecFile;
    private System.Windows.Forms.TextBox TbxSpecFile;
    private System.Windows.Forms.Label LblSpecFile;
    private System.Windows.Forms.ToolTip toolTip1;
    public System.Windows.Forms.TextBox TbxName;
    private System.Windows.Forms.Label LblSampleName;
    private System.Windows.Forms.Label LblSampleProgram;
    private System.Windows.Forms.Button BtnTranslate;
    private System.Windows.Forms.RadioButton RbnUIWidget;
    private System.Windows.Forms.RadioButton RbnUIPopup;
    private System.Windows.Forms.RadioButton RbnUIMain;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label LblProgramName;
    private System.Windows.Forms.TextBox TbxUIProgram;
    private System.Windows.Forms.Label LblEventList;
    private System.Windows.Forms.DataGridView DgvEventList;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEventName;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRelatedAPI;
    private System.Windows.Forms.CheckBox CkbDesignDetail;
    private System.Windows.Forms.CheckBox CkbDesignSkeleton;
  }
}