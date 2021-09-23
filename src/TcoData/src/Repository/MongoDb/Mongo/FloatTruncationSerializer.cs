using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace TcOpen.Inxton.Data.MongoDb
{
    /// <summary>
    /// Write the float value to mongo as bool as read it back as float.
    /// Useful if you want to rewrite the values in the DB and read it back in C# app.
    /// </summary>
    public class FloatTruncationSerializer : SerializerBase<float>
    {
        /// <inheritdoc/>
        public override float Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadDouble();
            if (value == double.Epsilon)
                return float.Epsilon;
            else
                return (float)value;
        }

        /// <inheritdoc/>
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, float value)
        {
            if(value == float.Epsilon)
                context.Writer.WriteDouble(double.Epsilon);
            else
                context.Writer.WriteDouble(Math.Round(value,10));
        }
    }
}
