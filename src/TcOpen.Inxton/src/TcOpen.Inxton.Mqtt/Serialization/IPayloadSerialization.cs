namespace TcOpen.Inxton.Mqtt
{
    public interface IPayloadSerializer<T>
    {
        string Serialize(T plain);
    }
    public interface IPayloadDeserializer<T>
    {
        T Deserialize(string plain);
    }
}
