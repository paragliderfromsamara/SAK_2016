using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NormaMeasure.SAC_APP
{
    class ServiceFunctions
    {
        /// <summary>
        /// Проверяет ввод для полей типа double
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool DoubleCharChecker(char key) //проверка на ввод числа типа Double
        {
            return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Separator) || key == Convert.ToChar(Keys.Delete) || key == Convert.ToChar(Keys.Back));

            //return false;
        }

        /// <summary>
        /// Проверяет ввод для полей типа integer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IntCharChecker(char key) //проверка на ввод числа типа Integer
        {
            return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Back));

            //return false;
        }

        /// <summary>
        /// Проверяет введено ли в поле значение типа double
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static string setDefaultForDb(string s)
        {
            return (String.IsNullOrWhiteSpace(s)) ? "DEFAULT" : s;
        }
        public static string BinaryToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// Пытается конвертировать в десятичное число. Если получается возвращает число, если нет то 0
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal convertToDecimal(object text)
        {
            try
            {
                string txt = text.ToString();
                return Convert.ToDecimal(txt);
            }
            catch (FormatException)
            {
                return 0;
            }
        }
        /// <summary>
        /// Пытается конвертировать в целое число. Если получается возвращает число, если нет то 0
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static uint convertToUInt(object text)
        {
            try
            {
                string txt = text.ToString();
                return Convert.ToUInt32(txt);
            }
            catch (FormatException)
            {
                return 0;
            }
        }
    }
}
