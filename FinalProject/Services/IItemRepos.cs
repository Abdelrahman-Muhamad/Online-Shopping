using FinalProject.Models;

namespace FinalProject.Services
{
    public interface IItemRepos
    {
        public List<Item> GetAll();
        public Item GetDetails(int id);
        public void Insert(Item Item);
        public void UpdateItem(int id, Item Item);
        public void DeleteItem(int id);
    }
}
