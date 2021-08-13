using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PansyDev.Common.Web;
using PansyDev.Shetter.Infrastructure;
using PansyDev.Shetter.Web.Types;
using Volo.Abp;
using Volo.Abp.AspNetCore.Security.Claims;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace PansyDev.Shetter.Web
{
    [DependsOn(typeof(CommonWebModule))]
    [DependsOn(typeof(ShetterInfrastructureModule))]
    public class ShetterWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddGraphQLServer()
                .AddProjections()
                .AddAuthorization()
                .AddInMemorySubscriptions()
                .AddShetterTypes()
                .InitializeOnStartup();

            Configure<AbpClaimsMapOptions>(options =>
            {
                options.Maps[PansyClaimTypes.UserId] = () => AbpClaimTypes.UserId;
                options.Maps[PansyClaimTypes.Username] = () => AbpClaimTypes.UserName;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseWebSockets();
            app.UseEndpoints(builder => builder.MapGraphQL());
        }
    }
}
