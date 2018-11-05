using System;

namespace JsonSchemaMigrator
{
    /// <summary>
    /// Specifies how to upgrade an instance of this type to the specified type.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    public interface IMigrationHandler<TSource>
    {
        Type UpgradeTargetType { get; }
        Type DowngradeTargetType { get; }

        object Upgrade(TSource source);

        object Downgrade(TSource source);
    }
}
