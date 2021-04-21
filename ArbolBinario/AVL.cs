using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolBinario
{
    public class AVL<T> : ArbolBinario<T>
    {
        public AVL(Comparador<T> Funcomparador) : base(Funcomparador)  //Esta es la funcion
        {

        }

        public new void Add(T dato)
        {
            base.Add(dato);
            Balanceo();
        }
        public new void RemoveAt(T dato)
        {
            base.RemoveAt(dato);
            Balanceo();
        }
        private void Balanceo()
        {
            var resultadoPivote = Pivote(root, root);
            if (resultadoPivote is null) return;
            var nodoPivote = resultadoPivote[0];
            var nodoPadrePivote = resultadoPivote[1];
            var alturaI = 0;
            var alturaD = 0;
            if (nodoPivote.Left != null)
            {
                alturaI = Altura(nodoPivote.Left);
            }
            if (nodoPivote.Right != null)
            {
                alturaD = Altura(nodoPivote.Right);
            }
            var FactorDeEquilibrio = 0;
            FactorDeEquilibrio = alturaI - alturaD;
            if (FactorDeEquilibrio > 1)
            {
                int derecha = 1;
                VerificarRotaciondobble(nodoPivote.Left, nodoPivote, derecha);
                RotacionDerecha(nodoPivote, nodoPadrePivote);
            }
            if (FactorDeEquilibrio < -1)
            {
                int izquirda = -1;
                VerificarRotaciondobble(nodoPivote.Right, nodoPivote, izquirda);
                RotacionIzquierda(nodoPivote, nodoPadrePivote);
            }

        }
        private Nodo<T>[] Pivote(Nodo<T> CurrentRoot, Nodo<T> padre)
        {
            if (CurrentRoot is null) //osea que no hay algo adentro
            {
                return null;
            }
            else
            {
                Nodo<T>[] resultadoI = Pivote(CurrentRoot.Left, CurrentRoot);
                if (resultadoI != null) return resultadoI;
                Nodo<T>[] resultadoD = Pivote(CurrentRoot.Right, CurrentRoot);
                if (resultadoD != null) return resultadoD;
                //recorrido post order
                var alturaI = 0;
                var alturaD = 0;
                if (CurrentRoot.Left != null)
                {
                    alturaI = Altura(CurrentRoot.Left);
                }
                if (CurrentRoot.Right != null)
                {
                    alturaD = Altura(CurrentRoot.Right);
                }
                var FactorDeEquilibrio = 0;
                FactorDeEquilibrio = alturaI - alturaD;

                if (FactorDeEquilibrio > 1 || FactorDeEquilibrio < -1)
                {
                    return new Nodo<T>[] { CurrentRoot, padre };
                }
                return null;
            }
        }
private void VerificarRotaciondobble(Nodo<T> nodoPivote, Nodo<T> PadrePivote, int x)
        {
            var alturaI = 0;
            var alturaD = 0;
            if (nodoPivote.Left != null)
            {
                alturaI = Altura(nodoPivote.Left);
            }
            if (nodoPivote.Right != null)
            {
                alturaD = Altura(nodoPivote.Right);
            }
            var FactorDeEquilibrio = 0;
            FactorDeEquilibrio = alturaI - alturaD;
            if (x == 1)
            {
                if (FactorDeEquilibrio == -1)
                {
                    RotacionIzquierda(nodoPivote, PadrePivote);
                }
            }
            else
            {
                if (FactorDeEquilibrio == 1)
                {
                    RotacionDerecha(nodoPivote, PadrePivote);
                }
            }
        }

        private void RotacionIzquierda(Nodo<T> Pivote, Nodo<T> padrePivote)
        {
            if (Pivote == padrePivote.Left)
            {
                Nodo<T> i = new Nodo<T>();
                Nodo<T> f = new Nodo<T>();
                i = Pivote.Right.Left;
                f = Pivote;
                padrePivote.Left = Pivote.Right;
                Pivote = padrePivote.Left;
                Pivote.Left = f;
                Pivote.Left.Right = i;
            }
            else if (Pivote == padrePivote.Right)
            {
                Nodo<T> i = new Nodo<T>();
                Nodo<T> f = new Nodo<T>();
                i = Pivote.Right.Left;
                f = Pivote;
                padrePivote.Right = Pivote.Right;
                Pivote = padrePivote.Right;
                Pivote.Left = f;
                Pivote.Left.Right = i;
            }
            else
            {
                var i = Pivote.Right.Left;
                var f = Pivote;
                root = Pivote.Right;
                root.Left = f;
                root.Left.Right = i;
            }
        }
        private void RotacionDerecha(Nodo<T> Pivote, Nodo<T> padrePivote)
        {
            if (Pivote == padrePivote.Left)
            {
                Nodo<T> i = new Nodo<T>();
                Nodo<T> f = new Nodo<T>();
                i = Pivote.Left.Right;
                f = Pivote;
                padrePivote.Left = Pivote.Left;
                Pivote = padrePivote.Left;
                Pivote.Right = f;
                Pivote.Right.Left = i;
            }
            else if (Pivote == padrePivote.Right)
            {
                Nodo<T> i = new Nodo<T>();
                Nodo<T> f = new Nodo<T>();
                i = Pivote.Left.Right;
                f = Pivote;
                padrePivote.Right = Pivote.Left;
                Pivote = padrePivote.Right;
                Pivote.Right = f;
                Pivote.Right.Left = i;
            }
            else
            {
                var i = Pivote.Left.Right;
                var f = Pivote;
                root = Pivote.Left;
                root.Right = f;
                root.Right.Left = i;
            }
        }
    }
}
