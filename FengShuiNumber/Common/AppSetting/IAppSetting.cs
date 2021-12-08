using System;
using System.Collections.Generic;
using System.Text;

namespace FengShuiNumber.Common.AppSetting
{
    public interface IAppSetting
    {
        string GetConnectionStringFromAppSetting(string alias);
        string GetFengShuiConfigurationPath();
    }
}
