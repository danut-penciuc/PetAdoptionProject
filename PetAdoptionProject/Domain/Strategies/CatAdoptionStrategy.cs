using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies
{
    public class CatAdoptionStrategy : IAdoptionStrategy
    {
        public PetType SupportedType => PetType.Cat;

        public void Validate(Pet pet, Adopter adopter)
        {
            // Cats have minimal restrictions(way too many cats in shelters), so we only check if the adopter is not null
            if (adopter is null)
                throw new DomainException("Adopter is required");
        }
    }
}
