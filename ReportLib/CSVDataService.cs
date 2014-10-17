using System;
using System.Data;
using System.IO;
using System.Text;

namespace ReportLib
{   
    public class CSVDataService
    {
        private string _ColumnDelima = ",";
        private static CSVDataService _Instance;

        public static CSVDataService GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new CSVDataService();
            }
            return _Instance;
        }

        public DataTable GetTradingData(string fileName)
        {
            StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string str = "";
            bool flag = true;
            DataTable table = new DataTable();
            while ((str = reader.ReadLine()) != null)
            {
                int num;
                if (str.Trim().Length == 0)
                {
                    continue;
                }
                string[] strArray = str.Split(this._ColumnDelima.ToCharArray());
                if (flag)
                {
                    flag = false;
                    num = 0;
                    while (num < strArray.Length)
                    {
                        DataColumn column = new DataColumn(strArray[num].Trim(), Type.GetType("System.String"));
                        table.Columns.Add(column);
                        num++;
                    }
                }
                else
                {
                    DataRow row = table.NewRow();
                    for (num = 0; num < strArray.Length; num++)
                    {
                        if (num >= table.Columns.Count)
                        {
                            break;
                        }
                        row[num] = strArray[num].ToString();
                    }
                    table.Rows.Add(row);
                }
            }
            reader.Close();
            return table;
        }

        public string ColumnDelima
        {
            get
            {
                return this._ColumnDelima;
            }
            set
            {
                this._ColumnDelima = value;
            }
        }
    }
}

