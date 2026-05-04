using Domain.Entities.Common;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Guards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pet : BaseEntity
    {
        private readonly List<Adoption> _adoptions = new();

        public string Name { get; private set; }
        public PetType Type { get; private set; }
        public int AgeAtArrival { get; private set; }
        public DateTime ArrivalDate { get; private set; }

        public IReadOnlyCollection<Adoption> Adoptions => _adoptions;

        private Pet() { }

        public Pet(string name, PetType type, int ageAtArrival, DateTime arrivalDate)
        {
            Guard.AgainstNullOrWhiteSpace(name, nameof(name));
            Guard.AgainstNegative(ageAtArrival, nameof(ageAtArrival));
            Guard.AgainstFutureDate(arrivalDate, nameof(arrivalDate));

            Name = name;
            Type = type;
            AgeAtArrival = ageAtArrival;
            ArrivalDate = arrivalDate;
        }

        public bool IsAdopted()
            => _adoptions.Any(x => x.IsActive);

        public void AddAdoption(Adoption adoption)
        {
            if (IsAdopted())
                throw new DomainException("Pet is already adopted");

            _adoptions.Add(adoption);
        }

        public void Return()
        {
            var active = _adoptions.FirstOrDefault(x => x.IsActive);

            if (active is null)
                throw new DomainException("Pet is not currently adopted");

            active.Close();
        }

        public void Update(string name, PetType type, int ageAtArrival)
        {
            Guard.AgainstNullOrWhiteSpace(name, nameof(name));
            Guard.AgainstNegative(ageAtArrival, nameof(ageAtArrival));

            Name = name;
            Type = type;
            AgeAtArrival = ageAtArrival;
        }
    }
}
