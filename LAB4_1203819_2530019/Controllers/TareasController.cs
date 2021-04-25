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
        public void Cargardatos()
        {
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;
            using (TextFieldParser Datos = new TextFieldParser(R + "\\Dev.txt"))
            {
                Datos.TextFieldType = FieldType.Delimited;
                Datos.SetDelimiters(",");
                Developer Temp = new Developer();
                Temp.Tarea = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
                while (!Datos.EndOfData)
                {
                    string[] Escrito = Datos.ReadFields();
                    if (Escrito[0] == "u")
                    {
                        F.Tareas.Add(Temp);
                        Temp = new Developer();
                        Temp.Tarea = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
                    }
                    else
                    {
                        if (Escrito[0] != "T")
                        {
                            Temp.Id = Convert.ToInt32(Escrito[0]);
                            Temp.Name = Escrito[1];
                        }
                        else
                        {
                            LlaveArbolPrioridad Temp2 = new LlaveArbolPrioridad();
                            Temp2.CodigoHash = Escrito[1];
                            Temp2.Prioridad = Convert.ToInt32(Escrito[2]);
                            Temp.Tarea.add(Temp2);
                        }
                    }
                }
                Datos.Close();

            }
            Cargardatos2();
        }
        public void Cargardatos2()
        {
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;
            using (TextFieldParser Datos = new TextFieldParser(R + "\\Tabla.txt"))
            {
                Datos.TextFieldType = FieldType.Delimited;
                Datos.SetDelimiters(",");
                while (!Datos.EndOfData)
                {
                    string[] Escrito = Datos.ReadFields();
                    Tarea Temp = new Tarea();
                    Temp.Id = Convert.ToInt32(Escrito[0]);
                    Temp.Developer = Escrito[1];
                    Temp.Titulo = Escrito[2];
                    Temp.Descripcion = Escrito[3];
                    Temp.Proyecto = Escrito[4];
                    Temp.Prioridad = Convert.ToInt32(Escrito[5]);
                    Temp.Fecha = Convert.ToDateTime(Escrito[6]);
                    F.Tabla_Hash.Add(Temp.Titulo, Temp);
                }
                Datos.Close();
            }

            Cargardatos3();
        }
        public void Cargardatos3()
        {
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;
            using (TextFieldParser Datos = new TextFieldParser(R + "\\subdatos.txt"))
            {
                Datos.TextFieldType = FieldType.Delimited;
                Datos.SetDelimiters(",");
                while (!Datos.EndOfData)
                {
                    string[] Escrito = Datos.ReadFields();
                    if (Escrito[0] == "u")
                    {
                        F.Developer = Escrito[1];
                        F.id = Convert.ToInt32(Escrito[2]);
                        F.actualid = Convert.ToInt32(Escrito[3]);
                    }
                    else if (Escrito[0] != "T")
                    {
                        Developer Temp = new Developer();
                        Temp.Name = Escrito[1];
                        Temp.Id = Convert.ToInt32(Escrito[0]);
                        DeveloperVista.Add(Temp);
                    }
                    else
                    {
                        Tarea Temp = new Tarea();
                        Temp.Id = Convert.ToInt32(Escrito[1]);
                        Temp.Developer = Escrito[2];
                        Temp.Titulo = Escrito[3];
                        Temp.Descripcion = Escrito[4];
                        Temp.Proyecto = Escrito[5];
                        Temp.Prioridad = Convert.ToInt32(Escrito[6]);
                        Temp.Fecha = Convert.ToDateTime(Escrito[7]);
                        Temptarea = Temp;
                    }
                }
                Datos.Close();
            }
        }
        public bool isnull()
        {
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;
            using (TextFieldParser Datos = new TextFieldParser(R + "\\Dev.txt"))
            {
                Datos.TextFieldType = FieldType.Delimited;
                Datos.SetDelimiters(",");
                string[] Escrito = Datos.ReadFields();
                if (Escrito == null)
                {
                    Datos.Close();
                    return true;
                }
                else
                {
                    Datos.Close();
                    return false;
                }
                Datos.Close();
            }
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
            Escribir(R + "\\Tabla.txt", EscribirDatos2());
            Escribir(R + "\\subdatos.txt", EscribirDatos3());
            return RedirectToAction("Developer");
        }
        public void Escribir(string Archivo, string Texto)
        {
            using (StreamWriter cerrar = new StreamWriter(Archivo, false))
            {

                cerrar.Close();
            }
            using (StreamWriter Esc = new StreamWriter(Archivo, false))
            {

                Esc.WriteAsync(Texto);
                Esc.Close();
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
                if (!Temp.Tarea.isempty())
                {
                    Temp2 = Temp.Tarea.Clone();
                    while (!Temp2.isempty())
                    {
                        var Temp3 = new LlaveArbolPrioridad();
                        Temp3 = Temp2.Remove();
                        st = st + "T" + "," + Temp3.CodigoHash + "," + Temp3.Prioridad + "\n";
                    }
                }
                st = st + "u" + "\n";
            }
            return st;
        }
        // Persistir informacion del hash
        public string EscribirDatos2()
        {
            string st = "";
            for (int i = 0; i < F.Tabla_Hash.getsize(); i++)
            {
                for (int f = 1; f <= F.Tabla_Hash.BuscarCanti(i); f++)
                {
                    var Temp4 = new Tarea();
                    Temp4 = F.Tabla_Hash.Remove3(i, f);
                    st = st + Temp4.Id + "," + Temp4.Developer + "," + Temp4.Titulo + "," + Temp4.Descripcion + "," + Temp4.Proyecto +
                    "," + Temp4.Prioridad + "," + Temp4.Fecha + "\n";
                }
            }
            return st;
        }
        public string EscribirDatos3()
        {

            string st = "";
            st = "u" + "," + F.Developer + "," + F.id + "," + F.actualid + "\n";
            var Temp = new Developer();
            for (int i = 1; i <= DeveloperVista.Count2(); i++)
            {
                Temp = DeveloperVista.RemoveAt2(i);
                st = st + Temp.Id + "," + Temp.Name + "\n";
            }
            st = st + "T" + "," + Temptarea.Id + "," + Temptarea.Developer + "," + Temptarea.Titulo + "," + Temptarea.Descripcion + "," + Temptarea.Proyecto +
                   "," + Temptarea.Prioridad + "," + Temptarea.Fecha;



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
            if (!isnull() && F.CargarD == false && F.CargarD2 == false)
            {
                F.CargarD = true;
                Cargardatos();
                return RedirectToAction("DeveloperChoose");
            }
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
            F.CargarD2 = true;
            return RedirectToAction("Developer");
        }
        public IActionResult ErrorNull()
        {
            return View();
        }
        public IActionResult login()
        {
            if (!isnull() && F.CargarD == false && F.CargarD2 == false)
            {
                F.CargarD = true;
                Cargardatos();
            }
            return View();
        }
        [HttpPost]
        public IActionResult login([Bind("Usuario,Contreseña")] Login login)
        {
            if (login.Usuario == "Ing.Miguel" && login.Contreseña == "Devele1")
            {
                return RedirectToAction("ProjectManager");
            }
            return RedirectToAction("ErrorLogin");

        }
        public IActionResult ErrorLogin()
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
        public IActionResult ErrorTitulo()
        {
            return View();
        }
        public IActionResult ErrorNombre()
        {
            return View();
        }
        public IActionResult EliminarDeveloper(int id)
        {
            var TempListaDev = new DoubleLinkedList<Developer>();
            TempListaDev = F.Tareas.Clone() as DoubleLinkedList<Developer>;
            var Temp = new Developer();
            Temp = TempListaDev.GetbyIndex(id);
            var Temp2 = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
            Temp2 = Temp.Tarea.Clone();
            var Temp3 = new LlaveArbolPrioridad();
            Temp3 = Temp2.Remove();
            F.Tabla_Hash.Remove(Temp3.CodigoHash, Temp3.CodigoHash);
            var TempListaDev2 = F.Tareas;
            var TempB = TempListaDev2.GetbyIndex(id);
            TempB.Tarea.Remove();
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            string R = mydocs + '\\' + folder;
            Escribir(R + "\\Dev.txt", EscribirDatos());
            Escribir(R + "\\Tabla.txt", EscribirDatos2());
            Escribir(R + "\\subdatos.txt", EscribirDatos3());
            return RedirectToAction("Developer");
        }

    }
}
