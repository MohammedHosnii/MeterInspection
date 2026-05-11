using Microsoft.AspNetCore.Mvc;
using MeterInspectionDB;
using System.Net;

namespace MeterInspectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly OFFline_Online _offlineOnline;
        ApiResponse<string> res;

        public ConnectionController(OFFline_Online offlineOnline)
        {
            _offlineOnline = offlineOnline;
        }

        [HttpGet("status")]
        public IActionResult GetConnectionStatus()
        {
            try
            {
                res = new ApiResponse<string>();
                // var status = await _offlineOnline.GetConnectionStatusAsync();

                res.Data = Shared.GPI.Status;
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = "خطأ في الاتصال";
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }
    }
}