using T104.Store.Engine.Abstract;
using System.Collections.Generic;

namespace T104.Store.Engine.Models
{
    [BsonCollection("Settings")]
    public class WorkflowSettings : BaseEntity, IWorkflowSettings
    {
        public string Title { get; set; }

        public string Memo { get; set; }

        public List<IWorkflowStepSettings> Steps { get; set; } = new List<IWorkflowStepSettings>();

        public WorkflowSettings()
            : base()
        {

        }

        public void AddStep(IWorkflowStepSettings step)
        {
            Steps.Add(step);
        }

        public void ClearSteps()
        {
            Steps.Clear();
        }

        public void RemoveStep(IWorkflowStepSettings step)
        {
            Steps.Remove(step);
        }
    }
}
