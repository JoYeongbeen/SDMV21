using System;

using SDM.Common;

namespace SDM.Component
{
  public class SStep : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string JobGUID { get; set; } = string.Empty;
    public string JobName { get; set; } = string.Empty;
  }
}
