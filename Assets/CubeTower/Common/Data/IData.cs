namespace CubeTower.Common.Data
{
    public interface IData
    {
        string Name();

        void WhenCreateNewData();

        void BeforeSerialize();
    }
}