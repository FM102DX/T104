using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Service.Metamodel
{
    public interface IValidationFunction
    {
        //это конкретная функция, которая выполняет валидацию, она предоставляет собой класс
        ValidationResult Validate(ParameterInfo parameterInfo, object value);
    }
}
