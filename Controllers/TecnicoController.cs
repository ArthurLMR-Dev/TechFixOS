using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFixOS.Models.Entidades;
using TechFixOS.Models.Negocio;

namespace TechFixOS.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TecnicoController : Controller
    {
        private readonly TecnicoNegocio _negocio;

        public TecnicoController(TecnicoNegocio negocio)
        {
            _negocio = negocio;
        }

        // GET: Tecnico
        public async Task<IActionResult> Index()
        {
            var tecnicos = await _negocio.ListarAtivosAsync();
            return View(tecnicos);
        }

        // GET: Tecnico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                await _negocio.AdicionarAsync(tecnico);
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        // GET: Tecnico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tecnico = await _negocio.ObterPorIdAsync(id.Value);
            if (tecnico == null || !tecnico.Ativo) return NotFound();

            return View(tecnico);
        }

        // POST: Tecnico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tecnico tecnico)
        {
            if (id != tecnico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _negocio.AtualizarAsync(tecnico);
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        // POST: Tecnico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _negocio.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
