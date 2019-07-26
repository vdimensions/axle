namespace Axle.Data.DataSources
{
    [RequiresDataSources]
    public interface ISqlScriptLocationConfigurer
    {
        void RegisterScriptLocations(ISqlScriptLocationRegistry registry);
    }
}
