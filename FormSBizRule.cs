using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;


namespace SDM
{
  public partial class FormSBizRule : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SBizRule SBizRule;
    private SBR SBR;
    private SDA SDA;

    public FormSBizRule()
    {
      InitializeComponent();
    }


    private void FormBizRule_Load(object sender, EventArgs e)
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

      this.BtnSave.Visible = SCommon.HasPermission(this.SBizRule);

      this.LblSampleBRName.Text = SCommon.SProject.SampleBRName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleBRClass;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizRule);
      this.TbxID.Text = this.SBizRule.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SBizRule.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SBizRule.DesignCompleteDetail;
      this.TbxName.Text = this.SBizRule.Name;
      this.CkbCommmonBR.Checked = this.SBizRule.CommonBR;
      this.TbxClassName.Text = this.SBizRule.NameEnglish;
      this.TbxDescription.Text = this.SBizRule.Description;
      this.TbxSpecFile.Text = this.SBizRule.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizRule);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRule);
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
      if (this.CkbCommmonBR.Checked)
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("CBR") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "BR Name", "XxxCBR");
          this.TbxName.Focus();
          return;
        }
      }
      else
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("BR") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "BR Name", "XxxBR");
          this.TbxName.Focus();
          return;
        }

        if (this.TbxName.Text.EndsWith("CBR"))
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "BR Name", "XxxBR");
          this.TbxName.Focus();
          return;
        }
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        List<SBizRuleOperation> brOpList = this.SBR.SelectBizRuleOperationListByBR(this.SBizRule.GUID);

        if (brOpList.Count == 0)
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

      this.SBizRule.ProgramID = this.TbxID.Text.Trim();
      this.SBizRule.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SBizRule.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SBizRule.Name = this.TbxName.Text.Trim();
      this.SBizRule.CommonBR = this.CkbCommmonBR.Checked;
      this.SBizRule.NameEnglish = this.TbxClassName.Text;
      this.SBizRule.Description = this.TbxDescription.Text;
      this.SBizRule.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SBizRule, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizRule);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizRule);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRule);

      this.SDA.UpdateBizRule(this.SBizRule);
      this.ComponentChanged(this.SBizRule);

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
      this.MoveComponent(this.SBizRule.GUID);
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
        SCommon.SetLastModifiedTemp(this.SBizRule);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizRule);
        this.SDA.UpdateBizRuleLastModifiedTemp(this.SBizRule);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
