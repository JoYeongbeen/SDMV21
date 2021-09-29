using System;
using System.Windows.Forms;

using SDM.Common;

namespace SDM
{
  static class Program
  {
    /// <summary>
    /// 해당 응용 프로그램의 주 진입점입니다.
    /// </summary>
    [STAThread]
    static void Main()
    {
      //try
      //{

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        Application.Run(new FormMain());
        //Application.Run(new FormDictionaryNew());

      //}
      //catch(System.IO.IOException ex)
      //{
      //SMessageBox.ShowWarning(ex.Message);
      //}

    }
  }
}
