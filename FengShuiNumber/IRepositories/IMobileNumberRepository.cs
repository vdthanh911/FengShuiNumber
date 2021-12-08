using FengShuiNumber.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiNumber.IRepositories
{
    public interface IMobileNumberRepository
    {
        Task<IEnumerable<MobileNumber>> GetMobileNumbersAsync();
        Task<bool> CreateMobileNumberAsync(MobileNumber entity);
        Task<bool> ClearMobileNumberAsync();
    }
}
