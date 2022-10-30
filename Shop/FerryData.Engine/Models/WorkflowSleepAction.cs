using T104.Store.Engine.Abstract;
using T104.Store.Engine.Enums;
using System;

namespace T104.Store.Engine.Models
{
    [BsonCollection("WorkflowSleepAction")]
    public class WorkflowSleepAction : BaseEntity, IWorkflowStepAction
    {
        public int DelayMilliseconds { get; set; }
        public WorkflowActionKinds Kind { get; } = WorkflowActionKinds.Sleep;

        public WorkflowSleepAction() 
            : base()
        {

        }
    }
}
