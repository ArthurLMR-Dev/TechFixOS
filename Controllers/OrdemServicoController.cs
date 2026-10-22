using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechFixOS.Models.Entidades;
using TechFixOS.Models.Negocio;

namespace TechFixOS.Controllers
{
    [Authorize]
    public class OrdemServicoController : Controller
    {
        private readonly OrdemServicoNegocio _negocio;
        private readonly ClienteNegocio _clienteNegocio;
        private readonly TecnicoNegocio _tecnicoNegocio;
        private readonly ServicoNegocio _servicoNegocio;

        public OrdemServicoController(
            OrdemServicoNegocio negocio,
            ClienteNegocio clienteNegocio,
            TecnicoNegocio tecnicoNegocio,
            ServicoNegocio servicoNegocio)
        {
            _negocio = negocio;
            _clienteNegocio = clienteNegocio;
            _tecnicoNegocio = tecnicoNegocio;
            _servicoNegocio = servicoNegocio;
        }

        // GET: OrdemServico
        public async Task<IActionResult> Index()
        {
            var ordens = await _negocio.ListarTodasAsync();
            return View(ordens);
        }

        // GET: OrdemServico/Create
        public async Task<IActionResult> Create()
        {
            await CarregarDropDownsAsync();
            return View();
        }

        // POST: OrdemServico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdemServico ordemServico)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Tecnico");
            ModelState.Remove("Servico");

            if (ModelState.IsValid)
            {
                var servico = await _servicoNegocio.ObterPorIdAsync(ordemServico.ServicoId);
                if (servico == null)
                {
                    ModelState.AddModelError("ServicoId", "Serviço não encontrado.");
                    await CarregarDropDownsAsync(ordemServico);
                    return View(ordemServico);
                }

                ordemServico.ValorTotal = servico.Valor;
                await _negocio.AdicionarAsync(ordemServico);
                return RedirectToAction(nameof(Index));
            }

            await CarregarDropDownsAsync(ordemServico);
            return View(ordemServico);
        }

        // GET: OrdemServico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ordemServico = await _negocio.ObterPorIdAsync(id.Value);
            if (ordemServico == null) return NotFound();

            await CarregarDropDownsAsync(ordemServico);
            return View(ordemServico);
        }

        // POST: OrdemServico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrdemServico ordemServico)
        {
            if (id != ordemServico.Id) return NotFound();

            ModelState.Remove("Cliente");
            ModelState.Remove("Tecnico");
            ModelState.Remove("Servico");

            if (ModelState.IsValid)
            {
                await _negocio.AtualizarAsync(ordemServico);
                return RedirectToAction(nameof(Index));
            }

            await CarregarDropDownsAsync(ordemServico);
            return View(ordemServico);
        }

        // POST: OrdemServico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _negocio.ExcluirAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarDropDownsAsync(OrdemServico? ordemServico = null)
        {
            var clientes = await _clienteNegocio.ListarAtivosAsync();
            var tecnicos = await _tecnicoNegocio.ListarAtivosAsync();
            var servicos = await _servicoNegocio.ListarAtivosAsync();

            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nome", ordemServico?.ClienteId);
            ViewBag.TecnicoId = new SelectList(tecnicos, "Id", "Nome", ordemServico?.TecnicoId);
            ViewBag.ServicoId = new SelectList(servicos, "Id", "Nome", ordemServico?.ServicoId);
        }
    }
}
