using System;

namespace SDM.Component
{
  public class SJob : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;

    public string Schedule { get; set; } = string.Empty; //Daily, Weekly, Monthly
    public string Start { get; set; } = string.Empty; //hh:mm:ss
  }
}
