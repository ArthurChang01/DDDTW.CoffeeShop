using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses
{
    public class OrderResp : PropertyComparer<OrderResp>
    {
        #region Constructors

        public OrderResp()
        {
        }

        public OrderResp(string id, OrderStatus status, IEnumerable<OrderItemResp> items, DateTimeOffset createdDate, DateTimeOffset? modifiedDate = null)
        {
            this.Id = id;
            this.Status = status.ToString();
            this.Items = items;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
        }

        public OrderResp(Order order)
        {
            this.Id = order.Id.ToString();
            this.Status = order.Status.ToString();
            this.Items = order.OrderItems.Select(o => new OrderItemResp(o));
            this.CreatedDate = order.CreatedDate;
            this.ModifiedDate = order.ModifiedDate;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; private set; }

        public string Status { get; private set; }

        public IEnumerable<OrderItemResp> Items { get; private set; }

        public DateTimeOffset CreatedDate { get; private set; }

        public DateTimeOffset? ModifiedDate { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Status;
            foreach (var item in this.Items)
            {
                yield return item;
            }
            yield return this.CreatedDate;
            yield return this.ModifiedDate;
        }
    }
}