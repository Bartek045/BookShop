using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string product)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrEmpty(product))
            {
                // Filter products by category name
                products = _unitOfWork.Product.GetAll(filter: p => p.Category.Name == product, includeProperties: "Category");
            }
            else
            {
                // Get all products with their associated categories
                products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            }

            ViewBag.Product = product;
            return View(products);
        }
    }
}
