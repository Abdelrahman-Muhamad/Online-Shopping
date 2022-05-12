using FinalProject.Models;

namespace FinalProject.Services
{
    public class OrdersItemsServiceRepos : IOrdersItemsRepos
    {
        public PurchaseDbContext Context { get; set; }

        public OrdersItemsServiceRepos(PurchaseDbContext context)
        {
            Context = context;
        }

        public List<OrderItem> GetAll()
        {
            return Context.OrderItems.ToList();
        }

        public OrderItem GetDetails(int Order_id, int Item_id)
        {
            return (OrderItem)Context.OrderItems.Where(i => i.OrderId == Order_id && i.ItemId == Item_id);
        }

        public void Insert(OrderItem OrdersItem)
        {
            Context.OrderItems.Add(OrdersItem);
            Context.SaveChanges();
        }

        public void UpdateOrderItems(int Order_ID, int item_ID, OrderItem OrdersItem)
        {
            OrderItem OrderUpdated = (OrderItem)Context.OrderItems.Where(i=> i.OrderId == Order_ID && i.ItemId == item_ID);
            OrderUpdated.ItemId = OrdersItem.ItemId;
            OrderUpdated.OrderId = OrdersItem.OrderId;

            Context.SaveChanges();
        }
        public void DeleteOrderItems(int Order_id, int Item_id)
        {
            Context.Remove((OrderItem)Context.OrderItems.Where(i => i.OrderId == Order_id && i.ItemId == Item_id));
            Context.SaveChanges();
        }
    }
}
