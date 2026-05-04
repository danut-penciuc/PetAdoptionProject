using Application.DTOs.Requests;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Pagination;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Service
{
    public class PetManagementServiceTests
    {
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly PetManagementService _petManagementService;

        public PetManagementServiceTests()
        {
            _mockPetRepository = new Mock<IPetRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _petManagementService = new PetManagementService(_mockPetRepository.Object, _mockUnitOfWork.Object);
        }

        #region GetPagedAsync Tests

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResult_WhenPetsExist()
        {
            // Arrange
            var petList = new PagedResult<Pet>
            {
                Items = new List<Pet> { new Pet("Fluffy", PetType.Dog, 2, DateTime.UtcNow) },
                Page = 1,
                PageSize = 10,
                TotalCount = 1
            };

            _mockPetRepository.Setup(r => r.GetPagedAsync(It.IsAny<PaginationRequest>()))
                .ReturnsAsync(petList);

            // Act
            var result = await _petManagementService.GetPagedAsync(1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.Page.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalCount.Should().Be(1);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnEmptyList_WhenNoPetsExist()
        {
            // Arrange
            var petList = new PagedResult<Pet>
            {
                Items = new List<Pet>(),
                Page = 1,
                PageSize = 10,
                TotalCount = 0
            };

            _mockPetRepository.Setup(r => r.GetPagedAsync(It.IsAny<PaginationRequest>()))
                .ReturnsAsync(petList);

            // Act
            var result = await _petManagementService.GetPagedAsync(1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPet_WhenPetExists()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var pet = new Pet("Fluffy", PetType.Dog, 2, DateTime.UtcNow);
            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync(pet);

            // Act
            var result = await _petManagementService.GetByIdAsync(petId);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Fluffy");
            result.Type.Should().Be(PetType.Dog);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync((Pet)null);

            // Act
            Func<Task> act = async () => await _petManagementService.GetByIdAsync(petId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Pet not found");
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ShouldReturnPetId_WhenPetIsCreatedSuccessfully()
        {
            // Arrange
            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 2,
                ArrivalDate = DateTime.UtcNow
            };

            var pet = new Pet(request.Name, request.Type, request.AgeAtArrival, request.ArrivalDate);
            _mockPetRepository.Setup(r => r.AddAsync(It.IsAny<Pet>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _petManagementService.CreateAsync(request);

            // Assert
            result.Should().NotBeEmpty();
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePet_WhenPetExists()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 3,
                ArrivalDate = DateTime.UtcNow
            };
            var pet = new Pet("OldName", PetType.Cat, 1, DateTime.UtcNow);
            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync(pet);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _petManagementService.UpdateAsync(petId, request);

            // Assert
            pet.Name.Should().Be("Fluffy");
            pet.AgeAtArrival.Should().Be(3);
            pet.Type.Should().Be(PetType.Dog);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var request = new CreatePetRequest
            {
                Name = "Fluffy",
                Type = PetType.Dog,
                AgeAtArrival = 3,
                ArrivalDate = DateTime.UtcNow
            };

            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync((Pet)null);

            // Act
            Func<Task> act = async () => await _petManagementService.UpdateAsync(petId, request);

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
            var petId = Guid.NewGuid();
            var pet = new Pet("Fluffy", PetType.Dog, 2, DateTime.UtcNow);
            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync(pet);
            _mockPetRepository.Setup(r => r.DeleteAsync(pet)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _petManagementService.DeleteAsync(petId);

            // Assert
            _mockPetRepository.Verify(r => r.DeleteAsync(pet), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException_WhenPetDoesNotExist()
        {
            // Arrange
            var petId = Guid.NewGuid();
            _mockPetRepository.Setup(r => r.GetByIdAsync(petId)).ReturnsAsync((Pet)null);

            // Act
            Func<Task> act = async () => await _petManagementService.DeleteAsync(petId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Pet not found");
        }

        #endregion
    }
}
