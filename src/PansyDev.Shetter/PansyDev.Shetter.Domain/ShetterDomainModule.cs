using PansyDev.Common.Domain;
using Volo.Abp.Modularity;

namespace PansyDev.Shetter.Domain
{
    [DependsOn(typeof(CommonDomainModule))]
    public class ShetterDomainModule : AbpModule { }
}
