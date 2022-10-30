using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Shared.Models
{
    public class WorkflowStartDto
    {
        public Guid SettingsUid { get; set; }
        public Guid RunUid { get; set; }
    }
}
