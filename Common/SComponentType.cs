using System;

namespace SDM.Common
{
  public enum SComponentType
  {
    Microservice,
    InternalSystem,
    ExternalSystem,

    BizPackage,

    Controller,
    API,

    Producer,
    Publisher,

    Consumer,
    Subscriber,

    Other,

    BizRule,
    BizRuleOperation,

    DataAccess,
    DataAccessOperation,

    Dto,
    Entity,

    UI,
    
    Job,
    Step,

    None
  }
}
