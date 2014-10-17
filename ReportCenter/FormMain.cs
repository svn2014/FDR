using System;
using System.IO;
using System.Windows.Forms;
using ReportLib;

namespace ReportCenter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            setOutputOptionText();
        }
                
        private void tsbFile_Click(object sender, EventArgs e)
        {
            ofdExcelData.ShowDialog();
            tstbFileName.Text = ofdExcelData.FileName;
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = tstbFileName.Text;
                ReportManager.GetInstance().BuildReport(fileName);
                ReportManager.GetInstance().ReportConclution = tbConclution.Text;
                string html = ReportManager.GetInstance().GetHTMLReport();

                webBrowserReport.DocumentText = html;
            }
            catch (Exception ex)
            {
                webBrowserReport.DocumentText = ex.Message;
            }
            
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            sfdHTMLFile.DefaultExt = ".html";
            sfdHTMLFile.FileName = "德邦基金公平交易报告"+DateTime.Today.ToString("yyyyMMdd")+".html";
            sfdHTMLFile.ShowDialog();

            string fileName = sfdHTMLFile.FileName;

            SaveFile(fileName);
        }

        private void SaveFile(string fileName)
        {
            Stream stream = webBrowserReport.DocumentStream;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Flush();
            }
            stream.Close();
        }

        private void toolStripMenuItemShowInIE_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.FileName = "iexplore.exe";

            string path = Application.StartupPath;
            string fileName = path + @"\tmp.html";
            SaveFile(fileName);

            myProcess.StartInfo.Arguments = @"file://" + fileName;
            myProcess.Start(); 
        }

        private void setOutputOptionText()
        {
            toolStripMenuItemShowAll.Text = " 显示所有交易";
            toolStripMenuItemShowCrossTrades.Text = " 仅显示相关交易";
            toolStripMenuItemQuestionTrades.Text = " 仅显示问题交易";

            switch (ReportManager.GetInstance().ReportOutputOption)
            {
                case FairDealReport.OutputOption.All:
                    toolStripMenuItemShowAll.Text = "*显示所有交易";
                    tssbOutputOption.Text = "显示所有交易";
                    break;
                case FairDealReport.OutputOption.CrossTrades:
                    toolStripMenuItemShowCrossTrades.Text = "*仅显示相关交易";
                    tssbOutputOption.Text = "仅显示相关交易";
                    break;
                case FairDealReport.OutputOption.Questionable:
                    toolStripMenuItemQuestionTrades.Text = "*仅显示问题交易";
                    tssbOutputOption.Text = "仅显示问题交易";
                    break;
                default:
                    break;
            }
        }

        private void toolStripMenuItemShowCrossTrades_Click(object sender, EventArgs e)
        {
            ReportManager.GetInstance().ReportOutputOption = FairDealReport.OutputOption.CrossTrades;
            setOutputOptionText();
        }

        private void toolStripMenuItemShowAll_Click(object sender, EventArgs e)
        {
            ReportManager.GetInstance().ReportOutputOption = FairDealReport.OutputOption.All;
            setOutputOptionText();
        }

        private void toolStripMenuItemShowQuestionTrades_Click(object sender, EventArgs e)
        {
            ReportManager.GetInstance().ReportOutputOption = FairDealReport.OutputOption.Questionable;
            setOutputOptionText();
        }

        private void tssbOutputOption_ButtonClick(object sender, EventArgs e)
        {
            tssbOutputOption.DropDown.Show();
        }
    }
}
