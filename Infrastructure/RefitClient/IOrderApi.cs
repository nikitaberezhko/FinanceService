using CommonModel.Contracts;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;
using Refit;

namespace Infrastructure.RefitClient;

public interface IOrderApi
{
    [Get("/api/v1/orders/periods")]
    Task<CommonResponse<GetOrdersInPeriodResponse>> GetOrdersInPeriod(
        [Query] GetOrdersInPeriodRequest request);
}