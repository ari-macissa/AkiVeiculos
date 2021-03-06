﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AkiVeiculos.Services;
using AkiVeiculos.Services.Exceptions;
using AkiVeiculos.Models;

namespace AkiVeiculos.Controllers
{
    [Authorize]
    public class ModelosController : Controller
    {
        private readonly ModeloService _modelo;
        private MarcaService _marca;

        public ModelosController(ModeloService modelo, MarcaService marca)
        {
            _modelo = modelo;
            _marca = marca;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelo.BuscaTodosAsync(null));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _modelo.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Modelo não encontrado." });
            }

            return View(obj);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var marcas = await _marca.BuscaTodasAsync();

            var viewModel = new ModeloFormViewModel { Marcas = marcas };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Modelo modelo)
        {
            await _modelo.CriarAsync(modelo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _modelo.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Modelo não encontrado." });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _modelo.ApagarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Editar(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Problema na URL: id não foi fornecido." });
            }

            var obj = await _modelo.BuscaPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Modelo não encontrado." });
            }

            var marcas = await _marca.BuscaTodasAsync();

            var viewModel = new ModeloFormViewModel { Modelo = obj, Marcas = marcas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Modelo modelo)
        {
            if (id != modelo.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Há incompatibilidade na requisição." });
            }
            try
            {
                await _modelo.AtualizarAsync(modelo);
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
