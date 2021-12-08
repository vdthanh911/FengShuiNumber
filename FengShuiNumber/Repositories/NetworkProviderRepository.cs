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
    public class NetworkProviderRepository : INetworkProviderRepository
    {
        public FengShuiNumberDbContext FengShuiNumberDbContext { get; }

        public NetworkProviderRepository(FengShuiNumberDbContext fengShuiNumberDbContext) 
        {
            FengShuiNumberDbContext = fengShuiNumberDbContext;
        }

        public async Task<IEnumerable<NetworkProvider>> GetNetworkProvidersAsync()
        {
            return await FengShuiNumberDbContext.NetworkProviders.ToListAsync();
        }

        public async Task<bool> CreateNetworkProviderAsync(NetworkProvider entity)
        {
            FengShuiNumberDbContext.NetworkProviders.Add(entity);
            await FengShuiNumberDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearNetworkProviderAsync()
        {
            FengShuiNumberDbContext.NetworkProviders.RemoveRange(FengShuiNumberDbContext.NetworkProviders);
            await FengShuiNumberDbContext.SaveChangesAsync();
            return true;
        }
    }
}
