namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Static functions to make basic conversions
    /// </summary>
    public class DataConverter
    {
        /// <summary>
        /// Converts string to decimal is string reprsents a valid decimal value
        /// </summary>
        /// <param name="str">string of a decimal</param>
        /// <returns>decimal value that was converted</returns>
        public static decimal Decimal(string str)
        {
            decimal value = 0.00m;
            decimal.TryParse(str, out value);
            return value;
        }

        public static decimal? NullableDecimal(string str)
        {
            decimal? result = null;
            if (!string.IsNullOrEmpty(str))
            {
                result = Decimal(str);
            }
            
            return result;
        }

        public static DateTime? Date(string input)
        {
            if (input == string.Empty)
            {
                return null;
            }
            else
            {
                return System.Convert.ToDateTime(input);
            }
        }

        public static string String(string s)
        {
            return s != null ? s : string.Empty;
        }
    }
}
