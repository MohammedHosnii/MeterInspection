using MeterInspectionApi;
using MeterInspectionDB.Model;
using MeterInspectionDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeterInspectionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceRecordDetailController : ControllerBase
    {
        private readonly MaintenanceRecordDetailRepository _repo;

        public MaintenanceRecordDetailController(MaintenanceRecordDetailRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new ApiResponse<IEnumerable<MaintenanceRecordDetail>>();

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
            var res = new ApiResponse<MaintenanceRecordDetail>();

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

        /// <summary>
        /// التفاصيل الخاصة لمحضر معين
        /// </summary>
        [HttpGet("ByMaintenanceRecord/{id}")]
        public async Task<IActionResult> GetByMaintenanceRecordId(int id)
        {
            var res = new ApiResponse<MaintenanceRecordDetail>();

            try
            {
                var data = await _repo.GetByMaintenanceRecordIdAsync(id);

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
        public async Task<IActionResult> Create(MaintenanceRecordDetail model)
        {
            var res = new ApiResponse<MaintenanceRecordDetail>();

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
        public async Task<IActionResult> Update(MaintenanceRecordDetail model)
        {
            var res = new ApiResponse<MaintenanceRecordDetail>();

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

                if (result == null)
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
        public async Task<IActionResult> Delete(int id,int DeletedUser)
        {
            var res = new ApiResponse<MaintenanceRecordDetail>();

            try
            {
                var result = await _repo.DeleteAsync(id, DeletedUser);

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
