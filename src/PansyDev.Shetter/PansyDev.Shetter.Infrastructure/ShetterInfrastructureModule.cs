using System.Collections.Generic;
using System.IO;
using PansyDev.Common.Infrastructure;
using PansyDev.Common.Infrastructure.Authentication;
using PansyDev.Common.Infrastructure.EntityFramework;
using PansyDev.Common.Infrastructure.EntityFramework.Extensions;
using PansyDev.Common.Infrastructure.Extensions;
using PansyDev.Shetter.Application;
using PansyDev.Shetter.Application.Models.WriteModels;
using PansyDev.Shetter.Infrastructure.Data;
using PansyDev.Shetter.Infrastructure.Mapping;
using PansyDev.Shetter.Infrastructure.Options;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace PansyDev.Shetter.Infrastructure
{
    [DependsOn(typeof(ShetterApplicationModule))]
    [DependsOn(typeof(CommonInfrastructureModule))]
    [DependsOn(typeof(CommonInfrastructureAuthenticationModule))]
    [DependsOn(typeof(CommonInfrastructureEntityFrameworkModule))]
    public class ShetterInfrastructureModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            context.Configure<ImageConverterOptions>(ShetterInfrastructureConfigurationKeys.ImageConverter);
            context.Configure<ImageStorageOptions>(ShetterInfrastructureConfigurationKeys.ImageStorage);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.ConfigureDatabase<ShetterDbContext>();

            context.ConfigureMappingProfile<TransportProfile>();

            Configure<AbpAuditingOptions>(options =>
            {
                options.IgnoredTypes.Add(typeof(IReadOnlyList<Stream>));
                options.IgnoredTypes.Add(typeof(IReadOnlyList<PostImageWriteModel>));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
#if RELEASE
            // Warm up services
            context.ServiceProvider.GetRequiredService<RootZoneRepository>();
            context.ServiceProvider.GetRequiredService<IImageStorage>();
#endif

            context.InitializeDatabase<ShetterDbContext>();
        }
    }
}
