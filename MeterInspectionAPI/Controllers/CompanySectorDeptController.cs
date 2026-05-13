using MeterInspectionApi;
using MeterInspectionDB.Model;
using MeterInspectionDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeterInspectionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CompanySectorDeptController : ControllerBase
    {
        private readonly CompanySectorDeptRepository _repo;

        public CompanySectorDeptController(CompanySectorDeptRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new ApiResponse<IEnumerable<CompanySectorDept>>();

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
            var res = new ApiResponse<CompanySectorDept>();

            try
            {
                var data = await _repo.GetByIdAsync(id);

                if (data == null)
                {
                    res.Message = $"Not found Id = {id}";
                    res.StatusCode = HttpStatusCode.NotFound;
                    res.Succeeded = false;

                    return NotFound(res);
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
        /// الشركات
        /// </summary>
        /// <returns></returns>

        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var res = new ApiResponse<IEnumerable<CompanySectorDept>>();

            try
            {
                var data = await _repo.GetALLCompany();

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
        /// القطاعات
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("sectors/{parentId}")]
        public async Task<IActionResult> GetAllSectors(int parentId)
        {
            var res = new ApiResponse<IEnumerable<CompanySectorDept>>();

            try
            {
                var data = await _repo.GetALLSectors(parentId);

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
        /// الفروع
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("departments/{parentId}")]
        public async Task<IActionResult> GetAllDepartments(int parentId)
        {
            var res = new ApiResponse<IEnumerable<CompanySectorDept>>();

            try
            {
                var data = await _repo.GetALLDepartments(parentId);

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
        public async Task<IActionResult> Create([FromBody] CompanySectorDept dept)
        {
            var res = new ApiResponse<CompanySectorDept>();

            try
            {
                var id = await _repo.AddAsync(dept);

                res.Data = dept;
                res.Message = "Created successfully";
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
        public async Task<IActionResult> Update([FromBody] CompanySectorDept dept)
        {
            var res = new ApiResponse<CompanySectorDept>();

            try
            {
                if (dept == null)
                {
                    res.Message = "بيان خاطئ";
                    res.StatusCode = HttpStatusCode.BadRequest;
                    res.Succeeded = false;

                    return BadRequest(res);
                }

                var result = await _repo.UpdateAsync(dept);

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
            var res = new ApiResponse<CompanySectorDept>();

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
