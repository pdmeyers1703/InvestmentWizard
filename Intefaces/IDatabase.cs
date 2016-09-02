namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDatabase
    {
        DataTable GetTableData(string tableName);

        DataRow GetRow(string tableName, string col, dynamic criteria);

        DataTable GetRows(string tableName, string col, dynamic criteria);

        void AddRecord(string tableName, string[] columns, dynamic[] values);

        void UpdateRecord(string tableName, int row, string[] columns, dynamic[] values);       
    }
}
