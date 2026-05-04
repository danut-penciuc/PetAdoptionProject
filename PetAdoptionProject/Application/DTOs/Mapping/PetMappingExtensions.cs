using Application.DTOs.Responses;
using Domain.Entities;

namespace Application.DTOs.Mapping
{

    public static class PetMappingExtensions
    {
        public static PetResponse ToResponse(this Pet pet)
        {
            return new PetResponse
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                AgeAtArrival = pet.AgeAtArrival,
                ArrivalDate = pet.ArrivalDate,
                IsAdopted = pet.IsAdopted()
            };
        }
    }
}
