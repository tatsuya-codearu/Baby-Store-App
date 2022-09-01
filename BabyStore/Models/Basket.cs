using BabyStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyStore.Models
{
    public class Basket
    {
        //setting the CartID
        private string BasketID { get; set; }
        //setting the const string key to BasketID
        private const string key = "BasketID";
        //setting the StoreContext db
        private StoreContext db = new StoreContext();

        private string GetBasketID()
        {
            if (HttpContext.Current.Session[key] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[key] =
                 HttpContext.Current.User.Identity.Name;
                }
                else {
                    Guid tempBasketID = Guid.NewGuid();
                    HttpContext.Current.Session[key] = tempBasketID.ToString();
                }
            }
            return HttpContext.Current.Session[key].ToString();
        }

        public static Basket GetBasket()
        {
            Basket basket = new Basket();
            basket.BasketID = basket.GetBasketID();
            return basket;
        }

        public void AddToBasket(int productID, int quantity)
        {
            var basketLine = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID && b.ProductID
             == productID);

            if (basketLine == null)
            {
                basketLine = new BasketLine
                {
                    ProductID = productID,
                    BasketID = BasketID,
                    Quantity = quantity,
                    DateCreated = DateTime.Now
                };
                db.BasketLines.Add(basketLine);
            }
            else
            {
                basketLine.Quantity += quantity;
            }
            db.SaveChanges();
        }

        public void RemoveLine(int productID)
        {
            var basketLine = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID && b.ProductID
             == productID);
            if (basketLine != null)
            {
                db.BasketLines.Remove(basketLine);
            }
            db.SaveChanges();
        }

        public void UpdateBasket(List<BasketLine> lines)
        {
            foreach (var line in lines)
            {
                var basketLine = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID &&
                 b.ProductID == line.ProductID);
                if (basketLine != null)
                {
                    if (line.Quantity == 0)
                    {
                        RemoveLine(line.ProductID);
                    }
                    else
                    {
                        basketLine.Quantity = line.Quantity;
                    }
                }
            }
            db.SaveChanges();
        }

        //method to empty the basket
        public void EmptyBasket()
        {
            var basketLines = db.BasketLines.Where(b => b.BasketID == BasketID);
            foreach (var basketLine in basketLines)
            {
                db.BasketLines.Remove(basketLine);
            }
            db.SaveChanges();
        }

        public List<BasketLine> GetBasketLines()
        {
            return db.BasketLines.Where(b => b.BasketID == BasketID).ToList();
        }

        public decimal GetTotalCost()
        {
            decimal basketTotal = decimal.Zero;

            if (GetBasketLines().Count > 0)
            {
                basketTotal = db.BasketLines.Where(b => b.BasketID == BasketID).Sum(b => b.Product.Price
                 * b.Quantity);
            }

            return basketTotal;
        }

        public int GetNumberOfItems()
        {
            int numberOfItems = 0;
            if (GetBasketLines().Count > 0)
            {
                numberOfItems = db.BasketLines.Where(b => b.BasketID == BasketID).Sum(b => b.Quantity);
            }

            return numberOfItems;
        }

        public void MigrateBasket(string userName)
        {
            var basket = db.BasketLines.Where(b => b.BasketID == BasketID).ToList();

            //check to see if there is already a cart or not
            var usersBasket = db.BasketLines.Where(b => b.BasketID == userName).ToList();

            //if theres a cart then the item will be added
            if (usersBasket != null)
            {
                //cartId to the username
                string prevID = BasketID;
                BasketID = userName;
                
                foreach (var line in basket)
                {
                    AddToBasket(line.ProductID, line.Quantity);
                }
                
                BasketID = prevID;EmptyBasket();
            }else{
               
                foreach (var basketLine in basket)
                {
                    basketLine.BasketID = userName;
                }

                db.SaveChanges();
            }
            HttpContext.Current.Session[key] = userName;
        }

        public decimal CreateOrderLines(int orderID)
        {
            decimal orderTotal = 0;

            var basketLines = GetBasketLines();

            foreach (var item in basketLines)
            {
                OrderLine orderLine = new OrderLine
                {
                    Product = item.Product,
                    ProductID = item.ProductID,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    OrderID = orderID
                };
orderTotal += (item.Quantity * item.Product.Price);
                db.OrderLines.Add(orderLine);
            }

            db.SaveChanges();
            EmptyBasket();
            return orderTotal;
        }
    }
}
