using System;
using System.ComponentModel.DataAnnotations;

namespace MeterInspectionAPI.Models.Identity
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
