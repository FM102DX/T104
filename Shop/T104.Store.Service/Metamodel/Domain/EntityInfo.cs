using System;
using System.Collections.Generic;
using Serilog;

namespace T104.Store.Service.Metamodel
{

    public delegate ValidationResult EntityInfoValidationdelegate(object member, Serilog.ILogger Logger);

    public class EntityInfo<T> : IEntityInfo where T : class
    {
        //хранит инфо о сущности

        public IMetaModel Parent { get; set; }

        public Serilog.ILogger Logger { get { return Parent.Logger; } }

        List<ParameterInfo> _parameters = new List<ParameterInfo>();

        List<ValidationRule> _validationRules = new List<ValidationRule>();
        public string TableName { get; set; } = "";
        public bool AllowId { get; set; } = true;
        public bool AllowDateTimeOfCreation { get; set; } = true;
        public bool AllowDateTimeOfLastChange { get; set; } = true;
        public bool AllowCreatedByUserId { get; set; } = true;

        public int RulesCount
        {
            get
            {
                return _validationRules.Count;  
            }
        }

        public string TypeName
        {
            get
            {
                return typeof(T).Name;
            }
        }

        public ParameterInfo AddParameterInfo(ParameterInfo parameterInfo)
        {
            parameterInfo.Parent = this;
            _parameters.Add(parameterInfo);
            return parameterInfo;
        }

        public IEntityInfo Configure(string tableName = "",
                                            bool allowId = true,
                                            bool allowDateTimeOfCreation = true,
                                            bool allowDateTimeOfLastChange = true,
                                            bool allowCreatedByUserId = true)
        {
            TableName = tableName;
            AllowId = allowId;
            AllowDateTimeOfCreation = allowDateTimeOfCreation;
            AllowDateTimeOfLastChange = allowDateTimeOfLastChange;
            AllowCreatedByUserId = allowCreatedByUserId;
            return this;
        }


        public ValidationResult Validate(ValidationTypeEnum validationType, object member)
        {
            T _member = (T)member;
            ValidationResult _rez;
            
            // Logger.Information($"validating member {_member.GetType().FullName}");
            // Logger.Information($"has {_validationRules.Count} validation rules ");

            //сначала перебрать vr уровня entity
            foreach (ValidationRule validationRule in _validationRules)
            {
                Logger.Information($"processing rule {validationRule.Alias} ");

                //здесь не линк, потому что линк медленный, а этих правил и так не много
                if (validationRule.ValidationType == validationType)
                {
                    _rez = validationRule.Validate(_member);
                    if (!_rez.success) return _rez;
                }
            }

            // теперь перебираем параметры, и для каждого параметра вызываем валидацию по указанному типу правил
            // в разработке


            return ValidationResult.sayOk();
        }

        public IEntityInfo AddValidationRule(ValidationTypeEnum validationType, EntityInfoValidationdelegate validationDelegate, string Alias= "")
        {
            //правило валидации, кот. прим. ко всему объекту, т.е. там несколько полей, и оно все считается бизнес
            _validationRules.Add(new ValidationRule { ValidationType= validationType, ValidationDelegate = validationDelegate, Parent = this, Alias = Alias});
            return this;
        }

        public class ValidationRule
        {
            public IEntityInfo Parent { get; set; }
            public string Alias { get; set; }
            public Serilog.ILogger Logger { get { return Parent.Logger; } }
            public ValidationTypeEnum ValidationType { get; set; }

            public EntityInfoValidationdelegate ValidationDelegate;

            public ValidationResult Validate(T member)
            {
                //Logger.Information($"VR00: Validating member = {member.ToString()}");
                
                if (ValidationDelegate != null)
                {
                    ValidationResult vr = ValidationDelegate(member, Logger);
                    return vr;
                }
                else
                {
                    Logger.Information($"Noosfer fail VDLG not exist ");
                    return ValidationResult.sayFail("Noosfer fail");
                }
                    
            }


        }


    }



}
