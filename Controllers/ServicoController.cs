using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFixOS.Models.Entidades;
using TechFixOS.Models.Negocio;

namespace TechFixOS.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ServicoController : Controller
    {
        private readonly ServicoNegocio _negocio;

        public ServicoController(ServicoNegocio negocio)
        {
            _negocio = negocio;
        }

        // GET: Servico
        public async Task<IActionResult> Index()
        {
            var servicos = await _negocio.ListarAtivosAsync();
            return View(servicos);
        }

        // GET: Servico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico)
        {
            if (ModelState.IsValid)
            {
                await _negocio.AdicionarAsync(servico);
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        // GET: Servico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _negocio.ObterPorIdAsync(id.Value);
            if (servico == null || !servico.Ativo) return NotFound();

            return View(servico);
        }

        // POST: Servico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {
            if (id != servico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _negocio.AtualizarAsync(servico);
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _negocio.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
