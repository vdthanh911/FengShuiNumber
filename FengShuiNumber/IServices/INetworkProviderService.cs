using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.IServices
{
    public interface INetworkProviderService
    {
        Task<IEnumerable<NetworkProviderModel>> GetNetworkProvidersAsync();
        Task<bool> CreateNetworkProviderAsync(NetworkProviderModelRq networkProvider);
        Task<bool> ClearNetworkProviderAsync();
        Task<int> CountNetworkProviderAsync();
    }
}
