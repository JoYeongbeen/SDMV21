using System;
using System.IO;
using System.Windows.Forms;

using SDM.Common;
using SDM.Project;

namespace SDM
{
  public partial class FormLogin : Form
  {
    private bool loginSuccess = false;

    public event LoggedInHandler LoggedIn;
    public delegate void LoggedInHandler(bool refreshTree);

    private SBR SBR;
    private SDA SDA;

    public FormLogin()
    {
      InitializeComponent();
    }

    private void FormLogin_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Login";

      this.SBR = new SBR();
      this.SDA = new SDA();

      //SCommon.SPartList = this.SDA.SelectPartList();
      //SCommon.SUserList = this.SDA.SelectUserListAll();
      //MessageBox.Show("User : " + SCommon.SUserList.Count.ToString());
      
      this.LblProjectName.Text = SCommon.SProject.Name;
      
      if(SCommon.SProject.LogoURL != null && SCommon.SProject.LogoURL.Length > 0)
        this.ProjectIconPictureBox.ImageLocation = SCommon.SProject.LogoURL;
      else
        this.ProjectIconPictureBox.ImageLocation = "https://www.lgcns.com/static/images/header_logo.png";

      SCommon.SetBizPartCombo(this.CmbPart);
      //this.CmbPart.Focus();

      this.SetSavedPartAndUser();

      this.TbxPassword.Focus();
    }

    private void SetSavedPartAndUser()
    {
      this.CbxSaveName.Checked = false;

      if (File.Exists(SCommon.UserFileName) == false)
        return;

      string savedPartAndUserName = File.ReadAllText(SCommon.UserFileName);

      if (savedPartAndUserName.Length == 0)
        return;

      string[] partUser = savedPartAndUserName.Split(new string[] { "|" }, StringSplitOptions.None);

      if (partUser.Length == 2)
      {
        this.CbxSaveName.Checked = true;

        this.CmbPart.Text = partUser[0];
        this.TbxName.Text = partUser[1];
        this.TbxName.Focus();
      }
    }

    #region Login

    private void TbxName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.Login();
    }

    private void TbxPassword_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.Login();
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
      this.Login();
    }

    private void Login()
    {
      if(this.CmbPart.SelectedItem == null)
      {
        SMessageBox.ShowWarning(SMessage.SELECT_PART);
        this.CmbPart.Focus();
        return;
      }

      //1단계 : 입력한 이름으로 해당 파트의 사용자를 조회할 수 있어야 로그인 됨
      SUser selectedUser = this.SBR.SelectUserListByPartGUIDAndName((this.CmbPart.SelectedItem as SPart).GUID, this.TbxName.Text);

      if (selectedUser == null)
      {
        SMessageBox.ShowWarning(SMessage.CHECK_LOGIN_PART_NAME);
        this.TbxName.Focus();
        return;
      }

      //2단계 : 패스워드를 설정해둔 경우 이름과 패스워드까지 맞아야 함
      if (selectedUser.Password != null && selectedUser.Password.Length > 0)
      {
        if (selectedUser.Password != this.TbxPassword.Text)
        {
          SMessageBox.ShowWarning(SMessage.CHECK_LOGIN_PART_NAME_PW);
          this.TbxPassword.Focus();
          return;
        }
      }

      SCommon.LoggedInUser = selectedUser;
      SCommon.LoggedInUser.PartName = (this.CmbPart.SelectedItem as SPart).Name;

      this.loginSuccess = true;
      this.LoggedIn(this.loginSuccess);

      this.SaveUserFile();

      this.Close();
    }

    private void SaveUserFile()
    {
      string partUser = string.Empty;

      if (this.CbxSaveName.Checked)
        partUser = string.Format("{0}|{1}", this.CmbPart.Text, this.TbxName.Text);

      File.WriteAllText(SCommon.UserFileName, partUser);
    }

    private void BtnLoginTest_Click(object sender, EventArgs e)
    {
      //SCommon.LoggedInUser = this.SBR.GetDefaultModeler();

      //this.loginSuccess = true;
      //this.LoggedIn(this.loginSuccess);
      //this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.LoggedIn(this.loginSuccess);
      this.Close();
    }

    private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.LoggedIn(this.loginSuccess);
    }

    #endregion


    #region 로그인의 사용자 콤보 - 현재는 미사용

    private void CmbPart_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TbxName.Focus();
    }

    /// <summary>
    /// 현재는 파트선택하고 입력한 이름이 해당 파트에 소속된 사용자로 조회되면 로그인 성공으로, 향후 필요한 경우 콤보로 변경
    /// </summary>
    private void CmbUser_SelectedIndexChanged(object sender, EventArgs e)
    {
      //this.SelectedUser = this.CmbUser.SelectedItem as SUser;

      //if (this.SelectedUser != null && this.SelectedUser.Password.Length > 0)
      //  this.TbxPassword.Enabled = true;
      //else
      //  this.TbxPassword.Enabled = false;
    }


    #endregion

  }
}
