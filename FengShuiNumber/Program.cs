using Autofac;
using AutoMapper;
using FengShuiNumber.Common.AppSetting;
using FengShuiNumber.Common.FengShuiConfiguration;
using FengShuiNumber.Controllers;
using FengShuiNumber.IRepositories;
using FengShuiNumber.IServices;
using FengShuiNumber.ModelRequests;
using FengShuiNumber.ModelResponses;
using FengShuiNumber.Models;
using FengShuiNumber.Repositories;
using FengShuiNumber.Services;
using System.Threading.Tasks;

namespace FengShuiNumber
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AppSetting>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterType<FengShuiConfiguration>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterType<FengShuiNumberDbContext>().AsSelf();

            containerBuilder.Register(
                c => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new NetworkProviderModelRqMapper());
                    cfg.AddProfile(new NetworkProviderModelMapper());
                    cfg.AddProfile(new MobileNumberModelRqMapper());
                    cfg.AddProfile(new MobileNumberModelMapper());
                    cfg.AddProfile(new NetworkProviderPrefixModelRqMapper());
                    cfg.AddProfile(new NetworkProviderPrefixModelMapper());
                }))
                .AsSelf()
                .SingleInstance();

            containerBuilder.Register(
                c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();


            //register repositories
            containerBuilder.RegisterType<NetworkProviderRepository>().As<INetworkProviderRepository>();
            containerBuilder.RegisterType<MobileNumberRepository>().As<IMobileNumberRepository>();
            containerBuilder.RegisterType<NetworkProviderPrefixRepository>().As<INetworkProviderPrefixRepository>();

            //register services
            containerBuilder.RegisterType<NetworkProviderService>().As<INetworkProviderService>();
            containerBuilder.RegisterType<MobileNumberService>().As<IMobileNumberService>();
            containerBuilder.RegisterType<NetworkProviderPrefixService>().As<INetworkProviderPrefixService>();

            //controllers
            containerBuilder.RegisterType<FengShuiNumberController>().AsSelf();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var controller = scope.Resolve<FengShuiNumberController>();

                await controller.RunFengShuiAsync();
            }
        }
    }
}
