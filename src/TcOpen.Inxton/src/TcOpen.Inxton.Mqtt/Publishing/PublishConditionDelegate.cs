namespace TcOpen.Inxton.Mqtt
{
    public delegate bool PublishConditionDelegate<T>(T LastPublished, T ToPublish);
}