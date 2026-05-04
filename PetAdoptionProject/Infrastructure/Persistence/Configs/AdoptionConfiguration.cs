using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configs
{
    public class AdoptionConfiguration : IEntityTypeConfiguration<Adoption>
    {
        public void Configure(EntityTypeBuilder<Adoption> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.PetId).IsRequired();
            builder.Property(a => a.AdopterId).IsRequired();
            builder.Property(a => a.AdoptedAt).IsRequired();
            builder.Property(a => a.ReturnedAt).IsRequired(false);

            builder.HasOne(a => a.Pet)
                .WithMany(p => p.Adoptions)
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Adopter)
                .WithMany()
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => a.PetId);
            builder.HasIndex(a => a.AdopterId);

            //needed to ensure we can have one active adoption per pet, but allow multiple historical adoptions for the same pet
            builder.HasIndex(a => a.PetId)
             .IsUnique()
             .HasFilter("[ReturnedAt] IS NULL"); 
        }
    }
}
