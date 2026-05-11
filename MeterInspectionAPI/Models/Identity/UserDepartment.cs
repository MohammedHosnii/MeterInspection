using Microsoft.AspNetCore.Identity;

namespace MeterInspectionAPI.Models.Identity
{
    public class UserDepartment
    {
        public int Id { get; set; } // Primary Key
        public string UserId { get; set; } // Foreign Key (GUID from AspNetUsers table)
        public string DepartmentCode { get; set; } // Department code

        // Navigation property
        public ApplicationUser User { get; set; } // Assuming you're using IdentityUser
    }
}
