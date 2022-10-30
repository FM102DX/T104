using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace T104.Store.Service.Metamodel
{
    public interface IEntityInfo
    {

        //хранит инфо о сущности
        public IMetaModel Parent { get; set; }
        public Serilog.ILogger Logger { get; }
        public string TypeName { get; }
        public string TableName { get; set; }
        public bool AllowId { get; set; }
        public int RulesCount { get; }
        public bool AllowDateTimeOfCreation { get; set; }
        public bool AllowDateTimeOfLastChange { get; set; }
        public bool AllowCreatedByUserId { get; set; }
        public IEntityInfo Configure(string tableName = "",
                                    bool allowId = true,
                                    bool allowDateTimeOfCreation = true,
                                    bool allowDateTimeOfLastChange = true,
                                    bool allowCreatedByUserId = true);
        public ParameterInfo AddParameterInfo(ParameterInfo parameterInfo);

        public ValidationResult Validate(ValidationTypeEnum validationType, object member);



        public IEntityInfo AddValidationRule(ValidationTypeEnum validationType, EntityInfoValidationdelegate validationDelegate, string Alias="");
    }
}
