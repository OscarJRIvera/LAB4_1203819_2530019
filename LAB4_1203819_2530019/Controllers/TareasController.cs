using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4_1203819_2530019.Data;
using LAB4_1203819_2530019.Models;
using DoubleLinkedListLibrary1;
using ArbolDePrioridad;
using Microsoft.VisualBasic.FileIO;
using System.IO;


namespace LAB4_1203819_2530019.Controllers
{
    public class TareasController : Controller
    {
        private Models.Data.Singleton F = Models.Data.Singleton.Instance;
        public static Tarea Temptarea = new Tarea();
        public DoubleLinkedList<Tarea> Viewlista = new DoubleLinkedList<Tarea>();
        public static DoubleLinkedList<Developer> DeveloperVista = new DoubleLinkedList<Developer>();
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
        public IActionResult Create()
        {
            return View(Temptarea);
        }
       
        public delegate int TareaD(Developer a, string b);
        public delegate int TareaD2(Tarea a, string b);
        public delegate int TareaD3(LlaveArbolPrioridad a, LlaveArbolPrioridad b);
        [HttpPost]
        public IActionResult Create([Bind("Id,Titulo,Descripcion,Proyecto,Prioridad,Fecha")] Tarea Tarea, [Bind("Prioridad")] LlaveArbolPrioridad ArbolModel)
        {
            Tarea.Developer = F.Developer;
            TareaD comparador = Tarea.Compare_Titulo2;
            TareaD2 comparador2 = Tarea.Compare_Titulo;
            ArbolModel.CodigoHash = Tarea.Titulo;
            Tarea Temp1 = F.Tabla_Hash.Remove2(ArbolModel.CodigoHash, ArbolModel.CodigoHash);
            if (Temp1 != null)
            {
                return RedirectToAction("ErrorTitulo");
            }
            Developer Temp = F.Tareas.Find(m => comparador(m, F.Developer) == 0);
            int posicion = F.Tareas.Find2(m => comparador(m, F.Developer) == 0);
            Temp.Tarea.add(ArbolModel);
            F.Tareas.replace(Temp, posicion);
            F.Tabla_Hash.Add(Tarea.Titulo, Tarea);
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;

            Escribir(R + "\\Dev.txt", EscribirDatos());
            return RedirectToAction("Developer");
        }
        public void Escribir(string Archivo, string Texto)
        {
            using (StreamWriter Esc = new StreamWriter(Archivo, false))
            {
                Esc.WriteAsync(Texto);
                Esc.Flush();
            }
        }
        // Persistir informacion de los developers
        public string EscribirDatos()
        {
            string st = "";
            for (int i = 1; i <= F.Tareas.Count2(); i++)
            {
                var TempListaDev = new DoubleLinkedList<Developer>();
                TempListaDev = F.Tareas.Clone() as DoubleLinkedList<Developer>;
                var Temp = new Developer();
                Temp = TempListaDev.GetbyIndex(i);
                st = st + Temp.Id + "," + Temp.Name + "\n";
                var Temp2 = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
                Temp2 = Temp.Tarea.Clone();
                while (!Temp2.isempty())
                {
                    var Temp3 = new LlaveArbolPrioridad();
                    Temp3 = Temp2.Remove();
                    st = st + Temp3.CodigoHash + "," + Temp3.Prioridad + "\n";
                }
            }
            return st;
        }
        // Persistir informacion del hash
        public string EscribirDatos2()
        {
            string st = "";
            for (int i = 0; i < F.Tabla_Hash.getsize(); i++)
            {
                for (int f = 0; f < F.Tabla_Hash.BuscarCanti(i); i++)
                {

                }
            }
            return st;
        } 
       
        public IActionResult Developer(int id)
        {
            int tempid = F.actualid;
            if (id != 0 && id != -1)
            {
                tempid = id;
                F.actualid = id;
            }


            if (tempid > DeveloperVista.Count())
            {

                Developer Temp = new Developer();
                Temp.Name = F.Developer;
                Temp.Id = F.id;
                F.Tareas.Add(Temp);
                DeveloperVista.Add(Temp);
                Temptarea.Developer = F.Developer;
                return View(Temptarea);
            }
            else
            {

                Developer Temp = F.Tareas.RemoveAt2(F.actualid);
                F.Developer = Temp.Name;
                Temptarea.Developer = F.Developer;
                return View(Temptarea);
            }
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
            TareaD comparador = Tarea.Compare_Titulo2;
            Developer Temp = F.Tareas.Find(m => comparador(m, F.Developer) == 0);

            if (Temp.Tarea.isempty())
            {
                return RedirectToAction("ErrorNull");
            }
            LlaveArbolPrioridad Primer = Temp.Tarea.Peek();
            Tarea Realizar = F.Tabla_Hash.Remove2(Primer.CodigoHash, Primer.CodigoHash);
            Realizar.Id = F.actualid;
            return View(Realizar);
        }
        public IActionResult DeveloperChoose()
        {
            return View(DeveloperVista);
        }
        public IActionResult DeveloperName()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeveloperName([Bind("Developer")] Tarea tarea)
        {
            TareaD comparador = Tarea.Compare_Titulo2;
            Developer Temp = F.Tareas.Find(m => comparador(m, tarea.Developer) == 0);
            if (Temp != null)
            {
                return RedirectToAction("ErrorNombre");
            }
            F.id += 1;
            F.actualid = F.id;
            F.Developer = tarea.Developer;
            return RedirectToAction("Developer");
        }
        public IActionResult ErrorNull()
        {
            return View();
        }
        public IActionResult ProjectManager()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int f = 1; f <= F.Tareas.Count2(); f++)
                {
                   var TempListaDev = new DoubleLinkedList<Developer>();
                   TempListaDev = F.Tareas.Clone() as DoubleLinkedList<Developer>;

                    var Temp = new Developer();
                    Temp = TempListaDev.GetbyIndex(f);
                    var Temp2 = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
                    if (!Temp.Tarea.isempty())
                    {
                        Temp2 = Temp.Tarea.Clone();
                        while (!Temp2.isempty())
                        {
                            var Temp3 = new LlaveArbolPrioridad();
                            Temp3 = Temp2.Remove();
                            var Temp4 = new Tarea();
                            Temp4 = F.Tabla_Hash.Remove2(Temp3.CodigoHash, Temp3.CodigoHash);
                            if (Temp4.Prioridad == i)
                            {
                                Viewlista.Add(Temp4);
                            }
                        }
                    }
                    
                }
            }


            return View(Viewlista);
        }
    }
}
