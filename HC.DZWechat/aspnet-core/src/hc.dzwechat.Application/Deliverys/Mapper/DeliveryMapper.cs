
using AutoMapper;
using HC.DZWechat.Deliverys;
using HC.DZWechat.Deliverys.Dtos;

namespace HC.DZWechat.Deliverys.Mapper
{

	/// <summary>
    /// 配置Delivery的AutoMapper
    /// </summary>
	internal static class DeliveryMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Delivery,DeliveryListDto>();
            configuration.CreateMap <DeliveryListDto,Delivery>();

            configuration.CreateMap <DeliveryEditDto,Delivery>();
            configuration.CreateMap <Delivery,DeliveryEditDto>();

        }
	}
}
