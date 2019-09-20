using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using MongoDB.Bson.Serialization;
using System;

namespace DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos
{
    public class EntityIdSerializer<T> : IBsonSerializer<T>
        where T : EntityId, new()
    {
        private readonly Func<string, T> concretFunc;

        public EntityIdSerializer(Func<string, T> concretFunc)
        {
            this.concretFunc = concretFunc;
        }

        public Type ValueType => typeof(T);

        public T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            return this.concretFunc(value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is EntityId email)
            {
                context.Writer.WriteString(email.ToString());
            }
            else
            {
                throw new NotSupportedException("This is not an entityId");
            }
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            return this.concretFunc(value);
        }
    }
}