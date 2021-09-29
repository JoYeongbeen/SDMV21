using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSProducer : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SProducer SProducer;
    private SBR SBR;
    private SDA SDA;

    public FormSProducer()
    {
      InitializeComponent();
    }


    private void FormSProducer_Load(object sender, EventArgs e)
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

      this.BtnSave.Visible = SCommon.HasPermission(this.SProducer);

      this.LblSampleName.Text = SCommon.SProject.SampleProducerName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleProducerClass;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SProducer);
      this.TbxID.Text = this.SProducer.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SProducer.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SProducer.DesignCompleteDetail;
      this.TbxName.Text = this.SProducer.Name;
      this.TbxClassName.Text = this.SProducer.NameEnglish;
      this.TbxDescription.Text = this.SProducer.Description;
      this.TbxSpecFile.Text = this.SProducer.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SProducer);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SProducer);
    }

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {

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
      if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("Producer") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Name", "XxxProducer");
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        List<SPublisher> pubList = this.SBR.SelectPublisherListByParent(this.SProducer);

        if (pubList.Count == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "하위 Publisher");
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

      this.SProducer.ProgramID = this.TbxID.Text.Trim();
      this.SProducer.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SProducer.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SProducer.Name = this.TbxName.Text.Trim();
      this.SProducer.NameEnglish = this.TbxClassName.Text;
      this.SProducer.Description = this.TbxDescription.Text;
      this.SProducer.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SProducer, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SProducer);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SProducer);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SProducer);

      this.SDA.UpdateProducer(this.SProducer);
      this.ComponentChanged(this.SProducer);

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
      this.MoveComponent(this.SProducer.GUID);
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
        SCommon.SetLastModifiedTemp(this.SProducer);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SProducer);
        this.SDA.UpdateProducerLastModifiedTemp(this.SProducer);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
