using Domain.Enums;
using Domain.Exceptions;
using Domain.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Strategies
{

    public class AdoptionStrategyResolver : IAdoptionStrategyResolver
    {
        private readonly IEnumerable<IAdoptionStrategy> _strategies;

        public AdoptionStrategyResolver(IEnumerable<IAdoptionStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IAdoptionStrategy Resolve(PetType petType)
        {
            var strategy = _strategies.FirstOrDefault(s => s.SupportedType == petType);

            if (strategy is null)
                throw new DomainException($"No adoption strategy found for {petType}");

            return strategy;
        }
    }
}
