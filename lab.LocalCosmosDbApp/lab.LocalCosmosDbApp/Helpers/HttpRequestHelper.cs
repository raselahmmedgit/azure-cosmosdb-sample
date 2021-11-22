using FluentValidation;
using lab.LocalCosmosDbApp.Utility;
using lab.LocalCosmosDbApp.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.Globalization;

namespace lab.LocalCosmosDbApp.Helpers
{
    public static class HttpRequestHelper
    {
        public static T GetModelFromQuery<T>(HttpRequest httpRequest) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties(); // to get all properties from Class(Object)  
            foreach (var property in properties)
            {
                var valueAsString = httpRequest.Query[property.Name];
                object value = ParseToObject(property.PropertyType, valueAsString); // parse data types  

                if (value == null)
                { continue; }

                property.SetValue(obj, value, null); //set values to properties.  
            }
            return obj;
        }

        // all in one parse method.   
        public static object ParseToObject(Type dataType, string valueToConvert)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(dataType);
            object value = typeConverter.ConvertFromString(null, CultureInfo.InvariantCulture, valueToConvert);
            return value;
        }

        public static FluentValidationResult GetModelFromQuery<T, V>(this HttpRequest request)
            where T : new()
            where V : AbstractValidator<T>, new()
        {
            var requestObject = HttpRequestHelper.GetModelFromQuery<T>(request);
            var validationResult = FluentValidationHelper.IsValid<T, V>(requestObject);
            return validationResult;
        }
    }

}
