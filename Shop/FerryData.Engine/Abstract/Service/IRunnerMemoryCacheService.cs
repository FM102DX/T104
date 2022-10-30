using T104.Store.Engine.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Engine.Abstract.Service
{
    public interface IRunnerMemoryCacheService
    {
        void SetResult(WorkflowExecuteResultDto resultDto);
        WorkflowExecuteResultDto GetResult();
    }
}
