using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Models.Negocio
{
    public class OrdemServicoNegocio
    {
        private readonly AppDbContext _context;

        public OrdemServicoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdemServico>> ListarTodasAsync()
        {
            return await _context.OrdensServico
                .Include(o => o.Cliente)
                .Include(o => o.Tecnico)
                .Include(o => o.Servico)
                .OrderByDescending(o => o.DataAbertura)
                .ToListAsync();
        }

        public async Task<OrdemServico?> ObterPorIdAsync(int id)
        {
            return await _context.OrdensServico
                .Include(o => o.Cliente)
                .Include(o => o.Tecnico)
                .Include(o => o.Servico)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AdicionarAsync(OrdemServico ordemServico)
        {
            ordemServico.DataAbertura = DateTime.UtcNow;
            _context.OrdensServico.Add(ordemServico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(OrdemServico ordemServico)
        {
            ordemServico.DataAbertura = DateTime.SpecifyKind(ordemServico.DataAbertura, DateTimeKind.Utc);
            _context.OrdensServico.Update(ordemServico);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var os = await _context.OrdensServico.FindAsync(id);
            if (os != null)
            {
                _context.OrdensServico.Remove(os);
                await _context.SaveChangesAsync();
            }
        }
    }
}
