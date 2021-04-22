using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbolDePrioridad;
using TablaHash;
using DoubleLinkedListLibrary1;
namespace LAB4_1203819_2530019.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public bool? Type;
        public bool? ActionDeveloper;
        public ArbolDePrioridad<LlaveArbolPrioridad> Arbol_Prioridad;
        public TablaHash<string, Tarea> Tabla_Hash;
        public string Developer;
        public DoubleLinkedList<ArbolDePrioridad<LlaveArbolPrioridad>> Tareas;
        private Singleton()
        {
            Tabla_Hash = new TablaHash<String, Tarea>(20, Tarea.Compare_Titulo);
            Arbol_Prioridad = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);
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
