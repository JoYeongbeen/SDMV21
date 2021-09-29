using System;
using System.Collections.Generic;

using SDM.Common;

namespace SDM.Component
{
  public class SBizRuleOperation : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string BizRuleGUID { get; set; } = string.Empty;
    public string BizRuleName { get; set; } = string.Empty;

    public bool Tx { get; set; } = false; //Transaction 은 DB의 예약어로 사용 불가

    public string InputGUID { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;

    public string OutputGUID { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;

    public List<SCallee> CalleeList { get; set; } = new List<SCallee>();
  }
}
