using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{

    public class PetAdoptionService : IPetAdoptionService
    {
        private readonly IPetRepository _petRepository;
        private readonly IAdopterRepository _adopterRepository;
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IAdoptionStrategyResolver _strategyResolver;
        private readonly IUnitOfWork _unitOfWork;

        public PetAdoptionService(
            IPetRepository petRepository,
            IAdopterRepository adopterRepository,
            IAdoptionRepository adoptionRepository,
            IAdoptionStrategyResolver strategyResolver,
            IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _adopterRepository = adopterRepository;
            _adoptionRepository = adoptionRepository;
            _strategyResolver = strategyResolver;
            _unitOfWork = unitOfWork;
        }

        public async Task AdoptPetAsync(Guid petId, Guid adopterId)
        {
            var pet = await _petRepository.GetByIdAsync(petId)
                ?? throw new NotFoundException("Pet not found");

            var adopter = await _adopterRepository.GetByIdAsync(adopterId)
                ?? throw new NotFoundException("Adopter not found");

            _strategyResolver
                .Resolve(pet.Type)
                .Validate(pet, adopter);

            var adoption = new Adoption(pet.Id, adopter.Id);

            pet.AddAdoption(adoption);

            await _adoptionRepository.AddAsync(adoption);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReturnPetAsync(Guid petId)
        {
            var pet = await _petRepository.GetByIdAsync(petId)
                ?? throw new NotFoundException("Pet not found");

            pet.Return();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
