using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IOrderService
    {
        void Create(OrderDto orderDto);

        void Edit(OrderDto orderDto);

        OrderDto GetSingleBy(string customerId);
    }
}