using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PetAdoptionProject.Controllers
{
    [ApiController]
    [Route("api/adoptions")]
    public class AdoptionsController : ControllerBase
    {
        private readonly IPetAdoptionService _adoptionService;

        public AdoptionsController(IPetAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }

        [HttpPost("adopt")]
        public async Task<IActionResult> Adopt([FromBody] AdoptPetRequest request)
        {
            await _adoptionService.AdoptPetAsync(request.PetId, request.AdopterId);
            return NoContent();
        }

        [HttpPost("return/{petId:guid}")]
        public async Task<IActionResult> Return(Guid petId)
        {
            await _adoptionService.ReturnPetAsync(petId);
            return NoContent();
        }
    }
}
