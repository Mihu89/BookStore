using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart
    {       [Key]
        public int Id { get; set; }
        private List<CartLine> _linesCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get
            {
                return _linesCollection;
            }
        }
        public Client Client { get; set; }

        public void AddItem(Product product, int quantity)
        {
            var line = _linesCollection.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (line == null)
            {
                _linesCollection.Add(new CartLine{
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveItem (Product product)
        {
            _linesCollection.RemoveAll(x => x.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return _linesCollection.Sum(x => x.Product.Price * x.Quantity);
        }

        public void Clear()
        {
            _linesCollection.Clear();
        }
    }
}
