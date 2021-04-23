using System;
using System.Collections.Generic;
using System.Text;


namespace DoubleLinkedListLibrary1
{

    public class Node<T> : ICloneable
    {
        public T Value;

        public Node<T> next;
        public Node<T> Behind;

        public Node<T> Clone()
        {
            return new Node<T>(this);
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        public Node(Node<T> n)
        {
            this.Behind = n.Behind;
            this.next = n.next;
            this.Value = n.Value;
        }
        public Node()
        {
            this.next = null;
            this.Behind = null;
        }
    }
}