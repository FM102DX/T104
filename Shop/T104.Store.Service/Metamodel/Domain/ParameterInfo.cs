using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public class ParameterInfo
    {
        public ParameterInfo (string fieldClassName, FieldTypeEnum fieldType, string caption, string fieldDbName="")
        {
            FieldClassName = fieldClassName;
            FieldDbName = fieldDbName;
            FieldType = fieldType;
            Caption = caption;
        }

        private List<ValidationRule> _validationRules = new List<ValidationRule>();

        public IEntityInfo Entity{get => Parent;}
        
        public IEntityInfo Parent;
        public string FieldClassName { get; set; }
        public string FieldDbName { get; set; }
        public string Caption { get; set; } = ""; //для подписей в контролах
        public FieldTypeEnum FieldType { get; set; }
        public object ActualValue { get; set; }
        public object NewValue { get; set; }   //в ситуации когда поле dirty, сохраняет значение, которое есть в объекте

        //public bool SaveHistory = false; // сохранять ли историю конкретно по этому полю
        //public bool IsSearchable = false;
        //public bool isAvialbeForGroupOperations;
        //public bool sortable = false; //можно ли по этому полю сортировать в DFC
        //public bool isDbStorable = true;

        public bool IsPrimaryKey = false; //игнорируется, если 

        public bool IsUnique = false; 

        public bool IsNull { get { return _nullabilityInfo.allowNull && _nullabilityInfo.considerNull; } }

        public NullabilityInfo _nullabilityInfo = new NullabilityInfo();

        public bool IsStringParameter
        {
            get
            {
                if (FieldType == FieldTypeEnum.String || FieldType == FieldTypeEnum.Memo) { return true; }
                return false;
            }
        }


        public bool IsDateTimeParameter
        {
            get
            {
                return FieldType == FieldTypeEnum.Date || FieldType == FieldTypeEnum.DateTime || FieldType == FieldTypeEnum.Time;
            }
        }

        public ParameterInfo AddValidationRule(ValidationTypeEnum validationType, IValidationFunction validationFunction)
        {
            _validationRules.Add(new ValidationRule { Parent = this, ValidationType = validationType, ValidationFunction = validationFunction });
            return this;
        }

        public ValidationResult Validate(ValidationTypeEnum validationType, object value)
        {
            //валидация данного поля по определенному типу правил
            ValidationResult _rez;

            foreach (ValidationRule validationRule in _validationRules)
            {
                //здесь не линк, потому что линк медленный, а этих правил и так не много
                if (validationRule.ValidationType == validationType)
                {
                    _rez = validationRule.Validate(value);
                    if (!_rez.success) return _rez;
                }
            }

            return ValidationResult.sayOk();
        }
    }



    public class ValidationRule
    {
        public ParameterInfo Parent { get; set; }

        public ValidationTypeEnum ValidationType { get; set; }

        public IValidationFunction ValidationFunction { get; set; }

        public ValidationResult Validate(object value)
        {
            return ValidationFunction.Validate(Parent, value);
        }


    }

}
