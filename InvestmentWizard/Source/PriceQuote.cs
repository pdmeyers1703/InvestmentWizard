namespace InvestmentWizard
{ 
    public class PriceQuote
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal LastPrice { get; set; }

        public decimal PreviousClose { get; set; }

        public string PriceChangeAbsolute { get; set; }

        public string PriceChangePercent { get; set; }
    }
}
