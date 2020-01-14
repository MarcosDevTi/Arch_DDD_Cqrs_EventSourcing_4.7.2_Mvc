using AutoMapper;

namespace Arch.CqrsClient.AutoMapper
{
    public interface ICustomMapper
    {
        void Map(IMapperConfigurationExpression config);
    }
}
