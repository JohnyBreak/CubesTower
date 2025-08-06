using System.Collections;
using System.Collections.Generic;

namespace CubeTower
{
    public class TowerList<T>
    {
        public TowerNode<T> Head;
        public TowerNode<T> Tail;

        public int Count { get; private set; }

        public TowerList()
        {
        
        }
    
        public TowerList(T data)
        {
            var node = GetNode(data);
            Head = node;
            Tail = node;
            Count = 1;
        }

        public void Add(T data)
        {
            var node = GetNode(data);

            if (Count < 1)
            {
                Head = node;
                Tail = node;
                Count = 1;
                return;
            }

            Tail.Next = node;
            node.Previous = Tail;
            Tail = node;
            Count++;
        }

        public void Remove(T data)
        {
            if (Count < 1 || Head == null)
            {
                return;
            }

            var current = Head;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    var next = current.Next;
                    var previous = current.Previous;

                    if (next != null)
                    {
                        next.Previous = previous;
                        if (previous == null)
                        {
                            Head = next;
                        }
                    }

                    if (previous != null)
                    {
                        previous.Next = next;
                        if (next == null)
                        {
                            Tail = previous;
                        }
                    }

                    Count--;
                    return;
                }

                current = current.Next;
            }
        }

        public IEnumerable<T> GetDatasAfter(T data)
        {
            if (Count < 1 || Head == null)
            {
                yield break;
            }
            
            var current = Head;

            bool targetFound = false;
            
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    targetFound = true;
                    current = current.Next;
                    continue;
                }

                if (targetFound)
                {
                    yield return current.Data;
                }

                current = current.Next;
            }
        }

        private TowerNode<T> GetNode(T data)
        {
            return new TowerNode<T>(data);
        }
    }

}