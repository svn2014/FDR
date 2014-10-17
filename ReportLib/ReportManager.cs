using System;
using System.Collections.Generic;
using System.Data;

namespace ReportLib
{
    public class ReportManager
    {
        private string _conclution = "";
        private DateTime _EndDate = new DateTime(0x76c, 1, 1);
        private string _FileName = "";
        private DataTable _FundList = null;
        private List<long> _FundNoList = new List<long>();
        private static ReportManager _Instance;
        private FairDealReport.OutputOption _outputOption = FairDealReport.OutputOption.CrossTrades;
        private List<FairDealReport> _ReportList = new List<FairDealReport>();
        private DateTime _StartDate = DateTime.Today;
        private DataTable _TransactionData = null;

        public ReportManager()
        {
            this.initialize();
        }

        public void BuildReport(string fileName)
        {
            if (this._FileName != fileName)
            {
                this.initialize();
                this._TransactionData = CSVDataService.GetInstance().GetTradingData(fileName);
                this._FileName = fileName;
                this._TransactionData.Columns.Add(new DataColumn("TDate", Type.GetType("System.DateTime")));
                this._FundList.Clear();
                this._FundNoList.Clear();
                foreach (DataRow row in this._TransactionData.Rows)
                {
                    if (row["基金编号"].ToString().Trim().Length != 0)
                    {
                        long item = Convert.ToInt64(row["基金编号"]);
                        string str = row["基金名称"].ToString();
                        DateTime time = Convert.ToDateTime(row["发生日期"]);
                        if (time > this._EndDate)
                        {
                            this._EndDate = time;
                        }
                        if (time < this._StartDate)
                        {
                            this._StartDate = time;
                        }
                        row["TDate"] = time;
                        if (!this._FundNoList.Contains(item))
                        {
                            this._FundNoList.Add(item);
                            DataRow row2 = this._FundList.NewRow();
                            row2["基金编号"] = item;
                            row2["基金名称"] = str;
                            this._FundList.Rows.Add(row2);
                        }
                    }
                }
                this._FundList.DefaultView.Sort = "基金编号";
                this._FundList = this._FundList.DefaultView.ToTable();
                for (int i = 0; i < this._FundList.Rows.Count; i++)
                {
                    for (int j = i + 1; j < this._FundList.Rows.Count; j++)
                    {
                        DataRow[] rowsAB = this._TransactionData.Select(string.Concat(new object[] { "基金编号 = '", this._FundList.Rows[i]["基金编号"], "' OR 基金编号 = '", this._FundList.Rows[j]["基金编号"], "'" }));
                        DataRow[] rowsA = this._TransactionData.Select("基金编号 = '" + this._FundList.Rows[i]["基金编号"] + "'");
                        DataRow[] rowsB = this._TransactionData.Select("基金编号 = '" + this._FundList.Rows[j]["基金编号"] + "'");
                        FairDealReport report = new FairDealReport(rowsAB, rowsA, rowsB, FairDealReport.TimeWindow.In1TradingDay) {
                            ReportOutputOption = this.ReportOutputOption
                        };
                        this._ReportList.Add(report);
                        report = new FairDealReport(rowsAB, rowsA, rowsB, FairDealReport.TimeWindow.In3TradingDays) {
                            ReportOutputOption = this.ReportOutputOption
                        };
                        this._ReportList.Add(report);
                        report = new FairDealReport(rowsAB, rowsA, rowsB, FairDealReport.TimeWindow.In5TradingDays) {
                            ReportOutputOption = this.ReportOutputOption
                        };
                        this._ReportList.Add(report);
                    }
                }
            }
        }

        private string GetHTMLFundsTable()
        {
            int num2 = (this._FundList.Rows.Count / 2) + 1;
            string str = "<h4>一、组合名称</h4>";
            str = str + "<Table width=\"100%\">";
            for (int i = 0; i < num2; i++)
            {
                string str2 = "";
                str2 = this._FundList.Rows[i]["基金名称"].ToString().Trim();
                str = ((str + "<tr>") + "<td width=\"10%\">基金" + this._FundList.Rows[i]["基金编号"].ToString() + "</td>") + "<td>" + ((str2.Length == 0) ? "(未命名)" : str2) + "</td>";
                if ((i + num2) < this._FundList.Rows.Count)
                {
                    str2 = this._FundList.Rows[i + num2]["基金名称"].ToString().Trim();
                    str = (str + "<td width=\"10%\">基金" + this._FundList.Rows[i + num2]["基金编号"].ToString() + "</td>") + "<td>" + ((str2.Length == 0) ? "(未命名)" : str2) + "</td>";
                }
                else
                {
                    str = str + "<td>&nbsp;</td><td>&nbsp;</td>";
                }
                str = str + "</tr>";
            }
            return (str + "</Table>");
        }

        public string GetHTMLReport()
        {
            string str = "<html>";
            return (((((str + "<head></head>" + "<body>") + this.GetHTMLReportTitle() + this.GetHTMLFundsTable()) + this.GetHTMLReportTimeWindow() + this.GetHTMLReportSummary()) + this.GetHTMLReportDetail() + this.GetHTMLReportConclution()) + "</body>" + "</html>");
        }

        private string GetHTMLReportConclution()
        {
            string str = "<h4>五、结论</h4>";
            if (this._conclution.Contains("\r"))
            {
                string[] strArray = this._conclution.Split("\r\n".ToCharArray());
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].Trim().Length != 0)
                    {
                        str = str + "<P>" + strArray[i] + "</P>";
                    }
                }
                return str;
            }
            return (str + "<P>" + this._conclution + "</P>");
        }

        private string GetHTMLReportDetail()
        {
            string str = "<h4>四、交易明细</h4>";
            foreach (FairDealReport report in this._ReportList)
            {
                report.ReportOutputOption = this._outputOption;
                str = str + report.GetDetailHTMLTable();
            }
            return str;
        }

        private string GetHTMLReportSummary()
        {
            string str = "<h4>三、报告摘要</h4>";
            foreach (FairDealReport report in this._ReportList)
            {
                report.ReportOutputOption = this._outputOption;
                str = str + report.GetSummaryHTMLTable();
            }
            return str;
        }

        private string GetHTMLReportTimeWindow()
        {
            string str = "<h4>二、报告期</h4>";
            return (str + this._StartDate.ToString("yyyy-MM-dd") + "至" + this._EndDate.ToString("yyyy-MM-dd"));
        }

        private string GetHTMLReportTitle()
        {
            string str = "<h1 align=\"center\">德邦基金公平交易报告</h1>";
            return ((str + "<h4 align=\"center\">报告日期：" + DateTime.Today.ToString("yyyy-MM-dd") + "</h4>") + "<hr/>");
        }

        public static ReportManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new ReportManager();
            }
            return _Instance;
        }

        private void initialize()
        {
            if (this._FundList == null)
            {
                this._FundList = new DataTable();
                this._FundList.Columns.Add(new DataColumn("基金编号", Type.GetType("System.Int64")));
                this._FundList.Columns.Add(new DataColumn("基金名称", Type.GetType("System.String")));
            }
            if (this._TransactionData != null)
            {
                this._TransactionData.Clear();
            }
            this._FundList.Clear();
            this._FundNoList.Clear();
            this._ReportList.Clear();
            this._StartDate = DateTime.Today;
            this._EndDate = new DateTime(0x76c, 1, 1);
        }

        public string ReportConclution
        {
            get
            {
                return this._conclution;
            }
            set
            {
                this._conclution = value;
            }
        }

        public FairDealReport.OutputOption ReportOutputOption
        {
            get
            {
                return this._outputOption;
            }
            set
            {
                this._outputOption = value;
            }
        }
    }
}

