
using FinalProject.Models;

namespace FinalProject.Services
{
    public class OrderServiceRepos : IOrderRepos
    {
        public PurchaseDbContext Context { get; set; }

        public OrderServiceRepos(PurchaseDbContext context)
        {
            Context = context;
        }

        public List<Order> GetAll()
        {
            return Context.Orders.ToList();
        }

        public Order GetDetails(int id)
        {
            return Context.Orders.Find(id);
        }

        public void Insert(Order Order)
        {
            Context.Orders.Add(Order);
            Context.SaveChanges();
        }

        public void UpdateOrder(int id, Order Order)
        {
            Order OrderUpdated = Context.Orders.Find(id);
            OrderUpdated.ID = Order.ID;
            OrderUpdated.TotalCost = Order.TotalCost;
            OrderUpdated.Date = Order.Date;
            OrderUpdated.CustomerID = Order.CustomerID;

            Context.SaveChanges();
        }
        public void DeleteOrder(int id)
        {
            Context.Remove(Context.Orders.Find(id));
            Context.SaveChanges();
        }
    }
}
