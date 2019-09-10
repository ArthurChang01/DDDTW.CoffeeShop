using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels
{
    public class OrderVM : PropertyComparer<OrderVM>
    {
        #region Constructors

        public OrderVM()
        {
        }

        public OrderVM(string id, OrderStatus status, IEnumerable<OrderItemVM> items, DateTimeOffset createdDate, DateTimeOffset? modifiedDate)
        {
            this.Id = id;
            this.Status = status.ToString();
            this.Items = items;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
        }

        public OrderVM(Domain.Orders.Models.Order order)
        {
            this.Id = order.Id.ToString();
            this.Status = order.Status.ToString();
            this.Items = order.OrderItems.Select(o => new OrderItemVM(o));
            this.CreatedDate = CreatedDate;
            this.ModifiedDate = ModifiedDate;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderItemVM> Items { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        #endregion Properties

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