using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Shared;
using MeterInspectionDB;
using System.Net;
using MeterInspectionAPI;
using MeterInspectionDB.Model;

namespace MeterInspectionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly SyncRepository
            _syncRepository;

        private readonly ConnectionStatusService
            _connectionStatus;
        

        public SyncController(
            SyncRepository syncRepository,
            ConnectionStatusService connectionStatus)
        {
            _syncRepository =
                syncRepository;

            _connectionStatus =
                connectionStatus;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync()
        {
            var res = new ApiResponse<String>();

            bool isOnline =
                _connectionStatus
                    .IsOnline();

            if (!isOnline)
            {
                res.Message = "Server is OFFline";
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }

            await _syncRepository
                .ExecuteSyncAllTablesAsync();

            res.Data = "Sync Succeeded";
            res.StatusCode = HttpStatusCode.OK;
            res.Succeeded = true;

            return Ok(res);
        }
    }
}