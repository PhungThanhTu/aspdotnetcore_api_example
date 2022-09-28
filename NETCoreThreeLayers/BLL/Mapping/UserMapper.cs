using AutoMapper;
using BLL.DTOs.Users;
using DAL.Aggregates;

namespace BLL.Mapping
{
    public class UserMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserDTO>();
            config.CreateMap<RegisterRequest, User>().AfterMap((source, destination) => destination.Id = Guid.NewGuid());
        }
    }
}