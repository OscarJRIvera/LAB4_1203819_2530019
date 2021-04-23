using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LAB4_1203819_2530019.Models
{
    [Serializable]
    public class LlaveArbolPrioridad
    {
        public string CodigoHash { get; set; }
        public double Prioridad { get; set; }
        public static int Compare_Llave_Arbol(LlaveArbolPrioridad x, LlaveArbolPrioridad y)
        {
            int r = x.Prioridad.CompareTo(y.Prioridad);
            return r;
        }
    }
}
