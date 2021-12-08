using FengShuiNumber.Common.AppSetting;
using FengShuiNumber.ModelResponses.FengShuiConfigurationModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace FengShuiNumber.Common.FengShuiConfiguration
{
    public class FengShuiConfiguration : IFengShuiConfiguration
    {
        private readonly IAppSetting _appSetting;
        public FengShuiConfiguration(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }
        public FengShuiConfigurationModel GetFengShuiConfiguration()
        {
            string file_path = Directory.GetCurrentDirectory() + _appSetting.GetFengShuiConfigurationPath();
            if (File.Exists(file_path))
            {
                return JsonConvert.DeserializeObject<FengShuiConfigurationModel>(File.ReadAllText(file_path));
            }
            return new FengShuiConfigurationModel();
        }
    }
}
