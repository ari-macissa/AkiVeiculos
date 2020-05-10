using AkiVeiculos.Models;
using AkiVeiculos.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AkiVeiculos.Services
{
    public class AnuncioService
    {
        private readonly AkiVeiculosContext _context;

        public AnuncioService(AkiVeiculosContext context)
        {
            _context = context;
        }

        public async Task<List<Anuncio>> BuscaTodosAsync(DateTime inicial, DateTime final)
        {
            return await _context.Anuncio.Where(x => x.DataVenda >= inicial && x.DataVenda <= final).Include(a => a.Modelo).OrderByDescending(x => x.DataVenda).ToListAsync();
        }

        public async Task<Anuncio> BuscaPorIdAsync(int id)
        {
            return await _context.Anuncio.Include(a => a.Modelo).ThenInclude(m => m.Marca).FirstOrDefaultAsync(obj => obj.Id == id);
        }


        public async Task CriarAsync(Anuncio obj)
        {
            var modelo = await _context.Modelo.FindAsync(obj.ModeloId);
            obj.Modelo = modelo;
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        public async Task ApagarAsync(int id)
        {
            var obj = await _context.Anuncio.FindAsync(id);
            try
            {
                _context.Anuncio.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não foi possível fazer a deleção, erro de integridade de dados.");
            }
        }

        public async Task AtualizarAsync(Anuncio obj)
        {
            bool existe = await _context.Anuncio.AnyAsync(x => x.Id == obj.Id);
            if (!existe)
            {
                throw new NotFoundException("Anúncio não encontrado.");
            }
            try
            {
                var modelo = await _context.Modelo.FindAsync(obj.ModeloId);
                obj.Modelo = modelo;
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
