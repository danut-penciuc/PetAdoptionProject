using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IAdopterService
    {
        Task<Guid> CreateAsync(CreateAdopterRequest request);
        Task<PagedResult<AdopterResponse>> GetPagedAsync(int page, int pageSize);
    }
}
