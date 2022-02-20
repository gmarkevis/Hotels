using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotels.Models;
using Hotels.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Hotels.Controllers
{
    public class QuartoController : Controller
    {
        private readonly HotelsContext _context;

        public QuartoController(HotelsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hotelsContext = _context.Quarto.Include(q => q.Hotel);
            return View(await hotelsContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto
                .Include(q => q.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuartoVM quartoVM)
        {
            if (ModelState.IsValid)
            {
                Quarto quarto = new Quarto()
                {
                    HotelId = quartoVM.HotelId,
                    Nome = quartoVM.Nome,
                    NumeroOcupantes = quartoVM.NumeroOcupantes,
                    NumeroAdultos = quartoVM.NumeroAdultos,
                    NumeroCriancas = quartoVM.NumeroCriancas,
                    Preco = quartoVM.Preco
                };

                if (quartoVM.Foto != null)
                    quarto.Foto = ConverterFotoParaBytes(quartoVM.Foto);

                _context.Add(quarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Nome", quartoVM.HotelId);
            return View(quartoVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto.FindAsync(id);

            if (quarto == null)
            {
                return NotFound();
            }

            QuartoVM quartoVM = new QuartoVM()
            {
                Id = quarto.Id,
                HotelId = quarto.HotelId,
                Nome = quarto.Nome,
                NumeroOcupantes = quarto.NumeroOcupantes,
                NumeroAdultos = quarto.NumeroAdultos,
                NumeroCriancas = quarto.NumeroCriancas,
                Preco = quarto.Preco
            };

            ViewBag.FotoAtual = quarto.Foto;

            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Nome", quartoVM.HotelId);
            return View(quartoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuartoVM quartoVM)
        {
            if (id != quartoVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Quarto quarto = new Quarto()
                    {
                        Id = quartoVM.Id,
                        HotelId = quartoVM.HotelId,
                        Nome = quartoVM.Nome,
                        NumeroOcupantes = quartoVM.NumeroOcupantes,
                        NumeroAdultos = quartoVM.NumeroAdultos,
                        NumeroCriancas = quartoVM.NumeroCriancas,
                        Preco = quartoVM.Preco
                    };

                    if (quartoVM.Foto == null)
                        quarto.Foto = _context.Quarto.Where(q => q.Id == id).Select(q => q.Foto).FirstOrDefault();
                    else
                        quarto.Foto = ConverterFotoParaBytes(quartoVM.Foto);

                    _context.Update(quarto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartoExists(quartoVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Nome", quartoVM.HotelId);
            return View(quartoVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto
                .Include(q => q.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quarto = await _context.Quarto.FindAsync(id);
            _context.Quarto.Remove(quarto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuartoExists(int id)
        {
            return _context.Quarto.Any(e => e.Id == id);
        }

        public byte[] ConverterFotoParaBytes(IFormFile foto)
        {
            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
