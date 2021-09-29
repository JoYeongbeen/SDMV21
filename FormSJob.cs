using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSJob : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SJob SJob;
    private SBR SBR;
    private SDA SDA;

    public FormSJob()
    {
      InitializeComponent();
    }

    private void FormSJob_Load(object sender, EventArgs e)
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

      this.BtnSave.Visible = SCommon.HasPermission(this.SJob);

      this.LblSampleSchedule.Text = SCommon.SProject.SampleJobSchedule;
      this.LblSampleStart.Text = SCommon.SProject.SampleJobStart;

      this.BtnTranslate.Visible = SCommon.SProject.Dictionary;
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SJob);
      this.TbxID.Text = this.SJob.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SJob.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SJob.DesignCompleteDetail;
      this.TbxName.Text = this.SJob.Name;
      this.TbxJobName.Text = this.SJob.NameEnglish;
      this.TbxSchedule.Text = this.SJob.Schedule;
      this.TbxStart.Text = this.SJob.Start;
      this.TbxDescription.Text = this.SJob.Description;
      this.TbxSpecFile.Text = this.SJob.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SJob);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SJob);
    }


    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblJobName.Font = new Font(this.LblJobName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblSchedule.Font = new Font(this.LblSchedule.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblStart.Font = new Font(this.LblStart.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      
    }

    private void CkbDesignDetail_CheckedChanged_1(object sender, EventArgs e)
    {
      this.LblJobName.Font = new Font(this.LblJobName.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblSchedule.Font = new Font(this.LblSchedule.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblStart.Font = new Font(this.LblStart.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
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
        List<SStep> stepList = this.SBR.SelectStepListByJob(this.SJob.GUID);

        if (stepList.Count == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "하위 Step");
          return;
        }
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxJobName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Job Name");
          this.TbxJobName.Focus();
          return;
        }

        if (SCommon.IsEnglishOrNumber(this.TbxJobName.Text) == false)
        {
          SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Job Name");
          this.TbxJobName.Focus();
          return;
        }

        if (this.TbxSchedule.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Schedule");
          this.TbxSchedule.Focus();
          return;
        }

        if (this.TbxStart.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Start");
          this.TbxStart.Focus();
          return;
        }
      }

      this.SJob.ProgramID = this.TbxID.Text.Trim();
      this.SJob.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SJob.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SJob.Name = this.TbxName.Text.Trim();
      this.SJob.NameEnglish = this.TbxJobName.Text;
      this.SJob.Schedule = this.TbxSchedule.Text;
      this.SJob.Start = this.TbxStart.Text;
      this.SJob.Description = this.TbxDescription.Text;
      this.SJob.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SJob, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SJob);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SJob);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SJob);

      this.SDA.UpdateJob(this.SJob);
      this.ComponentChanged(this.SJob);

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
      this.MoveComponent(this.SJob.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      this.TbxJobName.Text = this.SBR.GetEngClassName(this.TbxName.Text);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SJob);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SJob);
        this.SDA.UpdateJobLastModifiedTemp(this.SJob);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
