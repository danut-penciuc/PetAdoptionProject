using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Responses
{
    public class PetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int AgeAtArrival { get; set; }
        public DateTime ArrivalDate { get; set; }
        public bool IsAdopted { get; set; }
    }
}
