using T104.Store.Engine.Abstract;

namespace T104.Store.Engine.Runner
{
    public class WorkflowStepExecuteResult: IWorkflowCommandResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
}