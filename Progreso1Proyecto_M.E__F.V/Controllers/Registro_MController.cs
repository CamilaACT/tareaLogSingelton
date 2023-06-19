using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Progreso1Proyecto_M.E__F.V.Data;
using Progreso1Proyecto_M.E__F.V.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Progreso1Proyecto_M.E__F.V.Controllers
{
    public class Registro_MController : Controller
    {
        private readonly Progreso1Proyecto_ME__FVContext _context;

        Log logger = Log.Instance;

        public Registro_MController(Progreso1Proyecto_ME__FVContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Cerrar el archivo de registro
                logger.CloseLogFile();
            }
            base.Dispose(disposing);
        }

        public class LogActionFilter : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext context)
            {
                Log logger = Log.Instance;
                logger.CloseLogFile();
                base.OnActionExecuted(context);
            }
        }

        // GET: Registro_M
        public async Task<IActionResult> Index(string buscar, int filtro)
        {
            var registro_M = from Registro_M in _context.Registro_M select Registro_M;

            if (!String.IsNullOrEmpty(buscar))
            {
                registro_M = registro_M.Where(s => s.Materia!.Contains(buscar));
            }

            if (filtro != 0)
            {
                registro_M = registro_M.Where(s => s.Calificacion == filtro);
                ViewData["FiltroCalificacion"] = filtro;
            }
            else
            {
                ViewData["FiltroCalificacion"] = null;
            }
            registro_M = registro_M.OrderByDescending(s => s.Calificacion);

            return View(await registro_M.ToListAsync());
        }

        // GET: Registro_M/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registro_M == null)
            {
                return NotFound();
            }

            var registro_M = await _context.Registro_M
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registro_M == null)
            {
                return NotFound();
            }

            return View(registro_M);
        }

        [Authorize]
        // GET: Registro_M/Create
        public IActionResult Create()
        {
            logger.LogButtonClick("Crear");
            return View();
        }

        // POST: Registro_M/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Semestre,Materia,Profesor,Calificacion,Descripcion,Cualidad,Horario")] Registro_M registro_M)
        {
            logger.LogButtonClick("Registrar");
            if (ModelState.IsValid)
            {
                _context.Add(registro_M);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registro_M);
        }

        // GET: Registro_M/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            logger.LogButtonClick("Editar");
            if (id == null || _context.Registro_M == null)
            {
                return NotFound();
            }

            var registro_M = await _context.Registro_M.FindAsync(id);
            if (registro_M == null)
            {
                return NotFound();
            }
            return View(registro_M);
        }

        // POST: Registro_M/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Semestre,Materia,Profesor,Calificacion,Descripcion,Cualidad,Horario")] Registro_M registro_M)
        {
            logger.LogButtonClick("Editar");
            if (id != registro_M.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registro_M);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Registro_MExists(registro_M.Id))
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
            return View(registro_M);
        }

        [Authorize]
        // GET: Registro_M/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            logger.LogButtonClick("Borrar");
            if (id == null || _context.Registro_M == null)
            {
                return NotFound();
            }

            var registro_M = await _context.Registro_M
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registro_M == null)
            {
                return NotFound();
            }

            return View(registro_M);
        }

        // POST: Registro_M/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            logger.LogButtonClick("Borrar");
            if (_context.Registro_M == null)
            {
                return Problem("Entity set 'Progreso1Proyecto_ME__FVContext.Registro_M' is null.");
            }
            var registro_M = await _context.Registro_M.FindAsync(id);
            if (registro_M != null)
            {
                _context.Registro_M.Remove(registro_M);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Registro_MExists(int id)
        {
            return (_context.Registro_M?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
