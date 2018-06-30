using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonSchemaMigrator
{
    public static class JsonStore
    {
        public static string Serialize<T>(T source) 
            where T : class
        {
            var envelope = Envelope.Create(source);
            return JsonConvert.SerializeObject(envelope);
        }

        public static T Deserialize<T>(string source)
            where T : class
        {
            var jsonEnvelope = JObject.Parse(source);
            var payloadAssemblyName = jsonEnvelope[nameof(Envelope.PayloadFullyQualifiedName)];
            var jsonPayloadType = Type.GetType(payloadAssemblyName.ToString());
            var resultPayload = jsonEnvelope.SelectToken("$.Payload").ToObject(jsonPayloadType);
            
            if(jsonPayloadType != typeof(T))
            {
                var upgradeInterface = GetUpgradeInterface(jsonPayloadType);

                if(upgradeInterface != null 
                    && upgradeInterface.GetGenericArguments()[0] == typeof(T))
                {
                    return upgradeInterface.GetMethod("UpgradeTo")
                        .Invoke(resultPayload, null) as T;
                }
            }

            return resultPayload as T;
        }

        private static Type GetUpgradeInterface(Type payloadType)
        {
            return payloadType.GetInterfaces()
                .FirstOrDefault(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUpgradeSchema<>));
        }
    }
}
