namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransactionHistory : ITransaction
    {
        public int RowID { get; set; }

        public DateTime? PurchasedDate { get; set; }
        
        public string EquitySymbol { get; set; }
       
        public double Quanity { get; set; }

        public decimal PurchasePrice
        {
            get
            {
                try
                {
                    if (this.Quanity == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return Math.Round((decimal)((double)this.Cost / this.Quanity), 2);
                    }
                }
                catch (DivideByZeroException ex)
                {
                    throw ex;
                }
            }
        }
        
        public decimal Cost { get; set; }
       
        public DateTime? SaleDate { get; set; }
        
        public decimal? SalePrice 
        {
            get
            {
                try
                {
                    if (this.SaleProceeds == null || this.Quanity == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return Math.Round((decimal)((double)this.SaleProceeds / this.Quanity), 2);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public decimal? SaleProceeds { get; set; }
        
        public decimal? Dividends { get; set; }
    }
}
