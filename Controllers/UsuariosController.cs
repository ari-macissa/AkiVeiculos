using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AkiVeiculos.Services;
using AkiVeiculos.Models;

namespace AkiVeiculos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AkiVeiculosContext _context;

        public UsuariosController(AkiVeiculosContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario obj)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Login == obj.Login && x.Senha == obj.Senha);
            if (usuario != null)
            {
                HttpContext.Session.SetInt32("Id", usuario.Id);
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Login));
        }

    }
}
