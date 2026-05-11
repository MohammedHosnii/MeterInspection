using System.ComponentModel.DataAnnotations;

namespace MeterInspectionAPI.Models.Identity
{
    public class UpdateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
