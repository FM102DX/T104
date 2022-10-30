using T104.Store.Engine.Enums;
//using T104.Store.Engine.JsonConverters;
using System;

namespace T104.Store.Engine.Abstract
{
    //[JsonInterfaceConverter(typeof(IWorkflowStepActionConverter))]
    public interface IWorkflowStepAction
    {
        Guid Uid { get; set; }
        WorkflowActionKinds Kind { get; }
    }
}
