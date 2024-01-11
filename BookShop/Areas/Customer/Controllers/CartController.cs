
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var cartProducts = _unitOfWork.Product.GetAll(filter: p => p.QuantityInCart > 0, includeProperties: "Category");
            var totalItemsInCart = cartProducts.Sum(p => p.QuantityInCart);
            TempData["TotalItemsInCart"] = totalItemsInCart;

            return View(cartProducts);
        }
        public IActionResult Delivery()
        {
            return View();
        }


        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }
        [HttpPost]
        public IActionResult AddToCart(ProductData productData)
        {

            var product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == productData.ProductId, includeProperties: "Category");

            if (product != null)
            {

                product.QuantityInCart += productData.Quantity;


                _unitOfWork.Save();


                var cartProducts = _unitOfWork.Product.GetAll(filter: p => p.QuantityInCart > 0, includeProperties: "Category");


                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Category");

            if (product != null)
            {
                // Decrease the quantity or remove from the cart as needed
                // For example, you can set QuantityInCart to 0 to remove the item
                product.QuantityInCart = 0;

                _unitOfWork.Save();

                // Redirect back to the cart page
                return RedirectToAction("Index");
            }
            
            return NotFound();
        }
        [HttpGet]
        public IActionResult GetCartItemCount()
        {
            var totalItemsInCart = TempData["TotalItemsInCart"] ?? 0;
            return Json(totalItemsInCart);
        }



    }
}
