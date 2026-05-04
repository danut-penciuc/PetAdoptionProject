using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies.Interfaces
{
    public interface IAdoptionStrategy
    {
        PetType SupportedType { get; }

        void Validate(Pet pet, Adopter adopter);
    }
}
