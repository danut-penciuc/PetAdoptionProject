using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Requests
{
    public class CreatePetRequest
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int AgeAtArrival { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
