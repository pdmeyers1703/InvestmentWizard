namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class YahooFinancalDataClient : IFinancialData
    {
        public bool GetPrices(List<string> tickerSymbols, out List<PriceQuote> prices)
        {
            string url = "http://finance.yahoo.com/d/quotes.csv?s=";
            string csv = string.Empty;

            foreach (string s in tickerSymbols)
            {
                url += s.ToString() + "+";
            }

            url = url.TrimEnd(new char[] { '+' });
            url += "&f=snl1pc1p2";

            try
            {
                csv = this.GetCSV(url);
            }
            catch
            {
                prices = new List<PriceQuote>();
                return false;
            }

            prices = this.ParseCSV(csv);

            if (prices.Any(p => p.Name == "N/A"))
            {
                return false;
            }

            return true;
        }

        public bool GetHistoricalPrice(string tickerSymbols, DateTime date, out string price)
        {
            string url = "http://ichart.finance.yahoo.com/table.csv?s=";
            string csv = string.Empty;

            url += tickerSymbols;
            url += "&a=" + (date.Month - 1).ToString();
            url += "&b=" + date.Day.ToString();
            url += "&c=" + date.Year.ToString();
            url += "&d=" + (date.Month - 1).ToString();
            url += "&e=" + date.Day.ToString();
            url += "&f=" + date.Year.ToString();
            url += "&g=d" + "&ignore=.csv";

            try
            {
                csv = this.GetCSV(url);
            }
            catch (WebException)
            {
				price = null;
                return false;
            }

            price = this.GetHistoryCSVPrice(csv);

            return true;
        }

        public bool GetDividendsOverTimeSpan(string tickerSyymbols, DateTime begin, DateTime end, ref List<decimal> dividends)
        {
            string url = "http://ichart.finance.yahoo.com/table.csv?s=";
            string csv = string.Empty;

            url += tickerSyymbols;
            url += "&a=" + (begin.Month - 1).ToString();
            url += "&b=" + begin.Day.ToString();
            url += "&c=" + begin.Year.ToString();
            url += "&d=" + (end.Month - 1).ToString();
            url += "&e=" + end.Day.ToString();
            url += "&f=" + end.Year.ToString();
            url += "&g=v";
            url += "&ignore=.csv";

            try
            {
                csv = this.GetCSV(url);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Status.ToString());
                return false;
            }

            dividends = this.GetCumulativeHistoryCSVDividends(csv);

            return true;
        }

        private string GetCSV(string url)
        {
            using (WebClient web = new WebClient())
            {
                return web.DownloadString(url);
            }
        }

        // Parse the detail quote for multipe stocks
        private List<PriceQuote> ParseCSV(string csv)
        {
            List<PriceQuote> prices = new List<PriceQuote>();
            string[] rows = csv.Replace("\r", string.Empty).Split('\n');

            foreach (string row in rows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }

                // Splits at comma's that are not in double quotes!
                string[] cols = Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                PriceQuote p = new PriceQuote();
                p.Symbol = cols[0].TrimStart('\"').TrimEnd('\"');
                p.Name = cols[1].TrimStart('\"').TrimEnd('\"');
                p.LastPrice = DataConverter.Decimal(cols[2]);
                p.PreviousClose = DataConverter.Decimal(cols[3]);
                p.PriceChangeAbsolute = cols[4].TrimStart('\"').TrimEnd('\"');
                p.PriceChangePercent = cols[5].TrimStart('\"').TrimEnd('\"');

                if (p.Name != "NA")
                {
                    prices.Add(p);
                }
            }

            return prices;
        }

        private string GetHistoryCSVPrice(string csv)
        {
            string[] rows = csv.Replace("\r", string.Empty).Split('\n');

            string[] cols = rows[1].Split(',');

            return cols.Length == 6 ? cols[6] : string.Empty;
        }

        private List<decimal> GetCumulativeHistoryCSVDividends(string csv)
        {
            List<decimal> dividends = new List<decimal>();
            string[] rows = csv.Replace("\r", string.Empty).Split('\n');

            foreach (var r in rows.AsEnumerable().Skip(1))
            {
                if (r != string.Empty)
                {
                    string[] cols = r.Split(',');
                    dividends.Add(Convert.ToDecimal(cols[1]));
                }
            }

            return dividends;
        }
    }
}   
