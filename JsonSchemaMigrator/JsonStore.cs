using Newtonsoft.Json;
using System;

namespace JsonSchemaMigrator
{
    public static class JsonStore
    {
        public static string Serialize<T>(T source) 
            where T : class
        {
            var envelope = new Envelope<T>(source);
            return JsonConvert.SerializeObject(envelope);
        }

        public static T Deserialize<T>(string source)
            where T : class
        {
            var envelope = JsonConvert.DeserializeObject<Envelope<T>>(source);
            return envelope.Payload;
        }
    }
}
