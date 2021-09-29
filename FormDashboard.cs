using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

using SDM.Common;
using SDM.Component;
using SDM.Project;

namespace SDM
{
  public partial class FormDashboard : Form
  {
    public TreeView TrvModel;
    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);
    public bool ExternalLinked = false;
    private SBR SBR;
    private SDA SDA;

    public FormDashboard()
    {
      InitializeComponent();
    }

    private void FormDashboard_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();

      this.Text = SCommon.ProductVersion + " - Dashboard";

      this.BtnExternalLink.Enabled = this.ExternalLinked == false;
      this.BtnMoveRight.Enabled = this.ExternalLinked;

      SCommon.SetBizPartCombo(this.CmbPart1);
      SCommon.SetBizPartCombo(this.CmbPart2);
      SCommon.SetBizPartCombo(this.CmbPartProgram, true);
      SCommon.SetMicroserviceCombo(this.CmbMS, true);

      this.CmbPart1.Text = SCommon.LoggedInUser.PartName;
      this.CmbPart2.Text = SCommon.LoggedInUser.PartName;

      this.DtpFrom.Value = System.DateTime.Now.AddDays(-6);
      this.DtpFromChart.Value = System.DateTime.Now.AddDays(-6);
    }

    private void TabControl_Selected(object sender, TabControlEventArgs e)
    {
      if (this.TabControl.SelectedTab == this.TabConsumerProvider)
      {
        this.DgvConsumerProvider.Rows.Clear();
        this.SetConsumerProviderSynch(string.Empty, this.TrvModel.Nodes);
      }
    }

    #region User

    private void CmbPart1_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CmbUser1.Items.Clear();

      List<SUser> userList = this.SBR.SelectUserListByPartGUID((this.CmbPart1.SelectedItem as SPart).GUID);

      foreach (SUser user in userList)
        this.CmbUser1.Items.Add(user);

      this.CmbUser1.Text = SCommon.LoggedInUser.Name;

      this.DgvPartList.Rows.Clear();
      //this.TabPart.Text = this.CmbPart1.Text;
      //this.SetPartComponent("", this.TrvModel.Nodes, (this.CmbPart1.SelectedItem as SPart).GUID);
    }

    private void CmbUser1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.CmbUser1.SelectedItem != null)
      {
        this.DgvMyList.Rows.Clear();
        //this.TabUser.Text = string.Format("[{0}]{1}", this.CmbPart1.Text, this.CmbUser1.Text);
        //this.SetUserComponent("", this.TrvModel.Nodes, (this.CmbUser1.SelectedItem as SUser).GUID);
      }
    }

    private void BtnSearchUser_Click(object sender, EventArgs e)
    {
      this.SetUserComponent("", this.TrvModel.Nodes, (this.CmbUser1.SelectedItem as SUser).GUID);
    }

    private void SetUserComponent(string parentNodeText, TreeNodeCollection nodeList, string userGUID)
    {
      foreach(TreeNode node in nodeList)
      {
        string fullPath = parentNodeText.Length > 0 ? parentNodeText + " > " + node.Text : node.Text;

        if (node.Tag is SComponent)
        {
          SComponent component = node.Tag as SComponent;

          if (this.IsMyComponent(component, userGUID))
            this.DgvMyList.Rows.Add(component.GUID, component.GetType().ToString().Replace("SDM.Component.S", ""), fullPath, SCommon.GetRegistered(component), SCommon.GetLastModified(component));
        }

        this.SetUserComponent(fullPath, node.Nodes, userGUID);
      }
    }

    private void DgvMyList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      //if (e.ColumnIndex == 2)
      if(this.ExternalLinked)
        this.MoveComponent(this.DgvMyList.SelectedRows[0].Cells[0].Value.ToString());
    }


    private bool IsMyComponent(SComponent component, string userGUID = "")
    {
      userGUID = userGUID.Length == 0 ? SCommon.LoggedInUser.GUID : userGUID;

      if (component.RegisteredUserGUID == userGUID)
        return true;
      else if (component.LastModifiedUserGUID == userGUID)
        return true;
      else
        return false;
    }

    #endregion

    #region Part

    private void BtnSearchPart_Click(object sender, EventArgs e)
    {
      this.SetPartComponent("", this.TrvModel.Nodes, (this.CmbPart2.SelectedItem as SPart).GUID);
    }

    private void SetPartComponent(string parentNodeText, TreeNodeCollection nodeList, string partGUID)
    {
      foreach (TreeNode node in nodeList)
      {
        string fullPath = parentNodeText.Length > 0 ? parentNodeText + " > " + node.Text : node.Text;

        if (node.Tag is SComponent)
        {
          SComponent component = node.Tag as SComponent;

          if (this.IsMyPartComponent(component, partGUID))
            this.DgvPartList.Rows.Add(component.GUID, component.GetType().ToString().Replace("SDM.Component.S", ""), fullPath, SCommon.GetRegistered(component), SCommon.GetLastModified(component));
        }

        this.SetPartComponent(fullPath, node.Nodes, partGUID);
      }
    }

    private void DgvPartList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if(this.ExternalLinked)
        this.MoveComponent(this.DgvPartList.SelectedRows[0].Cells[0].Value.ToString());
    }


    private bool IsMyPartComponent(SComponent component, string partGUID = "")
    {
      partGUID = partGUID.Length == 0 ? SCommon.LoggedInUser.PartGUID : partGUID;

      if (component.RegisteredPartGUID == partGUID)
        return true;
      else if (component.LastModifiedPartGUID == partGUID)
        return true;
      else
        return false;
    }

    #endregion

    #region Program

    private void CmbPart3_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CmbUserProgram.Items.Clear();

      if (this.CmbPartProgram.Text != SCommon.ALL)
      {
        List<SUser> userList = this.SBR.SelectUserListByPartGUID((this.CmbPartProgram.SelectedItem as SPart).GUID);

        foreach (SUser user in userList)
          this.CmbUserProgram.Items.Add(user);
      }

      this.CmbUserProgram.Items.Insert(0, SCommon.ALL);
      this.CmbUserProgram.Text = SCommon.ALL;

      //this.SearchProgram();
    }

    private void CmbUser3_SelectedIndexChanged(object sender, EventArgs e)
    {
      //this.SearchProgram();
    }

    private void TbxNameProgram_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchProgram();
    }

    private void BtnSearchProgram_Click(object sender, EventArgs e)
    {
      this.SearchProgram();
    }

    private void SearchProgram()
    {
      this.DgvProgram.Rows.Clear();

      string searchPartName = this.CmbPartProgram.Text == SCommon.ALL ? string.Empty : this.CmbPartProgram.Text;
      string searchUserName = this.CmbUserProgram.Text == SCommon.ALL ? string.Empty : this.CmbUserProgram.Text;

      DataTable dt = this.SDA.SelectProgramList(this.TbxNameProgram.Text, searchPartName, searchUserName);
      //this.DgvProgram.DataSource = dt;

      foreach (DataRow row in dt.Rows)
      {
        SComponent component = new SComponent();

        component.GUID = Convert.ToString(row["GUID"]);
        component.ProgramID = Convert.ToString(row["ProgramID"]);
        component.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        component.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        component.Name = Convert.ToString(row["Name"]);
        component.NameEnglish = Convert.ToString(row["NameEnglish"]);
        component.Description = Convert.ToString(row["Description"]);

        component.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        component.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        component.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        component.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        component.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        component.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        component.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        component.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        component.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        component.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvProgram.Rows.Add(
          component.GUID,
          Convert.ToString(row["MicroserviceName"]),
          Convert.ToString(row["BizPackageName"]),
          Convert.ToString(row["Type"]),
          component.ProgramID,
          SCommon.GetName(component),
          component.NameEnglish,
          SCommon.GetDesignCompleteDisplay(component),
          SCommon.GetDesription(component),
          SCommon.GetRegistered(component),
          SCommon.GetLastModified(component)
        );


      }
    }

    private void DgvProgram_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvProgram.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region MS

    private void TbxNameMS_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchMS();
    }

    private void BtnSearchMS_Click(object sender, EventArgs e)
    {
      this.SearchMS();  
    }

    private void SearchMS()
    {
      this.DgvMS.Rows.Clear();

      DataTable dt = this.SDA.SelectMicroserviceList(this.TbxNameMS.Text);

      foreach (DataRow row in dt.Rows)
      {
        SMicroservice ms = new SMicroservice();
        ms.GUID = Convert.ToString(row["GUID"]);
        ms.ProgramID = Convert.ToString(row["ProgramID"]);
        ms.Name = Convert.ToString(row["Name"]);
        ms.BizPartGUID = Convert.ToString(row["BizPartGUID"]);
        ms.BizPartName = Convert.ToString(row["BizPartName"]);
        ms.Description = Convert.ToString(row["Description"]);

        //  int countAPI = 0;
        //  int countPub = 0;
        //  int countSub = 0;
        //  int countOther = 0;
        //  int countBPOp = 0;
        //  int countDAOp = 0;
        //  int countDto = 0;
        //  int countEntity = 0;
        //  int countUI = 0;
        //  int countJob = 0;

        this.DgvMS.Rows.Add(
          ms.GUID,
          ms.BizPartName,
          ms.ProgramID, //ID
          ms.Name,
          SCommon.GetDesription(ms),
          Convert.ToInt32(row["BPCount"]), //BP
          Convert.ToInt32(row["APICount"]), //API
          Convert.ToInt32(row["PubCount"]), //PUB
          Convert.ToInt32(row["SubCount"]), //SUB
          Convert.ToInt32(row["OtherCount"]), //Other
          Convert.ToInt32(row["BROpCount"]), //BP Op
          Convert.ToInt32(row["DAOpCount"]), //DA Op
          Convert.ToInt32(row["DtoCount"]), //Dto
          Convert.ToInt32(row["EntityCount"]), //Entity
          Convert.ToInt32(row["UICount"]), //UI
          Convert.ToInt32(row["JobCount"]),//Job
          "" //bpList.Count == 0 ? "BizPackage 미정의" : ""
        );
      }

      //foreach (DataGridViewRow row in this.DgvMS.Rows)
      //{
      //  if (row.Cells[16].Value != null && row.Cells[16].Value.ToString().Length > 0)
      //    row.DefaultCellStyle.ForeColor = Color.Salmon;
      //}
    }

    private void DgvMS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvMS.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region System

    private void TbxNameSystem_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchSystem();
    }

    private void BtnSearchSystem_Click(object sender, EventArgs e)
    {
      this.SearchSystem();
    }

    private void SearchSystem()
    {
      this.DgvSystem.Rows.Clear();

      DataTable dt = this.SDA.SelectSystemList(this.TbxNameSystem.Text);

      foreach (DataRow row in dt.Rows)
      {
        SComponent component = new SComponent();
        component.GUID = Convert.ToString(row["GUID"]);
        component.ProgramID = Convert.ToString(row["ProgramID"]);
        component.Name = Convert.ToString(row["Name"]);
        component.Description = Convert.ToString(row["Description"]);

        this.DgvSystem.Rows.Add(
          component.GUID,
          Convert.ToString(row["Type"]),
          component.ProgramID,
          component.Name,
          SCommon.GetDesription(component),
          Convert.ToInt32(row["APICount"]), //API
          Convert.ToInt32(row["PubCount"]), //PUB
          Convert.ToInt32(row["SubCount"]), //SUB
          Convert.ToInt32(row["OtherCount"]), //Other
          "" //countAPI == 0 && countPub == 0 && countSub == 0 && countOther == 0 ? "API/Publisher/Subscriber/Other 미정의" : ""
        );
      }

      //foreach (DataGridViewRow row in this.DgvSystem.Rows)
      //{
      //  if (row.Cells[9].Value != null && row.Cells[9].Value.ToString().Length > 0)
      //    row.DefaultCellStyle.ForeColor = Color.Salmon;
      //}
    }

    private void DgvSystem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvSystem.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region BP

    private void TbxNameBP_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchBizPackage();
    }

    private void BtnSearchBP_Click(object sender, EventArgs e)
    {
      this.SearchBizPackage();
    }

    private void SearchBizPackage()
    {
      this.DgvBP.Rows.Clear();

      DataTable dt = this.SDA.SelectBizPackgeList(this.TbxNameBP.Text);

      foreach (DataRow row in dt.Rows)
      {
        int countAPI = Convert.ToInt32(row["APICount"]);
        int countPub = Convert.ToInt32(row["PubCount"]);
        int countSub = Convert.ToInt32(row["SubCount"]);
        int countOther = Convert.ToInt32(row["OtherCount"]);
        int countBPOp = Convert.ToInt32(row["BROpCount"]);
        int countDAOp = Convert.ToInt32(row["DAOpCount"]);
        int countDto = Convert.ToInt32(row["DtoCount"]);
        int countEntity = Convert.ToInt32(row["EntityCount"]);
        int countUI = Convert.ToInt32(row["UICount"]);
        int countJob = Convert.ToInt32(row["JobCount"]);

        SBizPackage bp = new SBizPackage();
        bp.GUID = Convert.ToString(row["GUID"]);
        bp.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        bp.ProgramID = Convert.ToString(row["ProgramID"]);
        bp.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        bp.Name = Convert.ToString(row["Name"]);
        bp.Description = Convert.ToString(row["Description"]);

        bp.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        bp.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        bp.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        bp.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        bp.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        bp.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        bp.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        bp.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        bp.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        bp.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        string verification = string.Empty;

        if (countAPI == 0 && countPub == 0 && countSub == 0)
          verification += "API/Publisher/Subscriber 미정의";

        if (countBPOp == 0)
          verification += verification.Length > 0 ? ", " + "BR OP 미정의" : "BR OP 미정의";

        if (countDAOp == 0)
          verification += verification.Length > 0 ? ", " + "DA OP 미정의" : "DA OP 미정의";

        this.DgvBP.Rows.Add(
          bp.GUID,
          bp.MicroserviceName,
          SCommon.GetDesignCompleteDisplay(bp),
          bp.ProgramID,
          bp.Name,
          bp.NameEnglish,
          SCommon.GetDesription(bp),
          countAPI,
          countPub,
          countSub,
          countOther,
          countBPOp,
          countDAOp,
          countDto,
          countEntity,
          countUI,
          countJob,
          verification, //17
          SCommon.GetRegistered(bp),
          SCommon.GetLastModified(bp)
        );
      }

      foreach (DataGridViewRow row in this.DgvBP.Rows)
      {
        if (row.Cells[17].Value != null && row.Cells[17].Value.ToString().Length > 0)
          row.DefaultCellStyle.ForeColor = Color.Salmon;
      }
    }

    private void DgvBP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvBP.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region API

    private void TbxAPIName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchAPI();
    }

    private void TbxAPIMethodName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchAPI();
    }

    private void BtnSearchAPI_Click(object sender, EventArgs e)
    {
      this.SearchAPI();
    }

    private void SearchAPI()
    {
      //Cursor.Current = Cursors.WaitCursor;
      
      this.DgvAPI.Rows.Clear();

      DataTable dt = this.SDA.SelectAPIList(this.TbxNameAPI.Text, this.TbxNameAPIMethod.Text);

      foreach (DataRow row in dt.Rows)
      {
        SAPI api = new SAPI();
        api.GUID = Convert.ToString(row["GUID"]);
        api.BizPackageName = Convert.ToString(row["BizPackageName"]);
        api.ControllerName = Convert.ToString(row["ControllerName"]);
        api.ProgramID = Convert.ToString(row["ProgramID"]);
        api.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        api.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        api.Name = Convert.ToString(row["Name"]);
        api.NameEnglish = Convert.ToString(row["NameEnglish"]);
        api.HttpMethod = Convert.ToString(row["HttpMethod"]);
        api.URI = Convert.ToString(row["URI"]);
        api.Input = Convert.ToString(row["Input"]);
        api.Output = Convert.ToString(row["Output"]);
        api.Description = Convert.ToString(row["Description"]);

        api.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        api.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        api.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        api.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        api.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        api.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        api.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        api.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        api.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        api.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvAPI.Rows.Add(
          api.GUID,
          Convert.ToString(row["ServiceSystem"]),
          api.BizPackageName,
          api.ControllerName,
          api.ProgramID,
          SCommon.GetDesignCompleteDisplay(api),
          SCommon.GetName(api),
          api.NameEnglish,
          api.HttpMethod,
          api.URI,
          api.Input,
          api.Output,
          SCommon.GetName(Convert.ToString(row["CalleeBROperationName"])),
          SCommon.GetDesription(api),
          SCommon.GetRegistered(api),
          SCommon.GetLastModified(api)
        );
      }

      //Cursor.Current = Cursors.Default;
    }

    private void DgvAPI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvAPI.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion


    #region PUB

    private void TbxNamePub_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchPublisher();
    }

    private void BtnSearchPub_Click(object sender, EventArgs e)
    {
      this.SearchPublisher();
    }

    private void SearchPublisher()
    {
      this.DgvPub.Rows.Clear();

      DataTable dt = this.SDA.SelectPublisherList(this.TbxNamePub.Text);

      foreach (DataRow row in dt.Rows)
      {
        SPublisher pub = new SPublisher();
        pub.GUID = Convert.ToString(row["GUID"]);
        pub.BizPackageName = Convert.ToString(row["BizPackageName"]);
        pub.ProducerName = Convert.ToString(row["ProducerName"]);
        pub.ProgramID = Convert.ToString(row["ProgramID"]);
        pub.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        pub.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        pub.Name = Convert.ToString(row["Name"]);
        pub.NameEnglish = Convert.ToString(row["NameEnglish"]);
        pub.Input = Convert.ToString(row["Input"]);
        pub.Topic = Convert.ToString(row["Topic"]);
        pub.Description = Convert.ToString(row["Description"]);

        pub.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        pub.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        pub.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        pub.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        pub.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        pub.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        pub.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        pub.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        pub.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        pub.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvPub.Rows.Add(
          pub.GUID,
          Convert.ToString(row["ServiceSystem"]),
          pub.BizPackageName,
          pub.ProducerName,
          pub.ProgramID,
          SCommon.GetDesignCompleteDisplay(pub),
          SCommon.GetName(pub),
          pub.NameEnglish,
          pub.Input,
          pub.Topic,
          SCommon.GetDesription(pub),
          SCommon.GetRegistered(pub),
          SCommon.GetLastModified(pub)
        );
      }
    }

    private void DgvPub_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvPub.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region SUB

    private void TbxNameSub_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchSubscriber();
    }

    private void BtnSearchSub_Click(object sender, EventArgs e)
    {
      this.SearchSubscriber();
    }

    private void SearchSubscriber()
    {
      this.DgvSub.Rows.Clear();

      DataTable dt = this.SDA.SelectSubscriberList(this.TbxNameSub.Text);

      foreach (DataRow row in dt.Rows)
      {
        SSubscriber sub = new SSubscriber();
        sub.GUID = Convert.ToString(row["GUID"]);
        sub.BizPackageName = Convert.ToString(row["BizPackageName"]);
        sub.ConsumerName = Convert.ToString(row["ConsumerName"]);
        sub.ProgramID = Convert.ToString(row["ProgramID"]);
        sub.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        sub.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        sub.Name = Convert.ToString(row["Name"]);
        sub.NameEnglish = Convert.ToString(row["NameEnglish"]);
        sub.Input = Convert.ToString(row["Input"]);
        sub.Topic = Convert.ToString(row["Topic"]);
        sub.Description = Convert.ToString(row["Description"]);

        sub.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        sub.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        sub.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        sub.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        sub.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        sub.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        sub.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        sub.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        sub.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        sub.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvSub.Rows.Add(
          sub.GUID,
          Convert.ToString(row["ServiceSystem"]),
          sub.BizPackageName,
          sub.ConsumerName,
          sub.ProgramID,
          SCommon.GetDesignCompleteDisplay(sub),
          SCommon.GetName(sub),
          sub.NameEnglish,
          sub.Input,
          sub.Topic,
          SCommon.GetName(Convert.ToString(row["CalleeBROperationName"])),
          SCommon.GetDesription(sub),
          SCommon.GetRegistered(sub),
          SCommon.GetLastModified(sub)
        );
      }
    }

    private void DgvSub_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvSub.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region Other

    private void TbxNameOther_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchOther();
    }

    private void BtnSearchOther_Click(object sender, EventArgs e)
    {
      this.SearchOther();
    }

    private void SearchOther()
    {
      this.DgvOther.Rows.Clear();

      DataTable dt = this.SDA.SelectOtherList(this.TbxNameOther.Text);

      foreach (DataRow row in dt.Rows)
      {
        SOther other = new SOther();
        other.GUID = Convert.ToString(row["GUID"]);
        other.BizPackageName = Convert.ToString(row["BizPackageName"]);
        other.ProgramID = Convert.ToString(row["ProgramID"]);
        other.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        other.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        other.Name = Convert.ToString(row["Name"]);
        other.NameEnglish = Convert.ToString(row["NameEnglish"]);
        other.SenderReceiver = Convert.ToString(row["SenderReceiver"]);

        if (SComponentType.InternalSystem.ToString() == Convert.ToString(row["SystemType"]))
          other.SystemType = SComponentType.InternalSystem;
        else if (SComponentType.ExternalSystem.ToString() == Convert.ToString(row["SystemType"]))
          other.SystemType = SComponentType.ExternalSystem;

        //other.SystemGUID = Convert.ToString(row["SystemGUID"]);
        other.SystemName = Convert.ToString(row["SystemName"]);
        other.Type = Convert.ToString(row["Type"]);
        other.TypeText = Convert.ToString(row["TypeText"]);
        other.Input = Convert.ToString(row["Input"]);
        other.Output = Convert.ToString(row["Output"]);
        other.Description = Convert.ToString(row["Description"]);

        other.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        other.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        other.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        other.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        other.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        other.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        other.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        other.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        other.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        other.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvOther.Rows.Add(
          other.GUID,
          Convert.ToString(row["ServiceSystem"]),
          other.BizPackageName,
          other.ProgramID,
          SCommon.GetDesignCompleteDisplay(other),
          other.Name,
          other.NameEnglish,
          other.SenderReceiver,
          other.SystemName,
          other.Type,
          other.Input,
          other.Output,
          SCommon.GetDesription(other),
          SCommon.GetRegistered(other),
          SCommon.GetLastModified(other)
        );
      }
    }

    private void DgvOther_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvOther.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region BROp

    private void TbxNameBR_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchBROperation();
    }

    private void TbxNameBROp_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchBROperation();
    }

    private void BtnSearchBROp_Click(object sender, EventArgs e)
    {
      this.SearchBROperation();
    }

    private void SearchBROperation()
    {
      this.DgvBROp.Rows.Clear();

      DataTable dt = this.SDA.SelectBizRuleOperationList(this.TbxNameBR.Text, this.TbxNameBROp.Text);

      foreach (DataRow row in dt.Rows)
      {
        SBizRuleOperation op = new SBizRuleOperation();

        op.GUID = Convert.ToString(row["GUID"]);
        op.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        op.BizPackageName = Convert.ToString(row["BizPackageName"]);
        op.BizRuleName = Convert.ToString(row["BizRuleName"]);
        //op.ProgramID = Convert.ToString(row["ProgramID"]);
        op.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        op.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        op.Name = Convert.ToString(row["Name"]);
        op.NameEnglish = Convert.ToString(row["NameEnglish"]);
        op.Tx = SCommon.GetBoolean(row["Tx"]);
        op.Input = Convert.ToString(row["Input"]);
        op.Output = Convert.ToString(row["Output"]);
        op.Description = Convert.ToString(row["Description"]);

        op.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        op.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        op.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        op.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        op.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        op.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        op.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        op.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        op.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        op.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvBROp.Rows.Add(
          op.GUID,
          op.MicroserviceName,
          op.BizPackageName,
          SCommon.GetName(op.BizRuleName),
          Convert.ToString(row["BizRuleNameEnglish"]),
          SCommon.GetDesignCompleteDisplay(op),
          SCommon.GetName(op),
          op.NameEnglish,
          op.Tx ? "Y" : "N",
          op.Input,
          op.Output,
          Convert.ToInt32(row["CalleeCount"]),
          SCommon.GetDesription(op),
          SCommon.GetRegistered(op),
          SCommon.GetLastModified(op)
        );
      }
    }

    private void DgvBROp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvOther.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region DAOp

    private void TbxNameDA_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchDAOperation();
    }

    private void TbxNameDAOp_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchDAOperation();
    }

    private void BtnSearchDAOp_Click(object sender, EventArgs e)
    {
      this.SearchDAOperation();
    }

    private void SearchDAOperation()
    {
      this.DgvDAOp.Rows.Clear();

      DataTable dt = this.SDA.SelectDataAccessOperationList(this.TbxNameDA.Text, this.TbxNameDAOp.Text);

      foreach (DataRow row in dt.Rows)
      {
        SDataAccessOperation op = new SDataAccessOperation();

        op.GUID = Convert.ToString(row["GUID"]);
        op.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        op.BizPackageName = Convert.ToString(row["BizPackageName"]);
        op.DataAccessName = Convert.ToString(row["DataAccessName"]);
        op.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        op.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        op.Name = Convert.ToString(row["Name"]);
        op.NameEnglish = Convert.ToString(row["NameEnglish"]);
        op.Input = Convert.ToString(row["Input"]);
        op.Output = Convert.ToString(row["Output"]);
        op.Description = Convert.ToString(row["Description"]);
        op.SQL = Convert.ToString(row["SQL"]);

        op.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        op.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        op.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        op.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        op.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        op.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        op.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        op.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        op.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        op.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvDAOp.Rows.Add(
          op.GUID,
          op.MicroserviceName,
          op.BizPackageName,
          SCommon.GetName(op.DataAccessName),
          Convert.ToString(row["DataAccessNameEnglish"]),
          SCommon.GetDesignCompleteDisplay(op),
          SCommon.GetName(op),
          op.NameEnglish,
          op.Input,
          op.Output,
          SCommon.GetDesription(op),
          string.IsNullOrEmpty(op.SQL) ? string.Empty : "Y",
          SCommon.GetRegistered(op),
          SCommon.GetLastModified(op)
        );
      }
    }

    private void DgvDAOp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvDAOp.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion


    #region Dto

    private void TbxNameDto_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchDtoAttr();
    }

    private void TbxNameDtoAttr_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchDtoAttr();
    }

    private void BtnSearchDtoAttr_Click(object sender, EventArgs e)
    {
      this.SearchDtoAttr();
    }

    private void SearchDtoAttr()
    {
      this.DgvDto.Rows.Clear();

      DataTable dt = this.SDA.SelectDtoAttributeList(this.TbxNameDto.Text, this.TbxNameDtoAttr.Text);

      foreach (DataRow row in dt.Rows)
      {
        SDtoAttribute attr = new SDtoAttribute();
        attr.GUID = Convert.ToString(row["GUID"]);
        attr.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        attr.BizPackageName = Convert.ToString(row["BizPackageName"]);
        attr.DtoName = Convert.ToString(row["DtoName"]);
        attr.DataType = Convert.ToString(row["DataType"]);
        attr.Name = Convert.ToString(row["Name"]);
        attr.NameEnglish = Convert.ToString(row["NameEnglish"]);
        attr.Description = Convert.ToString(row["Description"]);

        this.DgvDto.Rows.Add(
          attr.GUID,
          attr.MicroserviceName,
          attr.BizPackageName,
          SCommon.GetName(attr.DtoName),
          Convert.ToString(row["DtoNameEnglish"]),
          attr.DataType,
          SCommon.GetName(attr),
          attr.NameEnglish,
          SCommon.GetDesription(attr)
        );
      }
    }

    private void DgvDto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvDto.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region Entity

    private void TbxNameEntity_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchEntityAttr();
    }

    private void TbxNameEntityAttr_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchEntityAttr();
    }

    private void BtnSearchEntityAttr_Click(object sender, EventArgs e)
    {
      this.SearchEntityAttr();
    }

    private void SearchEntityAttr()
    {
      this.DgvEntity.Rows.Clear();

      DataTable dt = this.SDA.SelectEntityAttributeList(this.TbxNameDto.Text, this.TbxNameDtoAttr.Text);

      foreach (DataRow row in dt.Rows)
      {
        SEntityAttribute attr = new SEntityAttribute();
        attr.GUID = Convert.ToString(row["GUID"]);
        attr.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        attr.BizPackageName = Convert.ToString(row["BizPackageName"]);
        attr.EntityName = Convert.ToString(row["EntityName"]);
        attr.DataType = Convert.ToString(row["DataType"]);
        attr.Name = Convert.ToString(row["Name"]);
        attr.NameEnglish = Convert.ToString(row["NameEnglish"]);
        attr.DBDataType = Convert.ToString(row["DBDataType"]);
        attr.DBColumn = Convert.ToString(row["DBColumn"]);
        attr.Description = Convert.ToString(row["Description"]);

        this.DgvEntity.Rows.Add(
          attr.GUID,
          attr.MicroserviceName,
          attr.BizPackageName,
          SCommon.GetName(attr.EntityName),
          Convert.ToString(row["EntityNameEnglish"]),
          Convert.ToString(row["EntityTableName"]),
          attr.DataType,
          SCommon.GetName(attr),
          attr.NameEnglish,
          attr.DBDataType,
          attr.DBColumn,
          SCommon.GetDesription(attr)
        );
      }
    }

    private void DgvEntity_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvEntity.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region UI

    private void TbxNameUI_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchUI();
    }

    private void BtnSearchUI_Click(object sender, EventArgs e)
    {
      this.SearchUI();
    }

    private void SearchUI()
    {
      this.DgvUI.Rows.Clear();

      DataTable dt = this.SDA.SelectUIList(this.TbxNameUI.Text);

      foreach (DataRow row in dt.Rows)
      {
        SUI ui = new SUI();
        ui.GUID = Convert.ToString(row["GUID"]);
        ui.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        ui.BizPackageName = Convert.ToString(row["BizPackageName"]);
        ui.ProgramID = Convert.ToString(row["ProgramID"]);
        ui.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        ui.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        ui.Name = Convert.ToString(row["Name"]);
        ui.UIType = Convert.ToString(row["UIType"]);
        ui.NameEnglish = Convert.ToString(row["NameEnglish"]);
        ui.Description = Convert.ToString(row["Description"]);

        ui.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        ui.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        ui.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        ui.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        ui.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        ui.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        ui.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        ui.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        ui.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        ui.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvUI.Rows.Add(
          ui.GUID,
          ui.MicroserviceName,
          ui.BizPackageName,
          ui.ProgramID,
          SCommon.GetDesignCompleteDisplay(ui),
          SCommon.GetName(ui),
          ui.UIType,
          ui.NameEnglish,
          Convert.ToInt32(row["EventCount"]),
          SCommon.GetDesription(ui),
          SCommon.GetRegistered(ui),
          SCommon.GetLastModified(ui)
        );
      }
    }

    private void DgvUI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvUI.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    #region Batch

    private void TbxNameJob_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchJob();
    }

    private void BtnSearchJob_Click(object sender, EventArgs e)
    {
      this.SearchJob();
    }

    private void SearchJob()
    {
      this.DgvJob.Rows.Clear();

      DataTable dt = this.SDA.SelectJobList(this.TbxNameJob.Text);

      foreach (DataRow row in dt.Rows)
      {
        SJob job = new SJob();
        job.GUID = Convert.ToString(row["GUID"]);
        job.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        job.BizPackageName = Convert.ToString(row["BizPackageName"]);
        job.ProgramID = Convert.ToString(row["ProgramID"]);
        job.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        job.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        job.Name = Convert.ToString(row["Name"]);
        job.NameEnglish = Convert.ToString(row["NameEnglish"]);
        job.Schedule = Convert.ToString(row["Schedule"]);
        job.Start = Convert.ToString(row["Start"]);
        job.Description = Convert.ToString(row["Description"]);

        job.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        job.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        job.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        job.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        job.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        job.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        job.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        job.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        job.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        job.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        this.DgvJob.Rows.Add(
          job.GUID,
          job.MicroserviceName,
          job.BizPackageName,
          job.ProgramID,
          SCommon.GetDesignCompleteDisplay(job),
          SCommon.GetName(job),
          job.NameEnglish,
          job.Schedule,
          job.Start,
          Convert.ToInt32(row["StepCount"]),
          SCommon.GetDesription(job),
          SCommon.GetRegistered(job),
          SCommon.GetLastModified(job)
        );
      }
    }

    private void DgvJob_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvJob.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion


    #region Consumer/Provider

    private void SetConsumerProviderSynch(string parentNodeText, TreeNodeCollection nodeList)
    {
      foreach (TreeNode node in nodeList)
      {
        string fullPath = fullPath = parentNodeText.Length > 0 ? parentNodeText + " > " + node.Text : node.Text;

        if (node.Tag is SAPI || node.Tag is SPublisher || node.Tag is SOther || node.Tag is SBizRuleOperation || node.Tag is SDataAccessOperation)
        {
          SComponent providerComponent = node.Tag as SComponent;

          List<SCaller> consumerList = this.SBR.GetConsumerList(providerComponent);

          foreach(SCaller consumer in consumerList)
          {
            this.DgvConsumerProvider.Rows.Add(
              consumer.GUID,
              consumer.ComponentType.ToString(),
              consumer.FullName,
              "Move",

              providerComponent.GUID,
              providerComponent.GetType().ToString().Replace("SDM.Component.S", ""),
              this.SBR.GetComponentFullPath(providerComponent), 
              "Move"
            );
          }
        }

        this.SetConsumerProviderSynch(fullPath, node.Nodes);
      }
    }

    private void DgvConsumerProviderSynch_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
      {
        //Consumer 3, //Provider 7
        if (e.ColumnIndex == 3 && this.DgvConsumerProvider.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
          this.MoveComponent(this.DgvConsumerProvider.SelectedRows[0].Cells[0].Value.ToString());
        else if (e.ColumnIndex == 7 && this.DgvConsumerProvider.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
          this.MoveComponent(this.DgvConsumerProvider.SelectedRows[0].Cells[4].Value.ToString());
      }
    }

    #endregion

    #region Last modified

    private void BtnSearchLMD_Click(object sender, EventArgs e)
    {
      int dateFromInt = Convert.ToInt32(this.DtpFrom.Value.ToString("yyyyMMdd"));
      int dateToInt = Convert.ToInt32(this.DtpTo.Value.ToString("yyyyMMdd"));

      this.DgvLMD.Rows.Clear();
      this.SetLastModified(string.Empty, this.TrvModel.Nodes, dateFromInt, dateToInt);
    }

    private void SetLastModified(string parentNodeText, TreeNodeCollection nodeList, int dateFromInt, int dateToInt)
    {
      foreach (TreeNode node in nodeList)
      {
        string fullPath = fullPath = parentNodeText.Length > 0 ? parentNodeText + " > " + node.Text : node.Text;

        if (node.Tag is SComponent)
        {
          SComponent component = node.Tag as SComponent;

          if(component.LastModifiedDate != null && component.LastModifiedDate.Length > 0)
          {
            int lmdInt = Convert.ToInt32(component.LastModifiedDate.Replace(".", ""));

            if (dateFromInt <= lmdInt && lmdInt <= dateToInt)
            {
              this.DgvLMD.Rows.Add(
                component.GUID,
                component.LastModifiedDate,
                component.GetType().ToString().Replace("SDM.Component.S", ""),
                fullPath,
                SCommon.GetRegistered(component),
                SCommon.GetLastModified(component)
              );
            }
          }
        }

        this.SetLastModified(fullPath, node.Nodes, dateFromInt, dateToInt);
      }
    }

    private void DgvLMD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.ExternalLinked)
        this.MoveComponent(this.DgvLMD.SelectedRows[0].Cells[0].Value.ToString());
    }

    #endregion

    private void ExportExcel(string csvString)
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.Filter = "csv files (*.csv)|*.csv";
        saveFileDialog.RestoreDirectory = false;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
          if (saveFileDialog.FileName.Length > 0)
          {
            File.WriteAllText(saveFileDialog.FileName, csvString, Encoding.UTF8);
            SCommon.OpenDocument(saveFileDialog.FileName);
          }
        }
      }
    }

    private void BtnExternalLink_Click(object sender, EventArgs e)
    {
      FormDashboard formDS = new FormDashboard();
      formDS.MoveComponent += MoveComponent;
      formDS.TrvModel = this.TrvModel;
      formDS.ExternalLinked = true;
      formDS.FormBorderStyle = FormBorderStyle.Sizable;
      formDS.Show();
    }

    private void BtnMoveRight_Click(object sender, EventArgs e)
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form.GetType() == typeof(FormMain))
        {
          this.Location = new Point(form.Location.X + form.Width - 15, form.Location.Y);
          this.Height = form.Height;
        }
      }
    }


    private void BtnExcelMyComponent_Click(object sender, EventArgs e)
    {
      string csvString = "Type,Fullpath,Registered,Last Modified" + Environment.NewLine;

      foreach (DataGridViewRow row in this.DgvMyList.Rows)
      {
        csvString +=
          string.Format("{0},{1},{2},{3}{4}",
            row.Cells[1].Value,
            row.Cells[2].Value,
            row.Cells[3].Value,
            row.Cells[4].Value,
            Environment.NewLine
          );
      }

      this.ExportExcel(csvString);
    }

    #region Chart

    private void BtnSearchChart_Click(object sender, EventArgs e)
    {
      this.SetChart();
    }

    private void SetChart()
    {
      Series sAPI = this.ChartLine.Series[0];
      Series sPub = this.ChartLine.Series[1];
      Series sSub = this.ChartLine.Series[2];
      Series sOther = this.ChartLine.Series[3];

      sAPI.Points.Clear();
      sPub.Points.Clear();
      sSub.Points.Clear();
      sOther.Points.Clear();

      //string msGUID = this.CmbMS.SelectedItem is SMicroservice ? (this.CmbMS.SelectedItem as SMicroservice).GUID : string.Empty;
      DataTable dt = this.SDA.SelectComponentCountListByDate(this.DtpFromChart.Value.ToString("yyyy.MM.dd"), this.DtpToChart.Value.ToString("yyyy.MM.dd"));

      foreach (DataRow row in dt.Rows)
      {
        string type = Convert.ToString(row["Type"]);
        string date = Convert.ToString(row["LastModifiedDate"]);
        int cnt = Convert.ToInt32(row["Cnt"]);

        DataPoint point = new DataPoint();
        point.SetValueXY(date, cnt);

        if(SComponentType.API.ToString() == type)
          sAPI.Points.Add(point);
        else if (SComponentType.Publisher.ToString() == type)
          sPub.Points.Add(point);
        else if (SComponentType.Subscriber.ToString() == type)
          sSub.Points.Add(point);
        else if (SComponentType.Other.ToString() == type)
          sOther.Points.Add(point);
      }
    }

    #endregion
  }
}
