using FinalProject.Models;


namespace FinalProject.Services
{
    public class CategoryServiceRepos : ICategoryRepos
    {
        public PurchaseDbContext Context { get; set; }

        public CategoryServiceRepos(PurchaseDbContext context)
        {
            Context = context;
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetDetails(int id)
        {
            return Context.Categories.Find(id);
        }

        public void Insert(Category Category)
        {
            Context.Categories.Add(Category);
            Context.SaveChanges();
        }

        public void UpdateCatg(int id, Category Category)
        {
            Category CatUpdated = Context.Categories.Find(id);
            CatUpdated.Name = Category.Name;
            CatUpdated.ID = Category.ID;

            Context.SaveChanges();
        }
        public void DeleteCatg(int id)
        {
            Context.Remove(Context.Categories.Find(id));
            Context.SaveChanges();
        }
    }
}