using System;

namespace JsonSchemaMigrator
{
    /// <summary>
    /// Holds the object to be stored as JSON and its fully qualified name.
    /// </summary>
    class Envelope
    {
        private Envelope(Type payloadType, object payload)
        {
            this.PayloadFullyQualifiedName = payloadType.AssemblyQualifiedName;
            this.Payload = payload;
        }

        /// <summary>
        /// Creates an Envelope for the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The envelope.</returns>
        public static Envelope Create<T>(T source)
            where T : class
        {
            return new Envelope(typeof(T), source);
        }

        public object Payload { get; }
        public string PayloadFullyQualifiedName { get; }
    }
}
