using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Seed
{

    public static class AdopterGenerator
    {
        public static List<Adopter> Generate()
        {
            return new List<Adopter>
            {
                new Adopter("John", "Doe", "A12345"),
                new Adopter("Jane", "Smith", "A54321"),
                new Adopter("Michael", "Brown", "A11111"),
                new Adopter("Anna", "White", "A22222"),
                new Adopter("David", "Black", "A33333"),
                new Adopter("Laura", "Green", "A44444"),
                new Adopter("Robert", "Taylor", "A55555"),

                // Hamster-specific adopters
                new Adopter("HamsterLover", "One", "H10001"),
                new Adopter("Tiny", "Cages", "H10002"),
                new Adopter("Fluffy", "Wheel", "H10003"),
                new Adopter("Nora", "Hammy", "H10004"),
                new Adopter("Chris", "Burrow", "H10005")
            };
        }
    }
}
