using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc) { repository = repo; orderProcessor = proc; }

        public ViewResult Index(Cart cart, string returnUrl)
        
            {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.lines.Count() == 0) { ModelState.AddModelError("", "Sorry, your cart is empty!"); }

            if (ModelState.IsValid) { orderProcessor.ProcessOrder(cart, shippingDetails); cart.Clear(); return View("Completed"); } else { return View(shippingDetails); }
        }
        //private cart getcart()
        //{

        //    cart cart = (cart)session["cart"];
        //    if (cart == null)
        //    {
        //        cart = new cart(); session["cart"] = cart;
        //    }
        //    return cart;
        //}
        /*
         There are a few points to note about this controller. The first is that I use the ASP.NET session state feature to store and 
         retrieve Cart objects. This is the purpose of the GetCart method. ASP.NET has a nice session feature that uses cookies or URL 
         rewriting to associate multiple requests from a user together to form a single browsing session.  A related feature is session 
         state, which associates data with a session. This is an ideal fit for the Cart class. I want each user to have their own cart, 
         and I want the cart to be persistent between requests. Data associated with a session is deleted when a session expires 
         (typically because a user has not made a request for a while), which means that I do not need to manage the storage or 
         life cycle of the Cart objects. To add an object to the session state, I set the value for a key on the Session object, like this:
            ... Session["Cart"] = cart; ...
            To retrieve an object again, I simply read the same key, like this:
            ... Cart cart = (Cart)Session["Cart"];
         */
    }
}