namespace ReportCenter
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ofdExcelData = new System.Windows.Forms.OpenFileDialog();
            this.webBrowserReport = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFile = new System.Windows.Forms.ToolStripButton();
            this.tstbFileName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbOutputOption = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShowCrossTrades = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemQuestionTrades = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowInIE = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdHTMLFile = new System.Windows.Forms.SaveFileDialog();
            this.tbConclution = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserReport
            // 
            this.webBrowserReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserReport.Location = new System.Drawing.Point(0, 25);
            this.webBrowserReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserReport.Name = "webBrowserReport";
            this.webBrowserReport.Size = new System.Drawing.Size(798, 250);
            this.webBrowserReport.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFile,
            this.tstbFileName,
            this.toolStripSeparator1,
            this.tssbOutputOption,
            this.toolStripSeparator2,
            this.tsbRun,
            this.toolStripSeparator3,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(798, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbFile
            // 
            this.tsbFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbFile.Image")));
            this.tsbFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFile.Name = "tsbFile";
            this.tsbFile.Size = new System.Drawing.Size(57, 22);
            this.tsbFile.Text = "选择文件";
            this.tsbFile.Click += new System.EventHandler(this.tsbFile_Click);
            // 
            // tstbFileName
            // 
            this.tstbFileName.Name = "tstbFileName";
            this.tstbFileName.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tssbOutputOption
            // 
            this.tssbOutputOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssbOutputOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowAll,
            this.toolStripMenuItemShowCrossTrades,
            this.toolStripMenuItemQuestionTrades});
            this.tssbOutputOption.Image = ((System.Drawing.Image)(resources.GetObject("tssbOutputOption.Image")));
            this.tssbOutputOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbOutputOption.Name = "tssbOutputOption";
            this.tssbOutputOption.Size = new System.Drawing.Size(93, 22);
            this.tssbOutputOption.Text = "选择输出模式";
            this.tssbOutputOption.ButtonClick += new System.EventHandler(this.tssbOutputOption_ButtonClick);
            // 
            // toolStripMenuItemShowAll
            // 
            this.toolStripMenuItemShowAll.Name = "toolStripMenuItemShowAll";
            this.toolStripMenuItemShowAll.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItemShowAll.Text = "显示所有交易";
            this.toolStripMenuItemShowAll.Click += new System.EventHandler(this.toolStripMenuItemShowAll_Click);
            // 
            // toolStripMenuItemShowCrossTrades
            // 
            this.toolStripMenuItemShowCrossTrades.Name = "toolStripMenuItemShowCrossTrades";
            this.toolStripMenuItemShowCrossTrades.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItemShowCrossTrades.Text = "仅显示相关交易";
            this.toolStripMenuItemShowCrossTrades.Click += new System.EventHandler(this.toolStripMenuItemShowCrossTrades_Click);
            // 
            // toolStripMenuItemQuestionTrades
            // 
            this.toolStripMenuItemQuestionTrades.Name = "toolStripMenuItemQuestionTrades";
            this.toolStripMenuItemQuestionTrades.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItemQuestionTrades.Text = "仅显示问题交易";
            this.toolStripMenuItemQuestionTrades.Click += new System.EventHandler(this.toolStripMenuItemShowQuestionTrades_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRun
            // 
            this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbRun.Image")));
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(57, 22);
            this.tsbRun.Text = "开始分析";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSave,
            this.toolStripSeparator4,
            this.toolStripMenuItemShowInIE});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(69, 22);
            this.toolStripSplitButton1.Text = "报告处理";
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemSave.Text = "保存...";
            this.toolStripMenuItemSave.Click += new System.EventHandler(this.toolStripMenuItemSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(127, 6);
            // 
            // toolStripMenuItemShowInIE
            // 
            this.toolStripMenuItemShowInIE.Name = "toolStripMenuItemShowInIE";
            this.toolStripMenuItemShowInIE.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemShowInIE.Text = "在IE中打开";
            this.toolStripMenuItemShowInIE.Click += new System.EventHandler(this.toolStripMenuItemShowInIE_Click);
            // 
            // tbConclution
            // 
            this.tbConclution.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbConclution.Location = new System.Drawing.Point(0, 287);
            this.tbConclution.Multiline = true;
            this.tbConclution.Name = "tbConclution";
            this.tbConclution.Size = new System.Drawing.Size(798, 87);
            this.tbConclution.TabIndex = 6;
            this.tbConclution.Text = "当期没有发现组合之间明显违背公平交易制度的现象";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "测试结论";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 374);
            this.Controls.Add(this.webBrowserReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbConclution);
            this.Name = "FormMain";
            this.Text = "德邦基金公平交易报告";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdExcelData;
        private System.Windows.Forms.WebBrowser webBrowserReport;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tssbOutputOption;
        private System.Windows.Forms.ToolStripTextBox tstbFileName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowCrossTrades;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemQuestionTrades;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowInIE;
        private System.Windows.Forms.SaveFileDialog sfdHTMLFile;
        private System.Windows.Forms.TextBox tbConclution;
        private System.Windows.Forms.Label label1;
    }
}

