using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _productRepository;
        private IOrderProcessor _orderProcessor;

        public CartController(IProductRepository productRepository, IOrderProcessor orderProcessor)
        {
            _productRepository = productRepository;
            _orderProcessor = orderProcessor;
        }
        // GET: Cart
        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = HttpContext.Session["Cart"] as Cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult AddToCart(Cart cart, Guid Id, string returnUrl)
        {
            // check into repository for existing product id
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == Id);
            cart = HttpContext.Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
            }
            if (product != null)
            {
                cart.AddItem(product, 1);
                HttpContext.Session["Cart"] = cart;
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(Cart cart, Guid? Id, string returnUrl)
        {
            if (Id == null || Id == Guid.Empty)
            {
                ModelState.AddModelError("", "Product is missing");
                RedirectToAction("Index", new { returnUrl });
            }
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == Id);
            cart = HttpContext.Session["Cart"] as Cart;
            
            if (product != null && cart != null)
            {
                cart.RemoveItem(product);
                HttpContext.Session["Cart"] = cart;
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            
            return View(new ShippingDetail() { 
            OrderId = _productRepository.GetLastOrder() +1});
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetail shippingDetail)
        {
            cart = HttpContext.Session["Cart"] as Cart;
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                // save Order
                _productRepository.SaveOrder(new Order
                {
                    Id = _productRepository.GetLastOrder(),
                    Date = DateTime.UtcNow
                });
                _orderProcessor.ProcessOrder(cart, shippingDetail);
                cart.Clear();
                HttpContext.Session["Cart"] = null;
                return View("Complete");
            }
            else
            {
                return View(shippingDetail);
            }
        }
    }
}