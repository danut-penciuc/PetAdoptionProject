using Domain.Entities;
using Domain.Exceptions;
using Domain.Guards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{

    public class AdopterFactory : IAdopterFactory
    {
        public Adopter Create(string firstName, string lastName, string clientCode)
        {
            Guard.AgainstNullOrWhiteSpace(firstName, nameof(firstName));
            Guard.AgainstNullOrWhiteSpace(lastName, nameof(lastName));
            Guard.AgainstNullOrWhiteSpace(clientCode, nameof(clientCode));

            return new Adopter(firstName, lastName, clientCode);
        }
    }
}
