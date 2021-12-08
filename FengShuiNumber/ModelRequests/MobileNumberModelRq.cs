using AutoMapper;
using FengShuiNumber.Models;

namespace FengShuiNumber.ModelRequests
{
    public class MobileNumberModelRq
    {
        public string MobileNumber1 { get; set; }
        public int? NetworkProviderId { get; set; }
    }

    public class MobileNumberModelRqMapper : Profile
    {
        public MobileNumberModelRqMapper()
        {
            CreateMap<MobileNumberModelRq, MobileNumber>();
            var mapers = CreateMap<MobileNumber, MobileNumberModelRq>();
        }
    }
}
