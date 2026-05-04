using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PetAdoptionProject.Controllers
{
    [ApiController]
    [Route("api/adopters")]
    public class AdoptersController : ControllerBase
    {
        private readonly IAdopterService _adopterService;

        public AdoptersController(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdopterRequest request)
        {
            var id = await _adopterService.CreateAsync(request);
            return Ok(id);
        }


        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _adopterService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }
    }
}
