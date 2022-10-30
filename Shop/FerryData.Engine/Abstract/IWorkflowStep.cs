using System;

namespace T104.Store.Engine.Abstract
{
    public interface IWorkflowStep
    {
        Guid Uid { get; set; }
        IWorkflowStepSettings Settings { get; set; }
        object Data { get; set; }
        bool Finished { get; set; }

    }
}
