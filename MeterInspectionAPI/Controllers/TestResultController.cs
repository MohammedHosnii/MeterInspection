using Microsoft.AspNetCore.Mvc;
using System.Net;
using MeterInspectionDB;
using MeterInspectionAPI.Models;
using MeterInspectionApi;
using MeterInspectionDB.Model;

namespace MeterInspectionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultController : ControllerBase
    {
        private readonly TestResultRepository _repo;
        ApiResponse<TestResult> res;

        public TestResultController(TestResultRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IEnumerable<TestResult>> res1 =
                new ApiResponse<IEnumerable<TestResult>>();

            try
            {
                var testResults = await _repo.GetAllAsync();

                res1.Data = testResults;
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
        public async Task<IActionResult> GetById(int id)
        {
            res = new ApiResponse<TestResult>();

            try
            {
                var testResult = await _repo.GetByIdAsync(id);

                if (testResult == null)
                {
                    res.Message = "غير موجود";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                res.Data = testResult;
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
        public async Task<IActionResult> Create(TestResult model)
        {
            try
            {
                res = new ApiResponse<TestResult>();

                if (model == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.AddAsync(model);

                res.Data = model;
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
        public async Task<IActionResult> Update(TestResult model)
        {
            try
            {
                res = new ApiResponse<TestResult>();

                if (model == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.UpdateAsync(model);

                res.Data = model;
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