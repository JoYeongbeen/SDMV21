using System;

using SDM.Common;

namespace SDM.Component
{
  public class SAPI : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string InternalSystemGUID { get; set; } = string.Empty;
    public string InternalSystemName { get; set; } = string.Empty;
    public string ExternalSystemGUID { get; set; } = string.Empty;
    public string ExternalSystemName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string ControllerGUID { get; set; } = string.Empty;
    public string ControllerName { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = string.Empty;
    public string URI { get; set; } = string.Empty;

    public string InputGUID { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;

    public string OutputGUID { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;

    public string CalleeBROperationGUID { get; set; } = string.Empty;
  }
}
