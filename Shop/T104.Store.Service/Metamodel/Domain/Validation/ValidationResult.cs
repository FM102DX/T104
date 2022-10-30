using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public class ValidationResult
    {
        //результат, возвращаемый после операций в объектном слое
        public bool success;
        public string msg;
        public object returningValue;

        public static ValidationResult getInstance(bool _success, string _msg, object _returningValue = null)
        {
            ValidationResult c = new ValidationResult();
            c.success = _success;
            c.msg = _msg;
            c.returningValue = _returningValue;
            return c;
        }

        public static ValidationResult sayOk(string _msg = "") { return getInstance(true, _msg, null); }

        public static ValidationResult sayFail(string _msg = "") { return getInstance(false, _msg, null); }

        public string ShrotString() => $"ValidationResult: success={success} message={msg}";
    }
}
