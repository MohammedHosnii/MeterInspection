using MeterInspectionApi;
using MeterInspectionDB;
using MeterInspectionDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeterInspectionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        private readonly ErrorRepository _repo;

        public ErrorController(ErrorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new ApiResponse<IEnumerable<Error>>();

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
            var res = new ApiResponse<Error>();

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
        public async Task<IActionResult> Create(Error model)
        {
            var res = new ApiResponse<Error>();

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
        public async Task<IActionResult> Update(Error model)
        {
            var res = new ApiResponse<Error>();

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
        public async Task<IActionResult> Delete(int id)
        {
            var res = new ApiResponse<Error>();

            try
            {
                var result = await _repo.DeleteAsync(id);

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
