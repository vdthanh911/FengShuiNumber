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
        public FengShuiNumberController(INetworkProviderService networkProviderService,
            INetworkProviderPrefixService networkProviderPrefix,
            IMobileNumberService mobileNumberService,
            IFengShuiConfiguration fengShuiConfiguration
            )
        {
            _networkProviderService = networkProviderService;
            _networkProviderPrefix = networkProviderPrefix;
            _mobileNumberService = mobileNumberService;
        }

        public async Task RunFengShuiAsync()
        {
            //Console.WriteLine("Clear Mobile Number");
            //await _mobileNumberService.ClearMobileNumberAsync();

            //Console.WriteLine("Clear Network Provider");
            //await _networkProviderService.ClearNetworkProviderAsync();

            if (await _networkProviderService.CountNetworkProviderAsync() == 0)
            {
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
            }

            Console.WriteLine("Auto generate Mobile Numbers");
            var network_providers = await _networkProviderService.GetNetworkProvidersAsync();
            foreach(var provider in network_providers)
            {
                Console.WriteLine("Auto generate Mobile Numbers for " + provider.Name);
                foreach (var prefix in provider.NetworkProviderPrefixModels)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        var number_model = new MobileNumberModelRq()
                        {
                            MobileNumber1 = prefix.Prefix + AutoGenerate.AutoGenerate.AutoGenerateNumber(7),
                            NetworkProviderId = prefix.NetworkProviderId
                        };
                        await _mobileNumberService.CreateMobileNumberAsync(number_model);
                    }
                    var feng_shui = new MobileNumberModelRq()
                    {
                        MobileNumber1 = _mobileNumberService.MakeFengShuiNumber(prefix.Prefix),
                        NetworkProviderId = prefix.NetworkProviderId
                    };
                    await _mobileNumberService.CreateMobileNumberAsync(feng_shui);
                }
            }

            Console.WriteLine("Generate Mobile Number is Done!");
            Console.WriteLine("");
            Console.WriteLine("List of Mobile Number In the Database:");
            var numbers = await _mobileNumberService.GetMobileNumbersAsync();
            int number_count = numbers.Count();
            for (int i = 0; true; i++)
            {
                if (i >= number_count)
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

            var fengshui_numbers = await _mobileNumberService.GetFengShuiMobileNumbersAsync();
            int fengshui_count = fengshui_numbers.Count();
            if(fengshui_count > 0)
            {
                for (int i = 0; true; i++)
                {
                    if (i >= fengshui_count)
                    {
                        break;
                    }
                    Console.Write(fengshui_numbers.ElementAt(i).MobileNumber1 + " ");
                    if ((i + 1) % 9 == 0)
                    {
                        Console.WriteLine("");
                    }
                }
            }
            else
            {
                Console.WriteLine("There is not any feng shui number in the database!");
            }

            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Tool for check Feng Shui Number");

            var prefixes = await _networkProviderPrefix.GetNetworkProviderPrefixesAsync();
            while (true)
            {
                Console.Write("Please Input a Mobile Number:");
                string mobile_number = Console.ReadLine();
                bool is_mobile_number = this.IsMobileNumber(mobile_number, prefixes);
                Console.WriteLine("");
                if (is_mobile_number)
                {
                    bool is_feng_shui = _mobileNumberService.CheckFengShuiMobileNumber(mobile_number);
                    if (is_feng_shui)
                    {
                        Console.WriteLine("Congratulation! You have Input a correct Feng Shui Mobile Number");
                    }
                    else
                    {
                        Console.WriteLine("It's not a Feng Shui Mobile Number");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Mobile Number! Please try again");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                
                Console.WriteLine("Press any key to Continue or Esc to Exit");
                var k = Console.ReadKey();
                if(k.Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }


            Console.WriteLine(Environment.NewLine + "Programmer feng shui number is done!");

            Console.ReadLine();
        }

        private bool IsMobileNumber(string mobile_number, IEnumerable<ModelResponses.NetworkProviderPrefixModel> prefixes)
        {
            return mobile_number.Length == 10 
                && prefixes.Select(r => r.Prefix).Contains(mobile_number.Substring(0,3))
                && int.TryParse(mobile_number, out _);
        }
    }
}
