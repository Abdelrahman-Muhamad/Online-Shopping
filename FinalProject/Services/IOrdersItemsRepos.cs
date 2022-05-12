using FinalProject.Models;

namespace FinalProject.Services
{
    public interface IOrdersItemsRepos
    {
        public List<OrderItem> GetAll();
        public OrderItem GetDetails(int Order_id, int Item_id);
        public void Insert(OrderItem OrdersItem);
        public void UpdateOrderItems(int Order_id, int Item_id, OrderItem OrderItem);
        public void DeleteOrderItems(int Order_id, int Item_id);
    }
}
