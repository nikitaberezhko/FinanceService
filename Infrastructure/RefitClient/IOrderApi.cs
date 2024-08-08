using OrderApiModels;
using OrderApiModels.ApiModels;
using Refit;

namespace Infrastructure.RefitClient;

public interface IOrderApi
{
    [Get("/api/v1/orders/periods")]
    Task<CommonResponse<GetOrdersInPeriodResponse>> GetOrdersInPeriod(
        [Query] GetOrdersInPeriodRequest request);
}