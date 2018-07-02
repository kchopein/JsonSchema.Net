namespace JsonSchemaMigrator
{
    /// <summary>
    /// Specifies how to upgrade an instance of this type to the specified type.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    public interface IUpgradable<TTarget>
    {
        /// <summary>
        /// Creates an instance of the <typeparamref name="TTarget"/> type using the instance data.
        /// </summary>
        /// <returns></returns>
        TTarget Upgrade();
    }
}
