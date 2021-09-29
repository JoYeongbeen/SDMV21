using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSDto : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SDto SDto;
    private List<SCaller> CallerList;
    private SBR SBR;
    private SDA SDA;

    public FormSDto()
    {
      InitializeComponent();
    }

    private void FormSDto_Load(object sender, EventArgs e)
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
      this.SetControl();
      this.SelectComponent();
      this.GetConsumerList();
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
      this.toolTip1.SetToolTip(this.BtnTranslateAttribute, SMessage.BTN_TRANSLATE_TO_ENG);

      this.toolTip1.SetToolTip(this.BtnUploadAttribute, SMessage.UPLOAD_ATTRIBUTE);
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_DTO);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SDto);

      this.LblSampleName.Text = SCommon.SProject.SampleDtoName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleDtoClass;

      if (SCommon.SProject.Dictionary)
      {
        this.BtnTranslate.Visible = true;
        this.LblSampleClassName.Location = new Point(this.LblSampleClassName.Location.X, this.LblSampleClassName.Location.Y);

        this.BtnTranslateAttribute.Visible = true;
      }
      else
      {
        this.BtnTranslate.Visible = false;
        this.LblSampleClassName.Location = new Point(this.LblSampleClassName.Location.X - 24, this.LblSampleClassName.Location.Y);

        this.BtnTranslateAttribute.Visible = false;
      }
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDto);
      this.TbxID.Text = this.SDto.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SDto.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SDto.DesignCompleteDetail;
      this.TbxName.Text = this.SDto.Name;
      this.TbxClassName.Text = this.SDto.NameEnglish;
      this.TbxDescription.Text = this.SDto.Description;
      this.TbxSpecFile.Text = this.SDto.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDto);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDto);

      //Attribue
      List<SDtoAttribute> attrList = this.SDA.SelectDtoAttributeList(this.SDto.GUID);

      foreach (SDtoAttribute attr in attrList)
        this.DgvAttributeList.Rows.Add(attr.ProgramID, attr.DataType, attr.Name, attr.NameEnglish, attr.Description);
    }

    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SDto);
      this.BtnConsumer.Enabled = this.CallerList.Count > 0;
      this.BtnConsumer.Text = this.CallerList.Count.ToString();
    }

    private void BtnConsumer_Click(object sender, EventArgs e)
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form.GetType() == typeof(FormCallerCallee))
        {
          FormCallerCallee formCC = form as FormCallerCallee;
          formCC.SetData(this.TbxParentPath.Text, this.CallerList);
          formCC.Activate();

          return;
        }
      }

      FormCallerCallee formCCNew = new FormCallerCallee();
      formCCNew.MoveComponentFromCaller += MoveComponentFromCaller;
      formCCNew.SetData(this.TbxParentPath.Text, this.CallerList);
      formCCNew.Show();
    }

    private void MoveComponentFromCaller(string guid)
    {
      this.MoveComponent(guid);
    }

    #region Upload

    private void BtnUploadAttribute_Click(object sender, EventArgs e)
    {
      FormUploader form = new FormUploader();
      form.SelectedComponent = this.SDto;
      form.UploaderChanged += UploaderChanged;
      form.ShowDialog();
    }

    private void UploaderChanged(string type, string[] inputList)
    {
      foreach (string input in inputList)
      {
        if (input.Length > 0)
        {
          if (type == "AT")
          {
            string[] columnList = input.Split(',');

            this.DgvAttributeList.Rows.Add(
              columnList[0],
              columnList[1],
              columnList[2],
              columnList[3],
              columnList[4]
            );
          }
        }
      }
    }

    #endregion


    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblAttribute.Font = new Font(this.LblAttribute.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
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
      if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("Dto") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Name", "XxxDto");
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        if (this.DgvAttributeList.Rows.Count == 1)
        {
          if (this.DgvAttributeList.Rows[0].IsNewRow)
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Attribute");
            this.DgvAttributeList.Focus();
            return;
          }
        }

        foreach (DataGridViewRow row in this.DgvAttributeList.Rows)
        {
          if (row.IsNewRow == false && row.Cells[2].Value == null)
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Attribute");
            this.DgvAttributeList.Focus();
            return;
          }
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

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SDto), "", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SDto.ProgramID = this.TbxID.Text.Trim();
      this.SDto.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SDto.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SDto.Name = this.TbxName.Text.Trim();
      this.SDto.NameEnglish = this.TbxClassName.Text;
      this.SDto.Description = this.TbxDescription.Text;
      this.SDto.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SDto, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SDto);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SDto);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SDto);

      //Attribue
      this.SDA.DeleteDtoAttributeList(this.SDto.GUID);

      foreach (DataGridViewRow row in this.DgvAttributeList.Rows)
      {
        if (row.Cells[2].Value != null)
        {
          this.SDA.InsertDtoAttribute(
            this.SDto,
            row.Cells[0].Value == null ? string.Empty : row.Cells[0].Value.ToString(),//Program ID
            row.Cells[1].Value == null ? string.Empty : row.Cells[1].Value.ToString(),//DataType
            row.Cells[2].Value.ToString(), //Name
            row.Cells[3].Value == null ? string.Empty : row.Cells[3].Value.ToString(),//Variable
            row.Cells[4].Value == null ? string.Empty : row.Cells[4].Value.ToString()//Description
          );
        }
      }

      this.SDA.UpdateDto(this.SDto);
      this.ComponentChanged(this.SDto);

      
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
      this.MoveComponent(this.SDto.GUID);
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

    private void BtnTranslateAttribute_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      foreach (DataGridViewRow row in this.DgvAttributeList.SelectedRows)
      {
        if (row.Cells[2].Value != null) //attribute
          row.Cells[3].Value = this.SBR.GetEngAttributeName(row.Cells[2].Value.ToString());
      }
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SDto);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SDto);
        this.SDA.UpdateDtoLastModifiedTemp(this.SDto);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_DTO);
    }
  }
}
