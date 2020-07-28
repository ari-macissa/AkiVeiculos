using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkiVeiculos.Services;
using AkiVeiculos.Services.Exceptions;
using AkiVeiculos.Models;

namespace AkiVeiculos.Controllers
{
    [Authorize]
    public class ApiController : Controller
    {
        private readonly AnuncioService _anuncio;
        private readonly ModeloService _modelo;
        private readonly MarcaService _marca;

        public ApiController(AnuncioService anuncio, ModeloService modelo, MarcaService marca)
        {
            _anuncio = anuncio;
            _modelo = modelo;
            _marca = marca;
        }

        [HttpGet("api/anuncio/{id}")]
        public async Task<JsonResult> Anuncio(int id)
        {
            return Json(await _anuncio.BuscaPorIdAsync(id));
        }

        [HttpGet]
        public async Task<JsonResult> Modelos(int id)
        {
            return Json(await _modelo.BuscaTodosAsync(id));
        }

    }
}