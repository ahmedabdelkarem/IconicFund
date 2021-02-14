using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace IconicFund.Helpers.Enums
{
    public static class EnumsExtensionMethods
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi?.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);
            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;

            else
                return enumValue.ToString();
        }

        //public static string GetDisplayName(this Enum e)
        //{
        //    var rm = new ResourceManager(typeof(Labels));
        //    var resourceDisplayName = rm.GetString(e.GetType().Name + "_" + e);
        //    return string.IsNullOrWhiteSpace(resourceDisplayName) ? string.Format("[[{0}]]", e) : resourceDisplayName;
        //}

        public static string Description<T>(this T enumerationValue) where T : struct
        {
            var descAttribute = GetAttributeObject(enumerationValue, typeof(DescriptionAttribute));
            if (descAttribute != null)
            {
                return ((DescriptionAttribute)descAttribute).Description;
            }
            return enumerationValue.ToString();
        }


        #region Private Methods
        private static object GetAttributeObject(object enumerationValue, Type attributueType)
        {
            var enumType = enumerationValue.GetType();
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a attribute for a potential friendly name
            //for the enum
            var memberInfo = enumType.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(attributueType, false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the attribute object
                    return attrs[0];
                }
            }
            //If we have no attribute return null
            return null;
        }
        #endregion




        public static IEnumerable<SelectListItem> GetEnumValueSelectList<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.GetType().GetField(x.ToString()).GetCustomAttribute<DisplayAttribute>()?.GetName(),
                        Value = x.ToString()
                    }), "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetEnumValueSelectListWithIntValues<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.GetType().GetField(x.ToString()).GetCustomAttribute<DisplayAttribute>()?.GetName(),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");
        }

    }
}
