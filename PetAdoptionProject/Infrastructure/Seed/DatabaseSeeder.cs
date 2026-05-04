using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seed
{

    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            if (await context.Pets.AnyAsync())
                return;

            var pets = PetGenerator.Generate(100);
            var adopters = AdopterGenerator.Generate();

            await context.Pets.AddRangeAsync(pets);
            await context.Adopters.AddRangeAsync(adopters);
            await context.SaveChangesAsync();

            var adoptions = CreateAdoptions(pets, adopters);

            await context.Adoptions.AddRangeAsync(adoptions);
            await context.SaveChangesAsync();
        }

        private static List<Adoption> CreateAdoptions(
            List<Pet> pets,
            List<Adopter> adopters)
        {
            var random = new Random();
            var adoptions = new List<Adoption>();

            var selectedPets = pets
                .OrderBy(_ => random.Next())
                .Take(20)
                .ToList();

            foreach (var pet in selectedPets)
            {
                var adopter = adopters[random.Next(adopters.Count)];

                var adoption = new Adoption(pet.Id, adopter.Id);

                pet.AddAdoption(adoption);

                adoptions.Add(adoption);
            }

            return adoptions;
        }

    }
}
