using AutoMapper;
using FengShuiNumber.Common.FengShuiConfiguration;
using FengShuiNumber.IRepositories;
using FengShuiNumber.IServices;
using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using FengShuiNumber.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FengShuiNumber.Services
{
    public class MobileNumberService : IMobileNumberService
    {
        private readonly IMobileNumberRepository _mobileNumberRepository;
        private readonly IMapper _mapper;
        private readonly IFengShuiConfiguration _fengShuiConfiguration;
        private Random rd = new Random();
        public MobileNumberService(IMobileNumberRepository mobileNumberRepository,
            IFengShuiConfiguration fengShuiConfiguration,
             IMapper mapper
            )
        {
            _fengShuiConfiguration = fengShuiConfiguration;
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

        public async Task<IEnumerable<MobileNumberModel>> GetFengShuiMobileNumbersAsync()
        {
            var configuration = _fengShuiConfiguration.GetFengShuiConfiguration();
            var data = await _mobileNumberRepository.GetMobileNumbersAsync();
            var numbers_are_not_taboo = data.Where(r => !(configuration.Taboos.Contains(r.MobileNumber1.Substring(r.MobileNumber1.Length - 2))));

            var numbers_with_createria = numbers_are_not_taboo.Where(r => CheckFengShuiMobileNumber(r.MobileNumber1));

            //var numbers_with_createria = numbers_are_not_taboo.Where(r => r.MobileNumber1
            //    .Substring(0, 5)
            //    .ToCharArray()
            //    .Select(s => int.Parse(s.ToString()))
            //    .Sum() == configuration.Criteria.Total.First5Nums
            //    && (
            //        r.MobileNumber1
            //        .Substring(5)
            //        .ToCharArray()
            //        .Select(s => int.Parse(s.ToString()))
            //        .Sum() == configuration.Criteria.Total.Last5Nums[0]
            //        ||
            //        r.MobileNumber1
            //        .Substring(5)
            //        .ToCharArray()
            //        .Select(s => int.Parse(s.ToString()))
            //        .Sum() == configuration.Criteria.Total.Last5Nums[1]
            //    )

            //    );
            return _mapper.Map<IEnumerable<MobileNumberModel>>(numbers_with_createria);
        }

        public bool CheckFengShuiMobileNumber(string mobileNumber)
        {
            var configuration = _fengShuiConfiguration.GetFengShuiConfiguration();
            bool b_result = !(configuration.Taboos.Contains(mobileNumber.Substring(mobileNumber.Length - 2)));
            if (b_result)
            {
                bool creteria = mobileNumber.Substring(0, 5)
                    .ToCharArray()
                    .Select(s => int.Parse(s.ToString()))
                    .Sum() == configuration.Criteria.Total.First5Nums
                    && (
                        mobileNumber
                        .Substring(5)
                        .ToCharArray()
                        .Select(s => int.Parse(s.ToString()))
                        .Sum() == configuration.Criteria.Total.Last5Nums[0]
                        ||
                        mobileNumber
                        .Substring(5)
                        .ToCharArray()
                        .Select(s => int.Parse(s.ToString()))
                        .Sum() == configuration.Criteria.Total.Last5Nums[1]
                    )
                    && configuration.Criteria.LastNicePairOfNumbers.Contains(int.Parse(mobileNumber.Substring(mobileNumber.Length - 2)))
                    ;
                b_result = creteria;
            }
            return b_result;
        }

        public string MakeFengShuiNumber(string prefix)
        {
            var configuration = _fengShuiConfiguration.GetFengShuiConfiguration();
            int prefix_sum = GetStringNumberSum(prefix);
            string last2_prefix = GetRandomNumber(configuration.Criteria.Total.First5Nums - prefix_sum, 2);
            string s5_prefix = prefix + last2_prefix;
            string last2 = configuration.Criteria.LastNicePairOfNumbers[rd.Next(0, configuration.Criteria.LastNicePairOfNumbers.Count)].ToString();
            int last2_total = GetStringNumberSum(last2);
            int last5_total = configuration.Criteria.Total.Last5Nums[rd.Next(0, configuration.Criteria.Total.Last5Nums.Count)];
            int last3_total = last5_total - last2_total;
            string last3 = GetRandomNumber(last3_total, 3);
            string last5 = last3 + last2;
            return s5_prefix + last5;
        }

        private string GetRandomNumber(int total, int lenght)
        {
            string s = AutoGenerate.AutoGenerate.AutoGenerateNumber(lenght);
            if(GetStringNumberSum(s) == total)
            {
                return s;
            }
            return GetRandomNumber(total, lenght);
        }

        private int GetStringNumberSum(string s)
        {
            return s.ToCharArray().Select(s => int.Parse(s.ToString())).Sum();
        }
    }
}
