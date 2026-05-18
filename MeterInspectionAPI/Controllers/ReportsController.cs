using MeterInspectionDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Data;

namespace MeterInspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsRepository _reportsRepository;

        public ReportsController(IDbConnection dbConnection)
        {
            _reportsRepository = new ReportsRepository(dbConnection);
        }
        [HttpPost("GetMaintenanceRecord")]
        public IActionResult GetRptReadingList([FromQuery] string MaintenanceRecord)
        {
            try
            {
                if (MaintenanceRecord == null)
                {
                    return BadRequest("MaintenanceRecord cannot be null");
                }
               
                var result = _reportsRepository.GetMaintenanceRecord(MaintenanceRecord);

                if (result == null)
                {
                    return NotFound("Report data not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
