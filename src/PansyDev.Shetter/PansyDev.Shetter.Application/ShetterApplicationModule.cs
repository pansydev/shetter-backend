using PansyDev.Common.Application;
using PansyDev.Shetter.Domain;
using Volo.Abp.Modularity;

namespace PansyDev.Shetter.Application
{
    [DependsOn(typeof(CommonApplicationModule))]
    [DependsOn(typeof(ShetterDomainModule))]
    public class ShetterApplicationModule : AbpModule { }
}
