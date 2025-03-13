using AutoMapper;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;

namespace BankingAppDataTier.Providers
{
    public class MapperProvider : IMapperProvider
    {
        private IMapper mapper;

        public MapperProvider()
        {
            var configuration = new MapperConfiguration(cfg => 
            {
                cfg.AllowNullCollections = true;
                //cfg.Advanced.AllowAdditiveTypeMapCreation = true;
                cfg.AddProfile<CommonMapperProfile>();

                cfg.AddProfile<ClientsMapperProfile>();
                cfg.AddProfile<AccountsMapperProfile>();
                cfg.AddProfile<PlasticsMapperProfile>();
            });

            // only during development, validate your mappings; remove it before release
            #if DEBUG
            configuration.AssertConfigurationIsValid();
            #endif

            mapper = configuration.CreateMapper();
        }

        public Target Map<Source, Target>(Source source)
        {
            return mapper.Map<Target>(source);
        }
    }
}
