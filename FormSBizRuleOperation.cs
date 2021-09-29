using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSBizRuleOperation : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    private List<SCaller> CallerList;
    public SBizRuleOperation SBizRuleOperation;
    private SBR SBR;
    private SDA SDA;

    public FormSBizRuleOperation()
    {
      InitializeComponent();
    }

    private void FormSBizRuleOperation_Load(object sender, EventArgs e)
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
      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_BR_OP);

      this.toolTip1.SetToolTip(this.LblCallList, SMessage.LBL_CALL_LIST_BR_OP);
      this.toolTip1.SetToolTip(this.BtnHelpCallList, SMessage.LBL_CALL_LIST_BR_OP);

      this.toolTip1.SetToolTip(this.BtnEditComment, SMessage.BTN_EDIT_COMMENT);
      this.toolTip1.SetToolTip(this.BtnAddComment, SMessage.BTN_ADD_COMMENT);

      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SBizRuleOperation);

      this.LblSampleBROperationName.Text = SCommon.SProject.SampleBROpName;
      this.LblSampleMethodName.Text = SCommon.SProject.SampleBROpMethod;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizRuleOperation);
      this.TbxID.Text = this.SBizRuleOperation.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SBizRuleOperation.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SBizRuleOperation.DesignCompleteDetail;
      this.TbxName.Text = this.SBizRuleOperation.Name;
      this.CkbTransaction.Checked = this.SBizRuleOperation.Tx;
      this.TbxMethodName.Text = this.SBizRuleOperation.NameEnglish;
      SCommon.SetTbxInputOutput(this.TbxInput, this.SBizRuleOperation.InputGUID, this.SBizRuleOperation.Input, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SBizRuleOperation.InputGUID));
      SCommon.SetTbxInputOutput(this.TbxOutput, this.SBizRuleOperation.OutputGUID, this.SBizRuleOperation.Output, this.SBR.GetComponentFullPath(SComponentType.Dto, this.SBizRuleOperation.OutputGUID));
      this.TbxDescription.Text = this.SBizRuleOperation.Description;
      this.TbxSpecFile.Text = this.SBizRuleOperation.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizRuleOperation);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRuleOperation);

      //Callee List
      List<SCallee> calleeList = this.SDA.SelectCalleeList(this.SBizRuleOperation.GUID);

      foreach (SCallee callee in calleeList)
        callee.CalleeFullPath = this.SBR.GetComponentFullPath(callee.CalleeComponentType, callee.CalleeGUID);

      this.SBizRuleOperation.CalleeList = calleeList;

      this.BindCalleeListBox();
    }

    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SBizRuleOperation);

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

      if (dto.MicroserviceGUID != this.SBizRuleOperation.MicroserviceGUID)
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

      if (dto.MicroserviceGUID != this.SBizRuleOperation.MicroserviceGUID)
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

    #region Call list

    private void LbxMessageList_DragEnter(object sender, DragEventArgs e)
    {
      if (SCommon.DraggedNodeFromTree == null)
      {
        e.Effect = DragDropEffects.None;
      }
      else
      {
        if (
          SCommon.DraggedNodeFromTree.Tag is SAPI || 
          SCommon.DraggedNodeFromTree.Tag is SBizRuleOperation || 
          SCommon.DraggedNodeFromTree.Tag is SDataAccessOperation ||
          SCommon.DraggedNodeFromTree.Tag is SPublisher ||
          SCommon.DraggedNodeFromTree.Tag is SOther
          )
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void LbxMessageList_DragDrop(object sender, DragEventArgs e)
    {
      SComponent calleeComponent = SCommon.DraggedNodeFromTree.Tag as SComponent;
      SComponentType componentType = SComponentType.DataAccessOperation;

      if (calleeComponent is SBizRuleOperation)
      {
        SBizRuleOperation calleeBROp = calleeComponent as SBizRuleOperation;

        if(calleeBROp.MicroserviceGUID != this.SBizRuleOperation.MicroserviceGUID)
        {
          SMessageBox.ShowWarning(SMessage.NO_CALLEE_OTHER_MS_BR_OP);
          return;
        }

        componentType = SComponentType.BizRuleOperation;
      }
      else if (calleeComponent is SDataAccessOperation)
      {
        SDataAccessOperation calleeDAOp = calleeComponent as SDataAccessOperation;

        if (calleeDAOp.BizPackageGUID != this.SBizRuleOperation.BizPackageGUID)
        {
          SMessageBox.ShowWarning(SMessage.NO_CALLEE_OTHER_BP_DA_OP);
          return;
        }

        componentType = SComponentType.DataAccessOperation;
      }
      else if (calleeComponent is SAPI)
      {
        SAPI calleeAPI = calleeComponent as SAPI;

        if (calleeAPI.MicroserviceGUID == this.SBizRuleOperation.MicroserviceGUID)
        {
          SMessageBox.ShowWarning(SMessage.NO_CALLEE_SAME_MS_API);
          return;
        }

        componentType = SComponentType.API;
      }
      else if (calleeComponent is SPublisher)
      {
        SPublisher calleePub = calleeComponent as SPublisher;

        if (calleePub.MicroserviceGUID != this.SBizRuleOperation.MicroserviceGUID)
        {
          SMessageBox.ShowWarning(SMessage.NO_CALLEE_OTHER_MS_PUB);
          return;
        }

        componentType = SComponentType.Publisher;
      }
      else if (calleeComponent is SOther)
      {
        SOther calleeOther = calleeComponent as SOther;

        if (calleeOther.MicroserviceGUID != this.SBizRuleOperation.MicroserviceGUID)
        {
          SMessageBox.ShowWarning(SMessage.NO_CALLEE_OTHER_MS_OTHER);
          return;
        }

        componentType = SComponentType.Other;
      }
      else
      {
        SMessageBox.ShowInformation(SMessage.DND_CALL_LIST);
        return;
      }

      SCallee sCallee = new SCallee(SComponentType.BizRuleOperation, this.SBizRuleOperation.GUID, componentType, calleeComponent.GUID, this.SBR.GetComponentFullPath(calleeComponent), string.Empty);
      this.SBizRuleOperation.CalleeList.Add(sCallee);

      this.BindCalleeListBox();
    }

    private void BindCalleeListBox()
    {
      this.LbxMessageList.DataSource = null;
      this.LbxMessageList.DataSource = this.SBizRuleOperation.CalleeList;
      this.LbxMessageList.DisplayMember = "CalleeWithComment";
    }

    private void LbxMessageList_DoubleClick(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        SCallee callee = this.LbxMessageList.SelectedItem as SCallee;

        if (callee.CalleeGUID != null && callee.CalleeGUID.Length > 0)
          this.MoveComponent(callee.CalleeGUID);
      }
    }


    private void BtnMoveUpCall_Click(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        SCallee selectedCallee = this.LbxMessageList.SelectedItem as SCallee;
        int currentIndex = this.LbxMessageList.SelectedIndex;

        if (currentIndex > 0)
        {
          this.SBizRuleOperation.CalleeList.Insert(currentIndex - 1, selectedCallee);
          this.SBizRuleOperation.CalleeList.RemoveAt(currentIndex + 1);

          this.BindCalleeListBox();
          this.LbxMessageList.SelectedIndex = currentIndex - 1;
        }
      }
    }

    private void BtnMoveDownCall_Click(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        SCallee selectedCallee = this.LbxMessageList.SelectedItem as SCallee;
        int currentIndex = this.LbxMessageList.SelectedIndex;

        if (currentIndex < this.LbxMessageList.Items.Count - 1)
        {
          this.SBizRuleOperation.CalleeList.Insert(currentIndex + 2, selectedCallee);
          this.SBizRuleOperation.CalleeList.RemoveAt(currentIndex);

          this.BindCalleeListBox();
          this.LbxMessageList.SelectedIndex = currentIndex + 1;
        }
      }
    }

    private void BtnEditComment_Click(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        FormComment formComment = new FormComment();
        formComment.CommentChanged += CommentChanged;
        formComment.SelectedIndex = this.LbxMessageList.SelectedIndex;
        formComment.Comment = (this.LbxMessageList.SelectedItem as SCallee).Comment;
        formComment.ShowDialog();
      }
    }

    private void CommentChanged(int selectedIndex, string comment)
    {
      this.SBizRuleOperation.CalleeList[selectedIndex].Comment = comment;
      this.BindCalleeListBox();
    }

    private void BtnAddComment_Click(object sender, EventArgs e)
    {
      SCallee sCallee = new SCallee(SComponentType.BizRuleOperation, this.SBizRuleOperation.GUID, "New Comment");
      this.SBizRuleOperation.CalleeList.Add(sCallee);

      this.BindCalleeListBox();
    }

    private void BtnDeleteCall_Click(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        this.SBizRuleOperation.CalleeList.Remove(this.LbxMessageList.SelectedItem as SCallee);
        this.BindCalleeListBox();
      }
    }


    #endregion

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblParameter.Font = new Font(this.LblParameter.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblReturn.Font = new Font(this.LblReturn.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblCallList.Font = new Font(this.LblCallList.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblMethodName.Font = new Font(this.LblMethodName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblDesc.Font = new Font(this.LblDesc.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Operation Name");
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

        if (this.SBizRuleOperation.CalleeList.Count == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Call List(호출관계)");
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

        if (this.TbxDescription.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "상세설계 내용(Description)");
          this.TbxDescription.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SBizRuleOperation), "BR Operation", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SBizRuleOperation.ProgramID = this.TbxID.Text.Trim();
      this.SBizRuleOperation.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SBizRuleOperation.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SBizRuleOperation.Name = this.TbxName.Text.Trim();
      this.SBizRuleOperation.Tx = this.CkbTransaction.Checked;
      this.SBizRuleOperation.NameEnglish = this.TbxMethodName.Text;

      this.SBizRuleOperation.InputGUID = Convert.ToString(this.TbxInput.Tag);
      this.SBizRuleOperation.Input = this.TbxInput.Text.Trim();

      this.SBizRuleOperation.OutputGUID = Convert.ToString(this.TbxOutput.Tag);
      this.SBizRuleOperation.Output = this.TbxOutput.Text.Trim();

      this.SBizRuleOperation.Description = this.TbxDescription.Text;
      this.SBizRuleOperation.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SBizRuleOperation, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizRuleOperation);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizRuleOperation);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRuleOperation);

      //Callee List
      this.SDA.DeleteCalleeList(this.SBizRuleOperation.GUID);

      foreach (SCallee callee in this.SBizRuleOperation.CalleeList)
        this.SDA.InsertCallee(callee);

      this.SDA.UpdateBizRuleOperation(this.SBizRuleOperation);
      this.ComponentChanged(this.SBizRuleOperation);

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
      this.MoveComponent(this.SBizRuleOperation.GUID);
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
        SCommon.SetLastModifiedTemp(this.SBizRuleOperation);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRuleOperation);
        this.SDA.UpdateBizRuleOperationLastModifiedTemp(this.SBizRuleOperation);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpCallList_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.LBL_CALL_LIST_BR_OP);
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_BR_OP);
    }
  }
}
