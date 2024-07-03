namespace TcOpen.Inxton.Mqtt
{
    internal class PropertyValue<T>
    {
        public string Property { get; set; }
        public T Value { get; set; }
    }
}
