using System;

using SDM.Common;

namespace SDM.Component
{
  public class SOther : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string InternalSystemGUID { get; set; } = string.Empty;
    public string InternalSystemName { get; set; } = string.Empty;
    public string ExternalSystemGUID { get; set; } = string.Empty;
    public string ExternalSystemName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;

    public string SenderReceiver { get; set; } = string.Empty;

    public SComponentType SystemType { get; set; }
    public string SystemGUID { get; set; } = string.Empty;
    public string SystemName { get; set; } = string.Empty; //Join only

    public string Type { get; set; } = string.Empty;
    public string TypeText { get; set; } = string.Empty;

    public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;
  }
}
