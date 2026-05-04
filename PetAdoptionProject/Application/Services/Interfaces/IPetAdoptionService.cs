using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IPetAdoptionService
    {
        Task AdoptPetAsync(Guid petId, Guid adopterId);
        Task ReturnPetAsync(Guid petId);
    }
}
