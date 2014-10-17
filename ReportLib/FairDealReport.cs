using System;
using System.Collections.Generic;
using System.Data;

namespace ReportLib
{   
    public class FairDealReport
    {
        private List<string> _CheckActionList = new List<string>();
        private double _contributionThreshhold = 0.05;
        private DataTable _FundA;
        private DataTable _FundAB;
        private string _FundAName = "";
        private string _FundANo = "";
        private DataTable _FundB;
        private string _FundBName = "";
        private string _FundBNo = "";
        private bool _IsQuestionableDeal = false;
        private OutputOption _outputOption = OutputOption.CrossTrades;
        private double _premiumThreshhold = 0.01;
        private DataTable _ReportDetail = null;
        private DataTable _ReportSummary = null;
        private TimeWindow _TimeWindow = TimeWindow.In1TradingDay;
        private string _TimeWindowText = "1日";
        public const string C_Action_BuyBond = "债券买入";
        public const string C_Action_BuyStock = "买入";
        public const string C_Action_SellBond = "债券卖出";
        public const string C_Action_SellStock = "卖出";
        public const string C_ColumnName_Action = "委托方向";
        public const string C_ColumnName_Amount = "成交金额";
        public const string C_ColumnName_AssetCategory = "资产类别";
        public const string C_ColumnName_AvgPx = "成交均价";
        public const string C_ColumnName_Contribution = "贡献率";
        public const string C_ColumnName_Currency = "币种";
        public const string C_ColumnName_Date = "TDate";
        public const string C_ColumnName_FundA = "基金A";
        public const string C_ColumnName_FundB = "基金B";
        public const string C_ColumnName_FundName = "基金名称";
        public const string C_ColumnName_FundNo = "基金编号";
        public const string C_ColumnName_GroupName = "组合名称";
        public const string C_ColumnName_GroupNo = "组合编号";
        public const string C_ColumnName_Id = "序号";
        public const string C_ColumnName_Instructor = "指令下达人";
        public const string C_ColumnName_Premium = "溢价率";
        public const string C_ColumnName_PremiumAmount = "利益输送";
        public const string C_ColumnName_Rule = "规则";
        public const string C_ColumnName_SecurityCode = "证券代码";
        public const string C_ColumnName_SecurityName = "证券名称";
        public const string C_ColumnName_TimeWindow = "时间窗口";
        public const string C_ColumnName_Trader = "交易员";
        public const string C_ColumnName_TransactionDate = "发生日期";
        public const string C_ColumnName_Transactions = "成交次数";
        public const string C_ColumnName_Volumn = "成交数量";
        public const string C_ColumnName_WindowDate = "窗口期末";

        public FairDealReport(DataRow[] RowsAB, DataRow[] RowsA, DataRow[] RowsB, TimeWindow timeWindow)
        {
            if ((RowsA.Length != 0) && (RowsB.Length != 0))
            {
                this.initReportTable();
                this._TimeWindow = timeWindow;
                switch (this._TimeWindow)
                {
                    case TimeWindow.In1TradingDay:
                        this._TimeWindowText = "1日";
                        this._premiumThreshhold = 0.01;
                        break;

                    case TimeWindow.In3TradingDays:
                        this._TimeWindowText = "3日";
                        this._premiumThreshhold = 0.03;
                        break;

                    case TimeWindow.In5TradingDays:
                        this._TimeWindowText = "5日";
                        this._premiumThreshhold = 0.05;
                        break;
                }
                this._FundAB = RowsAB.CopyToDataTable<DataRow>();
                this._FundAB.DefaultView.Sort = "发生日期, 证券代码, 委托方向";
                this._FundAB = this._FundAB.DefaultView.ToTable();
                this._FundA = RowsA.CopyToDataTable<DataRow>();
                this._FundA.DefaultView.Sort = "发生日期, 证券代码, 委托方向";
                this._FundA = this._FundA.DefaultView.ToTable();
                this._FundB = RowsB.CopyToDataTable<DataRow>();
                this._FundB.DefaultView.Sort = "发生日期, 证券代码, 委托方向";
                this._FundB = this._FundB.DefaultView.ToTable();
                this._FundAName = this._FundA.Rows[0]["基金名称"].ToString();
                this._FundBName = this._FundB.Rows[0]["基金名称"].ToString();
                this._FundANo = this._FundA.Rows[0]["基金编号"].ToString();
                this._FundBNo = this._FundB.Rows[0]["基金编号"].ToString();
                this.Compare1();
                this.Compare2();
            }
        }

        private void Compare1()
        {
            for (int i = 0; i < this._FundAB.Rows.Count; i++)
            {
                string code = this._FundAB.Rows[i]["证券代码"].ToString();
                string name = this._FundAB.Rows[i]["证券名称"].ToString();
                string item = this._FundAB.Rows[i]["委托方向"].ToString();
                DateTime tradeDate = Convert.ToDateTime(this._FundAB.Rows[i]["发生日期"]);
                if (this._CheckActionList.Contains(item))
                {
                    DateTime today = DateTime.Today;
                    DateTime windowEndDate = DateTime.Today;
                    this.GetWindowDate(tradeDate, ref today, ref windowEndDate);
                    DataRow[] rowArray = this._FundA.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", item, "'" }));
                    DataRow[] rowArray2 = this._FundB.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", item, "'" }));
                    if ((rowArray.Length != 0) && (rowArray2.Length != 0))
                    {
                        double num2 = 0.0;
                        double num3 = 0.0;
                        double num4 = 0.0;
                        double num5 = 0.0;
                        double num6 = 0.0;
                        double num7 = 0.0;
                        foreach (DataRow row in rowArray)
                        {
                            num4 += Convert.ToDouble(row["成交数量"]);
                            num5 += Convert.ToDouble(row["成交金额"]);
                        }
                        if (num4 > 0.0)
                        {
                            num2 = num5 / num4;
                        }
                        else
                        {
                            num2 = 0.0;
                        }
                        foreach (DataRow row in rowArray2)
                        {
                            num6 += Convert.ToDouble(row["成交数量"]);
                            num7 += Convert.ToDouble(row["成交金额"]);
                        }
                        if (num6 > 0.0)
                        {
                            num3 = num7 / num6;
                        }
                        else
                        {
                            num3 = 0.0;
                        }
                        double premium = 0.0;
                        double premiumAmount = 0.0;
                        double contribution = 0.0;
                        switch (item)
                        {
                            case "买入":
                            case "债券买入":
                                premium = (num3 / num2) - 1.0;
                                premiumAmount = num4 * (num3 - num2);
                                break;
                        }
                        if ((item == "卖出") || (item == "债券卖出"))
                        {
                            premium = (num2 / num3) - 1.0;
                            premiumAmount = num4 * (num2 - num3);
                        }
                        this.WriteSummaryTable(today, windowEndDate, code, name, premium, premiumAmount, contribution, "同向", item, i + 1);
                        this.WriteDetailTable(today, windowEndDate, code, item, i + 1, false);
                        if (this.IsQuestionable(premium, contribution))
                        {
                            this._IsQuestionableDeal = true;
                        }
                    }
                }
            }
        }

        private void Compare2()
        {
            for (int i = 0; i < this._FundAB.Rows.Count; i++)
            {
                string code = this._FundAB.Rows[i]["证券代码"].ToString();
                string name = this._FundAB.Rows[i]["证券名称"].ToString();
                string item = this._FundAB.Rows[i]["委托方向"].ToString();
                DateTime tradeDate = Convert.ToDateTime(this._FundAB.Rows[i]["发生日期"]);
                double num2 = Convert.ToDouble(this._FundAB.Rows[i]["成交金额"]);
                double num3 = Convert.ToDouble(this._FundAB.Rows[i]["成交次数"]);
                if (this._CheckActionList.Contains(item))
                {
                    string reverseAction = this.GetReverseAction(item);
                    DateTime today = DateTime.Today;
                    DateTime windowEndDate = DateTime.Today;
                    this.GetWindowDate(tradeDate, ref today, ref windowEndDate);
                    DataRow[] rowArray = this._FundA.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", item, "'" }));
                    DataRow[] rowArray2 = this._FundB.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", reverseAction, "'" }));
                    if ((rowArray.Length == 0) && (rowArray2.Length == 0))
                    {
                        rowArray = this._FundA.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", reverseAction, "'" }));
                        rowArray2 = this._FundB.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", today, "' AND TDate<='", windowEndDate, "' AND 委托方向='", item, "'" }));
                    }
                    if ((rowArray.Length != 0) && (rowArray2.Length != 0))
                    {
                        double num4 = 0.0;
                        double num5 = 0.0;
                        double num6 = 0.0;
                        double num7 = 0.0;
                        double num8 = 0.0;
                        double num9 = 0.0;
                        foreach (DataRow row in rowArray)
                        {
                            num6 += Convert.ToDouble(row["成交数量"]);
                            num7 += Convert.ToDouble(row["成交金额"]);
                        }
                        if (num6 > 0.0)
                        {
                            num4 = num7 / num6;
                        }
                        else
                        {
                            num4 = 0.0;
                        }
                        foreach (DataRow row in rowArray2)
                        {
                            num8 += Convert.ToDouble(row["成交数量"]);
                            num9 += Convert.ToDouble(row["成交金额"]);
                        }
                        if (num8 > 0.0)
                        {
                            num5 = num9 / num8;
                        }
                        else
                        {
                            num5 = 0.0;
                        }
                        double premium = 0.0;
                        double premiumAmount = 0.0;
                        double contribution = 0.0;
                        switch (item)
                        {
                            case "买入":
                            case "债券买入":
                                premium = (num5 / num4) - 1.0;
                                premiumAmount = num6 * (num5 - num4);
                                break;
                        }
                        if ((item == "卖出") || (item == "债券卖出"))
                        {
                            premium = (num4 / num5) - 1.0;
                            premiumAmount = num6 * (num4 - num5);
                        }
                        this.WriteSummaryTable(today, windowEndDate, code, name, premium, premiumAmount, contribution, "反向", item, i + 1);
                        this.WriteDetailTable(today, windowEndDate, code, item, i + 1, true);
                        if (this.IsQuestionable(premium, contribution))
                        {
                            this._IsQuestionableDeal = true;
                        }
                    }
                }
            }
        }

        public string GetDetailHTMLTable()
        {
            return this.GetHTMLTable(this._ReportDetail, false);
        }

        public DataTable GetDetailTable()
        {
            return this._ReportDetail;
        }

        private string GetHTMLTable(DataTable dt, bool isSummary)
        {
            if (!((this._outputOption != OutputOption.Questionable) || this._IsQuestionableDeal))
            {
                return "";
            }
            string str = "";
            string str4 = str;
            str = str4 + "<span>基金" + this._FundANo + " 和 基金" + this._FundBNo + " (时间窗：" + this._TimeWindowText + ") </span>";
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                if (this._outputOption == OutputOption.All)
                {
                    return (str + "&nbsp;(无相关交易)<br/>");
                }
                return "";
            }
            str = str + "<hr /><Table width=\"100%\">" + "<tr>";
            foreach (DataColumn column in dt.Columns)
            {
                str = str + "<td>" + column.ColumnName + "</td>";
            }
            str = str + "</tr>";
            foreach (DataRow row in dt.Rows)
            {
                double premium = 0.0;
                double contribution = 0.0;
                if (row.Table.Columns.Contains("溢价率"))
                {
                    premium = Convert.ToDouble(row["溢价率"]);
                }
                if (row.Table.Columns.Contains("贡献率"))
                {
                    contribution = Convert.ToDouble(row["贡献率"]);
                }
                if (((this._outputOption != OutputOption.Questionable) || !isSummary) || this.IsQuestionable(premium, contribution))
                {
                    str = str + "<tr>";
                    foreach (DataColumn column in dt.Columns)
                    {
                        string str2 = "";
                        switch (column.ColumnName)
                        {
                            case "窗口期末":
                            case "发生日期":
                                if (row[column.ColumnName] == DBNull.Value)
                                {
                                    break;
                                }
                                str2 = Convert.ToDateTime(row[column.ColumnName]).ToString("yyyy-MM-dd");
                                goto Label_0479;

                            case "成交均价":
                                str2 = Convert.ToDouble(row[column.ColumnName]).ToString("N");
                                goto Label_0479;

                            case "溢价率":
                                str2 = premium.ToString("P");
                                if (Math.Abs(premium) > this._premiumThreshhold)
                                {
                                    str2 = "<span style=\"background:#FF0000\">" + str2 + "</span>";
                                }
                                goto Label_0479;

                            case "贡献率":
                                str2 = contribution.ToString("P");
                                if (Math.Abs(contribution) > this._contributionThreshhold)
                                {
                                    str2 = "<span style=\"background:#FF0000\">" + str2 + "</span>";
                                }
                                goto Label_0479;

                            case "成交金额":
                            case "利益输送":
                            {
                                double num4 = Convert.ToDouble(row[column.ColumnName]) / 10000.0;
                                str2 = num4.ToString("N") + " 万";
                                goto Label_0479;
                            }
                            case "成交数量":
                                str2 = ((Convert.ToDouble(row[column.ColumnName]) / 10000.0)).ToString("N") + " 万";
                                goto Label_0479;

                            default:
                                str2 = row[column.ColumnName].ToString();
                                goto Label_0479;
                        }
                        str2 = "";
                    Label_0479:
                        str = str + "<td>" + str2 + "</td>";
                    }
                    str = str + "</tr>";
                }
            }
            return (str + "</Table><br/>");
        }

        private string GetReverseAction(string action)
        {
            switch (action)
            {
                case "债券买入":
                    return "债券卖出";

                case "买入":
                    return "卖出";

                case "债券卖出":
                    return "债券买入";

                case "卖出":
                    return "买入";
            }
            return "";
        }

        public string GetSummaryHTMLTable()
        {
            return this.GetHTMLTable(this._ReportSummary, true);
        }

        public DataTable GetSummaryTable()
        {
            return this._ReportSummary;
        }

        private void GetWindowDate(DateTime tradeDate, ref DateTime windowStartDate, ref DateTime windowEndDate)
        {
            switch (this._TimeWindow)
            {
                case TimeWindow.In1TradingDay:
                    windowStartDate = tradeDate;
                    windowEndDate = tradeDate;
                    break;

                case TimeWindow.In3TradingDays:
                    switch (tradeDate.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            windowStartDate = tradeDate.AddDays(-4.0);
                            windowEndDate = tradeDate.AddDays(2.0);
                            goto Label_0207;

                        case DayOfWeek.Tuesday:
                            windowStartDate = tradeDate.AddDays(-4.0);
                            windowEndDate = tradeDate.AddDays(2.0);
                            goto Label_0207;

                        case DayOfWeek.Wednesday:
                            windowStartDate = tradeDate.AddDays(-2.0);
                            windowEndDate = tradeDate.AddDays(2.0);
                            goto Label_0207;

                        case DayOfWeek.Thursday:
                            windowStartDate = tradeDate.AddDays(-2.0);
                            windowEndDate = tradeDate.AddDays(4.0);
                            goto Label_0207;

                        case DayOfWeek.Friday:
                            windowStartDate = tradeDate.AddDays(-2.0);
                            windowEndDate = tradeDate.AddDays(4.0);
                            goto Label_0207;
                    }
                    break;

                case TimeWindow.In5TradingDays:
                    switch (tradeDate.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            windowStartDate = tradeDate.AddDays(-6.0);
                            windowEndDate = tradeDate.AddDays(4.0);
                            goto Label_0207;

                        case DayOfWeek.Tuesday:
                        case DayOfWeek.Wednesday:
                        case DayOfWeek.Thursday:
                            windowStartDate = tradeDate.AddDays(-6.0);
                            windowEndDate = tradeDate.AddDays(6.0);
                            goto Label_0207;

                        case DayOfWeek.Friday:
                            windowStartDate = tradeDate.AddDays(-4.0);
                            windowEndDate = tradeDate.AddDays(6.0);
                            goto Label_0207;
                    }
                    break;
            }
        Label_0207:
            windowStartDate = tradeDate;
        }

        private void initReportTable()
        {
            if (this._ReportDetail == null)
            {
                this._ReportDetail = new DataTable();
                this._ReportDetail.Columns.Add(new DataColumn("序号", Type.GetType("System.Int64")));
                this._ReportDetail.Columns.Add(new DataColumn("发生日期", Type.GetType("System.DateTime")));
                this._ReportDetail.Columns.Add(new DataColumn("基金编号", Type.GetType("System.Int64")));
                this._ReportDetail.Columns.Add(new DataColumn("组合名称", Type.GetType("System.String")));
                this._ReportDetail.Columns.Add(new DataColumn("证券代码", Type.GetType("System.String")));
                this._ReportDetail.Columns.Add(new DataColumn("证券名称", Type.GetType("System.String")));
                this._ReportDetail.Columns.Add(new DataColumn("委托方向", Type.GetType("System.String")));
                this._ReportDetail.Columns.Add(new DataColumn("成交均价", Type.GetType("System.Double")));
                this._ReportDetail.Columns.Add(new DataColumn("成交数量", Type.GetType("System.Int64")));
                this._ReportDetail.Columns.Add(new DataColumn("成交金额", Type.GetType("System.Double")));
                this._ReportDetail.Columns.Add(new DataColumn("成交次数", Type.GetType("System.Double")));
                this._ReportDetail.Columns.Add(new DataColumn("指令下达人", Type.GetType("System.String")));
                this._ReportDetail.Columns.Add(new DataColumn("交易员", Type.GetType("System.String")));
            }
            if (this._ReportSummary == null)
            {
                this._ReportSummary = new DataTable();
                this._ReportSummary.Columns.Add(new DataColumn("序号", Type.GetType("System.Int64")));
                this._ReportSummary.Columns.Add(new DataColumn("发生日期", Type.GetType("System.DateTime")));
                this._ReportSummary.Columns.Add(new DataColumn("窗口期末", Type.GetType("System.DateTime")));
                this._ReportSummary.Columns.Add(new DataColumn("证券代码", Type.GetType("System.String")));
                this._ReportSummary.Columns.Add(new DataColumn("证券名称", Type.GetType("System.String")));
                this._ReportSummary.Columns.Add(new DataColumn("委托方向", Type.GetType("System.String")));
                this._ReportSummary.Columns.Add(new DataColumn("规则", Type.GetType("System.String")));
                this._ReportSummary.Columns.Add(new DataColumn("溢价率", Type.GetType("System.Double")));
                this._ReportSummary.Columns.Add(new DataColumn("利益输送", Type.GetType("System.Double")));
            }
            if ((this._CheckActionList == null) || (this._CheckActionList.Count == 0))
            {
                this._CheckActionList.Add("买入");
                this._CheckActionList.Add("卖出");
                this._CheckActionList.Add("债券买入");
                this._CheckActionList.Add("债券卖出");
            }
        }

        private bool IsQuestionable(double premium, double contribution)
        {
            return ((Math.Abs(premium) > this._premiumThreshhold) || (Math.Abs(contribution) > this._contributionThreshhold));
        }

        private void WriteDetailTable(DateTime startDate, DateTime endDate, string code, string action, int id, bool isReverseAction)
        {
            string reverseAction;
            DataRow row2;
            if (isReverseAction)
            {
                reverseAction = this.GetReverseAction(action);
            }
            else
            {
                reverseAction = action;
            }
            DataRow[] rowArray = this._FundA.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", startDate, "' AND TDate<='", endDate, "' AND 委托方向='", action, "'" }));
            DataRow[] rowArray2 = this._FundB.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", startDate, "' AND TDate<='", endDate, "' AND 委托方向='", reverseAction, "'" }));
            if (((rowArray.Length == 0) || (rowArray2.Length == 0)) && isReverseAction)
            {
                rowArray = this._FundA.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", startDate, "' AND TDate<='", endDate, "' AND 委托方向='", reverseAction, "'" }));
                rowArray2 = this._FundB.Select(string.Concat(new object[] { "证券代码='", code, "' AND TDate>='", startDate, "' AND TDate<='", endDate, "' AND 委托方向='", action, "'" }));
            }
            foreach (DataRow row in rowArray)
            {
                row2 = this._ReportDetail.NewRow();
                row2["序号"] = id;
                foreach (DataColumn column in row2.Table.Columns)
                {
                    if (row.Table.Columns.Contains(column.ColumnName))
                    {
                        row2[column.ColumnName] = row[column.ColumnName];
                    }
                }
                this._ReportDetail.Rows.Add(row2);
            }
            foreach (DataRow row in rowArray2)
            {
                row2 = this._ReportDetail.NewRow();
                row2["序号"] = id;
                foreach (DataColumn column in row2.Table.Columns)
                {
                    if (row.Table.Columns.Contains(column.ColumnName))
                    {
                        row2[column.ColumnName] = row[column.ColumnName];
                    }
                }
                this._ReportDetail.Rows.Add(row2);
            }
        }

        private void WriteSummaryTable(DateTime startDate, DateTime endDate, string code, string name, double premium, double premiumAmount, double contribution, string rule, string action, int id)
        {
            DataRow row = this._ReportSummary.NewRow();
            row["序号"] = id;
            row["发生日期"] = startDate;
            row["窗口期末"] = endDate;
            row["证券代码"] = code;
            row["证券名称"] = name;
            row["委托方向"] = action;
            row["规则"] = rule;
            row["溢价率"] = premium;
            row["利益输送"] = premiumAmount;
            this._ReportSummary.Rows.Add(row);
        }

        public double ContributionThreshhold
        {
            get
            {
                return this._contributionThreshhold;
            }
            set
            {
                this._contributionThreshhold = value;
            }
        }

        public double PremiumThreshhold
        {
            get
            {
                return this._premiumThreshhold;
            }
            set
            {
                this._premiumThreshhold = value;
            }
        }

        public OutputOption ReportOutputOption
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

        public enum OutputOption
        {
            All,
            CrossTrades,
            Questionable
        }

        public enum TimeWindow
        {
            In1TradingDay,
            In3TradingDays,
            In5TradingDays
        }
    }
}

