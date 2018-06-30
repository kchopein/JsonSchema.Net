namespace JsonSchemaMigrator
{
    internal class Envelope<T> where T : class
    {
        public Envelope(T payload)
        {
            this.PayloadType = typeof(T).FullName;
            this.Payload = payload;
        }

        public string PayloadType { get; }
        public T Payload { get; }
    }
}
