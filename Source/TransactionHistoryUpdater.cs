namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public enum TRANSACTION_HISTORY
    {
        PURCHASED_DATE,
        EQUITY,
        QUANTITY,
        PURCHASE_PRICE,
        COST_BASIS,
        SOLD_DATE,
        SALE_PRICE,
        SALE_PROCEEDS,
        DIVIDENDS,
    }

    public class TransactionHistoryUpdater  
    {
        private const string DataTableName = "Transactions";
        private readonly string[] columnNames = 
        { 
            "Purchased Date", 
            "Equity",
            "Quantity",
            "Price",
            "Cost Basis",
            "Sold Date",
            "Sales Price",
            "Market Value",
            "Dividends" 
        };

        private DataTable dt;
        private IDatabase db;

        // Constructor
        public TransactionHistoryUpdater(IDatabase database)
        {
            this.dt = new DataTable();
            this.db = database;
        }

        public DataTable Update()
        {
            try
            {
                // Query database for transactions table
                this.dt = this.db.GetTableData(DataTableName);
            }
            catch
            {
                throw;
            }

            // Add new columns
            this.RemoveColumn("ID");
            this.AddColumnAt("Price", 3);

            this.DeterminePurchasePriceColumn();

            return this.dt;
        }

        public List<PriceQuote> GetOpenPositions()
        {
            var open = from r in this.dt.AsEnumerable()
                       where r["Date Sold"].ToString() == string.Empty
                       select r;
                      
            List<PriceQuote> list = new List<PriceQuote>();

            foreach (var row in open.AsEnumerable())
            {
                PriceQuote p = new PriceQuote();

                p.Symbol = row["Stock"].ToString();

               // p.Quantity = Convert.ToUInt16(row["Quantity"]);
               // p.Cost = Convert.ToDecimal(row["Cost"]);
                list.Add(p);
            }
            
            return new List<PriceQuote>();
        }

        private void RemoveColumn(string colName)
        {
            if (this.dt.Columns.Contains(colName))
            {
                this.dt.Columns.Remove(colName);
            }
        }

        private void AddColumnAt(string colName, int index)
        {
            if (!this.dt.Columns.Contains(colName))
            {
                this.dt.Columns[this.dt.Columns.Add(colName, typeof(string)).ColumnName].SetOrdinal(index);
            }
        }

        private void DeterminePurchasePriceColumn()
        {
            foreach (DataRow row in this.dt.AsEnumerable())
            {
                if (this.dt.Columns.Contains(this.GetColumnName(TRANSACTION_HISTORY.COST_BASIS)) &&
                    this.dt.Columns.Contains(this.GetColumnName(TRANSACTION_HISTORY.QUANTITY)))
                {
                    int costIndex = this.dt.Columns.IndexOf(this.GetColumnName(TRANSACTION_HISTORY.COST_BASIS));
                    int quantityIndex = this.dt.Columns.IndexOf(this.GetColumnName(TRANSACTION_HISTORY.QUANTITY));
                    if (Convert.ToDouble(row[quantityIndex]) == 0)
                    {
                        row[this.GetColumnName(TRANSACTION_HISTORY.PURCHASE_PRICE)] = "N/A";
                    }
                    else
                    {
                        double value = Convert.ToDouble(row[costIndex]) / Convert.ToDouble(row[quantityIndex]);
                        row[this.GetColumnName(TRANSACTION_HISTORY.PURCHASE_PRICE)] = Math.Round(value, 2).ToString("#.00");
                    }
                }
            }
        }

        private string GetColumnName(TRANSACTION_HISTORY val)
        {
            return this.columnNames[Convert.ToInt32(val)];
        }
    }
}
