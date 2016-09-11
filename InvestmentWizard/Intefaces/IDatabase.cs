namespace InvestmentWizard
{
    using System.Data;

    /// <summary>
    /// Interface to database services.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Sets the name of the database to query
        /// </summary>
        string DatabasePath { get; set; }

        /// <summary>
        /// Returns a entire database table
        /// </summary>
        /// <param name="tableName">name to data table</param>
        /// <returns>DataTable</returns>
        DataTable GetTableData(string tableName);

        /// <summary>
        /// Queries a database table to match the colunn critera for 1 
        /// row.  If more than 1 row exists this method throws.
        /// </summary>
        /// <param name="tableName">Name of database table</param>
        /// <param name="col">Column name</param>
        /// <param name="criteria">Value in column to match</param>
        /// <returns>DataRow - a single row</returns>
        DataRow GetRow(string tableName, string col, dynamic criteria);

        /// <summary>
        /// Queries a database table to match the colunn critera for all 
        /// matching rows.
        /// </summary>
        /// <param name="tableName">Name of database table</param>
        /// <param name="col">Column name</param>
        /// <param name="criteria">Value in column to match</param>
        /// <returns>DataTable with 1 or rows</returns>
        DataTable GetRows(string tableName, string col, dynamic criteria);

        /// <summary>
        /// Inserts a new row in a table with values in the provided columns
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="columns">An array of column names</param>
        /// <param name="values">An array of values that correspond to the column array</param>
        void AddRecord(string tableName, string[] columns, dynamic[] values);

        /// <summary>
        /// Updates mulitple records in a single row
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="row">Row to update</param>
        /// <param name="columns">columns to be updated</param>
        /// <param name="values">values for updating records</param>
        void UpdateRecord(string tableName, int row, string[] columns, dynamic[] values);       
    }
}
