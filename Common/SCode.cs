using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

using SDM.Component;

namespace SDM.Common
{
  public class SCode
  {
    private SBR SBR;
    private SDA SDA;

    private string TemplateController;
    private string TemplateAPI;

    private string TemplateProducer;
    private string TemplatePublisher;

    private string TemplateConsumer;
    private string TemplateSubscriber;

    private string TemplateBR;
    private string TemplateBROperation;

    private string TemplateDA;
    private string TemplateDAOperation;

    private string TemplateSQL;
    private string TemplateSQLOperation;

    private string TemplateDto;
    private string TemplateDtoAttribute;

    private string TemplateEntity;
    private string TemplateEntityAttribute;

    private string DescTypeClass = "CLASS";
    private string DescTypeOperation = "OPERATION";
    private string DescTypeSQL = "SQL";

    public SCode()
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
    }

    public void GenerateCode(SComponent component)
    {
      string codeFolder = SCommon.SProject.ProjectFolder + @"\\template_code";

      if (Directory.Exists(codeFolder) == false)
      {
        SMessageBox.ShowWarning(SMessage.NO_PROJECT_FOLDER, "template_code");
        return;
      }

      this.TemplateController = File.ReadAllText(codeFolder + @"\\controller.java");
      this.TemplateAPI = File.ReadAllText(codeFolder + @"\\controller_operation.java");

      this.TemplateProducer = File.ReadAllText(codeFolder + @"\\producer.java");
      this.TemplatePublisher = File.ReadAllText(codeFolder + @"\\producer_operation.java");

      this.TemplateConsumer = File.ReadAllText(codeFolder + @"\\consumer.java");
      this.TemplateSubscriber = File.ReadAllText(codeFolder + @"\\consumer_operation.java");

      this.TemplateBR = File.ReadAllText(codeFolder + @"\\br.java");
      this.TemplateBROperation = File.ReadAllText(codeFolder + @"\\br_operation.java");

      this.TemplateDA = File.ReadAllText(codeFolder + @"\\da.java");
      this.TemplateDAOperation = File.ReadAllText(codeFolder + @"\\da_operation.java");

      this.TemplateSQL = File.ReadAllText(codeFolder + @"\\da_sql.xml");
      this.TemplateSQLOperation = File.ReadAllText(codeFolder + @"\\da_sql_operation.xml");

      this.TemplateDto = File.ReadAllText(codeFolder + @"\\dto.java");
      this.TemplateDtoAttribute = File.ReadAllText(codeFolder + @"\\dto_attribute.java");

      this.TemplateEntity = File.ReadAllText(codeFolder + @"\\entity.java");
      this.TemplateEntityAttribute = File.ReadAllText(codeFolder + @"\\entity_attribute.java");

      string code = string.Empty;

      if (component is SController)
        code = this.GetController(component as SController);

      else if (component is SProducer)
        code = this.GetProducer(component as SProducer);
      else if (component is SConsumer)
        code = this.GetConsumer(component as SConsumer);

      else if (component is SBizRule)
        code = this.GetBR(component as SBizRule);

      else if (component is SDataAccess)
        code = this.GetDA(component as SDataAccess);

      else if (component is SDto)
        code = this.GetDto(component as SDto);
      else if (component is SEntity)
        code = this.GetEntity(component as SEntity);

      this.SaveCodeFile(component, code);

      //SQL
      if (component is SDataAccess)
      {
        DialogResult result = SMessageBox.ShowConfirm(SMessage.GENERATE_SQL);

        if (result == DialogResult.Yes)
        {
          code = this.GetSQL(component as SDataAccess);
          this.SaveCodeFile(component, code, "xml");
        }
      }
    }

    private void SaveCodeFile(SComponent component, string code, string fileType = "java")
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.FileName = string.Format("{0}.{1}", component.NameEnglish, fileType);
        saveFileDialog.Filter = string.Format("{0} files (*.{0})|*.{0}", fileType);

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
          if (saveFileDialog.FileName.Length > 0)
          {
            File.WriteAllText(saveFileDialog.FileName, code);
            SCommon.OpenDocument(saveFileDialog.FileName);
          }
        }
      }
    }

    #region Controller

    private string GetController(SController controller)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(controller.BizPackageGUID);
      List<SAPI> apiList = this.SBR.SelectAPIListByParent(controller);

      //List<SBizRule> calleeBRList = new List<SBizRule>();
      List<string> calleeBRGUIDList = new List<string>();

      string brList = string.Empty;

      foreach (SAPI api in apiList)
      {
        SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, api.CalleeBROperationGUID) as SBizRuleOperation;

        if (calleeBROp != null)
        {
          SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

          if (calleeBRGUIDList.Contains(calleeBR.GUID) == false)
          {
            calleeBRGUIDList.Add(calleeBR.GUID);

            brList += string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(calleeBR), Environment.NewLine);
            brList += string.Format("\t@Autowired{0}", Environment.NewLine);
            brList += string.Format("\tprivate {0} {1};{2}", calleeBR.NameEnglish, SCommon.GetCamelCasing(calleeBR.NameEnglish), Environment.NewLine);
          }
        }
      }

      string code = this.TemplateController
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".controller")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(controller))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(controller.Description, this.DescTypeClass))
        .Replace("[URI]", controller.URI)
        .Replace("[NAME]", SCommon.GetName(controller))
        .Replace("[CLASS_NAME]", controller.NameEnglish)
        .Replace("[BR_LIST]", brList)
        .Replace("[OPERATION_LIST]", this.GetAPIList(apiList));//this.GetAPIList(apiList, controller.URI));

      return code;
    }

    private string GetAPIList(List<SAPI> apiList)
    {
      string specOperationList = string.Empty;

      foreach (SAPI api in apiList)
      {
        if (api.DesignCompleteSkeleton && api.DesignCompleteDetail)
          specOperationList += this.GetAPI(api);//this.GetAPI(api, controllerURI);
      }

      return specOperationList;
    }

    private string GetAPI(SAPI api)
    {
      string calleeBROpText = string.Empty;
      SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, api.CalleeBROperationGUID) as SBizRuleOperation;
      SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

      if (calleeBROp != null)
      {
        calleeBROpText = string.Format("\t\t//{0}{1}", this.SBR.GetComponentFullPath(calleeBROp), Environment.NewLine);

        calleeBROpText +=
          string.Format("\t\t{0}{1}.{2}({3});", 
            this.GetOutputTypeAndName(SComponentType.Dto, calleeBROp.OutputGUID, calleeBROp.Output),
            SCommon.GetCamelCasing(calleeBR.NameEnglish),
            calleeBROp.NameEnglish,
            "p" + calleeBROp.Input);
      }

      string swaggerInput = string.Empty;

      //swaggerInput = string.Format("parameters={@Parameter(name=\"{0}\", description=\"\")},", api.Input);
      if (api.HttpMethod == SCommon.HttpMethodGet)
        swaggerInput = "parameters={@Parameter(name=\"" + this.GetInputType(SComponentType.Dto, api.InputGUID, api.Input) + "\", description=\"\")},";
      else
        swaggerInput = string.Format("requestBody=@io.swagger.v3.oas.annotations.parameters.RequestBody(description=\"{0}\"),", this.GetInputType(SComponentType.Dto, api.InputGUID, api.Input));

      string code = this.TemplateAPI
        .Replace("[NAME]", SCommon.GetName(api))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", GetDesriptionForComment(api.Description, this.DescTypeOperation))
        .Replace("[HTTP_METHOD]", SCommon.GetPascalCasing(api.HttpMethod))
        .Replace("[URI]", api.URI)
        .Replace("[OUTPUT]", this.GetOutputType(SComponentType.Dto, api.OutputGUID, api.Output))
        .Replace("[RETURN]", this.GetOutputReturnStatement(SComponentType.Dto, api.OutputGUID, api.Output))
        .Replace("[NAME]", SCommon.GetName(api))
        .Replace("[METHOD_NAME]", api.NameEnglish)
        .Replace("[INPUT]", this.GetInputTypeAndName(SComponentType.Dto, api.InputGUID, api.Input))
        .Replace("[CALLEE_BR_OPERATION]", calleeBROpText)
        .Replace("[SWAGGER_INPUT]", swaggerInput);

      return code;
    }

    #endregion

    #region Producer

    private string GetProducer(SProducer producer)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(producer.BizPackageGUID);
      List<SPublisher> pubList = this.SBR.SelectPublisherListByParent(producer);

      string topicList = string.Empty;

      foreach (SPublisher pub in pubList)
      {
        if(string.IsNullOrEmpty(pub.Topic) == false)
        {
          topicList += string.Format("\tprivate static final String {0} = \"{1}\";{2}",
              pub.Topic.Replace("-", "_").ToUpper() + "_TOPIC", pub.Topic, Environment.NewLine);
        }
      }

      string code = this.TemplateProducer
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".service")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(producer))
        .Replace("[NAME]", SCommon.GetName(producer))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(producer.Description, this.DescTypeClass))
        .Replace("[CLASS_NAME]", producer.NameEnglish)
        .Replace("[TOPIC_LIST]", topicList)
        .Replace("[OPERATION_LIST]", this.GetPublisherList(pubList));

      return code;
    }

    private string GetPublisherList(List<SPublisher> publisherList)
    {
      string specOperationList = string.Empty;

      foreach (SPublisher pub in publisherList)
      {
        if (pub.DesignCompleteSkeleton && pub.DesignCompleteDetail)
          specOperationList += this.GetPublisher(pub);
      }

      return specOperationList;
    }

    private string GetPublisher(SPublisher pub)
    {
      string code = this.TemplatePublisher
        .Replace("[NAME]", SCommon.GetName(pub))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(pub.Description, this.DescTypeOperation))
        .Replace("[METHOD_NAME]", pub.NameEnglish)
        .Replace("[INPUT]", this.GetInputTypeAndName(SComponentType.Dto, pub.InputGUID, pub.Input))
        .Replace("[INPUT_NAME]", this.GetInputName(SComponentType.Dto, pub.InputGUID, pub.Input))
        .Replace("[TOPIC]", string.IsNullOrEmpty(pub.Topic) ? string.Empty : pub.Topic.Replace("-", "_").ToUpper() + "_TOPIC");

      return code;
    }

    #endregion

    #region Consumer

    private string GetConsumer(SConsumer consumer)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(consumer.BizPackageGUID);
      List<SSubscriber> subList = this.SBR.SelectSubscriberListByParent(consumer);

      List<string> calleeBRGUIDList = new List<string>();
      string brList = string.Empty;

      foreach (SSubscriber sub in subList)
      {
        SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, sub.CalleeBROperationGUID) as SBizRuleOperation;

        if (calleeBROp != null)
        {
          SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

          if (calleeBRGUIDList.Contains(calleeBR.GUID) == false)
          {
            calleeBRGUIDList.Add(calleeBR.GUID);

            brList += string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(calleeBR), Environment.NewLine);
            brList += string.Format("\t@Autowired{0}", Environment.NewLine);
            brList += string.Format("\tprivate {0} {1};{2}", calleeBR.NameEnglish, SCommon.GetCamelCasing(calleeBR.NameEnglish), Environment.NewLine);
          }
        }
      }

      //Topic
      string topicList = string.Empty;

      foreach (SSubscriber sub in subList)
      {
        if(string.IsNullOrEmpty(sub.Topic) == false)
          topicList += string.Format("\tprivate static final String {0} = \"{1}\";{2}", sub.Topic.Replace("-", "_").ToUpper() + "_TOPIC", sub.Topic, Environment.NewLine);
      }

      string code = this.TemplateConsumer
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".consumer")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(consumer))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(consumer.Description, this.DescTypeClass))
        .Replace("[CLASS_NAME]", consumer.NameEnglish)
        .Replace("[BR_LIST]", brList)
        .Replace("[TOPIC_LIST]", topicList)
        .Replace("[OPERATION_LIST]", this.GetSubscriberList(subList));

      return code;
    }

    private string GetSubscriberList(List<SSubscriber> subscriberList)
    {
      string specOperationList = string.Empty;

      foreach (SSubscriber sub in subscriberList)
      {
        if (sub.DesignCompleteSkeleton && sub.DesignCompleteDetail)
          specOperationList += this.GetSubscriber(sub);
      }

      return specOperationList;
    }

    private string GetSubscriber(SSubscriber sub)
    {
      string calleeBROpText = string.Empty;
      SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, sub.CalleeBROperationGUID) as SBizRuleOperation;
      SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

      if (calleeBROp != null)
      {
        calleeBROpText = string.Format("\t\t//{0}{1}", this.SBR.GetComponentFullPath(calleeBROp), Environment.NewLine);
        calleeBROpText += string.Format("\t\t{0}.{1}({2});", SCommon.GetCamelCasing(calleeBR.NameEnglish), calleeBROp.NameEnglish, this.GetInputName(SComponentType.Dto, calleeBROp.InputGUID, calleeBROp.Input));
      }

      string code = this.TemplateSubscriber
        .Replace("[NAME]", SCommon.GetName(sub))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(sub.Description, this.DescTypeOperation))
        .Replace("[TOPIC]", string.IsNullOrEmpty(sub.Topic) ? string.Empty : sub.Topic.Replace("-", "_").ToUpper() + "_TOPIC")
        .Replace("[METHOD_NAME]", sub.NameEnglish)
        .Replace("[INPUT]", this.GetInputTypeAndName(SComponentType.Dto, sub.InputGUID, sub.Input))
        .Replace("[INPUT_TYPE]", this.GetInputType(SComponentType.Dto, sub.InputGUID, sub.Input))
        .Replace("[CALLEE_BR_OPERATION]", calleeBROpText);

      return code;
    }

    #endregion

    #region BR

    private string GetBR(SBizRule br)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(br.BizPackageGUID);
      List<SBizRuleOperation> opList = this.SBR.SelectBizRuleOperationListByBR(br.GUID);

      string calleeMemberText = string.Empty;
      List<string> calleeMemberTextList = new List<string>();
      string callClassList = string.Empty;

      foreach (SBizRuleOperation op in opList)
      {
        //BR.Operation, DA.Operation, API, Publisher, Other
        foreach (SCallee callee in op.CalleeList)
        {
          SComponent calleeComponent = this.SBR.GetComponent(callee.CalleeComponentType, callee.CalleeGUID);

          if (calleeComponent != null)
          {
            if (calleeComponent is SAPI)
            {
              SAPI calleeAPI = calleeComponent as SAPI;
              SController controller = this.SBR.SelectController(calleeAPI.ControllerGUID);
              string rootName = this.SBR.GetRootComponentName(calleeAPI);

              if (controller != null)
              {
                calleeMemberText = string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(controller), Environment.NewLine);
                calleeMemberText += string.Format("\t@Autowired{0}", Environment.NewLine);
                calleeMemberText += string.Format("\tprivate {0}Client {0}Client;{1}", rootName, Environment.NewLine + Environment.NewLine);
              }
            }
            else if (calleeComponent is SOther)
            {
              calleeMemberText = string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(calleeComponent), Environment.NewLine);
              calleeMemberText += string.Format("\t@Autowired{0}", Environment.NewLine);
              calleeMemberText += string.Format("\t//{0}{1}", calleeComponent.NameEnglish, Environment.NewLine + Environment.NewLine);
            }
            else if (calleeComponent is SPublisher)
            {
              SPublisher calleePub = calleeComponent as SPublisher;
              SProducer producer = this.SBR.SelectProducer(calleePub.ProducerGUID);

              if (producer != null)
              {
                calleeMemberText = string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(producer), Environment.NewLine);
                calleeMemberText += string.Format("\t@Autowired{0}", Environment.NewLine);
                calleeMemberText += string.Format("\tprivate {0} {1};{2}", producer.NameEnglish, SCommon.GetCamelCasing(producer.NameEnglish), Environment.NewLine + Environment.NewLine);
              }
            }
            else if (calleeComponent is SBizRuleOperation)
            {
              SBizRuleOperation calleeBROp = calleeComponent as SBizRuleOperation;
              SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

              calleeMemberText = string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(calleeBR), Environment.NewLine);
              calleeMemberText += string.Format("\t@Autowired{0}", Environment.NewLine);
              calleeMemberText += string.Format("\tprivate {0} {1};{2}", calleeBR.NameEnglish, SCommon.GetCamelCasing(calleeBR.NameEnglish), Environment.NewLine + Environment.NewLine);
            }
            else if (calleeComponent is SDataAccessOperation)
            {
              SDataAccessOperation calleeDAOp = calleeComponent as SDataAccessOperation;
              SDataAccess calleeDA = this.SBR.SelectDataAccess(calleeDAOp.DataAccessGUID);

              calleeMemberText = string.Format("\t//{0}{1}", this.SBR.GetComponentFullPath(calleeDA), Environment.NewLine);
              calleeMemberText += string.Format("\t@Autowired{0}", Environment.NewLine);
              calleeMemberText += string.Format("\tprivate {0} {1};{2}", calleeDA.NameEnglish, SCommon.GetCamelCasing(calleeDA.NameEnglish), Environment.NewLine + Environment.NewLine);
            }

            if (calleeMemberTextList.Contains(calleeMemberText) == false)
            {
              calleeMemberTextList.Add(calleeMemberText);
              callClassList += calleeMemberText;
            }
          }
        }
      }

      string code = this.TemplateBR
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".service")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(br))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(br.Description, this.DescTypeClass))
        .Replace("[CLASS_NAME]", br.NameEnglish)
        .Replace("[CALL_CLASS_LIST]", callClassList)
        .Replace("[OPERATION_LIST]", this.GetBROperationList(opList));

      return code;
    }

    private string GetBROperationList(List<SBizRuleOperation> operationList)
    {
      string specOperationList = string.Empty;

      foreach (SBizRuleOperation op in operationList)
      {
        if (op.DesignCompleteSkeleton && op.DesignCompleteDetail)
          specOperationList += this.GetBROperation(op);
      }

      return specOperationList;
    }

    private string GetBROperation(SBizRuleOperation op)
    {
      string callList = string.Empty;

      //BR.Operation, DA.Operation, API, Publisher, Other
      foreach (SCallee callee in op.CalleeList)
      {
        SComponent calleeComponent = this.SBR.GetComponent(callee.CalleeComponentType, callee.CalleeGUID);

        callList += string.Format("\t\t//{0}{1}{2}",
          this.SBR.GetComponentFullPath(calleeComponent),
          string.IsNullOrEmpty(callee.Comment) ? string.Empty : " ▶ " + callee.Comment,
          Environment.NewLine
        );

        if (calleeComponent != null)
        {
          if (calleeComponent is SAPI)
          {
            SAPI api = calleeComponent as SAPI;
            string rootName = this.SBR.GetRootComponentName(api);

            callList += string.Format("\t\t{0}{1}Client.{2}({3});{4}",
              this.GetOutputTypeAndName(SComponentType.Dto, api.OutputGUID, api.Output),
              rootName,
              api.NameEnglish,
              this.GetInputName(SComponentType.Dto, api.InputGUID, api.Input),
              Environment.NewLine + Environment.NewLine);
          }
          else if (calleeComponent is SOther)
          {
            SOther other = calleeComponent as SOther;

            callList += string.Format("\t\t//{0}{1}({2});{3}",
              this.GetOutputTypeAndName(SComponentType.None, null, other.Output),
              other.NameEnglish,
              this.GetInputName(SComponentType.None, null, other.Input),
              Environment.NewLine + Environment.NewLine);
          }
          else if (calleeComponent is SPublisher)
          {
            SPublisher pub = calleeComponent as SPublisher;
            SProducer producer = this.SBR.SelectProducer(pub.ProducerGUID);

            callList += string.Format("\t\t{0}.{1}({2});{3}",
              SCommon.GetCamelCasing(producer.NameEnglish),
              pub.NameEnglish,
              this.GetInputName(SComponentType.Dto, pub.InputGUID, pub.Input),
              Environment.NewLine + Environment.NewLine);
          }
          else if (calleeComponent is SBizRuleOperation)
          {
            SBizRuleOperation calleeBROp = calleeComponent as SBizRuleOperation;
            SBizRule calleeBR = this.SBR.SelectBizRule(calleeBROp.BizRuleGUID);

            callList += string.Format("\t\t{0}{1}.{2}({3});{4}",
              this.GetOutputTypeAndName(SComponentType.Dto, calleeBROp.OutputGUID, calleeBROp.Output),
              SCommon.GetCamelCasing(calleeBR.NameEnglish),
              calleeBROp.NameEnglish,
              this.GetInputName(SComponentType.Dto, calleeBROp.InputGUID, calleeBROp.Input),
              Environment.NewLine + Environment.NewLine);
          }
          else if (calleeComponent is SDataAccessOperation)
          {
            SDataAccessOperation calleeDAOp = calleeComponent as SDataAccessOperation;
            SDataAccess calleeDA = this.SBR.SelectDataAccess(calleeDAOp.DataAccessGUID);

            callList += string.Format("\t\t{0}{1}.{2}({3});{4}",
              this.GetOutputTypeAndName(SComponentType.Entity, calleeDAOp.OutputGUID, calleeDAOp.Output),
              SCommon.GetCamelCasing(calleeDA.NameEnglish),
              calleeDAOp.NameEnglish,
              this.GetInputName(SComponentType.Entity, calleeDAOp.InputGUID, calleeDAOp.Input),
              Environment.NewLine + Environment.NewLine);
          }
        }
        else
        {
          callList += System.Environment.NewLine;
        }
      }

      string code = this.TemplateBROperation
        .Replace("[NAME]", SCommon.GetName(op))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(op.Description, this.DescTypeOperation))
        .Replace("[TX_OPTION]", op.Tx ? "@Transactional" : string.Empty)
        .Replace("[OUTPUT]", this.GetOutputType(SComponentType.Dto, op.OutputGUID, op.Output))
        .Replace("[RETURN]", this.GetOutputReturnStatement(SComponentType.Dto, op.OutputGUID, op.Output))
        .Replace("[METHOD_NAME]", op.NameEnglish)
        .Replace("[INPUT]", this.GetInputTypeAndName(SComponentType.Dto, op.InputGUID, op.Input))
        .Replace("[CALL_LIST]", callList);

      return code;
    }

    #endregion

    #region DA

    private string GetDA(SDataAccess da)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(da.BizPackageGUID);
      List<SDataAccessOperation> daOpList = this.SBR.SelectDataAccessOperationListByDA(da.GUID);

      string code = this.TemplateDA
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".dbmapper")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(da))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(da.Description, this.DescTypeClass))
        .Replace("[CLASS_NAME]", da.NameEnglish)
        .Replace("[OPERATION_LIST]", this.GetDAOperationList(daOpList));

      return code;
    }

    private string GetDAOperationList(List<SDataAccessOperation> daOpList)
    {
      string specOperationList = string.Empty;

      foreach (SDataAccessOperation op in daOpList)
      {
        if (op.DesignCompleteSkeleton && op.DesignCompleteDetail)
          specOperationList += this.GetDAOperation(op);
      }

      return specOperationList;
    }

    private string GetDAOperation(SDataAccessOperation daOp)
    {
      string code = this.TemplateDAOperation
        .Replace("[NAME]", SCommon.GetName(daOp))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(daOp.Description, this.DescTypeOperation))
        .Replace("[OUTPUT]", this.GetOutputType(SComponentType.Entity, daOp.OutputGUID, daOp.Output))
        .Replace("[METHOD_NAME]", daOp.NameEnglish)
        .Replace("[INPUT]", this.GetInputTypeAndName(SComponentType.Entity, daOp.InputGUID, daOp.Input));

      return code;
    }

    #endregion

    #region SQL

    private string GetSQL(SDataAccess da)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(da.BizPackageGUID);
      List<SDataAccessOperation> daOpList = this.SBR.SelectDataAccessOperationListByDA(da.GUID);

      string code = this.TemplateSQL
        .Replace("[SOURCE_PACKAGE]", bizPackage.Name + ".dbmapper")
        .Replace("[CLASS_NAME]", da.NameEnglish)
        .Replace("[OPERATION_LIST]", this.GetSQLOperationList(da, daOpList));

      return code;
    }

    private string GetSQLOperationList(SDataAccess da, List<SDataAccessOperation> daOpList)
    {
      string specOperationList = string.Empty;

      foreach (SDataAccessOperation op in daOpList)
      {
        if (op.DesignCompleteSkeleton && op.DesignCompleteDetail)
          specOperationList += this.GetSQLOperation(da, op);
      }

      return specOperationList;
    }

    private string GetSQLOperation(SDataAccess da, SDataAccessOperation daOp)
    {
      string methodVerb = "NO_VERB";

      if (daOp.NameEnglish.StartsWith("select"))
        methodVerb = "select";
      else if (daOp.NameEnglish.StartsWith("insert"))
        methodVerb = "insert";
      else if (daOp.NameEnglish.StartsWith("update"))
        methodVerb = "update";
      else if (daOp.NameEnglish.StartsWith("save"))
        methodVerb = "update";
      else if (daOp.NameEnglish.StartsWith("delete"))
        methodVerb = "delete";

      string code = this.TemplateSQLOperation
        .Replace("[METHOD_VERB]", methodVerb)
        .Replace("[METHOD_NAME]", daOp.NameEnglish)
        .Replace("[OUTPUT]", this.GetOutputType(SComponentType.Entity, daOp.OutputGUID, daOp.Output))
        .Replace("[INPUT]", this.GetInputType(SComponentType.Entity, daOp.InputGUID, daOp.Input))
        .Replace("[NAME]", SCommon.GetName(daOp))
        .Replace("[FILE_NAME]", da.NameEnglish)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[SQL]", this.GetDesriptionForComment(daOp.SQL, this.DescTypeSQL));

      return code;
    }

    #endregion

    #region DTO

    private string GetDto(SDto dto)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(dto.BizPackageGUID);
      List<SDtoAttribute> attrList = this.SDA.SelectDtoAttributeList(dto.GUID);

      string code = this.TemplateDto
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".dto")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(dto))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[DESC]", this.GetDesriptionForComment(dto.Description, this.DescTypeClass))
        .Replace("[NAME]", SCommon.GetName(dto))
        .Replace("[CLASS_NAME]", dto.NameEnglish)
        .Replace("[ATTRIBUTE_LIST]", this.GetDtoAttributeList(attrList));

      return code;
    }

    private string GetDtoAttributeList(List<SDtoAttribute> attributeList)
    {
      string specAttributeList = string.Empty;

      foreach (SDtoAttribute attr in attributeList)
        specAttributeList += this.GetDtoAttribute(attr);

      return specAttributeList;
    }

    private string GetDtoAttribute(SDtoAttribute attr)
    {
      string code = this.TemplateDtoAttribute
        .Replace("[DATA_TYPE]", attr.DataType)
        .Replace("[NAME_ENG]", attr.NameEnglish)
        .Replace("[NAME]", SCommon.GetName(attr))
        .Replace("[DESC]", string.IsNullOrEmpty(attr.Description) ? string.Empty : " : " + attr.Description);

      return code;
    }

    #endregion

    #region Entity

    private string GetEntity(SEntity entity)
    {
      SBizPackage bizPackage = this.SBR.SelectBizPackge(entity.BizPackageGUID);
      List<SEntityAttribute> attrList = this.SDA.SelectEntityAttributeList(entity.GUID);

      string code = this.TemplateEntity
        .Replace("[SOURCE_PACKAGE]", bizPackage.NameEnglish + ".entity")
        .Replace("[FULL_NAME]", this.SBR.GetComponentFullPath(entity))
        .Replace("[USER]", SCommon.LoggedInUserDisplay)
        .Replace("[YYYYMMDD]", SCommon.GetDate())
        .Replace("[TABLE_NAME]", entity.TableName)
        .Replace("[DESC]", this.GetDesriptionForComment(entity.Description, this.DescTypeClass))
        .Replace("[NAME]", SCommon.GetName(entity))
        .Replace("[CLASS_NAME]", entity.NameEnglish)
        .Replace("[ATTRIBUTE_LIST]", this.GetEntityAttributeList(attrList));

      return code;
    }

    private string GetEntityAttributeList(List<SEntityAttribute> attributeList)
    {
      string specAttributeList = string.Empty;

      foreach (SEntityAttribute attr in attributeList)
        specAttributeList += this.GetEntityAttribute(attr);

      return specAttributeList;
    }

    private string GetEntityAttribute(SEntityAttribute attr)
    {
      string option = string.Empty;

      if (attr.PK == false && attr.FK == false && attr.NN == false)
      {
        option = "";
      }
      else
      {
        //option = "(" + (attr.PK ? "PK" : "") + (attr.FK ? " FK " : "") + (attr.NotNull ? " NotNull" : "") + ")";
        option = "(";

        if (attr.PK)
          option += "PK";

        if (attr.FK)
        {
          if (attr.PK)
            option += ", ";

          option += "FK";
        }

        if (attr.NN)
        {
          if (attr.PK || attr.FK)
            option += ", ";

          option += "NotNull";
        }

        option += ")";
      }


      string code = this.TemplateEntityAttribute
        .Replace("[DATA_TYPE]", attr.DataType)
        .Replace("[NAME_ENG]", attr.NameEnglish)
        .Replace("[NAME]", SCommon.GetName(attr))
        .Replace("[OPTION]", option)
        .Replace("[DESC]", string.IsNullOrEmpty(attr.Description) ? string.Empty : " : " + attr.Description);

      return code;
    }

    #endregion


    #region Input

    private string GetInputType(SComponentType componentType, string tag, string text)
    {
      //OrderDto / int 주문번호, 주문Dto
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = component.NameEnglish;
      }
      else
      {
        DataTable dtInput = GetInputList(text);

        for (int i = 0; i < dtInput.Rows.Count; i++)
        {
          if (i > 0)
            result += ", ";

          result += dtInput.Rows[i]["TYPE"];
        }
      }

      return result;
    }

    private string GetInputTypeAndName(SComponentType componentType, string tag, string text)
    {
      //OrderDto pOrderDto / int 주문번호, 주문Dto p주문Dto
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = string.Format("{0} p{0}", component.NameEnglish);
      }
      else
      {
        if (string.IsNullOrEmpty(text) == false)
        {
          DataTable dtInput = this.GetInputList(text);

          for (int i = 0; i < dtInput.Rows.Count; i++)
          {
            if (i > 0)
              result += ", ";

            result += string.Format("{0} p{1}", dtInput.Rows[i]["TYPE"], dtInput.Rows[i]["NAME"]);

          }
        }
      }

      return result;
    }

    private string GetInputName(SComponentType componentType, string tag, string text)
    {
      //pOrderDto / (int) 주문번호, p주문Dto
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = string.Format("p{0}", component.NameEnglish);
      }
      else
      {
        if (string.IsNullOrEmpty(text) == false)
        {
          
          DataTable dtInput = GetInputList(text);

          for (int i = 0; i < dtInput.Rows.Count; i++)
          {
            if (i > 0)
              result += ", ";

            result += string.Format("p{0}", dtInput.Rows[i]["NAME"]);
          }
        }
      }

      return result;
    }

    private DataTable GetInputList(string componentInput)
    {
      DataTable dtInput = new DataTable();
      dtInput.Columns.Add("TYPE");
      dtInput.Columns.Add("NAME");

      if(string.IsNullOrEmpty(componentInput) == false)
      {
        //상품Dto 상품, String 주문번호: 2개 input 이고 공백앞은 Type, 공백뒤는 변수명
        string[] inputList = componentInput.Trim().Split(',');

        for (int i = 0; i < inputList.Length; i++)
        {
          string input = inputList[i];

          string type = string.Empty;
          string name = string.Empty;

          string[] typeAndName = input.Trim().Split(new string[] { " " }, StringSplitOptions.None);

          if (typeAndName.Length == 1)
          {
            type = typeAndName[0];
            name = typeAndName[0];//name = "p" + typeAndName[0];
          }
          else if (typeAndName.Length == 2)
          {
            type = typeAndName[0];
            name = typeAndName[1];
          }

          dtInput.Rows.Add(type, name);
        }
      }
    
      return dtInput;
    }

    #endregion

    #region Output

    private string GetOutputType(SComponentType componentType, string tag, string text)
    {
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = component.NameEnglish;
      }
      else
      {
        if (string.IsNullOrEmpty(text) == false)
        {
          //int 주문번호, 주문Dto
          string[] typeAndName = text.Trim().Split(new string[] { " " }, StringSplitOptions.None);

          if (typeAndName.Length > 0)
            result = typeAndName[0];
        }
      }

      if (result.Length == 0)
        result = "void";

      return result;
    }

    private string GetOutputReturnStatement(SComponentType componentType, string tag, string text)
    {
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = "return " + " r" + component.NameEnglish + ";";
      }
      else
      {
        if (string.IsNullOrEmpty(text) == false)
        {
          //int 주문번호, 주문Dto
          string[] typeAndName = text.Trim().Split(new string[] { " " }, StringSplitOptions.None);
          string type = string.Empty;
          string name = string.Empty;

          if (typeAndName.Length == 1)
          {
            type = typeAndName[0];
            name = "r" + typeAndName[0];
          }
          else if (typeAndName.Length == 2)
          {
            type = typeAndName[0];
            name = typeAndName[1];
          }

          if (type.Length > 0)
          {
            if (name.Length == 0)
              result = "return " + " r" + type + ";";
            else
              result = "return " + name + ";";
          }
        }
      }

      return result;
    }

    private string GetOutputTypeAndName(SComponentType componentType, string tag, string text)
    {
      //주문Dto r주문Dto = 공백
      string result = string.Empty;

      if (string.IsNullOrEmpty(tag) == false)
      {
        SComponent component = this.SBR.GetComponent(componentType, tag);
        result = string.Format("{0} r{0} = ", component.NameEnglish);
      }
      else
      {
        if (string.IsNullOrEmpty(text) == false)
        {
          //int 주문번호, 주문Dto
          string[] typeAndName = text.Trim().Split(new string[] { " " }, StringSplitOptions.None);
          string type = string.Empty;
          string name = string.Empty;

          if (typeAndName.Length == 1)
          {
            type = typeAndName[0];
            name = typeAndName[0];
          }
          if (typeAndName.Length == 2)
          {
            type = typeAndName[0];
            name = typeAndName[1];
          }

          if (name.Length == 0)
            result = string.Format("{0} r{0} = ", type);
          else
            result = string.Format("{0} {1} = ", type, name);
        }
      }

      if (result.Length == 0)
        result = "void";

      return result;
    }

    #endregion

    private string GetDesriptionForComment(string desc, string type)
    {
      string comment = string.Empty;

      if(string.IsNullOrEmpty(desc) == false)
      {
        string[] rowList = desc.Split(new string[] { "\r\n" }, StringSplitOptions.None);

        for (int i = 0; i < rowList.Length; i++)
        {
          string row = rowList[i];

          if (type == this.DescTypeClass)
            comment += (i == 0 ? "" : " * ") + row + (i == rowList.Length - 1 ? "" : System.Environment.NewLine);
          else if (type == this.DescTypeOperation)
            comment += (i == 0 ? "" : "\t * ") + row + (i == rowList.Length - 1 ? "" : System.Environment.NewLine);
          else if (type == this.DescTypeSQL)
            comment += (i == 0 ? "" : "\t\t") + row + (i == rowList.Length - 1 ? "" : System.Environment.NewLine);
        }
      }

      return comment;
    }

  }
}
