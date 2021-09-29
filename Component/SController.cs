using System;

namespace SDM.Component
{
  public class SController : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;

    public string URI { get; set; } = string.Empty;
  }
}
