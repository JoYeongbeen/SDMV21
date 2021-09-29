using System;
using System.Windows.Forms;

namespace SDM.Common
{
  public class SMessageBox
  {
    public static void ShowInformation(string message, params string[] parameters)
    {
      if (parameters != null && parameters.Length > 0)
        MessageBox.Show(string.Format(message, parameters), SCommon.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Information);
      else
        MessageBox.Show(message, SCommon.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void ShowWarning(string message, params string[] parameters)
    {
      if (parameters != null && parameters.Length > 0)
        MessageBox.Show(string.Format(message, parameters), SCommon.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      else
        MessageBox.Show(message, SCommon.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static DialogResult ShowConfirm(string message, params string[] parameters)
    {
      if (parameters != null && parameters.Length > 0)
        return MessageBox.Show(string.Format(message, parameters), SCommon.ProductVersion, MessageBoxButtons.YesNo);
      else
        return MessageBox.Show(message, SCommon.ProductVersion, MessageBoxButtons.YesNo);
    }
  }
}
