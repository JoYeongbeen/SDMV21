﻿using System;

using SDM.Common;

namespace SDM.Component
{
  public class SPublisher : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string InternalSystemGUID { get; set; } = string.Empty;
    public string InternalSystemName { get; set; } = string.Empty;
    public string ExternalSystemGUID { get; set; } = string.Empty;
    public string ExternalSystemName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string ProducerGUID { get; set; } = string.Empty;
    public string ProducerName { get; set; } = string.Empty;

    public string InputGUID { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;

    public string Topic { get; set; } = string.Empty;
  }
}
