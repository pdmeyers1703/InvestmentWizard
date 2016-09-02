namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
