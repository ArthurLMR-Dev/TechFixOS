using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TechFixOS.Data;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Models.Negocio
{
    public class UsuarioNegocio
    {
        private readonly AppDbContext _context;

        public UsuarioNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> AutenticarAsync(string email, string senha)
        {
            var senhaHash = GerarHashSha256(senha);

            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senhaHash);
        }

        public async Task<bool> EmailJaCadastradoAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            usuario.Senha = GerarHashSha256(usuario.Senha);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios
                .OrderBy(u => u.Nome)
                .ToListAsync();
        }

        public static string GerarHashSha256(string texto)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
