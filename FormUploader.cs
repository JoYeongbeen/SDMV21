using System;
using System.Windows.Forms;

using SDM.Common;
using SDM.Component;

namespace SDM
{
  public partial class FormUploader : Form
  {
    public SComponent SelectedComponent;

    public delegate void UploaderChangeHandler(string componentType, string[] inputList);
    public event UploaderChangeHandler UploaderChanged;

    public FormUploader()
    {
      InitializeComponent();
    }

    private void FormUploader_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Uploader";
      this.LblUploaderDescription.Text = SMessage.UPLOADER_DESC;

      this.RbnAPI.Enabled = false;
      this.RbnPub.Enabled = false;
      this.RbnSub.Enabled = false;
      this.RbnOther.Enabled = false;
      this.RbnBatch.Enabled = false;
      this.RbnOperation.Enabled = false;
      this.RbnAttribute.Enabled = false;

      if (this.SelectedComponent is SBizPackage)
      {
        this.RbnAPI.Enabled = true;
        this.RbnPub.Enabled = true;
        this.RbnSub.Enabled = true;
        this.RbnOther.Enabled = true;
        this.RbnBatch.Enabled = true;

        this.RbnAPI.Checked = true;
      }
      else if (this.SelectedComponent is SInternalSystem || this.SelectedComponent is SExternalSystem)
      {
        this.RbnAPI.Enabled = true;
        this.RbnPub.Enabled = true;
        this.RbnSub.Enabled = true;
        this.RbnOther.Enabled = true;

        this.RbnAPI.Checked = true;
      }
      else if (this.SelectedComponent is SController)
      {
        this.RbnAPI.Enabled = true;

        this.RbnAPI.Checked = true;
      }
      else if (this.SelectedComponent is SProducer)
      {
        this.RbnPub.Enabled = true;

        this.RbnPub.Checked = true;
      }
      else if (this.SelectedComponent is SConsumer)
      {
        this.RbnSub.Enabled = true;

        this.RbnSub.Checked = true;
      }
      else if (this.SelectedComponent is SBizRule || this.SelectedComponent is SDataAccess)
      {
        this.RbnOperation.Enabled = true;

        this.RbnOperation.Checked = true;
      }
      else if(this.SelectedComponent is SDto || this.SelectedComponent is SEntity)
      {
        this.RbnAttribute.Enabled = true;

        this.RbnAttribute.Checked = true;
      }

      this.SetExample();
      this.TbxInput.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
      string type = string.Empty;

      if (this.RbnAPI.Checked)
        type = "A";
      else if (this.RbnPub.Checked)
        type = "P";
      else if (this.RbnSub.Checked)
        type = "S";
      else if (this.RbnOther.Checked)
        type = "O";
      else if (this.RbnBatch.Checked)
        type = "B";
      else if (this.RbnOperation.Checked)
        type = "OP";
      else if (this.RbnAttribute.Checked)
        type = "AT";

      //input list
      string[] inputList = this.TbxInput.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

      if(type == "AT" && this.SelectedComponent is SDto)
      {
        foreach(string input in inputList)
        {
          if (input.Split(',').Length != 5)
          {
            SMessageBox.ShowWarning(SMessage.DTO_ATTR);
            return;
          }
        }
      }
      else if (type == "AT" && this.SelectedComponent is SEntity)
      {
        foreach (string input in inputList)
        {
          //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
          if (input.Split(',').Length != 10)
          {
            SMessageBox.ShowWarning(SMessage.ENTITY_ATTR);
            return;
          }
        }
      }

      this.UploaderChanged(type, inputList);
      this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void RbnAPI_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnPub_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnSub_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnOther_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnBatch_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnOperation_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnDtoAttr_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void RbnEntityAttr_CheckedChanged(object sender, EventArgs e)
    {
      this.SetExample();
    }

    private void SetExample()
    {
      if (this.RbnAPI.Checked)
        this.TbxInput.Text = "(예시1)주문 접수" + Environment.NewLine + "(예시2)주문 취소";
      else if (this.RbnPub.Checked)
        this.TbxInput.Text = "(예시1)승인 상품 발행" + Environment.NewLine + "(예시2)승인 취소 상품 발행";
      else if (this.RbnSub.Checked)
        this.TbxInput.Text = "(예시1)승인 상품 구독" + Environment.NewLine + "(예시2)승인 취소 상품 구독";
      else if (this.RbnOther.Checked)
        this.TbxInput.Text = "(예시1)발주 계약서 전송" + Environment.NewLine + "(예시2)발주 취소 계약서 전송";
      else if (this.RbnBatch.Checked)
        this.TbxInput.Text = "(예시1)입고 발주 마감" + Environment.NewLine + "(예시2)미입고 발주 마감";
      else if (this.RbnOperation.Checked && this.SelectedComponent is SBizRule)
        this.TbxInput.Text = "(예시1)주문 접수" + Environment.NewLine + "(예시2)주문 취소";
      else if (this.RbnOperation.Checked && this.SelectedComponent is SDataAccess)
        this.TbxInput.Text = "(예시1)주문 등록" + Environment.NewLine + "(예시2)주문 조회";
      else if (this.RbnAttribute.Checked && this.SelectedComponent is SDto)
        this.TbxInput.Text = "1,int,주문 번호,orderNo,주문번호설명(예시1)" + System.Environment.NewLine + "2,String,주문 상태 코드,orderStatusCd,주문상태코드설명(예시2)";
      else if (this.RbnAttribute.Checked && this.SelectedComponent is SEntity)
        this.TbxInput.Text =
          "1,int,주문 번호,orderNo,true,false,true,NUMBER(6),ORDER_NO,주문번호설명(예시1)" + 
          System.Environment.NewLine +
          "2,String,주문 상태 코드,orderStatusCd,false,true,true,VARCHAR(2),ORDER_STATUS_CD,주문상태코드설명(예시2)";
      //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
    }
  }
}
