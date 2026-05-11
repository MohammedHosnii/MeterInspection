using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MeterInspectionAPI.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; }
        public int LevelEnum { get; set; }//(2 section) ( 3 department)
        public int? SectorCode { get; set; }
        public int? DepartmentCode { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
