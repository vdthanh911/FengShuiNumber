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
    public class NetworkProviderService : INetworkProviderService
    {
        private readonly INetworkProviderRepository _networkProviderRepository;
        private readonly IMapper _mapper;
        public NetworkProviderService(INetworkProviderRepository networkProviderRepository,
            IMapper mapper
            )
        {
            _networkProviderRepository = networkProviderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NetworkProviderModel>> GetNetworkProvidersAsync()
        {
            var data = await _networkProviderRepository.GetNetworkProvidersAsync();
            return _mapper.Map<IEnumerable<NetworkProviderModel>>(data);
        }

        public async Task<bool> CreateNetworkProviderAsync(NetworkProviderModelRq modelRq)
        {
            var entity = _mapper.Map<NetworkProvider>(modelRq);
            return await _networkProviderRepository.CreateNetworkProviderAsync(entity);
        }

        public async Task<bool> ClearNetworkProviderAsync()
        {
            return await _networkProviderRepository.ClearNetworkProviderAsync();
        }
    }
}
