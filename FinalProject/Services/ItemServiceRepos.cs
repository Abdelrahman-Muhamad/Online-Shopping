
using FinalProject.Models;

namespace FinalProject.Services
{
    public class ItemServiceRepos : IItemRepos
    {
        public PurchaseDbContext Context { get; set; }

        public ItemServiceRepos(PurchaseDbContext context)
        {
            Context = context;
        }

        public List<Item> GetAll()
        {
            return Context.Items.ToList();
        }

        public Item GetDetails(int id)
        {
            return Context.Items.Find(id);
        }

        public void Insert(Item Item)
        {
            Context.Items.Add(Item);
            Context.SaveChanges();
        }

        public void UpdateItem(int id, Item Item)
        {
            Item ItemUpdated = Context.Items.Find(id);
            ItemUpdated.Name=Item.Name;
            ItemUpdated.Price=Item.Price;
            ItemUpdated.Description=Item.Description;
            ItemUpdated.InStock=Item.InStock;
            ItemUpdated.Details = Item.Details;
            ItemUpdated.ID=Item.ID;
            ItemUpdated.CategoryID=Item.CategoryID;
            ItemUpdated.Photopath = Item.Photopath;
            

            Context.SaveChanges();
        }
        public void DeleteItem(int id)
        {
            Context.Remove(Context.Items.Find(id));
            Context.SaveChanges();
        }

    }
}
