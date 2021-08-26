using App.Extensions;

namespace App.Serializers
{
    public interface IJsonSerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string json);
    }
}
