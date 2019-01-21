
using AutoMapper;
using HC.DZWechat.VipPurchases;
using HC.DZWechat.VipPurchases.Dtos;

namespace HC.DZWechat.VipPurchases.Mapper
{

	/// <summary>
    /// 配置VipPurchase的AutoMapper
    /// </summary>
	internal static class VipPurchaseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <VipPurchase,VipPurchaseListDto>();
            configuration.CreateMap <VipPurchaseListDto,VipPurchase>();

            configuration.CreateMap <VipPurchaseEditDto,VipPurchase>();
            configuration.CreateMap <VipPurchase,VipPurchaseEditDto>();

        }
	}
}
