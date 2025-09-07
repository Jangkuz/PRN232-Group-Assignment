namespace AirWaterStore.Web.Services;

public interface IOrderService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);

    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrdersByCustomerIdResponse> GetOrdersByCustomerId(int customerId);

    [Get("/ordering-service/orders/{orderId}")]
    Task<GetOrderByIdResponse> GetOrderById(string orderId);

    [Get("/ordering-service/orders/count")]
    Task<GetOrdersCountResponse> GetTotalCountAsync();

    //================================


    [Get("/ordering-service/orders/{orderId}")]
    Task<GetOrderByIdResponse> GetOrderDetailById(string orderId);
}
