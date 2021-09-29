using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSEntity : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SEntity SEntity;
    private List<SCaller> CallerList;
    private SBR SBR;
    private SDA SDA;

    public FormSEntity()
    {
      InitializeComponent();
    }

    private void FormSEntity_Load(object sender, EventArgs e)
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
      this.toolTip1.SetToolTip(this.BtnHelpConsumer, SMessage.HELP_CONSUMER_ENTITY);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SEntity);

      this.LblSampleName.Text = SCommon.SProject.SampleEntityName;
      this.LblSampleClassName.Text = SCommon.SProject.SampleEntityClass;
      this.LblSampleTableName.Text = SCommon.SProject.SampleEntityTable;

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
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SEntity);
      this.TbxID.Text = this.SEntity.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SEntity.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SEntity.DesignCompleteDetail;
      this.TbxName.Text = this.SEntity.Name;
      this.CkbJoinEntity.Checked = this.SEntity.JoinEntity;
      this.TbxClassName.Text = this.SEntity.NameEnglish;
      this.TbxTableName.Text = this.SEntity.TableName;
      this.TbxDescription.Text = this.SEntity.Description;
      this.TbxSpecFile.Text = this.SEntity.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SEntity);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SEntity);

      //Attribue
      List<SEntityAttribute> attrList = this.SDA.SelectEntityAttributeList(this.SEntity.GUID);

      //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
      foreach (SEntityAttribute attr in attrList)
      {
        this.DgvAttributeList.Rows.Add
        (
          attr.ProgramID, 
          attr.DataType, 
          attr.Name, 
          attr.NameEnglish, 
          attr.PK, 
          attr.FK, 
          attr.NN, 
          attr.DBDataType,
          attr.DBColumn,
          attr.Description
        );
      }
    }


    private void GetConsumerList()
    {
      this.CallerList = this.SBR.GetConsumerList(this.SEntity);
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
      form.SelectedComponent = this.SEntity;
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

            //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
            this.DgvAttributeList.Rows.Add
            (
              columnList[0],
              columnList[1],
              columnList[2],
              columnList[3],
              SCommon.ConvertStringToBoolean(columnList[4]),
              SCommon.ConvertStringToBoolean(columnList[5]),
              SCommon.ConvertStringToBoolean(columnList[6]),
              columnList[7],
              columnList[8],
              columnList[9]
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
      this.LblTableName.Font = new Font(this.LblTableName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.CkbJoinEntity.Checked)
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("JEntity") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Entity Name", "XxxJEntity");
          this.TbxName.Focus();
          return;
        }
      }
      else
      {
        if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("Entity") == false)
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Entity Name", "XxxEntity");
          this.TbxName.Focus();
          return;
        }

        if (this.TbxName.Text.EndsWith("JEntity"))
        {
          SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "Entity Name", "XxxEntity");
          this.TbxName.Focus();
          return;
        }
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
          //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
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

        if (this.TbxTableName.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Table Name");
          this.TbxTableName.Focus();
          return;
        }
      }

      //Consumer
      if (this.CallerList.Count > 0)
      {
        string callerList = string.Empty;

        foreach (SCaller caller in this.CallerList)
          callerList += caller.FullName + Environment.NewLine;

        DialogResult result = SMessageBox.ShowConfirm(SMessage.SAVE_CONFIRM_CONSUMER, this.CallerList.Count.ToString(), SCommon.GetName(this.SEntity), "", callerList);

        if (result != DialogResult.Yes)
          return;
      }

      this.SEntity.ProgramID = this.TbxID.Text.Trim();
      this.SEntity.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SEntity.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SEntity.Name = this.TbxName.Text.Trim();
      this.SEntity.JoinEntity = this.CkbJoinEntity.Checked;
      this.SEntity.NameEnglish = this.TbxClassName.Text;
      this.SEntity.TableName = this.TbxTableName.Text;
      this.SEntity.Description = this.TbxDescription.Text;
      this.SEntity.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SEntity, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SEntity);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SEntity);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SEntity);

      //Attribue
      this.SDA.DeleteEntityAttributeList(this.SEntity.GUID);

      foreach (DataGridViewRow row in this.DgvAttributeList.Rows)
      {
        //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
        if (row.Cells[2].Value != null)
        {
          this.SDA.InsertEntityAttribute(
            this.SEntity,
            row.Cells[0].Value == null ? string.Empty : row.Cells[0].Value.ToString(),//ProgramID
            row.Cells[1].Value == null ? string.Empty : row.Cells[1].Value.ToString(),//DataType
            row.Cells[2].Value.ToString(),//Attribute
            row.Cells[3].Value == null ? string.Empty : row.Cells[3].Value.ToString(),//Variable
            bool.Parse(row.Cells[4].Value == null ? "false" : row.Cells[4].Value.ToString()),//PK
            bool.Parse(row.Cells[5].Value == null ? "false" : row.Cells[5].Value.ToString()),//FK
            bool.Parse(row.Cells[6].Value == null ? "false" : row.Cells[6].Value.ToString()),//NotNull
            row.Cells[7].Value == null ? string.Empty : row.Cells[7].Value.ToString(),//DB DataType
            row.Cells[8].Value == null ? string.Empty : row.Cells[8].Value.ToString(),//DB Column
            row.Cells[9].Value == null ? string.Empty : row.Cells[9].Value.ToString()//Description
          );
        }
      }

      this.SDA.UpdateEntity(this.SEntity);
      this.ComponentChanged(this.SEntity);

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
      this.MoveComponent(this.SEntity.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      this.TbxClassName.Text = this.SBR.GetEngClassName(this.TbxName.Text);
    }

    private void BtnTranslateAttribute_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewRow row in this.DgvAttributeList.SelectedRows)
      {
        //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
        if (row.Cells[2].Value != null)
        {
          row.Cells[3].Value = this.SBR.GetEngAttributeName(row.Cells[2].Value.ToString());
          row.Cells[8].Value = this.SBR.GetDBColumn(row.Cells[2].Value.ToString());
        }
      }
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SEntity);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SEntity);
        this.SDA.UpdateEntityLastModifiedTemp(this.SEntity);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }

    private void BtnHelpConsumer_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.HELP_CONSUMER_ENTITY);
    }
  }
}
