
using FinalProject.Models;

namespace FinalProject.Services
{
    public interface ICategoryRepos
    {
        public List<Category> GetAll();
        public Category GetDetails(int id);
        public void Insert(Category Category);
        public void UpdateCatg(int id, Category Category);
        public void DeleteCatg(int id);
    }
}
