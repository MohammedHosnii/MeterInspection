using MeterInspectionApi;
using MeterInspectionDB.Model;
using MeterInspectionDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeterInspectionAPI.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceRecordController : ControllerBase
    {
        private readonly MaintenanceRecordRepository _repo;

        public MaintenanceRecordController(MaintenanceRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new ApiResponse<IEnumerable<MaintenanceRecord>>();

            try
            {
                var data = await _repo.GetAllAsync();

                res.Data = data;
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = new ApiResponse<MaintenanceRecord>();

            try
            {
                var data = await _repo.GetByIdAsync(id);

                if (data == null)
                {
                    res.Message = "غير موجود";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                res.Data = data;
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaintenanceRecord model)
        {
            var res = new ApiResponse<MaintenanceRecord>();

            try
            {
                if (model == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.AddAsync(model);

                res.Data = result;
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(MaintenanceRecord model)
        {
            var res = new ApiResponse<MaintenanceRecord>();

            try
            {
                if (model == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.UpdateAsync(model);

                if (result==null)
                {
                    res.Message = "غير موجود";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                res.Data = result;
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id ,int userID)
        {
            var res = new ApiResponse<MaintenanceRecord>();

            try
            {
                var result = await _repo.DeleteAsync(id, userID);

                if (!result)
                {
                    res.Message = "غير موجود";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                res.Data = null;
                res.Message = "تم الحذف بنجاح";
                res.StatusCode = HttpStatusCode.OK;
                res.Succeeded = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.StatusCode = HttpStatusCode.BadRequest;
                res.Succeeded = false;

                return BadRequest(res);
            }
        }
    }
}
