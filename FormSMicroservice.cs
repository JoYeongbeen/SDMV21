using System;
using System.Windows.Forms;

using SDM.Component;
using SDM.Common;
using SDM.Project;

namespace SDM
{
  public partial class FormSMicroservice : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SMicroservice SMicroservice;

    private SBR SBR;
    private SDA SDA;

    public FormSMicroservice()
    {
      InitializeComponent();
    }


    private void FormMicroservice_Load(object sender, EventArgs e)
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

      SCommon.SetBizPartCombo(this.CmbPart);

      this.BtnSave.Visible = SCommon.HasPermission(this.SMicroservice);

      this.LblSampleMSCode.Text = SCommon.SProject.SampleMSCode;
    }


    private void SelectComponent()
    {
      this.TbxID.Text = this.SMicroservice.ProgramID;
      this.TbxName.Text = this.SMicroservice.Name;
      this.TbxDescription.Text = this.SMicroservice.Description;
      this.TbxSpecFile.Text = this.SMicroservice.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SMicroservice);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SMicroservice);

      foreach(SPart part in this.CmbPart.Items)
      {
        if (part.GUID == this.SMicroservice.BizPartGUID)
          this.CmbPart.SelectedItem = part;
      }
    }


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
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Microservice Name");
        this.TbxName.Focus();
        return;
      }

      if(this.CmbPart.SelectedItem == null)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "Part");
        this.CmbPart.Focus();
        return;
      }

      this.SMicroservice.ProgramID = this.TbxID.Text.Trim();
      this.SMicroservice.Name = this.TbxName.Text.Trim();
      this.SMicroservice.BizPartGUID = (this.CmbPart.SelectedItem as SPart).GUID;
      this.SMicroservice.Description = this.TbxDescription.Text;
      this.SMicroservice.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SMicroservice, false);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SMicroservice);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SMicroservice);

      this.SDA.UpdateMicroservice(this.SMicroservice);
      this.ComponentChanged(this.SMicroservice);

      SMessageBox.ShowInformation(SMessage.SAVED);
    }

    private void BtnSynchTree_Click(object sender, EventArgs e)
    {
      this.MoveComponent(this.SMicroservice.GUID);
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SMicroservice);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SMicroservice);
        this.SDA.UpdateMicroserviceLastModifiedTemp(this.SMicroservice);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnSpecFile_Click(object sender, EventArgs e)
    {
      this.TbxSpecFile.Text = SCommon.GetSpecFilePath();
    }

    private void BtnViewSpec_Click(object sender, EventArgs e)
    {
      SCommon.OpenDocument(this.TbxSpecFile.Text);
    }

  }
}
