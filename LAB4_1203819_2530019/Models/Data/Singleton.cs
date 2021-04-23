using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbolDePrioridad;
using TablaHash;
using DoubleLinkedListLibrary1;
using System.IO;
using Newtonsoft;
namespace LAB4_1203819_2530019.Models.Data
{

    public sealed class Singleton
    {
        public static string GetFolder()
        {
            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = "LAB04";
            return mydocs + '\\' + folder;
        }

        private readonly static Singleton _instance = new Singleton();
        public bool? Type;
        public bool? ActionDeveloper;
        public TablaHash<string, Tarea> Tabla_Hash;
        public string Developer;
        public DoubleLinkedList<Developer> Tareas;
        public int id = 0;
        public int actualid = 0;

        public Singleton()
        {

            Tabla_Hash = new TablaHash<String, Tarea>(20, Tarea.Compare_Titulo);
            Tareas = new DoubleLinkedList<Developer>();
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
