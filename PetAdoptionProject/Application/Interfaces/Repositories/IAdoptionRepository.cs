using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IAdoptionRepository
    {
        Task<Adoption?> GetByIdAsync(Guid id);

        Task<List<Adoption>> GetByPetIdAsync(Guid petId);

        Task AddAsync(Adoption adoption);
    }
}
