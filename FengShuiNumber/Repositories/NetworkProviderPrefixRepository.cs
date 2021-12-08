using FengShuiNumber.IRepositories;
using FengShuiNumber.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiNumber.Repositories
{
    public class NetworkProviderPrefixRepository : INetworkProviderPrefixRepository
    {
        public FengShuiNumberDbContext FengShuiNumberDbContext { get; }
        public NetworkProviderPrefixRepository(FengShuiNumberDbContext fengShuiNumberDbContext)
        {
            FengShuiNumberDbContext = fengShuiNumberDbContext;
        }
        public async Task<IEnumerable<NetworkProviderPrefix>> GetNetworkProviderPrefixesAsync()
        {
            return await FengShuiNumberDbContext.NetworkProviderPrefixes.ToListAsync();
        }

        public async Task<bool> CreateNetworkProviderPrefixAsync(NetworkProviderPrefix entity)
        {
            FengShuiNumberDbContext.NetworkProviderPrefixes.Add(entity);
            await FengShuiNumberDbContext.SaveChangesAsync();
            return true;
        }
    }
}
