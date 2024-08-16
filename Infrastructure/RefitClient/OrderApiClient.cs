using CommonModel.Contracts;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;
using Services.RefitClients.Interfaces;

namespace Infrastructure.RefitClient;

public class OrderApiClient(IOrderApi orderApi) : IOrderApiClient
{
    public async Task<CommonResponse<GetOrdersInPeriodResponse>> GetOrdersInPeriod(
        GetOrdersInPeriodRequest request)
    {
        return await orderApi.GetOrdersInPeriod(request);
    }
}