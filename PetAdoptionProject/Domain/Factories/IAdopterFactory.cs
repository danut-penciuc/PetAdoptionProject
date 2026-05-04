using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
    public interface IAdopterFactory
    {
        Adopter Create(string firstName, string lastName, string clientCode);
    }
}
