using System;

namespace SDM.Component
{
  public class SEntity : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;

    public bool JoinEntity { get; set; } = false;
    public string TableName { get; set; } = string.Empty;
  }
}
