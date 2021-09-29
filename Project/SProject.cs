using System;

namespace SDM.Project
{
  public class SProject
  {
    public string GUID { get; set; }

    public string Name { get; set; } = "프로젝트명";

    //public string LogoURL { get; set; } = "https://www.lgcns.com/static/images/header_logo.png";
    public string LogoURL { get; set; } = "https://lgcns.com/static/images/header_logo_on.png";
    
    public string Contact { get; set; } = "문의 : LG CNS MSA모델링팀 구태형(taekoo@lgcns.com)";
    public string ProjectFolder { get; set; } = "";
    public string DeployFolder { get; set; } = "";
    public bool Dictionary { get; set; } = false;

    public string SampleMSCode { get; set; } = "(ex : ORD 주문)";

    public string SampleBPCode { get; set; } = "(ex : ORD10 주문)";
    public string SampleBPSourcePackage { get; set; } = "(ex : project.ms.biz)";

    public string SampleControllerName { get; set; } = "";
    public string SampleControllerClass { get; set; } = "";
    public string SampleControllerURI { get; set; } = "";

    public string SampleAPIName { get; set; } = "";
    public string SampleAPIMethod { get; set; } = "(ex : retrieveOrderList)";
    public string SampleAPIURL { get; set; } = "(ex : /orders/, /orders/{orderNo})";
    public string SampleAPIInput { get; set; } = "(ex : OrderDto)";
    public string SampleAPIOutput { get; set; } = "(ex : int orderNo)";

    public string SampleProducerName { get; set; } = "";
    public string SampleProducerClass { get; set; } = "";

    public string SamplePubName { get; set; } = "(ex : 주문상품발행)";
    public string SamplePubMethod { get; set; } = "(ex : publishOrderProduct)";
    public string SamplePubInput { get; set; } = "(ex : OrderDto)";
    public string SamplePubTopic { get; set; } = "(ex : order-cancelled)";

    public string SampleConsumerName { get; set; } = "";
    public string SampleConsumerClass { get; set; } = "";

    public string SampleSubName { get; set; } = "(ex : 주문상품구독)";
    public string SampleSubMethod { get; set; } = "(ex : subscribeOrderProduct)";
    public string SampleSubInput { get; set; } = "(ex : OrderDto)";

    public string SampleDtoName { get; set; } = "(ex : 주문Dto)";
    public string SampleDtoClass { get; set; } = "(ex : OrderDto)";

    public string SampleEntityName { get; set; } = "(ex : 주문Entity)";
    public string SampleEntityClass { get; set; } = "(ex : OrderEntity)";
    public string SampleEntityTable { get; set; } = "(ex : TB_ORDER)";

    public string SampleBRName { get; set; } = "(ex : 주문BR or 주문CBR)";
    public string SampleBRClass { get; set; } = "(ex : OrderService)";
    public string SampleBROpName { get; set; } = "(ex : 주문교환/반품/취소)";
    public string SampleBROpMethod { get; set; } = "(ex : orderProduct)";

    public string SampleDAName { get; set; } = "";
    public string SampleDAClass { get; set; } = "";
    public string SampleDAOpName { get; set; } = "";
    public string SampleDAOpMethod { get; set; } = "";

    public string SampleUIName { get; set; } = "(ex : 주문UI)";
    public string SampleUIProgram { get; set; } = "";

    public string SampleJobSchedule { get; set; } = "";
    public string SampleJobStart { get; set; } = "";

    public bool AddProducer { get; set; } = true;
    public bool AddPublisher { get; set; } = true;
    public bool AddConsumer { get; set; } = true;
    public bool AddSubscriber { get; set; } = true;
    public bool GenerateSpec { get; set; } = false;
    public bool GenerateCode { get; set; } = false;

    public string CodeGenClassNameDA { get; set; } = "Dao"; //Mapper
    public string CodeGenClassNameJDA { get; set; } = "JDao"; //JMapper
    public bool CodeGenSQLWithDA { get; set; } = false;
  }
}
