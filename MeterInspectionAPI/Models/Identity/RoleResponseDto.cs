using System.Collections.Generic;

namespace MeterInspectionAPI.Models.Identity
{
    public class RoleResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
