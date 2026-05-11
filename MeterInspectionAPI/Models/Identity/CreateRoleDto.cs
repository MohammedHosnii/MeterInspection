using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeterInspectionAPI.Models.Identity
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }
}
