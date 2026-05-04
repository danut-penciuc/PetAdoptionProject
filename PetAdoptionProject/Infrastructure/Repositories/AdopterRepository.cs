using Application.Interfaces.Repositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{

    public class AdopterRepository : IAdopterRepository
    {
        private readonly ApplicationDbContext _context;

        public AdopterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Adopter?> GetByIdAsync(Guid id)
        {
            return await _context.Adopters
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<PagedResult<Adopter>> GetPagedAsync(PaginationRequest request)
        {
            var query = _context.Adopters
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.FirstName)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Adopter>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }


        public async Task AddAsync(Adopter adopter)
        {
            await _context.Adopters.AddAsync(adopter);
        }
    }
}
