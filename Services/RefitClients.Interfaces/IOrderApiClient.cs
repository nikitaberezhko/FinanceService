using CommonModel.Contracts;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;

namespace Services.RefitClients.Interfaces;

public interface IOrderApiClient
{
    Task<CommonResponse<GetOrdersInPeriodResponse>> GetOrdersInPeriod(
        GetOrdersInPeriodRequest request);
}