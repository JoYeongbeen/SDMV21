using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using SDM.Common;

namespace SDM
{
  public partial class FormCallerCallee : Form
  {
    public event MoveComponentHandler MoveComponentFromCaller;
    public delegate void MoveComponentHandler(string guid);

    //public List<SCaller> CallerList;
    //public string SelectedComponentFullPath;

    public FormCallerCallee()
    {
      InitializeComponent();
    }

    private void FormCallerCallee_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Caller/Callee";

      this.toolTip1.SetToolTip(this.BtnMoveRight, SMessage.MOVE_RIGHT);
    }

    public void SetData(string selectedComponentFullPath, List<SCaller> callerList)
    {
      this.LblComponentFullPath.Text = selectedComponentFullPath;// this.SelectedComponentFullPath;

      this.LbxMessageList.DataSource = callerList; // this.CallerList;
      this.LbxMessageList.DisplayMember = "FullName";
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void LbxMessageList_DoubleClick(object sender, EventArgs e)
    {
      if (this.LbxMessageList.SelectedItem != null)
      {
        SCaller caller = this.LbxMessageList.SelectedItem as SCaller;
        this.MoveComponentFromCaller(caller.GUID);
      }
    }

    private void BtnMoveRight_Click(object sender, EventArgs e)
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form.GetType() == typeof(FormMain))
        {
          this.Location = new Point(form.Location.X + form.Width - 15, form.Location.Y);
          this.Height = form.Height;
        }
      }
    }
  }
}
