
using AutoMapper;
using HC.DZWechat.VipUsers;
using HC.DZWechat.VipUsers.Dtos;

namespace HC.DZWechat.VipUsers.Mapper
{

	/// <summary>
    /// 配置VipUser的AutoMapper
    /// </summary>
	internal static class VipUserMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <VipUser,VipUserListDto>();
            configuration.CreateMap <VipUserListDto,VipUser>();

            configuration.CreateMap <VipUserEditDto,VipUser>();
            configuration.CreateMap <VipUser,VipUserEditDto>();

        }
	}
}
