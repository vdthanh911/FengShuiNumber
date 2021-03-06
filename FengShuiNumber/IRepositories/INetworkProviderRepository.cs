using FengShuiNumber.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.IRepositories
{
    public interface INetworkProviderRepository
    {
        Task<IEnumerable<NetworkProvider>> GetNetworkProvidersAsync();
        Task<bool> CreateNetworkProviderAsync(NetworkProvider entity);
        Task<bool> ClearNetworkProviderAsync();
        Task<int> CountNetworkProviderAsync();
    }
}
