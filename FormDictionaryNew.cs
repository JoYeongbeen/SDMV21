using System;
using System.Windows.Forms;
using System.Data;

using SDM.Common;

namespace SDM
{
  public partial class FormDictionaryNew : Form
  {
    public FormDictionaryNew()
    {
      InitializeComponent();
    }

    private SBR SBR;
    private SDA SDA;

    private void FormDictionaryNew_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Dictionary";

      this.SBR = new SBR();
      this.SDA = new SDA();

      this.CmbType.Items.Add(SCommon.WordNoun);
      this.CmbType.Items.Add(SCommon.WordVerbBR);
      this.CmbType.Items.Add(SCommon.WordVerbDA);
      this.CmbType.Text = SCommon.WordNoun;

      this.SearchWord();

      if (SCommon.LoggedInUser.Role != SCommon.RoleModeler)
        this.Width = this.Width - 250;
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
      this.SearchWord();
    }

    private void SearchWord()
    {
      DataTable wordList = this.SDA.SelectWordListDataTable(this.CmbType.Text, this.TbxSearchKorean.Text, this.TbxSearchEnglish.Text);
      this.DgvWord.DataSource = wordList;
    }

    private void DgvWord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.DgvWord.SelectedRows.Count == 1)
      {
        this.TabWord.Text = this.CmbType.Text;

        this.TbxKorean.Text = this.DgvWord.SelectedRows[0].Cells[0].Value.ToString();
        this.TbxEnglish.Text = this.DgvWord.SelectedRows[0].Cells[1].Value.ToString();

        this.TbxKorean.Enabled = false;
        this.TbxEnglish.Focus();
      }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
      this.TbxKorean.Text = string.Empty;
      this.TbxEnglish.Text = string.Empty;

      this.TbxKorean.Enabled = true;
      this.TbxKorean.Focus();
    }

    private void BtnSaveSingle_Click(object sender, EventArgs e)
    {
      string type = this.CmbType.Text;
      string korean = this.TbxKorean.Text.Trim();
      string english = this.TbxEnglish.Text.Trim();

      if (korean.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "한글");
        this.TbxKorean.Focus();
        return;
      }

      if (english.Length == 0)
      {
        SMessageBox.ShowWarning(SMessage.REQUIRED, "English");
        this.TbxEnglish.Focus();
        return;
      }

      if(SCommon.IsEnglishOrNumber(english) == false)
      {
        SMessageBox.ShowWarning(SMessage.CAN_BE_ENG_OR_NO);
        this.TbxEnglish.Focus();
        return;
      }

      DataRow row = this.SDA.SelectWord(type, korean);

      if(row != null)
      {
        SMessageBox.ShowWarning(SMessage.IS_DUPLICATED, korean);
        this.TbxKorean.Focus();
        return;
      }

      this.SDA.InsertWord(type, korean, english);

      SMessageBox.ShowInformation(SMessage.SAVED);
      this.SearchWord();
    }

    private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TabWord.Text = this.CmbType.Text;
      this.SearchWord();
    }

    private void TbxSearchKorean_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchWord();
    }

    private void TbxSearchEnglish_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SearchWord();
    }

    private void BtnSaveMulti_Click(object sender, EventArgs e)
    {
      int count = 0;
      string type = this.CmbType.Text;
      string[] inputList = this.TbxInput.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

      foreach (string input in inputList)
      {
        string[] wordKorEng = input.Split(',');

        if (wordKorEng.Length == 2)
        {
          string korean = wordKorEng[0];
          string english = wordKorEng[1];

          DataRow row = this.SDA.SelectWord(type, korean);

          if (row == null)
          {
            this.SDA.InsertWord(type, korean, english);
            count += 1;
          }
          else
          {
            SMessageBox.ShowWarning(SMessage.IS_DUPLICATED, korean);
          }
        }
      }

      SMessageBox.ShowInformation(SMessage.SAVED_COUNT, count.ToString());
      this.SearchWord();
    }
  }
}
