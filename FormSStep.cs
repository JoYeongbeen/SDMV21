using System;
using System.Windows.Forms;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSStep : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SStep SStep;
    private SBR SBR;
    private SDA SDA;

    public FormSStep()
    {
      InitializeComponent();
    }

    private void FormSStep_Load(object sender, EventArgs e)
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

      this.BtnSave.Visible = SCommon.HasPermission(this.SStep);

      this.BtnTranslate.Visible = SCommon.SProject.Dictionary;
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SStep);
      this.TbxID.Text = this.SStep.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SStep.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SStep.DesignCompleteDetail;
      this.TbxName.Text = this.SStep.Name;
      this.TbxStepName.Text = this.SStep.NameEnglish;
      this.TbxDescription.Text = this.SStep.Description;
      this.TbxSpecFile.Text = this.SStep.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SStep);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SStep);
    }

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblStepName.Font = new Font(this.LblStepName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
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
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Name");
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxStepName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Step Name");
          this.TbxStepName.Focus();
          return;
        }

        if (SCommon.IsEnglishOrNumber(this.TbxStepName.Text) == false)
        {
          SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Step Name");
          this.TbxStepName.Focus();
          return;
        }

        if (this.TbxDescription.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "상세설계 내용(Description)");
          this.TbxDescription.Focus();
          return;
        }
      }

      this.SStep.ProgramID = this.TbxID.Text.Trim();
      this.SStep.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SStep.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SStep.Name = this.TbxName.Text.Trim();
      this.SStep.NameEnglish = this.TbxStepName.Text;
      this.SStep.Description = this.TbxDescription.Text;
      this.SStep.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SStep, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SStep);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SStep);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SStep);

      this.SDA.UpdateStep(this.SStep);
      this.ComponentChanged(this.SStep);

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
      this.MoveComponent(this.SStep.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      this.TbxStepName.Text = this.SBR.GetEngClassName(this.TbxName.Text);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SStep);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SStep);
        this.SDA.UpdateStepLastModifiedTemp(this.SStep);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
