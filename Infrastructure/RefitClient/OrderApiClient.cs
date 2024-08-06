using OrderApiModels;
using OrderApiModels.ApiModels;
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