using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Shared;
using MeterInspectionDB;
using System.Net;

namespace MeterInspectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly GpiConfig _config;
        ApiResponse<string> res;
        public SyncController(GpiConfig config)
        {
            _config = config;
        }

        [HttpPost("sync-all")]
        public async Task<IActionResult> SyncAll()
        {
            try
            {
                res = new ApiResponse<string>();
              
                var repository = new SyncRepository(_config);

                await repository.ExecuteSyncAllTablesAsync();

                res.Data = "قد تم المزامنة بنجاح";
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = "خطأ في المزامنة";
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }
    }
}