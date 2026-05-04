using Application.DTOs.Requests;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.Service
{
    public class PetManagementServiceIntegrationTests : IDisposable
    {
        private readonly PetManagementService _petManagementService;
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public PetManagementServiceIntegrationTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PetDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _petRepository = new PetRepository(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext);
            _petManagementService = new PetManagementService(_petRepository, _unitOfWork);
        }

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ShouldAddPetToDatabase_WhenPetIsCreated()
        {
            // Arrange
            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 2,
                ArrivalDate = DateTime.UtcNow
            };

            // Act
            var petId = await _petManagementService.CreateAsync(request);

            // Assert
            var pet = await _dbContext.Pets.FindAsync(petId);
            pet.Should().NotBeNull();
            pet.Name.Should().Be("Fluffy");
            pet.Type.Should().Be(PetType.Dog);
            pet.AgeAtArrival.Should().Be(2);
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPet_WhenPetExists()
        {
            // Arrange
            var pet = new Pet("Fluffy", PetType.Dog, 2, DateTime.UtcNow);
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _petManagementService.GetByIdAsync(pet.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Fluffy");
            result.Type.Should().Be(PetType.Dog);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Act
            Func<Task> act = async () => await _petManagementService.GetByIdAsync(Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Pet not found");
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePet_WhenPetExists()
        {
            // Arrange
            var pet = new Pet("OldName", PetType.Cat, 1, DateTime.UtcNow);
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 3,
                ArrivalDate = DateTime.UtcNow
            };

            // Act
            await _petManagementService.UpdateAsync(pet.Id, request);

            // Assert
            var updatedPet = await _dbContext.Pets.FindAsync(pet.Id);
            updatedPet.Name.Should().Be("Fluffy");
            updatedPet.AgeAtArrival.Should().Be(3);
            updatedPet.Type.Should().Be(PetType.Dog);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Act
            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 3,
                ArrivalDate = DateTime.UtcNow
            };

            Func<Task> act = async () => await _petManagementService.UpdateAsync(Guid.NewGuid(), request);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Pet not found");
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ShouldDeletePet_WhenPetExists()
        {
            // Arrange
            var pet = new Pet("Fluffy", PetType.Dog, 2, DateTime.UtcNow);
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            // Act
            await _petManagementService.DeleteAsync(pet.Id);

            // Assert
            var deletedPet = await _dbContext.Pets.FindAsync(pet.Id);
            deletedPet.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Act
            Func<Task> act = async () => await _petManagementService.DeleteAsync(Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Pet not found");
        }

        #endregion

        #region Cleanup

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        #endregion
    }
}
