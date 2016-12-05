// <copyright file="IFinanceData.cs" company="Meyervinski">
// Copyright (c) Meyervinski, Inc. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;

    /// <summary> 
    /// Interface to public financial API's. 
    /// </summary> 
    public interface IFinancialData
    {
        /// <summary> 
        /// Get last price quote for list of ticker symbols 
        /// </summary>
        /// <param name="tickerSymbols">An list of strings containing ticker
        /// symbols to query.</param>
        /// <param name="prices">An ref to a list of Prices to be passed back
        /// based the results of the query.</param>
        /// <returns><see cref="string.CompareTo(string)"/></returns>
        bool GetPrices(List<string> tickerSymbols, out List<PriceQuote> prices);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickerSymbols"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        bool GetHistoricalPrice(string tickerSymbols, DateTime date, out string price);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickerSyymbols"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        bool GetDividendsOverTimeSpan(string tickerSyymbols, DateTime begin, DateTime end, ref List<decimal> dividends);
    }
}
