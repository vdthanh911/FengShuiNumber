using Microsoft.Extensions.Configuration;
using System.IO;

namespace FengShuiNumber.Common.AppSetting
{
    public class AppSetting : IAppSetting
    {
        private IConfigurationRoot _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .Build();

        public string GetConnectionStringFromAppSetting(string alias)
        {
            return _configuration.GetConnectionString(alias);
        }

        public string GetFengShuiConfigurationPath()
        {
            var section = _configuration.GetSection("ConfigurationPaths").GetSection("Path");
            return section.Value;
        }
    }
}
