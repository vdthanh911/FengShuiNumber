using AutoMapper;
using FengShuiNumber.Models;

namespace FengShuiNumber.ModelResponses
{
    public class NetworkProviderPrefixModel
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int? NetworkProviderId { get; set; }

        public virtual NetworkProviderModel NetworkProviderModel { get; set; }
    }

    public class NetworkProviderPrefixModelMapper : Profile
    {
        public NetworkProviderPrefixModelMapper()
        {
            CreateMap<NetworkProviderPrefixModel, NetworkProviderPrefix>();
            var mapers = CreateMap<NetworkProviderPrefix, NetworkProviderPrefixModel>();
        }
    }
}
