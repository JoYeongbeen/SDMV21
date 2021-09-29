using System;

using SDM.Common;

namespace SDM.Component
{
  public class SDtoAttribute : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string DtoGUID { get; set; } = string.Empty;
    public string DtoName { get; set; } = string.Empty;

    public string DataType { get; set; } = string.Empty;
  }
}
