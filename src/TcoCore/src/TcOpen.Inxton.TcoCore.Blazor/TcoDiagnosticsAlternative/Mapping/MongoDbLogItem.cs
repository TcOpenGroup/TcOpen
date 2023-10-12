using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

using System;
using System.Collections.Generic;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping
{
    public class MongoDbLogItem
    {

        static MongoDbLogItem()
        {
            try
            {
                BsonSerializer.RegisterSerializer(typeof(UInt64), new UInt64Serializer(BsonType.Int64, new RepresentationConverter(true, false)));
                BsonSerializer.RegisterSerializer(typeof(UInt32), new UInt32Serializer(BsonType.Int64, new RepresentationConverter(true, false)));
                BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
                //BsonSerializer.RegisterSerializer(typeof(float), new FloatTruncationSerializer());
            }
            catch (BsonSerializationException)
            {
                // Handle or log the exception as needed
            }
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string Level { get; set; }

        [BsonElement("UtcTimeStamp")]
        public DateTime UtcTimeStamp { get; set; }

        [BsonElement("UtcTimeStampAcknowledged")]
        public DateTime? TimeStampAcknowledged { get; set; } = null;

        public MessageTemplate MessageTemplate { get; set; }

        public string RenderedMessage { get; set; }

        public Properties Properties { get; set; }

        public Exception Exception { get; set; }
    }

    public class MessageTemplate
    {
        public string Text { get; set; }

        public List<Token> Tokens { get; set; }
    }

    public class Token
    {
        [BsonElement("_t")]
        public string Type { get; set; }

        public int StartIndex { get; set; }

        public string Text { get; set; }  // This property is only for TextToken
    }

    public class Properties
    {
        //public string? payload { get; set; }

        public MessageProperties message { get; set; }

        [BsonSerializer(typeof(PayloadDeserializer))]
        public PayloadProperties payload { get; set; }

        //public PayloadProperties payload { get; set; }

        public SenderProperties sender { get; set; }

        public string user { get; set; }

        public string EnvironmentName { get; set; }

        public string EnvironmentUserName { get; set; }

        public string MachineName { get; set; }

        public ulong? ExtractedIdentity { get; set; }

        public string? UserName { get; set; }
    }

    public class MessageProperties
    {
        public string Text { get; set; }

        public int Category { get; set; }
    }

    public class PayloadProperties
    {
        public string rootObject { get; set; }
        public string rootSymbol { get; set; }
        [BsonExtraElements]
        public IDictionary<string, object> ExtraElements { get; set; }
    }

    public class SenderProperties
    {
        public bool ControllerLogger { get; set; }

        public Payload Payload { get; set; }
    }

    public class Payload
    {
        public string PlcLogger { get; set; }

        public string ParentSymbol { get; set; }

        public string ParentName { get; set; }
        
        public int Cycle { get; set; }

        [BsonElement("PlcTimeStamp")]
        public DateTime PlcTimeStamp { get; set; }

        public string Raw { get; set; }

        public int Pcc { get; set; }

        public ulong Identity { get; set; }


        public int MessageDigest { get; set; }  // Adding MessageDigest property
    }

    public class Exception
    {
        [BsonElement("_csharpnull")]
        public bool CSharpNull { get; set; }
    }

    public class PayloadDeserializer : SerializerBase<PayloadProperties>
    {
        public override PayloadProperties Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.CurrentBsonType;
            if (bsonType == BsonType.String)
            {
                context.Reader.SkipValue();  // Skip the string value as it's not needed
                return null;  // or return a new PayloadProperties with default values if needed
            }
            else if (bsonType == BsonType.Document)
            {
                var serializer = BsonSerializer.LookupSerializer<PayloadProperties>();
                return serializer.Deserialize(context, args);
            }
            else
            {
                throw new BsonSerializationException($"Cannot deserialize 'payload' from BsonType {bsonType}.");
            }
        }
    }
}


