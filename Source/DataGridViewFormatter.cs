namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public static class DataGridViewFormatter
    {
        public enum CellFormatType
        {
            NONE = 0,
            Currency,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="headerText"></param>
        /// <param name="width"></param>
        public static void FormatColumnHeader(DataGridViewColumn col, string headerText, int width)
        {
            col.HeaderText = headerText;
            col.Width = width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="formatType"></param>
        public static void FormatColumnCells(DataGridViewColumn col, CellFormatType formatType)
        {
            if (formatType == CellFormatType.Currency)
            {
                col.DefaultCellStyle.Format = "c";
            }            
        }

        public static void SetCellColor(DataGridViewCell cell, Color color)
        {
            cell.Style.ForeColor = color;
        }
    }
}
