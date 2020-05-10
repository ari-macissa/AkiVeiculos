using AkiVeiculos.Models;
using AkiVeiculos.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AkiVeiculos.Services
{
    public class MarcaService
    {
        private readonly AkiVeiculosContext _context;

        public MarcaService(AkiVeiculosContext context)
        {
            _context = context;
        }

        public async Task<List<Marca>> BuscaTodasAsync()
        {
            return await _context.Marca.OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task CriarAsync(Marca obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Marca> BuscaPorIdAsync(int id)
        {
            return await _context.Marca.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task ApagarAsync(int id)
        {
            var obj = await _context.Marca.FindAsync(id);
            try
            {
                _context.Marca.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não foi possível fazer a deleção, erro de integridade de dados.");
            }
        }

        public async Task AtualizarAsync(Marca obj)
        {
            bool existe = await _context.Marca.AnyAsync(x => x.Id == obj.Id);
            if (!existe)
            {
                throw new NotFoundException("Marca não encontrada.");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
