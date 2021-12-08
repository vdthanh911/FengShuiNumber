using AutoMapper;
using FengShuiNumber.IRepositories;
using FengShuiNumber.IServices;
using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using FengShuiNumber.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.Services
{
    public class NetworkProviderPrefixService : INetworkProviderPrefixService
    {
        private readonly INetworkProviderPrefixRepository _networkProviderPrefixRepository;
        private readonly IMapper _mapper;
        public NetworkProviderPrefixService(INetworkProviderPrefixRepository networkProviderPrefixRepository,
            IMapper mapper
            )
        {
            _networkProviderPrefixRepository = networkProviderPrefixRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NetworkProviderPrefixModel>> GetNetworkProviderPrefixesAsync()
        {
            var data = await _networkProviderPrefixRepository.GetNetworkProviderPrefixesAsync();
            return _mapper.Map<IEnumerable<NetworkProviderPrefixModel>>(data);
        }

        public async Task<bool> CreateNetworkProviderPrefixAsync(NetworkProviderPrefixModelRq modelRq)
        {
            var entity = _mapper.Map<NetworkProviderPrefix>(modelRq);
            return await _networkProviderPrefixRepository.CreateNetworkProviderPrefixAsync(entity);
        }
    }
}
