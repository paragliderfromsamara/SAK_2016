using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NormaLib.DBControl
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

        public static float convertToFloat(object text)
        {
            try
            {
                string txt = text.ToString();
                return float.Parse(txt);
            }
            catch (FormatException)
            {
                return 0;
            }
        }


        public static object[] PushToArr(object[] arr, object val)
        {
            object[] newArr = new object[arr.Length + 1];
            for (int i = 0; i < arr.Length - 1; i++) newArr[i] = arr[i];
            newArr[newArr.Length - 1] = val;
            return newArr;
        }
        public static int convertToInt16(object text)
        {
            try
            {
                string t = text.ToString();
                return Convert.ToInt16(t);
            }
            catch (FormatException) { return 0; }
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
        public static string MyDate(DateTime date)
        {
            string t = "{0}/{1}/{2}";
            string day = fillZero(date.Day.ToString());
            string month = fillZero(date.Month.ToString());
            string year = date.Year.ToString();
            return String.Format(t, day, month, year);
        }
        public static string MyDateTime(DateTime date)
        {
            string t = "{0}/{1}/{2} в {3}:{4}";
            string day = fillZero(date.Day.ToString());
            string month = fillZero(date.Month.ToString());
            string year = date.Year.ToString();
            string hour = fillZero(date.Hour.ToString());
            string minute = fillZero(date.Minute.ToString());
            return String.Format(t, day, month, year, hour, minute);
        }

        private static string fillZero(string s)
        {
            return s.Length == 1 ? "0" + s : s;
        }
    }
}
