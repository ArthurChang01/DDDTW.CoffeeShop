﻿using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class CancelOrderMsg : IRequest<Unit>
    {
        public CancelOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}