using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSSubscriber : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SSubscriber SSubscriber;
    private SBR SBR;
    private SDA SDA;

    public FormSSubscriber()
    {
      InitializeComponent();
    }

    private void FormSSubscriber_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
      this.SetTopicCombo();
      this.SelectComponent();
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
      this.toolTip1.SetToolTip(this.BtnSearchProvider, SMessage.HELP_PROVIDER_SUB);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SSubscriber);

      this.LblSampleName.Text = SCommon.SProject.SampleSubName;
      this.LblSampleMethodName.Text = SCommon.SProject.SampleSubMethod;
      this.LblSampleInput.Text = SCommon.SProject.SampleSubInput;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SSubscriber);
      this.TbxID.Text = this.SSubscriber.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SSubscriber.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SSubscriber.DesignCompleteDetail;
      this.TbxName.Text = this.SSubscriber.Name;
      this.TbxMethodName.Text = this.SSubscriber.NameEnglish;
      SCommon.SetTbxInputOutput(this.TbxInput, this.SSubscriber.InputGUID, this.SSubscriber.Input, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SSubscriber.InputGUID));
      this.CmbTopic.Text = this.SSubscriber.Topic;
      this.TbxBROperation.Tag = this.SSubscriber.CalleeBROperationGUID;
      this.TbxBROperation.Text = this.SBR.GetComponentFullPath(SComponentType.BizRuleOperation, this.SSubscriber.CalleeBROperationGUID);
      this.TbxDescription.Text = this.SSubscriber.Description;
      this.TbxSpecFile.Text = this.SSubscriber.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SSubscriber);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SSubscriber);
    }

    #region Callee BR Operation

    private void TbxBROperation_DragEnter(object sender, DragEventArgs e)
    {
      if (SCommon.DraggedNodeFromTree == null)
      {
        e.Effect = DragDropEffects.None;
      }
      else
      {
        if (SCommon.DraggedNodeFromTree.Tag is SBizRuleOperation)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void TbxBROperation_DragDrop(object sender, DragEventArgs e)
    {
      SBizRuleOperation calleeBROp = SCommon.DraggedNodeFromTree.Tag as SBizRuleOperation;

      if (calleeBROp.BizPackageGUID != this.SSubscriber.BizPackageGUID)
      {
        SMessageBox.ShowWarning(SMessage.NO_CALLEE_OTHER_BP_BR_OP);
        return;
      }

      this.TbxBROperation.Tag = calleeBROp.GUID;
      this.TbxBROperation.Text = this.SBR.GetComponentFullPath(calleeBROp);
    }

    private void TbxBROperation_DoubleClick(object sender, EventArgs e)
    {
      if (this.TbxBROperation.Tag != null)
        this.MoveComponent(this.TbxBROperation.Tag.ToString());
    }

    private void BtnDeleteBROperation_Click(object sender, EventArgs e)
    {
      this.TbxBROperation.Tag = null;
      this.TbxBROperation.Text = string.Empty;
    }

    #endregion


    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblInput.Font = new Font(this.LblInput.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);

      if (string.IsNullOrEmpty(this.SSubscriber.MicroserviceGUID) == false)
        this.LblBROperation.Font = new Font(this.LblBROperation.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
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

      if (dto.MicroserviceGUID != this.SSubscriber.MicroserviceGUID)
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
      if (this.TbxName.Text.EndsWith("구독") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Subscriber Name", "Xxx구독");
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

        if (string.IsNullOrEmpty(this.SSubscriber.MicroserviceGUID) == false)
        {
          if (this.TbxBROperation.Tag == null || (this.TbxBROperation.Tag != null && this.TbxBROperation.Tag.ToString().Length == 0))
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "BR Operation");
            this.TbxBROperation.Focus();
            return;
          }
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

      this.SSubscriber.ProgramID = this.TbxID.Text.Trim();
      this.SSubscriber.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SSubscriber.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SSubscriber.Name = this.TbxName.Text.Trim();
      this.SSubscriber.NameEnglish = this.TbxMethodName.Text;

      this.SSubscriber.InputGUID = Convert.ToString(this.TbxInput.Tag);
      this.SSubscriber.Input = this.TbxInput.Text.Trim();

      this.SSubscriber.Topic = this.CmbTopic.Text;
      this.SSubscriber.CalleeBROperationGUID = this.TbxBROperation.Tag != null ? this.TbxBROperation.Tag.ToString() : string.Empty;
      this.SSubscriber.Description = this.TbxDescription.Text;
      this.SSubscriber.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SSubscriber, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SSubscriber);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SSubscriber);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SSubscriber);

      this.SDA.UpdateSubscriber(this.SSubscriber);
      this.ComponentChanged(this.SSubscriber);

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
      this.MoveComponent(this.SSubscriber.GUID);
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


    private void BtnSearchProvider_Click(object sender, EventArgs e)
    {
      SCaller provider = this.SBR.RetrieveProvider(this.SSubscriber);

      if(provider == null)
      {
        SMessageBox.ShowWarning(SMessage.NO_PUBLISHER);
        return;
      }
      else
      {
        DialogResult result = SMessageBox.ShowConfirm(SMessage.MOVE_PUBLISHER, provider.FullName);

        if (result == DialogResult.Yes)
          this.MoveComponent(provider.GUID);
      }
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SSubscriber);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SSubscriber);
        this.SDA.UpdateSubscriberLastModifiedTemp(this.SSubscriber);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    
  }
}
