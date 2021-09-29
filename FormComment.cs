using System;
using System.Windows.Forms;

namespace SDM
{
  public partial class FormComment : Form
  {
    public int SelectedIndex = 0;
    public string Comment = string.Empty;

    public delegate void CommentChangeHandler(int selectedIndex, string comment);
    public event CommentChangeHandler CommentChanged;

    public FormComment()
    {
      InitializeComponent();
    }

    private void FormComment_Load(object sender, EventArgs e)
    {
      this.TbxComment.Text = this.Comment;
      this.TbxComment.SelectAll();
      this.TbxComment.Focus();
    }

    private void TbxComment_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.SaveComment();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.SaveComment();
    }

    private void SaveComment()
    {
      this.CommentChanged(this.SelectedIndex, this.TbxComment.Text);
      this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
