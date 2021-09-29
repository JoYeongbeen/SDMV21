using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormTreeOption : Form
  {
    public event TreeOptionChangeHandler TreeOptionChanged;
    public delegate void TreeOptionChangeHandler();

    private SDA SDA;

    public FormTreeOption()
    {
      InitializeComponent();
    }

    private void FormTreeOption_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Tree Option";

      this.SDA = new SDA();

      this.LblFont.Text = SCommon.LoggedInUser.TreeFont;
      this.LblFontSize.Text = SCommon.LoggedInUser.TreeFontSize;

      //MS
      List<SMicroservice> msList = this.SDA.SelectMicroserviceList(true, false);

      foreach(SMicroservice ms in msList)
      {
        this.DgvMS.Rows.Add
        (
          ms.GUID,
          ms.Display == "Y",
          ms.ProgramID,
          ms.Name,
          "MS"
        );
      }

      //IS
      List<SInternalSystem> siList = this.SDA.SelectInternalSystemList(true, false);

      foreach (SInternalSystem si in siList)
      {
        this.DgvMS.Rows.Add
        (
          si.GUID,
          si.Display == "Y",
          si.ProgramID,
          si.TypedName,
          "IS"
        );
      }

      //ES
      List<SExternalSystem> seList = this.SDA.SelectExternalSystemList(true, false);

      foreach (SExternalSystem se in seList)
      {
        this.DgvMS.Rows.Add
        (
          se.GUID,
          se.Display == "Y",
          se.ProgramID,
          se.TypedName,
          "ES"
        );
      }
    }

    private void BtnSetFont_Click(object sender, EventArgs e)
    {
      this.fontDialog1.ShowColor = false;
      this.fontDialog1.ShowEffects = false;
      this.fontDialog1.Font = new Font(this.LblFont.Text, float.Parse(this.LblFontSize.Text));

      if (this.fontDialog1.ShowDialog() == DialogResult.OK)
      {
        this.LblFont.Text = this.fontDialog1.Font.FontFamily.Name;
        this.LblFontSize.Text = this.fontDialog1.Font.Size.ToString();
      }
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
      //--------------------------------
      //MyOption
      SCommon.LoggedInUser.TreeFont = this.LblFont.Text;
      SCommon.LoggedInUser.TreeFontSize = this.LblFontSize.Text;
      this.SDA.UpdateUserTreeOption(SCommon.LoggedInUser);

      //--------------------------------
      //NoDisplay
      //GUID(hidden), Display(checkbox), Code, Name, Type(hidden)
      this.SDA.DeleteNoDisplayListByUser(SCommon.LoggedInUser.GUID);

      foreach (DataGridViewRow row in this.DgvMS.Rows)
      {
        //this.SDA.UpdateMicroserviceForNoDisplayUser(row.Cells[0].Value as string, SCommon.LoggedInUser.GUID, Convert.ToBoolean(row.Cells[1].Value));
        if (Convert.ToBoolean(row.Cells[1].Value) == false)
          this.SDA.InsertNoDisplay(row.Cells[0].Value.ToString(), SCommon.LoggedInUser.GUID);
      }

      //--------------------------------
      this.TreeOptionChanged();
      this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

  }
}