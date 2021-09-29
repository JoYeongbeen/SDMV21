using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;
using SDM.Project;

namespace SDM
{
  public partial class FormUser : Form
  {
    private SBR SBR;
    private SDA SDA;
    private TreeNode SelectedNode;
    private bool IsTreeDoubleClick = false;
    TreeNode NodeProject;

    #region Form Load

    public FormUser()
    {
      InitializeComponent();
    }

    private void FormUser_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Part/User";

      this.SBR = new SBR();
      this.SDA = new SDA();

      this.NodeProject = new TreeNode(SCommon.SProject.Name);
      this.NodeProject.ImageKey = "SDM";
      this.NodeProject.SelectedImageKey = "SDM";
      this.NodeProject.Nodes.Clear();
      this.TrvUser.Nodes.Add(this.NodeProject);

      this.LoadTreeView();

      this.SetControl();
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnHelpCallList, SMessage.ROLE_DESCRIPTION);
      this.toolTip1.SetToolTip(this.RbnDesigner, SMessage.ROLE_DESIGNER_DEVELOPER);
      this.toolTip1.SetToolTip(this.RbnDeveloper, SMessage.ROLE_DESIGNER_DEVELOPER);
      this.toolTip1.SetToolTip(this.RbnPL, SMessage.ROLE_PL);
      this.toolTip1.SetToolTip(this.RbnModeler, SMessage.ROLE_MODELER);
      this.toolTip1.SetToolTip(this.RbnQA, SMessage.ROLE_VIEW_ONLY);
      this.toolTip1.SetToolTip(this.RbnPM, SMessage.ROLE_VIEW_ONLY);

      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);
      this.toolTip1.SetToolTip(this.BtnRefresh, SMessage.BTN_REFRESH);
    }

    #endregion

    #region Load TreeView

    private void BtnRefresh_Click(object sender, EventArgs e)
    {
      this.LoadTreeView();
    }

    private void LoadTreeView()
    {
      //this.TrvUser.Nodes.Clear();
      List<SPart> partList = this.SDA.SelectPartList();

      foreach (SPart part in partList)
      {
        TreeNode nodePart = new TreeNode();
        nodePart.Text = part.Name;
        nodePart.Tag = part;
        nodePart.ImageKey = "Part";
        nodePart.SelectedImageKey = "Part";

        if (part.GUID == SCommon.PartModeling)
          nodePart.ForeColor = Color.LightGray;

        List<SUser> userList = this.SBR.SelectUserListByPartGUID(part.GUID);

        foreach (SUser user in userList)
        {
          TreeNode nodeUser = new TreeNode();
          nodeUser.Text = user.Name;
          nodeUser.Tag = user;
          nodeUser.ImageKey = "User";
          nodeUser.SelectedImageKey = "User";

          if (user.GUID == SCommon.UserModeler || user.GUID == SCommon.UserTemp)
            nodeUser.ForeColor = Color.LightGray;

          nodePart.Nodes.Add(nodeUser);
        }

        this.NodeProject.Nodes.Add(nodePart);
      }

      if (this.CkbExpandAll.Checked)
        this.TrvUser.ExpandAll();
      else
        this.TrvUser.CollapseAll();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if(this.TrvUser.Focused)
      {
        if (keyData == Keys.Enter)
          this.ViewComponentDetail(this.TrvUser.SelectedNode, false);
        else if (keyData == Keys.F2)
          this.ViewComponentDetail(this.TrvUser.SelectedNode, true);
        else if (keyData == Keys.Delete)
          this.DeleteComponent();
      }

      if (keyData == Keys.F12)
        this.SaveComponent();
      else if (keyData == (Keys.Control | Keys.S))
        this.SaveComponent();
      else if (keyData == (Keys.Control | Keys.Enter))
        this.SaveComponent();

      else if (keyData == (Keys.Control | Keys.R))
        this.LoadTreeView();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void TrvUser_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
      if (this.IsTreeDoubleClick && e.Action == TreeViewAction.Collapse)
        e.Cancel = true;
    }

    private void TrvUser_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      if (this.IsTreeDoubleClick && e.Action == TreeViewAction.Expand)
        e.Cancel = true;
    }

    private void CkbExpandAll_CheckedChanged(object sender, EventArgs e)
    {
      this.IsTreeDoubleClick = false;

      if (this.CkbExpandAll.Checked)
        this.TrvUser.ExpandAll();
      else
        this.TrvUser.CollapseAll();
    }

    #endregion

    #region View Part/User

    private void TrvUser_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (this.TrvUser.SelectedNode != null && this.TrvUser.SelectedNode.Tag != null)
        this.ViewComponentDetail(e.Node, true);
    }

    private void ViewComponentDetail(TreeNode selectedNode, bool focusDetailView)
    {
      this.SelectedNode = selectedNode;

      if (selectedNode.Tag is SPart)
      {
        SPart part = selectedNode.Tag as SPart;
        this.RbnPart.Checked = true;

        this.TbxName.Text = part.Name;
        this.TbxPassword.Text = string.Empty;

        this.CkbViewer.Checked = false;
        this.TbxDescription.Text = part.Description;
        this.TbxRegistered.Text = SCommon.GetRegistered(part);
        this.TbxLastModified.Text = SCommon.GetLastModified(part);

        this.LblUserNameSample.Visible = false;
        this.LblPassword.Enabled = false;
        this.TbxPassword.Enabled = false;
        this.LblRole.Enabled = false;
        this.PnlRole.Enabled = false;
        this.CkbViewer.Enabled = false;

        if (part.GUID == SCommon.PartModeling)
          this.BtnSave.Enabled = false;
        else
          this.BtnSave.Enabled = true;
      }
      else if (selectedNode.Tag is SUser)
      {
        SUser user = selectedNode.Tag as SUser;
        this.RbnUser.Checked = true;

        this.TbxName.Text = user.Name;
        this.TbxPassword.Text = user.Password;

        if (user.Role == this.RbnDesigner.Text)
          this.RbnDesigner.Checked = true;
        else if (user.Role == this.RbnDeveloper.Text)
          this.RbnDeveloper.Checked = true;
        else if (user.Role == this.RbnPL.Text)
          this.RbnPL.Checked = true;
        else if (user.Role == this.RbnModeler.Text)
          this.RbnModeler.Checked = true;
        else if (user.Role == this.RbnQA.Text)
          this.RbnQA.Checked = true;
        else if (user.Role == this.RbnPM.Text)
          this.RbnPM.Checked = true;

        this.CkbViewer.Checked = user.ViewOnly;
        this.TbxDescription.Text = user.Description;
        this.TbxRegistered.Text = SCommon.GetRegistered(user);
        this.TbxLastModified.Text = SCommon.GetLastModified(user);

        this.LblUserNameSample.Visible = true;
        this.LblPassword.Enabled = true;
        this.TbxPassword.Enabled = true;
        this.LblRole.Enabled = true;
        this.PnlRole.Enabled = true;
        this.CkbViewer.Enabled = true;

        if (user.GUID == SCommon.UserModeler || user.GUID == SCommon.UserTemp)
          this.BtnSave.Enabled = false;
        else
          this.BtnSave.Enabled = true;
      }

      if (focusDetailView)
        SCommon.SelectAllAndFocus(this.TbxName);
      else
        this.TrvUser.Focus();
    }

    #endregion

    #region Add/Delete Part/User

    private void TrvUser_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && e.Clicks == 2)
        this.IsTreeDoubleClick = true;
      else
        this.IsTreeDoubleClick = false;

      if (e.Button != MouseButtons.Right)
        return;

      if (this.TrvUser.SelectedNode == null)
        return;

      if(this.TrvUser.SelectedNode.Tag == null)
      {
        this.AddPartMenu.Visible = true;
        this.AddUserMenu.Visible = false;
        this.DeleteMenu.Visible = false;
      }
      else if (this.TrvUser.SelectedNode.Tag is SPart)
      {
        this.AddPartMenu.Visible = false;
        this.AddUserMenu.Visible = true;
        this.DeleteMenu.Visible = true;
      }
      else if (this.TrvUser.SelectedNode.Tag is SUser)
      {
        this.AddPartMenu.Visible = false;
        this.AddUserMenu.Visible = false;
        this.DeleteMenu.Visible = true;
      }

      this.contextMenuStrip1.Show(this.TrvUser, e.Location);
    }

    private void AddPartMenu_Click(object sender, EventArgs e)
    {
      SPart part = new SPart();
      part.GUID = Guid.NewGuid().ToString();
      part.Name = "New Part";
      SCommon.SetDateDesigner(part, true);
      this.SDA.InsertPart(part);

      TreeNode node = new TreeNode();
      node.Text = part.Name;
      node.Tag = part;
      node.ImageKey = "Part";
      node.SelectedImageKey = "Part";

      this.NodeProject.Nodes.Add(node);
      this.NodeProject.Expand();

      this.ViewComponentDetail(node, true);
    }

    private void AddUserMenu_Click(object sender, EventArgs e)
    {
      SUser user = new SUser();
      user.PartGUID = (this.TrvUser.SelectedNode.Tag as SComponent).GUID;
      user.GUID = Guid.NewGuid().ToString();
      user.Name = "New User";
      SCommon.SetDateDesigner(user, true);
      this.SDA.InsertUser(user);

      TreeNode node = new TreeNode();
      node.Text = user.Name;
      node.Tag = user;
      node.ImageKey = "User";
      node.SelectedImageKey = "User";

      this.TrvUser.SelectedNode.Nodes.Add(node);
      this.TrvUser.SelectedNode.ExpandAll();

      this.ViewComponentDetail(node, true);
    }

    private void DeleteMenu_Click(object sender, EventArgs e)
    {
      if (this.TrvUser.SelectedNode != null && this.TrvUser.SelectedNode.Tag != null)
        this.DeleteComponent();
    }

    private void DeleteComponent()
    {
      if (this.TrvUser.SelectedNode.Nodes.Count > 0)
      {
        SMessageBox.ShowWarning(SMessage.HAS_CHILD_NODE);
        return;
      }

      if(this.TrvUser.SelectedNode.Tag is SUser)
      {
        SUser user = this.TrvUser.SelectedNode.Tag as SUser;

        if (user.GUID == SCommon.UserModeler || user.GUID == SCommon.UserTemp)
        {
          SMessageBox.ShowWarning(SMessage.NO_UD_USER);
          return;
        }
      }

      DialogResult result = SMessageBox.ShowConfirm(SMessage.DELETE, this.TrvUser.SelectedNode.Text);

      if (result == DialogResult.No)
        return;

      SComponent selectedComponent = this.TrvUser.SelectedNode.Tag as SComponent;

      if (selectedComponent is SPart)
        this.SDA.DeletePart(selectedComponent as SPart);
      else if (selectedComponent is SUser)
        this.SDA.DeleteUser(selectedComponent as SUser);

      this.TrvUser.SelectedNode.Remove();
    }

    #endregion

    #region Set View Only

    private void RbnDesigner_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void RbnDeveloper_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void RbnPL_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void RbnModeler_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void RbnQA_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void RbnPM_CheckedChanged(object sender, EventArgs e)
    {
      this.SetViewOnly();
    }

    private void SetViewOnly()
    {
      if(this.RbnQA.Checked || this.RbnPM.Checked)
      {
        this.CkbViewer.Checked = true;
        this.CkbViewer.Enabled = false;
      }
      else
      {
        this.CkbViewer.Checked = false;
        this.CkbViewer.Enabled = true;
      }
      
    }

    #endregion

    #region Save

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.SaveComponent();
    }

    private void SaveComponent()
    {
      if(this.RbnPart.Checked)
      {
        SPart part = this.SelectedNode.Tag as SPart;
        part.Name = this.TbxName.Text.Trim(); //varchar(100)
        part.Description = this.TbxDescription.Text; //varchar(1000)
        SCommon.SetDateDesigner(part, false);
        this.TbxRegistered.Text = SCommon.GetRegistered(part);
        this.TbxLastModified.Text = SCommon.GetLastModified(part);

        this.SDA.UpdatePart(part);

        this.SelectedNode.Text = part.Name;
        this.SelectedNode.Tag = part;
      }
      else
      {
        SUser user = this.SelectedNode.Tag as SUser;

        if (user.GUID == SCommon.UserModeler || user.GUID == SCommon.UserTemp)
        {
          SMessageBox.ShowWarning(SMessage.NO_UD_USER);
          return;
        }

        user.Name = this.TbxName.Text.Trim();
        user.Password = this.TbxPassword.Text.Trim();

        if (this.RbnDesigner.Checked)
          user.Role = this.RbnDesigner.Text;
        else if(this.RbnDeveloper.Checked)
          user.Role = this.RbnDeveloper.Text;
        else if (this.RbnPL.Checked)
          user.Role = this.RbnPL.Text;
        else if (this.RbnModeler.Checked)
          user.Role = this.RbnModeler.Text;
        else if(this.RbnQA.Checked)
          user.Role = this.RbnQA.Text;
        else if (this.RbnPM.Checked)
          user.Role = this.RbnPM.Text;

        user.ViewOnly = this.CkbViewer.Checked;
        user.Description = this.TbxDescription.Text;
        SCommon.SetDateDesigner(user, false);
        this.TbxRegistered.Text = SCommon.GetRegistered(user);
        this.TbxLastModified.Text = SCommon.GetLastModified(user);

        this.SDA.UpdateUser(user);

        //FormUser update 하는 부분에 현재 로그인 user와 수정하려는 user id 가 동일한 경우
        //Logginuser 도 업데이트해야 반영됨
        //일반 사용자는 이러한 경우가 별로 없으므로 보류

        this.SelectedNode.Text = user.Name;
        this.SelectedNode.Tag = user;
      }

      SMessageBox.ShowInformation(SMessage.SAVED);
    }

    #endregion

  }
}
