using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListLibrary1
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        Node<T> start;
        Node<T> end;
        int count;
        int eleminados;
        public DoubleLinkedList() //Esta es la funcion
        {
            start = null;
            end = null;
            count = 0;
            eleminados = 0;
        }
        public DoubleLinkedList(Comparador<T> Funcomparador) 
        {
            start = null;
            end = null;
            count = 0;
            eleminados = 0;
            this.comparador = Funcomparador; 
        }
        internal Comparador<T> comparador;
        public delegate int Comparador<T>(T a, T b);
        bool IsEmpty()
        {
            return count == 0;
        }

        public void Add(T dato)
        {
            Node<T> new_node = new Node<T>();
            new_node.Value = dato;

            if (IsEmpty())
            {
                start = new_node;
                end = new_node;
            }
            else
            {
                end.next = new_node;
                new_node.Behind = end;
                end = new_node;
            }

            count++;
            return;
        }

        public void RemoveAt(int index)
        {
            Node<T> actual;
            Node<T> anterior = null;
            actual = start;

            int i = 1;
            while (actual != null && i < index - eleminados)
            {
                anterior = actual;
                actual = actual.next;
                i++;
            }
            if (actual == start)
            {
                start = start.next;
                start.Behind = null;
                eleminados++;
            }
            else if (actual == end)
            {
                end = end.Behind;
                end.next = null;
                eleminados++;
            }
            else
            {
                actual.next = actual.next.next;
                actual.next.Behind = actual;
                eleminados++;
            }
        }
        public T GetbyIndex(int index)
        {
            Node<T> actual;
            actual = start;
            int cuantos = Count();
            int i = 1;
            while (actual != null && i < index)
            {
                actual = actual.next;
                i++;
            }
            if (actual is null) throw new IndexOutOfRangeException();
            return actual.Value;
        }
        public void Posi(int index, T model)
        {
            Node<T> new_node = new Node<T>();
            new_node.Value = model;
            Node<T> actual;

            Node<T> enfrente = null;

            if (index == 1)
            {
                actual = start;
                enfrente = actual.next;
                start = new_node;
                start.next = enfrente;
                start.next.Behind = actual;
                start.next.next = enfrente.next;
            }
            else
            {
                actual = start;
                for (int i = 2; i < index; i++)
                {

                    actual = actual.next;

                }
                if (actual.next.next != null)
                {
                    enfrente = actual.next.next;
                    actual.next = new_node;
                    actual.next.Behind = actual;
                    actual.next.next = enfrente;
                    enfrente.Behind = actual.next;
                }
                else
                {
                    actual.next = new_node;
                    actual.next.Behind = actual;
                    actual.next.next = enfrente;
                }
            }


        }
        public T Find(Predicate<T> match)
        {
            Node<T> actual;
            actual = start;
            int i = 1;
            while (actual != null)
            {
                if (match.Invoke(actual.Value)) //match es un delegado que verifica una condición
                {
                    return actual.Value;
                }
                actual = actual.next;
                i++;
            }
            return default(T);
        }
        public int Find2(Predicate<T> match)
        {
            Node<T> actual;
            actual = start;
            int i = 0;
            while (actual != null)
            {
                if (!match.Invoke(actual.Value)) //match es un delegado que verifica una condición
                {
                    
                    i++;
                }
                actual = actual.next;


            }
            return i;
        }

        public DoubleLinkedList<T> FindAll(Predicate<T> match)
        {
            DoubleLinkedList<T> resultados = new DoubleLinkedList<T>();
            Node<T> actual;
            actual = start;
            int i = 1;
            while (actual != null)
            {
                if (match.Invoke(actual.Value)) //match es un delegado que verifica una condición
                {
                    resultados.Add(actual.Value);
                }
                actual = actual.next;
                i++;
            }
            return resultados;
        }

        public int Count()
        {

            return count;
        }
        public int Count2()
        {
            return count - eleminados;
        }

        public int EleCount()
        {

            return eleminados;
        }
        public void eleiminartodo()
        {
            start = null;
            end = null;
            count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var Node = start;
            while (Node != null)
            {
                yield return Node.Value;
                Node = Node.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


