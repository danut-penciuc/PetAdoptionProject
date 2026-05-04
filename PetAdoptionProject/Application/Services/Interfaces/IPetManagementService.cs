using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Pagination;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IPetManagementService
    {
        Task<Guid> CreateAsync(CreatePetRequest request);
        Task<PetResponse> GetByIdAsync(Guid id);
        Task<PagedResult<PetResponse>> GetPagedAsync(int page, int pageSize);
        Task UpdateAsync(Guid id, CreatePetRequest request);
        Task DeleteAsync(Guid id);
    }
}
