using AutoMapper;
using BankingAppAuthenticationTier.Contracts.Providers;
using BankingAppAuthenticationTier.MapperProfiles;

namespace BankingAppAuthenticationTier.Providers
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
                cfg.AddProfile<ClientsMapperProfile>();
                cfg.AddProfile<TokensMapperProfile>();
            });

            mapper = configuration.CreateMapper();
        }

        public Target Map<Source, Target>(Source source)
        {
            return mapper.Map<Target>(source);
        }
    }
}
