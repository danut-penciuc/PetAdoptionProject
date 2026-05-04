using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly ApplicationDbContext _context;

        public AdoptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Adoption?> GetByIdAsync(Guid id)
        {
            return await _context.Adoptions
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Adoption>> GetByPetIdAsync(Guid petId)
        {
            return await _context.Adoptions
                .Where(a => a.PetId == petId)
                .ToListAsync();
        }

        public async Task AddAsync(Adoption adoption)
        {
            await _context.Adoptions.AddAsync(adoption);
        }
    }
}
