using System;
using System.Windows.Forms;
using System.Drawing;

using SDM.Common;
using SDM.Component;


namespace SDM
{
  public partial class FormSBizPackage : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SBizPackage SBizPackage;
    private SBR SBR;
    private SDA SDA;

    public FormSBizPackage()
    {
      InitializeComponent();
    }

    private void FormBizPackage_Load(object sender, EventArgs e)
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
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SBizPackage);

      this.LblSampleBPCode.Text = SCommon.SProject.SampleBPCode;
      this.LblSampleSourcePackage.Text = SCommon.SProject.SampleBPSourcePackage;
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizPackage);
      this.TbxID.Text = this.SBizPackage.ProgramID;
      this.CkbDesignDetail.Checked = this.SBizPackage.DesignCompleteDetail;
      this.TbxName.Text = this.SBizPackage.Name;
      this.TbxSourcePackage.Text = this.SBizPackage.NameEnglish;
      this.TbxDescription.Text = this.SBizPackage.Description;
      this.TbxSpecFile.Text = this.SBizPackage.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizPackage);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizPackage);
    }

    private void FormSBizPackage_Leave(object sender, EventArgs e)
    {
      if (this.CheckChanged())
        MessageBox.Show("변경된 내용이 있습니다. 저장하시겠습니까?");
    }

    private bool CheckChanged()
    {
      bool changed = false;

      //폼로드시 설정되는 SBizPackage에 값이 없는 경우 해당 항목이 NULL

      if (this.TbxID.Text != this.SBizPackage.ProgramID)
        changed = true;

      if (this.TbxName.Text != this.SBizPackage.Name)
        changed = true;

      if (this.SBizPackage.NameEnglish != null && this.TbxSourcePackage.Text != this.SBizPackage.NameEnglish)
        changed = true;

      if (this.SBizPackage.Description != null && this.TbxDescription.Text != this.SBizPackage.Description)
        changed = true;

      if (this.SBizPackage.SpecFile != null && this.TbxSpecFile.Text != this.SBizPackage.SpecFile)
        changed = true;

      return changed;
    }


    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblSourcePackage.Font = new Font(this.LblSourcePackage.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "BizPackage Name");
        this.TbxName.Focus();
        return;
      }

      //Validation 상세설계
      if(this.CkbDesignDetail.Checked)
      {
        if (this.TbxSourcePackage.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Source Package");
          this.TbxSourcePackage.Focus();
          return;
        }

        //if (SCommon.IsEnglishOrNumber(this.TbxSourcePackage.Text) == false)
        //{
        //  SMessageBox.ShowWarning(SMessage.NO_ENG_NO, "Source Package");
        //  this.TbxSourcePackage.Focus();
        //  return;
        //}
      }

      this.SBizPackage.ProgramID = this.TbxID.Text.Trim();
      this.SBizPackage.DesignCompleteSkeleton = this.CkbDesignDetail.Checked;
      this.SBizPackage.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SBizPackage.Name = this.TbxName.Text.Trim();
      this.SBizPackage.NameEnglish = this.TbxSourcePackage.Text;
      this.SBizPackage.Description = this.TbxDescription.Text;
      this.SBizPackage.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SBizPackage, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SBizPackage);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SBizPackage);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizPackage);

      this.SDA.UpdateBizPackge(this.SBizPackage);
      this.ComponentChanged(this.SBizPackage);

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
      this.MoveComponent(this.SBizPackage.GUID);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SBizPackage);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SBizPackage);
        this.SDA.UpdateBizPackgeLastModifiedTemp(this.SBizPackage);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
