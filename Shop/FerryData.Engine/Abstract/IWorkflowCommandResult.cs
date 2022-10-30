using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Engine.Abstract
{
    public interface IWorkflowCommandResult
    {
        int Status { get; set; }
        string Message { get; set; }
        object Data { get; set; }

    }
}
