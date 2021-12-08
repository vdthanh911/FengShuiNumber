using AutoMapper;
using FengShuiNumber.Models;
using System.Collections.Generic;

namespace FengShuiNumber.ModelRequests
{
    public class NetworkProviderModelRq
    {
        public string Name { get; set; }
        public List<MobileNumberModelRq> MobileNumbers { get; set; }
        public List<NetworkProviderPrefixModelRq> NetworkProviderPrefixes { get; set; }
    }

    public class NetworkProviderModelRqMapper : Profile
    {
        public NetworkProviderModelRqMapper()
        {
            CreateMap<NetworkProviderModelRq, NetworkProvider>();
            var mapers = CreateMap<NetworkProvider, NetworkProviderModelRq>();
        }
    }
}
