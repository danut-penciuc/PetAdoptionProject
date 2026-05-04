using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Seed
{

    public static class PetGenerator
    {
        public static List<Pet> Generate(int count)
        {
            var pets = new List<Pet>();
            var random = new Random();

            var types = Enum.GetValues<PetType>();

            for (int i = 1; i <= count; i++)
            {
                var type = types[random.Next(types.Length)];

                pets.Add(new Pet(
                     name: $"Pet-{type}-{i}",
                     type: type,
                     ageAtArrival: random.Next(1, 16),
                     arrivalDate: DateTime.UtcNow.AddDays(-random.Next(1, 180))
                 ));
            }

            return pets;
        }
    }
}
