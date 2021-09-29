namespace SDM.Common
{
  public class SCaller
  {
    public SComponentType ComponentType { get; set; }
    public string GUID { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public SCaller() { }

    public SCaller(SComponentType componentType, string guid, string fullName)
    {
      this.ComponentType = componentType;
      this.GUID = guid;
      this.FullName = fullName;
    }
  }
}
