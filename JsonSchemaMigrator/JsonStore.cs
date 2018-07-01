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
            
            while(resultPayload as T == null)
            if(jsonPayloadType != typeof(T))
            {
                var upgradeInterface = GetUpgradeInterface(jsonPayloadType);

                if(upgradeInterface != null)
                {
                    resultPayload = upgradeInterface.GetMethod("UpgradeTo")
                        .Invoke(resultPayload, null);
                        jsonPayloadType = upgradeInterface.GetGenericArguments()[0];
                }
                else
                    {
                        throw new InvalidOperationException("No upgrade path found.");
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
