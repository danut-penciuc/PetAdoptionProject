using Application.Pagination;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _context.Pets
                .Include(p => p.Adoptions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagedResult<Pet>> GetPagedAsync(PaginationRequest request)
        {
            var query = _context.Pets
                .Include(p => p.Adoptions)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.ArrivalDate)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Pet>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task AddAsync(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
        }

        public async Task DeleteAsync(Pet pet)
        {
            _context.Pets.Remove(pet);
        }
    }
}
