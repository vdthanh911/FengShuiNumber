using AutoMapper;
using FengShuiNumber.Models;

namespace FengShuiNumber.ModelRequests
{
    public class NetworkProviderPrefixModelRq
    {
        public string Prefix { get; set; }
        public int? NetworkProviderId { get; set; }
    }

    public class NetworkProviderPrefixModelRqMapper : Profile
    {
        public NetworkProviderPrefixModelRqMapper()
        {
            CreateMap<NetworkProviderPrefixModelRq, NetworkProviderPrefix>();
            var mapers = CreateMap<NetworkProviderPrefix, NetworkProviderPrefixModelRq>();
        }
    }
}
