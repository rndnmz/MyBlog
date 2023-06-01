using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;

namespace MyBlog.WebUI.Views.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            CategoryManager manager = new CategoryManager();
            // 1.Yol
            List<Category> categories = manager.List();
            return View(categories);
        }
    }
}
