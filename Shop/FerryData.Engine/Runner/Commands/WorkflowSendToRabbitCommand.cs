using T104.Store.Engine.Abstract;
using T104.Store.Engine.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Engine.Runner.Commands
{
    public class WorkflowSendToRabbitCommand: IWorkflowCommand
    {
        private readonly WorkflowHttpAction _settings;
        private readonly Logger _logger;
        private Dictionary<string, object> _stepsData;

        public WorkflowSendToRabbitCommand(WorkflowHttpAction settings, Dictionary<string, object> stepsData, Logger logger)
        {
            _settings = settings;
            _stepsData = stepsData;
            _logger = logger;
        }

        public async Task<IWorkflowCommandResult> ExecuteAsync()
        {
            //TODO: Send to rabbit here

            var execResult = new WorkflowStepExecuteResult();

            _logger.Info($"Step send to Rabbit");

            return execResult;
        }
    }
}
