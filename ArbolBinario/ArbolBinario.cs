using System;
using System.Collections.Generic;

namespace ArbolBinario
{
    public class ArbolBinario<T>
    {
        public ArbolBinario(Comparador<T> Funcomparador) //Esta es la funcion
        {
            this.comparador = Funcomparador; // el apuntador de la linea 12 que apunte a la funcion. 
        }
        internal Nodo<T> root;
        internal Comparador<T> comparador; //una de las propiedades del arbol es el comparador

        public void Add(T dato)
        {
            if (root == null)
            {
                root = new Nodo<T>();
                root.Value = dato;
            }
            else
            {
                if (comparador.Invoke(root.Value, dato) > 0) //si es currentroot es mayor que el dato 
                {
                    if (root.Left == null) //si no hay nada guardado que guarde el nuevo. 
                    {
                        root.Left = new Nodo<T>();
                        root.Left.Value = dato;
                    }
                    else
                    {
                        Add(dato, root.Left); //recursividad, para que vaya al siguiente nivel y encuentre donde agregarlo. 
                    }
                }
                if (comparador.Invoke(root.Value, dato) < 0) //si es currentroot es menor que el dato 
                {
                    if (root.Right == null) //si no hay nada guardado que guarde el nuevo. 
                    {
                        root.Right = new Nodo<T>();
                        root.Right.Value = dato;
                    }
                    else
                    {
                        Add(dato, root.Right); //recursividad, para que vaya al siguiente nivel y encuentre donde agregarlo. 
                    }
                }
            }
        }
        public delegate int Comparador<T>(T a, T b); //El tipo del Apuntador del delegado
        private void Add(T dato, Nodo<T> CurrentRoot)
        {
            if (comparador.Invoke(CurrentRoot.Value, dato) > 0) //si es currentroot es mayor que el dato 
            {
                if (CurrentRoot.Left == null) //si no hay nada guardado que guarde el nuevo. 
                {
                    CurrentRoot.Left = new Nodo<T>();
                    CurrentRoot.Left.Value = dato;
                }
                else
                {
                    Add(dato, CurrentRoot.Left); //recursividad, para que vaya al siguiente nivel y encuentre donde agregarlo. 
                }
            }
            else if (comparador.Invoke(CurrentRoot.Value, dato) < 0) //si es currentroot es menor que el dato 
            {
                if (CurrentRoot.Right == null) //si no hay nada guardado que guarde el nuevo. 
                {
                    CurrentRoot.Right = new Nodo<T>();
                    CurrentRoot.Right.Value = dato;
                }
                else
                {
                    Add(dato, CurrentRoot.Right); //recursividad, para que vaya al siguiente nivel y encuentre donde agregarlo. 
                }
            }
            else
            {
                throw new Exception("Item repetido"); //ya que no puede haber otro con el mismo valor. 
            }
        }
        public void RemoveAt(T dato)
        {
            RemoveAt(dato, root, null);
        }
        private void RemoveAt(T dato, Nodo<T> currentRoot, Nodo<T> padre)
        {
            int p = 0;
            if (currentRoot is null) throw new Exception("No encontrado");
            var comparacion = comparador.Invoke(currentRoot.Value, dato);
            if (comparacion == 0)//ya encontro el que quiere borrar
            {
                if (currentRoot.EsHoja)
                {
                    if (padre is null)
                    {
                        root = null;
                        return;
                    }
                    if (padre.Left != null && padre.Right != null)
                    {
                        if (comparador(padre.Left.Value, dato) == 0)//si es el de la izquierda el que quiere borrar
                            padre.Left = null;
                        else padre.Right = null;
                    }
                    else
                    {
                        if (padre.Left != null)
                        {
                            padre.Left = null;
                        }
                        else
                        {
                            padre.Right = null;
                        }
                    }

                }
                else if (currentRoot.TieneDosHijos)
                {
                    Nodo<T> padreSucesor = BuscarPadredelSucesor(currentRoot); //currentRoot el que quiere eliminar
                    Nodo<T> sucesor = Sucesor(currentRoot);
                    if (padre == null)
                    {
                        padreSucesor.Right = sucesor.Left;
                        sucesor.Left = currentRoot.Left;
                        sucesor.Right = currentRoot.Right;
                        root = sucesor;
                    }
                    else
                    {
                        if (padreSucesor == currentRoot)
                        {
                            sucesor.Right = currentRoot.Right;
                            if (padre.Left != null && padre.Right != null)
                            {
                                if (comparador(padre.Left.Value, dato) == 0)
                                {
                                    padre.Left = sucesor;
                                }
                                else padre.Right = sucesor;
                            }
                            else
                            {
                                if (padre.Left != null)
                                {
                                    padre.Left = sucesor;
                                }
                                else
                                {
                                    padre.Right = sucesor;
                                }
                            }
                        }
                        else
                        {
                            padreSucesor.Right = sucesor.Left;
                            sucesor.Left = currentRoot.Left;
                            sucesor.Right = currentRoot.Right;

                            if (padre.Left != null && padre.Right != null)
                            {
                                if (comparador(padre.Left.Value, dato) == 0)
                                {
                                    padre.Left = sucesor;
                                }
                                else padre.Right = sucesor;
                            }
                            else
                            {
                                if (padre.Left != null)
                                {
                                    padre.Left = sucesor;
                                }
                                else
                                {
                                    padre.Right = sucesor;
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (currentRoot.Left != null)
                    {
                        if (padre is null)
                        {
                            root = currentRoot.Left;
                        }
                        if (padre.Left != null && padre.Right != null)
                        {
                            if (comparador(padre.Left.Value, dato) == 0)//si es el de la izquierda el que quiere borrar
                                padre.Left = currentRoot.Left;
                            else padre.Right = currentRoot.Left;
                        }
                        else
                        {
                            if (padre.Left != null)
                            {
                                padre.Left = currentRoot.Left;
                            }
                            else
                            {
                                padre.Right = currentRoot.Left;
                            }
                        }

                    }
                    else
                    {
                        if (padre is null)
                        {
                            root = currentRoot.Right;
                        }
                        if (padre.Left != null && padre.Right != null)
                        {
                            if (comparador(padre.Left.Value, dato) == 0)//si es el de la izquierda el que quiere borrar
                                padre.Left = currentRoot.Right;
                            else padre.Right = currentRoot.Right;
                        }
                        else
                        {
                            if (padre.Left != null)
                            {
                                padre.Left = currentRoot.Right;
                            }
                            else
                            {
                                padre.Right = currentRoot.Right;
                            }
                        }
                    }
                }
                return;
            }

            if (comparacion > 0) RemoveAt(dato, currentRoot.Left, currentRoot); //que busque en el hijo izquierdo
            else if (comparacion < 0)
            {
                RemoveAt(dato, currentRoot.Right, currentRoot);
            }

        }

        private Nodo<T> Sucesor(Nodo<T> raiz)
        {
            Nodo<T> izquierdo = raiz.Left;
            if (izquierdo.EsHoja) return izquierdo; //ya encontro el ultimo que es el que va a reemplazar
            if (izquierdo.Right is null) return izquierdo;// ya encontro el mas derecho, mayor de los menores que va a reemplazar
            Nodo<T> actual = izquierdo.Right;
            while (actual.Right != null)
            {
                actual = actual.Right;
            }
            return actual;
        }

        private Nodo<T> BuscarPadredelSucesor(Nodo<T> raiz)
        {
            Nodo<T> izquierdo = raiz.Left;
            if (izquierdo.EsHoja) return raiz;
            if (izquierdo.Right is null) return raiz;
            Nodo<T> actual = izquierdo.Right;
            Nodo<T> padre = izquierdo;
            while (actual.Right != null)
            {
                padre = actual;
                actual = actual.Right;
            }
            return padre;
        }

        private Nodo<T> UbicarNodo(T buscador, Nodo<T> currentRoot)
        {
            if (currentRoot is null) return null;
            var comparacion = comparador.Invoke(currentRoot.Value, buscador);
            if (comparacion == 0) return currentRoot;
            if (comparacion > 0) return UbicarNodo(buscador, currentRoot.Left);
            return UbicarNodo(buscador, currentRoot.Right);
        }

        public List<T> ConvertirLista()
        {
            var list = new List<T>(); //se crea la lista nueva de resultados
            ConvertirLista(list, root); //recursividad a la funcion privada ya con los valores agregados
            return list;
        }
        private void ConvertirLista(List<T> lista, Nodo<T> CurrentRoot)
        {
            if (CurrentRoot != null) //osea que si hay algo adentro
            {
                //recorrido in order
                ConvertirLista(lista, CurrentRoot.Left);
                lista.Add(CurrentRoot.Value);
                ConvertirLista(lista, CurrentRoot.Right);
            }
        }
        public T Find(T buscador)
        {
            return Find(buscador, root);
        }
        private T Find(T buscador, Nodo<T> currentRoot)
        {
            if (currentRoot is null) return default(T);
            var comparacion = comparador.Invoke(currentRoot.Value, buscador);
            if (comparacion == 0) return currentRoot.Value;
            if (comparacion > 0) return Find(buscador, currentRoot.Left);
            return Find(buscador, currentRoot.Right);
        }
        
        public int Altura(Nodo<T> nodo)
        {
            if (nodo.EsHoja) return 1;
            var vistaI = 1;
            var vistaD = 1;
            if (nodo.Left != null)
            {
                vistaI += Altura(nodo.Left) ;
            }
            if (nodo.Right != null)
            {
                vistaD += Altura(nodo.Right);
            }
            return Math.Max(vistaI, vistaD);
        }
    }
}
