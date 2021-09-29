using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;


namespace SDM
{
  public partial class FormSDataAccess : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SDataAccess SDataAccess;
    private SBR SBR;
    private SDA SDA;

    public FormSDataAccess()
    {
      InitializeComponent();
    }


    private void FormSDataAccess_Load(object sender, EventArgs e)
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

      this.BtnSave.Visible = SCommon.HasPermission(this.SDataAccess);

      this.LblSampleDAName.Text = SCommon.SProject.SampleDAName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleDAClass;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDataAccess);
      this.TbxID.Text = this.SDataAccess.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SDataAccess.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SDataAccess.DesignCompleteDetail;
      this.TbxName.Text = this.SDataAccess.Name;
      this.CkbJoinDA.Checked = this.SDataAccess.JoinDA;
      this.TbxClassName.Text = this.SDataAccess.NameEnglish;
      this.TbxDescription.Text = this.SDataAccess.Description;
      this.TbxSpecFile.Text = this.SDataAccess.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDataAccess);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccess);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblClassName.Font = new Font(this.LblClassName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.CkbJoinDA.Checked)
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("JDA") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "DA Name", "XxxJDA");
          this.TbxName.Focus();
          return;
        }
      }
      else
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("DA") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "DA Name", "XxxDA");
          this.TbxName.Focus();
          return;
        }

        if (this.TbxName.Text.EndsWith("JDA"))
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "DA Name", "XxxDA");
          this.TbxName.Focus();
          return;
        }
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        List<SDataAccessOperation> daOpList = this.SBR.SelectDataAccessOperationListByDA(this.SDataAccess.GUID);

        if (daOpList.Count == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Operation");
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
      }

      this.SDataAccess.ProgramID = this.TbxID.Text.Trim();
      this.SDataAccess.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SDataAccess.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SDataAccess.Name = this.TbxName.Text.Trim();
      this.SDataAccess.JoinDA = this.CkbJoinDA.Checked;
      this.SDataAccess.NameEnglish = this.TbxClassName.Text;
      this.SDataAccess.Description = this.TbxDescription.Text;
      this.SDataAccess.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SDataAccess, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDataAccess);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDataAccess);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccess);

      this.SDA.UpdateDataAccess(this.SDataAccess);
      this.ComponentChanged(this.SDataAccess);

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
      this.MoveComponent(this.SDataAccess.GUID);
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
        SCommon.SetLastModifiedTemp(this.SDataAccess);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SDataAccess);
        this.SDA.UpdateDataAccessLastModifiedTemp(this.SDataAccess);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
