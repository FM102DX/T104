using System;
using System.Collections.Generic;

namespace T104.Store.Engine.Abstract
{
    public interface IWorkflow
    {
        Guid Uid { get; set; }

        IWorkflowSettings Settings { get; set; }

        bool Finished { get; set; }

        IEnumerable<IWorkflowStep> Steps { get; }
    }
}
