namespace InvestmentWizard
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public class AccessDB : IDatabase
    {
        private const string LocalPathAndName = "..\\..\\DataBases\\DataStore.accdb";
        private static readonly string DbLocation = Path.GetFullPath(LocalPathAndName);
        private static readonly string ConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DbLocation;

        public AccessDB()
        {
        }

        public DataTable GetTableData(string tableName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionStr))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM " + tableName;
                    using (OleDbDataAdapter adpater = new OleDbDataAdapter(cmd))
                    {
                        adpater.Fill(dt);
                    }
                }

                return dt;
            }
            catch (OleDbException)
            {
                throw;
            }
        }

        /// <summary>
        /// GetRow() - Queries a database table to match the colunn critera for 1 
        /// row.  If more than 1 row exists this method throws.
        /// </summary>
        /// <param name="tableName">Name of database table</param>
        /// <param name="col">Column name</param>
        /// <param name="criteria">Value in column to match</param>
        /// <returns>DataRow - a single row</returns>
        public DataRow GetRow(string tableName, string col, dynamic criteria)
        {
            DataTable dt = this.GetRows(tableName, col, criteria);

            if (dt.Rows.Count != 1)
            {
                throw new Exception();
            }
 
            return dt.Rows[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="col"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataTable GetRows(string tableName, string col, dynamic criteria)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionStr))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM " + tableName + " WHERE [" + col + "]=" + criteria.ToString();
                    using (OleDbDataAdapter adpater = new OleDbDataAdapter(cmd))
                    {
                        adpater.Fill(dt);
                    }
                }

                return dt;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Inserts a new row in a table with values in the provided columns
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="columns">An array of column names</param>
        /// <param name="values">An array of values that correspond to the column array</param>
        public void AddRecord(string tableName, string[] columns, dynamic[] values)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionStr))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO " + "[" + tableName + "] (";

                    foreach (var c in columns)
                    {
                        cmd.CommandText += "[" + c + "],";
                    }

                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.CommandText += ") VALUES (";

                    foreach (var v in values)
                    {
                        cmd.CommandText += "'" + v + "',";
                    }

                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.CommandText += ")";

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Updates mulitple records in a single row
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="row">Row to update</param>
        /// <param name="columns">columns to be updated</param>
        /// <param name="values">values for updating records</param>
        public void UpdateRecord(string tableName, int row, string[] columns, dynamic[] values)
        {
            if (columns.Count() != values.Count() ||
                columns.Count() == 0 || values.Count() == 0 ||
                columns == null || values == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionStr))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE " + "[" + tableName + "] SET ";

                    int i = 0;
                    foreach (var c in columns)
                    {
                        cmd.CommandText += "[" + c.ToString() + "] = '" + values[i++].ToString() + "',";
                    }

                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.CommandText += " WHERE [ID] =" + row.ToString();

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
