using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SAK_2016
{
    class ServiceFunctions
    {
        public static bool DoubleCharChecker(char key) //проверка на ввод числа типа Double
        {
            return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Separator) || key == Convert.ToChar(Keys.Delete) || key == Convert.ToChar(Keys.Back));

            //return false;
        }

        public static bool IntCharChecker(char key) //проверка на ввод числа типа Integer
        {
            return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Back));

            //return false;
        }

        public static string checkDecimalValInTextBox(TextBox t)
        {
            try
            {
                return Convert.ToDecimal(t.Text).ToString();
            }
            catch (FormatException)
            {
                return "0";
            }
        }
    }
}
