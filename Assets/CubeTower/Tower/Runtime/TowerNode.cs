namespace CubeTower
{
    public class TowerNode<T>
    {
        public T Data;
        public TowerNode<T> Next;
        public TowerNode<T> Previous;

        public TowerNode(T data)
        {
            Data = data;
        }
    }
}