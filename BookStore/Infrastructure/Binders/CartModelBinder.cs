using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace BookStore.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (modelBindingExecutionContext.HttpContext.Session != null)
            {
                cart = modelBindingExecutionContext.HttpContext.Session[sessionKey] as Cart; 
            }
            if (cart == null)
            {
                cart = new Cart();
                if (modelBindingExecutionContext.HttpContext.Session != null)
                {
                    modelBindingExecutionContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return true;
        }
    }
}