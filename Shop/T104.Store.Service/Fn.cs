using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Ricompany.CommonFunctions
{

    public static class Fn
    {
        public static string getRandomWord(string[] input)
        {
            if (input.Length == 0) return "";

            Random random = new Random();
            int no = random.Next(0, input.Length+1);
            if (no > input.Length - 1) no = input.Length - 1;
            return input[no];
        }

        public static double getRandomDouble(double min, double max)
        {
            Random random = new Random();
            double no = min + random.NextDouble()*(max-min);
            return no;
        }

        public class CommonOperationResult
        {
            //результат, возвращаемый после операций в объектном слое
            public bool success;
            public string msg;
            public object returningValue;

            public static CommonOperationResult getInstance(bool _success, string _msg, object _returningValue=null)
            {
                CommonOperationResult c = new CommonOperationResult();
                c.success = _success;
                c.msg = _msg;
                c.returningValue = _returningValue;
                return c;
            }

            public static CommonOperationResult returnValue(object _returningValue = null) { return getInstance(true, "", _returningValue); }
            public static CommonOperationResult sayFail(string _msg = "") { return getInstance(false, _msg, null); }
            public static CommonOperationResult sayOk(string _msg = "") { return getInstance(true, _msg, null); }
            public static CommonOperationResult sayItsNull(string _msg = "") { return getInstance(true, _msg, null); }
        }

        public static string substrBeginsFromLtrNo(string source, int begin)
        {
           //чтобы уж точно не было ошибок 
            string s="";
            try
            {
                s = source.Substring(begin);
            }
            catch
            {
                s = "";
            }
            return s;
        }
        public class FilePathAnalyzer
        {
            string source="";

            public FilePathAnalyzer (string _source)
            {
                source = _source;
            }

            private int fileNameLength
            {
                get
                {
                    return source.Length-sepIndex-1;
                }
            }
            private int filePathLength
            {
                get
                {
                    return sepIndex;
                }
            }
            private int sepIndex
            {
                get
                {
                    
                    if (source == "") return -1;
                    
                    string s="";
                    
                    int i;

                    for (i = source.Length-1; i >= 0; i--)
                    {
                        s = source.Substring(i, 1);

                        if (s == @"\")
                        {
                            return i;
                        }
                    }
                    return -1;
                }
            }
            public string getFileName
            {
                get
                {
                    if (source == "") return "";

                    return source.Substring(sepIndex+1, fileNameLength);
                }
            }
            public string getFilePath
            {
                get
                {
                    if (source == "") return "";

                    return source.Substring(0, filePathLength) + "\\";
                }
            }

        }    

        public static bool isItSimpleNumber (object _s)
        {
            //является ли s простым номером, т.е. состоящим только из цифр 1,2,3 и т.д., без точек, запятых и прочего
            string s = Fn.toStringNullConvertion(_s);

            if (s == "") return false;
            
            bool hasNotDigits = false;

            s.ToCharArray().ToList().ForEach(x =>{if (!char.IsDigit(x)) hasNotDigits = true;});

            if (hasNotDigits) return false;

            return true;

        }
        
        
        public static string chr10 { get { return Convert.ToChar(10).ToString(); } }
        public static string chr13 { get { return Convert.ToChar(13).ToString(); } }
        public class stringByExpressionMerger
        {
            //класс, который соединяет элементы строкового массива выражением
            string expr;
            List<string> elements = new List<string>();
            public stringByExpressionMerger(string _expr)
            {
                expr = _expr;
            }

            public void addElement(string element)
            {
                if (toStringNullConvertion(element) != "") elements.Add(element);
            }

            public string result
            {
                get
                {
                    string rez = "";
                    bool eos;
                    int counter = 0;
                    if (elements.Count == 0) return "";
                    foreach (string s in elements)
                    {
                        eos = (counter == elements.Count - 1);
                        rez = rez + s + (eos ? "" : expr);
                    }
                    return rez;
                }
            }

        }


        public class ParamStringManager
        {

            string paramStr = "";


            public ParamStringManager(string _paramStr)
            {
                paramStr = _paramStr;

            }

            public string getParamValue(string paramName)
            {
                if (paramStr == "") return "";
                string[] x = paramStr.Split(';');
                string[] z;
                for (int i = 0; i < x.Length; i++)
                {
                    z = x[i].Split('=');
                    if (z[0] == paramName) return z[1];
                }
                return "";
            }



        }

        public static string getStringArrayDump(string[] arr)
        {
            return string.Join(";", arr);
        }

        public static string generateNBlockGUID(int n, int bockLength=4)
        {
            //return "AAAA-AAAA-AAAA-AAAA-...";
            string[] myArray = new string[n];
            for (int i =0; i<n; i++)
            {
                myArray[i] = randomString(bockLength).ToUpper();
            }
            return string.Join("-", myArray);
        }


        public static string generate4blockGUID()
        {
            return generateNBlockGUID(4);
        }

        public static string generateValueThatIsNotInList(List<string> lst, int length)
        {
            if (lst == null) return "";

            string newValue = "";

            string oldValuesList = Fn.list2str(lst.Cast<object>().ToList());

            do
            {
                newValue = Fn.randomString(length);
            }
            while (oldValuesList.Contains(newValue));

            return newValue;
        }
        public static string list2str(List<object> arr)
        {
            string s = "";
            if (arr == null) return "";
            if (arr.Count == 0) return "";
            foreach (object x in arr)
            {
                string comma = (arr.IndexOf(x) == (arr.Count - 1)) ? "" : ",";
                s += toStringNullConvertion(x) + comma;
            }
            return s;
        }
        public static bool listIsNullOrEmpty(System.Collections.IList list)
        {
            if (list == null)
            {
                return true;
            }
            else
            {
                if (list.Count == 0) return true; else return false;
            }
        }
        public static CommonOperationResult convertedObject(string typeStr, object value)
        {
            //возвращает object - обертку исходя из того, какой тип передан в typeStr
            
            if (value == null) return CommonOperationResult.sayItsNull();

            if (value.GetType().ToString() == typeStr) { return CommonOperationResult.returnValue(value); }

            object rez = null;
            try
            {
                switch (typeStr)
                {
                    case "System.String":
                        rez = Convert.ToString(value);
                        break;


                    case "System.Double":
                        rez = Convert.ToDouble(value);
                        break;

                    case "System.Int32":
                    case "System.Int16":
                    case "System.Int":
                        rez = Convert.ToInt32(value);
                        break;

                    case "System.Boolean":
                        rez = Convert.ToBoolean(value);
                        break;

                    case "System.DateTime":
                        rez = Convert.ToDateTime(value);
                        break;

                    default:
                        //Fn.mb_info("Fn.convertedObject обнаружила неизвестный тип: "+ typeStr);
                        rez = value;
                        break;

                }
            }
            catch
            {
                //тут надо отдельно отследить, когда конвертация неуспешна
                return CommonOperationResult.sayFail("Error converting formats in Fn.convertedObject");
                //rez = null;
            }
            return CommonOperationResult.returnValue(rez); 
        }
        //здесь вспомогательные статические функции
        public static void h(int x) { Debug.WriteLine("marker " + x.ToString()); }
        public static void h0() { h(0); }
        public static void h1() { h(1); }
        public static void h2() { h(2); }
        public static void h3() { h(3); }
        public static void h4() { h(4); }

        public static string stringListDuplicates(List<string> s)
        {
            //возвращает дублирующиеся значения, если таковые есть в s
            string rez = "";

            var query = s.GroupBy(x => x).Where(g => g.Count() > 1).Select(n => n.Key).ToList();

            for (int i = 0; i < query.Count; i++)
            {
                rez += query[i].ToString() + ((i == query.Count - 1) ? "" : ",");
            }
            return rez;
        }

        public static void dp(string s) { Debug.WriteLine(s); }

        public static void dp(int x) { Debug.WriteLine(x.ToString()); }

        public static string toStringNullConvertion(object value)
        {
            string _value;
            if (value == null)
            {
                return "";
            }
            else
            {
                try
                {
                    _value = Convert.ToString(value);
                }
                catch
                {
                    _value = "";
                }
            }
            return _value;
        }

        public static int object2int(object o)
        {
            if (o == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(o);
            }
        }

        public static string getFillLine(int cnt)
        {

            //выдает линию * cnt (используется для нужд отладки)
            string s = "";
            for (int i = 1; i <= cnt; i++) { s = s + "______ "; }
            return s;
        }

        public static string getEntityTypeFromFullTypeNameString(string s)
        {
            //приходит строка вида ****.****.***. --- ***.xxx - вот нам нужен этот последний ххх
            string[] s0 = s.Split('.');
            if (s.Length <= 0) { return ""; }
            return s0[s0.Length - 1];

        }

        public static double getRndDouble(double min, double max, int digits = 2)
        {
            double d = min + random.NextDouble() * (max - min);

            d = Math.Round(d, digits);

            return d;
        }

        public static int getRndInt(int min, int max)
        {
            return random.Next(min, max);
        }


        private static Random random = new Random();
        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string sfn(string source, string b, string e = "")
        {
            return source == "" ? "" : b + source + e;
        }

        public static string saveToDBConverter(string s)
        {
            string r;
            r = s.Replace("'", "<r7specialitem_100>");
            return r;
        }
        public static string loadFormDBConverter(string s)
        {
            string r;
            r = s.Replace("<r7specialitem_100>", "'");
            return r;
        }

        public static bool stringArrEmpty(string[] s)
        {
            //опередляет, является ли массив пустым
            //он пустой, если а) это null, б) там нет элементов
            if (s == null)
            {
                return true;
            }
            else
            {
                if (s.Length == 0)
                {
                    return true;
                }
            }
            return false;

        }

        public static string strRepeater(string ptn, int q)
        {
            //повторяет строку q раз
            if (q <= 0) return "";

            string s = "";
            for (int i = 1; i <= q; i++)
            {
                s += ptn;
            }
            return s;
        }

        public static string strRemoveArrSymbols(string source, string symbols)
        {
            int i;
            string rez = "";
            for (i = 0; i < source.Length; i++)
            {
                if (!symbols.Contains(source[i]))
                {
                    rez += source[i];
                }
            }
            return rez;
        }
    }
}