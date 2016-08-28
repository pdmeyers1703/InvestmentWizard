namespace PetersInvestmentProgram
{
    using System;

    public interface ITransaction
    {
        int RowID { get; set; }

        DateTime? PurchasedDate { get; set; }

        string EquitySymbol { get; set; }

        double Quanity { get; set; }

        decimal PurchasePrice { get; }
        
        decimal Cost { get; set; }

        DateTime? SaleDate { get; set; }

        decimal? SalePrice { get; }
       
        decimal? SaleProceeds { get; set; }
         
        decimal? Dividends { get; set; }
    }
}
