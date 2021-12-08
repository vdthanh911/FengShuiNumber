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
    public class MobileNumberRepository : IMobileNumberRepository
    {
        public FengShuiNumberDbContext FengShuiNumberDbContext { get; }
        public MobileNumberRepository(FengShuiNumberDbContext fengShuiNumberDbContext)
        {
            FengShuiNumberDbContext = fengShuiNumberDbContext;
        }
        public async Task<IEnumerable<MobileNumber>> GetMobileNumbersAsync()
        {
            return await FengShuiNumberDbContext.MobileNumbers.ToListAsync();
        }

        public async Task<bool> CreateMobileNumberAsync(MobileNumber entity)
        {
            FengShuiNumberDbContext.MobileNumbers.Add(entity);
            await FengShuiNumberDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearMobileNumberAsync()
        {
            FengShuiNumberDbContext.MobileNumbers.RemoveRange(FengShuiNumberDbContext.MobileNumbers);
            await FengShuiNumberDbContext.SaveChangesAsync();
            return true;
        }
    }
}
