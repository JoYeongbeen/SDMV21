using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSOther : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    private List<SCaller> CallerList;
    public SOther SOther;
    private SBR SBR;
    private SDA SDA;

    public FormSOther()
    {
      InitializeComponent();
    }

    private void FormSOther_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
      this.SetSystemCombo();
      this.SelectComponent();
      this.GetConsumerList();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.F12)
        this.UpdateComponent();
      else if(keyData == (Keys.Control | Keys.S))
        this.UpdateComponent();
      else if (keyData == (Keys.Control | Keys.Enter))
        this.UpdateComponent();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_OTHER);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SOther);
    }

    private void SetSystemCombo()
    {
      List<SInternalSystem> siList = this.SDA.SelectInternalSystemList(true, false);
      siList.Insert(0, new SInternalSystem(string.Empty, string.Empty));
      this.CmbSIList.DataSource = siList;
      this.CmbSIList.ValueMember = "GUID";
      this.CmbSIList.DisplayMember = "Name";

      List<SExternalSystem> seList = this.SDA.SelectExternalSystemList(true, false);
      seList.Insert(0, new SExternalSystem(string.Empty, string.Empty));
      this.CmbSEList.DataSource = seList;
      this.CmbSEList.ValueMember = "GUID";
      this.CmbSEList.DisplayMember = "Name";
    }

    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SOther);
      this.TbxID.Text = this.SOther.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SOther.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SOther.DesignCompleteDetail;
      this.TbxName.Text = this.SOther.Name;
      this.TbxProgramName.Text = this.SOther.NameEnglish;

      //Sender/Receiver
      this.RbnOtherSend.Checked = this.SOther.SenderReceiver == this.RbnOtherSend.Text;
      this.RbnOtherReceive.Checked = this.SOther.SenderReceiver == this.RbnOtherReceive.Text;

      if(this.SOther.SystemType == SComponentType.InternalSystem)
        this.CmbSIList.SelectedValue = this.SOther.SystemGUID;
      else if(this.SOther.SystemType == SComponentType.ExternalSystem)
        this.CmbSEList.SelectedValue = this.SOther.SystemGUID;

      //Type
      this.RbnOtherTypeEJB.Checked = this.SOther.Type == this.RbnOtherTypeEJB.Text;
      this.RbnOtherTypeEAI.Checked = this.SOther.Type == this.RbnOtherTypeEAI.Text;
      this.RbnOtherTypeDB2DB.Checked = this.SOther.Type == this.RbnOtherTypeDB2DB.Text;
      this.RbnOtherTypeFTP.Checked = this.SOther.Type == this.RbnOtherTypeFTP.Text;
      this.RbnOtherTypeSMTP.Checked = this.SOther.Type == this.RbnOtherTypeSMTP.Text;
      this.RbnOtherTypeSocket.Checked = this.SOther.Type == this.RbnOtherTypeSocket.Text;
      this.RbnOtherTypeOthers.Checked = this.SOther.Type == this.RbnOtherTypeOthers.Text;
      this.TbxOtherTypeText.Text = this.SOther.TypeText;

      this.TbxInput.Text = this.SOther.Input;
      this.TbxOutput.Text = this.SOther.Output;

      this.TbxDescription.Text = this.SOther.Description;
      this.TbxSpecFile.Text = this.SOther.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SOther);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SOther);
    }

    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SOther);

      this.BtnConsumer.Enabled = this.CallerList.Count > 0;
      this.BtnConsumer.Text = this.CallerList.Count.ToString();
    }

    private void BtnConsumer_Click(object sender, EventArgs e)
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form.GetType() == typeof(FormCallerCallee))
        {
          FormCallerCallee formCC = form as FormCallerCallee;
          formCC.SetData(this.TbxParentPath.Text, this.CallerList);
          formCC.Activate();

          return;
        }
      }

      FormCallerCallee formCCNew = new FormCallerCallee();
      formCCNew.MoveComponentFromCaller += MoveComponentFromCaller;
      formCCNew.SetData(this.TbxParentPath.Text, this.CallerList);
      formCCNew.Show();
    }

    private void MoveComponentFromCaller(string guid)
    {
      this.MoveComponent(guid);
    }


    #region Other send/recieve

    private void RbnOtherSend_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSenderReceiver();
    }

    private void RbnOtherReceive_CheckedChanged(object sender, EventArgs e)
    {
      this.SetSenderReceiver();
    }

    private void SetSenderReceiver()
    {
      if (this.RbnOtherSend.Checked)
        this.LblOtherToFrom.Text = "to";
      else
        this.LblOtherToFrom.Text = "from";
    }

    #endregion

    #region Others Communication Type

    private void RbnOtherTypeEJB_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeEAI_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeFTP_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeSMTP_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeSocket_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeDB2DB_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void RbnOtherTypeOthers_CheckedChanged(object sender, EventArgs e)
    {
      this.DisplayOtherTypeTextbox();
    }

    private void DisplayOtherTypeTextbox()
    {
      if (this.RbnOtherTypeOthers.Checked)
      {
        this.TbxOtherTypeText.Enabled = true;
        this.TbxOtherTypeText.Focus();
      }
      else
      {
        this.TbxOtherTypeText.Enabled = false;
        this.TbxOtherTypeText.Text = string.Empty;
      }
    }

    #endregion

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblInput.Font = new Font(this.LblInput.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblOutput.Font = new Font(this.LblOutput.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblProgramName.Font = new Font(this.LblProgramName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Name");
        this.TbxName.Focus();
        return;
      }

      if (this.RbnOtherTypeEJB.Checked == false && this.RbnOtherTypeEAI.Checked == false &&
        this.RbnOtherTypeDB2DB.Checked == false && this.RbnOtherTypeFTP.Checked == false &&
        this.RbnOtherTypeSMTP.Checked == false && this.RbnOtherTypeSocket.Checked == false && this.RbnOtherTypeOthers.Checked == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Type");
        return;
      }

      if (this.RbnOtherTypeOthers.Checked && this.TbxOtherTypeText.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Others specific type");
        this.TbxOtherTypeText.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        if (this.TbxInput.Text.Length == 0 && this.TbxOutput.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Input 및 Output");
          this.TbxInput.Focus();
          return;
        }
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxProgramName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Program Name");
          this.TbxProgramName.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SOther), "Other", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SOther.ProgramID = this.TbxID.Text.Trim();
      this.SOther.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SOther.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SOther.Name = this.TbxName.Text.Trim();
      this.SOther.NameEnglish = this.TbxProgramName.Text;

      //Sender/Receiver
      if (this.RbnOtherSend.Checked)
        this.SOther.SenderReceiver = this.RbnOtherSend.Text;
      else if (this.RbnOtherReceive.Checked)
        this.SOther.SenderReceiver = this.RbnOtherReceive.Text;

      if (this.CmbSIList.SelectedValue != null && this.CmbSIList.SelectedValue.ToString().Length > 0)
      {
        this.SOther.SystemType = SComponentType.InternalSystem;
        this.SOther.SystemGUID = this.CmbSIList.SelectedValue.ToString();
      }
      else if (this.CmbSEList.SelectedValue != null && this.CmbSEList.SelectedValue.ToString().Length > 0)
      {
        this.SOther.SystemType = SComponentType.ExternalSystem;
        this.SOther.SystemGUID = this.CmbSEList.SelectedValue.ToString();
      }
      else
      {
        this.SOther.SystemGUID = string.Empty;
      }

      //Type
      if (this.RbnOtherTypeEJB.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeEJB.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeEAI.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeEAI.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeDB2DB.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeDB2DB.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeFTP.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeFTP.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeSMTP.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeSMTP.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeSocket.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeSocket.Text;
        this.SOther.TypeText = string.Empty;
      }
      else if (this.RbnOtherTypeOthers.Checked)
      {
        this.SOther.Type = this.RbnOtherTypeOthers.Text;
        this.SOther.TypeText = this.TbxOtherTypeText.Text;
      }

      this.SOther.Input = this.TbxInput.Text;
      this.SOther.Output = this.TbxOutput.Text;
      this.SOther.Description = this.TbxDescription.Text;
      this.SOther.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SOther, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SOther);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SOther);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SOther);

      this.SDA.UpdateOther(this.SOther);
      this.ComponentChanged(this.SOther);

      SMessageBox.ShowInformation(SMessage.SAVED);
    }


    private void BtnSpecFile_Click(object sender, EventArgs e)
    {
      this.TbxSpecFile.Text = SCommon.GetSpecFilePath();
    }

    private void BtnViewSpec_Click(object sender, EventArgs e)
    {
      SCommon.OpenDocument(this.TbxSpecFile.Text);
    }

    private void BtnSynchTree_Click(object sender, EventArgs e)
    {
      this.MoveComponent(this.SOther.GUID);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SOther);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SOther);
        this.SDA.UpdateOtherLastModifiedTemp(this.SOther);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_OTHER);
    }
  }
}
