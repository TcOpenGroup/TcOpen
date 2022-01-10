namespace TcOpen.Inxton.Mqtt
{
    public interface IPayloadFormatterFor<T>
    {
        string Format(T plain);
    }
}
