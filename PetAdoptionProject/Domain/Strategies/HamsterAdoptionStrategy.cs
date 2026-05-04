using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies
{
    public class HamsterAdoptionStrategy : IAdoptionStrategy
    {
        public PetType SupportedType => PetType.Hamster;

        public void Validate(Pet pet, Adopter adopter)
        {
            if (adopter is null)
                throw new DomainException("Adopter is required");

            if (string.IsNullOrWhiteSpace(adopter.ClientCode))
                throw new DomainException("Hamster adopters must have valid client code");

            if (!adopter.ClientCode.StartsWith("h", StringComparison.OrdinalIgnoreCase))
                throw new DomainException("Hamster adopters must have a client code starting with 'h'");
        }
    }
}
