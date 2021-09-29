using System;
using System.Windows.Forms;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSInternalSystem : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SInternalSystem SInternalSystem;
    private SBR SBR;
    private SDA SDA;

    public FormSInternalSystem()
    {
      InitializeComponent();
    }

    private void FormSInternalSystem_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
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
      this.toolTip1.SetToolTip(this.LblCallList, SMessage.LBL_CALL_LIST_SYSTEM);
      this.toolTip1.SetToolTip(this.BtnHelpCallList, SMessage.LBL_CALL_LIST_SYSTEM);

      this.toolTip1.SetToolTip(this.BtnEditComment, SMessage.BTN_EDIT_COMMENT);
      this.toolTip1.SetToolTip(this.BtnAddComment, SMessage.BTN_ADD_COMMENT);

      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SInternalSystem);
    }

    private void SelectComponent()
    {
      this.TbxID.Text = this.SInternalSystem.ProgramID;
      this.TbxName.Text = this.SInternalSystem.Name;      
      this.TbxDescription.Text = this.SInternalSystem.Description;
      this.TbxSpecFile.Text = this.SInternalSystem.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SInternalSystem);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SInternalSystem);

      //Callee List
      List<SCallee> calleeList = this.SDA.SelectCalleeList(this.SInternalSystem.GUID);

      foreach (SCallee callee in calleeList)
        callee.CalleeFullPath = this.SBR.GetComponentFullPath(callee.CalleeComponentType, callee.CalleeGUID);

      this.SInternalSystem.CalleeList = calleeList;

      this.BindCalleeListBox();
    }

    #region Call list

    private void LbxMessageList_DragEnter(object sender, DragEventArgs e)
    {
      if (SCommon.DraggedNodeFromTree == null)
      {
        e.Effect = DragDropEffects.None;
      }
      else
      {
        if (SCommon.DraggedNodeFromTree.Tag is SAPI || SCommon.DraggedNodeFromTree.Tag is SOther)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void LbxMessageList_DragDrop(object sender, DragEventArgs e)
    {
      SComponent calleeComponent = SCommon.DraggedNodeFromTree.Tag as SComponent;
      SComponentType componentType = SComponentType.DataAccessOperation;

      if (calleeComponent is SAPI)
        componentType = SComponentType.API;
      else if (calleeComponent is SOther)
        componentType = SComponentType.Other;

      SCallee sCallee = new SCallee(SComponentType.InternalSystem, this.SInternalSystem.GUID, componentType, calleeComponent.GUID, this.SBR.GetComponentFullPath(calleeComponent), string.Empty);
      this.SInternalSystem.CalleeList.Add(sCallee);

      this.BindCalleeListBox();
    }

    private void BindCalleeListBox()
    {
      this.LbxMessageList.DataSource = null;
      this.LbxMessageList.DataSource = this.SInternalSystem.CalleeList;
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
          this.SInternalSystem.CalleeList.Insert(currentIndex - 1, selectedCallee);
          this.SInternalSystem.CalleeList.RemoveAt(currentIndex + 1);

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
          this.SInternalSystem.CalleeList.Insert(currentIndex + 2, selectedCallee);
          this.SInternalSystem.CalleeList.RemoveAt(currentIndex);

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
      this.SInternalSystem.CalleeList[selectedIndex].Comment = comment;
      this.BindCalleeListBox();
    }

    private void BtnAddComment_Click(object sender, EventArgs e)
    {
      SCallee sCallee = new SCallee(SComponentType.InternalSystem, this.SInternalSystem.GUID, "New Comment");
      this.SInternalSystem.CalleeList.Add(sCallee);

      this.BindCalleeListBox();
    }

    private void BtnDeleteCall_Click(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        this.SInternalSystem.CalleeList.Remove(this.LbxMessageList.SelectedItem as SCallee);
        this.BindCalleeListBox();
      }
    }

    #endregion

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxID.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "ID");
        this.TbxID.Focus();
        return;
      }

      if (this.TbxName.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Name");
        this.TbxName.Focus();
        return;
      }

      this.SInternalSystem.ProgramID = this.TbxID.Text.Trim();
      this.SInternalSystem.Name = this.TbxName.Text.Trim();
      this.SInternalSystem.Description = this.TbxDescription.Text;
      this.SInternalSystem.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SInternalSystem, false);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SInternalSystem);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SInternalSystem);

      //Callee List
      this.SDA.DeleteCalleeList(this.SInternalSystem.GUID);

      foreach (SCallee callee in this.SInternalSystem.CalleeList)
        this.SDA.InsertCallee(callee);

      this.SDA.UpdateInternalSystem(this.SInternalSystem);
      this.ComponentChanged(this.SInternalSystem);

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
      this.MoveComponent(this.SInternalSystem.GUID);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SInternalSystem);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SInternalSystem);
        this.SDA.UpdateInternalSystemLastModifiedTemp(this.SInternalSystem);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpCallList_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.LBL_CALL_LIST_SYSTEM);
    }
  }
}
