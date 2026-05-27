using Volo.Abp.Threading;

namespace RCS.EntityFrameworkCore;

public static class RCSEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        RCSGlobalFeatureConfigurator.Configure();
        RCSModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
