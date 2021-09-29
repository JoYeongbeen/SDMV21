using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using SDM.Component;
using SDM.Common;
using SDM.Project;

namespace SDM
{
  public partial class FormMain : Form
  {
    private bool IsTreeDoubleClick = false;
    private bool SortByName = true;

    private SBR SBR;
    private SDA SDA;
    private SSpec SSpec;
    private SCode SCode;

    private TreeNode SelectedNode;
    private TreeNode CopiedNode;
    private Stack<string> SelectedNodeGuidStack = new Stack<string>();

    private List<TreeNode> SearchedNodeList = new List<TreeNode>();
    private int LastSearchedNodeIndex = 0;

    private TreeNode NodeMicroservices = new TreeNode(SMessage.SDM_TYPE_MS);
    private TreeNode NodeInternalSystems = new TreeNode(SMessage.SDM_TYPE_SI);
    private TreeNode NodeExternalSystems = new TreeNode(SMessage.SDM_TYPE_SE);

    #region Form Load/Closing

    public FormMain()
    {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      //----------------------------------------
      //DBFile : txt는 DRM으로 암호화 되므로 .sdm 확장명으로 변경
      SCommon.DBFilePath = this.GetDBFilePath();

      if(File.Exists(SCommon.DBFilePath) == false)
      {
        SMessageBox.ShowWarning(SMessage.NO_EXIST_DB_FILE, SCommon.DBFilePath);
        this.Close();
      }

      //----------------------------------------
      this.Text = SCommon.ProductVersion + " - " + SCommon.DBFilePath;

      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SSpec = new SSpec();
      this.SCode = new SCode();

      SCommon.SProject = this.SDA.SelectProject();
      SCommon.SPartList = this.SDA.SelectPartList();
      SCommon.SUserList = this.SBR.SelectUserListAll();
      SCommon.SMicroserviceList = this.SDA.SelectMicroserviceList();

      this.CheckNewVersion();
      this.ShowFormLogin();
    }

    private string GetDBFilePath()
    {
      string dbFilePath = string.Empty;
      string text = File.ReadAllText(SCommon.DBFileName);

      string[] inputList = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

      foreach (string input in inputList)
      {
        if (input.StartsWith("#") == false)
          dbFilePath = input;
      }

      return dbFilePath;
    }

    private void CheckNewVersion()
    {
      try
      {
        if (Directory.Exists(SCommon.SProject.DeployFolder) == false)
        {
          SMessageBox.ShowWarning(SMessage.SDM_DEPLOYMENT_FOLDER_NOT_FOUND);
          return;
        }

        DirectoryInfo dir = new DirectoryInfo(SCommon.SProject.DeployFolder);
        DirectoryInfo[] childDirList = dir.GetDirectories();

        List<DirectoryInfo> tempList = new List<DirectoryInfo>();
        tempList.AddRange(childDirList);
        tempList.Reverse();

        foreach (DirectoryInfo childDir in tempList)
        {
          if (DateTime.Compare(Convert.ToDateTime(childDir.Name), Convert.ToDateTime(SCommon.ProductYyyyMmDd)) > 0)
          {
            string updateNotice = string.Empty;
            string updateFileName = SCommon.SProject.DeployFolder + @"\" + childDir.Name + @"\" + "update.sdm";
            
            if(File.Exists(updateFileName))
              updateNotice = File.ReadAllText(updateFileName) + Environment.NewLine + Environment.NewLine;

            updateNotice = updateNotice + SMessage.SDM_UPDATE_NOTICE;

            DialogResult result = SMessageBox.ShowConfirm(updateNotice);

            if (result == DialogResult.Yes)
            {
              System.Diagnostics.Process.Start(SCommon.SProject.DeployFolder);
              this.Close();
            }

            return;
          }
        }
      }
      catch (FormatException ex)
      {
        SMessageBox.ShowWarning(SMessage.SDM_DEPLOYMENT_FOLDER_NAMING + Environment.NewLine + ex.Message);
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (this.TrvModel.Focused)
      {
        #region Control + Shift + X 단축키 정리
        //A : API 
        //B : (BizPackage)
        //C : (generate Code )
        //D : (Dto )
        //E : (Entity)
        //F 
        //G 
        //H : (batcH)
        //I 
        //J 
        //K 
        //L 
        //M : (Microservice)
        //N : (opreatioN)
        //O : Other  
        //P : Pub
        //Q 
        //R : (biz Rule)
        //S : Sub
        //T : (generate spec documenT)
        //U : (UI)
        //V 
        //W 
        //X 
        //Y 
        //Z
        #endregion

        if (keyData == (Keys.Control | Keys.Shift | Keys.A))
          this.AddAPI();
        else if (keyData == (Keys.Control | Keys.Shift | Keys.P))
          this.AddPublisher();
        else if (keyData == (Keys.Control | Keys.Shift | Keys.S))
          this.AddSubscriber();
        else if (keyData == (Keys.Control | Keys.Shift | Keys.O))
          this.AddOther();
        else if (keyData == Keys.Delete)
          this.DeleteNode();
        else if (keyData == Keys.Enter)
          this.ViewComponentDetail(this.TrvModel.SelectedNode, false, false);
        else if (keyData == Keys.F2)
          this.ViewComponentDetail(this.TrvModel.SelectedNode, true, false);
        else if (keyData == (Keys.Control | Keys.C))
          this.CopyNode();
        else if (keyData == (Keys.Control | Keys.V))
          this.PasteNode();
      }

      if (keyData == (Keys.Control | Keys.F))
        SCommon.SelectAllAndFocus(this.TbxSearchNodeText);
      else if (keyData == Keys.F3)
        this.SearchNodeListByF3();
      else if (keyData == (Keys.Control | Keys.R))
        this.RefreshTreeView();

      else if (keyData == (Keys.Control | Keys.Left))
        this.TrvModel.Focus();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void CkbDefaultSize_CheckedChanged(object sender, EventArgs e)
    {
      if (this.CkbDefaultSize.Checked)
      {
        //최대화였어도 일반으로 바꾸고 기본창 사이즈로
        this.WindowState = FormWindowState.Normal;
        this.StartPosition = FormStartPosition.CenterScreen;

        this.Width = SCommon.DEFAULT_FORM_WIDTH;
        this.Height = SCommon.DEFAULT_FORM_HEIGHT;
        this.splitContainer1.SplitterDistance = SCommon.DEFAULT_SPLITTER_DISTANCE;
      }
      else
      {
        this.Width = SCommon.LoggedInUser.Width;
        this.Height = SCommon.LoggedInUser.Height;
        //this.Location = new Point(SCommon.LoggedInUser.LocationX, SCommon.LoggedInUser.LocationY);
        this.splitContainer1.SplitterDistance = SCommon.LoggedInUser.SplitterDistance;
      }
    }


    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SaveMyOption();
    }

    private void SaveMyOption()
    {
      if(SCommon.LoggedInUser != null)
      {
        SCommon.LoggedInUser.Width = this.Width;
        SCommon.LoggedInUser.Height = this.Height;
        SCommon.LoggedInUser.SplitterDistance = this.splitContainer1.SplitterDistance;
        SCommon.LoggedInUser.ExpandType = this.CmbExpand.Text;
        SCommon.LoggedInUser.ShowID = this.CkbShowID.Checked;
        SCommon.LoggedInUser.ShowEng = this.CkbShowEng.Checked;
        SCommon.LoggedInUser.SortType = this.CmbSortBy.Text;

        this.SDA.UpdateUserMyOption(SCommon.LoggedInUser);
      }
    }

    #endregion

    #region Login/Logout

    private void ShowFormLogin()
    {
      FormLogin formLogin = new FormLogin();
      formLogin.LoggedIn += LoggedIn;
      formLogin.ShowDialog();
    }

    private void LoggedIn(bool success)
    {
      if (success)
      {
        this.LblUser.Text = SCommon.LoggedInUserDisplay;

        if (SCommon.LoggedInUser.Role == SCommon.RoleModeler)
        {
          this.BtnProjectOption.Visible = true;
          this.BtnUser.Visible = true;
        }
        else
        {
          this.BtnProjectOption.Visible = false;
          this.BtnUser.Visible = false;
        }

        this.LoadMyOption();
        this.LoadTreeOption();
        this.SetControl();
        this.SetProjectOption();
        this.ShowDashboard();
      }
      else
      {
        this.Close();
      }
    }

    private void LoadMyOption()
    {
      this.Width = SCommon.LoggedInUser.Width;
      this.Height = SCommon.LoggedInUser.Height;

      //this.Location = new Point(SCommon.LoggedInUser.LocationX, SCommon.LoggedInUser.LocationY);
      this.StartPosition = FormStartPosition.CenterScreen;

      this.splitContainer1.SplitterDistance = SCommon.LoggedInUser.SplitterDistance;

      //this.RbnSortByName.Checked = SCommon.LoggedInUser.SortByName;
      this.CmbExpand.Text = SCommon.LoggedInUser.ExpandType;
      this.CmbSortBy.Text = SCommon.LoggedInUser.SortType;
      this.SortByName = SCommon.LoggedInUser.SortType == "Name";

      this.CkbShowID.Checked = SCommon.LoggedInUser.ShowID;
      this.CkbShowEng.Checked = SCommon.LoggedInUser.ShowEng;
      SCommon.ShowID = SCommon.LoggedInUser.ShowID;
      SCommon.ShowEng = SCommon.LoggedInUser.ShowEng;
    }

    private void SetControl()
    {
      this.BtnGoBack.Enabled = false;

      //상단 메뉴 버튼
      this.toolTip1.SetToolTip(this.BtnGoBack, SMessage.BTN_GO_BACK);
      this.toolTip1.SetToolTip(this.BtnDashboard, SMessage.BTN_DASH_BOARD);
      this.toolTip1.SetToolTip(this.BtnDictionary, SMessage.BTN_DICTIONARY);
      this.toolTip1.SetToolTip(this.BtnMyOption, SMessage.BTN_MY_OPTION);
      this.toolTip1.SetToolTip(this.BtnProjectOption, SMessage.BTN_PROJECT_OPTION);
      this.toolTip1.SetToolTip(this.BtnUser, SMessage.BTN_USER);
      this.toolTip1.SetToolTip(this.BtnLogout, SMessage.BTN_LOGOUT);
      this.toolTip1.SetToolTip(this.BtnHelp, SMessage.BTN_HELP);

      //Tree
      this.toolTip1.SetToolTip(this.TbxSearchNodeText, SMessage.BTN_FIND_NODE);
      this.toolTip1.SetToolTip(this.BtnFindNode, SMessage.BTN_FIND_NODE);
      this.toolTip1.SetToolTip(this.BtnTreeOption, SMessage.BTN_TREE_OPTION);
      this.toolTip1.SetToolTip(this.BtnRefresh, SMessage.BTN_REFRESH);
      this.toolTip1.SetToolTip(this.BtnSynchTree, SMessage.BTN_SYNC_TREE);
    }

    private void SetProjectOption()
    {
      //Dictionary
      this.BtnDictionary.Visible = SCommon.SProject.Dictionary;
      //this.LblNounCount.Visible = SCommon.SProject.Dictionary;
      //this.LblBRVerbCount.Visible = SCommon.SProject.Dictionary;
      //this.LblDAVerbCount.Visible = SCommon.SProject.Dictionary;
    }

    private void BtnLogout_Click(object sender, EventArgs e)
    {
      this.SaveMyOption();

      this.ShowFormLogin();
    }

    #endregion


    #region Load TreeView

    private void LoadTreeView()
    {
      this.NodeMicroservices.Nodes.Clear();
      this.NodeMicroservices.ImageKey = "SDM";
      this.NodeMicroservices.SelectedImageKey = "SDM";

      this.NodeInternalSystems.Nodes.Clear();
      this.NodeInternalSystems.ImageKey = "SDM";
      this.NodeInternalSystems.SelectedImageKey = "SDM";

      this.NodeExternalSystems.Nodes.Clear();
      this.NodeExternalSystems.ImageKey = "SDM";
      this.NodeExternalSystems.SelectedImageKey = "SDM";

      List<SMicroservice> msList = this.SDA.SelectMicroserviceList(this.SortByName, true);
      List<SInternalSystem> siList = this.SDA.SelectInternalSystemList(this.SortByName, true);
      List<SExternalSystem> seList = this.SDA.SelectExternalSystemList(this.SortByName, true);

      foreach (SMicroservice ms in msList)
      {
        TreeNode nodeMS = SCommon.CreateNode(ms, false);

        List<SBizPackage> bpList = this.SBR.SelectBizPackgeListByMicroservice(ms.GUID, this.SortByName);

        foreach (SBizPackage bp in bpList)
        {
          TreeNode nodeBP = SCommon.CreateNode(bp, false);

          #region API

          List<SAPI> apiList = this.SBR.SelectAPIListByParent(bp, this.SortByName);

          foreach (SAPI api in apiList)
            nodeBP.Nodes.Add(SCommon.CreateNode(api, false));

          #endregion

          #region Controller

          List<SController> ctrList = this.SBR.SelectControllerListByBP(bp.GUID, this.SortByName);

          foreach (SController ctr in ctrList)
          {
            TreeNode nodeCtr = SCommon.CreateNode(ctr, false);

            #region API

            List<SAPI> ctrAPIList = this.SBR.SelectAPIListByParent(ctr, this.SortByName);

            foreach (SAPI api in ctrAPIList)
              nodeCtr.Nodes.Add(SCommon.CreateNode(api, false));

            #endregion

            nodeBP.Nodes.Add(nodeCtr);
          }

          #endregion


          #region Publisher

          List<SPublisher> pubList = this.SBR.SelectPublisherListByParent(bp, this.SortByName);

          foreach (SPublisher pub in pubList)
            nodeBP.Nodes.Add(SCommon.CreateNode(pub, false));

          #endregion

          #region Producer

          List<SProducer> producerList = this.SBR.SelectProducerListByBP(bp.GUID, this.SortByName);

          foreach (SProducer producer in producerList)
          {
            TreeNode nodeProducer = SCommon.CreateNode(producer, false);

            #region Publisher

            List<SPublisher> producerPubList = this.SBR.SelectPublisherListByParent(producer, this.SortByName);

            foreach (SPublisher pub in producerPubList)
              nodeProducer.Nodes.Add(SCommon.CreateNode(pub, false));

            #endregion

            nodeBP.Nodes.Add(nodeProducer);
          }

          #endregion


          #region Subscriber

          List<SSubscriber> subList = this.SBR.SelectSubscriberListByParent(bp, this.SortByName);

          foreach (SSubscriber sub in subList)
            nodeBP.Nodes.Add(SCommon.CreateNode(sub, false));

          #endregion

          #region Consumer

          List<SConsumer> consumerList = this.SBR.SelectConsumerListByBP(bp.GUID, this.SortByName);

          foreach (SConsumer consumer in consumerList)
          {
            TreeNode nodeConsumer = SCommon.CreateNode(consumer, false);

            #region Subscriber

            List<SSubscriber> consumerSubList = this.SBR.SelectSubscriberListByParent(consumer, this.SortByName);

            foreach (SSubscriber sub in consumerSubList)
              nodeConsumer.Nodes.Add(SCommon.CreateNode(sub, false));

            #endregion

            nodeBP.Nodes.Add(nodeConsumer);
          }

          #endregion


          #region Other

          List<SOther> otherList = this.SBR.SelectOtherListByParent(bp, this.SortByName);

          foreach (SOther other in otherList)
            nodeBP.Nodes.Add(SCommon.CreateNode(other, false));

          #endregion


          #region BizRule

          List<SBizRule> brList = this.SBR.SelectBizRuleListByBP(bp.GUID, this.SortByName);

          foreach (SBizRule br in brList)
          {
            TreeNode nodeBR = SCommon.CreateNode(br, false);

            #region BR Operation

            List<SBizRuleOperation> brOpList = this.SBR.SelectBizRuleOperationListByBR(br.GUID, this.SortByName);

            foreach (SBizRuleOperation brOp in brOpList)
              nodeBR.Nodes.Add(SCommon.CreateNode(brOp, false));

            #endregion

            nodeBP.Nodes.Add(nodeBR);
          }

          #endregion

          #region DataAccess

          List<SDataAccess> daList = this.SBR.SelectDataAccessListByBP(bp.GUID, this.SortByName);

          foreach (SDataAccess da in daList)
          {
            TreeNode nodeDA = SCommon.CreateNode(da, false);

            #region DA Operation

            List<SDataAccessOperation> daOpList = this.SBR.SelectDataAccessOperationListByDA(da.GUID, this.SortByName);

            foreach (SDataAccessOperation daOp in daOpList)
              nodeDA.Nodes.Add(SCommon.CreateNode(daOp, false));

            #endregion

            nodeBP.Nodes.Add(nodeDA);
          }

          #endregion


          #region Dto

          List<SDto> dtoList = this.SBR.SelectDtoListByBP(bp.GUID, this.SortByName);

          foreach (SDto dto in dtoList)
            nodeBP.Nodes.Add(SCommon.CreateNode(dto, false));

          #endregion

          #region Entity

          List<SEntity> entList = this.SBR.SelectEntityListByBP(bp.GUID, this.SortByName);

          foreach (SEntity ent in entList)
            nodeBP.Nodes.Add(SCommon.CreateNode(ent, false));

          #endregion


          #region UI

          List<SUI> uiList = this.SBR.SelectUIListByBP(bp.GUID, this.SortByName);

          foreach (SUI ui in uiList)
            nodeBP.Nodes.Add(SCommon.CreateNode(ui, false));

          #endregion


          #region Job

          List<SJob> jobList = this.SBR.SelectJobListByBP(bp.GUID, this.SortByName);

          foreach (SJob job in jobList)
          {
            TreeNode nodeJob = SCommon.CreateNode(job, false);

            #region Step

            List<SStep> stepList = this.SBR.SelectStepListByJob(job.GUID, this.SortByName);

            foreach (SStep step in stepList)
              nodeJob.Nodes.Add(SCommon.CreateNode(step, false));

            #endregion

            nodeBP.Nodes.Add(nodeJob);
          }

          #endregion

          nodeMS.Nodes.Add(nodeBP);
        }

        this.NodeMicroservices.Nodes.Add(nodeMS);
      }

      foreach(SInternalSystem si in siList)
      {
        TreeNode nodeSI = SCommon.CreateNode(si, false);

        #region API

        List<SAPI> apiList = this.SBR.SelectAPIListByParent(si, this.SortByName);

        foreach (SAPI api in apiList)
          nodeSI.Nodes.Add(SCommon.CreateNode(api, false));

        #endregion

        #region Publisher

        List<SPublisher> pubList = this.SBR.SelectPublisherListByParent(si, this.SortByName);

        foreach (SPublisher pub in pubList)
          nodeSI.Nodes.Add(SCommon.CreateNode(pub, false));

        #endregion

        #region Subscriber

        List<SSubscriber> subList = this.SBR.SelectSubscriberListByParent(si, this.SortByName);

        foreach (SSubscriber sub in subList)
          nodeSI.Nodes.Add(SCommon.CreateNode(sub, false));

        #endregion

        #region Other

        List<SOther> otherList = this.SBR.SelectOtherListByParent(si, this.SortByName);

        foreach (SOther other in otherList)
          nodeSI.Nodes.Add(SCommon.CreateNode(other, false));

        #endregion

        this.NodeInternalSystems.Nodes.Add(nodeSI);
      }

      foreach (SExternalSystem se in seList)
      {
        TreeNode nodeSE = SCommon.CreateNode(se, false);

        #region API

        List<SAPI> apiList = this.SBR.SelectAPIListByParent(se, this.SortByName);

        foreach (SAPI api in apiList)
          nodeSE.Nodes.Add(SCommon.CreateNode(api, false));

        #endregion

        #region Publisher

        List<SPublisher> pubList = this.SBR.SelectPublisherListByParent(se, this.SortByName);

        foreach (SPublisher pub in pubList)
          nodeSE.Nodes.Add(SCommon.CreateNode(pub, false));

        #endregion

        #region Subscriber

        List<SSubscriber> subList = this.SBR.SelectSubscriberListByParent(se, this.SortByName);

        foreach (SSubscriber sub in subList)
          nodeSE.Nodes.Add(SCommon.CreateNode(sub, false));

        #endregion

        #region Other

        List<SOther> otherList = this.SBR.SelectOtherListByParent(se, this.SortByName);

        foreach (SOther other in otherList)
          nodeSE.Nodes.Add(SCommon.CreateNode(other, false));

        #endregion

        this.NodeExternalSystems.Nodes.Add(nodeSE);
      }

      this.TrvModel.Nodes.Clear();
      this.TrvModel.Nodes.Add(this.NodeMicroservices);
      this.TrvModel.Nodes.Add(this.NodeInternalSystems);
      this.TrvModel.Nodes.Add(this.NodeExternalSystems);

      this.ExpandTrvModel();
    }

    #endregion


    #region Context Menu

    private void TrvModel_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && e.Clicks == 2)
        this.IsTreeDoubleClick = true;
      else
        this.IsTreeDoubleClick = false;

      if (e.Button != MouseButtons.Right)
        return;

      if (this.TrvModel.SelectedNode == null)
        return;

      #region 컴포넌트별, 권한별 메뉴 Display 여부

      this.AddMicroserviceMenu.Visible = false;
      this.AddInternalSystemMenu.Visible = false;
      this.AddExternalSystemMenu.Visible = false;

      this.AddBizPackageMenu.Visible = false;

      this.AddControllerMenu.Visible = false;
      this.AddAPIMenu.Visible = false;

      this.AddProducerMenu.Visible = false;
      this.AddPublisherMenu.Visible = false;

      this.AddConsumerMenu.Visible = false;
      this.AddSubscriberMenu.Visible = false;

      this.AddOtherMenu.Visible = false;

      this.AddBizRuleMenu.Visible = false;
      this.AddDataAccessMenu.Visible = false;
      this.AddOperationMenu.Visible = false;

      this.AddDtoMenu.Visible = false;
      this.AddEntityMenu.Visible = false;

      this.AddUIMenu.Visible = false;

      this.AddJobMenu.Visible = false;
      this.AddStepMenu.Visible = false;

      this.CutMenu.Visible = false;
      this.CopyMenu.Visible = false;
      this.PasteMenu.Visible = false;
      this.DeleteMenu.Visible = SCommon.HasPermission(this.TrvModel.SelectedNode.Tag as SComponent);

      this.UploadMenu.Visible = false;

      this.GenerateSpecMenu.Visible = false;
      this.GenerateCodeMenu.Visible = false;

      if (this.TrvModel.SelectedNode == this.NodeMicroservices)
      {
        this.AddMicroserviceMenu.Visible = true;
      }
      else if (this.TrvModel.SelectedNode == this.NodeInternalSystems)
      {
        this.AddInternalSystemMenu.Visible = true;
      }
      else if (this.TrvModel.SelectedNode == this.NodeExternalSystems)
      {
        this.AddExternalSystemMenu.Visible = true;
      }

      else if (this.TrvModel.SelectedNode.Tag is SMicroservice)
      {
        this.AddBizPackageMenu.Visible = true;
      }
      else if (this.TrvModel.SelectedNode.Tag is SInternalSystem)
      {
        this.AddAPIMenu.Visible = true;
        this.AddPublisherMenu.Visible = SCommon.SProject.AddPublisher;
        this.AddSubscriberMenu.Visible = SCommon.SProject.AddSubscriber;
        this.AddOtherMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;
      }
      else if (this.TrvModel.SelectedNode.Tag is SExternalSystem)
      {
        this.AddAPIMenu.Visible = true;
        this.AddPublisherMenu.Visible = SCommon.SProject.AddPublisher;
        this.AddSubscriberMenu.Visible = SCommon.SProject.AddSubscriber;
        this.AddOtherMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;
      }

      else if (this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        this.AddControllerMenu.Visible = true;
        this.AddAPIMenu.Visible = true;

        this.AddProducerMenu.Visible = SCommon.SProject.AddProducer;
        this.AddPublisherMenu.Visible = SCommon.SProject.AddPublisher;

        this.AddConsumerMenu.Visible = SCommon.SProject.AddConsumer;
        this.AddSubscriberMenu.Visible = SCommon.SProject.AddSubscriber;

        this.AddOtherMenu.Visible = true;

        this.AddBizRuleMenu.Visible = true;
        this.AddDataAccessMenu.Visible = true;

        this.AddDtoMenu.Visible = true;
        this.AddEntityMenu.Visible = true;

        this.AddUIMenu.Visible = true;

        this.AddJobMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;
      }

      else if (this.TrvModel.SelectedNode.Tag is SController)
      {
        this.AddAPIMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SAPI)
      {
        this.CopyMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SProducer)
      {
        this.AddPublisherMenu.Visible = SCommon.SProject.AddPublisher;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SPublisher)
      {
        this.CopyMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SConsumer)
      {
        this.AddSubscriberMenu.Visible = SCommon.SProject.AddSubscriber;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SSubscriber)
      {
        this.CopyMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SOther)
      {
        this.CopyMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SBizRule)
      {
        this.AddOperationMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SDataAccess)
      {
        this.AddOperationMenu.Visible = true;

        this.PasteMenu.Visible = true;

        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SBizRuleOperation || this.TrvModel.SelectedNode.Tag is SDataAccessOperation)
      {
        this.CopyMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SDto)
      {
        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }
      else if (this.TrvModel.SelectedNode.Tag is SEntity)
      {
        this.UploadMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
        this.GenerateCodeMenu.Visible = SCommon.SProject.GenerateCode;
      }

      else if (this.TrvModel.SelectedNode.Tag is SUI)
      {
        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }

      else if (this.TrvModel.SelectedNode.Tag is SJob)
      {
        this.AddStepMenu.Visible = true;

        this.GenerateSpecMenu.Visible = SCommon.SProject.GenerateSpec;
      }
      else if (this.TrvModel.SelectedNode.Tag is SStep)
      {
        this.CopyMenu.Visible = true;
      }

      #endregion

      this.contextMenuStrip1.Show(this.TrvModel, e.Location);
    }

    #region Add components

    private void AddMicroserviceMenu_Click(object sender, EventArgs e)
    {
      SMicroservice ms = this.SDA.InsertMicroservice();

      TreeNode node = SCommon.CreateNode(ms, true);
      this.TrvModel.SelectedNode.Nodes.Add(node);
      this.TrvModel.SelectedNode.Expand();

      this.ViewComponentDetail(node, true, false);
    }

    private void AddInternalSystemMenu_Click(object sender, EventArgs e)
    {
      SInternalSystem si = this.SDA.InsertInternalSystem();

      TreeNode node = SCommon.CreateNode(si, true);
      this.TrvModel.SelectedNode.Nodes.Add(node);
      this.TrvModel.SelectedNode.Expand();

      this.ViewComponentDetail(node, true, false);
    }

    private void AddExternalSystemMenu_Click(object sender, EventArgs e)
    {
      SExternalSystem se = this.SDA.InsertExternalSystem();

      TreeNode node = SCommon.CreateNode(se, true);
      this.TrvModel.SelectedNode.Nodes.Add(node);
      this.TrvModel.SelectedNode.Expand();

      this.ViewComponentDetail(node, true, false);
    }

    private void AddBizPackageMenu_Click(object sender, EventArgs e)
    {
      SBizPackage bp = this.SDA.InsertBizPackge(this.TrvModel.SelectedNode.Tag as SMicroservice);

      TreeNode node = SCommon.CreateNode(bp, true);
      this.TrvModel.SelectedNode.Nodes.Add(node);
      this.TrvModel.SelectedNode.Expand();

      this.ViewComponentDetail(node, true, false);
    }


    private void AddControllerMenu_Click(object sender, EventArgs e)
    {
      SController ctr = this.SDA.InsertController(this.TrvModel.SelectedNode.Tag as SBizPackage);

      TreeNode node = SCommon.CreateNode(ctr, true);
      this.TrvModel.SelectedNode.Nodes.Add(node);
      this.TrvModel.SelectedNode.Expand();

      this.ViewComponentDetail(node, true, false);
    }

    private void AddAPIMenu_Click(object sender, EventArgs e)
    {
      this.AddAPI();
    }

    private void AddAPI(string name = "")
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag is SBizPackage || this.TrvModel.SelectedNode.Tag is SController || this.TrvModel.SelectedNode.Tag is SInternalSystem || this.TrvModel.SelectedNode.Tag is SExternalSystem)
      {
        SAPI api = this.SBR.InsertAPI(this.TrvModel.SelectedNode.Tag as SComponent, name);

        TreeNode node = SCommon.CreateNode(api, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if(name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddProducerMenu_Click(object sender, EventArgs e)
    {
      if(this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SProducer producer = this.SDA.InsertProducer(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(producer, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddPublisherMenu_Click(object sender, EventArgs e)
    {
      this.AddPublisher();
    }

    private void AddPublisher(string name = "")
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag is SBizPackage || this.TrvModel.SelectedNode.Tag is SProducer || this.TrvModel.SelectedNode.Tag is SInternalSystem || this.TrvModel.SelectedNode.Tag is SExternalSystem)
      {
        SPublisher pub = this.SBR.InsertPublisher(this.TrvModel.SelectedNode.Tag as SComponent, name);

        TreeNode node = SCommon.CreateNode(pub, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddConsumerMenu_Click(object sender, EventArgs e)
    {
      if(this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SConsumer consumer = this.SDA.InsertConsumer(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(consumer, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddSubscriberMenu_Click(object sender, EventArgs e)
    {
      this.AddSubscriber();
    }

    private void AddSubscriber(string name = "")
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag is SBizPackage || this.TrvModel.SelectedNode.Tag is SConsumer || this.TrvModel.SelectedNode.Tag is SInternalSystem || this.TrvModel.SelectedNode.Tag is SExternalSystem)
      {
        SSubscriber sub = this.SBR.InsertSubscriber(this.TrvModel.SelectedNode.Tag as SComponent, name);

        TreeNode node = SCommon.CreateNode(sub, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddOtherMenu_Click(object sender, EventArgs e)
    {
      this.AddOther();
    }

    private void AddOther(string name = "")
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag is SBizPackage || this.TrvModel.SelectedNode.Tag is SInternalSystem || this.TrvModel.SelectedNode.Tag is SExternalSystem)
      {
        SOther other = this.SBR.InsertOther(this.TrvModel.SelectedNode.Tag as SComponent, name);

        TreeNode node = SCommon.CreateNode(other, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddBizRuleMenu_Click(object sender, EventArgs e)
    {
      if(this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SBizRule br = this.SDA.InsertBizRule(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(br, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddDataAccessMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SDataAccess da = this.SDA.InsertDataAccess(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(da, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddOperationMenu_Click(object sender, EventArgs e)
    {
      this.AddOperation();
    }

    private void AddOperation(string name = "")
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag is SBizRule)
      {
        SBizRuleOperation brOp = this.SBR.InsertBizRuleOperation(this.TrvModel.SelectedNode.Tag as SBizRule, name);

        TreeNode node = SCommon.CreateNode(brOp, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
      else if (this.TrvModel.SelectedNode.Tag is SDataAccess)
      {
        SDataAccessOperation daOp = this.SBR.InsertDataAccessOperation(this.TrvModel.SelectedNode.Tag as SDataAccess, name);

        TreeNode node = SCommon.CreateNode(daOp, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }



    private void AddDtoMenu_Click(object sender, EventArgs e)
    {
      if(this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SDto dto = this.SDA.InsertDto(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(dto, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddEntityMenu_Click(object sender, EventArgs e)
    {
      if(this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SEntity entity = this.SDA.InsertEntity(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(entity, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }


    private void AddUIMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SUI ui = this.SDA.InsertUI(this.TrvModel.SelectedNode.Tag as SBizPackage);

        TreeNode node = SCommon.CreateNode(ui, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddJobMenu_Click(object sender, EventArgs e)
    {
      this.AddJob();
    }

    private void AddJob(string name = "")
    {
      if (this.TrvModel.SelectedNode.Tag is SBizPackage)
      {
        SJob job = this.SDA.InsertJob(this.TrvModel.SelectedNode.Tag as SBizPackage, name);

        TreeNode node = SCommon.CreateNode(job, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        if (name.Length == 0)
          this.ViewComponentDetail(node, true, false);
      }
    }

    private void AddStepMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag is SJob)
      {
        SStep step = this.SBR.InsertStep(this.TrvModel.SelectedNode.Tag as SJob);

        TreeNode node = SCommon.CreateNode(step, true);
        this.TrvModel.SelectedNode.Nodes.Add(node);
        this.TrvModel.SelectedNode.Expand();

        this.ViewComponentDetail(node, true, false);
      }
    }

    #endregion

    private void CopyMenu_Click(object sender, EventArgs e)
    {
      this.CopyNode();
    }

    private void CopyNode()
    {
      this.CopiedNode = this.TrvModel.SelectedNode;
    }

    private void PasteMenu_Click(object sender, EventArgs e)
    {
      this.PasteNode();
    }

    private void PasteNode()
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.CopiedNode == null)
        return;

      TreeNode parentNode = this.TrvModel.SelectedNode;

      if (parentNode == null)
        return;

      if (this.CopiedNode.Tag == parentNode.Tag)
        parentNode = this.TrvModel.SelectedNode.Parent;

      if (this.CopiedNode.Tag is SAPI)
      {
        if (parentNode.Tag is SBizPackage || parentNode.Tag is SController || parentNode.Tag is SInternalSystem || parentNode.Tag is SExternalSystem)
        {
          SAPI copiedAPI = this.SBR.CopyAPI(parentNode.Tag as SComponent, this.CopiedNode.Tag as SAPI);
          TreeNode copiedNodeAPI = SCommon.CreateNode(copiedAPI, true);
          parentNode.Nodes.Add(copiedNodeAPI);
        }
      }
      else if (this.CopiedNode.Tag is SPublisher)
      {
        if (parentNode.Tag is SBizPackage || parentNode.Tag is SProducer || parentNode.Tag is SInternalSystem || parentNode.Tag is SExternalSystem)
        {
          SPublisher copiedPub = this.SBR.CopyPublisher(parentNode.Tag as SComponent, this.CopiedNode.Tag as SPublisher);
          TreeNode copiedNodePub = SCommon.CreateNode(copiedPub, true);
          parentNode.Nodes.Add(copiedNodePub);
        }
      }
      else if (this.CopiedNode.Tag is SSubscriber)
      {
        if (parentNode.Tag is SBizPackage || parentNode.Tag is SConsumer || parentNode.Tag is SInternalSystem || parentNode.Tag is SExternalSystem)
        {
          SSubscriber copiedSub = this.SBR.CopySubscriber(parentNode.Tag as SComponent, this.CopiedNode.Tag as SSubscriber);
          TreeNode copiedNodeSub = SCommon.CreateNode(copiedSub, true);
          parentNode.Nodes.Add(copiedNodeSub);
        }
      }
      else if (this.CopiedNode.Tag is SOther)
      {
        if (parentNode.Tag is SBizPackage || parentNode.Tag is SInternalSystem || parentNode.Tag is SExternalSystem)
        {
          SOther copiedOther = this.SBR.CopyOther(parentNode.Tag as SComponent, this.CopiedNode.Tag as SOther);
          TreeNode copiedNodeOther = SCommon.CreateNode(copiedOther, true);
          parentNode.Nodes.Add(copiedNodeOther);
        }
      }
      else if (this.CopiedNode.Tag is SBizRuleOperation)
      {
        if (parentNode.Tag is SBizRule)
        {
          SBizRuleOperation copiedBizRuleOperation = this.SBR.CopyBizRuleOperation(parentNode.Tag as SBizRule, this.CopiedNode.Tag as SBizRuleOperation);
          TreeNode copiedNodeBizRuleOperation = SCommon.CreateNode(copiedBizRuleOperation, true);
          parentNode.Nodes.Add(copiedNodeBizRuleOperation);
        }
      }
      else if (this.CopiedNode.Tag is SDataAccessOperation)
      {
        if (parentNode.Tag is SDataAccess)
        {
          SDataAccessOperation copiedDataAccessOperation = this.SBR.CopyDataAccessOperation(parentNode.Tag as SDataAccess, this.CopiedNode.Tag as SDataAccessOperation);
          TreeNode copiedNodeDataAccessOperation = SCommon.CreateNode(copiedDataAccessOperation, true);
          parentNode.Nodes.Add(copiedNodeDataAccessOperation);
        }
      }

      else if (this.CopiedNode.Tag is SStep)
      {
        if (parentNode.Tag is SJob)
        {
          SStep copiedStep = this.SBR.CopyStep(parentNode.Tag as SJob, this.CopiedNode.Tag as SStep);
          TreeNode copiedNodeStep = SCommon.CreateNode(copiedStep, true);
          parentNode.Nodes.Add(copiedNodeStep);
        }
      }

      parentNode.Expand();
    }

    private void DeleteMenu_Click(object sender, EventArgs e)
    {
      this.DeleteNode();
    }

    private void DeleteNode()
    {
      if (this.TrvModel.SelectedNode == null)
        return;

      if (this.TrvModel.SelectedNode.Tag == null)
        return;

      //하위 노드가 있는 경우 삭제 불가
      if (this.TrvModel.SelectedNode.Nodes.Count > 0)
      {
        SMessageBox.ShowWarning(SMessage.HAS_CHILD_NODE);
        return;
      }

      SComponent selectedComponent = this.TrvModel.SelectedNode.Tag as SComponent;

      //Consumer 체크
      List<SCaller> callerList = this.SBR.GetConsumerList(selectedComponent);

      if (callerList.Count > 0)
      {
        string callerListMessage = string.Empty;

        foreach (SCaller caller in callerList)
          callerListMessage += caller.FullName + Environment.NewLine;

        SMessageBox.ShowWarning(SMessage.CAN_NOT_DELETE_CONSUMER, callerListMessage);
        return;
      }

      //Confirm
      DialogResult result = SMessageBox.ShowConfirm(SMessage.DELETE, this.TrvModel.SelectedNode.Text);

      if (result == DialogResult.No)
        return;

      //Delete
      if (selectedComponent is SMicroservice)
        this.SDA.DeleteMicroservice(selectedComponent as SMicroservice);
      else if (selectedComponent is SInternalSystem)
        this.SBR.DeleteInternalSystem(selectedComponent as SInternalSystem);
      else if (selectedComponent is SExternalSystem)
        this.SBR.DeleteExternalSystem(selectedComponent as SExternalSystem);

      else if (selectedComponent is SBizPackage)
        this.SDA.DeleteBizPackge(selectedComponent as SBizPackage);

      else if (selectedComponent is SController)
        this.SDA.DeleteController(selectedComponent as SController);
      else if (selectedComponent is SAPI)
        this.SDA.DeleteAPI(selectedComponent as SAPI);

      else if (selectedComponent is SProducer)
        this.SDA.DeleteProducer(selectedComponent as SProducer);
      else if (selectedComponent is SPublisher)
        this.SDA.DeletePublisher(selectedComponent as SPublisher);

      else if (selectedComponent is SConsumer)
        this.SDA.DeleteConsumer(selectedComponent as SConsumer);
      else if (selectedComponent is SSubscriber)
        this.SDA.DeleteSubscriber(selectedComponent as SSubscriber);

      else if (selectedComponent is SOther)
        this.SDA.DeleteOther(selectedComponent as SOther);

      else if (selectedComponent is SBizRule)
        this.SDA.DeleteBizRule(selectedComponent as SBizRule);
      else if (selectedComponent is SBizRuleOperation)
        this.SBR.DeleteBizRuleOperation(selectedComponent as SBizRuleOperation);

      else if (selectedComponent is SDataAccess)
        this.SDA.DeleteDataAccess(selectedComponent as SDataAccess);
      else if (selectedComponent is SDataAccessOperation)
        this.SDA.DeleteDataAccessOperation(selectedComponent as SDataAccessOperation);

      else if (selectedComponent is SDto)
        this.SBR.DeleteDto(selectedComponent as SDto);
      else if (selectedComponent is SEntity)
        this.SBR.DeleteEntity(selectedComponent as SEntity);

      else if (selectedComponent is SUI)
        this.SBR.DeleteUI(selectedComponent as SUI);

      else if (selectedComponent is SJob)
        this.SDA.DeleteJob(selectedComponent as SJob);
      else if (selectedComponent is SStep)
        this.SDA.DeleteStep(selectedComponent as SStep);

      this.TrvModel.SelectedNode.Remove();
    }


    private void UploadMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag == null)
        return;

      FormUploader form = new FormUploader();
      form.SelectedComponent = this.TrvModel.SelectedNode.Tag as SComponent;
      form.UploaderChanged += UploaderChanged;
      form.ShowDialog();
    }

    private void UploaderChanged(string type, string[] inputList)
    {
      foreach (string input in inputList)
      {
        if (input.Length > 0)
        {
          if (type == "A")
            this.AddAPI(input);
          else if (type == "P")
            this.AddPublisher(input);
          else if (type == "S")
            this.AddSubscriber(input);
          else if (type == "O")
            this.AddOther(input);
          else if (type == "OP")
            this.AddOperation(input);
          else if (type == "B")
            this.AddJob(input);
        }
      }
    }

    #endregion


    #region Detail View

    private void TrvModel_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      this.ViewComponentDetail(e.Node, false, false);
    }

    //로그인시 또는 상단DS 메뉴 선택시 FormDS 띄워야 함
    //트리에서 해당 요소 클릭시에도 해당 Form 띄워야 함
    private void ViewComponentDetail(TreeNode node, bool focusDetailView, bool move)
    {
      if (node == null)
        return;

      if (node.Tag == null)
        return;

      if (this.splitContainer1.Panel2.Controls.Count == 1)
        (this.splitContainer1.Panel2.Controls[0] as Form).Close();

      this.SelectedNode = node;
      SComponent selectedComponent = node.Tag as SComponent;
      
      //move를 기록하지 않으면 UI > API > BR 등의 이동을 뒤로가기 할 수 없어 사용하지 않음
      //move 관련 parameter code는 나중에 정리
      this.SelectedNodeGuidStack.Push(selectedComponent.GUID);
      this.BtnGoBack.Enabled = true;

      #region Child form

      if (selectedComponent is SMicroservice)
      {
        FormSMicroservice form = new FormSMicroservice();
        form.SMicroservice = selectedComponent as SMicroservice;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SInternalSystem)
      {
        FormSInternalSystem form = new FormSInternalSystem();
        form.SInternalSystem = selectedComponent as SInternalSystem;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SExternalSystem)
      {
        FormSExternalSystem form = new FormSExternalSystem();
        form.SExternalSystem = selectedComponent as SExternalSystem;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SBizPackage)
      {
        FormSBizPackage form = new FormSBizPackage();
        form.SBizPackage = selectedComponent as SBizPackage;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SController)
      {
        FormSController form = new FormSController();
        form.SController = selectedComponent as SController;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SAPI)
      {
        FormSAPI form = new FormSAPI();
        form.SAPI = selectedComponent as SAPI;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SProducer)
      {
        FormSProducer form = new FormSProducer();
        form.SProducer = selectedComponent as SProducer;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SPublisher)
      {
        FormSPublisher form = new FormSPublisher();
        form.SPublisher = selectedComponent as SPublisher;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SConsumer)
      {
        FormSConsumer form = new FormSConsumer();
        form.SConsumer = selectedComponent as SConsumer;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SSubscriber)
      {
        FormSSubscriber form = new FormSSubscriber();
        form.SSubscriber = selectedComponent as SSubscriber;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SOther)
      {
        FormSOther form = new FormSOther();
        form.SOther = selectedComponent as SOther;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }


      else if (selectedComponent is SBizRule)
      {
        FormSBizRule form = new FormSBizRule();
        form.SBizRule = selectedComponent as SBizRule;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SBizRuleOperation)
      {
        FormSBizRuleOperation form = new FormSBizRuleOperation();
        form.SBizRuleOperation = selectedComponent as SBizRuleOperation;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SDataAccess)
      {
        FormSDataAccess form = new FormSDataAccess();
        form.SDataAccess = selectedComponent as SDataAccess;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SDataAccessOperation)
      {
        FormSDataAccessOperation form = new FormSDataAccessOperation();
        form.SDataAccessOperation = selectedComponent as SDataAccessOperation;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SDto)
      {
        FormSDto form = new FormSDto();
        form.SDto = selectedComponent as SDto;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SEntity)
      {
        FormSEntity form = new FormSEntity();
        form.SEntity = selectedComponent as SEntity;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SUI)
      {
        FormSUI form = new FormSUI();
        form.SUI = selectedComponent as SUI;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      else if (selectedComponent is SJob)
      {
        FormSJob form = new FormSJob();
        form.SJob = selectedComponent as SJob;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }
      else if (selectedComponent is SStep)
      {
        FormSStep form = new FormSStep();
        form.SStep = selectedComponent as SStep;
        form.ComponentChanged += ComponentChanged;
        form.MoveComponent += MoveComponent;
        this.ShowChldForm(form);

        if (focusDetailView)
          SCommon.SelectAllAndFocus(form.TbxName);
      }

      #endregion

      if (focusDetailView == false)
        this.TrvModel.Focus();
    }

    private void ShowDashboard()
    {
      //FormDashboard formDS;

      //foreach (Form form in Application.OpenForms)
      //{
      //  if (form.GetType() == typeof(FormDashboard))
      //  {
      //    formDS = form as FormDashboard;
      //    formDS.Activate();
      //    this.ShowChldForm(formDS);

      //    return;
      //  }
      //}

      if (this.splitContainer1.Panel2.Controls.Count == 1)
        (this.splitContainer1.Panel2.Controls[0] as Form).Close();

      FormDashboard form = new FormDashboard();
      form.ExternalLinked = false;
      form.MoveComponent += MoveComponent;
      form.TrvModel = this.TrvModel;
      this.ShowChldForm(form);
    }

    private void ShowChldForm(Form form)
    {
      form.TopLevel = false;
      form.Dock = DockStyle.Fill;

      this.splitContainer1.Panel2.Controls.Add(form);
      form.Show();
    }


    private void ComponentChanged(SComponent component)
    {
      this.SelectedNode.Text = SCommon.GetNodeText(component);
      this.SelectedNode.Tag = component;

      this.TrvModel.Focus();
    }

    private void MoveComponent(string guid)
    {
      this.MoveToTheComponent(guid);
    }

    #endregion


    #region TreeView Drag/Drop

    private void TrvModel_ItemDrag(object sender, ItemDragEventArgs e)
    {
      //Child form으로 전달하기 위한 TreeNode
      SCommon.DraggedNodeFromTree = (TreeNode)e.Item;
      this.DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);
    }

    private void TrvModel_DragOver(object sender, DragEventArgs e)
    {
      TreeView tv = sender as TreeView;
      Point pt = tv.PointToClient(new Point(e.X, e.Y));

      int delta = tv.Height - pt.Y;

      if ((delta < tv.Height / 2) && (delta > 0))
      {
        TreeNode tn = tv.GetNodeAt(pt.X, pt.Y);

        if (tn != null && tn.NextVisibleNode != null)
          tn.NextVisibleNode.EnsureVisible();
      }

      if ((delta > tv.Height / 2) && (delta < tv.Height))
      {
        TreeNode tn = tv.GetNodeAt(pt.X, pt.Y);

        if (tn != null && tn.PrevVisibleNode != null)
          tn.PrevVisibleNode.EnsureVisible();
      }
    }

    private void TrvModel_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.Text))
        e.Effect = DragDropEffects.Move;
      else
        e.Effect = DragDropEffects.None;
    }

    private void TrvModel_DragDrop(object sender, DragEventArgs e)
    {
      Point pos = this.TrvModel.PointToClient(new Point(e.X, e.Y));
      TreeNode targetNode = this.TrvModel.GetNodeAt(pos);

      if (targetNode == null)
        return;

      if (targetNode.Tag == null)
        return;

      //이동할 권한이 있는지 체크
      if (SCommon.HasPermission(SCommon.DraggedNodeFromTree.Tag as SComponent) == false)
      {
        SMessageBox.ShowWarning(SMessage.CAN_NOT_MOVE);
        return;
      }

      bool updatedParent = false;

      //Data
      if (SCommon.DraggedNodeFromTree.Tag is SBizPackage)
      {
        if (targetNode.Tag is SMicroservice)
        {
          this.SBR.UpdateBizPackgeParent(targetNode.Tag as SMicroservice, SCommon.DraggedNodeFromTree.Tag as SBizPackage);
          updatedParent = true;
        }
      }

      if (SCommon.DraggedNodeFromTree.Tag is SController)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateControllerParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SController);
          updatedParent = true;
        }
      }
      else if(SCommon.DraggedNodeFromTree.Tag is SAPI)
      {
        if(targetNode.Tag is SBizPackage || targetNode.Tag is SController || targetNode.Tag is SInternalSystem || targetNode.Tag is SExternalSystem)
        {
          this.SDA.UpdateAPIParent(targetNode.Tag as SComponent, SCommon.DraggedNodeFromTree.Tag as SAPI);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SProducer)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateProducerParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SProducer);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SPublisher)
      {
        if (targetNode.Tag is SBizPackage || targetNode.Tag is SProducer || targetNode.Tag is SInternalSystem || targetNode.Tag is SExternalSystem)
        {
          this.SDA.UpdatePublisherParent(targetNode.Tag as SComponent, SCommon.DraggedNodeFromTree.Tag as SPublisher);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SConsumer)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateConsumerParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SConsumer);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SSubscriber)
      {
        if (targetNode.Tag is SBizPackage || targetNode.Tag is SConsumer || targetNode.Tag is SInternalSystem || targetNode.Tag is SExternalSystem)
        {
          this.SDA.UpdateSubscriberParent(targetNode.Tag as SComponent, SCommon.DraggedNodeFromTree.Tag as SSubscriber);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SOther)
      {
        if (targetNode.Tag is SBizPackage || targetNode.Tag is SInternalSystem || targetNode.Tag is SExternalSystem)
        {
          this.SDA.UpdateOtherParent(targetNode.Tag as SComponent, SCommon.DraggedNodeFromTree.Tag as SOther);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SBizRule)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateBizRuleParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SBizRule);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SBizRuleOperation)
      {
        if (targetNode.Tag is SBizRule)
        {
          this.SDA.UpdateBizRuleOperationParent(targetNode.Tag as SBizRule, SCommon.DraggedNodeFromTree.Tag as SBizRuleOperation);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SDataAccess)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateDataAccessParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SDataAccess);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SDataAccessOperation)
      {
        if (targetNode.Tag is SDataAccess)
        {
          this.SDA.UpdateDataAccessOperationParent(targetNode.Tag as SDataAccess, SCommon.DraggedNodeFromTree.Tag as SDataAccessOperation);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SDto)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateDtoParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SDto);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SEntity)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateEntityParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SEntity);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SUI)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SDA.UpdateUIParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SUI);
          updatedParent = true;
        }
      }

      else if (SCommon.DraggedNodeFromTree.Tag is SJob)
      {
        if (targetNode.Tag is SBizPackage)
        {
          this.SBR.UpdateJobParent(targetNode.Tag as SBizPackage, SCommon.DraggedNodeFromTree.Tag as SJob);
          updatedParent = true;
        }
      }
      else if (SCommon.DraggedNodeFromTree.Tag is SStep)
      {
        if (targetNode.Tag is SJob)
        {
         this.SDA.UpdateStepParent(targetNode.Tag as SJob, SCommon.DraggedNodeFromTree.Tag as SStep);
          updatedParent = true;
        }
      }

      //Display : add to target and delete selected node
      if (updatedParent)
      {
        targetNode.Nodes.Add((TreeNode)SCommon.DraggedNodeFromTree.Clone());
        targetNode.Expand();
        SCommon.DraggedNodeFromTree.Remove();
      }
    }

    #endregion


    #region Tree Option

    private void CmbExpand_SelectedIndexChanged(object sender, EventArgs e)
    {
      //더블클릭 후 TreeViewCancelEventArgs e.Canecl되어 작동하지 않으므로 아래와 같이 변경 후 수행
      this.IsTreeDoubleClick = false;

      this.ExpandTrvModel();
    }

    private void ExpandTrvModel()
    {
      if (this.CmbExpand.Text == "All")
      {
        this.TrvModel.ExpandAll();
      }
      else if (this.CmbExpand.Text == "Microservice")
      {
        this.TrvModel.CollapseAll();

        foreach (TreeNode nodeType in this.TrvModel.Nodes)
          nodeType.Expand();
      }
      else if (this.CmbExpand.Text == "BizPackage")
      {
        this.TrvModel.CollapseAll();

        foreach (TreeNode nodeType in this.TrvModel.Nodes)
        {
          nodeType.Expand();

          foreach (TreeNode nodeMS in nodeType.Nodes)
          {
            if (nodeMS.Tag is SMicroservice)
              nodeMS.Expand();
          }
        }
      }

      this.TrvModel.Focus();
    }


    private void CmbSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.CmbSortBy.Text == "Name")
        this.SortByName = true;
      else
        this.SortByName = false;

      this.LoadTreeView();
    }


    private void CkbShowID_CheckedChanged(object sender, EventArgs e)
    {
      SCommon.ShowID = this.CkbShowID.Checked;

      this.LoadTreeView();
    }

    private void CkbShowEng_CheckedChanged(object sender, EventArgs e)
    {
      SCommon.ShowEng = this.CkbShowEng.Checked;

      this.LoadTreeView();
    }


    private void TrvModel_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      //MessageBox.Show("node click");
      //this.SelectedNode = e.Node;
    }

    private void TrvModel_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
      if (this.IsTreeDoubleClick && e.Action == TreeViewAction.Collapse)
        e.Cancel = true;
    }

    private void TrvModel_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      if (this.IsTreeDoubleClick && e.Action == TreeViewAction.Expand)
        e.Cancel = true;
    }


    private void BtnTreeOption_Click(object sender, EventArgs e)
    {
      FormTreeOption form = new FormTreeOption();
      form.TreeOptionChanged += TreeOptionChanged;
      form.ShowDialog();
    }

    private void TreeOptionChanged()
    {
      this.LoadTreeOption();
    }

    private void LoadTreeOption()
    {
      if(SCommon.LoggedInUser.TreeFont != null && SCommon.LoggedInUser.TreeFont.Length > 0)
        this.TrvModel.Font = new Font(SCommon.LoggedInUser.TreeFont, float.Parse(SCommon.LoggedInUser.TreeFontSize));
      else
        this.TrvModel.Font = new Font("맑은 고딕", float.Parse("9.75"));

      this.LoadTreeView();
      
    }

    private void BtnSynchTree_Click(object sender, EventArgs e)
    {
      if(this.SelectedNode.Tag != null && this.SelectedNode.Tag is SComponent)
        this.MoveComponent((this.SelectedNode.Tag as SComponent).GUID);
    }

    #endregion


    #region Refresh / MoveToTheComponent

    private void BtnRefresh_Click(object sender, EventArgs e)
    {
      this.RefreshTreeView();
    }

    private void RefreshTreeView()
    {
      if (this.TrvModel.SelectedNode != null && this.TrvModel.SelectedNode.Tag is SComponent)
      {
        string selectedGUID = (this.TrvModel.SelectedNode.Tag as SComponent).GUID;
        this.LoadTreeView();
        this.MoveToTheComponent(selectedGUID);
      }
    }

    private void MoveToTheComponent(string guid)
    {
      if(guid != null && guid.Length > 0)
      {
        this.TrvModel.Focus();

        TreeNode searchedNode = this.SearchTreeNodeByComponentGUID(this.TrvModel.Nodes, guid);

        if(searchedNode != null)
        {
          this.TrvModel.SelectedNode = searchedNode;
          this.ViewComponentDetail(this.TrvModel.SelectedNode, false, true);

          this.TrvModel.SelectedNode.Parent.Expand();
          this.TrvModel.Focus();
        }
      }
    }

    private TreeNode SearchTreeNodeByComponentGUID(TreeNodeCollection nodeList, string guid)
    {
      foreach (TreeNode node in nodeList)
      {
        if (node.Tag is SComponent)
        {
          SComponent component = node.Tag as SComponent;

          if (component.GUID == guid)
            return node;

          TreeNode searchedNode = this.SearchTreeNodeByComponentGUID(node.Nodes, guid);

          if (searchedNode != null)
            return searchedNode;
        }
        else
        {
          TreeNode searchedNode = this.SearchTreeNodeByComponentGUID(node.Nodes, guid);

          if (searchedNode != null)
            return searchedNode;
        }
      }

      return null;
    }

    #endregion

    #region Find

    private void TbxSearchNodeText_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchNodeList(this.TbxSearchNodeText.Text);
    }

    private void BtnFindNode_Click(object sender, EventArgs e)
    {
      this.SearchNodeList(this.TbxSearchNodeText.Text);
    }

    private void SearchNodeList(string searchNodeText)
    {
      this.TrvModel.Focus();

      this.SearchedNodeList.Clear();
      this.LastSearchedNodeIndex = 0;

      this.SearchTreeNodeListByText(this.TrvModel.Nodes, searchNodeText);

      if (this.SearchedNodeList.Count > 0)
      {
        this.TrvModel.SelectedNode = this.SearchedNodeList[0];
        this.LastSearchedNodeIndex += 1;

        if (this.TrvModel.SelectedNode != null)
          this.ViewComponentDetail(this.TrvModel.SelectedNode, false, false);

        //this.TrvModel.SelectedNode.Parent.Expand();
      }
      else
      {
        SMessageBox.ShowInformation(SMessage.NOT_FOUND);
      }
    }

    private void SearchTreeNodeListByText(TreeNodeCollection nodeList, string searchText)
    {
      foreach (TreeNode node in nodeList)
      {
        //node.Text 가 이미 공백없이 구성되어 있음
        if(searchText.StartsWith("%"))
        {
          //%문자열
          if (node.Text.ToLower().EndsWith(searchText.Replace("%", "").ToLower()))//ToLower는 대소문자 관계없이 찾기 위해
            this.SearchedNodeList.Add(node);
        }
        else if (searchText.EndsWith("%"))
        {
          //문자열%
          if (node.Text.ToLower().StartsWith(searchText.Replace("%", "").ToLower()))//ToLower는 대소문자 관계없이 찾기 위해
            this.SearchedNodeList.Add(node);
        }
        else
        {
          if (node.Text.ToLower().Contains(searchText.ToLower()))//ToLower는 대소문자 관계없이 찾기 위해
            this.SearchedNodeList.Add(node);
        }
        

        this.SearchTreeNodeListByText(node.Nodes, searchText);
      }
    }

    private void SearchNodeListByF3()
    {
      if (this.LastSearchedNodeIndex == this.SearchedNodeList.Count)
        this.LastSearchedNodeIndex = 0; //모두 검색한 경우 다시 처음부터

      if (this.LastSearchedNodeIndex < this.SearchedNodeList.Count)
      {
        this.TrvModel.SelectedNode = this.SearchedNodeList[this.LastSearchedNodeIndex];
        this.LastSearchedNodeIndex += 1;

        if (this.TrvModel.SelectedNode != null)
          this.ViewComponentDetail(this.TrvModel.SelectedNode, false, false);
      }
    }

    #endregion


    #region 상단 메뉴

    private void BtnGoBack_Click(object sender, EventArgs e)
    {
      //A > B > C 인 경우, 마지막인 C를 제외하고 B A 순으로 보이기
      if (this.SelectedNodeGuidStack.Count > 0)
      {
        this.BtnGoBack.Enabled = true;

        //현재 선택된 상세보기(this.SelectedNode)와 이동하려는 컴포넌트가 동일할 때(C의 경우), 다음(B)로 이동
        string guid = this.SelectedNodeGuidStack.Pop();

        if (this.SelectedNodeGuidStack.Count > 0 && (this.SelectedNode.Tag as SComponent).GUID == guid)
          guid = this.SelectedNodeGuidStack.Pop();

        this.MoveToTheComponent(guid);
      }
      else
      {
        this.BtnGoBack.Enabled = false;
      }
    }

    private void BtnDashboard_Click(object sender, EventArgs e)
    {
      this.ShowDashboard();
    }

    private void BtnUser_Click(object sender, EventArgs e)
    {
      FormUser form = new FormUser();
      form.ShowDialog();
    }


    private void BtnProjectOption_Click(object sender, EventArgs e)
    {
      FormProjectOption form = new FormProjectOption();
      form.ProjectOptionChanged += ProjectOptionChanged;
      form.ShowDialog();
    }

    //private void ProjectOptionChanged(bool refresh)
    private void ProjectOptionChanged()
    {
      this.SetProjectOption();
    }


    private void BtnMyOption_Click(object sender, EventArgs e)
    {
      FormMyOption formMyOption = new FormMyOption();
      //formMyOption.MyOptionChanged += MyOptionChanged;
      formMyOption.ShowDialog();
    }

    private void MyOptionChanged(bool refreshTree)
    {
    }

    private void BtnHelp_Click(object sender, EventArgs e)
    {
      FormHelp form = new FormHelp();
      form.ShowDialog();
    }

    #endregion

    #region 사전

    private void BtnDictionary_Click(object sender, EventArgs e)
    {
      //FormDictionary form = new FormDictionary();
      //form.FormClosed += DictionaryFormClosed;
      //form.ShowDialog();

      FormDictionaryNew form = new FormDictionaryNew();
      form.ShowDialog();
    }

    private void DictionaryFormClosed(object sender, EventArgs e)
    {
      //this.DisplayDictionary();
    }

    //private void DisplayDictionary()
    //{
    //  this.LblNounCount.Text = string.Format("명사 : {0}", SDictionary.DicNoun.Rows.Count);
    //  this.LblBRVerbCount.Text = string.Format("BR동사 : {0}", SDictionary.DicBRVerb.Rows.Count);
    //  this.LblDAVerbCount.Text = string.Format("DA동사 : {0}", SDictionary.DicDAVerb.Rows.Count);
    //}

    #endregion

    #region 설계서/코드

    private void GenerateSpecMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag is SComponent)
      {
        SComponent component = this.TrvModel.SelectedNode.Tag as SComponent;

        if (component.DesignCompleteSkeleton && component.DesignCompleteDetail)
        {
          this.SSpec.GenerateSpec(component);
        }
        else
        {
          SMessageBox.ShowWarning(SMessage.NO_GENERATE);
          return;
        }
      }

      //before : class와 하위 operation 모두 기본설계, 상세설계 완료시 출력
      //after : class만 기본설계, 상세설계 완료, 하위는 기본설계, 상세설계 완료된것만 출력
      //if (this.TrvModel.SelectedNode.Tag is SComponent)
      //{
      //  SComponent component = this.TrvModel.SelectedNode.Tag as SComponent;

      //  foreach (TreeNode childNode in this.TrvModel.SelectedNode.Nodes)
      //  {
      //    SComponent childComponent = childNode.Tag as SComponent;

      //    if (childComponent.DesignCompleteSkeleton && childComponent.DesignCompleteDetail)
      //    {

      //    }
      //    else
      //    {
      //      SMessageBox.ShowWarning(SMessage.NO_GENERATE);
      //      return;
      //    }
      //  }

      //  if (component.DesignCompleteSkeleton && component.DesignCompleteDetail)
      //  {
      //    this.SSpec.GenerateSpec(component);
      //  }
      //  else
      //  {
      //    SMessageBox.ShowWarning(SMessage.NO_GENERATE);
      //    return;
      //  }
      //}
    }

    private void GenerateCodeMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvModel.SelectedNode.Tag is SComponent)
      {
        SComponent component = this.TrvModel.SelectedNode.Tag as SComponent;

        if (component.DesignCompleteSkeleton && component.DesignCompleteDetail)
        {
          this.SCode.GenerateCode(component);
        }
        else
        {
          SMessageBox.ShowWarning(SMessage.NO_GENERATE);
          return;
        }
      }
    }

    #endregion
  }
}