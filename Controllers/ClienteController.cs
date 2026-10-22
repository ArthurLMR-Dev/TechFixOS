using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFixOS.Models.Entidades;
using TechFixOS.Models.Negocio;

namespace TechFixOS.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteNegocio _negocio;

        public ClienteController(ClienteNegocio negocio)
        {
            _negocio = negocio;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            var clientes = await _negocio.ListarAtivosAsync();
            return View(clientes);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _negocio.AdicionarAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _negocio.ObterPorIdAsync(id.Value);
            if (cliente == null || !cliente.Ativo) return NotFound();

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _negocio.AtualizarAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _negocio.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
