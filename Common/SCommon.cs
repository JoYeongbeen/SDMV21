using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

using SDM.Component;
using SDM.Project;

namespace SDM.Common
{
  public class SCommon
  {
    public static string ProductYyyyMmDd = "2021.09.03";
    public static string ProductVersion { get { return string.Format("Service Definition Manager({0})", ProductYyyyMmDd); } }

    public static string DBFileName = "SDM_DB.sdm";
    public static string UserFileName = "SDM_USER.sdm";

    public static string DBFilePath { get; set; }

    public static TreeNode DraggedNodeFromTree;

    public static SProject SProject { get; set; }
    public static List<SPart> SPartList { get; set; }
    public static List<SUser> SUserList { get; set; }
    public static List<SMicroservice> SMicroserviceList { get; set; }

    public static int DEFAULT_FORM_WIDTH = 1014;
    public static int DEFAULT_FORM_HEIGHT = 646;
    public static int DEFAULT_SPLITTER_DISTANCE = 300;

    public static string RoleDesigner = "Designer";
    public static string RoleDeveloper = "Developer";
    public static string RolePL = "PL";
    public static string RoleModeler = "Modeler";
    public static string RoleQA = "QA";
    public static string RolePM = "PM";

    public static string WordKorean = "Korean";
    public static string WordEnglish = "English";
    public static string WordNoun = "Noun";
    public static string WordVerbBR = "VerbBR";
    public static string WordVerbDA = "VerbDA";

    public static string PartModeling = "modeling";
    public static string PartModelingName = "모델링";
    public static string UserModeler = "modeler";
    public static string UserModelerName = "모델러";
    public static string UserTemp = "temp";
    public static string UserTempName = "임시";

    public static string HttpMethodGet = "GET";
    public static string HttpMethodPost = "POST";
    public static string HttpMethodPut = "PUT";
    public static string HttpMethodDelete = "DELETE";

    public static string AbbrMicroservice = "M";
    public static string AbbrInternalSystem = "IS";
    public static string AbbrExternalSystem = "ES";
    public static string AbbrBizPackage = "BP";
    public static string AbbrController = "CT";
    public static string AbbrAPI = "A";
    public static string AbbrProducer = "PR";
    public static string AbbrPublisher = "P";
    public static string AbbrConsumer = "CS";
    public static string AbbrSubscriber = "S";
    public static string AbbrOther = "O";
    public static string AbbrDto = "D";
    public static string AbbrEntity = "E";
    public static string AbbrUI = "UI";
    public static string AbbrBizRule = "BR";
    public static string AbbrDataAccess = "DA";
    public static string AbbrOperation = "OP";
    public static string AbbrJob = "Job";
    public static string AbbrStep = "Step";

    public static string ALL = "ALL";

    #region User

    public static SUser LoggedInUser { get; set; }

    public static string GetRegistered(SComponent component)
    {
      return string.Format("{0} [{1}]{2}", component.RegisteredDate, component.RegisteredPartName, component.RegisteredUserName);
    }

    public static string GetLastModified(SComponent component)
    {
      return string.Format("{0} [{1}]{2}", component.LastModifiedDate, component.LastModifiedPartName, component.LastModifiedUserName);
    }

    public static string LoggedInUserDisplay
    {
      get
      {
        return 
          string.Format("[{0}]{1}({2}){3}",
            LoggedInUser.PartName,
            LoggedInUser.Name,
            LoggedInUser.Role,
            LoggedInUser.ViewOnly ? " - VIEW ONLY" : "");
      }
    }

    public static bool HasPermission(SComponent component)
    {
      bool permission = false;

      if (component == null)
        return false;

      //Modeler
      if (LoggedInUser.Role == SCommon.RoleModeler)
      {
        permission = true;
      }
      else
      {
        if (SCommon.LoggedInUser.Role == SCommon.RolePL)
        {
          //PL
          if (SCommon.LoggedInUser.PartGUID == component.LastModifiedPartGUID)
            permission = true;
        }
        else if (SCommon.LoggedInUser.Role == SCommon.RoleDesigner || SCommon.LoggedInUser.Role == SCommon.RoleDeveloper)
        {
          //Designer, Developer
          if (SCommon.LoggedInUser.GUID == component.RegisteredUserGUID || SCommon.LoggedInUser.GUID == component.LastModifiedUserGUID)
            permission = true;
        }
      }

      //신규의 경우
      if (component.LastModifiedUserGUID == null || (component.LastModifiedUserGUID != null && component.LastModifiedUserGUID.Length == 0))
        permission = true;

      //임시 권한자로 할당된 경우 누구나 수정할 수 있음
      if (component.LastModifiedPartGUID == SCommon.PartModeling && component.LastModifiedUserGUID == SCommon.UserTemp)
        permission = true;

      //QA, PM
      if (LoggedInUser.ViewOnly)
        permission = false;

      return permission;
    }

    public static bool IsModeler()
    {
      bool isModeler = false;

      return isModeler;
    }

    #endregion

    #region Registerd/Last Modified

    public static string GetDate()
    {
      return DateTime.Now.ToString("yyyy.MM.dd");
    }

    public static string GetDateForExport()
    {
      return DateTime.Now.ToString("yyyyMMdd.HHmm");
    }


    public static void SetDateDesigner(SComponent component, bool registered)
    {
      if (registered)
      {
        component.RegisteredDate = GetDate();
        component.RegisteredPartGUID = SCommon.LoggedInUser.PartGUID;
        component.RegisteredPartName = SCommon.LoggedInUser.PartName;
        component.RegisteredUserGUID = SCommon.LoggedInUser.GUID;
        component.RegisteredUserName = SCommon.LoggedInUser.Name;
      }

      component.LastModifiedDate = GetDate();
      component.LastModifiedPartGUID = SCommon.LoggedInUser.PartGUID;
      component.LastModifiedPartName = SCommon.LoggedInUser.PartName;
      component.LastModifiedUserGUID = SCommon.LoggedInUser.GUID;
      component.LastModifiedUserName = SCommon.LoggedInUser.Name;
    }

    public static void SetLastModifiedTemp(SComponent component)
    {
      component.LastModifiedDate = GetDate();
      component.LastModifiedPartGUID = SCommon.PartModeling;
      component.LastModifiedPartName = SCommon.PartModelingName;
      component.LastModifiedUserGUID = SCommon.UserTemp;
      component.LastModifiedUserName = SCommon.UserTempName;
    }



    #endregion

    #region Component

    public static string GetComponentAbbr(SComponent component, bool fullName = false)
    {
      string abbr = string.Empty;

      if (component is SMicroservice)
        abbr = fullName ? "Microservice" : "M";
      else if (component is SInternalSystem)
        abbr = fullName ? "InternalSystem" : "IS";
      else if (component is SExternalSystem)
        abbr = fullName ? "ExternalSystem" : "ES";
      else if (component is SBizPackage)
        abbr = fullName ? "BizPackage" : "BP";

      else if (component is SController)
        abbr = fullName ? "Controller" : "C";//CT CTR
      else if (component is SAPI)
        abbr = fullName ? "API" : "A";

      else if (component is SProducer)
        abbr = fullName ? "Producer" : "P";//PR
      else if (component is SPublisher)
        abbr = fullName ? "Publisher" : "P";

      else if (component is SConsumer)
        abbr = fullName ? "Consumer" : "C";//CS
      else if (component is SSubscriber)
        abbr = fullName ? "Subscriber" : "S";

      else if (component is SOther)
        abbr = fullName ? "Other" : "O";
      else if (component is SDto)
        abbr = fullName ? "Dto" : "D";

      else if (component is SEntity && (component as SEntity).JoinEntity)
        abbr = fullName ? "Entity" : "JE";
      else if (component is SEntity && (component as SEntity).JoinEntity == false)
        abbr = fullName ? "Entity" : "E";

      else if (component is SUI)
        abbr = fullName ? "UI" : "UI";

      else if (component is SBizRule && (component as SBizRule).CommonBR)
        abbr = fullName ? "BizRuleComponent" : "CBR";
      else if (component is SBizRule && (component as SBizRule).CommonBR == false)
        abbr = fullName ? "BizRuleComponent" : "BR";

      else if (component is SDataAccess && (component as SDataAccess).JoinDA)
        abbr = fullName ? "DataAccessComponent" : "JDA";
      else if (component is SDataAccess && (component as SDataAccess).JoinDA == false)
        abbr = fullName ? "DataAccessComponent" : "DA";

      else if (component is SBizRuleOperation || component is SDataAccessOperation)
        abbr = fullName ? "Operation" : "OP";

      else if (component is SJob)
        abbr = fullName ? "Job" : "Job";
      else if (component is SStep)
        abbr = fullName ? "Step" : "Step";

      return abbr;
    }

    public static string GetComponentAbbr(SComponentType componentType, bool fullName = false)
    {
      string abbr = string.Empty;

      if (componentType == SComponentType.Microservice)
        abbr = fullName ? "Microservice" : "M";
      else if (componentType == SComponentType.InternalSystem)
        abbr = fullName ? "InternalSystem" : "IS";
      else if (componentType == SComponentType.ExternalSystem)
        abbr = fullName ? "ExternalSystem" : "ES";
      else if (componentType == SComponentType.BizPackage)
        abbr = fullName ? "BizPackage" : "BP";

      else if (componentType == SComponentType.Controller)
        abbr = fullName ? "Controller" : "C";//CT CTR
      else if (componentType == SComponentType.API)
        abbr = fullName ? "API" : "A";

      else if (componentType == SComponentType.Producer)
        abbr = fullName ? "Producer" : "P";//PR
      else if (componentType == SComponentType.Publisher)
        abbr = fullName ? "Publisher" : "P";

      else if (componentType == SComponentType.Consumer)
        abbr = fullName ? "Consumer" : "C";//CS
      else if (componentType == SComponentType.Subscriber)
        abbr = fullName ? "Subscriber" : "S";

      else if (componentType == SComponentType.Other)
        abbr = fullName ? "Other" : "O";
      else if (componentType == SComponentType.Dto)
        abbr = fullName ? "Dto" : "D";

      else if (componentType == SComponentType.Entity)
        abbr = fullName ? "Entity" : "E";

      else if (componentType == SComponentType.UI)
        abbr = fullName ? "UI" : "UI";

      else if (componentType == SComponentType.BizRule)
        abbr = fullName ? "BizRuleComponent" : "BR";

      else if (componentType == SComponentType.DataAccess)
        abbr = fullName ? "DataAccessComponent" : "DA";

      else if (componentType == SComponentType.BizRuleOperation || componentType == SComponentType.DataAccessOperation)
        abbr = fullName ? "Operation" : "OP";

      else if (componentType == SComponentType.Job)
        abbr = fullName ? "Job" : "Job";
      else if (componentType == SComponentType.Step)
        abbr = fullName ? "Step" : "Step";

      return abbr;
    }

    public static string GetName(SComponent component)
    {
      string name = string.Empty;

      if (component != null)
      {
        if (component is SMicroservice || component is SBizPackage)
          name = component.Name;
        else
          name = component.Name.Replace(" ", "").Replace(",", "").Replace("/", "").Replace("_", "");
      }

      return name;
    }

    public static string GetName(string componentName, bool keepName = false)
    {
      if (keepName)
        return componentName;
      else
        return componentName.Replace(" ", "").Replace(",", "").Replace("/", "").Replace("_", "");
    }

    public static string GetDesription(SComponent component)
    {
      string description = string.Empty;

      if (string.IsNullOrEmpty(component.Description) == false)
        description = component.Description.Replace(Environment.NewLine, " [줄바꿈] ");

      return description;
    }

    public static string GetDesription(SComponent component, bool isHTML = false)
    {
      string description = string.Empty;

      if (component.Description != null)
      {
        if (isHTML)
          description = component.Description.Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br>");
        else
          description = component.Description.Replace(",", " / ").Replace("\r\n", " // ");
      }

      return description;
    }

    public static string GetSQL(SDataAccessOperation daOp)
    {
      string sql = string.Empty;

      if (daOp.SQL != null)
        sql = daOp.SQL.Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br>");

      if (SCommon.LoggedInUser.MonospacedFontDAOperationDesc)
        sql = "<div style='font-family:Courier New;'>" + sql + "</div>";

      return sql;
    }

    #endregion

    #region Create TreeNode

    public static bool ShowID = false;
    public static bool ShowEng = false;

    public static TreeNode CreateNode(SComponent component, bool isNew)
    {
      TreeNode node = new TreeNode();
      node.Tag = component;
      node.Text = GetNodeText(component);

      #region Node color and icon

      if (component is SMicroservice)
      {
        node.ImageKey = "MS";
        node.SelectedImageKey = "MS";
      }
      else if (component is SInternalSystem)
      {
        node.ImageKey = "SYS";
        node.SelectedImageKey = "SYS";
      }
      else if (component is SExternalSystem)
      {
        node.ImageKey = "SYS";
        node.SelectedImageKey = "SYS";
      }
      else if (component is SBizPackage)
      {
        node.ImageKey = "BP";
        node.SelectedImageKey = "BP";
      }
      else if (component is SController)
      {
        //node.ForeColor = Color.Blue;
        node.ImageKey = "CTR";
        node.SelectedImageKey = "CTR";
      }
      else if (component is SAPI)
      {
        //node.ForeColor = Color.Blue;
        node.ImageKey = "API";
        node.SelectedImageKey = "API";
      }
      else if (component is SProducer)
      {
        //node.ForeColor = Color.DarkGreen;
        node.ImageKey = "PRD";
        node.SelectedImageKey = "PRD";
      }
      else if (component is SPublisher)
      {
        //node.ForeColor = Color.DarkGreen;
        node.ImageKey = "PUB";
        node.SelectedImageKey = "PUB";
      }
      else if (component is SConsumer)
      {
        //node.ForeColor = Color.DarkGreen;
        node.ImageKey = "CSM";
        node.SelectedImageKey = "CSM";
      }
      else if (component is SSubscriber)
      {
        //node.ForeColor = Color.DarkGreen;
        node.ImageKey = "SUB";
        node.SelectedImageKey = "SUB";
      }
      else if (component is SOther)
      {
        //node.ForeColor = Color.Red;
        node.ImageKey = "OTHER";
        node.SelectedImageKey = "OTHER";
      }
      else if (component is SDto)
      {
        //node.ForeColor = Color.DarkGray;
        node.ImageKey = "DTO";
        node.SelectedImageKey = "DTO";
      }
      else if (component is SEntity)
      {
        //node.ForeColor = Color.DarkGray;
        node.ImageKey = "ENT";
        node.SelectedImageKey = "ENT";
      }
      else if (component is SUI)
      {
        //node.ForeColor = Color.Pink;
        node.ImageKey = "UI";
        node.SelectedImageKey = "UI";
      }
      else if (component is SBizRule)
      {
        node.ImageKey = "BR";
        node.SelectedImageKey = "BR";
      }
      else if (component is SDataAccess)
      {
        node.ImageKey = "DA";
        node.SelectedImageKey = "DA";
      }
      else if (component is SBizRuleOperation || component is SDataAccessOperation)
      {
        node.ImageKey = "OP";
        node.SelectedImageKey = "OP";
      }
      else if (component is SJob)
      {
        node.ImageKey = "JOB";
        node.SelectedImageKey = "JOB";
      }
      else if (component is SStep)
      {
        node.ImageKey = "STEP";
        node.SelectedImageKey = "STEP";
      }

      #endregion

      if (isNew)
      {
        //SetDateDesigner(component, true);
      }
      else
      {
        if (SCommon.HasPermission(component) == false)
          node.ForeColor = Color.LightGray;
      }

      return node;
    }

    public static string GetNodeText(SComponent component)
    {
      StringBuilder nodeText = new StringBuilder();

      //1) 기본설계 완료 > ○ / 상세설계 완료 > ●
      //if (component.DesignCompleteSkeleton && component.DesignCompleteDetail == false)
      //  nodeText.Append("○");
      //else if (component.DesignCompleteSkeleton && component.DesignCompleteDetail)
      //  nodeText.Append("●");
      nodeText.Append(GetDesignCompleteDisplay(component));

      //2) ID
      if (ShowID)
        nodeText.Append(string.Format("[{0}]", component.ProgramID));

      //3) Name
      nodeText.Append(GetName(component));

      //4) English
      if (ShowEng)
        nodeText.Append(" " + component.NameEnglish);

      return nodeText.ToString();
    }

    public static string GetDesignCompleteDisplay(SComponent component)
    {
      string display = string.Empty;

      if (component.DesignCompleteSkeleton && component.DesignCompleteDetail == false)
        display = "○";
      else if (component.DesignCompleteSkeleton && component.DesignCompleteDetail)
        display = "●";

      return display;
    }

    #endregion

    #region Spec File

    public static string GetSpecFilePath()
    {
      string fileName = string.Empty;

      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.Filter = "office files(*.docx; *.xlsx; *.pptx)|*.docx; *.xlsx; *.pptx";
        openFileDialog.RestoreDirectory = false;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
          fileName = openFileDialog.FileName;
      }

      return fileName;
    }

    public static void OpenDocument(string filePath, bool isURL = false)
    {
      if (isURL)
      {
        System.Diagnostics.Process.Start(filePath);
      }
      else
      {
        if (System.IO.File.Exists(filePath))
          System.Diagnostics.Process.Start(filePath);
        else
          SMessageBox.ShowWarning(SMessage.FILE_NOT_FOUND);
      }

    }

    #endregion

    public static void SelectAllAndFocus(TextBox tbx)
    {
      tbx.SelectAll();
      tbx.Focus();
    }

    public static void SetMonospacedFont(bool monoSpaced, TextBox tbx)
    {
      if (monoSpaced)
        tbx.Font = new Font("Courier New", float.Parse("9"));
      else
        tbx.Font = new Font("굴림", float.Parse("9"));
    }


    public static void SetBizPartCombo(ComboBox cmb, bool displayAll = false)
    {
      cmb.Items.Clear();

      foreach (SPart part in SCommon.SPartList)
        cmb.Items.Add(part);

      if (displayAll)
      {
        cmb.Items.Insert(0, ALL);
        cmb.SelectedIndex = 0;
      }
    }

    public static void SetMicroserviceCombo(ComboBox cmb, bool displayAll = false)
    {
      cmb.Items.Clear();

      foreach (SMicroservice ms in SCommon.SMicroserviceList)
        cmb.Items.Add(ms);

      if (displayAll)
      {
        cmb.Items.Insert(0, ALL);
        cmb.SelectedIndex = 0;
      }
    }

    public static void SetUserCombo(ComboBox cmb)
    {
      cmb.Items.Clear();

      foreach (SUser user in SCommon.SUserList)
        cmb.Items.Add(user);
    }
    

    public static void SetTbxInputOutput(TextBox tbx, string tag, string text, string fullPath)
    {
      tbx.Tag = tag;

      if (tag != null && tag.Length > 0)
      {
        tbx.Text = fullPath;
        tbx.BackColor = Color.LightGray;
        tbx.ReadOnly = true;
      }
      else
      {
        tbx.Text = text;
        tbx.BackColor = Color.White;
        tbx.ReadOnly = false;
      }
    }

    public static bool ConvertStringToBoolean(string value)
    {
      bool result = false;

      if (value != null && value.Length > 0)
      {
        if (value.ToLower() == "true")
          result = true;
      }

      return result;
    }


    public static bool IsEnglishOrNumber(string text)
    {
      //Regex english = new Regex(@"[a-zA-Z]");//영문
      //Regex number = new Regex(@"[0-9]");//숫자
      //return english.IsMatch(text) || number.IsMatch(text);

      return Regex.IsMatch(text, @"^[a-zA-Z0-9]+$");
    }


    public static bool GetBoolean(object value)
    {
      bool result = false;

      if (value == null)
        return result;

      if (value is DBNull)
        return result;

      if (value.ToString() == "0")
        result = false;//0 false
      else if (value.ToString() == "1")
        result = true;//1 true
      else
        result = Convert.ToBoolean(value);

      return result;
    }


    public static string GetCamelCasing(string value)
    {
      string camelCasing = value;

      if (value.Length > 0)
      {
        for (int i = 0; i < value.Length; i++)
        {
          if (i == 0)
            camelCasing = value[i].ToString().ToLower();
          else
            camelCasing += value[i].ToString();
        }
      }

      return camelCasing;
    }

    public static string GetPascalCasing(string value)
    {
      string pascalCasing = value;

      if (value.Length > 0)
      {
        for (int i = 0; i < value.Length; i++)
        {
          if (i == 0)
            pascalCasing = value[i].ToString().ToUpper();
          else
            pascalCasing += value[i].ToString().ToLower();
        }
      }

      return pascalCasing;
    }
  }
}
