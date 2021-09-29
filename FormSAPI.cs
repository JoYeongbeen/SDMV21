using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSAPI : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SAPI SAPI;
    private List<SCaller> CallerList;
    private SBR SBR;
    private SDA SDA;

    public FormSAPI()
    {
      InitializeComponent();
    }

    private void FormSAPI_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
      this.SelectComponent();
      this.GetConsumerList();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.F12)
        this.UpdateComponent();
      else if(keyData == (Keys.Control | Keys.S))
        this.UpdateComponent();
      else if(keyData == (Keys.Control | Keys.Enter))
        this.UpdateComponent();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_API);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SAPI);

      this.LblSampleName.Text = SCommon.SProject.SampleAPIName;
      this.LblSampleMethodName.Text = SCommon.SProject.SampleAPIMethod;
      this.LblSampleURI.Text = SCommon.SProject.SampleAPIURL;
      this.LblSampleInput.Text = SCommon.SProject.SampleAPIInput;
      this.LblSampleOutput.Text = SCommon.SProject.SampleAPIOutput;

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

    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SAPI);
      this.TbxID.Text = this.SAPI.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SAPI.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SAPI.DesignCompleteDetail;

      this.TbxName.Text = this.SAPI.Name;
      this.TbxMethodName.Text = this.SAPI.NameEnglish;

      if (this.SAPI.HttpMethod == this.RbnGet.Text)
        this.RbnGet.Checked = true;
      else if (this.SAPI.HttpMethod == this.RbnPost.Text)
        this.RbnPost.Checked = true;
      else if (this.SAPI.HttpMethod == this.RbnPut.Text)
        this.RbnPut.Checked = true;
      else if (this.SAPI.HttpMethod == this.RbnDelete.Text)
        this.RbnDelete.Checked = true;

      this.TbxURI.Text = this.SAPI.URI;
      SCommon.SetTbxInputOutput(this.TbxInput, this.SAPI.InputGUID, this.SAPI.Input, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SAPI.InputGUID));
      SCommon.SetTbxInputOutput(this.TbxOutput, this.SAPI.OutputGUID, this.SAPI.Output, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SAPI.OutputGUID));
      this.TbxBROperation.Tag = this.SAPI.CalleeBROperationGUID;
      this.TbxBROperation.Text = this.SBR.GetComponentFullPath(SComponentType.BizRuleOperation, this.SAPI.CalleeBROperationGUID);

      this.TbxDescription.Text = this.SAPI.Description;
      this.TbxSpecFile.Text = this.SAPI.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SAPI);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SAPI);
    }


    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SAPI);
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
      //form.SelectedComponentFullPath = this.TbxParentPath.Text;
      //form.CallerList = this.CallerList;
      formCCNew.MoveComponentFromCaller += MoveComponentFromCaller;
      formCCNew.SetData(this.TbxParentPath.Text, this.CallerList);
      formCCNew.Show();
    }

    private void MoveComponentFromCaller(string guid)
    {
      this.MoveComponent(guid);
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

      if (dto.MicroserviceGUID != this.SAPI.MicroserviceGUID)
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

    #region Output

    private void TbxOutput_DragEnter(object sender, DragEventArgs e)
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

    private void TbxOutput_DragDrop(object sender, DragEventArgs e)
    {
      SDto dto = SCommon.DraggedNodeFromTree.Tag as SDto;

      if (dto.MicroserviceGUID != this.SAPI.MicroserviceGUID)
      {
        SMessageBox.ShowWarning(SMessage.NO_OTHER_MS_DTO);
        return;
      }

      SCommon.SetTbxInputOutput(this.TbxOutput, dto.GUID, "", this.SBR.GetComponentFullPath(dto));
    }

    private void TbxOutput_DoubleClick(object sender, EventArgs e)
    {
      if (this.TbxInput.Tag != null)
        this.MoveComponent(this.TbxOutput.Tag.ToString());
    }

    private void BtnDeleteOutput_Click(object sender, EventArgs e)
    {
      SCommon.SetTbxInputOutput(this.TbxOutput, null, "", "");
      this.TbxOutput.Focus();
    }

    #endregion

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

      if (calleeBROp.BizPackageGUID != this.SAPI.BizPackageGUID)
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
      this.LblOutput.Font = new Font(this.LblOutput.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);

      //상위가 MS인 경우에만 체크
      if (string.IsNullOrEmpty(this.SAPI.MicroserviceGUID) == false)
        this.LblBROperation.Font = new Font(this.LblBROperation.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblMethodName.Font = new Font(this.LblMethodName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblURI.Font = new Font(this.LblURI.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      //SD단계부터 적용되는 기본적인 Validation
      if (this.TbxName.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "API Name");
        this.TbxName.Focus();
        return;
      }

      if (this.RbnGet.Checked == false && this.RbnPost.Checked == false && this.RbnPut.Checked == false && this.RbnDelete.Checked == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "HTTP Method");
        this.RbnGet.Focus();
        return;
      }

      //Validation 기본설계
      if(this.CkbDesignSkeleton.Checked)
      {
        if (this.TbxInput.Text.Length == 0 && this.TbxOutput.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Input 또는 Output");
          this.TbxInput.Focus();
          return;
        }

        //상위가 MS인 경우에만 체크
        if (string.IsNullOrEmpty(this.SAPI.MicroserviceGUID) == false)
        {
          if (this.TbxBROperation.Tag == null || (this.TbxBROperation.Tag != null && this.TbxBROperation.Tag.ToString().Length == 0))
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "BR Operation");
            this.TbxBROperation.Focus();
            return;
          }

          SBizRuleOperation calleeBROp = this.SBR.SelectBizRuleOperation(this.TbxBROperation.Tag.ToString());

          if (calleeBROp != null)
          {
            if (SCommon.GetName(this.SAPI) != SCommon.GetName(calleeBROp))
            {
              SMessageBox.ShowWarning(SMessage.NO_SAME_API_BR_OP);
              this.TbxBROperation.Focus();
              return;
            }
          }
        }
      }

      //Validation 상세설계
      if(this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxMethodName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Method Name");
          this.TbxMethodName.Focus();
          return;
        }

        if(SCommon.IsEnglishOrNumber(this.TbxMethodName.Text) == false)
        {
          SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Method Name");
          this.TbxMethodName.Focus();
          return;
        }

        if (this.TbxURI.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "URI");
          this.TbxURI.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SAPI), "API", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      //Save
      this.SAPI.ProgramID = this.TbxID.Text.Trim();

      this.SAPI.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SAPI.DesignCompleteDetail = this.CkbDesignDetail.Checked;

      this.SAPI.Name = this.TbxName.Text.Trim();
      this.SAPI.NameEnglish = this.TbxMethodName.Text;

      if (this.RbnGet.Checked)
        this.SAPI.HttpMethod = this.RbnGet.Text;
      else if (this.RbnPost.Checked)
        this.SAPI.HttpMethod = this.RbnPost.Text;
      else if (this.RbnPut.Checked)
        this.SAPI.HttpMethod = this.RbnPut.Text;
      else if (this.RbnDelete.Checked)
        this.SAPI.HttpMethod = this.RbnDelete.Text;

      this.SAPI.URI = this.TbxURI.Text;
      
      this.SAPI.InputGUID = Convert.ToString(this.TbxInput.Tag);
      this.SAPI.Input = this.TbxInput.Text.Trim();

      this.SAPI.OutputGUID= Convert.ToString(this.TbxOutput.Tag);
      this.SAPI.Output = this.TbxOutput.Text.Trim();

      this.SAPI.CalleeBROperationGUID = this.TbxBROperation.Tag != null ? this.TbxBROperation.Tag.ToString() : string.Empty;
      this.SAPI.Description = this.TbxDescription.Text;
      this.SAPI.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SAPI, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SAPI);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SAPI);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SAPI);

      this.SDA.UpdateAPI(this.SAPI);
      this.ComponentChanged(this.SAPI);

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
      this.MoveComponent(this.SAPI.GUID);
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
        SCommon.SetLastModifiedTemp(this.SAPI);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SAPI);
        this.SDA.UpdateAPILastModifiedTemp(this.SAPI);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_API);
    }
  }
}
