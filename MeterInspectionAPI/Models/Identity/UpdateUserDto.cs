namespace MeterInspectionAPI.Models.Identity
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        //public string Email { get; set; }
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int LevelEnum { get; set; }
        public int SectorCode { get; set; }
        public int DepartmentCode { get; set; }

    }
}
