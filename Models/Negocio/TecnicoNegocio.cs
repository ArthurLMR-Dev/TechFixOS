using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Models.Negocio
{
    public class TecnicoNegocio
    {
        private readonly AppDbContext _context;

        public TecnicoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tecnico>> ListarAtivosAsync()
        {
            return await _context.Tecnicos
                .Where(t => t.Ativo)
                .OrderBy(t => t.Nome)
                .ToListAsync();
        }

        public async Task<Tecnico?> ObterPorIdAsync(int id)
        {
            return await _context.Tecnicos.FindAsync(id);
        }

        public async Task AdicionarAsync(Tecnico tecnico)
        {
            tecnico.Ativo = true;
            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Tecnico tecnico)
        {
            _context.Tecnicos.Update(tecnico);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico != null)
            {
                tecnico.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
