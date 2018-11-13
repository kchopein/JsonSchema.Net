using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonSchemaMigrator
{
    /// <summary>
    /// Serializes and deserializes objects taking into account the serialized type. 
    /// If the deserialization target type is not the same as the originally used for serialization, it will check if the original type implements
    /// <seealso cref="IMigrationHandler{TTarget}"/> and will try to upgrade all the way to the target type.
    /// </summary>
    public static class JsonStore
    {
        private static Dictionary<string, object> registeredActions = new Dictionary<string, object>();

        private static IMigrationHandlerProvider migrationHandlerProvider;

        /// <summary>
        /// Serializes the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string Serialize<T>(T source) 
            where T : class
        {
            var envelope = Envelope.Create(source);
            return JsonConvert.SerializeObject(envelope);
        }

        /// <summary>
        /// Deserializes the specified source to the specified type. It will try to find a way to get to that type using <seealso cref="IMigrationHandler{TTarget}"/> implementations
        /// present in the source and intermediate types.
        /// </summary>
        /// <typeparam name="TTarget">Target type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No upgrade path found.</exception>
        public static TTarget Deserialize<TTarget>(string source)
            where TTarget : class
        {
            var jsonEnvelope = JObject.Parse(source);
            var payloadAssemblyName = jsonEnvelope[nameof(Envelope.PayloadFullyQualifiedName)];
            var payloadType = Type.GetType(payloadAssemblyName.ToString());
            var payload = jsonEnvelope.SelectToken("$.Payload").ToObject(payloadType);

            while (payload as TTarget == null)
            {
                if (payloadType != typeof(TTarget))
                {
                    var migrationHandlerWrapper = GetMigrationHandlerWrapper(payloadType);

                    if (migrationHandlerWrapper != null)
                    {
                        var sourcePayload = payload;
                        payload = migrationHandlerWrapper.Upgrade(sourcePayload);
                        var sourcePayloadType = payloadType;
                        payloadType = migrationHandlerWrapper.UpgradeTargetType;
                    }
                    else
                    {
                        throw new InvalidOperationException("No upgrade path found.");
                    }
                }
            }

            return payload as TTarget;
        }

        private static MigrationHandlerWrapper GetMigrationHandlerWrapper(Type payloadType)
        {
            var providerMethod = migrationHandlerProvider.GetType().GetMethod(nameof(IMigrationHandlerProvider.GetMigrationHandler));
            var parameterizedProviderMethod = providerMethod.MakeGenericMethod(payloadType);

            var migrationHandler = parameterizedProviderMethod.Invoke(migrationHandlerProvider, null);

            return migrationHandler != null ? new MigrationHandlerWrapper(migrationHandler): null;
        }

        public static void ConfigureMigrationHandlerProvider(IMigrationHandlerProvider migrationHandlerProvider)
        {
            JsonStore.migrationHandlerProvider = migrationHandlerProvider;
        }
    }
}
