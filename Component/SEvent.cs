using System;

namespace SDM.Component
{
  public class SEvent : SComponent
  {
    public string UIGUID { get; set; } = string.Empty;

    public string CalleeApiGuid { get; set; }

    public SEvent() { }

    public SEvent(string uiGUID, string id, string name, string calleeApiGuid)
    {
      this.GUID = Guid.NewGuid().ToString();

      this.UIGUID = uiGUID;

      this.ProgramID = id;
      this.Name = name;
      this.CalleeApiGuid = calleeApiGuid;
    }
  }
}
