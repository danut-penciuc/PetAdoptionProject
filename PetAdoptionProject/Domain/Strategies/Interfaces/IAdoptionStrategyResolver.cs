using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies.Interfaces
{
    public interface IAdoptionStrategyResolver
    {
        IAdoptionStrategy Resolve(PetType petType);
    }
}
