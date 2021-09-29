using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSDataAccessOperation : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    private List<SCaller> CallerList;
    public SDataAccessOperation SDataAccessOperation;
    private SBR SBR;
    private SDA SDA;

    public FormSDataAccessOperation()
    {
      InitializeComponent();
    }

    private void FormSDataAccessOperation_Load(object sender, EventArgs e)
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
      else if (keyData == (Keys.Control | Keys.S))
        this.UpdateComponent();
      else if (keyData == (Keys.Control | Keys.Enter))
        this.UpdateComponent();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetControl()
    {
      this.CkbMonospacedFont.Checked = SCommon.LoggedInUser.MonospacedFontDAOperationDesc;
      SCommon.SetMonospacedFont(this.CkbMonospacedFont.Checked, this.TbxSQL);

      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_DA_OP);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SDataAccessOperation);

      this.LblSampleDAOperationName.Text = SCommon.SProject.SampleDAOpName;
      this.LblSampleMethodName.Text = SCommon.SProject.SampleDAOpMethod;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDataAccessOperation);
      this.TbxID.Text = this.SDataAccessOperation.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SDataAccessOperation.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SDataAccessOperation.DesignCompleteDetail;
      this.TbxName.Text = this.SDataAccessOperation.Name;
      this.TbxMethodName.Text = this.SDataAccessOperation.NameEnglish;
      SCommon.SetTbxInputOutput(this.TbxInput, this.SDataAccessOperation.InputGUID, this.SDataAccessOperation.Input, this.SBR.GetComponentFullPath(SComponentType.Entity, this.SDataAccessOperation.InputGUID));
      SCommon.SetTbxInputOutput(this.TbxOutput, this.SDataAccessOperation.OutputGUID, this.SDataAccessOperation.Output, this.SBR.GetComponentFullPath(SComponentType.Entity, this.SDataAccessOperation.OutputGUID));
      this.TbxDescription.Text = this.SDataAccessOperation.Description;
      this.TbxSQL.Text = this.SDataAccessOperation.SQL;
      this.TbxSpecFile.Text = this.SDataAccessOperation.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDataAccessOperation);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccessOperation);
    }


    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SDataAccessOperation);

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
      this.LblParameter.Font = new Font(this.LblParameter.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblReturn.Font = new Font(this.LblReturn.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblMethodName.Font = new Font(this.LblMethodName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblDesc.Font = new Font(this.LblDesc.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblSQL.Font = new Font(this.LblSQL.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
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
        if (SCommon.DraggedNodeFromTree.Tag is SEntity)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void TbxInput_DragDrop(object sender, DragEventArgs e)
    {
      SEntity entity = SCommon.DraggedNodeFromTree.Tag as SEntity;

      if (entity.MicroserviceGUID != this.SDataAccessOperation.MicroserviceGUID)
      {
        SMessageBox.ShowWarning(SMessage.NO_OTHER_MS_ENTITY);
        return;
      }

      SCommon.SetTbxInputOutput(this.TbxInput, entity.GUID, "", this.SBR.GetComponentFullPath(entity));
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
        if (SCommon.DraggedNodeFromTree.Tag is SEntity)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void TbxOutput_DragDrop(object sender, DragEventArgs e)
    {
      SEntity entity = SCommon.DraggedNodeFromTree.Tag as SEntity;

      if (entity.MicroserviceGUID != this.SDataAccessOperation.MicroserviceGUID)
      {
        SMessageBox.ShowWarning(SMessage.NO_OTHER_MS_ENTITY);
        return;
      }

      SCommon.SetTbxInputOutput(this.TbxOutput, entity.GUID, "", this.SBR.GetComponentFullPath(entity));
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


    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.EndsWith("등록") == false && this.TbxName.Text.EndsWith("수정") == false && this.TbxName.Text.EndsWith("저장") == false && this.TbxName.Text.EndsWith("삭제") == false && this.TbxName.Text.EndsWith("조회") == false)
      {
        SMessageBox.ShowWarning(SMessage.NO_DA_VERB);
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        if (this.TbxInput.Text.Length == 0 && this.TbxOutput.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Input 또는 Output");
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

        if (this.TbxDescription.Text.Length == 0 && this.TbxSQL.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "상세설계 내용(Description) 또는 SQL");
          this.TbxSQL.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SDataAccessOperation), "DA Operation", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SDataAccessOperation.ProgramID = this.TbxID.Text.Trim();
      this.SDataAccessOperation.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SDataAccessOperation.DesignCompleteDetail = this.CkbDesignDetail.Checked;

      this.SDataAccessOperation.Name = this.TbxName.Text.Trim();
      this.SDataAccessOperation.NameEnglish = this.TbxMethodName.Text;

      this.SDataAccessOperation.InputGUID = Convert.ToString(this.TbxInput.Tag);
      this.SDataAccessOperation.Input = this.TbxInput.Text.Trim();

      this.SDataAccessOperation.OutputGUID = Convert.ToString(this.TbxOutput.Tag);
      this.SDataAccessOperation.Output = this.TbxOutput.Text.Trim();

      this.SDataAccessOperation.Description = this.TbxDescription.Text;
      this.SDataAccessOperation.SQL = this.TbxSQL.Text;
      this.SDataAccessOperation.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SDataAccessOperation, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDataAccessOperation);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDataAccessOperation);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccessOperation);

      this.SDA.UpdateDataAccessOperation(this.SDataAccessOperation);
      this.ComponentChanged(this.SDataAccessOperation);

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

    private void CkbMonospacedFont_CheckedChanged(object sender, EventArgs e)
    {
      SCommon.LoggedInUser.MonospacedFontDAOperationDesc = this.CkbMonospacedFont.Checked;
      this.SDA.UpdateUserMonospacedFontDAOperationDesc(SCommon.LoggedInUser);

      SCommon.SetMonospacedFont(this.CkbMonospacedFont.Checked, this.TbxSQL);
    }

    private void BtnSynchTree_Click(object sender, EventArgs e)
    {
      this.MoveComponent(this.SDataAccessOperation.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0 || SDictionary.DicDAVerb.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      this.TbxMethodName.Text = this.SBR.GetEngOperationName(this.TbxName.Text, true);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SDataAccessOperation);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccessOperation);
        this.SDA.UpdateDataAccessOperationLastModifiedTemp(this.SDataAccessOperation);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_DA_OP);
    }
  }
}