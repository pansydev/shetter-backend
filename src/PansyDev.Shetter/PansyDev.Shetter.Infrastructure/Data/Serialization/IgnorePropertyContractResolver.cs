using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PansyDev.Shetter.Infrastructure.Data.Serialization
{
    internal class IgnorePropertyContractResolver : DefaultContractResolver
    {
        private readonly string[] _ignoredProps;

        public IgnorePropertyContractResolver(params string[] ignoredProps)
        {
            _ignoredProps = ignoredProps;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (_ignoredProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }

            return property;
        }
    }
}