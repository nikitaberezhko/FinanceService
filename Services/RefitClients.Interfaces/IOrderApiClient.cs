using OrderApiModels;
using OrderApiModels.ApiModels;

namespace Services.RefitClients.Interfaces;

public interface IOrderApiClient
{
    Task<CommonResponse<GetOrdersInPeriodResponse>> GetOrdersInPeriod(
        GetOrdersInPeriodRequest request);
}