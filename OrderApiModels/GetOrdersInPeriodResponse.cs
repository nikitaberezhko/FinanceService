using OrderApiModels.ApiModels;

namespace OrderApiModels;

public class GetOrdersInPeriodResponse
{
    public List<OrderApiFullModel> Orders { get; set; }
}