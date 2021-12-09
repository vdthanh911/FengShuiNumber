using FengShuiNumber.Common.AppSetting;
using FengShuiNumber.ModelResponses.FengShuiConfigurationModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace FengShuiNumber.Common.FengShuiConfiguration
{
    public class FengShuiConfiguration : IFengShuiConfiguration
    {
        private FengShuiConfigurationModel _fengShuiConfiguration { get; set; }
        private readonly IAppSetting _appSetting;
        public FengShuiConfiguration(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public FengShuiConfigurationModel GetFengShuiConfiguration()
        {
            if (_fengShuiConfiguration == null)
            {
                string file_path = Directory.GetCurrentDirectory() + _appSetting.GetFengShuiConfigurationPath();
                if (File.Exists(file_path))
                {
                    _fengShuiConfiguration = JsonConvert.DeserializeObject<FengShuiConfigurationModel>(File.ReadAllText(file_path));
                }
                else
                {
                    _fengShuiConfiguration = new FengShuiConfigurationModel();
                }
            }
            return _fengShuiConfiguration;
        }
    }
}
