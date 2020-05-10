using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkiVeiculos.Services;
using AkiVeiculos.Services.Exceptions;
using AkiVeiculos.Models;

namespace AkiVeiculos.Controllers
{
    public class AnunciosController : Controller
    {
        private readonly AnuncioService _anuncio;
        private readonly ModeloService _modelo;
        private readonly MarcaService _marca;
        private readonly UsuarioFilterService _usuario;



        public AnunciosController(AnuncioService anuncio, ModeloService modelo, MarcaService marca, UsuarioFilterService usuario)
        {
            _anuncio = anuncio;
            _modelo = modelo;
            _marca = marca;
            _usuario = usuario;
        }

        public async Task<IActionResult> Index(DateTime? minData, DateTime? maxData)
        {
            if (!minData.HasValue)
            {
                minData = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxData.HasValue)
            {
                maxData = DateTime.Now;
            }

            ViewData["minData"] = minData.Value.ToString("yyyy-MM-dd");
            ViewData["maxData"] = maxData.Value.ToString("yyyy-MM-dd");


            return View(await _anuncio.BuscaTodosAsync(minData.Value, maxData.Value));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _anuncio.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Anúncio não encontrado." });
            }

            return View(obj);
        }

        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Cadastrar()
        {

            var marcas = await _marca.BuscaTodasAsync();
            var modelos = await _modelo.BuscaTodosAsync(marcas[0].Id);

            var viewModel = new AnuncioFormViewModel { Marcas = marcas, Modelos = modelos };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Cadastrar(Anuncio anuncio)
        {

            await _anuncio.CriarAsync(anuncio);
            return RedirectToAction(nameof(Index));
        }

        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Excluir(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _anuncio.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Anúncio não encontrado." });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Excluir(int id)
        {

            try
            {
                await _anuncio.ApagarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Editar(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _anuncio.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Anúncio não encontrado." });
            }

            var marcas = await _marca.BuscaTodasAsync();
            var modelos = await _modelo.BuscaTodosAsync(obj.Modelo.MarcaId);

            var viewModel = new AnuncioFormViewModel { Anuncio = obj, Modelos = modelos, Marcas = marcas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UsuarioFilterService))]
        public async Task<IActionResult> Editar(int id, Anuncio anuncio)
        {

            if (id != anuncio.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Há incompatibilidade na requisição." });
            }
            try
            {
                await _anuncio.AtualizarAsync(anuncio);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }


    }
}
