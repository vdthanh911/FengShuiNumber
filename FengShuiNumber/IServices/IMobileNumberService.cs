using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiNumber.IServices
{
    public interface IMobileNumberService
    {
        Task<IEnumerable<MobileNumberModel>> GetMobileNumbersAsync();
        Task<bool> CreateMobileNumberAsync(MobileNumberModelRq modelRq);
        Task<bool> ClearMobileNumberAsync();
        Task<IEnumerable<MobileNumberModel>> GetFengShuiMobileNumbersAsync();
        bool CheckFengShuiMobileNumber(string mobileNumber);
        string MakeFengShuiNumber(string prefix);
    }
}
