using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core.Internal;
using Castle.DynamicProxy.Internal;

namespace Castle.Facilities.Aspect
{
    public static class TypeExtensions
    {
        public static T GetCustomAttributes<T>(Type type, MethodInfo methodInfo) where T : Attribute
        {
            var method = InvocationHelper.GetMethodOnType(type, methodInfo);

            if (method != null)
                return method.GetAttribute<T>();

            return null;
        }

        public static bool IsBasedOn(this Type type, Type baseType)
        {
            return CheckBaseType(type, baseType);
        }

        public static bool CheckBaseType(Type type, Type baseType)
        {
            if (baseType.IsAssignableFrom(type))
            {

                return true;
            }
            if (baseType.IsGenericTypeDefinition)
            {
                if (baseType.IsInterface)
                {
                    return IsBasedOnGenericInterface(type, baseType);
                }
                return IsBasedOnGenericClass(type, baseType);
            }
            return false;
        }

        public static bool IsBasedOnGenericClass(Type type, Type baseType)
        {
            while (type != null)
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == baseType))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        public static bool IsBasedOnGenericInterface(Type type, Type baseType)
        {
            List<Type> list = new List<Type>(4);
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType && (interfaceType.GetGenericTypeDefinition() == baseType))
                {
                    if ((interfaceType.ReflectedType == null) && interfaceType.ContainsGenericParameters)
                    {
                        list.Add(interfaceType.GetGenericTypeDefinition());
                    }
                    else
                    {
                        list.Add(interfaceType);
                    }
                }
            }
            return (list.Count > 0);
        }
    }
}
