using System;

namespace JsonSchemaMigrator
{
    internal class Envelope
    {
        private Envelope(Type payloadType, object payload)
        {
            this.PayloadFullyQualifiedName = payloadType.AssemblyQualifiedName;
            this.Payload = payload;
        }

        public static Envelope Create<T>(T source)
            where T : class
        {
            return new Envelope(typeof(T), source);
        }

        public object Payload { get; }
        public string PayloadFullyQualifiedName { get; }
    }
}
