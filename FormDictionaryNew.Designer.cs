namespace SDM
{
  partial class FormDictionaryNew
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDictionaryNew));
      this.DgvWord = new System.Windows.Forms.DataGridView();
      this.TbxSearchEnglish = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.TbxSearchKorean = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.BtnSearch = new System.Windows.Forms.Button();
      this.CmbType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.TabWord = new System.Windows.Forms.TabPage();
      this.BtnSaveSingle = new System.Windows.Forms.Button();
      this.TbxEnglish = new System.Windows.Forms.TextBox();
      this.TbxKorean = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.BtnNew = new System.Windows.Forms.Button();
      this.TbxInput = new System.Windows.Forms.TextBox();
      this.LblUploaderDescription = new System.Windows.Forms.Label();
      this.BtnSaveMulti = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.DgvWord)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.TabWord.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // DgvWord
      // 
      this.DgvWord.AllowUserToAddRows = false;
      this.DgvWord.AllowUserToDeleteRows = false;
      this.DgvWord.AllowUserToResizeRows = false;
      this.DgvWord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.DgvWord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DgvWord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
      this.DgvWord.Location = new System.Drawing.Point(14, 32);
      this.DgvWord.MultiSelect = false;
      this.DgvWord.Name = "DgvWord";
      this.DgvWord.ReadOnly = true;
      this.DgvWord.RowHeadersVisible = false;
      this.DgvWord.RowTemplate.Height = 23;
      this.DgvWord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.DgvWord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.DgvWord.Size = new System.Drawing.Size(415, 406);
      this.DgvWord.TabIndex = 1;
      this.DgvWord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvWord_CellDoubleClick);
      // 
      // TbxSearchEnglish
      // 
      this.TbxSearchEnglish.Location = new System.Drawing.Point(324, 5);
      this.TbxSearchEnglish.Name = "TbxSearchEnglish";
      this.TbxSearchEnglish.Size = new System.Drawing.Size(76, 21);
      this.TbxSearchEnglish.TabIndex = 123;
      this.TbxSearchEnglish.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxSearchEnglish_KeyDown);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(271, 9);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(47, 12);
      this.label4.TabIndex = 126;
      this.label4.Text = "English";
      // 
      // TbxSearchKorean
      // 
      this.TbxSearchKorean.Location = new System.Drawing.Point(178, 5);
      this.TbxSearchKorean.Name = "TbxSearchKorean";
      this.TbxSearchKorean.Size = new System.Drawing.Size(76, 21);
      this.TbxSearchKorean.TabIndex = 122;
      this.TbxSearchKorean.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxSearchKorean_KeyDown);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(143, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(29, 12);
      this.label3.TabIndex = 125;
      this.label3.Text = "한글";
      // 
      // BtnSearch
      // 
      this.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.Image")));
      this.BtnSearch.Location = new System.Drawing.Point(406, 4);
      this.BtnSearch.Name = "BtnSearch";
      this.BtnSearch.Size = new System.Drawing.Size(23, 23);
      this.BtnSearch.TabIndex = 124;
      this.BtnSearch.UseVisualStyleBackColor = true;
      this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
      // 
      // CmbType
      // 
      this.CmbType.DisplayMember = "Name";
      this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbType.FormattingEnabled = true;
      this.CmbType.Location = new System.Drawing.Point(52, 5);
      this.CmbType.Name = "CmbType";
      this.CmbType.Size = new System.Drawing.Size(76, 20);
      this.CmbType.Sorted = true;
      this.CmbType.TabIndex = 127;
      this.CmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 12);
      this.label1.TabIndex = 128;
      this.label1.Text = "Type";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.TabWord);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(439, 32);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(241, 406);
      this.tabControl1.TabIndex = 129;
      // 
      // TabWord
      // 
      this.TabWord.Controls.Add(this.BtnNew);
      this.TabWord.Controls.Add(this.BtnSaveSingle);
      this.TabWord.Controls.Add(this.TbxEnglish);
      this.TabWord.Controls.Add(this.TbxKorean);
      this.TabWord.Controls.Add(this.label2);
      this.TabWord.Controls.Add(this.label5);
      this.TabWord.Location = new System.Drawing.Point(4, 22);
      this.TabWord.Name = "TabWord";
      this.TabWord.Padding = new System.Windows.Forms.Padding(3);
      this.TabWord.Size = new System.Drawing.Size(233, 380);
      this.TabWord.TabIndex = 0;
      this.TabWord.Text = "Single";
      this.TabWord.UseVisualStyleBackColor = true;
      // 
      // BtnSaveSingle
      // 
      this.BtnSaveSingle.Location = new System.Drawing.Point(142, 70);
      this.BtnSaveSingle.Name = "BtnSaveSingle";
      this.BtnSaveSingle.Size = new System.Drawing.Size(75, 23);
      this.BtnSaveSingle.TabIndex = 134;
      this.BtnSaveSingle.Text = "Save";
      this.BtnSaveSingle.UseVisualStyleBackColor = true;
      this.BtnSaveSingle.Click += new System.EventHandler(this.BtnSaveSingle_Click);
      // 
      // TbxEnglish
      // 
      this.TbxEnglish.Location = new System.Drawing.Point(63, 43);
      this.TbxEnglish.Name = "TbxEnglish";
      this.TbxEnglish.Size = new System.Drawing.Size(154, 21);
      this.TbxEnglish.TabIndex = 131;
      // 
      // TbxKorean
      // 
      this.TbxKorean.Location = new System.Drawing.Point(63, 16);
      this.TbxKorean.Name = "TbxKorean";
      this.TbxKorean.Size = new System.Drawing.Size(154, 21);
      this.TbxKorean.TabIndex = 130;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 133;
      this.label2.Text = "English";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(29, 12);
      this.label5.TabIndex = 132;
      this.label5.Text = "한글";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.BtnSaveMulti);
      this.tabPage2.Controls.Add(this.TbxInput);
      this.tabPage2.Controls.Add(this.LblUploaderDescription);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(233, 380);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Upload";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "Korean";
      this.Column1.HeaderText = "한글";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "English";
      this.Column2.HeaderText = "Engilsh";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // BtnNew
      // 
      this.BtnNew.Location = new System.Drawing.Point(61, 70);
      this.BtnNew.Name = "BtnNew";
      this.BtnNew.Size = new System.Drawing.Size(75, 23);
      this.BtnNew.TabIndex = 135;
      this.BtnNew.Text = "New";
      this.BtnNew.UseVisualStyleBackColor = true;
      this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
      // 
      // TbxInput
      // 
      this.TbxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxInput.Location = new System.Drawing.Point(11, 28);
      this.TbxInput.Multiline = true;
      this.TbxInput.Name = "TbxInput";
      this.TbxInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxInput.Size = new System.Drawing.Size(210, 317);
      this.TbxInput.TabIndex = 4;
      // 
      // LblUploaderDescription
      // 
      this.LblUploaderDescription.AutoSize = true;
      this.LblUploaderDescription.Location = new System.Drawing.Point(11, 9);
      this.LblUploaderDescription.Name = "LblUploaderDescription";
      this.LblUploaderDescription.Size = new System.Drawing.Size(199, 12);
      this.LblUploaderDescription.TabIndex = 3;
      this.LblUploaderDescription.Text = "\'한글,English\'와 같이 한 줄 씩 입력";
      // 
      // BtnSaveMulti
      // 
      this.BtnSaveMulti.Location = new System.Drawing.Point(146, 351);
      this.BtnSaveMulti.Name = "BtnSaveMulti";
      this.BtnSaveMulti.Size = new System.Drawing.Size(75, 23);
      this.BtnSaveMulti.TabIndex = 135;
      this.BtnSaveMulti.Text = "Save";
      this.BtnSaveMulti.UseVisualStyleBackColor = true;
      this.BtnSaveMulti.Click += new System.EventHandler(this.BtnSaveMulti_Click);
      // 
      // FormDictionaryNew
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(690, 450);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.CmbType);
      this.Controls.Add(this.TbxSearchEnglish);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.TbxSearchKorean);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.BtnSearch);
      this.Controls.Add(this.DgvWord);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormDictionaryNew";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Dictionary";
      this.Load += new System.EventHandler(this.FormDictionaryNew_Load);
      ((System.ComponentModel.ISupportInitialize)(this.DgvWord)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.TabWord.ResumeLayout(false);
      this.TabWord.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView DgvWord;
    private System.Windows.Forms.TextBox TbxSearchEnglish;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox TbxSearchKorean;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button BtnSearch;
    private System.Windows.Forms.ComboBox CmbType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage TabWord;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button BtnSaveSingle;
    private System.Windows.Forms.TextBox TbxEnglish;
    private System.Windows.Forms.TextBox TbxKorean;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.Button BtnNew;
    private System.Windows.Forms.Button BtnSaveMulti;
    private System.Windows.Forms.TextBox TbxInput;
    private System.Windows.Forms.Label LblUploaderDescription;
  }
}