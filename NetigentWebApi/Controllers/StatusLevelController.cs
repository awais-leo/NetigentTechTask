using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace NetigentWebApi.Controllers
{
    [Route("api/statuslevels")]
    [ApiController]
    public class StatusLevelController : ControllerBase
    {
        private readonly IStatusLevelService _statusLevelService;

        public StatusLevelController(IStatusLevelService statusLevelService)
        {
            _statusLevelService = statusLevelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusLevel>>> GetAll()
        {
            return Ok(await _statusLevelService.GetAllStatusLevelsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusLevel>> GetById(int id)
        {
            var status = await _statusLevelService.GetStatusLevelByIdAsync(id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StatusLevel statusLevel)
        {
            var result = await _statusLevelService.AddStatusLevelAsync(statusLevel);
            if (result > 0) return CreatedAtAction(nameof(GetById), new { id = statusLevel.Id }, statusLevel);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] StatusLevel statusLevel)
        {
            if (id != statusLevel.Id) return BadRequest();
            var result = await _statusLevelService.UpdateStatusLevelAsync(statusLevel);
            return result > 0 ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _statusLevelService.DeleteStatusLevelAsync(id);
            return result > 0 ? NoContent() : NotFound();
        }
    }
}
