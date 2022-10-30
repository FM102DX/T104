using T104.Store.Engine.Abstract;
//using T104.Store.Engine.JsonConverters;

namespace T104.Store.Engine.Models
{
    [BsonCollection("ActionStepSettings")]
    public class WorkflowActionStepSettings : WorkflowStepSettingsBase
    {
        public IWorkflowStepAction Action { get; set; }

        public WorkflowActionStepSettings()
        {
            Kind = Enums.WorkflowStepKinds.Action;
        }
    }
}
