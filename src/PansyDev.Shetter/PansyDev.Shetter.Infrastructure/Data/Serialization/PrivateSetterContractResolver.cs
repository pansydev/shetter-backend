using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PansyDev.Shetter.Infrastructure.Data.Serialization
{
    internal class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (jsonProperty.Writable) return jsonProperty;

            if (member is PropertyInfo propertyInfo)
            {
                jsonProperty.Writable = propertyInfo.GetSetMethod(true) != null;
            }

            return jsonProperty;
        }
    }
}