using FengShuiNumber.Common.FengShuiConfiguration;
using FengShuiNumber.IServices;
using FengShuiNumber.ModelRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiNumber.Controllers
{
    public class FengShuiNumberController
    {
        private readonly INetworkProviderService _networkProviderService;
        private readonly INetworkProviderPrefixService _networkProviderPrefix;
        private readonly IMobileNumberService _mobileNumberService;
        private readonly IFengShuiConfiguration _fengShuiConfiguration;
        public FengShuiNumberController(INetworkProviderService networkProviderService,
            INetworkProviderPrefixService networkProviderPrefix,
            IMobileNumberService mobileNumberService,
            IFengShuiConfiguration fengShuiConfiguration
            )
        {
            _networkProviderService = networkProviderService;
            _networkProviderPrefix = networkProviderPrefix;
            _mobileNumberService = mobileNumberService;
            _fengShuiConfiguration = fengShuiConfiguration;
        }

        public async Task RunFengShuiAsync()
        {
            Console.WriteLine("Clear Mobile Number");
            await _mobileNumberService.ClearMobileNumberAsync();

            Console.WriteLine("Clear Network Provider");
            await _networkProviderService.ClearNetworkProviderAsync();

            Console.WriteLine("Init Network Provider Data");
            await _networkProviderService.CreateNetworkProviderAsync(
                new NetworkProviderModelRq()
                {
                    Name = "Viettel",
                    NetworkProviderPrefixes = new List<NetworkProviderPrefixModelRq>()
                    {
                        new NetworkProviderPrefixModelRq(){Prefix = "086"},
                        new NetworkProviderPrefixModelRq(){Prefix = "096"},
                        new NetworkProviderPrefixModelRq(){Prefix = "097"},
                    }
                });
            await _networkProviderService.CreateNetworkProviderAsync(new NetworkProviderModelRq()
            {
                Name = "Mobi",
                NetworkProviderPrefixes = new List<NetworkProviderPrefixModelRq>()
                    {
                        new NetworkProviderPrefixModelRq(){Prefix = "089"},
                        new NetworkProviderPrefixModelRq(){Prefix = "090"},
                        new NetworkProviderPrefixModelRq(){Prefix = "093"},
                    }
            });
            await _networkProviderService.CreateNetworkProviderAsync(new NetworkProviderModelRq()
            {
                Name = "VinaPhone",
                NetworkProviderPrefixes = new List<NetworkProviderPrefixModelRq>()
                    {
                        new NetworkProviderPrefixModelRq(){Prefix = "088"},
                        new NetworkProviderPrefixModelRq(){Prefix = "091"},
                        new NetworkProviderPrefixModelRq(){Prefix = "094"},
                    }
            });

            Console.WriteLine("Auto generate Mobile Numbers");
            var network_providers = await _networkProviderService.GetNetworkProvidersAsync();
            foreach(var provider in network_providers)
            {
                Console.WriteLine("Auto generate Mobile Numbers for " + provider.Name);
                foreach (var prefix in provider.NetworkProviderPrefixModels)
                {
                    for(int i = 0; i < 100; i++)
                    {
                        var number_model = new MobileNumberModelRq()
                        {
                            MobileNumber1 = prefix.Prefix + AutoGenerate.AutoGenerate.AutoGenerateNumber(7),
                            NetworkProviderId = prefix.NetworkProviderId
                        };
                        await _mobileNumberService.CreateMobileNumberAsync(number_model);
                    }
                }
            }

            Console.WriteLine("Generate Mobile Number is Done!");
            Console.WriteLine("");
            Console.WriteLine("List of Mobile Number Generated:");
            var numbers = await _mobileNumberService.GetMobileNumbersAsync();

            for (int i = 0; true; i++)
            {
                if (i >= numbers.Count())
                {
                    break;
                }
                Console.Write(numbers.ElementAt(i).MobileNumber1 + " ");
                if((i + 1) % 9 == 0)
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Printing All Numbers End!");

            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("");

            Console.WriteLine("Good feng shui numbers:");

            var configurations = _fengShuiConfiguration.GetFengShuiConfiguration();

            Console.ReadLine();
        }
    }
}
