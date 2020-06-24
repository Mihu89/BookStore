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
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult AddToCart(Cart cart, Guid Id, string returnUrl)
        {
            // check into repository for existing product id
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == Id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(Cart cart, Guid Id, string returnUrl)
        {
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetail());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetail shippingDetail)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetail);
                cart.Clear();
                // HttpContext.Session["Cart"] = null;
                return View("Complete");
            }
            else
            {
                return View(shippingDetail);
            }
        }
    }
}