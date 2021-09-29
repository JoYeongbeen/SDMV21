namespace SDM
{
  partial class FormUser
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUser));
      this.TrvUser = new System.Windows.Forms.TreeView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.BtnRefresh = new System.Windows.Forms.Button();
      this.CkbExpandAll = new System.Windows.Forms.CheckBox();
      this.LblUserNameSample = new System.Windows.Forms.Label();
      this.PnlRole = new System.Windows.Forms.Panel();
      this.RbnPM = new System.Windows.Forms.RadioButton();
      this.RbnDeveloper = new System.Windows.Forms.RadioButton();
      this.RbnDesigner = new System.Windows.Forms.RadioButton();
      this.RbnModeler = new System.Windows.Forms.RadioButton();
      this.RbnPL = new System.Windows.Forms.RadioButton();
      this.RbnQA = new System.Windows.Forms.RadioButton();
      this.CkbViewer = new System.Windows.Forms.CheckBox();
      this.LblRole = new System.Windows.Forms.Label();
      this.TbxPassword = new System.Windows.Forms.TextBox();
      this.LblPassword = new System.Windows.Forms.Label();
      this.TbxLastModified = new System.Windows.Forms.TextBox();
      this.label89 = new System.Windows.Forms.Label();
      this.TbxRegistered = new System.Windows.Forms.TextBox();
      this.label88 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label40 = new System.Windows.Forms.Label();
      this.TbxDescription = new System.Windows.Forms.TextBox();
      this.RbnUser = new System.Windows.Forms.RadioButton();
      this.RbnPart = new System.Windows.Forms.RadioButton();
      this.TbxName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.BtnSave = new System.Windows.Forms.Button();
      this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddUserMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddPartMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.BtnHelpCallList = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.PnlRole.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TrvUser
      // 
      this.TrvUser.AllowDrop = true;
      this.TrvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TrvUser.ImageIndex = 0;
      this.TrvUser.ImageList = this.imageList1;
      this.TrvUser.Location = new System.Drawing.Point(7, 26);
      this.TrvUser.Name = "TrvUser";
      this.TrvUser.SelectedImageIndex = 0;
      this.TrvUser.Size = new System.Drawing.Size(219, 296);
      this.TrvUser.TabIndex = 0;
      this.TrvUser.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvUser_BeforeCollapse);
      this.TrvUser.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvUser_BeforeExpand);
      this.TrvUser.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvUser_NodeMouseDoubleClick);
      this.TrvUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrvUser_MouseDown);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "SDM");
      this.imageList1.Images.SetKeyName(1, "Part");
      this.imageList1.Images.SetKeyName(2, "User");
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer1.Location = new System.Drawing.Point(12, 12);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.BtnRefresh);
      this.splitContainer1.Panel1.Controls.Add(this.TrvUser);
      this.splitContainer1.Panel1.Controls.Add(this.CkbExpandAll);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.BtnHelpCallList);
      this.splitContainer1.Panel2.Controls.Add(this.LblUserNameSample);
      this.splitContainer1.Panel2.Controls.Add(this.PnlRole);
      this.splitContainer1.Panel2.Controls.Add(this.CkbViewer);
      this.splitContainer1.Panel2.Controls.Add(this.LblRole);
      this.splitContainer1.Panel2.Controls.Add(this.TbxPassword);
      this.splitContainer1.Panel2.Controls.Add(this.LblPassword);
      this.splitContainer1.Panel2.Controls.Add(this.TbxLastModified);
      this.splitContainer1.Panel2.Controls.Add(this.label89);
      this.splitContainer1.Panel2.Controls.Add(this.TbxRegistered);
      this.splitContainer1.Panel2.Controls.Add(this.label88);
      this.splitContainer1.Panel2.Controls.Add(this.label2);
      this.splitContainer1.Panel2.Controls.Add(this.label40);
      this.splitContainer1.Panel2.Controls.Add(this.TbxDescription);
      this.splitContainer1.Panel2.Controls.Add(this.RbnUser);
      this.splitContainer1.Panel2.Controls.Add(this.RbnPart);
      this.splitContainer1.Panel2.Controls.Add(this.TbxName);
      this.splitContainer1.Panel2.Controls.Add(this.label1);
      this.splitContainer1.Panel2.Controls.Add(this.BtnSave);
      this.splitContainer1.Size = new System.Drawing.Size(788, 329);
      this.splitContainer1.SplitterDistance = 233;
      this.splitContainer1.TabIndex = 13;
      // 
      // BtnRefresh
      // 
      this.BtnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("BtnRefresh.Image")));
      this.BtnRefresh.Location = new System.Drawing.Point(203, 0);
      this.BtnRefresh.Name = "BtnRefresh";
      this.BtnRefresh.Size = new System.Drawing.Size(23, 23);
      this.BtnRefresh.TabIndex = 7;
      this.BtnRefresh.UseVisualStyleBackColor = true;
      this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
      // 
      // CkbExpandAll
      // 
      this.CkbExpandAll.AutoSize = true;
      this.CkbExpandAll.Checked = true;
      this.CkbExpandAll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CkbExpandAll.Location = new System.Drawing.Point(7, 3);
      this.CkbExpandAll.Name = "CkbExpandAll";
      this.CkbExpandAll.Size = new System.Drawing.Size(85, 16);
      this.CkbExpandAll.TabIndex = 0;
      this.CkbExpandAll.Text = "Expand All";
      this.CkbExpandAll.UseVisualStyleBackColor = true;
      this.CkbExpandAll.CheckedChanged += new System.EventHandler(this.CkbExpandAll_CheckedChanged);
      // 
      // LblUserNameSample
      // 
      this.LblUserNameSample.AutoSize = true;
      this.LblUserNameSample.Location = new System.Drawing.Point(258, 51);
      this.LblUserNameSample.Name = "LblUserNameSample";
      this.LblUserNameSample.Size = new System.Drawing.Size(75, 12);
      this.LblUserNameSample.TabIndex = 40;
      this.LblUserNameSample.Text = "(예 : 홍길동)";
      // 
      // PnlRole
      // 
      this.PnlRole.Controls.Add(this.RbnPM);
      this.PnlRole.Controls.Add(this.RbnDeveloper);
      this.PnlRole.Controls.Add(this.RbnDesigner);
      this.PnlRole.Controls.Add(this.RbnModeler);
      this.PnlRole.Controls.Add(this.RbnPL);
      this.PnlRole.Controls.Add(this.RbnQA);
      this.PnlRole.Location = new System.Drawing.Point(93, 99);
      this.PnlRole.Name = "PnlRole";
      this.PnlRole.Size = new System.Drawing.Size(365, 21);
      this.PnlRole.TabIndex = 39;
      // 
      // RbnPM
      // 
      this.RbnPM.AutoSize = true;
      this.RbnPM.Location = new System.Drawing.Point(322, 3);
      this.RbnPM.Name = "RbnPM";
      this.RbnPM.Size = new System.Drawing.Size(42, 16);
      this.RbnPM.TabIndex = 5;
      this.RbnPM.TabStop = true;
      this.RbnPM.Text = "PM";
      this.RbnPM.UseVisualStyleBackColor = true;
      this.RbnPM.CheckedChanged += new System.EventHandler(this.RbnPM_CheckedChanged);
      // 
      // RbnDeveloper
      // 
      this.RbnDeveloper.AutoSize = true;
      this.RbnDeveloper.Location = new System.Drawing.Point(80, 3);
      this.RbnDeveloper.Name = "RbnDeveloper";
      this.RbnDeveloper.Size = new System.Drawing.Size(79, 16);
      this.RbnDeveloper.TabIndex = 1;
      this.RbnDeveloper.TabStop = true;
      this.RbnDeveloper.Text = "Developer";
      this.RbnDeveloper.UseVisualStyleBackColor = true;
      this.RbnDeveloper.CheckedChanged += new System.EventHandler(this.RbnDeveloper_CheckedChanged);
      // 
      // RbnDesigner
      // 
      this.RbnDesigner.AutoSize = true;
      this.RbnDesigner.Location = new System.Drawing.Point(3, 3);
      this.RbnDesigner.Name = "RbnDesigner";
      this.RbnDesigner.Size = new System.Drawing.Size(73, 16);
      this.RbnDesigner.TabIndex = 0;
      this.RbnDesigner.TabStop = true;
      this.RbnDesigner.Text = "Designer";
      this.RbnDesigner.UseVisualStyleBackColor = true;
      this.RbnDesigner.CheckedChanged += new System.EventHandler(this.RbnDesigner_CheckedChanged);
      // 
      // RbnModeler
      // 
      this.RbnModeler.AutoSize = true;
      this.RbnModeler.Location = new System.Drawing.Point(205, 3);
      this.RbnModeler.Name = "RbnModeler";
      this.RbnModeler.Size = new System.Drawing.Size(69, 16);
      this.RbnModeler.TabIndex = 3;
      this.RbnModeler.TabStop = true;
      this.RbnModeler.Text = "Modeler";
      this.RbnModeler.UseVisualStyleBackColor = true;
      this.RbnModeler.CheckedChanged += new System.EventHandler(this.RbnModeler_CheckedChanged);
      // 
      // RbnPL
      // 
      this.RbnPL.AutoSize = true;
      this.RbnPL.Location = new System.Drawing.Point(163, 3);
      this.RbnPL.Name = "RbnPL";
      this.RbnPL.Size = new System.Drawing.Size(38, 16);
      this.RbnPL.TabIndex = 2;
      this.RbnPL.TabStop = true;
      this.RbnPL.Text = "PL";
      this.RbnPL.UseVisualStyleBackColor = true;
      this.RbnPL.CheckedChanged += new System.EventHandler(this.RbnPL_CheckedChanged);
      // 
      // RbnQA
      // 
      this.RbnQA.AutoSize = true;
      this.RbnQA.Location = new System.Drawing.Point(278, 3);
      this.RbnQA.Name = "RbnQA";
      this.RbnQA.Size = new System.Drawing.Size(40, 16);
      this.RbnQA.TabIndex = 4;
      this.RbnQA.TabStop = true;
      this.RbnQA.Text = "QA";
      this.RbnQA.UseVisualStyleBackColor = true;
      this.RbnQA.CheckedChanged += new System.EventHandler(this.RbnQA_CheckedChanged);
      // 
      // CkbViewer
      // 
      this.CkbViewer.AutoSize = true;
      this.CkbViewer.Location = new System.Drawing.Point(465, 102);
      this.CkbViewer.Name = "CkbViewer";
      this.CkbViewer.Size = new System.Drawing.Size(80, 16);
      this.CkbViewer.TabIndex = 4;
      this.CkbViewer.Text = "View only";
      this.CkbViewer.UseVisualStyleBackColor = true;
      // 
      // LblRole
      // 
      this.LblRole.AutoSize = true;
      this.LblRole.Location = new System.Drawing.Point(9, 104);
      this.LblRole.Name = "LblRole";
      this.LblRole.Size = new System.Drawing.Size(30, 12);
      this.LblRole.TabIndex = 32;
      this.LblRole.Text = "Role";
      // 
      // TbxPassword
      // 
      this.TbxPassword.Location = new System.Drawing.Point(93, 75);
      this.TbxPassword.Name = "TbxPassword";
      this.TbxPassword.PasswordChar = '*';
      this.TbxPassword.Size = new System.Drawing.Size(159, 21);
      this.TbxPassword.TabIndex = 3;
      // 
      // LblPassword
      // 
      this.LblPassword.AutoSize = true;
      this.LblPassword.Location = new System.Drawing.Point(9, 78);
      this.LblPassword.Name = "LblPassword";
      this.LblPassword.Size = new System.Drawing.Size(62, 12);
      this.LblPassword.TabIndex = 30;
      this.LblPassword.Text = "Password";
      // 
      // TbxLastModified
      // 
      this.TbxLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxLastModified.Enabled = false;
      this.TbxLastModified.Location = new System.Drawing.Point(92, 292);
      this.TbxLastModified.Name = "TbxLastModified";
      this.TbxLastModified.Size = new System.Drawing.Size(200, 21);
      this.TbxLastModified.TabIndex = 7;
      // 
      // label89
      // 
      this.label89.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label89.AutoSize = true;
      this.label89.Location = new System.Drawing.Point(9, 295);
      this.label89.Name = "label89";
      this.label89.Size = new System.Drawing.Size(81, 12);
      this.label89.TabIndex = 29;
      this.label89.Text = "Last Modified";
      // 
      // TbxRegistered
      // 
      this.TbxRegistered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.TbxRegistered.Enabled = false;
      this.TbxRegistered.Location = new System.Drawing.Point(92, 265);
      this.TbxRegistered.Name = "TbxRegistered";
      this.TbxRegistered.Size = new System.Drawing.Size(200, 21);
      this.TbxRegistered.TabIndex = 6;
      // 
      // label88
      // 
      this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label88.AutoSize = true;
      this.label88.Location = new System.Drawing.Point(9, 268);
      this.label88.Name = "label88";
      this.label88.Size = new System.Drawing.Size(65, 12);
      this.label88.TabIndex = 27;
      this.label88.Text = "Registered";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 12);
      this.label2.TabIndex = 24;
      this.label2.Text = "Type";
      // 
      // label40
      // 
      this.label40.AutoSize = true;
      this.label40.Location = new System.Drawing.Point(9, 127);
      this.label40.Name = "label40";
      this.label40.Size = new System.Drawing.Size(68, 12);
      this.label40.TabIndex = 23;
      this.label40.Text = "Description";
      // 
      // TbxDescription
      // 
      this.TbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxDescription.Location = new System.Drawing.Point(90, 124);
      this.TbxDescription.Multiline = true;
      this.TbxDescription.Name = "TbxDescription";
      this.TbxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TbxDescription.Size = new System.Drawing.Size(452, 135);
      this.TbxDescription.TabIndex = 5;
      // 
      // RbnUser
      // 
      this.RbnUser.AutoSize = true;
      this.RbnUser.Enabled = false;
      this.RbnUser.Location = new System.Drawing.Point(144, 25);
      this.RbnUser.Name = "RbnUser";
      this.RbnUser.Size = new System.Drawing.Size(49, 16);
      this.RbnUser.TabIndex = 1;
      this.RbnUser.TabStop = true;
      this.RbnUser.Text = "User";
      this.RbnUser.UseVisualStyleBackColor = true;
      // 
      // RbnPart
      // 
      this.RbnPart.AutoSize = true;
      this.RbnPart.Enabled = false;
      this.RbnPart.Location = new System.Drawing.Point(93, 25);
      this.RbnPart.Name = "RbnPart";
      this.RbnPart.Size = new System.Drawing.Size(45, 16);
      this.RbnPart.TabIndex = 0;
      this.RbnPart.TabStop = true;
      this.RbnPart.Text = "Part";
      this.RbnPart.UseVisualStyleBackColor = true;
      // 
      // TbxName
      // 
      this.TbxName.Location = new System.Drawing.Point(93, 48);
      this.TbxName.Name = "TbxName";
      this.TbxName.Size = new System.Drawing.Size(159, 21);
      this.TbxName.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.label1.Location = new System.Drawing.Point(9, 51);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(43, 12);
      this.label1.TabIndex = 2;
      this.label1.Text = "Name";
      // 
      // BtnSave
      // 
      this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSave.Location = new System.Drawing.Point(469, 290);
      this.BtnSave.Name = "BtnSave";
      this.BtnSave.Size = new System.Drawing.Size(75, 23);
      this.BtnSave.TabIndex = 8;
      this.BtnSave.Text = "Save";
      this.BtnSave.UseVisualStyleBackColor = true;
      this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
      // 
      // DeleteMenu
      // 
      this.DeleteMenu.Name = "DeleteMenu";
      this.DeleteMenu.Size = new System.Drawing.Size(123, 22);
      this.DeleteMenu.Text = "Delete";
      this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
      // 
      // AddUserMenu
      // 
      this.AddUserMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddUserMenu.Image")));
      this.AddUserMenu.Name = "AddUserMenu";
      this.AddUserMenu.Size = new System.Drawing.Size(123, 22);
      this.AddUserMenu.Text = "Add User";
      this.AddUserMenu.Click += new System.EventHandler(this.AddUserMenu_Click);
      // 
      // AddPartMenu
      // 
      this.AddPartMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddPartMenu.Image")));
      this.AddPartMenu.Name = "AddPartMenu";
      this.AddPartMenu.Size = new System.Drawing.Size(123, 22);
      this.AddPartMenu.Text = "Add Part";
      this.AddPartMenu.Click += new System.EventHandler(this.AddPartMenu_Click);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPartMenu,
            this.AddUserMenu,
            this.DeleteMenu});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(124, 70);
      // 
      // BtnHelpCallList
      // 
      this.BtnHelpCallList.BackColor = System.Drawing.Color.Transparent;
      this.BtnHelpCallList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnHelpCallList.BackgroundImage")));
      this.BtnHelpCallList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnHelpCallList.FlatAppearance.BorderSize = 0;
      this.BtnHelpCallList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.BtnHelpCallList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.BtnHelpCallList.Location = new System.Drawing.Point(39, 101);
      this.BtnHelpCallList.Name = "BtnHelpCallList";
      this.BtnHelpCallList.Size = new System.Drawing.Size(16, 16);
      this.BtnHelpCallList.TabIndex = 117;
      this.BtnHelpCallList.UseVisualStyleBackColor = false;
      // 
      // FormUser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(812, 353);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "FormUser";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormUser";
      this.Load += new System.EventHandler(this.FormUser_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.PnlRole.ResumeLayout(false);
      this.PnlRole.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView TrvUser;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.CheckBox CkbExpandAll;
    private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
    private System.Windows.Forms.ToolStripMenuItem AddUserMenu;
    private System.Windows.Forms.ToolStripMenuItem AddPartMenu;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.TextBox TbxName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button BtnSave;
    private System.Windows.Forms.RadioButton RbnUser;
    private System.Windows.Forms.RadioButton RbnPart;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label40;
    private System.Windows.Forms.TextBox TbxDescription;
    private System.Windows.Forms.TextBox TbxLastModified;
    private System.Windows.Forms.Label label89;
    private System.Windows.Forms.TextBox TbxRegistered;
    private System.Windows.Forms.Label label88;
    private System.Windows.Forms.Button BtnRefresh;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.TextBox TbxPassword;
    private System.Windows.Forms.Label LblPassword;
    private System.Windows.Forms.RadioButton RbnDesigner;
    private System.Windows.Forms.Label LblRole;
    private System.Windows.Forms.RadioButton RbnDeveloper;
    private System.Windows.Forms.RadioButton RbnModeler;
    private System.Windows.Forms.RadioButton RbnQA;
    private System.Windows.Forms.RadioButton RbnPL;
    private System.Windows.Forms.Panel PnlRole;
    private System.Windows.Forms.CheckBox CkbViewer;
    private System.Windows.Forms.RadioButton RbnPM;
    private System.Windows.Forms.Label LblUserNameSample;
    private System.Windows.Forms.Button BtnHelpCallList;
  }
}