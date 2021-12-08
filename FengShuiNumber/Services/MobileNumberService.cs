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
    public class MobileNumberService : IMobileNumberService
    {
        private readonly IMobileNumberRepository _mobileNumberRepository;
        private readonly IMapper _mapper;
        public MobileNumberService(IMobileNumberRepository mobileNumberRepository,
             IMapper mapper
            )
        {
            _mobileNumberRepository = mobileNumberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MobileNumberModel>> GetMobileNumbersAsync()
        {
            var data = await _mobileNumberRepository.GetMobileNumbersAsync();
            return _mapper.Map<IEnumerable<MobileNumberModel>>(data);
        }

        public async Task<bool> CreateMobileNumberAsync(MobileNumberModelRq modelRq)
        {
            var entity = _mapper.Map<MobileNumber>(modelRq);
            return await _mobileNumberRepository.CreateMobileNumberAsync(entity);
        }

        public async Task<bool> ClearMobileNumberAsync()
        {
            return await _mobileNumberRepository.ClearMobileNumberAsync();
        }
    }
}
