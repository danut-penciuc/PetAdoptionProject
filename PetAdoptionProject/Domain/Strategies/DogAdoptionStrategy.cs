using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies
{

    public class DogAdoptionStrategy : IAdoptionStrategy
    {
        public PetType SupportedType => PetType.Dog;

        public void Validate(Pet pet, Adopter adopter)
        {
            if (adopter is null)
                throw new DomainException("Adopter is required");

            if (string.IsNullOrWhiteSpace(adopter.ClientCode))
                throw new DomainException("Dog adopters must have valid client code");

            if (adopter.ClientCode.Length < 5)
                throw new DomainException("Dog adoption requires valid client code length");
        }
    }
}
