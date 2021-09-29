using System;

namespace SDM.Common
{
  public class SMessage
  {
    public static string NO_EXIST_DB_FILE { get { return "'{0}' DB File이 없습니다"; } }

    //Top Node
    public static string SDM_TYPE_MS { get { return "Microservices"; } }
    public static string SDM_TYPE_SI { get { return "Internal Systems"; } }
    public static string SDM_TYPE_SE { get { return "External Systems"; } }

    //배포
    public static string SDM_DEPLOYMENT_FOLDER_NOT_FOUND { get { return "SDM 배포 폴더를 찾을수 없습니다"; } }
    public static string SDM_DEPLOYMENT_FOLDER_NAMING { get { return "배포폴더를 YYYY.MM.DD 형식으로 구성해주세요"; } }
    public static string SDM_UPDATE_NOTICE { get { return "SDM이 배포되었습니다." + Environment.NewLine + "SDM을 종료하고 배포폴더로 이동하시겠습니까?"; } }

    //업로드
    public static string UPLOADER_DESC { get { return "한 줄 씩 입력하세요"; } }
    public static string UPLOAD_ATTRIBUTE { get { return "속성 업로드"; } }
    public static string DTO_ATTR { get { return "ID, DataType, Attribute, Variable, Description 과 같이 입력하세요"; } }
    public static string ENTITY_ATTR { get { return "ID, DataType, Attribute, Variable, PK, FK, NOT NULL, DB DataType, DB Column, Description 과 같이 입력하세요"; } }

    //상단 메뉴
    public static string BTN_GO_BACK { get { return "뒤로 가기"; } }
    public static string BTN_DASH_BOARD { get { return "Dashbaord"; } }
    public static string BTN_REPORT { get { return "Report"; } }
    public static string BTN_VERIFICATION { get { return "모델 검증"; } }
    public static string BTN_DICTIONARY { get { return "단어사전"; } }
    public static string BTN_MY_OPTION { get { return "개인 옵션"; } }
    public static string BTN_PROJECT_OPTION { get { return "프로젝트 옵션"; } }
    public static string BTN_USER { get { return "프로젝트 파트/사용자"; } }
    public static string BTN_LOGOUT { get { return "로그아웃"; } }
    public static string BTN_HELP { get { return "도움말 및 연락처"; } }

    //좌측 트리
    public static string BTN_TREE_OPTION { get { return "Tree Option"; } }
    public static string BTN_REFRESH { get { return "새로고침 (Ctrl+R)"; } }
    public static string BTN_SYNC_TREE { get { return "우측 선택요소를 좌측 트리에 표시"; } }
    public static string BTN_FIND_NODE { get { return "찾기(Ctrl+F, 앞뒤 % 가능), 다음 찾기(F3)"; } }
    public static string NOT_FOUND { get { return "찾을수 없습니다"; } }

    //삭제/이동/저장
    public static string DELETE { get { return "'{0}' 삭제하시겠습니까?"; } }
    public static string DELETED { get { return "삭제되었습니다"; } }
    public static string HAS_CHILD_NODE { get { return "Child Node가 있어 삭제할 수 없습니다"; } }
    public static string CAN_NOT_DELETE_CONSUMER { get { return "아래와 같이 사용(호출)하는 곳이 있어 삭제할 수 없습니다" + Environment.NewLine + Environment.NewLine + "{0}"; } }
    public static string CAN_NOT_DELETE_NO_ROLE { get { return "삭제할 권한이 없습니다"; } }
    public static string CAN_NOT_MOVE { get { return "이동할 권한이 없습니다"; } }

    public static string SAVE_CONFIRM_CONSUMER
    {
      get
      {
        return 
          "아래 {0}곳에서 {1} {2}을/를 사용(호출)하고 있습니다" + Environment.NewLine +
          "그래도 저장하시겠습니까?" + Environment.NewLine + Environment.NewLine +
          "{3}";
      }
    }

    //우측 상세페이지
    public static string REQUIRED_AND_SUFFIX { get { return "'{0}' 필수 입력이고 '{1}'형식 입니다"; } }
    public static string REQUIRED { get { return "'{0}' 필수 입력 입니다"; } }
    public static string BTN_SAVE { get { return "저장 (F12 or Ctrl+S or Ctrl+Enter)"; } }
    public static string SAVED { get { return "저장되었습니다"; } }
    public static string SAVED_COUNT { get { return "{0}건이 저장되었습니다"; } }
    public static string BTN_TRANSLATE_TO_ENG { get { return "한영변환"; } }
    public static string FILE_NOT_FOUND { get { return "파일을 찾을수 없습니다"; } }

    //기본설계
    public static string NO_DEFINE { get { return "'{0}' 이/가 정의되지 않았습니다"; } }
    public static string NO_SAME_API_BR_OP { get { return "API명과 BR Operation명이 동일하지 않습니다"; } }
    public static string NO_DA_VERB { get { return "등록/수정/저장/삭제/조회 동사만 사용할 수 있습니다"; } }

    //상세설계
    public static string NO_ENG_NO { get { return "{0}을 영문 또는 숫자로 작성해주세요"; } }

    //BR Operation
    public static string BTN_EDIT_COMMENT { get { return "Comment 수정(Condition, Loop, Exception 등)"; } }
    public static string BTN_ADD_COMMENT { get { return "Comment만 추가"; } }

    //Call list type
    public static string LBL_CALL_LIST_BR_OP
    {
      get
      {
        return
          "아래 항목을 좌측 트리에서 Drag/Drop" + Environment.NewLine +
          " - BR Operation" + Environment.NewLine +
          " - DA Operation" + Environment.NewLine +
          " - API" + Environment.NewLine +
          " - Publisher" + Environment.NewLine +
          " - Other";
      }
    }
    public static string LBL_CALL_LIST_SYSTEM
    {
      get
      {
        return
          "아래 항목을 좌측 트리에서 Drag/Drop" + Environment.NewLine +
          " - API" + Environment.NewLine +
          " - Other";
      }
    }

    //Consumer/Provider
    public static string HELP_CONSUMER_API
    {
      get
      {
        return
          "이 API를 호출(사용)하는 Consumer" + Environment.NewLine +
          " - UI " + Environment.NewLine +
          " - BR Operation" + Environment.NewLine +
          " - Internal(대내) System" + Environment.NewLine +
          " - External(대외) System";
      }
    }
    public static string HELP_CONSUMER_PUB
    {
      get
      {
        return
          "이 Publisher를 호출(사용)하는 Consumer" + Environment.NewLine +
          " - BR Operation" + Environment.NewLine +
          " - Subscriber";
      }
    }
    public static string HELP_CONSUMER_OTHER
    {
      get
      {
        return
          "이 API를 호출(사용)하는 Consumer" + Environment.NewLine +
          " - BR Operation" + Environment.NewLine +
          " - Internal(대내) System" + Environment.NewLine +
          " - External(대외) System";
      }
    }
    public static string HELP_CONSUMER_BR_OP
    {
      get
      {
        return
          "이 BR Operation을 호출(사용)하는 Consumer" + Environment.NewLine +
          " - API " + Environment.NewLine +
          " - Subscriber " + Environment.NewLine +
          " - BR Operation";
      }
    }
    public static string HELP_CONSUMER_DA_OP
    {
      get
      {
        return
          "이 DA Operation을 호출(사용)하는 Consumer" + Environment.NewLine +
          " - BR Operation";
      }
    }
    public static string HELP_CONSUMER_DTO
    {
      get
      {
        return
          "이 Dto를 Input 또는 Output으로 사용하는 Consumer" + Environment.NewLine +
          " - API " + Environment.NewLine +
          " - Publisher" + Environment.NewLine +
          " - Subscriber" + Environment.NewLine +
          " - BR Operation";
      }
    }
    public static string HELP_CONSUMER_ENTITY
    {
      get
      {
        return
          "이 Entity를 Input 또는 Output으로 사용하는 Consumer" + Environment.NewLine +
          " - DA Operation";
      }
    }

    public static string HELP_PROVIDER_SUB { get{ return "이 Topic을 제공(Provider)하는 Publisher 찾기"; } }
    public static string NO_PUBLISHER { get{ return "Publisher를 찾을 수 없습니다"; } }
    public static string MOVE_PUBLISHER { get{ return "'{0}' (으)로 이동하시겠습니까?"; } }

    public static string MOVE_RIGHT { get{ return "Main창 우측으로 이동"; } }

    //권한/사용자
    public static string SELECT_PART { get { return "Part를 선택하세요"; } }
    public static string CHECK_LOGIN_PART_NAME { get { return "Part 또는 Name을 확인하세요"; } }
    public static string CHECK_LOGIN_PART_NAME_PW { get { return "Part/Name 또는 Password를 확인하세요"; } }
    public static string ROLE_VIEW_ONLY { get { return "읽기전용"; } }
    public static string ROLE_DESIGNER_DEVELOPER { get { return "본인 작성 건 만 수정 가능"; } }
    public static string ROLE_PL { get { return "작업자와 동일한 해당 파트 건 만 수정 가능"; } }
    public static string ROLE_MODELER { get { return "전체 수정 가능"; } }
    public static string NO_UD_USER { get { return "'modeler'와 'temp' 사용자는 수정/삭제할 수 없습니다"; } }
    public static string CHANGED_TEMP_USER { get { return "누구나 수정할 수 있도록 임시 사용자로 변경되었습니다"; } }
    public static string ROLE_DESCRIPTION
    {
      get
      {
        return
          "Designer/Developer : 본인 작성 건 만 수정 가능" + Environment.NewLine +
          "PL : 작업자와 동일한 해당 파트 건 만 수정 가능" + Environment.NewLine +
          "Modeler : 전체 수정 가능" + Environment.NewLine +
          "QA/PM : 읽기전용";
      }
    }

    //BR call list
    public static string NO_CALLEE_OTHER_MS_BR_OP { get { return "타 Microservice의 BR Operation은 추가할 수 없습니다"; } }
    public static string NO_CALLEE_OTHER_BP_DA_OP { get { return "타 BizPackage의 DA Operation은 추가할 수 없습니다"; } }
    public static string NO_CALLEE_SAME_MS_API { get { return "동일 Microservice의 API는 호출할 수 없습니다" + Environment.NewLine + "대응되는 BR Operation을 호출하세요"; } }
    public static string NO_CALLEE_OTHER_MS_PUB { get { return "타 Microservice의 Publisher는 추가할 수 없습니다"; } }
    public static string NO_CALLEE_OTHER_MS_OTHER { get { return "타 Microservice의 Other는추가할 수 없습니다"; } }
    public static string DND_CALL_LIST { get { return "좌측 트리뷰에서 API/Publisher/Other/Operation을 Drag & Drop하세요"; } }

    //API/SUB callee br operation
    public static string NO_CALLEE_OTHER_BP_BR_OP { get { return "타 BizPackage의 BR Operation은 지정할 수 없습니다"; } }

    //Input/Output
    public static string NO_OTHER_MS_DTO { get { return "타 Microservice의 Dto는 추가할 수 없습니다"; } }
    public static string NO_OTHER_MS_ENTITY { get { return "타 Microservice의 Entity는 추가할 수 없습니다"; } }

    //사전
    public static string NO_DIC { get { return "사전으로 로드해주세요"; } }
    public static string DIC_DISPLAY { get { return "명사: {0}, BR동사 : {1}, DA동사 : {2}"; } }

    //Project option
    public static string PROJECT_FOLDER_DESC
    {
      get
      {
        return
          "Project Folder 하위에는 아래 3개 폴더 필요" + Environment.NewLine +
          "- Dictionary 폴더(dictionary)" + Environment.NewLine +
          "- Spec 템플릿 폴더(template_code)" + Environment.NewLine +
          "- Code 템플릿 폴더(template_code)";
      }
    }
    public static string NO_PROJECT_FOLDER { get { return "프로젝트 폴더({0})가 존재하지 않습니다"; } }

    //Spec
    public static string NO_GENERATE { get { return "기본설계 및 상세설계 완료 후 생성할 수 있습니다(하위 포함)"; } }
    public static string GENERATE_CHILD_COMPONENT { get { return "호출하는 컴포넌트도 함께 생성하시겠습니까?"; } }
    public static string GENERATE_SQL { get { return "SQL XML도 함께 생성하시겠습니까?"; } }

    //My Option
    public static string INPUT_PW { get { return "Password를 입력하세요"; } }
    public static string NO_MATCH_PW { get { return "Password와 Password Confirmation이 다릅니다"; } }

    //Word
    public static string IS_DUPLICATED { get { return "'{0}' 은/는 등록되어 있습니다"; } }
    public static string CAN_BE_ENG_OR_NO { get { return "영문 또는 숫자만 가능합니다"; } }

  }

}
