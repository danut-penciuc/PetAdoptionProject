using Application.DTOs.Mapping;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Pagination;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{

    public class PetManagementService : IPetManagementService
    {
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetManagementService(
            IPetRepository petRepository,
            IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<PetResponse>> GetPagedAsync(int page, int pageSize)
        {
            var result = await _petRepository.GetPagedAsync(new PaginationRequest
            {
                Page = page,
                PageSize = pageSize
            });

            return new PagedResult<PetResponse>
            {
                Items = result.Items
                     .Select(p => p.ToResponse())
                     .ToList(),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<PetResponse> GetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Pet not found");

            return pet.ToResponse();
        }

        public async Task<Guid> CreateAsync(CreatePetRequest request)
        {
            var pet = new Pet(
                request.Name,
                request.Type,
                request.AgeAtArrival,
                DateTime.UtcNow);

            await _petRepository.AddAsync(pet);
            await _unitOfWork.SaveChangesAsync();

            return pet.Id;
        }

        public async Task UpdateAsync(Guid id, CreatePetRequest request)
        {
            var pet = await _petRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Pet not found");

            pet.Update(request.Name, request.Type, request.AgeAtArrival);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Pet not found");

            await _petRepository.DeleteAsync(pet);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
