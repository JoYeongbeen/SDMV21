using System;

using SDM.Common;

namespace SDM.Component
{
  public class SDataAccessOperation : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string DataAccessGUID { get; set; } = string.Empty;
    public string DataAccessName { get; set; } = string.Empty;

    public string InputGUID { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;

    public string OutputGUID { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;

    public string SQL { get; set; } = string.Empty;
  }
}
