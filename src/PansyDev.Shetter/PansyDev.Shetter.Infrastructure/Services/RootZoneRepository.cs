using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;
using PansyDev.Shetter.Domain.Services.Abstractions;
using PansyDev.Shetter.Infrastructure.Options;
using StackExchange.Redis;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class RootZoneRepository : IRootZoneRepository, ISingletonDependency
    {
        private readonly HashSet<string> _domainNames;
        private readonly ShetterTextParsingOptions _shetterTextParsingOptions;
        private readonly IdnMapping _idnMapping = new();

        public RootZoneRepository(IDatabase database,
            IOptions<ShetterRedisOptions> shetterRedisOptions,
            IOptions<ShetterTextParsingOptions> shetterTextParsingOptions)
        {
            _shetterTextParsingOptions = shetterTextParsingOptions.Value;

            _domainNames = database.SetMembers(shetterRedisOptions.Value.RootDomainZonesKey)
                .Select(x => (string)x)
                .ToHashSet();

            if (_domainNames.Any()) return;

            _domainNames = RetrieveRootDomainZones();

            var hashEntries = _domainNames.Select(x => new RedisValue(x)).ToArray();
            database.SetAdd(shetterRedisOptions.Value.RootDomainZonesKey, hashEntries);
        }

        public bool IsValidRootZone(string zone)
        {
            return _domainNames.Contains(_idnMapping.GetAscii(zone).ToUpper());
        }

        private HashSet<string> RetrieveRootDomainZones()
        {
            var client = new HttpClient();
            var response = client.GetStringAsync(_shetterTextParsingOptions.RootDomainZonesUrl);

            return response.Result.Split("\n").Skip(1).ToHashSet();
        }
    }
}