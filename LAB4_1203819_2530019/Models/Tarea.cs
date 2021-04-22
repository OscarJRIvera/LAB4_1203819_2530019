using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LAB4_1203819_2530019.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Proyecto { get; set; }
        [Required]
        public int Prioridad { get; set; }
        [Required]
        public int Dia { get; set; }
        [Required]
        public int Mes { get; set; }
        [Required]
        public int Año { get; set; }
        public static int Compare_Titulo(Tarea x, String y)
        {
            int r = x.Titulo.CompareTo(y);
            return r;
        }

    }
}
