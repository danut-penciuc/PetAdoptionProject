using Application.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Mapping
{
    public static class AdopterMappingExtensions
    {
        public static AdopterResponse ToResponse(this Adopter adopter)
        {
            return new AdopterResponse
            {
                Id = adopter.Id,
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                ClientCode = adopter.ClientCode
            };
        }
    }
}
