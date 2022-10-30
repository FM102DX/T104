
using System;
using T104.Store.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace T104.Store.Engine.Models
{
    
    public class ShopSetting : BaseEntity
    {
        //домен - например, Shop.Catalogue.Listing
        public string DomainAlias{ get; set; }

        private Serilog.ILogger _logger;

        //имя - напрмиер, PositionsPerPage
        public string Name { get; set; }
                
        //значение. хранится в строке, потом преобразуется к нужному формату
        public string Value { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ShopSettingsManager Manager { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<FixedEnumValueOption> FixedEnumValueOptions { get; private set; } = new List<FixedEnumValueOption>();

        public ShopSetting()
        {
         //   SysMessage = "parameterless constructor was used";
        }


        public ShopSetting(Guid id, DateTime createdDateTime, DateTime lastModifiedDateTime) : base (id, createdDateTime, lastModifiedDateTime)
        {
           // SysMessage = "constructor with parameters was used";
        }

        public ShopSetting SetLogger(Serilog.ILogger logger)
        {
            _logger = logger;
            return this;
        }

        [NotMapped]
        [JsonIgnore]
        public ShopSettingDomain Domain 
        { 
            get 
            {
                ShopSettingDomain rez = Manager?.Domains.GetByAliasOrNull(DomainAlias);
                if (rez == null) return new ShopSettingDomain{ Alias="GeneralAlias", Name="GeneralName" };
                return rez;
            } 
        }


        [NotMapped]
        [JsonIgnore]
        public bool BoolValue
        {
            get 
            {
                bool parseToBoolRez = Boolean.TryParse(Value, out bool boolValue);
                if (parseToBoolRez) return boolValue; else return false;
            }
            set 
            {
                Value =  Convert.ToString(value);
            }

        }

        [NotMapped]
        [JsonIgnore]
        public DateTime DateTimeValue
        {
            get
            {
                bool parseToDateTimeRez = DateTime.TryParse(Value, out DateTime dateTimeValue);
                if (parseToDateTimeRez) return dateTimeValue; else return  DateTime.Parse("01.01.2001");
            }
            set
            {
                Value = value.ToShortDateString();
            }
        }

        [JsonIgnore]
        [NotMapped]
        public int IntValue
        {
            get
            {
                bool parseToIntRez = Int32.TryParse(Value, out int intValue);
                int rez = parseToIntRez ? intValue : 0;
                //_logger?.Information($"getting int value {rez}");
                return rez;

            }
            set
            {
                _logger?.Information($"setting int value {value}");
                Value = Convert.ToString(value);
            }
        }
        
        [JsonIgnore]
        [NotMapped]
        public double DoubleValue
        {
            get
            {
                bool parseToBoubleRez = Double.TryParse(Value, out double doubleValue);
                if (parseToBoubleRez) return doubleValue; else return 0;
            }
            set
            {
                
                Value = Convert.ToString(value);
            }
        }
        
        [JsonIgnore]
        [NotMapped]
        public object TypedValue {

            get
            {
                switch (InnerType)
                {
                    case SettingTypeEnum.Bool:
                        return BoolValue;
                    
                    case SettingTypeEnum.String:
                        return Value;

                    case SettingTypeEnum.Int:
                        return IntValue;

                    case SettingTypeEnum.Double:
                        return DoubleValue;

                    case SettingTypeEnum.DateTime:
                        return DateTimeValue;

                    default: 
                        return null;
                }
            }

            set
            {
                Value = Convert.ToString(value);
            } 
        }
        

        public SettingTypeEnum InnerType { get; set; }

        //описание которое выводится при редактировании
        public string Title { get; set; }

        public string FixedEnumValueOptionsContents 
        {
            get
            {
                //сериализация  строка
                return JsonConvert.SerializeObject(FixedEnumValueOptions);
            }

            set
            {
                //десериализация  строка => коллекция
                FixedEnumValueOptions = JsonConvert.DeserializeObject<List<FixedEnumValueOption>>(value);
            }
        }

        [JsonIgnore]
        [NotMapped]
        public string FullName
        {
            get { return $"{Domain}.{Name}".Trim('.'); }
        }


        public override ShopSetting Clone()
        {
            ShopSetting setting = new ShopSetting(this.Id, this.CreatedDateTime, this.LastModifiedDatTime);
            setting.Manager = Manager;
            setting.DomainAlias = DomainAlias;
            setting.Name = Name;
            setting.Value = Value;
            setting.InnerType = InnerType;
            setting.Title = Title;
            setting._logger = _logger;

            FixedEnumValueOptions.ForEach(x => {
                setting.FixedEnumValueOptions.Add(new FixedEnumValueOption { Index = x.Index, Text = x.Text }); 
            });
            return setting;

        }


        public ShopSetting AddFixedEnumValueOption(int index, string text)
        {
            FixedEnumValueOptions.Add(new FixedEnumValueOption() {  Index= index , Text = text} );

            return this;
        }


        public override string ToString()
        {
            return $"{Name}      ---- {ShortTimeStamp} {SysMessage}";
        }

        public class FixedEnumValueOption
        {
              public int Index{ get; set; }
              public string Text { get; set; }

            public override string ToString()
            {
                return $"index={Index} Text={Text}";
            }

        }

        public enum SettingTypeEnum
        {
            String = 1,
            Int=2,
            Double = 3,
            Bool = 4,
            DateTime=5,
            FixedEnum = 6,
            ObjectList = 7
        }

    }
}