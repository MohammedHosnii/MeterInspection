using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MeterInspectionAPI.Models.Identity.Permissions
{
    public static class PermissionHelper
    {
        public static List<string> GetAllPermissions()
        {
            var permissions = new List<string>();
            var nestedClasses = typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            foreach (var nestedClass in nestedClasses)
            {
                var fields = nestedClass.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                permissions.AddRange(fields.Select(field => field.GetValue(null)?.ToString()));
            }

            return permissions;
        }
    }
}
