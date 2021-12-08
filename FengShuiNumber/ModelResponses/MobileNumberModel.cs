using AutoMapper;
using FengShuiNumber.Models;

namespace FengShuiNumber.ModelResponses
{
    public class MobileNumberModel
    {
        public int Id { get; set; }
        public string MobileNumber1 { get; set; }
        public int? NetworkProviderId { get; set; }

        public virtual NetworkProviderModel NetworkProviderModel { get; set; }
    }

    public class MobileNumberModelMapper : Profile
    {
        public MobileNumberModelMapper()
        {
            CreateMap<MobileNumberModel, MobileNumber>();
            var mapers = CreateMap<MobileNumber, MobileNumberModel>();
        }
    }
}
