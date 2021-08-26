namespace App.Serializers
{
    public interface IXmlSerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string xml);
    }
}