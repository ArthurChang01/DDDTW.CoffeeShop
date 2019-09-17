﻿using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.DomainEvents
{
    public class OrderCreated : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderCreated(OrderId id, string tableNo, IEnumerable<OrderItem> orderItems, DateTimeOffset createdDate)
            : base(id)
        {
            this.TableNo = tableNo;
            this.OrderItems = orderItems;
            this.CreatedDate = createdDate;
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string TableNo { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.OrderItems)
            {
                yield return item;
            }

            yield return this.CreatedDate;
        }
    }
}