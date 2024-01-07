using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string category)
        {
            IEnumerable<Product> productlist;

            if(!string.IsNullOrEmpty(category))
            {
                productlist = _unitOfWork.Product.GetAll(filter: lm => lm.Category.Name == category, includeProperties: "Category");
            }

            else
            {
                productlist = _unitOfWork.Product.GetAll(includeProperties: "Category");
            }
            ViewBag.Category = category;

            return View(productlist);
        }
    }
}
