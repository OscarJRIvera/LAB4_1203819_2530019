using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolBinario
{
    public class Nodo<T>
    {
        public T Value;

        public Nodo<T> Left;
        public Nodo<T> Right;
      
        public Nodo()
        {
            this.Left = null;
            this.Right = null;
        }

        public bool EsHoja
        {
            get
            {
                return Left is null && Right is null;
            }
        }
        public bool TieneDosHijos
        {
            get
            {
                return Left != null && Right != null;
            }
        }

    }
}
