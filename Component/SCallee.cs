using System.Text.Json.Serialization;

using SDM.Common;

namespace SDM.Component
{
  public class SCallee
  {
    public SComponentType ParentComponentType { get; set; }
    public string ParentGUID { get; set; } = string.Empty;

    public SComponentType CalleeComponentType { get; set; }
    public string CalleeGUID { get; set; } = string.Empty;
    public string CalleeFullPath { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

    public SCallee() { }

    public SCallee(SComponentType parentComponentType, string parentGUID, SComponentType calleeComponentType, string calleeGUID, string calleeFullPath, string comment)
    {
      this.ParentComponentType = parentComponentType;
      this.ParentGUID = parentGUID;

      this.CalleeComponentType = calleeComponentType;
      this.CalleeGUID = calleeGUID;
      this.CalleeFullPath = calleeFullPath;
      this.Comment = comment;
    }

    public SCallee(SComponentType parentComponentType, string parentGUID, string comment)
    {
      this.ParentComponentType = parentComponentType;
      this.ParentGUID = parentGUID;

      //this.CalleeComponentType = calleeComponentType;
      //this.CalleeGUID = calleeGUID;
      //this.CalleeFullPath = calleeFullPath;
      this.Comment = comment;
    }

    public string CalleeWithComment
    {
      get
      {
        string fullName = this.CalleeFullPath;

        if (this.Comment != null && this.Comment.Length > 0)
        {
          if (fullName.Length > 0)
            fullName += " ";

          fullName += "▶ " + this.Comment;
        }

        return fullName;
      }
    }
  }
}
