
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace UnitTests.Domain
{
    public class PetTests
    {
        #region Constructor Tests

        [Fact]
        public void PetConstructor_WhenValidParameters_ShouldCreatePet()
        {
            // Arrange
            var name = "Bella";
            var type = PetType.Dog;
            var ageAtArrival = 3;
            var arrivalDate = DateTime.UtcNow.AddDays(-10);

            // Act
            var pet = new Pet(name, type, ageAtArrival, arrivalDate);

            // Assert
            pet.Name.Should().Be(name);
            pet.Type.Should().Be(type);
            pet.AgeAtArrival.Should().Be(ageAtArrival);
            pet.ArrivalDate.Should().Be(arrivalDate);
        }

        [Fact]
        public void PetConstructor_WhenNameIsEmpty_ShouldThrowDomainException()
        {
            // Arrange
            Action action = () => new Pet("", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));

            // Assert
            action.Should().Throw<DomainException>().WithMessage("name is required");
        }

        [Fact]
        public void PetConstructor_WhenAgeIsNegative_ShouldThrowDomainException()
        {
            // Arrange
            Action action = () => new Pet("Bella", PetType.Dog, -1, DateTime.UtcNow.AddDays(-10));

            // Assert
            action.Should().Throw<DomainException>().WithMessage("ageAtArrival cannot be negative");
        }

        [Fact]
        public void PetConstructor_WhenArrivalDateIsInFuture_ShouldThrowDomainException()
        {
            // Arrange
            Action action = () => new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(10));

            // Assert
            action.Should().Throw<DomainException>().WithMessage("arrivalDate cannot be in the future");
        }

        #endregion

        #region Business Logic Methods

        [Fact]
        public void IsAdopted_WhenPetHasNoAdoptions_ShouldReturnFalse()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));

            // Act
            var result = pet.IsAdopted();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsAdopted_WhenPetHasAdopted_ShouldReturnTrue()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var adoption = new Adoption(Guid.NewGuid(), Guid.NewGuid());
            pet.AddAdoption(adoption);

            // Act
            var result = pet.IsAdopted();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AddAdoption_WhenPetIsNotAdopted_ShouldAddAdoption()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var adoption = new Adoption(Guid.NewGuid(), Guid.NewGuid());

            // Act
            pet.AddAdoption(adoption);

            // Assert
            pet.Adoptions.Should().Contain(adoption);
        }

        [Fact]
        public void AddAdoption_WhenPetIsAlreadyAdopted_ShouldThrowDomainException()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var adoption1 = new Adoption(Guid.NewGuid(), Guid.NewGuid());
            pet.AddAdoption(adoption1);
            var adoption2 = new Adoption(Guid.NewGuid(), Guid.NewGuid());

            // Act
            Action action = () => pet.AddAdoption(adoption2);

            // Assert
            action.Should().Throw<DomainException>().WithMessage("Pet is already adopted");
        }

        [Fact]
        public void Return_WhenPetIsAdopted_ShouldCloseAdoption()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var adoption = new Adoption(Guid.NewGuid(), Guid.NewGuid());
            pet.AddAdoption(adoption);

            // Act
            pet.Return();

            // Assert
            adoption.IsActive.Should().BeFalse();
        }

        [Fact]
        public void Return_WhenPetIsNotAdopted_ShouldThrowDomainException()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));

            // Act
            Action action = () => pet.Return();

            // Assert
            action.Should().Throw<DomainException>().WithMessage("Pet is not currently adopted");
        }

        [Fact]
        public void Return_WhenPetHasAlreadyBeenReturned_ShouldThrowDomainException()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var adoption = new Adoption(Guid.NewGuid(), Guid.NewGuid());
            pet.AddAdoption(adoption);
            pet.Return();  // Pet is now returned

            // Act
            Action action = () => pet.Return();

            // Assert
            action.Should().Throw<DomainException>().WithMessage("Pet is not currently adopted");
        }

        [Fact]
        public void Update_WhenValidParameters_ShouldUpdatePet()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));
            var newName = "Max";
            var newType = PetType.Cat;
            var newAge = 4;

            // Act
            pet.Update(newName, newType, newAge);

            // Assert
            pet.Name.Should().Be(newName);
            pet.Type.Should().Be(newType);
            pet.AgeAtArrival.Should().Be(newAge);
        }

        [Fact]
        public void Update_WhenNameIsEmpty_ShouldThrowDomainException()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));

            // Act
            Action action = () => pet.Update("", PetType.Cat, 4);

            // Assert
            action.Should().Throw<DomainException>().WithMessage("name is required");
        }

        [Fact]
        public void Update_WhenAgeIsNegative_ShouldThrowDomainException()
        {
            // Arrange
            var pet = new Pet("Bella", PetType.Dog, 3, DateTime.UtcNow.AddDays(-10));

            // Act
            Action action = () => pet.Update("Max", PetType.Cat, -1);

            // Assert
            action.Should().Throw<DomainException>().WithMessage("ageAtArrival cannot be negative");
        }

        #endregion
    }
}