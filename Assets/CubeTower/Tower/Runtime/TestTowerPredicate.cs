using Cubes;

namespace CubeTower
{
    public class TestTowerPredicate : ITowerPredicate
    {
        public bool Can(Cube cube)
        {
            return cube != null;
        }
    }
}
