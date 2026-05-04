using Domain.Entities.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Adoption : BaseEntity
    {
        public Guid PetId { get; private set; }
        public Guid AdopterId { get; private set; }

        public DateTime AdoptedAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }

        public bool IsActive => ReturnedAt is null;

        public Pet Pet { get; private set; }
        public Adopter Adopter { get; private set; }

        private Adoption() { } 

        public Adoption(Guid petId, Guid adopterId)
        {
            if (petId == Guid.Empty)
                throw new DomainException("PetId is required");

            if (adopterId == Guid.Empty)
                throw new DomainException("AdopterId is required");

            PetId = petId;
            AdopterId = adopterId;
            AdoptedAt = DateTime.UtcNow;
        }

        public void Close()
        {
            if (!IsActive)
                throw new DomainException("Adoption already closed");

            ReturnedAt = DateTime.UtcNow;
        }
    }
}
