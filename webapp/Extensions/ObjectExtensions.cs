using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

// @see https://www.codeproject.com/Tips/177125/Get-Nested-Property-value-using-reflection-and-Lin
namespace webapp.Extensions
{

    public static class ObjectExtensions
    {

        // @param TSource source : Root object, reference type or subclass of IEnumerable
        // @param string propertyPath : Dot-separated list of property names
        public static System.Type GetNestedPropertyType<TSource>(
                this TSource source,
                string propertyPath
            )
        {
            var value = GetNestedValue(propertyPath, source);
            return value?.GetType();
        }

        // @param TSource source : Root object, reference type or subclass of IEnumerable
        // @param string propertyPath : Dot-separated list of property names
        // @param TResult defaultValue : Value returned if property not found
        public static TResult GetNestedPropertyValue<TSource, TResult>(
                this TSource source,
                string propertyPath,
                TResult defaultValue
            )
        {
            var value = GetNestedValue(propertyPath, source);
            return value == null ? defaultValue : (TResult)value;
        }

        // @param TSource source : Root object, reference type or subclass of IEnumerable
        // @param TResult expression : Labmda expression to set the property value returned
        // @param TResult defaultValue : Value returned if property not found
        public static TResult NullSafeGetValue<TSource, TResult>(
                this TSource source,
                Expression<Func<TSource, TResult>> expression,
                TResult defaultValue
            )
        {
            var value = GetValue(expression, source);
            return value == null ? defaultValue : (TResult)value;
        }

        // @param TSource source : Root object
        // @param TResult expression : Labmda expression to set the property value returned
        // @param TCastResultType defaultValue : Value returned if property not found
        // @param Func convertToResultToAction : Labmda expression to cast the returned value
        public static TCastResultType NullSafeGetValue<TSource, TResult, TCastResultType>(
                this TSource source,
                Expression<Func<TSource, TResult>> expression,
                TCastResultType defaultValue,
                Func<object, TCastResultType> convertToResultToAction
                )
        {
            var value = GetValue(expression, source);
            return value == null ? defaultValue : convertToResultToAction.Invoke(value);
        }

        private static object GetValue<TSource, TResult>(
                Expression<Func<TSource, TResult>> expression,
                TSource source
                )
        {
            string propertyPath = expression.Body.ToString().Replace(expression.Parameters[0] + ".", string.Empty);
            return GetNestedValue(propertyPath, source);
        }

        private static object GetNestedValue(
                string name,
                object obj
                )
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null)
                    return null;


                if (obj is IEnumerable)
                {
                    Type type = (obj as IEnumerable).GetType();
                    MethodInfo methodInfo = type.GetMethod("get_Item");
                    int index = int.Parse(part.Split('(')[1].Replace(")", string.Empty));
                    try
                    {
                        obj = methodInfo.Invoke(obj, new object[] { index });
                    }
                    catch (Exception)
                    {
                        obj = null;
                    }
                }
                else
                {
                    PropertyInfo info = obj.GetType().GetProperty(part);
                    if (info == null)
                    {
                        return null;
                    }
                    obj = info.GetValue(obj, null);
                }
            }
            return obj;
        }
    }

}
