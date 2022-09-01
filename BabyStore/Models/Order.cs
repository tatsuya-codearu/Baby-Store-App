using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{
    public class Order
    {
        [Display(Name = "OrderId")]
        public int OrderID { get; set; }
        [Display(Name = "Users")]
        public string UserID { get; set; }
        [Display(Name = "Delivery")]
        public string DeliveryName { get; set; }
        [Display(Name = "Delivery Address")]
        public Address DeliveryAddress { get; set; }
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Time of Order:")]
        public DateTime DateCreated { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}