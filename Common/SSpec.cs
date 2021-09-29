using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using SDM.Component;

namespace SDM.Common
{
  public class SSpec
  {
    private SBR SBR;
    private SDA SDA;

    private string TemplateUI;
    private string TemplateEvent;

    private string TemplateController;
    private string TemplateAPI;

    private string TemplateProducer;
    private string TemplatePublisher;

    private string TemplateConsumer;
    private string TemplateSubscriber;

    private string TemplateOther;

    private string TemplateBR;
    private string TemplateBROperation;

    private string TemplateDA;
    private string TemplateDAOperation;

    private string TemplateDto;
    private string TemplateDtoAttribute;

    private string TemplateEntity;
    private string TemplateEntityAttribute;

    private string TemplateJob;
    private string TemplateStep;

    private List<SAPI> EventAPIList = new List<SAPI>();

    private List<string> DtoEntityGUIDList = new List<string>();
    private List<SDto> DtoList = new List<SDto>();
    private List<SEntity> EntityList = new List<SEntity>();

    public SSpec()
    {
      this.SBR = new SBR();
      this.SDA = new SDA();
    }

    public void GenerateSpec(SComponent component)
    {
      string specFolder = SCommon.SProject.ProjectFolder + @"\\template_spec";

      if (Directory.Exists(specFolder) == false)
      {
        SMessageBox.ShowWarning(SMessage.NO_PROJECT_FOLDER, "template_spec");
        return;
      }

      this.TemplateUI = File.ReadAllText(specFolder + @"\\ui.html");
      this.TemplateEvent = File.ReadAllText(specFolder + @"\\ui_event.html");

      this.TemplateController = File.ReadAllText(specFolder + @"\\controller.html");
      this.TemplateAPI = File.ReadAllText(specFolder + @"\\controller_api.html");

      this.TemplateProducer = File.ReadAllText(specFolder + @"\\producer.html");
      this.TemplatePublisher = File.ReadAllText(specFolder + @"\\producer_pub.html");

      this.TemplateConsumer = File.ReadAllText(specFolder + @"\\consumer.html");
      this.TemplateSubscriber = File.ReadAllText(specFolder + @"\\consumer_sub.html");

      this.TemplateOther = File.ReadAllText(specFolder + @"\\other.html");

      this.TemplateBR = File.ReadAllText(specFolder + @"\\br.html");
      this.TemplateBROperation = File.ReadAllText(specFolder + @"\\br_operation.html");

      this.TemplateDA = File.ReadAllText(specFolder + @"\\da.html");
      this.TemplateDAOperation = File.ReadAllText(specFolder + @"\\da_operation.html");

      this.TemplateDto = File.ReadAllText(specFolder + @"\\dto.html");
      this.TemplateDtoAttribute = File.ReadAllText(specFolder + @"\\dto_attribute.html");

      this.TemplateEntity = File.ReadAllText(specFolder + @"\\entity.html");
      this.TemplateEntityAttribute = File.ReadAllText(specFolder + @"\\entity_attribute.html");

      this.TemplateJob = File.ReadAllText(specFolder + @"\\batch_job.html");
      this.TemplateStep = File.ReadAllText(specFolder + @"\\batch_step.html");

      string spec = string.Empty;

      bool childComponent = false;

      if(component is SUI || component is SController || component is SAPI || component is SConsumer || component is SSubscriber || component is SBizRule || component is SBizRuleOperation)
      {
        DialogResult result = SMessageBox.ShowConfirm(SMessage.GENERATE_CHILD_COMPONENT);

        if (result == DialogResult.Yes)
          childComponent = true;
      }

      if (component is SUI)
      {
        spec = this.GetUI(component as SUI, childComponent);
      }

      else if (component is SController || component is SAPI)
      {
        if (component is SController)
          spec = this.GetController(component as SController, childComponent);
        else if (component is SAPI)
          spec = this.GetAPI(component as SAPI, true, childComponent, string.Empty);
      }

      else if (component is SProducer || component is SPublisher)
      {
        if (component is SProducer)
          spec = this.GetProducer(component as SProducer);
        else if (component is SPublisher)
          spec = this.GetPublisher(component as SPublisher, true, string.Empty);
      }

      else if (component is SConsumer || component is SSubscriber)
      {
        if (component is SConsumer)
          spec = this.GetConsumer(component as SConsumer, childComponent);
        else if (component is SSubscriber)
          spec = this.GetSubscriber(component as SSubscriber, true, childComponent);
      }

      else if (component is SOther)
      {
        spec = this.GetOther(component as SOther, string.Empty);
      }

      else if (component is SBizRule || component is SBizRuleOperation)
      {
        if (component is SBizRule)
          spec = this.GetBR(component as SBizRule, childComponent);
        else if (component is SBizRuleOperation)
          spec = this.GetBROperation(component as SBizRuleOperation, true, childComponent, string.Empty);
      }

      else if (component is SDataAccess || component is SDataAccessOperation)
      {
        if (component is SDataAccess)
          spec = this.GetDA(component as SDataAccess);
        else if (component is SDataAccessOperation)
          spec = this.GetDAOperation(component as SDataAccessOperation, true, string.Empty);
      }

      else if (component is SDto)
      {
        spec = this.GetDto(component as SDto);
      }
      else if (component is SEntity)
      {
        spec = this.GetEntity(component as SEntity);
      }

      else if (component is SJob)
      {
        spec = this.GetJob(component as SJob);
      }

      if(childComponent)
      {
        //spec += "<h4>관련 Dto</h4>";

        foreach (SDto dto in this.DtoList)
          spec += this.GetDto(dto);

        foreach (SEntity entity in this.EntityList)
          spec += this.GetEntity(entity);
      }

      string templateStyle = File.ReadAllText(specFolder + @"\\style.html");
      spec = templateStyle.Replace("[SPEC]", spec);

      this.SaveSpecFile(component, spec);
    }

    #region UI/Event

    private string GetUI(SUI ui, bool childComponent)
    {
      this.EventAPIList.Clear();

      string spec = this.TemplateUI
          .Replace("[ID]", ui.ProgramID)
          .Replace("[NAME]", SCommon.GetName(ui))
          .Replace("[TYPE]", ui.UIType)
          .Replace("[PROGRAM_NAME]", ui.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(ui, true))
          .Replace("[EVENT_LIST]", this.GetEventList(this.SBR.SelectEventListByUI(ui.GUID), childComponent));

      if(childComponent)
      {
        for (int i = 0; i < this.EventAPIList.Count; i++)
        {
          //spec += this.Division + this.GetAPI(this.EventAPIList[i], true, true, "▶");
          spec += this.GetAPI(this.EventAPIList[i], true, true, "▶");
        }
      }

      return spec;
    }

    private string GetEventList(List<SEvent> eventList, bool childComponent)
    {
      string spec = string.Empty;

      for (int i = 0; i < eventList.Count; i++)
      {
        SEvent evt = eventList[i];
        spec += this.GetEvent(evt, childComponent);
      }

      return spec;
    }

    private string GetEvent(SEvent evt, bool childComponent)
    {
      SAPI calleeAPI = this.SBR.GetComponent(SComponentType.API, evt.CalleeApiGuid) as SAPI;
      string apiName = string.Empty;
      string apiURI = string.Empty;

      if(calleeAPI != null)
      {
        apiName = SCommon.GetName(calleeAPI);
        apiURI = calleeAPI.URI;
      }

      string spec = this.TemplateEvent
        .Replace("[EVENT_NAME]", SCommon.GetName(evt))
        .Replace("[API_NAME]", apiName)
        .Replace("[API_URI]", apiURI);

      if (childComponent && calleeAPI != null)
      {
        //spec += this.GetAPI(calleeAPI, true, true, "▶");
        this.EventAPIList.Add(calleeAPI);
      }

      return spec;
    }

    #endregion 

    #region Controller/API

    private string GetController(SController controller, bool childComponent)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(controller.BizPackageGUID);

      string spec = this.TemplateConsumer
          .Replace("[NAME]", SCommon.GetName(controller))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", controller.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(controller, true))
          .Replace("[OPERATION_LIST]", this.GetAPIList(this.SBR.SelectAPIListByParent(controller), childComponent));

      return spec;
    }

    private string GetAPIList(List<SAPI> apiList, bool childComponent)
    {
      string spec = string.Empty;

      for (int i = 0; i < apiList.Count; i++)
      {
        SAPI api = apiList[i];

        if(api.DesignCompleteSkeleton && api.DesignCompleteDetail)
          spec += this.GetAPI(api, false, childComponent, "");
      }

      return spec;
    }

    private string GetAPI(SAPI api, bool methodOnly, bool childComponent, string childLevelSign)
    {
      string id = api.ProgramID;
      string name = SCommon.GetName(api);
      string methodName = api.NameEnglish;

      if (methodOnly)
      {
        SController controller = this.SBR.SelectController(api.ControllerGUID);

        if (controller != null)
        {
          name = SCommon.GetName(controller) + "." + name;
          methodName = controller.NameEnglish + "." + methodName;
        }
      }

      string spec = string.Empty;

      id = childLevelSign + id;

      spec += this.TemplateAPI
        .Replace("[ID]", id)
        .Replace("[NAME]", name)
        .Replace("[METHOD_NAME]", methodName)
        .Replace("[HTTP_METHOD]", api.HttpMethod)
        .Replace("[URI]", api.URI)
        .Replace("[INPUT]", this.GetInputOutput(SComponentType.Dto, api.InputGUID, api.Input))
        .Replace("[OUTPUT]", this.GetInputOutput(SComponentType.Dto, api.OutputGUID, api.Output))
        .Replace("[BR_OPERATION]", this.SBR.GetComponentFullPath(SComponentType.BizRuleOperation, api.CalleeBROperationGUID))
        .Replace("[DESC]", SCommon.GetDesription(api, true));

      if (childComponent)
      {
        SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, api.CalleeBROperationGUID) as SBizRuleOperation;
        spec += this.GetBROperation(calleeBROp, true, true, childLevelSign + "▶");
      }

      return spec;
    }

    #endregion 

    #region Producer/Publisher

    private string GetProducer(SProducer producer)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(producer.BizPackageGUID);

      string spec = this.TemplateProducer
          .Replace("[NAME]", SCommon.GetName(producer))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", producer.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(producer, true))
          .Replace("[OPERATION_LIST]", this.GetPublisherList(this.SBR.SelectPublisherListByParent(producer)));

      return spec;
    }

    private string GetPublisherList(List<SPublisher> pubList)
    {
      string spec = string.Empty;

      for (int i = 0; i < pubList.Count; i++)
      {
        SPublisher pub = pubList[i];

        if (pub.DesignCompleteSkeleton && pub.DesignCompleteDetail)
          spec += this.GetPublisher(pub, false, string.Empty);
      }

      return spec;
    }

    private string GetPublisher(SPublisher pub, bool methodOnly, string childLevelSign)
    {
      string id = pub.ProgramID;
      string name = SCommon.GetName(pub);
      string methodName = pub.NameEnglish;

      if (methodOnly)
      {
        SProducer producer = this.SBR.SelectProducer(pub.ProducerGUID);

        if (producer != null)
        {
          name = SCommon.GetName(producer) + "." + name;
          methodName = producer.NameEnglish + "." + methodName;
        }
      }

      id = childLevelSign + id;

      string spec = this.TemplatePublisher
        .Replace("[ID]", id)
        .Replace("[NAME]", name)
        .Replace("[METHOD_NAME]", methodName)
        .Replace("[DTO]", this.GetInputOutput(SComponentType.Dto, pub.InputGUID, pub.Input))
        .Replace("[TOPIC]", pub.Topic)
        .Replace("[DESC]", SCommon.GetDesription(pub, true));

      return spec;
    }

    #endregion 

    #region Consumer/Subscriber

    private string GetConsumer(SConsumer consumer, bool childComponent)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(consumer.BizPackageGUID);

      string spec = this.TemplateConsumer
          .Replace("[NAME]", SCommon.GetName(consumer))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", consumer.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(consumer, true))
          .Replace("[OPERATION_LIST]", this.GetSubscriberList(this.SBR.SelectSubscriberListByParent(consumer), childComponent));

      return spec;
    }

    private string GetSubscriberList(List<SSubscriber> subList, bool childComponent)
    {
      string specSubList = string.Empty;

      for (int i = 0; i < subList.Count; i++)
      {
        SSubscriber sub = subList[i];

        if (sub.DesignCompleteSkeleton && sub.DesignCompleteDetail)
          specSubList += this.GetSubscriber(sub, false, childComponent);
      }

      return specSubList;
    }

    private string GetSubscriber(SSubscriber sub, bool methodOnly, bool childComponent)
    {
      string id = sub.ProgramID;
      string name = SCommon.GetName(sub);
      string methodName = sub.NameEnglish;

      if (methodOnly)
      {
         SConsumer consumer = this.SBR.SelectConsumer(sub.ConsumerGUID);

        if(consumer != null)
        {
          name = SCommon.GetName(consumer) + "." + name;
          methodName = consumer.NameEnglish + "." + methodName;
        }
      }

      //id = childLevelSign + id;

      string spec = this.TemplateSubscriber
        .Replace("[ID]", id)
        .Replace("[NAME]", name)
        .Replace("[METHOD_NAME]", methodName)
        .Replace("[DTO]", this.GetInputOutput(SComponentType.Dto, sub.InputGUID, sub.Input))
        .Replace("[TOPIC]", sub.Topic)
        .Replace("[BR_OPERATION]", this.SBR.GetComponentFullPath(SComponentType.BizRuleOperation, sub.CalleeBROperationGUID))
        .Replace("[DESC]", SCommon.GetDesription(sub, true));

      if(childComponent)
      {
        SBizRuleOperation calleeBROp = this.SBR.GetComponent(SComponentType.BizRuleOperation, sub.CalleeBROperationGUID) as SBizRuleOperation;
        spec += this.GetBROperation(calleeBROp, true, true, "▶");
      }

      return spec;
    }

    #endregion 

    #region Other

    private string GetOther(SOther other, string childLevelSign)
    {
      string spec = this.TemplateOther
        .Replace("[ID]", childLevelSign + other.ProgramID)
        .Replace("[NAME]", other.Name)
        .Replace("[METHOD_NAME]", other.NameEnglish)
        .Replace("[SENDER_RECEIVER]", other.SenderReceiver)

        //.Replace("[SENDER_RECEIVER_SYSTEM]", this.SBR.RetrieveSystemName(other))
        .Replace("[SENDER_RECEIVER_SYSTEM]", other.SystemName)
        
        .Replace("[TYPE]", other.Type)
        .Replace("[TYPE_OTHER]", other.TypeText)
        .Replace("[INPUT]", other.Input)
        .Replace("[OUTPUT]", other.Output)
        .Replace("[DESC]", SCommon.GetDesription(other, true));

      return spec;
    }

    #endregion 

    #region BR

    private string GetBR(SBizRule br, bool childComponent)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(br.BizPackageGUID);

      string specClass = this.TemplateBR
          .Replace("[NAME]", SCommon.GetName(br))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", br.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(br, true))
          .Replace("[OPERATION_LIST]", this.GetBROperationList(this.SBR.SelectBizRuleOperationListByBR(br.GUID), childComponent));

      return specClass;
    }

    private string GetBROperationList(List<SBizRuleOperation> brOpist, bool childComponent)
    {
      string specOperationList = string.Empty;

      for (int i = 0; i < brOpist.Count; i++)
      {
        SBizRuleOperation op = brOpist[i];

        if (op.DesignCompleteSkeleton && op.DesignCompleteDetail)
          specOperationList += this.GetBROperation(op, false, childComponent, "");
      }

      return specOperationList;
    }

    private string GetBROperation(SBizRuleOperation op, bool methodOnly, bool childComponent, string childLevelSign)
    {
      if (op == null)
        return "";

      string name = SCommon.GetName(op);
      string methodName = op.NameEnglish;

      if (methodOnly)
      {
        SBizRule br = this.SBR.SelectBizRule(op.BizRuleGUID);
        name = SCommon.GetName(br) + "." + name;
        methodName = br.NameEnglish + "." + methodName;
      }

      name = childLevelSign + name;

      string callList = string.Empty;
      List<SComponent> calleeComponentList = new List<SComponent>();

      foreach (SCallee callee in op.CalleeList)
      {
        SComponent calleeComponent = this.SBR.GetComponent(callee.CalleeComponentType, callee.CalleeGUID);

        //callList += "<li>" + this.SBR.GetComponentFullPath(callee.ComponentType, callee.CalleeGUID);
        callList += "<li>" + this.SBR.GetComponentFullPath(calleeComponent);

        if (string.IsNullOrEmpty(callee.Comment) == false)
          callList += " ▶ " + callee.Comment;

        callList += "</li>";

        if (childComponent)
          calleeComponentList.Add(calleeComponent);
      }

      string specOperation = this.TemplateBROperation
        .Replace("[METHOD_NAME]", methodName)
        .Replace("[NAME]", name)
        .Replace("[INPUT]", this.GetInputOutput(SComponentType.Dto, op.InputGUID, op.Input))
        .Replace("[OUTPUT]", this.GetInputOutput(SComponentType.Dto, op.OutputGUID, op.Output))
        .Replace("[DESC]", SCommon.GetDesription(op, true))
        .Replace("[CALL_LIST]", callList);

      if(childComponent)
      {
        foreach (SComponent calleeComponent in calleeComponentList)
        {
          if (calleeComponent is SBizRuleOperation)
            specOperation += this.GetBROperation(calleeComponent as SBizRuleOperation, true, true, childLevelSign + "▶");
          else if (calleeComponent is SDataAccessOperation)
            specOperation += this.GetDAOperation(calleeComponent as SDataAccessOperation, true, childLevelSign + "▶");
          else if (calleeComponent is SAPI)
            specOperation += this.GetAPI(calleeComponent as SAPI, true, true, childLevelSign + "▶");
          else if (calleeComponent is SPublisher)
            specOperation += this.GetPublisher(calleeComponent as SPublisher, true, childLevelSign + "▶");
          else if (calleeComponent is SOther)
            specOperation += this.GetOther(calleeComponent as SOther, childLevelSign + "▶");
        }
      }

      return specOperation;
    }

    #endregion 

    #region DA

    private string GetDA(SDataAccess da)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(da.BizPackageGUID);

      string specClass = this.TemplateDA
          .Replace("[NAME]", SCommon.GetName(da))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", da.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(da, true))
          .Replace("[OPERATION_LIST]", this.GetDAOperationList(this.SBR.SelectDataAccessOperationListByDA(da.GUID)));

      return specClass;
    }

    private string GetDAOperationList(List<SDataAccessOperation> daOpist)
    {
      string specOperationList = string.Empty;

      for (int i = 0; i < daOpist.Count; i++)
      {
        SDataAccessOperation op = daOpist[i];

        if (op.DesignCompleteSkeleton && op.DesignCompleteDetail)
          specOperationList += this.GetDAOperation(op, false, "");
      }

      return specOperationList;
    }

    private string GetDAOperation(SDataAccessOperation op, bool methodOnly, string childLevelSign)
    {
      string name = SCommon.GetName(op);
      string methodName = op.NameEnglish;

      if (methodOnly)
      {
        SDataAccess da = this.SBR.SelectDataAccess(op.DataAccessGUID);
        name = SCommon.GetName(da) + "." + name;
        methodName = da.NameEnglish + "." + methodName;
      }

      name = childLevelSign + name;

      string specOperation = this.TemplateDAOperation
        .Replace("[METHOD_NAME]", methodName)
        .Replace("[NAME]", name)
        .Replace("[INPUT]", this.GetInputOutput(SComponentType.Entity, op.InputGUID, op.Input))
        .Replace("[OUTPUT]", this.GetInputOutput(SComponentType.Entity, op.OutputGUID, op.Output))
        .Replace("[DESC]", SCommon.GetDesription(op, true))
        .Replace("[SQL]", SCommon.GetSQL(op));

      return specOperation;
    }

    #endregion

    #region Dto

    private string GetDto(SDto dto)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(dto.BizPackageGUID);

      string spec = this.TemplateDto
          .Replace("[NAME]", SCommon.GetName(dto))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", dto.NameEnglish)
          .Replace("[DESC]", SCommon.GetDesription(dto, true))
          .Replace("[ATTRIBUTE_LIST]", this.GetDtoAttributeList(this.SDA.SelectDtoAttributeList(dto.GUID)));

      return spec;
    }

    private string GetDtoAttributeList(List<SDtoAttribute> attrList)
    {
      string spec = string.Empty;

      for (int i = 0; i < attrList.Count; i++)
      {
        SDtoAttribute attr = attrList[i];
        spec += this.GetDtoAttribute(attr);
      }

      return spec;
    }

    private string GetDtoAttribute(SDtoAttribute attr)
    {
      string spec = this.TemplateDtoAttribute
        .Replace("[DATA_TYPE]", attr.DataType.Replace("<", "&lt;").Replace(">", "&gt;"))
        .Replace("[ATTRIBUTE]", SCommon.GetName(attr))
        .Replace("[VARIABLE]", attr.NameEnglish)
        .Replace("[DESC]", attr.Description);

      return spec;
    }

    #endregion

    #region Entity

    private string GetEntity(SEntity entity)
    {
      SBizPackage bp = this.SBR.SelectBizPackge(entity.BizPackageGUID);

      string spec = this.TemplateEntity
          .Replace("[NAME]", SCommon.GetName(entity))
          .Replace("[SOURCE_PACKAGE]", bp.NameEnglish)
          .Replace("[CLASS_NAME]", entity.NameEnglish)
          .Replace("[TABLE_NAME]", entity.TableName)
          .Replace("[DESC]", SCommon.GetDesription(entity, true))
          .Replace("[ATTRIBUTE_LIST]", this.GetEntityAttributeList(this.SDA.SelectEntityAttributeList(entity.GUID)));

      return spec;
    }

    private string GetEntityAttributeList(List<SEntityAttribute> attrList)
    {
      string spec = string.Empty;

      for (int i = 0; i < attrList.Count; i++)
      {
        SEntityAttribute attr = attrList[i];
        spec += this.GetEntityAttribute(attr);
      }

      return spec;
    }

    private string GetEntityAttribute(SEntityAttribute attr)
    {
      string spec = this.TemplateEntityAttribute
        .Replace("[PK]", attr.PK ? "Y" : string.Empty)
        .Replace("[DATA_TYPE]", attr.DataType.Replace("<", "&lt;").Replace(">", "&gt;"))
        .Replace("[ATTRIBUTE]", SCommon.GetName(attr))
        .Replace("[VARIABLE]", attr.NameEnglish)
        .Replace("[FK]", attr.FK ? "Y" : string.Empty)
        .Replace("[NOT_NULL]", attr.NN ? "Y" : string.Empty)
        .Replace("[DESC]", attr.Description);

      return spec;
    }

    #endregion

    #region Job

    private string GetJob(SJob job)
    {
      string spec = this.TemplateJob
          .Replace("[ID]", job.ProgramID)
          .Replace("[NAME]", SCommon.GetName(job))
          .Replace("[JOB_NAME]", job.NameEnglish)
          .Replace("[SCHEDULE]", job.Schedule)
          .Replace("[START]", job.Start)
          .Replace("[DESC]", SCommon.GetDesription(job, true))
          .Replace("[STEP_LIST]", this.GetStepList(this.SBR.SelectStepListByJob(job.GUID)));

      return spec;
    }

    private string GetStepList(List<SStep> stepList)
    {
      string spec = string.Empty;

      for (int i = 0; i < stepList.Count; i++)
      {
        SStep step = stepList[i];

        if (step.DesignCompleteSkeleton && step.DesignCompleteDetail)
          spec += this.GetStep(step);
      }

      return spec;
    }

    private string GetStep(SStep step)
    {
      string spec = this.TemplateStep
        .Replace("[NAME]", SCommon.GetName(step))
        .Replace("[STEP_NAME]", step.NameEnglish)
        .Replace("[DESC]", SCommon.GetDesription(step, true));

      return spec;
    }

    #endregion

    private string GetInputOutput(SComponentType componentType, string tag, string text)
    {
      string result = text;

      if (string.IsNullOrEmpty(tag) == false)
      {
        if(componentType == SComponentType.Dto)
        {
          SDto dto = this.SBR.GetComponent(componentType, tag) as SDto;
          result = this.SBR.GetComponentFullPath(dto);

          if(this.DtoEntityGUIDList.Contains(dto.GUID) == false)
          {
            this.DtoEntityGUIDList.Add(dto.GUID);
            this.DtoList.Add(dto);
          }
        }
        else if (componentType == SComponentType.Entity)
        {
          SEntity entity = this.SBR.GetComponent(componentType, tag) as SEntity;
          result = this.SBR.GetComponentFullPath(entity);

          if (this.DtoEntityGUIDList.Contains(entity.GUID) == false)
          {
            this.DtoEntityGUIDList.Add(entity.GUID);
            this.EntityList.Add(entity);
          }
        }
      }

      return result;
    }

    private void SaveSpecFile(SComponent component, string specContents)
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        //(M)주문.(BP)주문.(CT)주문Controller_20210723.0918
        saveFileDialog.FileName =
          string.Format(
            "{0}_{1}.doc",
            this.SBR.GetComponentFullPath(component, "."),
            SCommon.GetDateForExport()
          );

        saveFileDialog.Filter = "doc files (*.doc)|*.doc";
        saveFileDialog.RestoreDirectory = false;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
          if (saveFileDialog.FileName.Length > 0)
          {
            File.WriteAllText(saveFileDialog.FileName, specContents);
            SCommon.OpenDocument(saveFileDialog.FileName);
          }
        }
      }
    }

  }
}
