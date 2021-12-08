using FengShuiNumber.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.IRepositories
{
    public interface INetworkProviderPrefixRepository
    {
        Task<IEnumerable<NetworkProviderPrefix>> GetNetworkProviderPrefixesAsync();
        Task<bool> CreateNetworkProviderPrefixAsync(NetworkProviderPrefix entity);
    }
}
