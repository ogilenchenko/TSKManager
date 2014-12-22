using AutoMapper;
using TM.Database.Models;
using TM.Domain.Models;

namespace TM.API
{
    public class AutoMapperConfig
    {
        public static void CreateMappings()
        {
            // Lookup View Models
            Mapper.CreateMap<RefreshToken, RefreshTokenModel>();
            Mapper.CreateMap<RefreshTokenModel, RefreshToken>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}