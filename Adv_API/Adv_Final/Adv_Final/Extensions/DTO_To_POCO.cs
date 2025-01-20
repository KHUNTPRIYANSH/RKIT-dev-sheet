using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Adv_Final.Extensions
{
    /// <summary>
    /// Extension methods to convert DTO from POCO.
    /// </summary>
    public static class DTO_To_POCO
    {
        /// <summary>
        /// Converts a DTO object to its corresponding POCO type.
        /// </summary>
        /// <typeparam name="POCO">The target POCO type that the DTO will be converted to.</typeparam>
        /// <param name="objDTO">The DTO object to be converted.</param>
        /// <returns>A POCO instance with the same property values as the DTO.</returns>
        public static POCO Convert<POCO>(this object objDTO)
        {

            // As POCO Convert method is generic so we don't know about ref type
            // So get the type of the POCO
            Type pocoType = typeof(POCO);

            // Create a new instance of the POCO type
            // AS it's generic we don't know about type and can't make obj of POCO directly using new keyword
            // We typecase into POCO and use Activator to create obj
            POCO pocoInstance = (POCO)Activator.CreateInstance(pocoType);

            // Get the properties of the DTO object
            PropertyInfo[] dtoProperties = objDTO.GetType().GetProperties();

            // Get the properties of the POCO type
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            // Iterate through each property in the DTO
            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                // Find the corresponding property in the POCO with the same name
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                // If the matching property is found and their types are compatible, copy the value
                if (pocoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    // Get the value from the DTO property
                    object value = dtoProperty.GetValue(objDTO);

                    // Set the value in the POCO property
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            // Return the populated POCO instance
            return pocoInstance;
        }
    }
}