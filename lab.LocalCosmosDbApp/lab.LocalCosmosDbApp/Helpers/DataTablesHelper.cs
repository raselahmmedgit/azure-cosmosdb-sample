using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Helpers
{
    public static class DataTablesHelper
    {
        public static T AddDataTablesHeader<T>(T model) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties(); // to get all properties from Class(Object)  
            foreach (var property in properties)
            {

                var propertyInfo = typeof(T).GetProperty(property.Name);
                var displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                var displayName = (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;

            }
            return obj;
        }

    }

}
