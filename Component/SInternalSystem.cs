using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SDM.Component
{
  public class SInternalSystem : SComponent
  {
    public string Display { get; set; } = string.Empty;

    public List<SCallee> CalleeList { get; set; } = new List<SCallee>();

    [JsonIgnore]
    public string TypedName
    {
      get
      {
        return string.Format("[IS]{0}", this.Name);

        //if (this.Name != null && this.Name.Length > 0)
        //  return string.Format("[IS]{0}", this.Name);
        //else
        //  return string.Empty;
      }
    }

    public SInternalSystem() {}

    public SInternalSystem(string guid, string name)
    {
      this.GUID = guid;
      this.Name = name;
    }

  }
}
