using System;

using SDM.Common;

namespace SDM.Component
{
  public class SEntityAttribute : SComponent
  {
    public string MicroserviceGUID { get; set; } = string.Empty;
    public string MicroserviceName { get; set; } = string.Empty;
    public string BizPackageGUID { get; set; } = string.Empty;
    public string BizPackageName { get; set; } = string.Empty;
    public string EntityGUID { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;

    //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
    //ID : ProgramID
    public string DataType { get; set; } = string.Empty;
    //Attribute : Name
    //Variable : NameEnglish

    public bool PK { get; set; }
    public bool FK { get; set; }
    public bool NN { get; set; }
    public string DBDataType { get; set; } = string.Empty;
    public string DBColumn { get; set; } = string.Empty;
    //Description
  }
}
