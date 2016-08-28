namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateTimeHelper
    {
        /// <summary>
        /// Gets the last day of the previous year
        /// </summary>
        /// <returns>Date of last day last year</returns>
        public static DateTime GetYTD()
        {
            return new DateTime(DateTime.Now.Year - 1, 12, 31);
        }
    }
}
