using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T109.ActiveDive.Service
{
    public class CommonOperationResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public object ReturningValue { get; set; }

        public static CommonOperationResult getInstance(bool success, string msg, object returningValue = null)
        {
            CommonOperationResult c = new CommonOperationResult();
            c.Success = success;
            c.Msg = msg;
            c.ReturningValue = returningValue;
            return c;
        }

        public static CommonOperationResult SayFail(string msg = "") { return getInstance(false, msg, null); }
        public static CommonOperationResult SayOk(string msg = "") { return getInstance(true, msg, null); }

    }
}
