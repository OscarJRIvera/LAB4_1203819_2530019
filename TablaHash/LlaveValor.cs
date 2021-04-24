using System;
using System.Collections.Generic;
using System.Text;
using DoubleLinkedListLibrary1;

namespace TablaHash
{
    class LlaveValor<V>
    {
        public int Llave { get; private set; } //el private set para evitar que se modifique desde afuera
        public DoubleLinkedList<V> Valor { get; private set; }//para que no se inicialice desde otro lugar

        public LlaveValor(int llave)
        {
            Llave = llave;
            Valor = new DoubleLinkedList<V>();
        }

    }
}
