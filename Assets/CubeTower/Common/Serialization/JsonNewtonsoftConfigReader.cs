namespace Serialization
{
    public class JsonNewtonsoftConfigReader : IConfigReader
    {
        private readonly ISerializer _serializer = new JsonNewtonsoftSerializer();

        public T Read<T>(string json)
        {
            return _serializer.Read<T>(json, typeof(T));
        }
    }
}