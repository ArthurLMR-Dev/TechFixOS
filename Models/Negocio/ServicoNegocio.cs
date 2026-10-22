using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Models.Negocio
{
    public class ServicoNegocio
    {
        private readonly AppDbContext _context;

        public ServicoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servico>> ListarAtivosAsync()
        {
            return await _context.Servicos
                .Where(s => s.Ativo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<Servico?> ObterPorIdAsync(int id)
        {
            return await _context.Servicos.FindAsync(id);
        }

        public async Task AdicionarAsync(Servico servico)
        {
            servico.Ativo = true;
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Servico servico)
        {
            _context.Servicos.Update(servico);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                servico.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
