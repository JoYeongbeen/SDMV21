using System.Collections.Generic;

namespace SDM.Component
{
  public class SMicroservice : SComponent
  {
    public string BizPartGUID { get; set; } = string.Empty;
    public string BizPartName { get; set; } = string.Empty;

    public string Display { get; set; } = string.Empty;
  }
}