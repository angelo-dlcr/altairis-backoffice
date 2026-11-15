using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Altairis.Application.Helpers
{
    public static class ValidationsExtensions
    {
        public static bool AreAllPropertiesNull(this object? obj)
        {
            if (obj == null) return true;

            return obj.GetType()
                      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .All(p => p.GetValue(obj) == null);
        }

        public static bool HasAnyValue(this object? obj)
        {
            if (obj == null) return false;

            return obj.GetType()
                      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Any(p => p.GetValue(obj) != null);
        }
    }
}
