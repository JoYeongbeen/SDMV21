using System;

namespace SDM.Component
{
  public class SComponent
  {
    public string GUID { get; set; } = string.Empty;

    public string ProgramID { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NameEnglish { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SpecFile { get; set; } = string.Empty;

    public string RegisteredDate { get; set; } = string.Empty;
    public string RegisteredPartGUID { get; set; } = string.Empty;
    public string RegisteredPartName { get; set; } = string.Empty;
    public string RegisteredUserGUID { get; set; } = string.Empty;
    public string RegisteredUserName { get; set; } = string.Empty;

    public string LastModifiedDate { get; set; } = string.Empty;
    public string LastModifiedPartGUID { get; set; } = string.Empty;
    public string LastModifiedPartName { get; set; } = string.Empty;
    public string LastModifiedUserGUID { get; set; } = string.Empty;
    public string LastModifiedUserName { get; set; } = string.Empty;

    public bool DesignCompleteSkeleton { get; set; } = false;
    public bool DesignCompleteDetail { get; set; } = false;
  }
}