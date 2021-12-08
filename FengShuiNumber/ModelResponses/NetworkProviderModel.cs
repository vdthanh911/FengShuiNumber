using AutoMapper;
using FengShuiNumber.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FengShuiNumber.ModelResponses
{
    public class NetworkProviderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MobileNumberModel> MobileNumberModels { get; set; }
        public IEnumerable<NetworkProviderPrefixModel> NetworkProviderPrefixModels { get; set; }
    }

    public class NetworkProviderModelMapper : Profile
    {
        public NetworkProviderModelMapper()
        {
            CreateMap<NetworkProviderModel, NetworkProvider>();
            var mapers = CreateMap<NetworkProvider, NetworkProviderModel>();
            mapers.ForMember(r => r.MobileNumberModels
                    , s => s.MapFrom(k => k.MobileNumbers));
            mapers.ForMember(r => r.NetworkProviderPrefixModels
                    , s => s.MapFrom(k => k.NetworkProviderPrefixes));
        }
    }
}
