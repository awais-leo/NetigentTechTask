using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace NetigentWebApi.Controllers
{
    [Route("api/inquiries")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inqury>>> GetAll()
        {
            return Ok(await _inquiryService.GetAllInquiriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inqury>> GetById(int id)
        {
            var inquiry = await _inquiryService.GetInquiryByIdAsync(id);
            if (inquiry == null) return NotFound();
            return Ok(inquiry);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Inqury inquiry)
        {
            var result = await _inquiryService.AddInquiryAsync(inquiry);
            if (result > 0) return CreatedAtAction(nameof(GetById), new { id = inquiry.Id }, inquiry);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Inqury inquiry)
        {
            if (id != inquiry.Id) return BadRequest();
            var result = await _inquiryService.UpdateInquiryAsync(inquiry);
            return result > 0 ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _inquiryService.DeleteInquiryAsync(id);
            return result > 0 ? NoContent() : NotFound();
        }
    }
}
