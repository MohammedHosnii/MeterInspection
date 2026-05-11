namespace MeterInspectionAPI.Models.Identity.Permissions
{
    public class PermissionsType
    {
        public int Id { get; set; } // Identity column
        public string GroupName { get; set; } // Group name
        public string PermissionNameAr { get; set; } // Permission name in Arabic
        public string PermissionNameEn { get; set; } // Permission name in English

    }
}
