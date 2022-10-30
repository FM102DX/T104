using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service
{
    public class CommonOperationResult
    {
        //результат, возвращаемый после операций в объектном слое
        public bool success;
        public string msg;
        public object returningValue;

        public static CommonOperationResult getInstance(bool _success, string _msg, object _returningValue = null)
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

        public string ShrotString() => $"Success: {success} message: {msg}";
    }
}
