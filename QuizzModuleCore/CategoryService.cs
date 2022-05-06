using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzModuleCore
{
    public class CategoryService
    {
        public List<Category> Categories { get; private set; }

        public CategoryService()
        {
            Categories = new List<Category>();
        }

        public void CreateCategory(string name)
        {
            Categories.Add(new Category(name));
        }

        public void CreateCategory(string name, Category category)
        {
            category.Categories.Add(new Category(name));
        }

        public void DeleteCategory(Category category, List<Category> categories)
        {
            if (Categories.Remove(category))
                return;
            foreach (var c in categories)
            {
                c.Categories.Remove(category);
                DeleteCategory(category, c.Categories);
            }
        }
    }
}
