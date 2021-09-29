using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormSUI : Form
  {
    public event ComponentChangeHandler ComponentChanged;
    public delegate void ComponentChangeHandler(SComponent component);

    public event MoveComponentHandler MoveComponent;
    public delegate void MoveComponentHandler(string guid);

    public SUI SUI;
    private SBR SBR;
    private SDA SDA;

    public FormSUI()
    {
      InitializeComponent();
    }

    private void FormSUI_Load(object sender, EventArgs e)
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
      else if(keyData == (Keys.Control | Keys.S))
        this.UpdateComponent();
      else if (keyData == (Keys.Control | Keys.Enter))
        this.UpdateComponent();

      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnTranslate, SMessage.BTN_TRANSLATE_TO_ENG);
      this.toolTip1.SetToolTip(this.BtnSave, SMessage.BTN_SAVE);

      this.BtnSave.Visible = SCommon.HasPermission(this.SUI);

      this.LblSampleName.Text = SCommon.SProject.SampleUIName;
      this.LblSampleProgram.Text = SCommon.SProject.SampleUIProgram;

      if (SCommon.SProject.Dictionary)
      {
        this.BtnTranslate.Visible = true;
        this.LblSampleProgram.Location = new Point(this.LblSampleProgram.Location.X, this.LblSampleProgram.Location.Y);
      }
      else
      {
        this.BtnTranslate.Visible = false;
        this.LblSampleProgram.Location = new Point(this.LblSampleProgram.Location.X - 24, this.LblSampleProgram.Location.Y);
      }
    }


    private void SelectComponent()
    {
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SUI);
      this.TbxID.Text = this.SUI.ProgramID;
      this.CkbDesignSkeleton.Checked = this.SUI.DesignCompleteSkeleton;
      this.CkbDesignDetail.Checked = this.SUI.DesignCompleteDetail;
      this.TbxName.Text = this.SUI.Name;

      this.RbnUIMain.Checked = this.SUI.UIType == "M";
      this.RbnUIPopup.Checked = this.SUI.UIType == "P";
      this.RbnUIWidget.Checked = this.SUI.UIType == "W";

      this.TbxUIProgram.Text = this.SUI.NameEnglish;
      this.TbxDescription.Text = this.SUI.Description;
      this.TbxSpecFile.Text = this.SUI.SpecFile;
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SUI);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SUI);

      //Event
      List<SEvent> evtList = this.SBR.SelectEventListByUI(this.SUI.GUID);

      for (int i = 0; i < evtList.Count; i++)
      {
        SEvent evt = evtList[i];

        this.DgvEventList.Rows.Add(evt.ProgramID, evt.Name, this.SBR.GetComponentFullPath(SComponentType.API, evt.CalleeApiGuid));//Value = FullPath
        this.DgvEventList.Rows[i].Cells[2].Tag = evt.CalleeApiGuid;//Tag = GUID
      }
    }

    #region Event

    private void DgvEventList_DragEnter(object sender, DragEventArgs e)
    {
      if (SCommon.DraggedNodeFromTree == null)
      {
        e.Effect = DragDropEffects.None;
      }
      else
      {
        if (SCommon.DraggedNodeFromTree.Tag is SAPI)
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
    }

    private void DgvEventList_DragDrop(object sender, DragEventArgs e)
    {
      SAPI api = SCommon.DraggedNodeFromTree.Tag as SAPI;
      //SComponentType componentType = SComponentType.DataAccessOperation;

      Point point = this.DgvEventList.PointToClient(new Point(e.X, e.Y));
      DataGridView.HitTestInfo hti = this.DgvEventList.HitTest(point.X, point.Y);

      if (hti.Type == DataGridViewHitTestType.Cell)
      {
        this.DgvEventList.Rows[hti.RowIndex].Cells[hti.ColumnIndex].Tag = api.GUID;
        this.DgvEventList.Rows[hti.RowIndex].Cells[hti.ColumnIndex].Value = this.SBR.GetComponentFullPath(api);
      }
    }

    private void DgvEventList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 2)
      {
        if (this.DgvEventList.SelectedRows.Count == 1)
        {
          string eventCalleeAPIGuid = this.DgvEventList.SelectedRows[0].Cells[2].Tag as string;

          if (eventCalleeAPIGuid != null && eventCalleeAPIGuid.Length > 0)
            this.MoveComponent(eventCalleeAPIGuid);
        }
      }
    }

    #endregion

    private void CkbDesignSkeleton_CheckedChanged(object sender, EventArgs e)
    {
      this.LblEventList.Font = new Font(this.LblEventList.Font, this.CkbDesignSkeleton.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void CkbDesignDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.LblProgramName.Font = new Font(this.LblProgramName.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
      this.LblSpecFile.Font = new Font(this.LblSpecFile.Font, this.CkbDesignDetail.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.UpdateComponent();
    }

    private void UpdateComponent()
    {
      if (this.TbxName.Text.Length == 0 || this.TbxName.Text.EndsWith("UI") == false)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED_AND_SUFFIX, "UI Name", "XxxUI");
        this.TbxName.Focus();
        return;
      }

      //Validation 기본설계
      if (this.CkbDesignSkeleton.Checked)
      {
        if (this.DgvEventList.Rows.Count == 1)
        {
          if (this.DgvEventList.Rows[0].IsNewRow)
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Event");
            this.DgvEventList.Focus();
            return;
          }
        }

        foreach (DataGridViewRow row in this.DgvEventList.Rows)
        {
          if (row.IsNewRow == false && (row.Cells[1].Value == null || row.Cells[2].Value == null)) //1 Event //2 Callee API
          {
            SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Event 또는 Callee API");
            this.DgvEventList.Focus();
            return;
          }
        }
      }

      //Validation 상세설계
      if (this.CkbDesignSkeleton.Checked && this.CkbDesignDetail.Checked)
      {
        if (this.TbxUIProgram.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "Program Name");
          this.TbxUIProgram.Focus();
          return;
        }

        if (this.TbxSpecFile.Text.Length == 0)
        {
          SMessageBox.ShowWarning(SMessage.NO_DEFINE, "UI 설계서");
          this.TbxSpecFile.Focus();
          return;
        }
      }

      this.SUI.ProgramID = this.TbxID.Text.Trim();
      this.SUI.DesignCompleteSkeleton = this.CkbDesignSkeleton.Checked;
      this.SUI.DesignCompleteDetail = this.CkbDesignDetail.Checked;
      this.SUI.Name = this.TbxName.Text.Trim();

      if (this.RbnUIMain.Checked)
        this.SUI.UIType = "M";
      else if (this.RbnUIPopup.Checked)
        this.SUI.UIType = "P";
      else if (this.RbnUIWidget.Checked)
        this.SUI.UIType = "W";

      this.SUI.NameEnglish = this.TbxUIProgram.Text;
      this.SUI.Description = this.TbxDescription.Text;
      this.SUI.SpecFile = this.TbxSpecFile.Text;

      SCommon.SetDateDesigner(this.SUI, false);
      this.TbxParentPath.Text = this.SBR.GetComponentFullPath(this.SUI);
      this.TbxRegistered.Text = SCommon.GetRegistered(this.SUI);
      this.TbxLastModified.Text = SCommon.GetLastModified(this.SUI);

      //Event
      this.SDA.DeleteEventList(this.SUI.GUID);

      foreach (DataGridViewRow row in this.DgvEventList.Rows)
      {
        if (row.Cells[1].Value != null)
        {
          SEvent evt = new SEvent(
            this.SUI.GUID,
            row.Cells[0].Value == null ? string.Empty : row.Cells[0].Value.ToString(),//ID
            row.Cells[1].Value.ToString(),//Name
            row.Cells[2].Tag == null ? string.Empty : row.Cells[2].Tag.ToString()//API GUID
          );

          this.SDA.InsertEvent(evt);
        }
      }

      this.SDA.UpdateUI(this.SUI);
      this.ComponentChanged(this.SUI);

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
      this.MoveComponent(this.SUI.GUID);
    }

    private void BtnTranslate_Click(object sender, EventArgs e)
    {
      //if (SDictionary.DicNoun.Rows.Count == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.NO_DIC);
      //  return;
      //}

      string uiEngName = this.SBR.GetEngClassName(this.TbxName.Text.Replace("UI", string.Empty));
      uiEngName = SCommon.GetCamelCasing(uiEngName);

      if (this.RbnUIMain.Checked)
        uiEngName = uiEngName + "M.xml";
      else if (this.RbnUIPopup.Checked)
        uiEngName = uiEngName + "P.xml";
      else if (this.RbnUIWidget.Checked)
        uiEngName = uiEngName + "W.xml";

      this.TbxUIProgram.Text = uiEngName;
    }

    private void LblLastModified_DoubleClick(object sender, EventArgs e)
    {
      if (SCommon.LoggedInUser.Role == SCommon.RoleModeler || SCommon.LoggedInUser.Role == SCommon.RolePL)
      {
        SCommon.SetLastModifiedTemp(this.SUI);
        this.TbxLastModified.Text = SCommon.GetLastModified(this.SUI);
        this.SDA.UpdateUILastModifiedTemp(this.SUI);
        SMessageBox.ShowInformation(SMessage.CHANGED_TEMP_USER);
      }
    }
  }
}
