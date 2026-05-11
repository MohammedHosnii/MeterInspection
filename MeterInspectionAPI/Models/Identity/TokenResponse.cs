using System;

namespace MeterInspectionAPI.Models.Identity
{
    public class TokenResponse
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
