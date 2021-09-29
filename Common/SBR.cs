using System;
using System.Collections.Generic;
using System.Data;

using SDM.Component;
using SDM.Project;

namespace SDM.Common
{
  public class SBR
  {
    private SDA SDA;

    public SBR()
    {
      this.SDA = new SDA();
    }

    public string GetRootComponentName(SAPI api)
    {
      string rootComponentName = string.Empty;

      if (string.IsNullOrEmpty(api.MicroserviceName) == false)
        rootComponentName = api.MicroserviceName;
      else if (string.IsNullOrEmpty(api.InternalSystemName) == false)
        rootComponentName = api.InternalSystemName;
      else if (string.IsNullOrEmpty(api.ExternalSystemName) == false)
        rootComponentName = api.ExternalSystemName;

      return rootComponentName;
    }


    #region Consumer/Provider

    public bool HasConsumer(SComponent component)
    {
      List<SCaller> callerList = this.GetConsumerList(component);

      return callerList.Count > 0;
    }

    public List<SCaller> GetConsumerList(SComponent component)
    {
      List<SCaller> callerList = new List<SCaller>();

      if (component is SAPI)
        callerList = this.GetConsumerList(component as SAPI);
      else if (component is SPublisher)
        callerList = this.GetConsumerList(component as SPublisher);
      else if (component is SOther)
        callerList = this.GetConsumerList(component as SOther);
      else if (component is SBizRuleOperation)
        callerList = this.GetConsumerList(component as SBizRuleOperation);
      else if (component is SDataAccessOperation)
        callerList = this.GetConsumerList(component as SDataAccessOperation);
      else if (component is SDto)
        callerList = this.GetConsumerList(component as SDto);
      else if (component is SEntity)
        callerList = this.GetConsumerList(component as SEntity);

      return callerList;
    }


    private List<SCaller> GetConsumerList(SAPI selectedAPI)
    {
      List<SCaller> callerList = new List<SCaller>();

      //UI > Event
      List<SEvent> evtList = this.SelectEventListByCalleeAPIGUID(selectedAPI.GUID);

      foreach (SEvent evt in evtList)
        callerList.Add(new SCaller(SComponentType.UI, evt.UIGUID, this.GetComponentFullPath(SComponentType.UI, evt.UIGUID)));

      //BR Operation
      List<SCallee> calleeList = this.SDA.SelectCalleeListByCallee(SComponentType.BizRuleOperation, selectedAPI.GUID);

      foreach (SCallee callee in calleeList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, callee.ParentGUID, this.GetComponentFullPath(SComponentType.BizRuleOperation, callee.ParentGUID)));

      //SI
      List<SCallee> calleeListSI = this.SDA.SelectCalleeListByCallee(SComponentType.InternalSystem, selectedAPI.GUID);

      foreach (SCallee callee in calleeListSI)
        callerList.Add(new SCaller(SComponentType.InternalSystem, callee.ParentGUID, this.GetComponentFullPath(SComponentType.InternalSystem, callee.ParentGUID)));

      //SE
      List<SCallee> calleeListSE = this.SDA.SelectCalleeListByCallee(SComponentType.ExternalSystem, selectedAPI.GUID);

      foreach (SCallee callee in calleeListSE)
        callerList.Add(new SCaller(SComponentType.ExternalSystem, callee.ParentGUID, this.GetComponentFullPath(SComponentType.ExternalSystem, callee.ParentGUID)));

      return callerList;
    }

    private List<SCaller> GetConsumerList(SPublisher selectedPub)
    {
      List<SCaller> callerList = new List<SCaller>();

      //BR Operation
      List<SCallee> calleeList = this.SDA.SelectCalleeListByCallee(SComponentType.BizRuleOperation, selectedPub.GUID);

      foreach (SCallee callee in calleeList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, callee.ParentGUID, this.GetComponentFullPath(SComponentType.BizRuleOperation, callee.ParentGUID)));

      //Topic을 통한 Subscriber에서 사용
      if (string.IsNullOrEmpty(selectedPub.Topic) == false)
      {
        List<SSubscriber> subList = this.SelectSubscriberListByTopic(selectedPub.Topic);

        foreach (SSubscriber sub in subList)
          callerList.Add(new SCaller(SComponentType.Subscriber, sub.GUID, this.GetComponentFullPath(sub)));
      }
      

      return callerList;
    }

    private List<SCaller> GetConsumerList(SOther selectedOther)
    {
      List<SCaller> callerList = new List<SCaller>();

      //BR Operation
      List<SCallee> calleeList = this.SDA.SelectCalleeListByCallee(SComponentType.BizRuleOperation, selectedOther.GUID);

      foreach (SCallee callee in calleeList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, callee.ParentGUID, this.GetComponentFullPath(SComponentType.BizRuleOperation, callee.ParentGUID)));

      //SI
      List<SCallee> calleeListSI = this.SDA.SelectCalleeListByCallee(SComponentType.InternalSystem, selectedOther.GUID);

      foreach (SCallee callee in calleeListSI)
        callerList.Add(new SCaller(SComponentType.InternalSystem, callee.ParentGUID, this.GetComponentFullPath(SComponentType.InternalSystem, callee.ParentGUID)));

      //SE
      List<SCallee> calleeListSE = this.SDA.SelectCalleeListByCallee(SComponentType.ExternalSystem, selectedOther.GUID);

      foreach (SCallee callee in calleeListSE)
        callerList.Add(new SCaller(SComponentType.ExternalSystem, callee.ParentGUID, this.GetComponentFullPath(SComponentType.ExternalSystem, callee.ParentGUID)));

      return callerList;
    }

    private List<SCaller> GetConsumerList(SBizRuleOperation selectedBROp)
    {
      List<SCaller> callerList = new List<SCaller>();

      //API
      List<SAPI> apiList = this.SelectAPIListByCalleeBROpGUID(selectedBROp.GUID);

      foreach(SAPI api in apiList)
        callerList.Add(new SCaller(SComponentType.API, api.GUID, this.GetComponentFullPath(api)));

      //Subscriber
      List<SSubscriber> subList = this.SelectSubscriberListByCalleeBROpGUID(selectedBROp.GUID);

      foreach (SSubscriber sub in subList)
        callerList.Add(new SCaller(SComponentType.Subscriber, sub.GUID, this.GetComponentFullPath(sub)));

      //BR Operation
      List<SCallee> calleeList = this.SDA.SelectCalleeListByCallee(SComponentType.BizRuleOperation, selectedBROp.GUID);

      foreach (SCallee callee in calleeList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, callee.ParentGUID, this.GetComponentFullPath(SComponentType.BizRuleOperation, callee.ParentGUID)));

      return callerList;
    }

    private List<SCaller> GetConsumerList(SDataAccessOperation selectedDAOp)
    {
      List<SCaller> callerList = new List<SCaller>();

      //BR Operation
      List<SCallee> calleeList = this.SDA.SelectCalleeListByCallee(SComponentType.BizRuleOperation, selectedDAOp.GUID);

      foreach (SCallee callee in calleeList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, callee.ParentGUID, this.GetComponentFullPath(SComponentType.BizRuleOperation, callee.ParentGUID)));

      return callerList;
    }

    private List<SCaller> GetConsumerList(SDto selectedDto)
    {
      List<SCaller> callerList = new List<SCaller>();

      //API
      List<SAPI> apiList = this.SelectAPIListByIOGUID(selectedDto.GUID);

      foreach (SAPI api in apiList)
        callerList.Add(new SCaller(SComponentType.API, api.GUID, this.GetComponentFullPath(api)));

      //Publisher
      List<SPublisher> pubList = this.SDA.SelectPublisherList(null, string.Empty, selectedDto.GUID, string.Empty);

      foreach (SPublisher pub in pubList)
        callerList.Add(new SCaller(SComponentType.Publisher, pub.GUID, this.GetComponentFullPath(pub)));

      //Subscriber
      List<SSubscriber> subList = this.SelectSubscriberListByInputGUID(selectedDto.GUID);

      foreach (SSubscriber sub in subList)
        callerList.Add(new SCaller(SComponentType.Subscriber, sub.GUID, this.GetComponentFullPath(sub)));

      //BR Operation
      List<SBizRuleOperation> brOpList = this.SDA.SelectBizRuleOperationList(string.Empty, string.Empty, selectedDto.GUID);

      foreach (SBizRuleOperation brOp in brOpList)
        callerList.Add(new SCaller(SComponentType.BizRuleOperation, brOp.GUID, this.GetComponentFullPath(brOp)));

      return callerList;
    }

    private List<SCaller> GetConsumerList(SEntity selectedEntity)
    {
      List<SCaller> callerList = new List<SCaller>();

      //DA Operation
      List<SDataAccessOperation> daOpList = this.SDA.SelectDataAccessOperationList(string.Empty, string.Empty, selectedEntity.GUID);

      foreach (SDataAccessOperation daOp in daOpList)
        callerList.Add(new SCaller(SComponentType.DataAccessOperation, daOp.GUID, this.GetComponentFullPath(daOp)));

      return callerList;
    }


    public SCaller RetrieveProvider(SSubscriber selectedSub)
    {
      SCaller caller = null;

      if (string.IsNullOrWhiteSpace(selectedSub.Topic) == false)
      {
        List<SPublisher> pubList = this.SDA.SelectPublisherList(null, string.Empty, string.Empty, selectedSub.Topic);

        foreach (SPublisher pub in pubList)
          caller = new SCaller(SComponentType.Publisher, pub.GUID, this.GetComponentFullPath(pub));
      }

      return caller;
    }

    #endregion

    #region Component 및 Fullpath

    public SComponent GetComponent(SComponentType componentType, string guid)
    {
      SComponent component = null;

      if (guid == null)
        return component;

      if (guid.Length == 0)
        return component;

      if (componentType == SComponentType.Microservice)
        component = this.SDA.SelectMicroservice(guid);
      else if (componentType == SComponentType.InternalSystem)
        component = this.SDA.SelectInternalSystem(guid);
      else if (componentType == SComponentType.ExternalSystem)
        component = this.SDA.SelectExternalSystem(guid);

      else if (componentType == SComponentType.BizPackage)
        component = this.SelectBizPackge(guid);

      else if (componentType == SComponentType.Controller)
        component = this.SelectController(guid);
      else if (componentType == SComponentType.API)
        component = this.SelectAPI(guid);

      else if (componentType == SComponentType.Producer)
        component = this.SelectProducer(guid);
      else if (componentType == SComponentType.Publisher)
        component = this.SelectPublisher(guid);

      else if (componentType == SComponentType.Consumer)
        component = this.SelectConsumer(guid);
      else if (componentType == SComponentType.Subscriber)
        component = this.SelectSubscriber(guid);

      else if (componentType == SComponentType.Other)
        component = this.SelectOther(guid);

      else if (componentType == SComponentType.BizRule)
        component = this.SelectBizRule(guid);
      else if (componentType == SComponentType.BizRuleOperation)
        component = this.SelectBizRuleOperation(guid);

      else if (componentType == SComponentType.DataAccess)
        component = this.SelectDataAccess(guid);
      else if (componentType == SComponentType.DataAccessOperation)
        component = this.SelectDataAccessOperation(guid);

      else if (componentType == SComponentType.Dto)
        component = this.SelectDto(guid);
      else if (componentType == SComponentType.Entity)
        component = this.SelectEntity(guid);

      else if (componentType == SComponentType.UI)
        component = this.SelectUI(guid);

      else if (componentType == SComponentType.Job)
        component = this.SelectJob(guid);
      else if (componentType == SComponentType.Step)
        component = this.SelectStep(guid);

      return component;
    }


    public string GetComponentFullPath(SComponentType componentType, string guid, string delimiter = "")
    {
      string fullPath = string.Empty;

      SComponent component = this.GetComponent(componentType, guid);
      fullPath = this.GetComponentFullPath(component);

      if (delimiter.Length > 0)
        fullPath = fullPath.Replace(" > ", delimiter);

      return fullPath;
    }

    public string GetComponentFullPath(SComponent component, string delimiter = "")
    {
      string fullPath = string.Empty;

      if (component == null)
        return fullPath;

      if (component is SMicroservice)
      {
        //(M)주문
        fullPath = string.Format("({0}){1}", SCommon.AbbrMicroservice, component.Name);
      }
      else if (component is SInternalSystem)
      {
        //(IS)대내시스템
        fullPath = string.Format("({0}){1}", SCommon.AbbrInternalSystem, component.Name);
      }
      else if (component is SExternalSystem)
      {
        //(ES)대외시스템
        fullPath = string.Format("({0}){1}", SCommon.AbbrExternalSystem, component.Name);
      }
      else if (component is SBizPackage)
      {
        //(M)주문 > (BP)주문
        SBizPackage bp = component as SBizPackage;

        fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrMicroservice, bp.MicroserviceName,
            SCommon.AbbrBizPackage, bp.Name
          );
      }
      else if (component is SController)
      {
        SController ct = component as SController;

        //(M)주문 > (BP)주문 > (C)주문Controller
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
            SCommon.AbbrMicroservice, ct.MicroserviceName,
            SCommon.AbbrBizPackage, ct.BizPackageName,
            SCommon.AbbrController, SCommon.GetName(ct.Name)
          );
      }
      else if (component is SAPI)
      {
        SAPI api = component as SAPI;

        if(string.IsNullOrEmpty(api.MicroserviceName) == false)
        {
          if (string.IsNullOrEmpty(api.ControllerName) == false)
          {
            //(M)주문 > (BP)주문 > (C)주문Controller > (A)API
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
              SCommon.AbbrMicroservice, api.MicroserviceName,
              SCommon.AbbrBizPackage, api.BizPackageName,
              SCommon.AbbrController, SCommon.GetName(api.ControllerName),
              SCommon.AbbrAPI, SCommon.GetName(api.Name)
            );
          }
          else
          {
            //(M)주문 > (BP)주문 > (A)API
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
              SCommon.AbbrMicroservice, api.MicroserviceName,
              SCommon.AbbrBizPackage, api.BizPackageName,
              SCommon.AbbrAPI, SCommon.GetName(api.Name)
            );
          }
        }
        else if (string.IsNullOrEmpty(api.InternalSystemName) == false)
        {
          //SI : (SI)대내시스템 > (A)API
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrInternalSystem, api.InternalSystemName,
            SCommon.AbbrAPI, SCommon.GetName(api.Name)
          );
        }
        else if (string.IsNullOrEmpty(api.ExternalSystemName) == false)
        {
          //SE : (SE)대외시스템 > (A)API
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrExternalSystem, api.ExternalSystemName,
            SCommon.AbbrAPI, SCommon.GetName(api.Name)
          );
        }
      }
      else if (component is SProducer)
      {
        SProducer pd = component as SProducer;

        //(M)주문 > (BP)주문 > (PD)Producer
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, pd.MicroserviceName,
          SCommon.AbbrBizPackage, pd.BizPackageName,
          SCommon.AbbrProducer, SCommon.GetName(pd.Name)
        );
      }
      else if (component is SPublisher)
      {
        SPublisher pub = component as SPublisher;

        if (string.IsNullOrEmpty(pub.MicroserviceName) == false)
        {
          if (string.IsNullOrEmpty(pub.ProducerName) == false)
          {
            //(M)주문 > (BP)주문 > (P)주문Producer > (P)Publisher
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
              SCommon.AbbrMicroservice, pub.MicroserviceName,
              SCommon.AbbrBizPackage, pub.BizPackageName,
              SCommon.AbbrProducer, SCommon.GetName(pub.ProducerName),
              SCommon.AbbrPublisher, SCommon.GetName(pub.Name)
            );
          }
          else
          {
            //(M)주문 > (BP)주문 > (P)Publisher
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
              SCommon.AbbrMicroservice, pub.MicroserviceName,
              SCommon.AbbrBizPackage, pub.BizPackageName,
              SCommon.AbbrPublisher, SCommon.GetName(pub.Name)
            );
          }
        }
        else if (string.IsNullOrEmpty(pub.InternalSystemName) == false)
        {
          //(SI)대내시스템 > (P)Publisher
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrInternalSystem, pub.InternalSystemName,
            SCommon.AbbrPublisher, SCommon.GetName(pub.Name)
          );
        }
        else if (string.IsNullOrEmpty(pub.ExternalSystemName) == false)
        {
          //(SE)대외시스템 > (P)Publisher
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrExternalSystem, pub.ExternalSystemName,
            SCommon.AbbrPublisher, SCommon.GetName(pub.Name)
          );
        }
      }
      else if (component is SConsumer)
      {
        SConsumer cs = component as SConsumer;

        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, cs.MicroserviceName,
          SCommon.AbbrBizPackage, cs.BizPackageName,
          SCommon.AbbrConsumer, SCommon.GetName(cs.Name)
        );
      }
      else if (component is SSubscriber)
      {
        SSubscriber sub = component as SSubscriber;

        if (string.IsNullOrEmpty(sub.MicroserviceName) == false)
        {
          if (string.IsNullOrEmpty(sub.ConsumerName) == false)
          {
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
              SCommon.AbbrMicroservice, sub.MicroserviceName,
              SCommon.AbbrBizPackage, sub.BizPackageName,
              SCommon.AbbrConsumer, SCommon.GetName(sub.ConsumerName),
              SCommon.AbbrSubscriber, SCommon.GetName(sub.Name)
            );
          }
          else
          {
            fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
              SCommon.AbbrMicroservice, sub.MicroserviceName,
              SCommon.AbbrBizPackage, sub.BizPackageName,
              SCommon.AbbrSubscriber, SCommon.GetName(sub.Name)
            );
          }
        }
        else if (string.IsNullOrEmpty(sub.InternalSystemName) == false)
        {
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrInternalSystem, sub.InternalSystemName,
            SCommon.AbbrSubscriber, SCommon.GetName(sub.Name)
          );
        }
        else if (string.IsNullOrEmpty(sub.ExternalSystemName) == false)
        {
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrExternalSystem, sub.ExternalSystemName,
            SCommon.AbbrSubscriber, SCommon.GetName(sub.Name)
          );
        }
      }
      else if (component is SOther)
      {
        SOther other = component as SOther;

        if (string.IsNullOrEmpty(other.MicroserviceName) == false)
        {
          fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
            SCommon.AbbrMicroservice, other.MicroserviceName,
            SCommon.AbbrBizPackage, other.BizPackageName,
            SCommon.AbbrOther, SCommon.GetName(other.Name)
          );
        }
        else if (string.IsNullOrEmpty(other.InternalSystemName) == false)
        {
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrInternalSystem, other.InternalSystemName,
            SCommon.AbbrSubscriber, SCommon.GetName(other.Name)
          );
        }
        else if (string.IsNullOrEmpty(other.ExternalSystemName) == false)
        {
          fullPath = string.Format("({0}){1} > ({2}){3}",
            SCommon.AbbrExternalSystem, other.ExternalSystemName,
            SCommon.AbbrSubscriber, SCommon.GetName(other.Name)
          );
        }
      }
      else if (component is SBizRule)
      {
        SBizRule br = component as SBizRule;

        //(M)주문 > (BP)주문 > (BR)주문BR
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, br.MicroserviceName,
          SCommon.AbbrBizPackage, br.BizPackageName,
          SCommon.AbbrBizRule, SCommon.GetName(br.Name)
        );
      }
      else if (component is SBizRuleOperation)
      {
        SBizRuleOperation op = component as SBizRuleOperation;

        //(M)주문 > (BP)주문 > (BR)주문BR > (OP)Opeartion
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
          SCommon.AbbrMicroservice, op.MicroserviceName,
          SCommon.AbbrBizPackage, op.BizPackageName,
          SCommon.AbbrBizRule, SCommon.GetName(op.BizRuleName),
          SCommon.AbbrOperation, SCommon.GetName(op.Name)
        );
      }
      else if (component is SDataAccess)
      {
        SDataAccess da = component as SDataAccess;

        //(M)주문 > (BP)주문 > (DA)주문DA
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, da.MicroserviceName,
          SCommon.AbbrBizPackage, da.BizPackageName,
          SCommon.AbbrDataAccess, SCommon.GetName(da.Name)
        );
      }
      else if (component is SDataAccessOperation)
      {
        SDataAccessOperation op = component as SDataAccessOperation;

        //(M)주문 > (BP)주문 > (DA)주문DA > (OP)Opeartion
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
          SCommon.AbbrMicroservice, op.MicroserviceName,
          SCommon.AbbrBizPackage, op.BizPackageName,
          SCommon.AbbrDataAccess, SCommon.GetName(op.DataAccessName),
          SCommon.AbbrOperation, SCommon.GetName(op.Name)
        );
      }
      else if (component is SDto)
      {
        SDto dto = component as SDto;

        //(M)주문 > (BP)주문 > (D)주문Dto
        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, dto.MicroserviceName,
          SCommon.AbbrBizPackage, dto.BizPackageName,
          SCommon.AbbrDto, SCommon.GetName(dto.Name)
        );
      }
      else if (component is SEntity)
      {
        SEntity entity = component as SEntity;

        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, entity.MicroserviceName,
          SCommon.AbbrBizPackage, entity.BizPackageName,
          SCommon.AbbrEntity, SCommon.GetName(entity.Name)
        );
      }
      else if (component is SUI)
      {
        SUI ui = component as SUI;

        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, ui.MicroserviceName,
          SCommon.AbbrBizPackage, ui.BizPackageName,
          SCommon.AbbrUI, SCommon.GetName(ui.Name)
        );
      }
      else if (component is SJob)
      {
        SJob job = component as SJob;

        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5}",
          SCommon.AbbrMicroservice, job.MicroserviceName,
          SCommon.AbbrBizPackage, job.BizPackageName,
          SCommon.AbbrJob, SCommon.GetName(job.Name)
        );
      }
      else if (component is SStep)
      {
        SStep step = component as SStep;

        fullPath = string.Format("({0}){1} > ({2}){3} > ({4}){5} > ({6}){7}",
          SCommon.AbbrMicroservice, step.MicroserviceName,
          SCommon.AbbrBizPackage, step.BizPackageName,
          SCommon.AbbrJob, SCommon.GetName(step.JobName),
          SCommon.AbbrStep, SCommon.GetName(step.Name));
      }

      if (delimiter.Length > 0)
        fullPath = fullPath.Replace(" > ", delimiter);

      return fullPath;
    }

    #endregion


    #region SI

    public void DeleteInternalSystem(SInternalSystem si)
    {
      this.SDA.DeleteCalleeList(si.GUID);
      this.SDA.DeleteInternalSystem(si);
    }

    #endregion

    #region SE

    public void DeleteExternalSystem(SExternalSystem se)
    {
      this.SDA.DeleteCalleeList(se.GUID);
      this.SDA.DeleteExternalSystem(se);
    }

    #endregion

    #region BizPackage

    public List<SBizPackage> SelectBizPackgeListByMicroservice(string msGUID, bool sortByName = true)
    {
      return this.SDA.SelectBizPackgeList(msGUID, string.Empty, sortByName);
    }

    public SBizPackage SelectBizPackge(string guid)
    {
      List<SBizPackage> bpList = this.SDA.SelectBizPackgeList(string.Empty, guid);

      if (bpList.Count == 1)
        return bpList[0];
      else
        return null;
    }

    public void UpdateBizPackgeParent(SMicroservice ms, SBizPackage bp)
    {
      this.SDA.UpdateBizPackgeParent(ms, bp);

      List<SAPI> apiList = this.SelectAPIListByParent(bp);
      foreach (SAPI api in apiList)
        this.SDA.UpdateAPIParent(bp, api);

      List<SController> ctrList = this.SelectControllerListByBP(bp.GUID);
      foreach (SController ctr in ctrList)
        this.UpdateControllerParent(bp, ctr);

      List<SPublisher> pubList = this.SelectPublisherListByParent(bp);

      foreach (SPublisher pub in pubList)
        this.SDA.UpdatePublisherParent(bp, pub);

      List<SProducer> producerList = this.SelectProducerListByBP(bp.GUID);
      foreach (SProducer producer in producerList)
        this.UpdateProducerParent(bp, producer);

      List<SSubscriber> subList = this.SelectSubscriberListByParent(bp);
      foreach (SSubscriber sub in subList)
        this.SDA.UpdateSubscriberParent(bp, sub);

      List<SConsumer> consumerList = this.SelectConsumerListByBP(bp.GUID);
      foreach (SConsumer consumer in consumerList)
        this.UpdateConsumerParent(bp, consumer);

      List<SOther> otherList = this.SelectOtherListByParent(bp);
      foreach (SOther other in otherList)
        this.SDA.UpdateOtherParent(bp, other);

      List<SBizRule> brList = this.SelectBizRuleListByBP(bp.GUID);
      foreach (SBizRule br in brList)
        this.UpdateBizRuleParent(bp, br);

      List<SDataAccess> daList = this.SelectDataAccessListByBP(bp.GUID);
      foreach (SDataAccess da in daList)
        this.UpdateDataAccessParent(bp, da);

      List<SDto> dtoList = this.SelectDtoListByBP(bp.GUID);
      foreach (SDto dto in dtoList)
        this.UpdateDtoParent(bp, dto);

      List<SEntity> entList = this.SelectEntityListByBP(bp.GUID);
      foreach (SEntity ent in entList)
        this.UpdateEntityParent(bp, ent);

      List<SUI> uiList = this.SelectUIListByBP(bp.GUID);
      foreach (SUI ui in uiList)
        this.SDA.UpdateUIParent(bp, ui);

      List<SJob> jobList = this.SelectJobListByBP(bp.GUID);
      foreach (SJob job in jobList)
        this.UpdateJobParent(bp, job);
    }

    #endregion

    #region Controller

    public List<SController> SelectControllerListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectControllerList(bpGUID, string.Empty, sortByName);
    }

    public SController SelectController(string guid)
    {
      List<SController> controllerList = this.SDA.SelectControllerList(string.Empty, guid);

      if (controllerList.Count == 1)
        return controllerList[0];
      else
        return null;
    }

    public void UpdateControllerParent(SBizPackage bp, SController ctr)
    {
      this.SDA.UpdateControllerParent(bp, ctr);

      List<SAPI> apiList = this.SelectAPIListByParent(ctr);

      foreach(SAPI api in apiList)
        this.SDA.UpdateAPIParent(ctr, api);
    }

    #endregion

    #region API

    public List<SAPI> SelectAPIListByParent(SComponent parentComponent, bool sortByName = true)
    {
      return this.SDA.SelectAPIList(parentComponent, string.Empty, string.Empty, string.Empty, sortByName);
    }

    public List<SAPI> SelectAPIListByIOGUID(string dtoGUID, bool sortByName = true)
    {
      return this.SDA.SelectAPIList(null, string.Empty, dtoGUID, string.Empty, sortByName);
    }

    public List<SAPI> SelectAPIListByCalleeBROpGUID(string calleeBROpGUID, bool sortByName = true)
    {
      return this.SDA.SelectAPIList(null, string.Empty, string.Empty, calleeBROpGUID, sortByName);
    }

    public SAPI SelectAPI(string guid)
    {
      List<SAPI> apiList = this.SDA.SelectAPIList(null, guid, string.Empty, string.Empty);

      if (apiList.Count == 1)
        return apiList[0];
      else
        return null;
    }

    public SAPI InsertAPI(SComponent parentComponent, string name = "")
    {
      SAPI api = new SAPI();
      api.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        api.MicroserviceGUID = bp.MicroserviceGUID;
        api.MicroserviceName = bp.MicroserviceName;
        api.BizPackageGUID = bp.GUID;
        api.BizPackageName = bp.Name;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SController)
      {
        SController controller = parentComponent as SController;
        api.MicroserviceGUID = controller.MicroserviceGUID;
        api.MicroserviceName = controller.MicroserviceName;
        api.BizPackageGUID = controller.BizPackageGUID;
        api.BizPackageName = controller.BizPackageName;
        api.ControllerGUID = controller.GUID;
        api.ControllerName = SCommon.GetName(controller.Name);
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        api.MicroserviceGUID = string.Empty;
        api.MicroserviceName = string.Empty;
        api.BizPackageGUID = string.Empty;
        api.BizPackageName = string.Empty;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = si.GUID;
        api.InternalSystemName = si.Name;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        api.MicroserviceGUID = string.Empty;
        api.MicroserviceName = string.Empty;
        api.BizPackageGUID = string.Empty;
        api.BizPackageName = string.Empty;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = se.GUID;
        api.ExternalSystemName = se.Name;
      }

      api.Name = name.Length == 0 ? "New API" : name;

      if (name.EndsWith("조회"))
        api.HttpMethod = SCommon.HttpMethodGet;
      else if (name.EndsWith("등록"))
        api.HttpMethod = SCommon.HttpMethodPost;
      else if (name.EndsWith("수정"))
        api.HttpMethod = SCommon.HttpMethodPut;
      else if (name.EndsWith("삭제"))
        api.HttpMethod = SCommon.HttpMethodDelete;
      else if (name.EndsWith("저장"))
        api.HttpMethod = SCommon.HttpMethodPost;

      SCommon.SetDateDesigner(api, true);

      this.SDA.InsertAPI(api);

      return api;
    }

    public SAPI CopyAPI(SComponent parentComponent, SAPI fromAPI)
    {
      SAPI newAPI = new SAPI();
      newAPI.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        newAPI.MicroserviceGUID = bp.MicroserviceGUID;
        newAPI.MicroserviceName = bp.MicroserviceName;
        newAPI.BizPackageGUID = bp.GUID;
        newAPI.BizPackageName = bp.Name;
        newAPI.ControllerGUID = string.Empty;
        newAPI.ControllerName = string.Empty;
        newAPI.InternalSystemGUID = string.Empty;
        newAPI.InternalSystemName = string.Empty;
        newAPI.ExternalSystemGUID = string.Empty;
        newAPI.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SController)
      {
        SController controller = parentComponent as SController;
        newAPI.MicroserviceGUID = controller.MicroserviceGUID;
        newAPI.MicroserviceName = controller.MicroserviceName;
        newAPI.BizPackageGUID = controller.BizPackageGUID;
        newAPI.BizPackageName = controller.BizPackageName;
        newAPI.ControllerGUID = controller.GUID;
        newAPI.ControllerName = SCommon.GetName(controller.Name);
        newAPI.InternalSystemGUID = string.Empty;
        newAPI.InternalSystemName = string.Empty;
        newAPI.ExternalSystemGUID = string.Empty;
        newAPI.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        newAPI.MicroserviceGUID = string.Empty;
        newAPI.MicroserviceName = string.Empty;
        newAPI.BizPackageGUID = string.Empty;
        newAPI.BizPackageName = string.Empty;
        newAPI.ControllerGUID = string.Empty;
        newAPI.ControllerName = string.Empty;
        newAPI.InternalSystemGUID = si.GUID;
        newAPI.InternalSystemName = si.Name;
        newAPI.ExternalSystemGUID = string.Empty;
        newAPI.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        newAPI.MicroserviceGUID = string.Empty;
        newAPI.MicroserviceName = string.Empty;
        newAPI.BizPackageGUID = string.Empty;
        newAPI.BizPackageName = string.Empty;
        newAPI.ControllerGUID = string.Empty;
        newAPI.ControllerName = string.Empty;
        newAPI.InternalSystemGUID = string.Empty;
        newAPI.InternalSystemName = string.Empty;
        newAPI.ExternalSystemGUID = se.GUID;
        newAPI.ExternalSystemName = se.Name;
      }

      newAPI.ProgramID = "(복사)" + fromAPI.ProgramID;
      newAPI.Name = "(복사)" + fromAPI.Name;
      newAPI.NameEnglish = fromAPI.NameEnglish;
      newAPI.HttpMethod = fromAPI.HttpMethod;
      newAPI.URI = fromAPI.URI;
      newAPI.InputGUID = fromAPI.InputGUID;
      newAPI.Input = fromAPI.Input;
      newAPI.OutputGUID = fromAPI.OutputGUID;
      newAPI.Output = fromAPI.Output;
      newAPI.Description = fromAPI.Description;
      SCommon.SetDateDesigner(newAPI, true);

      this.SDA.InsertAPI(newAPI);

      return newAPI;
    }

    #endregion


    #region Producer

    public List<SProducer> SelectProducerListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectProducerList(bpGUID, string.Empty, sortByName);
    }

    public SProducer SelectProducer(string guid)
    {
      List<SProducer> pdList = this.SDA.SelectProducerList(string.Empty, guid);

      if (pdList.Count == 1)
        return pdList[0];
      else
        return null;
    }

    public void UpdateProducerParent(SBizPackage bp, SProducer producer)
    {
      this.SDA.UpdateProducerParent(bp, producer);

      List<SPublisher> pubList = this.SelectPublisherListByParent(producer);

      foreach (SPublisher pub in pubList)
        this.SDA.UpdatePublisherParent(producer, pub);
    }

    #endregion

    #region Publisher

    public List<SPublisher> SelectPublisherListByParent(SComponent parentComponent, bool sortByName = true)
    {
      return this.SDA.SelectPublisherList(parentComponent, string.Empty, string.Empty, string.Empty, sortByName);
    }

    public List<SPublisher> SelectPublisherListByParentByInputGUID(string inputGUID, bool sortByName = true)
    {
      return this.SDA.SelectPublisherList(null, string.Empty, inputGUID, string.Empty, sortByName);
    }

    public List<SPublisher> SelectPublisherListByParentByTopic(string topic, bool sortByName = true)
    {
      return this.SDA.SelectPublisherList(null, string.Empty, string.Empty, topic, sortByName);
    }

    public SPublisher SelectPublisher(string guid)
    {
      List<SPublisher> pubList = this.SDA.SelectPublisherList(null, guid, string.Empty, string.Empty);

      if (pubList.Count == 1)
        return pubList[0];
      else
        return null;
    }

    public SPublisher InsertPublisher(SComponent parentComponent, string name = "")
    {
      SPublisher pub = new SPublisher();
      pub.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        pub.MicroserviceGUID = bp.MicroserviceGUID;
        pub.MicroserviceName = bp.MicroserviceName;
        pub.BizPackageGUID = bp.GUID;
        pub.BizPackageName = bp.Name;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SProducer)
      {
        SProducer producer = parentComponent as SProducer;
        pub.MicroserviceGUID = producer.MicroserviceGUID;
        pub.MicroserviceName = producer.MicroserviceName;
        pub.BizPackageGUID = producer.BizPackageGUID;
        pub.BizPackageName = producer.BizPackageName;
        pub.ProducerGUID = producer.GUID;
        pub.ProducerName = SCommon.GetName(producer.GUID);
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        pub.MicroserviceGUID = string.Empty;
        pub.MicroserviceName = string.Empty;
        pub.BizPackageGUID = string.Empty;
        pub.BizPackageName = string.Empty;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = si.GUID;
        pub.InternalSystemName = si.Name;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        pub.MicroserviceGUID = string.Empty;
        pub.MicroserviceName = string.Empty;
        pub.BizPackageGUID = string.Empty;
        pub.BizPackageName = string.Empty;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = se.GUID;
        pub.ExternalSystemName = se.Name;
      }

      pub.Name = name.Length == 0 ? "New Publisher" : name;

      SCommon.SetDateDesigner(pub, true);

      this.SDA.InsertPublisher(pub);

      return pub;
    }

    public SPublisher CopyPublisher(SComponent parentComponent, SPublisher fromPub)
    {
      SPublisher newPub = new SPublisher();
      newPub.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        newPub.MicroserviceGUID = bp.MicroserviceGUID;
        newPub.MicroserviceName = bp.MicroserviceName;
        newPub.BizPackageGUID = bp.GUID;
        newPub.BizPackageName = bp.Name;
        newPub.ProducerGUID = string.Empty;
        newPub.ProducerName = string.Empty;
        newPub.InternalSystemGUID = string.Empty;
        newPub.InternalSystemName = string.Empty;
        newPub.ExternalSystemGUID = string.Empty;
        newPub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SProducer)
      {
        SProducer producer = parentComponent as SProducer;
        newPub.MicroserviceGUID = producer.MicroserviceGUID;
        newPub.MicroserviceName = producer.MicroserviceName;
        newPub.BizPackageGUID = producer.BizPackageGUID;
        newPub.BizPackageName = producer.BizPackageName;
        newPub.ProducerGUID = producer.GUID;
        newPub.ProducerName = SCommon.GetName(producer.GUID);
        newPub.InternalSystemGUID = string.Empty;
        newPub.InternalSystemName = string.Empty;
        newPub.ExternalSystemGUID = string.Empty;
        newPub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        newPub.MicroserviceGUID = string.Empty;
        newPub.MicroserviceName = string.Empty;
        newPub.BizPackageGUID = string.Empty;
        newPub.BizPackageName = string.Empty;
        newPub.ProducerGUID = string.Empty;
        newPub.ProducerName = string.Empty;
        newPub.InternalSystemGUID = si.GUID;
        newPub.InternalSystemName = si.Name;
        newPub.ExternalSystemGUID = string.Empty;
        newPub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        newPub.MicroserviceGUID = string.Empty;
        newPub.MicroserviceName = string.Empty;
        newPub.BizPackageGUID = string.Empty;
        newPub.BizPackageName = string.Empty;
        newPub.ProducerGUID = string.Empty;
        newPub.ProducerName = string.Empty;
        newPub.InternalSystemGUID = string.Empty;
        newPub.InternalSystemName = string.Empty;
        newPub.ExternalSystemGUID = se.GUID;
        newPub.ExternalSystemName = se.Name;
      }

      newPub.ProgramID = "(복사)" + fromPub.ProgramID;
      newPub.Name = "(복사)" + fromPub.Name;
      newPub.NameEnglish = fromPub.NameEnglish;
      newPub.Input = fromPub.Input;
      newPub.InputGUID = fromPub.InputGUID;
      //newPub.Topic = fromPub.Topic;
      newPub.Description = fromPub.Description;

      SCommon.SetDateDesigner(newPub, true);

      this.SDA.InsertPublisher(newPub);

      return newPub;
    }

    #endregion


    #region Consumer

    public List<SConsumer> SelectConsumerListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectConsumerList(bpGUID, string.Empty, sortByName);
    }

    public SConsumer SelectConsumer(string guid)
    {
      List<SConsumer> csList = this.SDA.SelectConsumerList(string.Empty, guid);

      if (csList.Count == 1)
        return csList[0];
      else
        return null;
    }

    public void UpdateConsumerParent(SBizPackage bp, SConsumer cs)
    {
      this.SDA.UpdateConsumerParent(bp, cs);

      List<SSubscriber> subList = this.SelectSubscriberListByParent(cs);

      foreach (SSubscriber sub in subList)
        this.SDA.UpdateSubscriberParent(cs, sub);
    }

    #endregion

    #region Subscriber

    public List<SSubscriber> SelectSubscriberListByParent(SComponent parentComponent, bool sortByName = true)
    {
      return this.SDA.SelectSubscriberList(parentComponent, string.Empty, string.Empty, string.Empty, string.Empty, sortByName);
    }

    public List<SSubscriber> SelectSubscriberListByInputGUID(string inputGUID, bool sortByName = true)
    {
      return this.SDA.SelectSubscriberList(null, string.Empty, inputGUID, string.Empty, string.Empty, sortByName);
    }

    public List<SSubscriber> SelectSubscriberListByCalleeBROpGUID(string calleeBROpGUID, bool sortByName = true)
    {
      return this.SDA.SelectSubscriberList(null, string.Empty, string.Empty, calleeBROpGUID, string.Empty, sortByName);
    }

    public List<SSubscriber> SelectSubscriberListByTopic(string topic, bool sortByName = true)
    {
      return this.SDA.SelectSubscriberList(null, string.Empty, string.Empty, string.Empty, topic, sortByName);
    }

    public SSubscriber SelectSubscriber(string guid)
    {
      List<SSubscriber> subList = this.SDA.SelectSubscriberList(null, guid, string.Empty, string.Empty, string.Empty);

      if (subList.Count == 1)
        return subList[0];
      else
        return null;
    }

    public SSubscriber InsertSubscriber(SComponent parentComponent, string name = "")
    {
      SSubscriber sub = new SSubscriber();
      sub.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        sub.MicroserviceGUID = bp.MicroserviceGUID;
        sub.MicroserviceName = bp.MicroserviceName;
        sub.BizPackageGUID = bp.GUID;
        sub.BizPackageName = bp.Name;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SConsumer)
      {
        SConsumer consumer = parentComponent as SConsumer;
        sub.MicroserviceGUID = consumer.MicroserviceGUID;
        sub.MicroserviceName = consumer.MicroserviceName;
        sub.BizPackageGUID = consumer.BizPackageGUID;
        sub.BizPackageName = consumer.BizPackageName;
        sub.ConsumerGUID = consumer.GUID;
        sub.ConsumerName = SCommon.GetName(consumer.GUID);
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        sub.MicroserviceGUID = string.Empty;
        sub.MicroserviceName = string.Empty;
        sub.BizPackageGUID = string.Empty;
        sub.BizPackageName = string.Empty;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = si.GUID;
        sub.InternalSystemName = si.Name;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        sub.MicroserviceGUID = string.Empty;
        sub.MicroserviceName = string.Empty;
        sub.BizPackageGUID = string.Empty;
        sub.BizPackageName = string.Empty;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = se.GUID;
        sub.ExternalSystemName = se.Name;
      }

      sub.Name = name.Length == 0 ? "New Subscriber" : name;

      SCommon.SetDateDesigner(sub, true);

      this.SDA.InsertSubscriber(sub);

      return sub;
    }

    public SSubscriber CopySubscriber(SComponent parentComponent, SSubscriber fromSub)
    {
      SSubscriber newSub = new SSubscriber();
      newSub.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        newSub.MicroserviceGUID = bp.MicroserviceGUID;
        newSub.MicroserviceName = bp.MicroserviceName;
        newSub.BizPackageGUID = bp.GUID;
        newSub.BizPackageName = bp.Name;
        newSub.ConsumerGUID = string.Empty;
        newSub.ConsumerName = string.Empty;
        newSub.InternalSystemGUID = string.Empty;
        newSub.InternalSystemName = string.Empty;
        newSub.ExternalSystemGUID = string.Empty;
        newSub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SConsumer)
      {
        SConsumer consumer = parentComponent as SConsumer;
        newSub.MicroserviceGUID = consumer.MicroserviceGUID;
        newSub.MicroserviceName = consumer.MicroserviceName;
        newSub.BizPackageGUID = consumer.BizPackageGUID;
        newSub.BizPackageName = consumer.BizPackageName;
        newSub.ConsumerGUID = consumer.GUID;
        newSub.ConsumerName = SCommon.GetName(consumer.GUID);
        newSub.InternalSystemGUID = string.Empty;
        newSub.InternalSystemName = string.Empty;
        newSub.ExternalSystemGUID = string.Empty;
        newSub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        newSub.MicroserviceGUID = string.Empty;
        newSub.MicroserviceName = string.Empty;
        newSub.BizPackageGUID = string.Empty;
        newSub.BizPackageName = string.Empty;
        newSub.ConsumerGUID = string.Empty;
        newSub.ConsumerName = string.Empty;
        newSub.InternalSystemGUID = si.GUID;
        newSub.InternalSystemName = si.Name;
        newSub.ExternalSystemGUID = string.Empty;
        newSub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        newSub.MicroserviceGUID = string.Empty;
        newSub.MicroserviceName = string.Empty;
        newSub.BizPackageGUID = string.Empty;
        newSub.BizPackageName = string.Empty;
        newSub.ConsumerGUID = string.Empty;
        newSub.ConsumerName = string.Empty;
        newSub.InternalSystemGUID = string.Empty;
        newSub.InternalSystemName = string.Empty;
        newSub.ExternalSystemGUID = se.GUID;
        newSub.ExternalSystemName = se.Name;
      }

      newSub.ProgramID = "(복사)" + fromSub.ProgramID;
      newSub.Name = "(복사)" + fromSub.Name;
      newSub.NameEnglish = fromSub.NameEnglish;
      newSub.Input = fromSub.Input;
      newSub.InputGUID = fromSub.InputGUID;
      //newSub.Topic = fromPub.Topic;
      //newSub.CalleeBROperationGUID
      newSub.Description = fromSub.Description;

      SCommon.SetDateDesigner(newSub, true);

      this.SDA.InsertSubscriber(newSub);

      return newSub;
    }

    #endregion


    #region Other

    public List<SOther> SelectOtherListByParent(SComponent parentComponent, bool sortByName = true)
    {
      return this.SDA.SelectOtherList(parentComponent, string.Empty, sortByName);
    }

    public SOther SelectOther(string guid)
    {
      List<SOther> otherList = this.SDA.SelectOtherList(null, guid);

      if (otherList.Count == 1)
        return otherList[0];
      else
        return null;
    }

    public SOther InsertOther(SComponent parentComponent, string name = "")
    {
      SOther other = new SOther();
      other.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        other.MicroserviceGUID = bp.MicroserviceGUID;
        other.MicroserviceName = bp.MicroserviceName;
        other.BizPackageGUID = bp.GUID;
        other.BizPackageName = bp.Name;
        other.InternalSystemGUID = string.Empty;
        other.InternalSystemName = string.Empty;
        other.ExternalSystemGUID = string.Empty;
        other.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        other.MicroserviceGUID = string.Empty;
        other.MicroserviceName = string.Empty;
        other.BizPackageGUID = string.Empty;
        other.BizPackageName = string.Empty;
        other.InternalSystemGUID = si.GUID;
        other.InternalSystemName = si.Name;
        other.ExternalSystemGUID = string.Empty;
        other.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        other.MicroserviceGUID = string.Empty;
        other.MicroserviceName = string.Empty;
        other.BizPackageGUID = string.Empty;
        other.BizPackageName = string.Empty;
        other.InternalSystemGUID = string.Empty;
        other.InternalSystemName = string.Empty;
        other.ExternalSystemGUID = se.GUID;
        other.ExternalSystemName = se.Name;
      }

      other.Name = name.Length == 0 ? "New Other" : name;

      SCommon.SetDateDesigner(other, true);

      this.SDA.InsertOther(other);

      return other;
    }

    public SOther CopyOther(SComponent parentComponent, SOther fromOther)
    {
      SOther newOther = new SOther();
      newOther.GUID = Guid.NewGuid().ToString();

      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        newOther.MicroserviceGUID = bp.MicroserviceGUID;
        newOther.MicroserviceName = bp.MicroserviceName;
        newOther.BizPackageGUID = bp.GUID;
        newOther.BizPackageName = bp.Name;
        newOther.InternalSystemGUID = string.Empty;
        newOther.InternalSystemName = string.Empty;
        newOther.ExternalSystemGUID = string.Empty;
        newOther.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        newOther.MicroserviceGUID = string.Empty;
        newOther.MicroserviceName = string.Empty;
        newOther.BizPackageGUID = string.Empty;
        newOther.BizPackageName = string.Empty;
        newOther.InternalSystemGUID = si.GUID;
        newOther.InternalSystemName = si.Name;
        newOther.ExternalSystemGUID = string.Empty;
        newOther.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        newOther.MicroserviceGUID = string.Empty;
        newOther.MicroserviceName = string.Empty;
        newOther.BizPackageGUID = string.Empty;
        newOther.BizPackageName = string.Empty;
        newOther.InternalSystemGUID = string.Empty;
        newOther.InternalSystemName = string.Empty;
        newOther.ExternalSystemGUID = se.GUID;
        newOther.ExternalSystemName = se.Name;
      }

      newOther.ProgramID = "(복사)" + fromOther.ProgramID;
      newOther.Name = "(복사)" + fromOther.Name;
      newOther.NameEnglish = fromOther.NameEnglish;
      newOther.Input = fromOther.Input;
      newOther.Output = fromOther.Output;
      newOther.Description = fromOther.Description;

      SCommon.SetDateDesigner(newOther, true);

      this.SDA.InsertOther(newOther);

      return newOther;
    }

    #endregion


    #region BR

    public List<SBizRule> SelectBizRuleListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectBizRuleList(bpGUID, string.Empty, sortByName);
    }

    public SBizRule SelectBizRule(string guid)
    {
      List<SBizRule> brList = this.SDA.SelectBizRuleList(string.Empty, guid);

      if (brList.Count == 1)
        return brList[0];
      else
        return null;
    }

    public void DeleteBizRuleOperation(SBizRuleOperation brOp)
    {
      this.SDA.DeleteCalleeList(brOp.GUID);
      this.SDA.DeleteBizRuleOperation(brOp);
    }

    public void UpdateBizRuleParent(SBizPackage bp, SBizRule br)
    {
      this.SDA.UpdateBizRuleParent(bp, br);

      List<SBizRuleOperation> opList = this.SelectBizRuleOperationListByBR(br.GUID);

      foreach (SBizRuleOperation op in opList)
        this.SDA.UpdateBizRuleOperationParent(br, op);
    }

    #endregion

    #region BR Operation

    public List<SBizRuleOperation> SelectBizRuleOperationListByBR(string brGUID, bool sortByName = true)
    {
      return this.SDA.SelectBizRuleOperationList(brGUID, string.Empty, string.Empty, sortByName);
    }

    public List<SBizRuleOperation> SelectBizRuleOperationListByIOGUID(string dtoGUID, bool sortByName = true)
    {
      return this.SDA.SelectBizRuleOperationList(string.Empty, string.Empty, dtoGUID, sortByName);
    }

    public SBizRuleOperation SelectBizRuleOperation(string guid)
    {
      List<SBizRuleOperation> opList = this.SDA.SelectBizRuleOperationList(string.Empty, guid, string.Empty);

      if (opList.Count == 1)
        return opList[0];
      else
        return null;
    }

    public SBizRuleOperation InsertBizRuleOperation(SBizRule br, string name = "")
    {
      SBizRuleOperation brOp = new SBizRuleOperation();
      brOp.MicroserviceGUID = br.MicroserviceGUID;
      brOp.MicroserviceName = br.MicroserviceName;
      brOp.BizPackageGUID = br.BizPackageGUID;
      brOp.BizPackageName = br.BizPackageName;
      brOp.BizRuleGUID = br.GUID;
      brOp.BizRuleName = SCommon.GetName(br.Name);
      brOp.GUID = Guid.NewGuid().ToString();
      brOp.Name = name.Length == 0 ? "New BR Operation" : name;
      SCommon.SetDateDesigner(brOp, true);

      this.SDA.InsertBizRuleOperation(brOp);

      return brOp;
    }

    public SBizRuleOperation CopyBizRuleOperation(SBizRule br, SBizRuleOperation fromBROp)
    {
      SBizRuleOperation newBROp = new SBizRuleOperation();
      newBROp.MicroserviceGUID = br.MicroserviceGUID;
      newBROp.MicroserviceName = br.MicroserviceName;
      newBROp.BizPackageGUID = br.BizPackageGUID;
      newBROp.BizPackageName = br.BizPackageName;
      newBROp.BizRuleGUID = br.GUID;
      newBROp.BizRuleName = SCommon.GetName(br.Name);
      newBROp.GUID = Guid.NewGuid().ToString();
      newBROp.ProgramID = "(복사)" + fromBROp.ProgramID;
      newBROp.Name = "(복사)" + fromBROp.Name;
      newBROp.NameEnglish = fromBROp.NameEnglish;
      newBROp.Tx = fromBROp.Tx;
      newBROp.InputGUID = fromBROp.InputGUID;
      newBROp.Input = fromBROp.Input;
      newBROp.OutputGUID = fromBROp.OutputGUID;
      newBROp.Output = fromBROp.Output;
      newBROp.Description = fromBROp.Description;
      SCommon.SetDateDesigner(newBROp, true);

      this.SDA.InsertBizRuleOperation(newBROp);

      return newBROp;
    }

    #endregion


    #region DA

    public List<SDataAccess> SelectDataAccessListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectDataAccessList(bpGUID, string.Empty, sortByName);
    }

    public SDataAccess SelectDataAccess(string guid)
    {
      List<SDataAccess> daList = this.SDA.SelectDataAccessList(string.Empty, guid);

      if (daList.Count == 1)
        return daList[0];
      else
        return null;
    }

    public void UpdateDataAccessParent(SBizPackage bp, SDataAccess da)
    {
      this.SDA.UpdateDataAccessParent(bp, da);

      List<SDataAccessOperation> opList = this.SelectDataAccessOperationListByDA(da.GUID);

      foreach (SDataAccessOperation op in opList)
        this.SDA.UpdateDataAccessOperationParent(da, op);
    }

    #endregion

    #region DA Operation

    public List<SDataAccessOperation> SelectDataAccessOperationListByDA(string daGUID, bool sortByName = true)
    {
      return this.SDA.SelectDataAccessOperationList(daGUID, string.Empty, string.Empty, sortByName);
    }

    public List<SDataAccessOperation> SelectDataAccessOperationListByIOGUID(string entityGUID, bool sortByName = true)
    {
      return this.SDA.SelectDataAccessOperationList(string.Empty, string.Empty, entityGUID, sortByName);
    }

    public SDataAccessOperation SelectDataAccessOperation(string guid)
    {
      List<SDataAccessOperation> opList = this.SDA.SelectDataAccessOperationList(string.Empty, guid, string.Empty);

      if (opList.Count == 1)
        return opList[0];
      else
        return null;
    }

    public SDataAccessOperation InsertDataAccessOperation(SDataAccess da, string name = "")
    {
      SDataAccessOperation daOp = new SDataAccessOperation();
      daOp.GUID = Guid.NewGuid().ToString();
      daOp.MicroserviceGUID = da.MicroserviceGUID;
      daOp.MicroserviceName = da.MicroserviceName;
      daOp.BizPackageGUID = da.BizPackageGUID;
      daOp.BizPackageName = da.BizPackageName;
      daOp.DataAccessGUID = da.GUID;
      daOp.DataAccessName = SCommon.GetName(da.Name);
      daOp.Name = name.Length == 0 ? "New DA Operation" : name;
      SCommon.SetDateDesigner(daOp, true);

      this.SDA.InsertDataAccessOperation(daOp);

      return daOp;
    }

    public SDataAccessOperation CopyDataAccessOperation(SDataAccess da, SDataAccessOperation fromDAOp)
    {
      SDataAccessOperation newDAOp = new SDataAccessOperation();
      newDAOp.GUID = Guid.NewGuid().ToString();
      newDAOp.MicroserviceGUID = da.MicroserviceGUID;
      newDAOp.MicroserviceName = da.MicroserviceName;
      newDAOp.BizPackageGUID = da.BizPackageGUID;
      newDAOp.BizPackageName = da.BizPackageName;
      newDAOp.DataAccessGUID = da.GUID;
      newDAOp.DataAccessName = SCommon.GetName(da.Name);
      newDAOp.ProgramID = "(복사)" + fromDAOp.ProgramID;
      newDAOp.Name = "(복사)" + fromDAOp.Name;
      newDAOp.NameEnglish = fromDAOp.NameEnglish;
      newDAOp.InputGUID = fromDAOp.InputGUID;
      newDAOp.Input = fromDAOp.Input;
      newDAOp.OutputGUID = fromDAOp.OutputGUID;
      newDAOp.Output = fromDAOp.Output;
      newDAOp.SQL = fromDAOp.SQL;
      newDAOp.Description = fromDAOp.Description;
      SCommon.SetDateDesigner(newDAOp, true);

      this.SDA.InsertDataAccessOperation(newDAOp);

      return newDAOp;
    }

    #endregion


    #region Dto

    public List<SDto> SelectDtoListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectDtoList(bpGUID, string.Empty, sortByName);
    }

    public SDto SelectDto(string guid)
    {
      List<SDto> dtoList = this.SDA.SelectDtoList(string.Empty, guid);

      if (dtoList.Count == 1)
        return dtoList[0];
      else
        return null;
    }

    public void UpdateDtoParent(SBizPackage bp, SDto dto)
    {
      this.SDA.UpdateDtoParent(bp, dto);

      List<SDtoAttribute> attrList = this.SDA.SelectDtoAttributeList(dto.GUID);

      foreach (SDtoAttribute attr in attrList)
        this.SDA.UpdateDtoAttributeParent(dto, attr);
    }

    public void DeleteDto(SDto dto)
    {
      this.SDA.DeleteDtoAttributeList(dto.GUID);
      this.SDA.DeleteDto(dto);
    }

    #endregion

    #region Entity

    public List<SEntity> SelectEntityListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectEntityList(bpGUID, string.Empty, sortByName);
    }

    public SEntity SelectEntity(string guid)
    {
      List<SEntity> entityList = this.SDA.SelectEntityList(string.Empty, guid);

      if (entityList.Count == 1)
        return entityList[0];
      else
        return null;
    }

    public void UpdateEntityParent(SBizPackage bp, SEntity entity)
    {
      this.SDA.UpdateEntityParent(bp, entity);

      List<SEntityAttribute> attrList = this.SDA.SelectEntityAttributeList(entity.GUID);

      foreach (SEntityAttribute attr in attrList)
        this.SDA.UpdateEntityAttributeParent(entity, attr);
    }

    public void DeleteEntity(SEntity entity)
    {
      this.SDA.DeleteEntityAttributeList(entity.GUID);
      this.SDA.DeleteEntity(entity);
    }

    #endregion

    #region UI

    public List<SUI> SelectUIListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectUIList(bpGUID, string.Empty, sortByName);
    }

    public SUI SelectUI(string guid)
    {
      List<SUI> uiList = this.SDA.SelectUIList(string.Empty, guid);

      if (uiList.Count == 1)
        return uiList[0];
      else
        return null;
    }

    public void DeleteUI(SUI ui)
    {
      this.SDA.DeleteEventList(ui.GUID);
      this.SDA.DeleteUI(ui);
    }

    #endregion

    #region Event

    public List<SEvent> SelectEventListByUI(string uiGUID)
    {
      return this.SDA.SelectEventList(uiGUID, string.Empty);
    }

    public List<SEvent> SelectEventListByCalleeAPIGUID(string calleeAPIGUID)
    {
      return this.SDA.SelectEventList(string.Empty, calleeAPIGUID);
    }

    #endregion

    #region Job

    public List<SJob> SelectJobListByBP(string bpGUID, bool sortByName = true)
    {
      return this.SDA.SelectJobList(bpGUID, string.Empty, sortByName);
    }

    public SJob SelectJob(string guid)
    {
      List<SJob> jobList = this.SDA.SelectJobList(string.Empty, guid);

      if (jobList.Count == 1)
        return jobList[0];
      else
        return null;
    }

    public void UpdateJobParent(SBizPackage bp, SJob job)
    {
      this.SDA.UpdateJobParent(bp, job);

      List<SStep> stepList = this.SelectStepListByJob(job.GUID);

      foreach (SStep step in stepList)
        this.SDA.UpdateStepParent(job, step);
    }

    #endregion

    #region Step

    public List<SStep> SelectStepListByJob(string jobGUID, bool sortByName = true)
    {
      return this.SDA.SelectStepList(jobGUID, string.Empty, sortByName);
    }

    public SStep SelectStep(string guid)
    {
      List<SStep> stepList = this.SDA.SelectStepList(string.Empty, guid);

      if (stepList.Count == 1)
        return stepList[0];
      else
        return null;
    }

    public SStep InsertStep(SJob job)
    {
      SStep step = new SStep();
      step.GUID = Guid.NewGuid().ToString();
      step.MicroserviceGUID = job.MicroserviceGUID;
      step.MicroserviceName = job.MicroserviceName;
      step.BizPackageGUID = job.BizPackageGUID;
      step.BizPackageName = job.BizPackageName;
      step.JobGUID = job.GUID;
      step.JobName = job.Name;
      step.Name = "New Step";
      SCommon.SetDateDesigner(step, true);

      this.SDA.InsertStep(step);

      return step;
    }

    public SStep CopyStep(SJob job, SStep fromStep)
    {
      SStep newStep = new SStep();

      newStep.GUID = Guid.NewGuid().ToString();
      newStep.MicroserviceGUID = job.MicroserviceGUID;
      newStep.MicroserviceName = job.MicroserviceName;
      newStep.BizPackageGUID = job.BizPackageGUID;
      newStep.BizPackageName = job.BizPackageName;
      newStep.JobGUID = job.GUID;
      newStep.JobName = job.Name;
      newStep.ProgramID = "(복사)" + fromStep.ProgramID;
      newStep.Name = "(복사)" + fromStep.Name;
      newStep.NameEnglish = fromStep.NameEnglish;
      newStep.Description = fromStep.Description;
      SCommon.SetDateDesigner(newStep, true);

      this.SDA.InsertStep(newStep);

      return newStep;
    }

    #endregion

    #region User

    public List<SUser> SelectUserListAll()
    {
      return this.SDA.SelectUserList(string.Empty, string.Empty);
    }

    public List<SUser> SelectUserListByPartGUID(string partGUID)
    {
      return this.SDA.SelectUserList(partGUID, string.Empty);
    }

    public SUser SelectUserListByPartGUIDAndName(string partGUID, string name)
    {
      List<SUser> userList = this.SDA.SelectUserList(partGUID, name);

      if (userList.Count == 1)
        return userList[0];
      else
        return null;
    }

    #endregion

    #region Word

    public string TranslateNoun(string korean)
    {
      string english = korean;

      DataRow row = this.SDA.SelectWord(SCommon.WordNoun, korean);

      if (row != null)
        english = row[SCommon.WordEnglish].ToString();

      return SCommon.GetPascalCasing(english);
    }

    public string TranslateBRVerb(string korean)
    {
      string english = korean;

      DataRow row = this.SDA.SelectWord(SCommon.WordVerbBR, korean);

      if (row != null)
        english = row[SCommon.WordEnglish].ToString();

      return english;
    }

    public string TranslateDAVerb(string korean)
    {
      string english = korean;

      DataRow row = this.SDA.SelectWord(SCommon.WordVerbDA, korean);

      if (row != null)
        english = row[SCommon.WordEnglish].ToString();

      return english;
    }

    public string GetEngClassName(string classNameKor)
    {
      string classNameEng = string.Empty;

      string[] korList = classNameKor.Split(' '); //협력사 상품

      for (int i = 0; i < korList.Length; i++)
      {
        //마지막단어가 BR/CBR, DA/JDA인 경우
        if (i == korList.Length - 1)
        {
          if (korList[i] == "BR")
            classNameEng += "Service";
          else if (korList[i] == "CBR")
            classNameEng += "CService";
          else if (korList[i] == "DA")
            classNameEng += SCommon.SProject.CodeGenClassNameDA;//Dao
          else if (korList[i] == "JDA")
            classNameEng += SCommon.SProject.CodeGenClassNameJDA;//JDao
          else
            classNameEng += this.TranslateNoun(korList[i]);
        }
        else
        {
          classNameEng += this.TranslateNoun(korList[i]);
        }
      }

      if (classNameEng.Length == 0)
        classNameEng = classNameKor;

      return classNameEng;
    }

    public string GetEngAttributeName(string attributeKor)
    {
      string engPascal = this.GetEngClassName(attributeKor);
      return SCommon.GetCamelCasing(engPascal);
    }

    public string GetDBColumn(string attributeKor)
    {
      string dbColumn = string.Empty;

      string[] korList = attributeKor.Split(' '); //협력사 상품

      for (int i = 0; i < korList.Length; i++)
      {
        dbColumn += this.TranslateNoun(korList[i]).ToUpper();

        if (i < korList.Length - 1)
          dbColumn += "_";
      }

      return dbColumn;
    }

    public string GetEngOperationName(string opNameKor, bool daOperation = false)
    {
      string opNameE = opNameKor;

      string[] korList = opNameKor.Split(' '); //상품 등록

      if (korList.Length > 1)
      {
        //마직막 단어 - 동사 : DA외에는 모두 BR 동사를 사용
        if (daOperation)
          opNameE = this.TranslateDAVerb(korList[korList.Length - 1]);
        else
          opNameE = this.TranslateBRVerb(korList[korList.Length - 1]);

        //3 : 0협력사 1상품 2등록
        //마지막을 제외한 명사 목록
        for (int i = 0; i < korList.Length - 1; i++)
          opNameE += this.TranslateNoun(korList[i]);
      }

      return opNameE;
    }

    #endregion
  }
}