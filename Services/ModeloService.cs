using AkiVeiculos.Models;
using AkiVeiculos.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AkiVeiculos.Services
{
    public class ModeloService
    {
        private readonly AkiVeiculosContext _context;

        public ModeloService(AkiVeiculosContext context)
        {
            _context = context;
        }

        public async Task<List<Modelo>> BuscaTodosAsync(int? marcaId)
        {
            if (marcaId == null)
                return await _context.Modelo.OrderBy(x => x.Nome).ToListAsync();

            return await _context.Modelo.Where(x => x.MarcaId == marcaId.Value).Include(y => y.Marca).OrderBy(z => z.Nome).ToListAsync();
        }

        public async Task<Modelo> BuscaPorIdAsync(int id)
        {
            return await _context.Modelo.Include(x => x.Marca).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task CriarAsync(Modelo obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        public async Task ApagarAsync(int id)
        {
            var obj = await _context.Modelo.FindAsync(id);
            try
            {
                _context.Modelo.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não foi possível fazer a deleção, erro de integridade de dados.");
            }
        }

        public async Task AtualizarAsync(Modelo obj)
        {
            bool existe = await _context.Modelo.AnyAsync(x => x.Id == obj.Id);
            if (!existe)
            {
                throw new NotFoundException("Modelo não encontrado.");
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
