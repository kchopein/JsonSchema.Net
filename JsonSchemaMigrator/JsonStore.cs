﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonSchemaMigrator
{
    /// <summary>
    /// Serializes and deserializes objects taking into account the serialized type. 
    /// If the deserialization target type is not the same as the originally used for serialization, it will check if the original type implements
    /// <seealso cref="IUpgradable{TTarget}"/> and will try to upgrade all the way to the target type.
    /// </summary>
    public static class JsonStore
    {
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
        /// Deserializes the specified source to the specified type. It will try to find a way to get to that type using <seealso cref="IUpgradable{TTarget}"/> implementations
        /// present in the source and intermediate types.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No upgrade path found.</exception>
        public static T Deserialize<T>(string source)
            where T : class
        {
            var jsonEnvelope = JObject.Parse(source);
            var payloadAssemblyName = jsonEnvelope[nameof(Envelope.PayloadFullyQualifiedName)];
            var payloadType = Type.GetType(payloadAssemblyName.ToString());
            var payload = jsonEnvelope.SelectToken("$.Payload").ToObject(payloadType);

            while (payload as T == null)
            {
                if (payloadType != typeof(T))
                {
                    var upgradeInterface = GetUpgradeInterface(payloadType);

                    if (upgradeInterface != null)
                    {
                        payload = upgradeInterface.GetMethod(nameof(IUpgradable<T>.Upgrade))
                            .Invoke(payload, null);
                        payloadType = upgradeInterface.GetGenericArguments()[0];
                    }
                    else
                    {
                        throw new InvalidOperationException("No upgrade path found.");
                    }
                }
            }

            return payload as T;
        }

        static Type GetUpgradeInterface(Type payloadType)
        {
            return payloadType.GetInterfaces()
                .FirstOrDefault(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUpgradable<>));
        }
    }
}
