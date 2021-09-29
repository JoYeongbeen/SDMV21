using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormMyOption : Form
  {
    //private bool RefreshTree = false;
    //Password를 제외한 Tree font, DA Op font, MS display 대부분 Tree refresh가 필요하여 일단 모두 true로
    //private bool RefreshTree = true;

    //public event MyOptionChangeHandler MyOptionChanged;
    //public delegate void MyOptionChangeHandler(bool refreshTree);

    private SDA SDA;

    public FormMyOption()
    {
      InitializeComponent();
    }

    private void FormMyOption_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - My Option";

      this.SDA = new SDA();

      this.LblUser.Text = SCommon.LoggedInUserDisplay;

      //this.TbxPassword1.Text = SCommon.LoggedInUser.Password;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      //패스워드 지정 후 다시 미지정할 경우 password와 confirm password 미입력후 저장 가능해야 하므로
      //if(this.TbxPassword1.Text.Length == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.INPUT_PW);
      //  this.TbxPassword1.Focus();
      //  return;
      //}

      //if (this.TbxPassword2.Text.Length == 0)
      //{
      //  SMessageBox.ShowWarning(SMessage.INPUT_PW);
      //  this.TbxPassword2.Focus();
      //  return;
      //}

      if (this.TbxPassword1.Text != this.TbxPassword2.Text)
      {
        SMessageBox.ShowWarning(SMessage.NO_MATCH_PW);
        this.TbxPassword1.Focus();
        return;
      }

      SCommon.LoggedInUser.Password = this.TbxPassword2.Text;
      this.SDA.UpdateUserPassword(SCommon.LoggedInUser);

      this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      //this.MyOptionChanged(false);
      this.Close();
    }

  }
}