using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Requests
{
    public class AdoptPetRequest
    {
        public Guid PetId { get; set; }
        public Guid AdopterId { get; set; }
    }
}
