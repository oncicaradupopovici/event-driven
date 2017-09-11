using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Charisma.SharedKernel.Reflection
{
    public class ReflectionUtils
    {
        public static object CallGenericMethod(object target, string methodName, Type genericType, params object[] arguments)
        {
            var method = target.GetType().GetTypeInfo().GetMethod(methodName);
            MethodInfo generic = method.MakeGenericMethod(genericType);
            return generic.Invoke(target, arguments);
        }
    }
}
