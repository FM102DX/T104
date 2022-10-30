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
    public class WorkflowSleepCommand : IWorkflowCommand
    {
        private readonly WorkflowSleepAction _settings;
        private readonly Logger _logger;

        public WorkflowSleepCommand(WorkflowSleepAction settings, Logger logger)
        {
            _settings = settings;
            _logger = logger;

        }

        public async Task<IWorkflowCommandResult> ExecuteAsync()
        {
            var execResult = new WorkflowStepExecuteResult();
            _logger.Info($"Sleep started for {_settings.DelayMilliseconds}");
            await Task.Delay(_settings.DelayMilliseconds);

            _logger.Info($"Resume after sleep");

            return execResult;

        }
    }
}
