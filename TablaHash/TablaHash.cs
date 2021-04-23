using System;
using DoubleLinkedListLibrary1;
using System.Text;
using System.IO;
using Newtonsoft;

namespace TablaHash
{
    public class TablaHash<K, V>
    {
        int largoTabla;
        int funcionHash(K llave)
        {
            if (llave is String)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(llave.ToString());
                long contador = 0;
                foreach (var item in bytes)
                {
                    contador += Convert.ToInt64(item);
                }
                return Convert.ToInt32(contador) % largoTabla;
            }
            return llave.GetHashCode() % largoTabla;
        }
        DoubleLinkedList<LlaveValor<V>> Diccionario;


        public TablaHash(int count, Comparador<V> Funcomparador) //se inicializa el diccionario con una cantidad fija de elementos
        {

            this.comparador = Funcomparador;
            Diccionario = new DoubleLinkedList<LlaveValor<V>>();
            largoTabla = count;
            for (int i = 0; i < largoTabla; i++)
            {
                Diccionario.Add(new LlaveValor<V>(i));
            }
        }
        internal Comparador<V> comparador;
        public delegate int Comparador<v>(V a, string b);

        public void Add(K llave, V valor)
        {
            var hash = funcionHash(llave);
            var llaveValor = Diccionario.Find(p => p.Llave.Equals(hash));//busca la posición en la que se va a agregar
            llaveValor.Valor.Add(valor);

        }
        public void Remove(K llave, string Titulo)
        {
            var hash = funcionHash(llave);
            var llaveValor = Diccionario.Find(p => p.Llave.Equals(hash));
            //busca la posición en la que se va a agregar
            int posicion = llaveValor.Valor.Find2(m => comparador(m, Titulo) == 0);
            llaveValor.Valor.RemoveAt(posicion);


        }
        public V Remove2(K llave, string Titulo)
        {
            var hash = funcionHash(llave);
            var llaveValor = Diccionario.Find(p => p.Llave.Equals(hash));
            //busca la posición en la que se va a agregar
            var I = llaveValor.Valor.Find(m => comparador(m, Titulo) == 0);
            return I;
        }
    }
}
