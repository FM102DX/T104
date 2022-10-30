using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.Engine.Models;
using T104.Store.Service.Metamodel;

namespace T104.Store.Engine.MetaModels
{
    public class T104ShopMetaModel : MetaModelBase, IMetaModel
    {
        public T104ShopMetaModel ()
        {
            Random random = new Random();

            IEntityInfo shopSettingEntity = new EntityInfo<ShopSetting>()
                                                .AddParameterInfo(new ParameterInfo("SkuPerPage", FieldTypeEnum.Int, "SkuPerPage"))
                                                .Entity
                                                .AddValidationRule(ValidationTypeEnum.Business, (x, logger) =>
                                                {
                                                    ShopSetting sh = (ShopSetting)x;
                                                    //logger.Information($"This is delegate of SkuPerPageValidation, {sh.Name}");

                                                    

                                                    if (sh.Name == "SkuPerPage")
                                                    {
                                                        int skuPerPage = sh.IntValue;

                                                        // logger.Information($"skuPerPage={skuPerPage}");

                                                        if (skuPerPage > 200 | skuPerPage < 10)
                                                        {
                                                            return ValidationResult.sayFail($"Введите значения от 10 до 200");
                                                        }
                                                    }
                                                    return ValidationResult.sayOk();
                                                }, "SkuPerPageValidation")
                                                .AddValidationRule(ValidationTypeEnum.Business, (x, logger) =>
                                                {
                                                    
                                                    ShopSetting sh = (ShopSetting)x;
                                                   // logger.Information($"This is delegate of MyCatWeightValidation, {sh.Name}");
                                                    if (sh.Name == "MyCatsWeight")
                                                    {
                                                        double myCatWeight = sh.DoubleValue;

                                                        double maxWeight = 5.5;
                                                        double minWeight = 0.3;

                                                        logger.Information($"MyCatsWeight={myCatWeight}");

                                                        if (myCatWeight > maxWeight | myCatWeight < minWeight)
                                                        {
                                                            return ValidationResult.sayFail($"Вес кота должен быть в диапазоне от {minWeight} до {maxWeight}");
                                                        }
                                                    }
                                                    return ValidationResult.sayOk();

                                                }, "MyCatWeightValidation");

            AddEntityInfo(shopSettingEntity);
        }



    }
}
