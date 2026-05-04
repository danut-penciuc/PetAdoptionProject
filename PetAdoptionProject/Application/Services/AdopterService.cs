using Application.DTOs.Mapping;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Pagination;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{

    public class AdopterService : IAdopterService
    {
        private readonly IAdopterRepository _repository;
        private readonly IAdopterFactory _factory;
        private readonly IUnitOfWork _unitOfWork;

        public AdopterService(
            IAdopterRepository repository,
            IAdopterFactory factory,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _factory = factory;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(CreateAdopterRequest request)
        {
            var adopter = _factory.Create(
                request.FirstName,
                request.LastName,
                request.ClientCode);

            await _repository.AddAsync(adopter);

            await _unitOfWork.SaveChangesAsync();

            return adopter.Id;
        }


        public async Task<PagedResult<AdopterResponse>> GetPagedAsync(int page, int pageSize)
        {
            var result = await _repository.GetPagedAsync(new PaginationRequest
            {
                Page = page,
                PageSize = pageSize
            });

            return new PagedResult<AdopterResponse>
            {
                Items = result.Items
                     .Select(p => p.ToResponse())
                     .ToList(),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
