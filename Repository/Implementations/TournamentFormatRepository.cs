using Microsoft.EntityFrameworkCore;
using Tournament.Management.API.Data;
using Tournament.Management.API.Models.Domain;
using Tournament.Management.API.Models.DTOs.TournamentFormat;
using Tournament.Management.API.Repository.Interfaces;

namespace Tournament.Management.API.Repository.Implementations
{
    public class TournamentFormatRepository (TournamentManagerContext context) : ITournamentFormatRepository
    {
        private readonly TournamentManagerContext _context = context;

        public async Task<TournamentFormatDto?> GetByIdAsync(int id)
        {
            var format =  await _context.TournamentFormats
                            .FirstOrDefaultAsync(f => f.Id == id);
            if (format == null)
            {
                return null;
            }

            return new TournamentFormatDto(format.Id, format.Name);
        }

        public async Task<IEnumerable<TournamentFormatDto>> GetFormatsAsync()
        {
            return await _context.TournamentFormats.Select(format => new TournamentFormatDto(format.Id, format.Name)).ToListAsync();
        }
    }
}
