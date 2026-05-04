using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Adopter : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ClientCode { get; private set; }
        private Adopter() { }
        public Adopter(string firstName, string lastName, string clientCode)
        {
            FirstName = firstName;
            LastName = lastName;
            ClientCode = clientCode;
        }
    }
}
