using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaLib.UI
{
    public static class NormaUIDataGridStyles
    {
        public static System.Windows.Forms.DataGridViewCellStyle BuildElementCellStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle parameterNameCellStyle = new DataGridViewCellStyle();
            parameterNameCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            parameterNameCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            parameterNameCellStyle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parameterNameCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            parameterNameCellStyle.NullValue = "-";
            parameterNameCellStyle.Padding = new System.Windows.Forms.Padding(3);
            parameterNameCellStyle.SelectionBackColor = parameterNameCellStyle.BackColor;
            parameterNameCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            parameterNameCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            return parameterNameCellStyle;
        }

        public static System.Windows.Forms.DataGridViewCellStyle BuildElementsHeaderStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            style.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            style.ForeColor = System.Drawing.Color.Gainsboro;
            style.NullValue = "-";
            style.Padding = new System.Windows.Forms.Padding(3);

            style.SelectionBackColor = style.BackColor;
            style.SelectionForeColor = System.Drawing.Color.Gainsboro;
            style.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            return style;
        }
        public static System.Windows.Forms.DataGridViewCellStyle BuildResultStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.White;
            style.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            style.ForeColor = System.Drawing.Color.DarkBlue;
            style.NullValue = "-";
            style.Padding = new System.Windows.Forms.Padding(3);

            style.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            style.SelectionForeColor = System.Drawing.Color.Gainsboro;
            style.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            return style;
        }
    }
}
