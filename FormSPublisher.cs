using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSPublisher : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    private List<SCaller> CallerList;
    public SPublisher SPublisher;
    private SBR SBR;
    private SDA SDA;

    public FormSPublisher()
    {
      InitializeComponent();
    }

    private void FormSPublisher_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
      this.SetTopicCombo();
      this.SelectComponent();
      this.GetConsumerList();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.F12)
        this.UpdateComponent();
      else if (keyData == (Keys.Control | Keys.S))
        this.UpdateComponent();
      else if (keyData == (Keys.Control | Keys.Enter))
        this.UpdateComponent();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_API);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SPublisher);

      this.LblSampleName.Text = SCommon.SProject.SamplePubName;
      this.LblSampleMethodName.Text = SCommon.SProject.SamplePubMethod;
      this.LblSampleInput.Text = SCommon.SProject.SamplePubInput;
      this.LblSampleTopic.Text = SCommon.SProject.SamplePubTopic;

      if (SCommon.SProject.Dictionary)
      {
        this.BtnTranslate.Visible = true;
        this.LblSampleMethodName.Location = new Point(this.LblSampleMethodName.Location.X, this.LblSampleMethodName.Location.Y);
      }
      else
      {
        this.BtnTranslate.Visible = false;
        this.LblSampleMethodName.Location = new Point(this.LblSampleMethodName.Location.X - 24, this.LblSampleMethodName.Location.Y);
      }
    }

    private void SetTopicCombo()
    {
      List<string> topicList = this.SDA.SelectTopicList();

      foreach (string topic in topicList)
        this.CmbTopic.Items.Add(topic);
    }

    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SPublisher);
      this.TbxID.Text = this.SPublisher.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SPublisher.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SPublisher.DesignCompleteDetail;
      this.TbxName.Text = this.SPublisher.Name;
      this.TbxMethodName.Text = this.SPublisher.NameEnglish;
      SCommon.SetTbxInputOutput(this.TbxInput, this.SPublisher.InputGUID, this.SPublisher.Input, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SPublisher.InputGUID));
      this.CmbTopic.Text = this.SPublisher.Topic;
      this.TbxDescription.Text = this.SPublisher.Description;
      this.TbxSpecFile.Text = this.SPublisher.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SPublisher);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SPublisher);
    }


    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SPublisher);

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

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblInput.Font = new Font(this.LblInput.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblMethodName.Font = new Font(this.LblMethodName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    #region Input

    private void TbxInput_DragEnter(object sender, DragEventArgs e)
    {
      if (SCommon.DraggedNodeFromTree == null)
      {
        e.Effect = DragDropEffects.None;
      }
      else
      {
        if (SCommon.DraggedNodeFromTree.Tag is SDto)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void TbxInput_DragDrop(object sender, DragEventArgs e)
    {
      SDto dto = SCommon.DraggedNodeFromTree.Tag as SDto;

      if (dto.MicroserviceGUID != this.SPublisher.MicroserviceGUID)
      {
        SMessageBox.ShowWarning(SMessage.NO_OTHER_MS_DTO);
        return;
      }

      SCommon.SetTbxInputOutput(this.TbxInput, dto.GUID, "", this.SBR.GetComponentFullPath(dto));
    }

    private void TbxInput_DoubleClick(object sender, EventArgs e)
    {
      if (this.TbxInput.Tag != null)
        this.MoveComponent(this.TbxInput.Tag.ToString());
    }

    private void BtnDeleteInput_Click(object sender, EventArgs e)
    {
      SCommon.SetTbxInputOutput(this.TbxInput, null, "", "");
      this.TbxInput.Focus();
    }

    #endregion

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.EndsWith("발행") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Publisher Name", "Xxx발행");
        this.TbxName.Focus();
        return;
      }

      if (this.CmbTopic.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Topic");
        this.CmbTopic.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        if (this.TbxInput.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Input");
          this.TbxInput.Focus();
          return;
        }
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxMethodName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Method Name");
          this.TbxMethodName.Focus();
          return;
        }

        if (SCommon.IsEnglishOrNumber(this.TbxMethodName.Text) == false)
        {
          SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Method Name");
          this.TbxMethodName.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SPublisher), "Publisher", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SPublisher.ProgramID = this.TbxID.Text.Trim();
      this.SPublisher.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SPublisher.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SPublisher.Name = this.TbxName.Text.Trim();
      this.SPublisher.NameEnglish = this.TbxMethodName.Text;

      this.SPublisher.InputGUID = Convert.ToString(this.TbxInput.Tag);
      this.SPublisher.Input = this.TbxInput.Text.Trim();

      this.SPublisher.Topic = this.CmbTopic.Text;
      this.SPublisher.Description = this.TbxDescription.Text;
      this.SPublisher.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SPublisher, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SPublisher);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SPublisher);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SPublisher);

      this.SDA.UpdatePublisher(this.SPublisher);
      this.ComponentChanged(this.SPublisher);

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
      this.MoveComponent(this.SPublisher.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0 || SDictionary.DicBRVerb.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      this.TbxMethodName.Text = this.SBR.GetEngOperationName(this.TbxName.Text);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SPublisher);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SPublisher);
        this.SDA.UpdatePublisherLastModifiedTemp(this.SPublisher);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_API);
    }
  }
}
