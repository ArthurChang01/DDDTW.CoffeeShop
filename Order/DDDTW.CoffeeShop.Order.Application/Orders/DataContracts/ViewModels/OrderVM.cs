using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels
{
    public class OrderVM : ValueObject<OrderVM>
    {
        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Status;
            yield return this.Items;
            yield return this.CreatedDate;
            yield return this.ModifiedDate;
        }
    }
}