using System;
using System.Windows.Forms;

using SDM.Common;

namespace SDM
{
  public partial class FormHelp : Form
  {
    public FormHelp()
    {
      InitializeComponent();
    }

    private void FormHelp_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Help";
      this.LblContact.Text = SCommon.SProject.Contact;

      this.TbxHistory.Text =
@"
2021.09.03 Dashboard 보완
2021.08.26 단어사전도 DB로 저장
2021.08.23 SDM Version2(with SQLite) 배포
";
    }
  }
}
