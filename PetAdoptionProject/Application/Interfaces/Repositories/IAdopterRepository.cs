using Application.Pagination;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IAdopterRepository
    {
        Task<Adopter?> GetByIdAsync(Guid id);
        Task<PagedResult<Adopter>> GetPagedAsync(PaginationRequest request);

        Task AddAsync(Adopter adopter);
    }
}
