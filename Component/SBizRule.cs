using System;

using SDM.Common;

namespace SDM.Component
{
  public class SBizRule : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;

    public bool CommonBR { get; set; } = false;
  }
}
