using Application.Pagination;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{

    public interface IPetRepository
    {
        Task<Pet?> GetByIdAsync(Guid id);

        Task<PagedResult<Pet>> GetPagedAsync(PaginationRequest request);

        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(Pet pet);
    }
}
