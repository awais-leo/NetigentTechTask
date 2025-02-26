using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace NetigentWebApi.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAll()
        {
            return Ok(await _applicationService.GetAllApplicationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetById(int id)
        {
            var app = await _applicationService.GetApplicationByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application application)
        {
            var result = await _applicationService.AddApplicationAsync(application);
            if (result > 0) return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application application)
        {
            if (id != application.Id) return BadRequest();
            var result = await _applicationService.UpdateApplicationAsync(application);
            return result > 0 ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _applicationService.DeleteApplicationAsync(id);
            return result > 0 ? NoContent() : NotFound();
        }
    }
}
