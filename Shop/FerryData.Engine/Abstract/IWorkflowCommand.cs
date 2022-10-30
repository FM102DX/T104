using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Engine.Abstract
{
    public interface IWorkflowCommand
    {
        Task<IWorkflowCommandResult> ExecuteAsync();
    }
}
