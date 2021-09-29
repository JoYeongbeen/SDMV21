using System;
using System.Windows.Forms;

using SDM.Common;
using SDM.Project;

namespace SDM
{
  public partial class FormProjectOption : Form
  {
    //public delegate void ProjectOptionChangeHandler(bool refresh);
    public delegate void ProjectOptionChangeHandler();
    public event ProjectOptionChangeHandler ProjectOptionChanged;

    private SDA SDA;
    private SProject SProject;

    public FormProjectOption()
    {
      InitializeComponent();
    }

    private void FormProjectOption_Load(object sender, EventArgs e)
    {
      this.Text = SCommon.ProductVersion + " - Project Option";
      this.SDA = new SDA();
      this.SetControl();
      this.SelectComponent();
    }

    private void SetControl()
    {
      this.toolTip1.SetToolTip(this.BtnHelpProjectFolder, SMessage.PROJECT_FOLDER_DESC);
    }

    private void SelectComponent()
    {
      this.SProject = this.SDA.SelectProject();

      this.TbxProjectName.Text = this.SProject.Name;
      this.TbxProjectLogoURL.Text = this.SProject.LogoURL;
      this.TbxContact.Text = this.SProject.Contact;
      this.TbxProjectFolder.Text = this.SProject.ProjectFolder;
      this.TbxDeployFolder.Text = this.SProject.DeployFolder;
      this.CkbDictionary.Checked = this.SProject.Dictionary;

      this.TbxMSCode.Text = this.SProject.SampleMSCode;
      this.TbxBPCode.Text = this.SProject.SampleBPCode;
      this.TbxBPSourcePackage.Text = this.SProject.SampleBPSourcePackage;

      this.TbxControllerName.Text = this.SProject.SampleControllerName;
      this.TbxControllerClassName.Text = this.SProject.SampleControllerClass;
      this.TbxControllerURI.Text = this.SProject.SampleControllerURI;

      this.TbxAPIName.Text = this.SProject.SampleAPIName;
      this.TbxAPIMethod.Text = this.SProject.SampleAPIMethod;
      this.TbxAPIURI.Text = this.SProject.SampleAPIURL;
      this.TbxAPIInput.Text = this.SProject.SampleAPIInput;
      this.TbxAPIOutput.Text = this.SProject.SampleAPIOutput;

      this.TbxProducerName.Text = this.SProject.SampleProducerName;
      this.TbxProducerClass.Text = this.SProject.SampleProducerClass;

      this.TbxPubName.Text = this.SProject.SamplePubName;
      this.TbxPubMethod.Text = this.SProject.SamplePubMethod;
      this.TbxPubInput.Text = this.SProject.SamplePubInput;
      this.TbxPubTopic.Text = this.SProject.SamplePubTopic;

      this.TbxConsumerName.Text = this.SProject.SampleConsumerName;
      this.TbxConsumerClass.Text = this.SProject.SampleConsumerClass;

      this.TbxSubName.Text = this.SProject.SampleSubName;
      this.TbxSubMethod.Text = this.SProject.SampleSubMethod;
      this.TbxSubInput.Text = this.SProject.SampleSubInput;

      this.TbxDtoName.Text = this.SProject.SampleDtoName;
      this.TbxDtoClass.Text = this.SProject.SampleDtoClass;

      this.TbxEntityName.Text = this.SProject.SampleEntityName;
      this.TbxEntityClass.Text = this.SProject.SampleEntityClass;
      this.TbxEntityTable.Text = this.SProject.SampleEntityTable;

      this.TbxBRName.Text = this.SProject.SampleBRName;
      this.TbxBRClass.Text = this.SProject.SampleBRClass;
      this.TbxBROpName.Text = this.SProject.SampleBROpName;
      this.TbxBROpMethod.Text = this.SProject.SampleBROpMethod;

      this.TbxDAName.Text = this.SProject.SampleDAName;
      this.TbxDAClass.Text = this.SProject.SampleDAClass;
      this.TbxDAOpName.Text = this.SProject.SampleDAOpName;
      this.TbxDAOpMethod.Text = this.SProject.SampleDAOpMethod;

      this.TbxUIName.Text = this.SProject.SampleUIName;
      this.TbxUIProgram.Text = this.SProject.SampleUIProgram;

      this.TbxJobSchedule.Text = this.SProject.SampleJobSchedule;
      this.TbxJobStart.Text = this.SProject.SampleJobStart;

      this.CkbAddProducer.Checked = this.SProject.AddProducer;
      this.CkbAddPublisher.Checked = this.SProject.AddPublisher;
      this.CkbAddConsumer.Checked = this.SProject.AddConsumer;
      this.CkbAddSubscriber.Checked = this.SProject.AddSubscriber;
      this.CkbGenerateSpec.Checked = this.SProject.GenerateSpec;
      this.CkbGenerateCode.Checked = this.SProject.GenerateCode;

      this.TbxGCDAClassName.Text = this.SProject.CodeGenClassNameDA;
      this.TbxGCJDAClassName.Text = this.SProject.CodeGenClassNameJDA;
      this.CkbGenerateSQL.Checked = this.SProject.CodeGenSQLWithDA;
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
      this.SProject.Name = this.TbxProjectName.Text;
      this.SProject.LogoURL = this.TbxProjectLogoURL.Text;
      this.SProject.Contact = this.TbxContact.Text;
      this.SProject.ProjectFolder = this.TbxProjectFolder.Text;
      this.SProject.DeployFolder = this.TbxDeployFolder.Text;
      this.SProject.Dictionary = this.CkbDictionary.Checked;

      this.SProject.SampleMSCode = this.TbxMSCode.Text;
      this.SProject.SampleBPCode = this.TbxBPCode.Text;
      this.SProject.SampleBPSourcePackage = this.TbxBPSourcePackage.Text;
      
      this.SProject.SampleControllerName = this.TbxControllerName.Text;
      this.SProject.SampleControllerClass = this.TbxControllerClassName.Text;
      this.SProject.SampleControllerURI = this.TbxControllerURI.Text;
      
      this.SProject.SampleAPIName = this.TbxAPIName.Text;
      this.SProject.SampleAPIMethod = this.TbxAPIMethod.Text;
      this.SProject.SampleAPIURL = this.TbxAPIURI.Text;
      this.SProject.SampleAPIInput = this.TbxAPIInput.Text;
      this.SProject.SampleAPIOutput = this.TbxAPIOutput.Text;
      
      this.SProject.SampleProducerName = this.TbxProducerName.Text;
      this.SProject.SampleProducerClass = this.TbxProducerClass.Text;
      
      this.SProject.SamplePubName = this.TbxPubName.Text;
      this.SProject.SamplePubMethod = this.TbxPubMethod.Text;
      this.SProject.SamplePubInput = this.TbxPubInput.Text;
      this.SProject.SamplePubTopic = this.TbxPubTopic.Text;
      
      this.SProject.SampleConsumerName = this.TbxConsumerName.Text;
      this.SProject.SampleConsumerClass = this.TbxConsumerClass.Text;
      
      this.SProject.SampleSubName = this.TbxSubName.Text;
      this.SProject.SampleSubMethod = this.TbxSubMethod.Text;
      this.SProject.SampleSubInput = this.TbxSubInput.Text;
      
      this.SProject.SampleDtoName = this.TbxDtoName.Text;
      this.SProject.SampleDtoClass = this.TbxDtoClass.Text;
      
      this.SProject.SampleEntityName = this.TbxEntityName.Text;
      this.SProject.SampleEntityClass = this.TbxEntityClass.Text;
      this.SProject.SampleEntityTable = this.TbxEntityTable.Text;
      
      this.SProject.SampleBRName = this.TbxBRName.Text;
      this.SProject.SampleBRClass = this.TbxBRClass.Text;
      this.SProject.SampleBROpName = this.TbxBROpName.Text;
      this.SProject.SampleBROpMethod = this.TbxBROpMethod.Text;
      
      this.SProject.SampleDAName = this.TbxDAName.Text;
      this.SProject.SampleDAClass = this.TbxDAClass.Text;
      this.SProject.SampleDAOpName = this.TbxDAOpName.Text;
      this.SProject.SampleDAOpMethod = this.TbxDAOpMethod.Text;
      
      this.SProject.SampleUIName = this.TbxUIName.Text;
      this.SProject.SampleUIProgram = this.TbxUIProgram.Text;
      
      this.SProject.SampleJobSchedule = this.TbxJobSchedule.Text;
      this.SProject.SampleJobStart = this.TbxJobStart.Text;
      
      this.SProject.AddProducer = this.CkbAddProducer.Checked;
      this.SProject.AddPublisher = this.CkbAddPublisher.Checked;
      this.SProject.AddConsumer = this.CkbAddConsumer.Checked;
      this.SProject.AddSubscriber = this.CkbAddSubscriber.Checked;
      this.SProject.GenerateSpec = this.CkbGenerateSpec.Checked;
      this.SProject.GenerateCode = this.CkbGenerateCode.Checked;
      
      this.SProject.CodeGenClassNameDA = this.TbxGCDAClassName.Text;
      this.SProject.CodeGenClassNameJDA = this.TbxGCJDAClassName.Text;
      this.SProject.CodeGenSQLWithDA = this.CkbGenerateSQL.Checked;

      this.SDA.UpdateProject(this.SProject);
      SCommon.SProject = this.SProject;

      SMessageBox.ShowInformation(SMessage.SAVED);
      //this.ProjectOptionChanged(false);
      this.ProjectOptionChanged();
      this.Close();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    private void BtnSearchProjectFolder_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        if (this.TbxProjectFolder.Text.Length > 0)
          folderBrowserDialog.SelectedPath = this.TbxProjectFolder.Text;

        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
          this.TbxProjectFolder.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void BtnSearchDeployFolder_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        if (this.TbxDeployFolder.Text.Length > 0)
          folderBrowserDialog.SelectedPath = this.TbxDeployFolder.Text;

        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
          this.TbxDeployFolder.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void BtnHelpProjectFolder_Click(object sender, EventArgs e)
    {
      SMessageBox.ShowInformation(SMessage.PROJECT_FOLDER_DESC);
    }
  }
}
