using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSController : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SController SController;
    private SBR SBR;
    private SDA SDA;

    public FormSController()
    {
      InitializeComponent();
    }


    private void FormSController_Load(object sender, EventArgs e)
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
      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SController);

      this.LblSampleName.Text = SCommon.SProject.SampleControllerName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleControllerClass;
      this.LblSampleURI.Text = SCommon.SProject.SampleControllerURI;

      if (SCommon.SProject.Dictionary)
      {
        this.BtnTranslate.Visible = true;
        this.LblSampleClassName.Location = new Point(this.LblSampleClassName.Location.X, this.LblSampleClassName.Location.Y);
      }
      else
      {
        this.BtnTranslate.Visible = false;
        this.LblSampleClassName.Location = new Point(this.LblSampleClassName.Location.X - 24, this.LblSampleClassName.Location.Y);
      }
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SController);
      this.TbxID.Text = this.SController.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SController.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SController.DesignCompleteDetail;
      this.TbxName.Text = this.SController.Name;
      this.TbxClassName.Text = this.SController.NameEnglish;
      this.TbxURI.Text = this.SController.URI;
      this.TbxDescription.Text = this.SController.Description;
      this.TbxSpecFile.Text = this.SController.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SController);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SController);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblClassName.Font = new Font(this.LblClassName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblURI.Font = new Font(this.LblURI.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("Controller") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Name", "XxxController");
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        List<SAPI> apiList = this.SBR.SelectAPIListByParent(this.SController);

        if (apiList.Count == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "하위 API");
          return;
        }
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxClassName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Class Name");
          this.TbxClassName.Focus();
          return;
        }

        if (SCommon.IsEnglishOrNumber(this.TbxClassName.Text) == false)
        {
          SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Class Name");
          this.TbxClassName.Focus();
          return;
        }

        if (this.TbxURI.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "URI");
          this.TbxURI.Focus();
          return;
        }
      }

      this.SController.ProgramID = this.TbxID.Text.Trim();
      this.SController.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SController.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SController.Name = this.TbxName.Text.Trim();
      this.SController.NameEnglish = this.TbxClassName.Text;
      this.SController.URI = this.TbxURI.Text;
      this.SController.Description = this.TbxDescription.Text;
      this.SController.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SController, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SController);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SController);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SController);

      this.SDA.UpdateController(this.SController);
      this.ComponentChanged(this.SController);

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
      this.MoveComponent(this.SController.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      this.TbxClassName.Text = this.SBR.GetEngClassName(this.TbxName.Text);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SController);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SController);
        this.SDA.UpdateControllerLastModifiedTemp(this.SController);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
