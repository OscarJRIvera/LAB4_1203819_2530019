using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4_1203819_2530019.Data;
using LAB4_1203819_2530019.Models;
using ArbolDePrioridad;
using DoubleLinkedListLibrary1;

namespace LAB4_1203819_2530019.Controllers
{
    public class TareasController : Controller
    {
        public static Tarea Temptarea = new Tarea();
        public static ArbolDePrioridad<LlaveArbolPrioridad> Arbolview;
        public static DoubleLinkedList<Tarea> Viewlista = new DoubleLinkedList<Tarea>();
        private readonly Models.Data.Singleton F = Models.Data.Singleton.Instance;
        private readonly LAB4_1203819_2530019Context _context;

        public TareasController(LAB4_1203819_2530019Context context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarea.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            return View(Temptarea);
        }
        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([Bind("Id,Titulo,Descripcion,Proyecto,Prioridad,Dia,Mes,Año")] Tarea Tarea, [Bind("Prioridad")] LlaveArbolPrioridad ArbolModel)
        {
            Tarea.Developer = Temptarea.Developer;
            ArbolModel.CodigoHash = Tarea.Titulo;
            F.Arbol_Prioridad.add(ArbolModel);
            F.Tabla_Hash.Add(Tarea.Titulo, Tarea);
            return View("Developer");
        }
        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Developer,Titulo,Descripcion,Proyecto,Prioridad,Dia,Mes,Año")] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
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
            return View();
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tarea.FindAsync(id);
            _context.Tarea.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tarea.Any(e => e.Id == id);
        }
        public IActionResult Developer(int id)
        {
            Temptarea.Developer = F.Developer;
            return View(Temptarea);
        }
        public IActionResult OptionList(int id)
        {
            Models.Data.Singleton.Instance.ActionDeveloper = id == 0 ? false : true;
            if (Models.Data.Singleton.Instance.ActionDeveloper == false)
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("Consulta"); ;

        }
        public IActionResult Consulta()
        {
            if (F.Arbol_Prioridad.isempty())
            {
                return RedirectToAction("ErrorNull");
            }
            LlaveArbolPrioridad Primer = F.Arbol_Prioridad.Peek();
            Tarea Realizar = F.Tabla_Hash.Remove2(Primer.CodigoHash, Primer.CodigoHash);


            return View(Realizar);
        }
        public IActionResult DeveloperName()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeveloperName([Bind("Developer")] Tarea tarea)
        {
            F.Developer = tarea.Developer;
            return RedirectToAction("Developer");
        }
        public IActionResult ErrorNull()
        {
            return View();
        }
        public IActionResult ProjectManager()
        {
            Arbolview = F.Arbol_Prioridad;
            while (!Arbolview.isempty())
            {
                LlaveArbolPrioridad Temp = Arbolview.Remove();
                Tarea Temp2 = F.Tabla_Hash.Remove2(Temp.CodigoHash, Temp.CodigoHash);
                Viewlista.Add(Temp2);
            }
            return View(Viewlista);
        }
    }
}
