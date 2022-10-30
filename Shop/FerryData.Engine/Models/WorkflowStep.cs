using T104.Store.Engine.Abstract;
using System;

namespace T104.Store.Engine.Models
{
    [BsonCollection("Steps")]
    public class WorkflowStep : BaseEntity, IWorkflowStep
    {
        public bool Finished { get; set; }
        public object Data { get; set; }
        public IWorkflowStepSettings Settings { get; set; }

        public WorkflowStep() 
            : base()
        {

        }
    }
}
