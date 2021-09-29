using System;
using System.Text.Json.Serialization;

using SDM.Common;
using SDM.Component;

namespace SDM.Project
{
  public class SUser : SComponent
  {
    public string PartName { get; set; } = string.Empty;
    public string PartGUID { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool ViewOnly { get; set; } = false;

    public int Width { get; set; } = SCommon.DEFAULT_FORM_WIDTH;
    public int Height { get; set; } = SCommon.DEFAULT_FORM_HEIGHT;
    public int SplitterDistance { get; set; } = SCommon.DEFAULT_SPLITTER_DISTANCE;
    
    //Location지정에 따라 모니터2개, Main 여부 등 다양한 조건에 따라 제대로 뜨지 않는 경우를 모두 제어하기 어려움
    //그냥 CenterScreen 으로 띄우기
    //public int LocationX { get; set; } = 150;
    //public int LocationY { get; set; } = 150;

    public string TreeFont { get; set; } = "맑은 고딕";
    public string TreeFontSize { get; set; } = "9.75";

    public string ExpandType { get; set; } = string.Empty;
    public bool ShowID { get; set; } = false;
    public bool ShowEng { get; set; } = false;
    public string SortType { get; set; } = string.Empty;

    //public bool MonospacedFontBROperationDesc { get; set; } = false;//BR DESC 상단 놓는곳 위치가 애매함, 향후 필요한 경우 추가
    public bool MonospacedFontDAOperationDesc { get; set; } = false;
  }
}