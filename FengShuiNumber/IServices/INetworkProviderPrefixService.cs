using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.IServices
{
    public interface INetworkProviderPrefixService
    {
        Task<IEnumerable<NetworkProviderPrefixModel>> GetNetworkProviderPrefixesAsync();
        Task<bool> CreateNetworkProviderPrefixAsync(NetworkProviderPrefixModelRq modelRq);
    }
}
