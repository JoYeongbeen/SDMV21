namespace SDM
{
  partial class FormMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.TrvModel = new System.Windows.Forms.TreeView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.AddMicroserviceMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddInternalSystemMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddExternalSystemMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddBizPackageMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddControllerMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddAPIMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddProducerMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddPublisherMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddConsumerMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddSubscriberMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddOtherMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddBizRuleMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddDataAccessMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddOperationMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddDtoMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddEntityMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddUIMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddJobMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.AddStepMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.UploadMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.CutMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.PasteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.GenerateSpecMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.GenerateCodeMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.CkbShowID = new System.Windows.Forms.CheckBox();
      this.label81 = new System.Windows.Forms.Label();
      this.CmbSortBy = new System.Windows.Forms.ComboBox();
      this.CkbShowEng = new System.Windows.Forms.CheckBox();
      this.CmbExpand = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.BtnSynchTree = new System.Windows.Forms.Button();
      this.BtnTreeOption = new System.Windows.Forms.Button();
      this.TbxSearchNodeText = new System.Windows.Forms.TextBox();
      this.BtnFindNode = new System.Windows.Forms.Button();
      this.BtnRefresh = new System.Windows.Forms.Button();
      this.label48 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.BtnDictionary = new System.Windows.Forms.Button();
      this.BtnUser = new System.Windows.Forms.Button();
      this.BtnLogout = new System.Windows.Forms.Button();
      this.BtnProjectOption = new System.Windows.Forms.Button();
      this.BtnMyOption = new System.Windows.Forms.Button();
      this.LblUser = new System.Windows.Forms.Label();
      this.CkbDefaultSize = new System.Windows.Forms.CheckBox();
      this.BtnHelp = new System.Windows.Forms.Button();
      this.BtnDashboard = new System.Windows.Forms.Button();
      this.BtnGoBack = new System.Windows.Forms.Button();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TrvModel
      // 
      this.TrvModel.AllowDrop = true;
      this.TrvModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TrvModel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.TrvModel.ImageIndex = 0;
      this.TrvModel.ImageList = this.imageList1;
      this.TrvModel.Location = new System.Drawing.Point(7, 63);
      this.TrvModel.Name = "TrvModel";
      this.TrvModel.PathSeparator = " > ";
      this.TrvModel.SelectedImageIndex = 0;
      this.TrvModel.Size = new System.Drawing.Size(288, 433);
      this.TrvModel.TabIndex = 0;
      this.TrvModel.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvModel_BeforeCollapse);
      this.TrvModel.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvModel_BeforeExpand);
      this.TrvModel.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TrvModel_ItemDrag);
      this.TrvModel.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvModel_NodeMouseClick);
      this.TrvModel.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvModel_NodeMouseDoubleClick);
      this.TrvModel.DragDrop += new System.Windows.Forms.DragEventHandler(this.TrvModel_DragDrop);
      this.TrvModel.DragEnter += new System.Windows.Forms.DragEventHandler(this.TrvModel_DragEnter);
      this.TrvModel.DragOver += new System.Windows.Forms.DragEventHandler(this.TrvModel_DragOver);
      this.TrvModel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrvModel_MouseDown);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "SDM");
      this.imageList1.Images.SetKeyName(1, "MS");
      this.imageList1.Images.SetKeyName(2, "SYS");
      this.imageList1.Images.SetKeyName(3, "BP");
      this.imageList1.Images.SetKeyName(4, "CTR");
      this.imageList1.Images.SetKeyName(5, "API");
      this.imageList1.Images.SetKeyName(6, "PRD");
      this.imageList1.Images.SetKeyName(7, "PUB");
      this.imageList1.Images.SetKeyName(8, "CSM");
      this.imageList1.Images.SetKeyName(9, "SUB");
      this.imageList1.Images.SetKeyName(10, "OTHER");
      this.imageList1.Images.SetKeyName(11, "BR");
      this.imageList1.Images.SetKeyName(12, "DA");
      this.imageList1.Images.SetKeyName(13, "OP");
      this.imageList1.Images.SetKeyName(14, "DTO");
      this.imageList1.Images.SetKeyName(15, "ENT");
      this.imageList1.Images.SetKeyName(16, "UI");
      this.imageList1.Images.SetKeyName(17, "JOB");
      this.imageList1.Images.SetKeyName(18, "STEP");
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMicroserviceMenu,
            this.AddInternalSystemMenu,
            this.AddExternalSystemMenu,
            this.AddBizPackageMenu,
            this.AddControllerMenu,
            this.AddAPIMenu,
            this.AddProducerMenu,
            this.AddPublisherMenu,
            this.AddConsumerMenu,
            this.AddSubscriberMenu,
            this.AddOtherMenu,
            this.AddBizRuleMenu,
            this.AddDataAccessMenu,
            this.AddOperationMenu,
            this.AddDtoMenu,
            this.AddEntityMenu,
            this.AddUIMenu,
            this.AddJobMenu,
            this.AddStepMenu,
            this.UploadMenu,
            this.CutMenu,
            this.CopyMenu,
            this.PasteMenu,
            this.DeleteMenu,
            this.GenerateSpecMenu,
            this.GenerateCodeMenu});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(244, 576);
      // 
      // AddMicroserviceMenu
      // 
      this.AddMicroserviceMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddMicroserviceMenu.Image")));
      this.AddMicroserviceMenu.Name = "AddMicroserviceMenu";
      this.AddMicroserviceMenu.Size = new System.Drawing.Size(243, 22);
      this.AddMicroserviceMenu.Text = "Microservice 추가";
      this.AddMicroserviceMenu.Click += new System.EventHandler(this.AddMicroserviceMenu_Click);
      // 
      // AddInternalSystemMenu
      // 
      this.AddInternalSystemMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddInternalSystemMenu.Image")));
      this.AddInternalSystemMenu.Name = "AddInternalSystemMenu";
      this.AddInternalSystemMenu.Size = new System.Drawing.Size(243, 22);
      this.AddInternalSystemMenu.Text = "Internal System 추가";
      this.AddInternalSystemMenu.Click += new System.EventHandler(this.AddInternalSystemMenu_Click);
      // 
      // AddExternalSystemMenu
      // 
      this.AddExternalSystemMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddExternalSystemMenu.Image")));
      this.AddExternalSystemMenu.Name = "AddExternalSystemMenu";
      this.AddExternalSystemMenu.Size = new System.Drawing.Size(243, 22);
      this.AddExternalSystemMenu.Text = "External System 추가";
      this.AddExternalSystemMenu.Click += new System.EventHandler(this.AddExternalSystemMenu_Click);
      // 
      // AddBizPackageMenu
      // 
      this.AddBizPackageMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddBizPackageMenu.Image")));
      this.AddBizPackageMenu.Name = "AddBizPackageMenu";
      this.AddBizPackageMenu.Size = new System.Drawing.Size(243, 22);
      this.AddBizPackageMenu.Text = "BizPackage 추가";
      this.AddBizPackageMenu.Click += new System.EventHandler(this.AddBizPackageMenu_Click);
      // 
      // AddControllerMenu
      // 
      this.AddControllerMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddControllerMenu.Image")));
      this.AddControllerMenu.Name = "AddControllerMenu";
      this.AddControllerMenu.Size = new System.Drawing.Size(243, 22);
      this.AddControllerMenu.Text = "Controller 추가";
      this.AddControllerMenu.Click += new System.EventHandler(this.AddControllerMenu_Click);
      // 
      // AddAPIMenu
      // 
      this.AddAPIMenu.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.AddAPIMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddAPIMenu.Image")));
      this.AddAPIMenu.Name = "AddAPIMenu";
      this.AddAPIMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
      this.AddAPIMenu.Size = new System.Drawing.Size(243, 22);
      this.AddAPIMenu.Text = "API 추가";
      this.AddAPIMenu.Click += new System.EventHandler(this.AddAPIMenu_Click);
      // 
      // AddProducerMenu
      // 
      this.AddProducerMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddProducerMenu.Image")));
      this.AddProducerMenu.Name = "AddProducerMenu";
      this.AddProducerMenu.Size = new System.Drawing.Size(243, 22);
      this.AddProducerMenu.Text = "Producer 추가";
      this.AddProducerMenu.Click += new System.EventHandler(this.AddProducerMenu_Click);
      // 
      // AddPublisherMenu
      // 
      this.AddPublisherMenu.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.AddPublisherMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddPublisherMenu.Image")));
      this.AddPublisherMenu.Name = "AddPublisherMenu";
      this.AddPublisherMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
      this.AddPublisherMenu.Size = new System.Drawing.Size(243, 22);
      this.AddPublisherMenu.Text = "Publisher 추가";
      this.AddPublisherMenu.Click += new System.EventHandler(this.AddPublisherMenu_Click);
      // 
      // AddConsumerMenu
      // 
      this.AddConsumerMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddConsumerMenu.Image")));
      this.AddConsumerMenu.Name = "AddConsumerMenu";
      this.AddConsumerMenu.Size = new System.Drawing.Size(243, 22);
      this.AddConsumerMenu.Text = "Consumer 추가";
      this.AddConsumerMenu.Click += new System.EventHandler(this.AddConsumerMenu_Click);
      // 
      // AddSubscriberMenu
      // 
      this.AddSubscriberMenu.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.AddSubscriberMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddSubscriberMenu.Image")));
      this.AddSubscriberMenu.Name = "AddSubscriberMenu";
      this.AddSubscriberMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
      this.AddSubscriberMenu.Size = new System.Drawing.Size(243, 22);
      this.AddSubscriberMenu.Text = "Subscriber 추가";
      this.AddSubscriberMenu.Click += new System.EventHandler(this.AddSubscriberMenu_Click);
      // 
      // AddOtherMenu
      // 
      this.AddOtherMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddOtherMenu.Image")));
      this.AddOtherMenu.Name = "AddOtherMenu";
      this.AddOtherMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
      this.AddOtherMenu.Size = new System.Drawing.Size(243, 22);
      this.AddOtherMenu.Text = "Other 추가";
      this.AddOtherMenu.Click += new System.EventHandler(this.AddOtherMenu_Click);
      // 
      // AddBizRuleMenu
      // 
      this.AddBizRuleMenu.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.AddBizRuleMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddBizRuleMenu.Image")));
      this.AddBizRuleMenu.Name = "AddBizRuleMenu";
      this.AddBizRuleMenu.Size = new System.Drawing.Size(243, 22);
      this.AddBizRuleMenu.Text = "BizRule 추가";
      this.AddBizRuleMenu.Click += new System.EventHandler(this.AddBizRuleMenu_Click);
      // 
      // AddDataAccessMenu
      // 
      this.AddDataAccessMenu.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.AddDataAccessMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddDataAccessMenu.Image")));
      this.AddDataAccessMenu.Name = "AddDataAccessMenu";
      this.AddDataAccessMenu.Size = new System.Drawing.Size(243, 22);
      this.AddDataAccessMenu.Text = "DataAccess 추가";
      this.AddDataAccessMenu.Click += new System.EventHandler(this.AddDataAccessMenu_Click);
      // 
      // AddOperationMenu
      // 
      this.AddOperationMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddOperationMenu.Image")));
      this.AddOperationMenu.Name = "AddOperationMenu";
      this.AddOperationMenu.Size = new System.Drawing.Size(243, 22);
      this.AddOperationMenu.Text = "Operation 추가";
      this.AddOperationMenu.Click += new System.EventHandler(this.AddOperationMenu_Click);
      // 
      // AddDtoMenu
      // 
      this.AddDtoMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddDtoMenu.Image")));
      this.AddDtoMenu.Name = "AddDtoMenu";
      this.AddDtoMenu.Size = new System.Drawing.Size(243, 22);
      this.AddDtoMenu.Text = "Dto 추가";
      this.AddDtoMenu.Click += new System.EventHandler(this.AddDtoMenu_Click);
      // 
      // AddEntityMenu
      // 
      this.AddEntityMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddEntityMenu.Image")));
      this.AddEntityMenu.Name = "AddEntityMenu";
      this.AddEntityMenu.Size = new System.Drawing.Size(243, 22);
      this.AddEntityMenu.Text = "Entity 추가";
      this.AddEntityMenu.Click += new System.EventHandler(this.AddEntityMenu_Click);
      // 
      // AddUIMenu
      // 
      this.AddUIMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddUIMenu.Image")));
      this.AddUIMenu.Name = "AddUIMenu";
      this.AddUIMenu.Size = new System.Drawing.Size(243, 22);
      this.AddUIMenu.Text = "UI 추가";
      this.AddUIMenu.Click += new System.EventHandler(this.AddUIMenu_Click);
      // 
      // AddJobMenu
      // 
      this.AddJobMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddJobMenu.Image")));
      this.AddJobMenu.Name = "AddJobMenu";
      this.AddJobMenu.Size = new System.Drawing.Size(243, 22);
      this.AddJobMenu.Text = "Batch(Job) 추가";
      this.AddJobMenu.Click += new System.EventHandler(this.AddJobMenu_Click);
      // 
      // AddStepMenu
      // 
      this.AddStepMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddStepMenu.Image")));
      this.AddStepMenu.Name = "AddStepMenu";
      this.AddStepMenu.Size = new System.Drawing.Size(243, 22);
      this.AddStepMenu.Text = "Step 추가";
      this.AddStepMenu.Click += new System.EventHandler(this.AddStepMenu_Click);
      // 
      // UploadMenu
      // 
      this.UploadMenu.Image = ((System.Drawing.Image)(resources.GetObject("UploadMenu.Image")));
      this.UploadMenu.Name = "UploadMenu";
      this.UploadMenu.Size = new System.Drawing.Size(243, 22);
      this.UploadMenu.Text = "업로드";
      this.UploadMenu.Click += new System.EventHandler(this.UploadMenu_Click);
      // 
      // CutMenu
      // 
      this.CutMenu.Image = ((System.Drawing.Image)(resources.GetObject("CutMenu.Image")));
      this.CutMenu.Name = "CutMenu";
      this.CutMenu.Size = new System.Drawing.Size(243, 22);
      this.CutMenu.Text = "잘라내기";
      this.CutMenu.Visible = false;
      // 
      // CopyMenu
      // 
      this.CopyMenu.Image = ((System.Drawing.Image)(resources.GetObject("CopyMenu.Image")));
      this.CopyMenu.Name = "CopyMenu";
      this.CopyMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.CopyMenu.Size = new System.Drawing.Size(243, 22);
      this.CopyMenu.Text = "복사";
      this.CopyMenu.Click += new System.EventHandler(this.CopyMenu_Click);
      // 
      // PasteMenu
      // 
      this.PasteMenu.Image = ((System.Drawing.Image)(resources.GetObject("PasteMenu.Image")));
      this.PasteMenu.Name = "PasteMenu";
      this.PasteMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.PasteMenu.Size = new System.Drawing.Size(243, 22);
      this.PasteMenu.Text = "붙여넣기";
      this.PasteMenu.Click += new System.EventHandler(this.PasteMenu_Click);
      // 
      // DeleteMenu
      // 
      this.DeleteMenu.Image = ((System.Drawing.Image)(resources.GetObject("DeleteMenu.Image")));
      this.DeleteMenu.Name = "DeleteMenu";
      this.DeleteMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.DeleteMenu.Size = new System.Drawing.Size(243, 22);
      this.DeleteMenu.Text = "삭제";
      this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
      // 
      // GenerateSpecMenu
      // 
      this.GenerateSpecMenu.Image = ((System.Drawing.Image)(resources.GetObject("GenerateSpecMenu.Image")));
      this.GenerateSpecMenu.Name = "GenerateSpecMenu";
      this.GenerateSpecMenu.Size = new System.Drawing.Size(243, 22);
      this.GenerateSpecMenu.Text = "설계서 생성";
      this.GenerateSpecMenu.Click += new System.EventHandler(this.GenerateSpecMenu_Click);
      // 
      // GenerateCodeMenu
      // 
      this.GenerateCodeMenu.Image = ((System.Drawing.Image)(resources.GetObject("GenerateCodeMenu.Image")));
      this.GenerateCodeMenu.Name = "GenerateCodeMenu";
      this.GenerateCodeMenu.Size = new System.Drawing.Size(243, 22);
      this.GenerateCodeMenu.Text = "코드 생성";
      this.GenerateCodeMenu.Click += new System.EventHandler(this.GenerateCodeMenu_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer1.Location = new System.Drawing.Point(12, 56);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.CkbShowID);
      this.splitContainer1.Panel1.Controls.Add(this.label81);
      this.splitContainer1.Panel1.Controls.Add(this.CmbSortBy);
      this.splitContainer1.Panel1.Controls.Add(this.CkbShowEng);
      this.splitContainer1.Panel1.Controls.Add(this.CmbExpand);
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      this.splitContainer1.Panel1.Controls.Add(this.BtnSynchTree);
      this.splitContainer1.Panel1.Controls.Add(this.BtnTreeOption);
      this.splitContainer1.Panel1.Controls.Add(this.TrvModel);
      this.splitContainer1.Panel1.Controls.Add(this.TbxSearchNodeText);
      this.splitContainer1.Panel1.Controls.Add(this.BtnFindNode);
      this.splitContainer1.Panel1.Controls.Add(this.BtnRefresh);
      this.splitContainer1.Panel1.Controls.Add(this.label48);
      this.splitContainer1.Size = new System.Drawing.Size(887, 503);
      this.splitContainer1.SplitterDistance = 302;
      this.splitContainer1.TabIndex = 12;
      // 
      // CkbShowID
      // 
      this.CkbShowID.AutoSize = true;
      this.CkbShowID.Location = new System.Drawing.Point(53, 44);
      this.CkbShowID.Name = "CkbShowID";
      this.CkbShowID.Size = new System.Drawing.Size(35, 16);
      this.CkbShowID.TabIndex = 1;
      this.CkbShowID.Text = "ID";
      this.CkbShowID.UseVisualStyleBackColor = true;
      this.CkbShowID.CheckedChanged += new System.EventHandler(this.CkbShowID_CheckedChanged);
      // 
      // label81
      // 
      this.label81.AutoSize = true;
      this.label81.Location = new System.Drawing.Point(3, 46);
      this.label81.Name = "label81";
      this.label81.Size = new System.Drawing.Size(37, 12);
      this.label81.TabIndex = 27;
      this.label81.Text = "Show";
      // 
      // CmbSortBy
      // 
      this.CmbSortBy.DisplayMember = "Name";
      this.CmbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbSortBy.FormattingEnabled = true;
      this.CmbSortBy.ItemHeight = 12;
      this.CmbSortBy.Items.AddRange(new object[] {
            "ID",
            "Name"});
      this.CmbSortBy.Location = new System.Drawing.Point(53, 23);
      this.CmbSortBy.Name = "CmbSortBy";
      this.CmbSortBy.Size = new System.Drawing.Size(120, 20);
      this.CmbSortBy.Sorted = true;
      this.CmbSortBy.TabIndex = 33;
      this.CmbSortBy.SelectedIndexChanged += new System.EventHandler(this.CmbSortBy_SelectedIndexChanged);
      // 
      // CkbShowEng
      // 
      this.CkbShowEng.AutoSize = true;
      this.CkbShowEng.Location = new System.Drawing.Point(90, 44);
      this.CkbShowEng.Name = "CkbShowEng";
      this.CkbShowEng.Size = new System.Drawing.Size(46, 16);
      this.CkbShowEng.TabIndex = 28;
      this.CkbShowEng.Text = "Eng";
      this.CkbShowEng.UseVisualStyleBackColor = true;
      this.CkbShowEng.CheckedChanged += new System.EventHandler(this.CkbShowEng_CheckedChanged);
      // 
      // CmbExpand
      // 
      this.CmbExpand.DisplayMember = "Name";
      this.CmbExpand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CmbExpand.FormattingEnabled = true;
      this.CmbExpand.ItemHeight = 12;
      this.CmbExpand.Items.AddRange(new object[] {
            "All",
            "BizPackage",
            "Microservice"});
      this.CmbExpand.Location = new System.Drawing.Point(53, 2);
      this.CmbExpand.Name = "CmbExpand";
      this.CmbExpand.Size = new System.Drawing.Size(120, 20);
      this.CmbExpand.Sorted = true;
      this.CmbExpand.TabIndex = 32;
      this.CmbExpand.SelectedIndexChanged += new System.EventHandler(this.CmbExpand_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 12);
      this.label1.TabIndex = 31;
      this.label1.Text = "Expand";
      // 
      // BtnSynchTree
      // 
      this.BtnSynchTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnSynchTree.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSynchTree.BackgroundImage")));
      this.BtnSynchTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnSynchTree.Location = new System.Drawing.Point(273, 37);
      this.BtnSynchTree.Name = "BtnSynchTree";
      this.BtnSynchTree.Size = new System.Drawing.Size(23, 23);
      this.BtnSynchTree.TabIndex = 30;
      this.BtnSynchTree.UseVisualStyleBackColor = true;
      this.BtnSynchTree.Click += new System.EventHandler(this.BtnSynchTree_Click);
      // 
      // BtnTreeOption
      // 
      this.BtnTreeOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnTreeOption.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnTreeOption.BackgroundImage")));
      this.BtnTreeOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnTreeOption.Location = new System.Drawing.Point(250, 37);
      this.BtnTreeOption.Name = "BtnTreeOption";
      this.BtnTreeOption.Size = new System.Drawing.Size(23, 23);
      this.BtnTreeOption.TabIndex = 29;
      this.BtnTreeOption.UseVisualStyleBackColor = true;
      this.BtnTreeOption.Click += new System.EventHandler(this.BtnTreeOption_Click);
      // 
      // TbxSearchNodeText
      // 
      this.TbxSearchNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TbxSearchNodeText.Location = new System.Drawing.Point(179, 2);
      this.TbxSearchNodeText.Name = "TbxSearchNodeText";
      this.TbxSearchNodeText.Size = new System.Drawing.Size(92, 21);
      this.TbxSearchNodeText.TabIndex = 4;
      this.TbxSearchNodeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxSearchNodeText_KeyDown);
      // 
      // BtnFindNode
      // 
      this.BtnFindNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnFindNode.Image = ((System.Drawing.Image)(resources.GetObject("BtnFindNode.Image")));
      this.BtnFindNode.Location = new System.Drawing.Point(273, 1);
      this.BtnFindNode.Name = "BtnFindNode";
      this.BtnFindNode.Size = new System.Drawing.Size(23, 23);
      this.BtnFindNode.TabIndex = 5;
      this.BtnFindNode.UseVisualStyleBackColor = true;
      this.BtnFindNode.Click += new System.EventHandler(this.BtnFindNode_Click);
      // 
      // BtnRefresh
      // 
      this.BtnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("BtnRefresh.Image")));
      this.BtnRefresh.Location = new System.Drawing.Point(226, 37);
      this.BtnRefresh.Name = "BtnRefresh";
      this.BtnRefresh.Size = new System.Drawing.Size(23, 23);
      this.BtnRefresh.TabIndex = 6;
      this.BtnRefresh.UseVisualStyleBackColor = true;
      this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
      // 
      // label48
      // 
      this.label48.AutoSize = true;
      this.label48.Location = new System.Drawing.Point(3, 26);
      this.label48.Name = "label48";
      this.label48.Size = new System.Drawing.Size(45, 12);
      this.label48.TabIndex = 26;
      this.label48.Text = "Sort by";
      // 
      // BtnDictionary
      // 
      this.BtnDictionary.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDictionary.BackgroundImage")));
      this.BtnDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnDictionary.ImageKey = "(없음)";
      this.BtnDictionary.Location = new System.Drawing.Point(108, 5);
      this.BtnDictionary.Name = "BtnDictionary";
      this.BtnDictionary.Size = new System.Drawing.Size(42, 45);
      this.BtnDictionary.TabIndex = 46;
      this.toolTip1.SetToolTip(this.BtnDictionary, "\r\n");
      this.BtnDictionary.UseVisualStyleBackColor = true;
      this.BtnDictionary.Visible = false;
      this.BtnDictionary.Click += new System.EventHandler(this.BtnDictionary_Click);
      // 
      // BtnUser
      // 
      this.BtnUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnUser.BackgroundImage")));
      this.BtnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnUser.ImageKey = "(없음)";
      this.BtnUser.Location = new System.Drawing.Point(761, 5);
      this.BtnUser.Name = "BtnUser";
      this.BtnUser.Size = new System.Drawing.Size(42, 45);
      this.BtnUser.TabIndex = 40;
      this.BtnUser.UseVisualStyleBackColor = true;
      this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
      // 
      // BtnLogout
      // 
      this.BtnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLogout.BackgroundImage")));
      this.BtnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnLogout.ImageKey = "(없음)";
      this.BtnLogout.Location = new System.Drawing.Point(809, 5);
      this.BtnLogout.Name = "BtnLogout";
      this.BtnLogout.Size = new System.Drawing.Size(42, 45);
      this.BtnLogout.TabIndex = 41;
      this.BtnLogout.UseVisualStyleBackColor = true;
      this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
      // 
      // BtnProjectOption
      // 
      this.BtnProjectOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnProjectOption.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnProjectOption.BackgroundImage")));
      this.BtnProjectOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnProjectOption.ImageKey = "(없음)";
      this.BtnProjectOption.Location = new System.Drawing.Point(713, 5);
      this.BtnProjectOption.Name = "BtnProjectOption";
      this.BtnProjectOption.Size = new System.Drawing.Size(42, 45);
      this.BtnProjectOption.TabIndex = 42;
      this.BtnProjectOption.UseVisualStyleBackColor = true;
      this.BtnProjectOption.Click += new System.EventHandler(this.BtnProjectOption_Click);
      // 
      // BtnMyOption
      // 
      this.BtnMyOption.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMyOption.BackgroundImage")));
      this.BtnMyOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnMyOption.ImageKey = "(없음)";
      this.BtnMyOption.Location = new System.Drawing.Point(156, 5);
      this.BtnMyOption.Name = "BtnMyOption";
      this.BtnMyOption.Size = new System.Drawing.Size(42, 45);
      this.BtnMyOption.TabIndex = 43;
      this.BtnMyOption.UseVisualStyleBackColor = true;
      this.BtnMyOption.Click += new System.EventHandler(this.BtnMyOption_Click);
      // 
      // LblUser
      // 
      this.LblUser.AutoSize = true;
      this.LblUser.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.LblUser.Location = new System.Drawing.Point(204, 14);
      this.LblUser.Name = "LblUser";
      this.LblUser.Size = new System.Drawing.Size(103, 12);
      this.LblUser.TabIndex = 47;
      this.LblUser.Text = "[Part]Designer";
      // 
      // CkbDefaultSize
      // 
      this.CkbDefaultSize.AutoSize = true;
      this.CkbDefaultSize.Location = new System.Drawing.Point(206, 29);
      this.CkbDefaultSize.Name = "CkbDefaultSize";
      this.CkbDefaultSize.Size = new System.Drawing.Size(88, 16);
      this.CkbDefaultSize.TabIndex = 45;
      this.CkbDefaultSize.Text = "기본 사이즈";
      this.CkbDefaultSize.UseVisualStyleBackColor = true;
      this.CkbDefaultSize.CheckedChanged += new System.EventHandler(this.CkbDefaultSize_CheckedChanged);
      // 
      // BtnHelp
      // 
      this.BtnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BtnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnHelp.BackgroundImage")));
      this.BtnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BtnHelp.ImageKey = "(없음)";
      this.BtnHelp.Location = new System.Drawing.Point(857, 5);
      this.BtnHelp.Name = "BtnHelp";
      this.BtnHelp.Size = new System.Drawing.Size(42, 45);
      this.BtnHelp.TabIndex = 44;
      this.BtnHelp.UseVisualStyleBackColor = true;
      this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
      // 
      // BtnDashboard
      // 
      this.BtnDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDashboard.BackgroundImage")));
      this.BtnDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnDashboard.ImageKey = "(없음)";
      this.BtnDashboard.Location = new System.Drawing.Point(60, 5);
      this.BtnDashboard.Name = "BtnDashboard";
      this.BtnDashboard.Size = new System.Drawing.Size(42, 45);
      this.BtnDashboard.TabIndex = 51;
      this.BtnDashboard.UseVisualStyleBackColor = true;
      this.BtnDashboard.Click += new System.EventHandler(this.BtnDashboard_Click);
      // 
      // BtnGoBack
      // 
      this.BtnGoBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnGoBack.BackgroundImage")));
      this.BtnGoBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.BtnGoBack.ImageKey = "(없음)";
      this.BtnGoBack.Location = new System.Drawing.Point(12, 5);
      this.BtnGoBack.Name = "BtnGoBack";
      this.BtnGoBack.Size = new System.Drawing.Size(42, 45);
      this.BtnGoBack.TabIndex = 52;
      this.BtnGoBack.UseVisualStyleBackColor = true;
      this.BtnGoBack.Click += new System.EventHandler(this.BtnGoBack_Click);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(911, 571);
      this.Controls.Add(this.BtnGoBack);
      this.Controls.Add(this.BtnDashboard);
      this.Controls.Add(this.BtnProjectOption);
      this.Controls.Add(this.BtnUser);
      this.Controls.Add(this.LblUser);
      this.Controls.Add(this.BtnDictionary);
      this.Controls.Add(this.CkbDefaultSize);
      this.Controls.Add(this.BtnHelp);
      this.Controls.Add(this.BtnMyOption);
      this.Controls.Add(this.BtnLogout);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FormMain";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.contextMenuStrip1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView TrvModel;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem AddMicroserviceMenu;
    private System.Windows.Forms.ToolStripMenuItem AddBizPackageMenu;
    private System.Windows.Forms.ToolStripMenuItem AddAPIMenu;
    private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox TbxSearchNodeText;
    private System.Windows.Forms.CheckBox CkbShowEng;
    private System.Windows.Forms.Label label81;
    private System.Windows.Forms.Button BtnFindNode;
    private System.Windows.Forms.Button BtnRefresh;
    private System.Windows.Forms.Label label48;
    private System.Windows.Forms.CheckBox CkbShowID;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button BtnUser;
    private System.Windows.Forms.Button BtnLogout;
    private System.Windows.Forms.Button BtnProjectOption;
    private System.Windows.Forms.Button BtnMyOption;
    private System.Windows.Forms.Label LblUser;
    private System.Windows.Forms.Button BtnDictionary;
    private System.Windows.Forms.CheckBox CkbDefaultSize;
    private System.Windows.Forms.Button BtnHelp;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Button BtnDashboard;
    private System.Windows.Forms.ToolStripMenuItem AddBizRuleMenu;
    private System.Windows.Forms.Button BtnTreeOption;
    private System.Windows.Forms.ToolStripMenuItem AddDataAccessMenu;
    private System.Windows.Forms.ToolStripMenuItem CopyMenu;
    private System.Windows.Forms.ToolStripMenuItem CutMenu;
    private System.Windows.Forms.ToolStripMenuItem PasteMenu;
    private System.Windows.Forms.ToolStripMenuItem AddOperationMenu;
    private System.Windows.Forms.ToolStripMenuItem AddControllerMenu;
    private System.Windows.Forms.ToolStripMenuItem AddJobMenu;
    private System.Windows.Forms.ToolStripMenuItem AddStepMenu;
    private System.Windows.Forms.ToolStripMenuItem AddPublisherMenu;
    private System.Windows.Forms.ToolStripMenuItem AddSubscriberMenu;
    private System.Windows.Forms.ToolStripMenuItem AddProducerMenu;
    private System.Windows.Forms.ToolStripMenuItem AddConsumerMenu;
    private System.Windows.Forms.ToolStripMenuItem AddDtoMenu;
    private System.Windows.Forms.ToolStripMenuItem AddEntityMenu;
    private System.Windows.Forms.ToolStripMenuItem AddUIMenu;
    private System.Windows.Forms.ToolStripMenuItem AddInternalSystemMenu;
    private System.Windows.Forms.ToolStripMenuItem AddExternalSystemMenu;
    private System.Windows.Forms.ToolStripMenuItem AddOtherMenu;
    private System.Windows.Forms.Button BtnGoBack;
    private System.Windows.Forms.Button BtnSynchTree;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox CmbExpand;
    private System.Windows.Forms.ComboBox CmbSortBy;
    private System.Windows.Forms.ToolStripMenuItem UploadMenu;
    private System.Windows.Forms.ToolStripMenuItem GenerateSpecMenu;
    private System.Windows.Forms.ToolStripMenuItem GenerateCodeMenu;
  }
}