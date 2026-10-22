using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Models.Negocio
{
    public class ClienteNegocio
    {
        private readonly AppDbContext _context;

        public ClienteNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ListarAtivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            cliente.Ativo = true;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
