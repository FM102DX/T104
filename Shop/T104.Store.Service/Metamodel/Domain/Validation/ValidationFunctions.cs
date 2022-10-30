using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using Ricompany.CommonFunctions;

namespace T104.Store.Service.Metamodel
{
    public static class ValudationFunctions
    {
        public static double StringToDouble(string value)
        {
            //посимвольный валидатор числа double
            //в поле можно вводить числа и сепаратор
            bool itsDigit;
            bool itsSeparator;
            string rez = "";
            double rezDouble = 0;

            string source = Convert.ToString(value);
            string separator = ",";
            for (int i = 0; i < source.Length; i++)
            {
                itsDigit = "0123456789".Contains(source[i]);
                itsSeparator = (source[i].ToString() == separator);
                if (itsDigit || itsSeparator) { rez += source[i]; }
            }

            double.TryParse(rez, out rezDouble);

            return rezDouble;
        }

       public static int StringToInt(string value)
        {
            //посимвольный валидатор числа double
            //в поле можно вводить числа и сепаратор
            bool itsDigit;
            string rez = "";
            int rezInt=0;
            string source = Convert.ToString(value);

            for (int i = 0; i < source.Length; i++)
            {
                itsDigit = "0123456789".Contains(source[i]);
                if (itsDigit) { rez += source[i]; }
            }

            int.TryParse(rez,out rezInt);
            return rezInt;
        }

        public static string CommonNamingString (string value)
        {
            //оставляет в строке только буквы, " @ # № ? * + - _ и пробелы

            Regex regex;
            string source = Convert.ToString(value);
            source = Fn.strRemoveArrSymbols(source, @"\|/^");
            regex = new Regex("[^a-zA-Zа-яА-Я0-9() +-_:;!?@#.*]", RegexOptions.IgnoreCase);
            source = regex.Replace(source, "");
            return source;
        }


        public static string StringToSnakeStyleAlias (string source)
        {
            //оставляет в строке только буквы, " @ # № ? * + - _ и пробелы
            Regex regex;
            source = Fn.strRemoveArrSymbols(source, @"\|/^");
            regex = new Regex("[^a-zA-Z0-9_*]", RegexOptions.IgnoreCase);
            source = regex.Replace(source, "");
            return source;
        }


        public static string StringToStrictNamingString(string source)
        {
            //оставляет в строке только буквы и пробелы
            Regex regex;
            source = Fn.strRemoveArrSymbols(source, @"\|/^");
            regex = new Regex("[^a-zA-Zа-яА-Я0-9 ]", RegexOptions.IgnoreCase);
            source = regex.Replace(source, "");
            return source;

        }

    }
 


}