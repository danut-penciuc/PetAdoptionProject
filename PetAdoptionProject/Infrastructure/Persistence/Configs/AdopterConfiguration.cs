using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configs
{

    public class AdopterConfiguration : IEntityTypeConfiguration<Adopter>
    {
        public void Configure(EntityTypeBuilder<Adopter> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.ClientCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(a => a.ClientCode)
                .IsUnique();
        }
    }
}

