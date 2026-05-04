using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PetAdoptionProject.Controllers
{

    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IPetManagementService _petService;

        public PetsController(IPetManagementService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetRequest request)
        {
            var id = await _petService.CreateAsync(request);
            return Ok(id);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pet = await _petService.GetByIdAsync(id);
            return Ok(pet);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _petService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePetRequest request)
        {
            await _petService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _petService.DeleteAsync(id);
            return NoContent();
        }
    }
}
