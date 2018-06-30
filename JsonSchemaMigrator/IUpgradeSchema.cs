namespace JsonSchemaMigrator
{
    public interface IUpgradeSchema<TTarget>
    {
        TTarget UpgradeTo();
    }
}
