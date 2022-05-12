using FinalProject.Models;

namespace FinalProject.Services
{
    public interface IOrderRepos
    {
        public List<Order> GetAll();
        public Order GetDetails(int id);
        public void Insert(Order Order);
        public void UpdateOrder(int id, Order Order);
        public void DeleteOrder(int id);
    }
}
