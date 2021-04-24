using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ArbolDePrioridad;
namespace LAB4_1203819_2530019.Models
{
    [Serializable]
    public class Developer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ArbolDePrioridad<LlaveArbolPrioridad> Tarea = new ArbolDePrioridad<LlaveArbolPrioridad>(LlaveArbolPrioridad.Compare_Llave_Arbol);


    }
}
