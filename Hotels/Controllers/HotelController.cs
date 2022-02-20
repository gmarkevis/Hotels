using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotels.Models;
using Hotels.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hotels.Controllers
{
    public class HotelController : Controller
    {
        private readonly HotelsContext _context;

        public HotelController(HotelsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotel.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelVM hotelVM)
        {

            if (ModelState.IsValid)
            {
                Hotel hotel = new Hotel();
                hotel.Nome = hotelVM.Nome;
                hotel.CNPJ = hotelVM.CNPJ;
                hotel.Endereco = hotelVM.Endereco;
                hotel.Descricao = hotelVM.Descricao;

                if (hotelVM.Foto != null)
                    hotel.Foto = ConverterFotoParaBytes(hotelVM.Foto);

                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            HotelVM hotelVM = new HotelVM()
            {
                Id = hotel.Id,
                Nome = hotel.Nome,
                CNPJ = hotel.CNPJ,
                Endereco = hotel.Endereco,
                Descricao = hotel.Descricao
            };

            ViewBag.FotoAtual = hotel.Foto;

            return View(hotelVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HotelVM hotelVM)
        {
            if (id != hotelVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Hotel hotel = new Hotel()
                    {
                        Id = hotelVM.Id,
                        Nome = hotelVM.Nome,
                        CNPJ = hotelVM.CNPJ,
                        Endereco = hotelVM.Endereco,
                        Descricao = hotelVM.Descricao
                    };
                    if (hotelVM.Foto == null)
                        hotel.Foto = _context.Hotel.Where(h => h.Id == id).Select(h => h.Foto).FirstOrDefault();
                    else
                        hotel.Foto = ConverterFotoParaBytes(hotelVM.Foto);
                    

                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotelVM.Id))
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
            return View(hotelVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.Id == id);
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
