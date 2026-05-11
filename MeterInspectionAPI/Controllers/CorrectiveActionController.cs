using MeterInspectionDB.Model;
using MeterInspectionDB;
using Microsoft.AspNetCore.Mvc;
using MeterInspectionApi;
using Shared;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace MeterInspectionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CorrectiveActionController : ControllerBase
    {
        private readonly CorrectiveActionRepository _repo;
        ApiResponse<CorrectiveAction> res;
        public CorrectiveActionController(CorrectiveActionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IEnumerable<CorrectiveAction>> res1 = new ApiResponse<IEnumerable<CorrectiveAction>>();

            try
            {
                var CorrectiveAction = await _repo.GetAllAsync();
                res1.Data = CorrectiveAction;
                res1.StatusCode = HttpStatusCode.OK;
                res1.Succeeded = true;

                return Ok(res1);
            }
            catch (Exception ex)
            {
                res1.Message = ex.Message;
                res1.StatusCode = HttpStatusCode.BadRequest;
                res1.Succeeded = false;

                return BadRequest(res1);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            res = new ApiResponse<CorrectiveAction>();
            try
            {
                var correctiveAction = await _repo.GetByIdAsync(id);
                if (correctiveAction == null)
                {
                    res.Message = "غير موجود";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                res.Data = correctiveAction;
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
        public async Task<IActionResult> Create(CorrectiveAction model)
        {
            try
            {
                res = new ApiResponse<CorrectiveAction>();
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
        public async Task<IActionResult> Update(CorrectiveAction model)
        {
            try
            {
                res = new ApiResponse<CorrectiveAction>();
                if (model == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.UpdateAsync(model);
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
            var result = await _repo.DeleteAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}
